using System;

namespace Classes
{
    /// <summary>
    /// Summary description for EclBlock.
    /// </summary>
    public class EclBlock
    {
        public const ushort ecl_struct_size = 0x1e00;

        private byte[] data;

        public EclBlock()
        {
            data = new byte[ecl_struct_size];
        }

        public EclBlock(byte[] _data, int offset)
        {
            data = new byte[ecl_struct_size];

            System.Array.Copy(_data, data, ecl_struct_size);
        }

        public void Clear()
        {
            Array.Clear(data, 0, ecl_struct_size);
        }

        public byte this[int index]
        {
            get
            {
                // simulate the 16 bit memory space.
                int loc = index & 0xFFFF;
                byte value = data[loc];
                //System.Console.WriteLine("     EclBlock[get] loc: {0,4:X} value {1,2:X}", loc, value);

                return value;
            }
            set
            {
                int loc = index & 0xFFFF;
                //System.Console.WriteLine("     EclBlock[set] loc: {0,4:X} value: {1,4:X}", loc, value);

                data[loc] = value;
            }
        }

        public void SetData(byte[] dataArray, int dataOffset, int dataLength)
        {
            System.Array.Copy(dataArray, dataOffset, this.data, 0, dataLength);
        }

        public byte[] ToByteArray()
        {
            return (byte[])data.Clone();
        }

        /// <summary>
        /// Dev Debugging Function
        /// </summary>
        public void SearchForBytes(byte[] bytes, int length)
        {
            for (int offset = 0; offset <= ecl_struct_size; offset++)
            {
                int i = 0;
                while (bytes[i] == data[(offset + i) % ecl_struct_size])
                {
                    i++;
                    if (i == length)
                    {
                        System.Console.WriteLine("{0,4:X}", offset + 0x8000);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Dev Debugging Function
        /// </summary>
        public void SearchECL(byte[,] blocks)
        {
            int last = 0;
            int step = 0;
            int dataCount = blocks.GetLength(0);

            int loc = 0;
            for (;loc < data.Length - 100; loc++)
            {
                if (step == 0)
                {
                    if (data[loc] == blocks[step, 0])
                    {
                        last = loc;
                        loc = skipOps(loc, blocks[step, 1]);
                        step += 1;
                    }
                }
                else if (data[loc] == blocks[step, 0])
                {
                    loc = skipOps(loc, blocks[step, 1]);
                    step += 1;
                }
                else
                {
                    step = 0;
                    loc = last;
                }

                if (step == dataCount)
                {
                    //for (int j = last; j <= loc; j++)
                    //{
                    //    System.Console.WriteLine("{0,4:X} {1,2:X}", j, data[j]);
                    //}

                    //System.Console.WriteLine();

                    step = 0;
                }
            }
        }

        private int skipOps(int loc, byte count)
        {
            for (int i = 0; i < count; i++)
            {
                loc = skipOp(loc);
            }

            return loc;
        }

        private int skipOp(int loc)
        {
            switch (data[loc + 1])
            {
                case 0:
                default:
                    return loc + 2;

                case 1:
                case 2:
                case 3:
                case 0x81:
                    return loc + 3;

                case 0x80:
                    return loc + 2 + data[loc + 2];
            }
        }
    }
}
