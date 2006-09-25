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
                return data[index & 0xFFFF];
            }
            set
            {
                data[index & 0xFFFF] = value;
            }
        }

        public void SetData(byte[] dataArray, int dataOffset, int dataLength)
        {
            System.Array.Copy(dataArray, dataOffset, this.data, 0, dataLength);
        }

        public byte[] ToByteArray()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
