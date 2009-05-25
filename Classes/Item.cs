using System;

namespace Classes
{
	/// <summary>
	/// Summary description for Item.
	/// </summary>
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
            byte _namenum2, byte _namenum1, ItemType _type)
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

            AddItemsText(string.Format("{0},{1},{2},{3},{4}", type, namenum1, namenum2, namenum3, GenerateName(0)));
            //System.Console.WriteLine("ITEM:,{0},{1},{2},{3},{4}", type, namenum1, namenum2, namenum3, GenerateName(0)); 
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
            "Battle Axe","Hand Axe","Bardiche","Bec De Corbin","Bill-Guisarme",
            "Bo Stick", "Club","Dagger","Dart","Fauchard",
            
            "Fauchard-Fork","Flail","Military Fork","Glaive","Glaive-Guisarme",
            "Guisarme","Guisarme-Voulge","Halberd","Lucern Hammer","Hammer",
            
            "Javelin","Jo Stick","Mace","Morning Star","Partisan",      
            "Military Pick","Awl Pike","Quarrel","Ranseur","Scimitar",
 
            "Spear","Spetum","Quarter Staff","Bastard Sword","Broad Sword",
            
            "Long Sword","Short Sword","Two-Handed Sword","Trident","Voulge",
            "Composite Long Bow","Composite Short Bow","Long Bow","Short Bow","Heavy Crossbow",
            
            "Light Crossbow","Sling","Mail","Armor","Leather",
            "Padded","Studded","Ring","Scale","Chain",					 
            "Splint","Banded","Plate","Shield","Woods",
            
            "Arrow",string.Empty,string.Empty,"Potion","Scroll",
            "Ring","Rod","Stave","Wand","Jug",
            "Amulet","Dragon Breath","Bag","Defoliation","Ice Storm",
            "Book","Boots","Hornets Nest","Bracers","Piercing",

            "Brooch","Elfin Chain","Wizardry","ac10", "Dexterity",
            "Fumbling","Chime","Cloak","Crystal","Cube",
			"Cubic","The Dwarves","Decanter","Gloves","Drums",
            "Dust","Thievery","Hat","Flask","Gauntlets",
            
            "Gem","Girdle","Helm","Horn","Stupidity",
            "Incense","Stone","Ioun Stone", "Javelin","Jewel",
            "Ointment","Pale Blue","Scarlet And","Manual","Incandescent",
            
            "Deep Red","Pink","Mirror","Necklace","And Green",
            "Blue","Pearl","Powerlessness",
								 "Vermin","Pipes","Hole","Dragon Slayer","Robe","Rope",
								 "Frost Brand","Berserker","Scarab","Spade","Sphere",
								 "Blessed","Talisman","Tome","Trident","Grimoire","Well",
								 "Wings","Vial","Lantern",string.Empty,"Flask of Oil",
								 "10 ft. Pole","50 ft. Rope","Iron","Thf Prickly Tools",
								 "Iron Rations","Standard Rations","Holy Symbol",
								 "Holy Water vial","Unholy Water vial","Barding","Dragon",
								 "Lightning","Saddle","Staff","Drow","Wagon","+1",
								 "+2","+3","+4","+5","of","Vulnerability","Cloak",
								 "Displacement","Torches","Oil","Speed","Tapestry",
								 "Spine","Copper","Silver","Electrum","Gold","Platinum",
								 "Ointment","Keoghtum's","Sheet","Strength","Healing",
								 
                                 "Holding","Extra","Gaseous Form","Slipperiness",
								 "Jewelled","Flying","Treasure Finding","Fear",
								 "Disappearance","Statuette","Fungus","Chain","Pendant",
								 "Broach","Of Seeking","-1","-2","-3","Lightning Bolt",
								 "Fire Resistance","Magic Missiles","Save","Clrc Scroll",
								 "MU Scroll","With 1 Spell","With 2 Spells","With 3 Spells",
								 "Prot. Scroll","Jewelry","Fine","Huge","Bone","Brass",
								 "Key","AC 2","AC 6","AC 4","AC 3","Of Prot.","Paralyzation",
								 "Ogre Power","Invisibility","Missiles","Elvenkind",
								 "Rotting","Covered","Efreeti","Bottle","Missile Attractor",
								 "Of Maglubiyet","Secr Door & Trap Det","Gd Dragon Control",
								 "Feather Falling","Giant Strength","Restoring Level(s)",
								 "Flame Tongue","Fireballs","Spiritual","Boulder","Diamond",
								 "Emerald","Opal","Saphire","Of Tyr","Of Tempus","Of Sune",
								 "Wooden","+3 vs Undead","Pass","Cursed" 
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

            return name;
        }

        static System.Collections.Generic.List<string> itemtext = new System.Collections.Generic.List<string>();

        static void AddItemsText(string text)
        {
            if (itemtext.Contains(text) == false)
            {
                itemtext.Add(text);
            }
        }

        public static void DumpItemsText(string filename)
        {
            System.IO.TextWriter tw = new System.IO.StreamWriter(filename, true);

            foreach (string s in itemtext)
            {
                tw.WriteLine(s);
            }
            tw.Flush();
            tw.Close();
        }

        public override string ToString()
        {
            return GenerateName(0);
        }
    }
}
