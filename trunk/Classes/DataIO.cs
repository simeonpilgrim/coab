using System;
using System.Reflection;
using System.Text;

namespace Classes
{
    public enum DataType
    {
        PString,
        Byte,
        SByte,
        IByte, //a byte that needs to be cast to int first.
        Word,
        SWord,
        Int,
        SInt,
        Bool,
        ByteArray,
        ShortArray,
        WordArray,
        Cust1Array
    }

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

        int DefaultSize(DataType type)
        {
            switch (type)
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

    public class DataIO
    {
        static public void ReadObject(object obj, byte[] data, int offset)
        {
            Type type = obj.GetType();

            // Iterate through all the fields of the class.
            foreach (FieldInfo fInfo in type.GetFields())
            {
                // Iterate through all the Attributes for each Field.
                foreach (Attribute attr in
                    Attribute.GetCustomAttributes(fInfo))
                {
                    // Check for the DataOffset attribute.
                    if (attr.GetType() == typeof(DataOffsetAttribute))
                    {
                        DataOffsetAttribute doAttr = (DataOffsetAttribute)attr;

                        readData(obj, fInfo, doAttr, data, 0);
                    }
                }

            }
        }

        static public void WriteObject(object obj, byte[] data)
        {
            Type type = obj.GetType();

            // Iterate through all the fields of the class.
            foreach (FieldInfo fInfo in type.GetFields())
            {
                // Iterate through all the Attributes for each Field.
                foreach (Attribute attr in
                    Attribute.GetCustomAttributes(fInfo))
                {
                    // Check for the DataOffset attribute.
                    if (attr.GetType() == typeof(DataOffsetAttribute))
                    {
                        DataOffsetAttribute doAttr = (DataOffsetAttribute)attr;

                        writeData(obj, fInfo, doAttr, data);
                    }
                }

            }
        }

        static private void writeData(object obj, FieldInfo fInfo, DataOffsetAttribute attr, byte[] data)
        {
            object o = fInfo.GetValue(obj);
            switch (attr.Type)
            {
                case DataType.SByte:
                    data[attr.Offset] = (byte)(sbyte)o;
                    break;

                case DataType.IByte:
                    data[attr.Offset] = (byte)(int)o;
                    break;

                case DataType.Byte:
                    data[attr.Offset] = (byte)o;
                    break;

                case DataType.SWord:
                    {
                        short s = (short)o;
                        data[attr.Offset] = (byte)(s & 0xff);
                        data[attr.Offset + 1] = (byte)((s >> 8) & 0xff);
                    }
                    break;

                case DataType.Word:
                    {
                        ushort s = (ushort)o;
                        data[attr.Offset] = (byte)(s & 0xff);
                        data[attr.Offset + 1] = (byte)((s >> 8) & 0xff);
                    }
                    break;

                case DataType.Int:
                    {
                        int i = (int)o;
                        int offset = attr.Offset;
                        data[offset++] = (byte)(i & 0xff);
                        data[offset++] = (byte)((i >> 8) & 0xff);
                        data[offset++] = (byte)((i >> 16) & 0xff);
                        data[offset++] = (byte)((i >> 24) & 0xff);
                    }
                    break;

                case DataType.Bool:
                    data[attr.Offset] = (bool)o ? (byte)1 : (byte)0;
                    break;

                case DataType.ByteArray:
                    {
                        byte[] values = (byte[])o;

                        System.Array.Copy(values, 0, data, attr.Offset, attr.Size);
                    }
                    break;

                case DataType.ShortArray:
                    {
                        short[] values = (short[])o;
                        int offset = attr.Offset;
                        for (int i = 0; i < attr.Size; i++)
                        {
                            short s = values[i];
                            data[offset++] = (byte)(s & 0xff);
                            data[offset++] = (byte)((s >> 8) & 0xff);
                        }
                    }
                    break;

                case DataType.WordArray:
                    {
                        ushort[] values = (ushort[])o;
                        int offset = attr.Offset;
                        for (int i = 0; i < attr.Size; i++)
                        {
                            ushort s = values[i];
                            data[offset++] = (byte)(s & 0xff);
                            data[offset++] = (byte)((s >> 8) & 0xff);
                        }
                    }
                    break;

                case DataType.PString:
                    {
                        byte b = (byte)attr.Size;
                        string s = (string)o;
                        if (s.Length < b)
                        {
                            b = (byte)s.Length;
                        }

                        data[attr.Offset] = b;
                        for (int i = 0; i < b; i++)
                        {
                            data[attr.Offset + i + 1] = (byte)s[i];
                        }
                    }
                    break;

                case DataType.Cust1Array:
                    {
                        int i = attr.Offset;
                        StatValue[] sv = (StatValue[])o;
                        foreach (StatValue v in sv)
                        {
                            data[i++] = v.tmp;
                            data[i++] = v.max;
                        }
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        static private void readData(object obj, FieldInfo fInfo, DataOffsetAttribute attr, byte[] data, int offset )
        {
            offset += attr.Offset;
            switch (attr.Type)
            {
                case DataType.SByte:
                    fInfo.SetValue(obj, (sbyte)data[offset]);
                    break;

                case DataType.IByte:
                    fInfo.SetValue(obj, data[offset]);
                    break;

                case DataType.Byte:
                    fInfo.SetValue(obj, data[offset]);
                    break;

                case DataType.SWord:
                    {
                        short s = (short)(data[offset] + (data[offset + 1] << 8));
                        fInfo.SetValue(obj, s);
                    }
                    break;

                case DataType.Word:
                    {
                        ushort w = (ushort)(data[offset] + (data[offset + 1] << 8));
                        fInfo.SetValue(obj, w);
                    }
                    break;

                case DataType.Int:
                    {
                        int i = data[offset] + (data[offset + 1] << 8) + (data[offset + 2] << 16) + (data[offset + 3] << 24);
                        fInfo.SetValue(obj, i);
                    }
                    break;

                case DataType.Bool:
                    fInfo.SetValue(obj, (data[offset] != 0));
                    break;

                case DataType.ByteArray:
                    {
                        byte[] a = new byte[attr.Size];

                        System.Array.Copy(data, offset, a, 0, attr.Size);
                        fInfo.SetValue(obj, a);
                    }
                    break;

                case DataType.ShortArray:
                    {
                        short[] a = new short[attr.Size];
                        for (int i = 0; i < attr.Size; i++)
                        {
                            a[i] = (short)(data[offset + (i * 2)] + (data[offset + 1 + (i * 2)] << 8));
                        }
                        fInfo.SetValue(obj, a);
                    }
                    break;

                case DataType.WordArray:
                    {
                        ushort[] a = new ushort[attr.Size];
                        for (int i = 0; i < attr.Size; i++)
                        {
                            a[i] = (ushort)(data[offset + (i * 2)] + (data[offset + 1 + (i * 2)] << 8));
                        }
                        fInfo.SetValue(obj, a);
                    }
                    break;

                case DataType.PString:
                    {
                        fInfo.SetValue(obj, Sys.ArrayToString(data,offset, attr.Size));
                    }
                    break;

                case DataType.Cust1Array:
                    {
                        StatValue[] sv = new StatValue[attr.Size];
                        for (int i = 0; i < attr.Size; i++)
                        {
                            sv[i].tmp = data[offset++];
                            sv[i].max = data[offset++];
                        }
                        fInfo.SetValue(obj, sv);
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
