using System;

namespace Classes
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
    public class Item
    {
        public string name; // 0x00

        public byte type; // 0x2e; /* 11 - 14 = scroll */
        public byte field_2EArray(int index)
        {
            switch (index)
            {
                case 1: return (byte)namenum1;
                case 2: return (byte)namenum2;
                case 3: return (byte)namenum3;
                default: throw new NotSupportedException();
            }
        }
        public int namenum1;
        public int namenum2;
        public int namenum3;

        public int plus; // 0x32 
        public byte plus_save; // 0x33 
        public bool readied; // 0x34
        public byte hidden_names_flag; // 0x35 
        public bool cursed; // 0x36 
        public short weight; // 0x37
        public int count;   // 0x39 
        public short _value; // 0x3A "seams like value is in electrum, as value is doubled.";
        public Affects affect_1; // 0x3C
        public Affects affect_2; // 0x3D
        public Affects affect_3; // 0x3E

        public bool ScrollLearning(int i, int spell)
        {
            return ((int)getAffect(i) > 0x7F && ((int)getAffect(i) & 0x7F) == spell);
        }

        public bool IsScroll()
        {
            return (gbl.ItemDataTable[type].item_slot > 10 && gbl.ItemDataTable[type].item_slot < 14);
        }

        public Affects getAffect(int i)
        {
            switch (i)
            {
                case 1:
                    return affect_1;
                case 2:
                    return affect_2;
                case 3:
                    return affect_3;
                default:
                    throw new System.ArgumentOutOfRangeException();
            }
        }
        public void setAffect(int i, Affects value)
        {
            switch (i)
            {
                case 1:
                    affect_1 = value;
                    break;
                case 2:
                    affect_2 = value;
                    break;
                case 3:
                    affect_3 = value;
                    break;
                default:
                    throw new System.ArgumentOutOfRangeException();
            }
        }

        public const int StructSize = 0x3F;

        public Item()
        {
            Clear();
        }


        public Item(Affects _affect_3, Affects _affect_2, Affects _affect_1, short __value, byte _count,
            short _weight, bool _cursed, byte _name_flags, bool _readied, byte _plus_save, sbyte _plus, byte _namenum3,
            byte _namenum2, byte _namenum1, byte _type)
        {
            name = string.Empty;
            type = _type;
            namenum1 = _namenum1;
            namenum2 = _namenum2;
            namenum3 = _namenum3;
            plus = _plus;
            plus_save = _plus_save;
            readied = _readied;
            hidden_names_flag = _name_flags;
            cursed = _cursed;
            weight = _weight;
            count = _count;
            _value = __value;
            affect_1 = _affect_1;
            affect_2 = _affect_2;
            affect_3 = _affect_3;
        }

        public Item(byte[] data, int offset)
        {
            name = Sys.ArrayToString(data, offset, 0x2a);

            type = data[offset + 0x2e];
            namenum1 = data[offset + 0x2f];
            namenum2 = data[offset + 0x30];
            namenum3 = data[offset + 0x31];
            plus = (sbyte)data[offset + 0x32];
            plus_save = data[offset + 0x33];
            readied = (data[offset + 0x34] != 0);
            hidden_names_flag = data[offset + 0x35];
            cursed = (data[offset + 0x36] != 0);

            weight = Sys.ArrayToShort(data, offset + 0x37);
            count = data[offset + 0x39];
            _value = Sys.ArrayToShort(data, offset + 0x3a);
            affect_1 = (Affects)data[offset + 0x3C];
            affect_2 = (Affects)data[offset + 0x3D];
            affect_3 = (Affects)data[offset + 0x3E];

            System.Console.WriteLine("ITEM:,{0},{1},{2},{3},{4}", type, namenum1, namenum2, namenum3, name); 
        }

        public Item ShallowClone()
        {
            Item i = (Item)this.MemberwiseClone();
            return i;
        }

        public void Clear()
        {
            name = string.Empty;

            type = 0;
            namenum1 = 0;
            namenum2 = 0;
            namenum3 = 0;
            plus = 0;
            plus_save = 0;
            readied = false;
            hidden_names_flag = 0;
            cursed = false;
            weight = 0;
            count = 0;
            _value = 0;
            affect_1 = 0;
            affect_2 = 0;
            affect_3 = 0;
        }

        public byte[] ToByteArray()
        {
            byte[] data = new byte[StructSize];

            Sys.StringToArray(data, 0, 0x2a, name);

            data[0x2e] = type;
            data[0x2f] = (byte)namenum1;
            data[0x30] = (byte)namenum2;
            data[0x31] = (byte)namenum3;
            data[0x32] = (byte)plus;
            data[0x33] = plus_save;
            data[0x34] = readied ? (byte)1 : (byte)0;
            data[0x35] = hidden_names_flag;
            data[0x36] = cursed ? (byte)1 : (byte)0;
            Sys.ShortToArray(weight, data, 0x37);
            data[0x39] = (byte)count;
            Sys.ShortToArray(_value, data, 0x3a);
            data[0x3C] = (byte)affect_1;
            data[0x3D] = (byte)affect_2;
            data[0x3E] = (byte)affect_3;

            return data;
        }
    }
}
