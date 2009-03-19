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
                case 1: return (byte)field_2F;
                case 2: return (byte)field_30;
                case 3: return (byte)field_31;
                default: throw new NotSupportedException();
            }
        }
        public sbyte field_2F;
        public sbyte field_30;
        public byte field_31;

        public sbyte plus; // 0x32 
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

        public Item(byte[] data, int offset)
        {
            name = Sys.ArrayToString(data, offset, 0x2a);

            type = data[offset + 0x2e];
            field_2F = (sbyte)data[offset + 0x2f];
            field_30 = (sbyte)data[offset + 0x30];
            field_31 = data[offset + 0x31];
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
            field_2F = 0;
            field_30 = 0;
            field_31 = 0;
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
            data[0x2f] = (byte)field_2F;
            data[0x30] = (byte)field_30;
            data[0x31] = field_31;
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
