using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Diagnostics;

namespace EclDump
{
    partial class Program
    {
        static void Main(string[] args)
        {
            SetupCommandTable();
            
            string path = System.IO.Directory.GetCurrentDirectory();
            //string path = @"C:\games\DARKNESS";
            //string path = @"C:\games\TREASURE";
            //string path = @"C:\games\coab";
            //string path = @"C:\games\secret";
            //string path = @"C:\games\deathkrynn";
            //string path = @"C:\games\nwn";
            //string path = @"c:\games\buckmatrix";
            //string path = @"c:\games\buckcount";

            //FindBugTest();
            foreach (var filea in Directory.GetFiles(path, "ecl*.dax"))
            {
                TryDump(filea);
            }

            //Console.ReadKey();
        }

        static void FindBugTest()
        {
            var br = new BinaryReader(File.Open(@"c:\games\coab\ecl2_001.bin", FileMode.Open));
            byte[] data = br.ReadBytes((int)br.BaseStream.Length);
            DumpEcl(data, @"c:\games\coab\ecl2_001");
        }

        static void TryDump(string file)
        {
            Console.WriteLine(file);
            foreach (var block in GetAllBlocks(file))
            {
                byte[] data = block.data;

                Console.WriteLine("File: {0} Block: {1} Size: {2}", block.file, block.id, data.Length);

                string block_name = string.Format("{0}_{1:000}", Path.Combine(Path.GetDirectoryName(block.file), Path.GetFileNameWithoutExtension(block.file)), block.id);

                DumpBin(data, block_name);
                DumpEcl(data, block_name);

            }
        }

        static bool skipNext;
        static bool stopVM;
        internal static int ecl_offset;
        static EclData ecl_ptr;
        static string[] stringTable = new string[40] { 
            string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 
            string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 
            string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 
            string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 
         };


        private static void DumpEcl(byte[] data, string block_name)
        {
            addrDone = new Dictionary<int,string>();
            addrTodo = new Queue<int>();
            byteMap = new Dictionary<int, int>();

            ecl_offset = 0x8000;
            ecl_ptr = new EclData(data);

            EclOpp[] opps;

            opps = LoadEclOpps(1);
            int vm_run_addr_1 = opps[0].Word;

            opps = LoadEclOpps(1);
            int SearchLocationAddr = opps[0].Word;
            opps = LoadEclOpps(1);
            int PreCampCheckAddr = opps[0].Word;
            opps = LoadEclOpps(1);
            int CampInterruptedAddr = opps[0].Word;
            opps = LoadEclOpps(1);
            int ecl_initial_entryPoint = opps[0].Word;


            using (var sw = new StreamWriter(block_name + ".txt", false))
            {
                sw.WriteLine("vm_run_1          0x{0:X4}", vm_run_addr_1);
                sw.WriteLine("SearchLocation    0x{0:X4}", SearchLocationAddr);
                sw.WriteLine("PreCampCheck      0x{0:X4}", PreCampCheckAddr);
                sw.WriteLine("CampInterrupted   0x{0:X4}", CampInterruptedAddr);
                sw.WriteLine("ecl_initial_entry 0x{0:X4}", ecl_initial_entryPoint);

                AddAddr(ecl_initial_entryPoint, "StartUp");
                AddAddr(CampInterruptedAddr, "StartUp");
                AddAddr(PreCampCheckAddr, "StartUp");
                AddAddr(SearchLocationAddr, "StartUp");
                AddAddr(vm_run_addr_1, "StartUp");

                DecodeBlock(sw);
            }
        }

        static Dictionary<int, int> byteMap;
        static Dictionary<int,string> addrDone;
        static Queue<int> addrTodo;

        private static void DecodeBlock(StreamWriter sw)
        {
            while (addrTodo.Count > 0)
            {
                int addr = addrTodo.Dequeue();
                if (ecl_ptr.IsValidAddr(addr + 0x8000) &&
                    addrDone.ContainsKey(addr) == false)
                {
                    Debug.WriteLine(String.Format("Pop: {0:x4}", addr));

                    if (byteMap.ContainsKey(addr) && byteMap[addr] != addr)
                    {
                        //crazy town!
                        Debug.WriteLine(String.Format("addr {0:x4} is not aligned with precous instructions {1:x4}", addr, byteMap[addr]));
                    }
                    else
                    {
                        DecodeAddr(addr);
                    }
                }
            }

            int lastAddr = int.MaxValue;
            int lastCodeAddr = int.MaxValue;

            foreach (var a in byteMap.OrderBy(ob => ob.Key))
            {
                if (a.Key != lastAddr + 1) sw.WriteLine();

                if (a.Value != lastCodeAddr)
                {
                    sw.WriteLine(addrDone[a.Value]);
                    lastCodeAddr = a.Value;
                }
                lastAddr = a.Key;    
            }
        }

