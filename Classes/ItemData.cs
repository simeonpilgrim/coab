using System;
using System.IO;

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

    public enum ItemSlot
    {
        Weapon = 0,
        Shield = 1,
        Armor = 2,
        Gauntlets = 3,
        Helm = 4,
        Belt = 5,
        Robe = 6,
        Cloak = 7,
        Boots = 8,
        Ring1 = 9,
        Ring2 = 10,
        Arrow = 11,
        Quarrel = 12,
        slot_13 = 13
    }

    public class ItemDataTable
    {
        ItemData[] table;

        public ItemDataTable(string fileName)
        {
            string filePath = Path.Combine(gbl.exe_path, fileName);

            FileStream stream = System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read);

            stream.Seek(2, SeekOrigin.Begin);
            byte[] data = new byte[0x810];
            stream.Read(data, 0, 0x810);

            table = new ItemData[0x81];
            for (int i = 0; i < 0x81; i++)
            {
                table[i] = new ItemData(data, i * 0x10);
            }

            stream.Close();
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
        public ItemSlot item_slot; //seg600:5D10 unk_1C020 - field_0
        public byte handsCount; //seg600:5D11 unk_1C021
        public byte diceCountLarge; //seg600:5D12 unk_1C022
        public byte diceSizeLarge; //seg600:5D13 unk_1C023
        public sbyte bonusLarge; //seg600:5D14
        public int numberAttacks; //seg600:5D15
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


        public ItemData(byte[] data, int offset)
        {
            item_slot = (ItemSlot)data[offset + 0];
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
        Type_0 = 0,
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
        Type_48 = 48,
        Type_49 = 49,
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
        Hat = 64,
        Girdle = 65,
        Robe = 66,
        CloakOfProt = 67,
        Boots = 68,
        Ring = 69, // Invis, Fire Resistence
        GemsJewelry = 70, // Gems, Jewel, Necklace, Dust, Ioun Stone
        Potion = 71, // Silver Mirror
        Commodities = 72, // Tapestry, Statuette, Chain of Bone, Rug, Brazier, Saddle
        Arrow = 73,
        HolySymbol = 74,
        Key = 75,
        CursedNecklace = 76,
        Bracers = 77,
        WandA = 78,
        WandB = 79,
        Type_80 = 80,
        Type_81 = 81,
        EfreetiBottle = 82,
        Type_83 = 83,
        PotionOfGiantStr = 84,
        HolyWater = 85,
        FlaskOfOil = 86,
        HillGiantBoulder = 87,
        CloudGiantBoulder = 88,
        FireGiantBoulder = 89,
        RingOfFeatherFall = 90,
        Pass = 91,
        Cloak = 92,
        RingOfProt = 93,
        DrowMace = 94,
        ElvenChain = 95,
        DrowChainMail = 96,
        DrowLongSword = 97,
        Spine = 98,
        RingOfWizardry = 99,
        DartOfHornetsNest = 100,
        StaffSling = 101,
        Type_102 = 102,
        Type_103 = 103,
        Type_104 = 104,
        Type_105 = 105,
        Type_106 = 106,
        Type_107 = 107,
        Type_108 = 108,
        Type_109 = 109,
        Type_110 = 110,
        Type_128 = 128,
    }

}
