using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace TlbDump
{
    class Program
    {
        static void Main(string[] args)
        {
            //string path = @"C:\games\DARKQUEEN\DISK2";
            string path = @".";
            string filter = @"*.tlb";

            if (args.Length > 0)
            {
                path = Path.GetDirectoryName(args[0]);
                filter = Path.GetFileName(args[0]);
            }

            foreach (var filea in Directory.GetFiles(path, filter))
            {
                TryDump(filea);
            }

            //Console.ReadKey();
        }


        static void TryDump(string file)
        {
            Console.WriteLine(file);
            foreach (var block in GetAllBlocksIndexed(file))
            {
                var data = block.data;

                Console.WriteLine("File: {0} Block: {1} Size: {2}", block.file, block.id, data.Length);

                string block_name = string.Format("{0}_{1:000}", Path.Combine(Path.GetDirectoryName(block.file), Path.GetFileNameWithoutExtension(block.file)), block.id);

                DumpBin(data, block_name);         
            }
        }

 

        private static void DumpBin(byte[] data, string block_name)
        {
            string bin_name = block_name + ".bin";

            using (BinaryWriter binWriter = new BinaryWriter(File.Open(bin_name, FileMode.Create)))
            {
                binWriter.Write(data);
            }
        }

        private static IEnumerable<Block> GetAllBlocksIndexed(string file)
        {
            // try old CotAB file types
            FileStream fsA = new FileStream(file, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);

            var fileA = new BinaryReader(fsA);

            if (fsA.Length > 16)
            {
                int hlibA = fileA.ReadInt32();
                int fileSize = fileA.ReadInt32();
                int ptrCount = fileA.ReadInt16();
                int reserved = fileA.ReadByte();
                int magic = fileA.ReadByte();
                int hlibB = fileA.ReadInt32();

                if (hlibA == 0x42494c48 && // HLIB
                    (hlibB == 0x454c4954 || hlibB == 0x41544144) && // TILE or DATA
                    fsA.Length > ((ptrCount * 4) + 16))
                {
                    List<HeaderEntry> headers = new List<HeaderEntry>();

                    int thisOffset = fileA.ReadInt32();

                    for (int i = 0; i < ptrCount; i++) // last is EOF pointer
                    {
                        int nextOffset = fileA.ReadInt32();
                        int length = nextOffset - thisOffset;

                        HeaderEntry dhe = new HeaderEntry(i, thisOffset, length);

                        Console.WriteLine(dhe);
                        headers.Add(dhe);

                        thisOffset = nextOffset;
                    }

                    if (headers.Count == 0)
                        yield break;

                    // build index map
                    var idhe = headers[0];
                    byte[] mapdata = new byte[idhe.size];

                    fileA.BaseStream.Seek(idhe.offset, SeekOrigin.Begin);
                    mapdata = fileA.ReadBytes(idhe.size);

                    int mapcount = Sys.ArrayToShort(mapdata, 0);

                    if (mapcount * 4 != (mapdata.Length - 2))
                    {
                        foreach (var dhe in headers)
                        {
                            byte[] comp = new byte[dhe.size];

                            fileA.BaseStream.Seek(dhe.offset, SeekOrigin.Begin);
                            comp = fileA.ReadBytes(dhe.size);

                            yield return new Block(file, dhe.idx, comp);
                        }
                    }
                    else
                    {
                        var map = new Dictionary<int, int>();

                        for (int i = 0; i < mapcount; i++)
                        {
                            int idx = Sys.ArrayToShort(mapdata, (i * 4) + 2);
                            int slot = Sys.ArrayToShort(mapdata, (i * 4) + 4);

                            map.Add(idx, slot);
                        }

                        foreach (var p in map)
                        {
                            var dhe = headers[p.Value];
                            byte[] comp = new byte[dhe.size];

                            fileA.BaseStream.Seek(dhe.offset, SeekOrigin.Begin);
                            comp = fileA.ReadBytes(dhe.size);

                            yield return new Block(file, p.Key, comp);
                        }

                        if (fileA.BaseStream.Position != fileA.BaseStream.Length)
                        {
                            Console.WriteLine("Wrong Size!");
                        }
                    }
                }
                else
                {
                }
            }
        }

        private static IEnumerable<Block> GetAllBlocksRaw(string file)
        {
            // try old CotAB file types
            FileStream fsA = new FileStream(file, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);

            var fileA = new BinaryReader(fsA);

            if (fsA.Length > 16)
            {
                int hlibA = fileA.ReadInt32(); 
                int fileSize = fileA.ReadInt32();
                int ptrCount = fileA.ReadInt16();
                int reserved = fileA.ReadByte();
                int magic = fileA.ReadByte();
                int hlibB = fileA.ReadInt32();

                if (hlibA == 0x42494c48 && // HLIB
                    (hlibB == 0x454c4954 || hlibB == 0x41544144) && // TILE or DATA
                    fsA.Length > ((ptrCount * 4) + 16))
                {

                    List<HeaderEntry> headers = new List<HeaderEntry>();

                    int thisOffset = fileA.ReadInt32();

                    for (int i = 0; i < ptrCount; i++) // last is EOF pointer
                    {
                        int nextOffset = fileA.ReadInt32();
                        int length = nextOffset - thisOffset;

                        HeaderEntry dhe = new HeaderEntry(i, thisOffset, length);
                        
                        Console.WriteLine(dhe);
                        headers.Add(dhe);

                        thisOffset = nextOffset;
                    }

                    foreach (var dhe in headers)
                    {
                        byte[] comp = new byte[dhe.size];

                        fileA.BaseStream.Seek(dhe.offset, SeekOrigin.Begin);
                        comp = fileA.ReadBytes(dhe.size);

                        yield return new Block(file, dhe.idx, comp);
                    }

                    if (fileA.BaseStream.Position != fileA.BaseStream.Length)
                    {
                        Console.WriteLine("Wrong Size!");
                    }
                }
            }
        }

        static void Decode(int decodeSize, int dataLength, byte[] output_ptr, byte[] input_ptr)
        {
            sbyte run_length;
            int output_index;
            int input_index;

            input_index = 0;
            output_index = 0;

            do
            {
                run_length = (sbyte)input_ptr[input_index];

                if (run_length >= 0)
                {
                    for (int i = 0; i <= run_length; i++)
                    {
                        output_ptr[output_index + i] = input_ptr[input_index + i + 1];
                    }

                    input_index += run_length + 2;
                    output_index += run_length + 1;
                }
                else
                {
                    run_length = (sbyte)(-run_length);

                    for (int i = 0; i < run_length; i++)
                    {
                        output_ptr[output_index + i] = input_ptr[input_index + 1];
                    }

                    input_index += 2;
                    output_index += run_length;
                }
            } while (input_index < dataLength);
        }
    }

    class Block
    {
        public string file;
        public int id;
        public byte[] data;

        public Block(string _file, int _id, byte[] _data)
        {
            file = _file;
            id = _id;
            data = _data;
        }
    }

    class HeaderEntry
    {
        internal int idx;
        internal int offset;
        internal int size;
        public HeaderEntry(int _idx, int _offset, int _size) { idx = _idx; offset = _offset; size = _size; }

        public override string ToString()
        {
            return string.Format("idx: {0} offset: {1} size: {2} ", idx, offset, size);
        }
    }
}
