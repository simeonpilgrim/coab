using System;

namespace Classes
{
    /// <summary>
    /// Summary description for Item.
    /// </summary>
    [Serializable]
    public class Item
    {
        public string name; // 0x00

        public ItemType type; // 0x2e;
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

        public byte HandsCount()
        {
            return gbl.ItemDataTable[type].handsCount;
        }

        public bool CheckMaskedAffect(int i, byte masked_val)
        {
            return ((int)getAffect(i) > 0x7F && ((int)getAffect(i) & 0x7F) == masked_val);
        }

        public bool IsScroll()
        {
            return (gbl.ItemDataTable[type].item_slot >= ItemSlot.Arrow && gbl.ItemDataTable[type].item_slot <= ItemSlot.slot_13);
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
            byte _namenum2, byte _namenum1, ItemType _type, bool AddToLibrary)
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

            if (AddToLibrary)
            {
                ItemLibrary.Add(this);
            }
        }

        public Item(byte _affect_3, Affects _affect_2, Affects _affect_1, short __value, byte _count,
            short _weight, bool _cursed, byte _name_flags, bool _readied, byte _plus_save, sbyte _plus, byte _namenum3,
            byte _namenum2, byte _namenum1, ItemType _type, bool AddToLibrary) :
            this((Affects)_affect_3, _affect_2, _affect_1, __value, _count, _weight, _cursed, _name_flags,
                _readied, _plus_save, _plus, _namenum3, _namenum2, _namenum1, _type, AddToLibrary)
        {
        }

        public Item(short __value, byte _count,
            short _weight, bool _cursed, byte _name_flags, bool _readied, byte _plus_save, sbyte _plus, byte _namenum3,
            byte _namenum2, byte _namenum1, ItemType _type, bool AddToLibrary) :
            this(Affects.none, Affects.none, Affects.none, __value, _count, _weight, _cursed, _name_flags,
                _readied, _plus_save, _plus, _namenum3, _namenum2, _namenum1, _type, AddToLibrary)
        {
        }

