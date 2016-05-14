using System;

namespace GoldBox.Classes
{
    public class DataOffsetAttribute : Attribute
    {

        // The constructor is called when the attribute is set.
        public DataOffsetAttribute(int offset, DataType type)
        {
            this.offset = offset;
            this.type = type;
            this.size = DefaultSize(type);
        }

        public DataOffsetAttribute(int offset, DataType type, int size)
        {
            this.offset = offset;
            this.type = type;
            this.size = size;
        }

        int DefaultSize(DataType dataType)
        {
            switch (dataType)
            {
                case DataType.Byte:
                    return 1;
                case DataType.SByte:
                    return 1;
                case DataType.IByte:
                    return 1;
                case DataType.Bool:
                    return 1;
                case DataType.Word:
                    return 2;
                case DataType.SWord:
                    return 2;
                case DataType.Int:
                    return 4;
                default:
                    throw new NotImplementedException();
            }
        }

        // Keep a variable internally ...
        protected int offset;
        public int Offset
        {
            get { return offset; }
        }

        protected int size;
        public int Size
        {
            get { return size; }
        }

        protected DataType type;
        public DataType Type
        {
            get { return type; }
        }


    }

}
