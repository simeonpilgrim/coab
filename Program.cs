using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace DaxDump
{
    partial class Program
    {
        static void Main(string[] args)
        {
            string path = System.IO.Directory.GetCurrentDirectory();
            //string path = @"C:\games\DARKNESS";
            //string path = @"C:\games\TREASURE";
            //string path = @"C:\games\coab";
            //string path = @"C:\games\pool";
            //string path = @"C:\games\secret";
            //string path = @"C:\games\deathkrynn";
            //string path = @"C:\games\nwn";
            //string path = @"c:\games\buckmatrix";
            //string path = @"c:\games\buckcount";

            foreach (var dir in Directory.GetDirectories(path))
            {
                string dirname = Path.GetFileName(dir);

                using (var sw = new StreamWriter(dir + "_text.txt", false))
                {
                    foreach (var filea in Directory.GetFiles(dir, "ecl*.dax"))
                    {
                        TryDump(filea, sw);
                    }
                }
            }
            //foreach (var filea in Directory.GetFiles(path, "ecl*.dax"))
            //{
            //    TryDump(filea);
            //}

            //Console.ReadKey();
        }

        static void TryDump(string file, StreamWriter sw)
        {
            Console.WriteLine(file);
            foreach (var block in GetAllBlocks(file))
            {
                byte[] data = block.data;

                Console.WriteLine("File: {0} Block: {1} Size: {2}", block.file, block.id, data.Length);

                string block_name = string.Format("{0}_{1:000}", Path.Combine(Path.GetDirectoryName(block.file), Path.GetFileNameWithoutExtension(block.file)), block.id);

                DumpEclText(data, block_name, sw);
            }
        }


        private static void DumpEclText(byte[] data, string block_name, StreamWriter sw)
        {
            sw.WriteLine();
            sw.WriteLine(block_name);
            sw.WriteLine();
            for (int i = 0; i < data.Length - 2; i++)
            {
                if (data[i] == 0x80)
                {
                    int len = data[i + 1];

                    if (i + 1 + len < data.Length)
                    {
                        byte[] tdata = new byte[len];

                        System.Array.Copy(data, i + 2, tdata, 0, len);
                        var txt = DecompressString(tdata);
                        if (TextTest(txt))
                        {
                            sw.WriteLine(txt);
                        }
                    }
                }
            }
        }

        private static bool TextTest(string txt)
        {
            int nospace = 0;

            if (txt.Trim().Length == 0) return false;
            if (txt == "Q" || txt == "C") return false;

            foreach (var ch in txt)
            {
                if (Char.IsWhiteSpace(ch))
                    nospace = 0;
                else
                    nospace++;

                if (nospace > 15) return false;

                switch (ch)
                {
                    case '=':
                    case '[':
                    case ']':
                    case '%':
                    case '<':
                    case '$':
                    case '*':
                    case '&':
                    case '\\':
                    case '/':
                    case '^':
                        return false;
                }
            }

            return true;
        }



        internal static string DecompressString(byte[] data)
        {
            var sb = new System.Text.StringBuilder();
            int state = 1;
            uint lastByte = 0;

            foreach (uint thisByte in data)
            {
                uint curr = 0;
                switch (state)
                {
                    case 1:
                        curr = (thisByte >> 2) & 0x3F;
                        if (curr != 0) sb.Append(inflateChar(curr));
                        state = 2;
                        break;

                    case 2:
                        curr = ((lastByte << 4) | (thisByte >> 4)) & 0x3F;
                        if (curr != 0) sb.Append(inflateChar(curr));
                        state = 3;
                        break;

                    case 3:
                        curr = ((lastByte << 2) | (thisByte >> 6)) & 0x3F;
                        if (curr != 0) sb.Append(inflateChar(curr));

                        curr = thisByte & 0x3F;
                        if (curr != 0) sb.Append(inflateChar(curr));
                        state = 1;
                        break;
                }
                lastByte = thisByte;
            }

            return sb.ToString();
        }


        internal static char inflateChar(uint arg_0)
        {
            if (arg_0 <= 0x1f)
            {
                arg_0 += 0x40;
            }

            return (char)arg_0;
        }
    }

}