        public Item(byte[] data, int offset)
        {
            name = Sys.ArrayToString(data, offset, 0x2a);

            type = (ItemType)data[offset + 0x2e];
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

            ItemLibrary.Add(this);
            //AddItemsText(string.Format("{0},{1},{2},{3},{4}", type, namenum1, namenum2, namenum3, GenerateName(0)));
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

            data[0x2e] = (byte)type;
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

        static string[] itemNames = { "",
            /*   1 -   5 */ "Battle Axe","Hand Axe","Bardiche","Bec De Corbin","Bill-Guisarme",
            /*   6 -  10 */"Bo Stick", "Club","Dagger","Dart","Fauchard",
            
            /*  11 -  15 */ "Fauchard-Fork","Flail","Military Fork","Glaive","Glaive-Guisarme",
            /*  16 -  20 */ "Guisarme","Guisarme-Voulge","Halberd","Lucern Hammer","Hammer",
            
            /*  21 -  25 */ "Javelin","Jo Stick","Mace","Morning Star","Partisan",
            /*  26 -  30 */ "Military Pick","Awl Pike","Quarrel","Ranseur","Scimitar",
 
            /*  31 -  35 */ "Spear","Spetum","Quarter Staff","Bastard Sword","Broad Sword",
            
            /*  36 -  40 */ "Long Sword","Short Sword","Two-Handed Sword","Trident","Voulge",
            /*  41 -  45 */ "Composite Long Bow","Composite Short Bow","Long Bow","Short Bow","Heavy Crossbow",
            
            /*  46 -  50 */ "Light Crossbow","Sling","Mail","Armor","Leather",
            /*  51 -  55 */ "Padded","Studded","Ring","Scale","Chain",
            /*  56 -  60 */ "Splint","Banded","Plate","Shield","Woods",
            
            /*  61 -  65 */ "Arrow",string.Empty,string.Empty,"Potion","Scroll",
            /*  66 -  70 */ "Ring","Rod","Stave","Wand","Jug",
            /*  71 -  75 */ "Amulet","Dragon Breath","Bag","Defoliation","Ice Storm",
            /*  76 -  80 */ "Book","Boots","Hornets Nest","Bracers","Piercing",

            /*  81 -  85 */ "Brooch","Elfin Chain","Wizardry","ac10", "Dexterity",
            /*  86 -  90 */ "Fumbling","Chime","Cloak","Crystal","Cube",
			/*  91 -  95 */ "Cubic","The Dwarves","Decanter","Gloves","Drums",
            /*  96 - 100 */ "Dust","Thievery","Hat","Flask","Gauntlets",
            
            /* 101 - 105 */ "Gem","Girdle","Helm","Horn","Stupidity",
            /* 106 - 110 */ "Incense","Stone","Ioun Stone", "Javelin","Jewel",
            /* 111 - 115 */ "Ointment","Pale Blue","Scarlet And","Manual","Incandescent",
            
            /* 116 - 120 */ "Deep Red","Pink","Mirror","Necklace","And Green",
            /* 121 - 125 */ "Blue","Pearl","Powerlessness","Vermin","Pipes",
            /* 126 - 130 */ "Hole","Dragon Slayer","Robe","Rope","Frost Brand",
            /* 131 - 135 */ "Berserker","Scarab","Spade","Sphere","Blessed",
            /* 136 - 140 */ "Talisman","Tome","Trident","Grimoire","Well",
            /* 141 - 145 */ "Wings","Vial","Lantern",string.Empty,"Flask of Oil",
            /* 146 - 150 */ "10 ft. Pole","50 ft. Rope","Iron","Thf Prickly Tools","Iron Rations",
            /* 151 - 155 */ "Standard Rations","Holy Symbol","Holy Water vial","Unholy Water vial","Barding",
            /* 156 - 160 */ "Dragon","Lightning","Saddle","Staff","Drow",
            /* 161 - 165 */ "Wagon","+1","+2","+3","+4",
            /* 166 - 170 */ "+5","of","Vulnerability","Cloak","Displacement",
            /* 171 - 175 */ "Torches","Oil","Speed","Tapestry","Spine",
            /* 176 - 180 */ "Copper","Silver","Electrum","Gold","Platinum",
            /* 181 - 185 */ "Ointment","Keoghtum's","Sheet","Strength","Healing",

            /* 186 - 190 */ "Holding","Extra","Gaseous Form","Slipperiness","Jewelled",
            /* 191 - 195 */ "Flying","Treasure Finding","Fear","Disappearance","Statuette",
            /* 196 - 200 */ "Fungus","Chain","Pendant","Broach","Of Seeking",
            /* 201 - 205 */ "-1","-2","-3","Lightning Bolt","Fire Resistance",
            /* 206 - 210 */ "Magic Missiles","Save","Clrc Scroll","MU Scroll","With 1 Spell",
            /* 211 - 215 */ "With 2 Spells","With 3 Spells","Prot. Scroll","Jewelry","Fine",
            /* 216 - 220 */ "Huge","Bone","Brass","Key","AC 2",
            /* 221 - 225 */ "AC 6","AC 4","AC 3","Of Prot.","Paralyzation",
            /* 226 - 230 */ "Ogre Power","Invisibility","Missiles","Elvenkind","Rotting",
            /* 231 - 235 */ "Covered","Efreeti","Bottle","Missile Attractor","Of Maglubiyet",
            /* 236 - 240 */ "Secr Door & Trap Det","Gd Dragon Control","Feather Falling","Giant Strength","Restoring Level(s)",
            /* 241 - 245 */ "Flame Tongue","Fireballs","Spiritual","Boulder","Diamond",
            /* 246 - 250 */ "Emerald","Opal","Saphire","Of Tyr","Of Tempus",
            /* 251 - 255 */ "Of Sune", "Wooden","+3 vs Undead","Pass","Cursed",
        };

        public string GenerateName(int hidden_names_flag)
        {
            int display_flags = 0;
            display_flags |= (namenum1 != 0 && (hidden_names_flag & 0x4) == 0) ? 0x1 : 0;
            display_flags |= (namenum2 != 0 && (hidden_names_flag & 0x2) == 0) ? 0x2 : 0;
            display_flags |= (namenum3 != 0 && (hidden_names_flag & 0x1) == 0) ? 0x4 : 0;

            bool pural_added = false;
            string name = "";

            for (int var_1 = 3; var_1 >= 1; var_1--)
            {
                if (((display_flags >> (var_1 - 1)) & 1) > 0)
                {
                    name += itemNames[field_2EArray(var_1)];

                    if (count < 2 ||
                        pural_added == true)
                    {
                        name += " ";
                    }
                    else if ((1 << (var_1 - 1) == display_flags) ||
                            (var_1 == 1 && display_flags > 4 && type != ItemType.FlaskOfOil) ||
                            (var_1 == 2 && (display_flags & 1) == 0) ||
                            (var_1 == 3 && type == ItemType.FlaskOfOil) ||
                            (namenum3 != 0x87 && (type == ItemType.Arrow || type == ItemType.Quarrel || type == ItemType.Dart) && namenum3 != 0xb1))
                    {
                        name += "s ";
                        pural_added = true;
                    }
                    else
                    {
                        name += " ";
                    }
                }
            }

            return name.Trim();
        }


        public override bool Equals(object obj)
        {
            Item rhs = obj as Item;
            return rhs != null &&
                rhs.type == type &&
                rhs.namenum1 == namenum1 &&
                rhs.namenum2 == namenum2 &&
                rhs.namenum3 == namenum3 &&
                rhs.plus == plus &&
                rhs.plus_save == plus_save &&
                rhs.readied == readied &&
                rhs.hidden_names_flag == hidden_names_flag &&
                rhs.cursed == cursed &&
                rhs.weight == weight &&
                rhs.count == count &&
                rhs._value == _value &&
                rhs.affect_1 == affect_1 &&
                rhs.affect_2 == affect_2 &&
                rhs.affect_3 == affect_3
                ;
        }

        public override string ToString()
        {
            return GenerateName(0);
        }


        public bool IsRanged()
        {
            return gbl.ItemDataTable[type].range > 1;
        }
    }
}