        private static void AddAddr(int addr, string txt)
        {
            if (addrDone.ContainsKey(addr) == false && addrTodo.Contains(addr) == false  )
            {
                Debug.WriteLine(String.Format("Add: {0:x4} ecl_offset: {1:x4} {2}", addr, ecl_offset, txt));
                addrTodo.Enqueue(addr);
            }
        }

        private static void AddLine(int addr, string txt, int len)
        {
            if (addrDone.ContainsKey(addr) == false)
            {
                addrDone.Add(addr, txt);
                for (int i = 0; i < len; i++)
                {
                    if (byteMap.ContainsKey(addr + i))
                    {
                        Debug.WriteLine(String.Format("map byte {0:x4} points to {1:x4} not {2:x4}", addr + i, byteMap[addr + i], addr));
                    }
                    else
                    {
                        byteMap.Add(addr + i, addr);
                    }
                }
            }
        } 
        
        private static void DecodeAddr(int entryPoint)
        {
            ecl_offset = entryPoint;
            stopVM = false;

            while (stopVM == false)
            {
                int command = ecl_ptr[ecl_offset + 0x8000];

                int addr = ecl_offset;
                string txt = string.Format("0x{0:X4} 0x{1:X2} ", addr, command);

                CmdItem cmd;
                if (CommandTable.TryGetValue(command, out cmd))
                {
                    bool lastSkip = skipNext;

                    txt += string.Format("{0} {1}", cmd.Name(), cmd.Dump());

                    if (lastSkip)
                    {
                        skipNext = false;
                        lastSkip = false;
                    }
                }
                else
                {
                    txt += "Unknown command";
                    break;
                }

                if (stopVM) txt += "\n\r";

                AddLine(addr, txt, (ecl_offset - addr) & 0xFFFF);
            }
        }





        static Dictionary<int, CmdItem> CommandTable = new Dictionary<int, CmdItem>();

