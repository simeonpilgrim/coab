using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace EclDump
{
    partial class Program
    {
        private static IEnumerable<Block> GetAllBlocks(string file)
        {
            // try old CotAB file types
            FileStream fsA = new FileStream(file, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);

            var fileA = new BinaryReader(fsA);

            int dataOffset = fileA.ReadInt16() + 2;

            List<HeaderEntry> headers = new List<HeaderEntry>();

            const int headerEntrySize = 9;

            for (int i = 0; i < ((dataOffset - 2) / headerEntrySize); i++)
            {
                HeaderEntry dhe = new HeaderEntry();
                dhe.id = fileA.ReadByte();
                dhe.offset = fileA.ReadInt32();
                dhe.rawSize = fileA.ReadUInt16();
                dhe.compSize = fileA.ReadUInt16();

                //Console.WriteLine(dhe);
                headers.Add(dhe);
            }

            foreach (var dhe in headers)
            {
                byte[] comp = new byte[dhe.compSize];
                if (dhe.rawSize < 0)
                {
                    fileA.BaseStream.Seek(dataOffset + dhe.offset, SeekOrigin.Begin);
                    comp = fileA.ReadBytes(dhe.compSize);

                    yield return new Block(file, dhe.id, comp);
                }
                else
                {
                    byte[] raw = new byte[dhe.rawSize];

                    fileA.BaseStream.Seek(dataOffset + dhe.offset, SeekOrigin.Begin);

                    comp = fileA.ReadBytes(dhe.compSize);

                    Decode(dhe.rawSize, dhe.compSize, raw, comp);

                    yield return new Block(file, dhe.id, raw);
                }
            }

            if (fileA.BaseStream.Position != fileA.BaseStream.Length)
            {
                Console.WriteLine("Wrong Size!");
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
        internal int id;
        internal int offset;
        internal int rawSize; // decodeSize
        internal int compSize; // dataLength

        public override string ToString()
        {
            return string.Format("id: {0} offset: {1} raw: {2} comp: {3}", id, offset, rawSize, compSize);
        }
    }
}
