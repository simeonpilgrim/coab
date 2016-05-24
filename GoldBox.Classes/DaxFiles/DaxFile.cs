using System;
using System.Collections.Generic;
using System.IO;
using GoldBox.Logging;

namespace GoldBox.Classes.DaxFiles
{
    class DaxFile
    {
        Dictionary<int, byte[]> entries;

        internal DaxFile(string filename)
        {
            entries = new Dictionary<int, byte[]>();

            LoadFile(filename);
        }

        private void LoadFile(string filename)
        {
            using (var fileStream = new ReadOnlyFileStream(filename))
            using (var fileA = new BinaryReader(fileStream.BaseStream))
            {
                int dataOffset = fileA.ReadInt16() + 2;

                var headers = new List<DaxHeaderEntry>();

                const int headerEntrySize = 9;

                for (int i = 0; i < ((dataOffset - 2) / headerEntrySize); i++)
                {
                    var dhe = new DaxHeaderEntry();
                    dhe.id = fileA.ReadByte();
                    dhe.offset = fileA.ReadInt32();
                    dhe.rawSize = fileA.ReadInt16();
                    dhe.compSize = fileA.ReadUInt16();

                    headers.Add(dhe);
                }

                foreach (DaxHeaderEntry dhe in headers)
                {
                    byte[] comp = new byte[dhe.compSize];
                    byte[] raw = new byte[dhe.rawSize];

                    fileA.BaseStream.Seek(dataOffset + dhe.offset, SeekOrigin.Begin);

                    comp = fileA.ReadBytes(dhe.compSize);

                    Decode(dhe.rawSize, dhe.compSize, raw, comp);

                    entries.Add(dhe.id, raw);
                }

            }
        }

        void Decode(int decodeSize, int dataLength, byte[] output_ptr, byte[] input_ptr)
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

        /// <summary>
        /// Tries read a block of data from a Dax file
        /// </summary>
        /// <param name="block_id">The block of data to get from the Dax file</param>
        /// <returns>the block of data, or null if the block can't be found</returns>
        internal byte[] GetData(int block_id)
        {
            byte[] orig;
            if (entries.TryGetValue(block_id, out orig) == false)
            {
                return null;
            }

            return (byte[])orig.Clone();
        }
    }

}
