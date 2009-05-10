using System;

namespace Classes
{
    [FlagsAttribute]
    public enum ItemDataFlags : byte
    {
        None = 0,
        arrows = 0x01,
        flag_02 = 0x02,
        melee = 0x04,
        flag_08 = 0x08,
        flag_10 = 0x10,
        flag_20 = 0x20,
        flag_40 = 0x40,
        quarrels = 0x80,

    }

    public class ItemDataTable
    {
        ItemData[] table;

        public ItemDataTable(byte[] data)
        {
            table = new ItemData[0x81];
            for (int i = 0; i < 0x81; i++)
            {
                table[i] = new ItemData(data, i * 0x10, i);
            }
        }

        public ItemData this[ItemType index]
        {
            get { return table[(int)index]; }
            set { table[(int)index] = value; }
        }
    }

	/// <summary>
	/// Summary description for Struct_1C020.
	/// </summary>
    public class ItemData
    {
        public int index;

        public byte item_slot; //seg600:5D10 unk_1C020 - field_0
        public byte handsCount; //seg600:5D11 unk_1C021
        public byte diceCountLarge; //seg600:5D12 unk_1C022
        public byte diceSizeLarge; //seg600:5D13 unk_1C023
        public sbyte bonusLarge; //seg600:5D14
        public byte numberAttacks; //seg600:5D15
        public byte field_6; //seg600:5D16 unk_1C026
        public byte field_7; //seg600:5D17 unk_1C027
        public byte field_8; //seg600:5D18
        public byte diceCountNormal; //seg600:5D19 field_9 maybe ranged 
        public byte diceSizeNormal; //seg600:5D1A field_A  maybe ranged
        public sbyte bonusNormal; //seg600:5D1B
        public int range; //seg600:5D1C unk_1C02C
        public byte classFlags; //seg600:5D1D field_D
        public ItemDataFlags field_E; //seg600:5D1E unk_1C02E
        public byte field_F; //seg600:5D1F 


        public ItemData(byte[] data, int offset, int _index)
        {
            index = _index;

            item_slot = data[offset + 0];
            handsCount = data[offset + 1];
            diceCountLarge = data[offset + 2];
            diceSizeLarge = data[offset + 3];
            bonusLarge = (sbyte)data[offset + 4];
            numberAttacks = data[offset + 5];
            field_6 = data[offset + 6];
            field_7 = data[offset + 7];
            field_8 = data[offset + 8];
            diceCountNormal = data[offset + 9];
            diceSizeNormal = data[offset + 0xa];
            bonusNormal = (sbyte)data[offset + 0xb];
            range = data[offset + 0xc];
            classFlags = data[offset + 0xd];
            field_E = (ItemDataFlags)data[offset + 0xe];
            field_F = data[offset + 0xf];
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}",
                   item_slot, handsCount, diceCountLarge, diceSizeLarge, bonusLarge, numberAttacks, field_6, field_7, field_8,
                   diceCountNormal, diceSizeNormal, bonusNormal, range, classFlags, field_E, field_F);
        }
    }


    public enum ItemType
    {
        BattleAxe = 1,
        HandAxe = 2,
        Bardiche = 3,
        BecDeCorbin = 4,
        BillGuisarme = 5,
        BoStick = 6,
        Club = 7,
        Dagger = 8,
        Dart = 9,
        Fauchard = 10,
        FauchardFork = 11,
        Flail = 12,
        MilitaryFork = 13,
        Glaive = 14,
        GlaiveGuisarme = 15,
        Guisarme = 16,
        GuisarmeVoulge = 17,
        Halberd = 18,
        LucernHammer = 19,
        Hammer = 20,
        Javelin = 21,
        JoStick = 22,
        Mace = 23,
        MorningStar = 24,
        Partisan = 25,
        MilitaryPick = 26,
        AwlPike = 27,
        Quarrel = 28,
        Ranseur = 29,
        Scimitar = 30,
        Spear = 31,
        Spetum = 32,
        QuarterStaff = 33,
        BastardSword = 34,
        BroadSword = 35,
        LongSword = 36,
        ShortSword = 37,
        TwoHandedSword = 38,
        Trident = 39,
        Voulge = 40,
        CompositeLongBow = 41,
        CompositeShortBow = 42,
        LongBow = 43,
        ShortBow = 44,
        HeavyCrossbow = 45,
        LightCrossbow = 46,
        Sling = 47,
        LeatherArmor = 50,
        PaddedArmor = 51,
        StuddedLeather = 52,
        RingMail = 53,
        ScaleMail = 54,
        ChainMail = 55,
        SplintMail = 56,
        BandedMail = 57,
        PlateMail = 58,
        Shield = 59,
        ScrollOfProt = 60,
        MUScroll = 61,
        ClrcScroll = 62,
        Gauntlets = 63, // Gloves, Gauntlets
        Girdle = 65,
        Type_67 = 67,
        RingInvis = 69,
        Necklace = 70, // Gems, Jewel, Necklace, Dust
        Potion = 71,
        Arrow = 73,
        Bracers = 77,
        WandA = 78,
        WandB = 79,
        Type_85 = 85,
        FlaskOfOil = 86,
        Type_87 = 87,
        Type_88 = 88,
        Type_89 = 89,
        Cloak = 92,
        RingOfProt = 93,
        DrowMage = 94,
        DrowChainMail = 96,
        DrowLongSword = 97,
        Spine = 98,
        RingOfWizardry = 99,
        DartOfHornetsNest = 100,
        StaffSling = 101  
    }

}