        public static void SetupCommandTable()
        {
            CommandTable.Add(0x00, new CmdItem(0, "EXIT", CMD_Exit));
            CommandTable.Add(0x01, new CmdItem(1, "GOTO", CMD_Goto));
            CommandTable.Add(0x02, new CmdItem(1, "GOSUB", CMD_Gosub));
            CommandTable.Add(0x03, new CmdItem(2, "COMPARE", CMD_Compare));
            CommandTable.Add(0x04, new CmdItem(3, "ADD", CMD_Add));
            CommandTable.Add(0x05, new CmdItem(3, "SUBTRACT", CMD_Sub));
            CommandTable.Add(0x06, new CmdItem(3, "DIVIDE", CMD_Div));
            CommandTable.Add(0x07, new CmdItem(3, "MULTIPLY", CMD_Multi));
            CommandTable.Add(0x08, new CmdItem(2, "RANDOM", CMD_Random));
            CommandTable.Add(0x09, new CmdItem(2, "SAVE", CMD_Save));
            CommandTable.Add(0x0A, new CmdItem(1, "LOAD CHARACTER", CMD_LoadCharacter));
            CommandTable.Add(0x0B, new CmdItem(3, "LOAD MONSTER", CMD_LoadMonster));
            CommandTable.Add(0x0C, new CmdItem(3, "SETUP MONSTER", CMD_SetupMonster));
            CommandTable.Add(0x0D, new CmdItem(0, "APPROACH", CMD_Approach));
            CommandTable.Add(0x0E, new CmdItem(1, "PICTURE", CMD_Picture));
            CommandTable.Add(0x0F, new CmdItem(2, "INPUT NUMBER", CMD_InputNumber));
            CommandTable.Add(0x10, new CmdItem(2, "INPUT STRING", CMD_InputString));
            CommandTable.Add(0x11, new CmdItem(1, "PRINT", CMD_Print));
            CommandTable.Add(0x12, new CmdItem(1, "PRINTCLEAR", CMD_Print));
            CommandTable.Add(0x13, new CmdItem(0, "RETURN", CMD_Return));
            CommandTable.Add(0x14, new CmdItem(4, "COMPARE AND", CMD_CompareAnd));
            CommandTable.Add(0x15, new CmdItem(0, "VERTICAL MENU", CMD_VertMenu));
            CommandTable.Add(0x16, new CmdItem(0, "IF =", CMD_If));
            CommandTable.Add(0x17, new CmdItem(0, "IF <>", CMD_If));
            CommandTable.Add(0x18, new CmdItem(0, "IF <", CMD_If));
            CommandTable.Add(0x19, new CmdItem(0, "IF >", CMD_If));
            CommandTable.Add(0x1A, new CmdItem(0, "IF <=", CMD_If));
            CommandTable.Add(0x1B, new CmdItem(0, "IF >=", CMD_If));
            CommandTable.Add(0x1C, new CmdItem(0, "CLEARMONSTERS", CMD_ClearMonsters));
            CommandTable.Add(0x1D, new CmdItem(1, "PARTYSTRENGTH", CMD_PartyStrength));
            CommandTable.Add(0x1E, new CmdItem(6, "CHECKPARTY", CMD_CheckParty));
            CommandTable.Add(0x1F, new CmdItem(2, "notsure 0x1f", null));
            CommandTable.Add(0x20, new CmdItem(1, "NEWECL", CMD_NewECL));
            CommandTable.Add(0x21, new CmdItem(3, "LOAD FILES", CMD_LoadFiles));
            CommandTable.Add(0x22, new CmdItem(2, "PARTY SURPRISE", null)); //CMD_PartySurprise));
            CommandTable.Add(0x23, new CmdItem(4, "SURPRISE", CMD_Surprise));
            CommandTable.Add(0x24, new CmdItem(0, "COMBAT", CMD_Combat));
            CommandTable.Add(0x25, new CmdItem(0, "ON GOTO", CMD_OnGoto));
            CommandTable.Add(0x26, new CmdItem(0, "ON GOSUB", CMD_OnGoto));
            CommandTable.Add(0x27, new CmdItem(8, "TREASURE", CMD_Treasure));
            CommandTable.Add(0x28, new CmdItem(3, "ROB", CMD_Rob));
            CommandTable.Add(0x29, new CmdItem(14, "ENCOUNTER MENU", CMD_EncounterMenu));
            CommandTable.Add(0x2A, new CmdItem(3, "GETTABLE", CMD_GetTable));
            CommandTable.Add(0x2B, new CmdItem(0, "HORIZONTAL MENU", CMD_HorizontalMenu));
            CommandTable.Add(0x2C, new CmdItem(6, "PARLAY", CMD_Parlay));
            CommandTable.Add(0x2D, new CmdItem(1, "CALL", CMD_Call));
            CommandTable.Add(0x2E, new CmdItem(5, "DAMAGE", CMD_Damage));
            CommandTable.Add(0x2F, new CmdItem(3, "AND", CMD_And));
            CommandTable.Add(0x30, new CmdItem(3, "OR", CMD_Or));
            CommandTable.Add(0x31, new CmdItem(0, "SPRITE OFF", CMD_SpriteOff));
            CommandTable.Add(0x32, new CmdItem(1, "FIND ITEM", CMD_FindItem));
            CommandTable.Add(0x33, new CmdItem(0, "PRINT RETURN", CMD_PrintReturn));
            CommandTable.Add(0x34, new CmdItem(1, "ECL CLOCK", CMD_EclClock));
            CommandTable.Add(0x35, new CmdItem(3, "SAVE TABLE", CMD_SaveTable));
            CommandTable.Add(0x36, new CmdItem(1, "ADD NPC", CMD_AddNPC));
            CommandTable.Add(0x37, new CmdItem(3, "LOAD PIECES", CMD_LoadPieces));
            CommandTable.Add(0x38, new CmdItem(1, "PROGRAM", CMD_Program));
            CommandTable.Add(0x39, new CmdItem(1, "WHO", CMD_Who));
            CommandTable.Add(0x3A, new CmdItem(0, "DELAY", CMD_Delay));
            CommandTable.Add(0x3B, new CmdItem(3, "SPELL", CMD_Spell));
            CommandTable.Add(0x3C, new CmdItem(1, "PROTECTION", CMD_Protection));
            CommandTable.Add(0x3D, new CmdItem(0, "CLEAR BOX", CMD_ClearBox));
            CommandTable.Add(0x3E, new CmdItem(0, "DUMP", CMD_Dump));
            CommandTable.Add(0x3F, new CmdItem(1, "FIND SPECIAL", CMD_FindSpecial));
            CommandTable.Add(0x40, new CmdItem(1, "DESTROY ITEMS", CMD_DestroyItems));
        }


        private static void DumpBin(byte[] data, string block_name)
        {
            string bin_name = block_name + ".bin";

            using (BinaryWriter binWriter = new BinaryWriter(File.Open(bin_name, FileMode.Create)))
            {
                binWriter.Write(data);
            }
        }
    }



 
    internal class CmdItem
    {
        public delegate string CmdDelegate();

        int size;
        string name;
        CmdDelegate cmd;

        public CmdItem(int Size, string Name, CmdDelegate Cmd)
        {
            size = Size;
            name = Name;
            cmd = Cmd;
        }

        public string Dump()
        {
            if (cmd != null)
            {
                return cmd();
            }
            else
            {
                if (size == 0)
                {
                    Program.ecl_offset += 1;
                }
                else
                {
                    Program.LoadEclOpps(size);
                }
                return "todo";
            }
        }

        public string Name()
        {
            return name;
        }
    }
}
