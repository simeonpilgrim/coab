using System;

namespace Classes
{
	/// <summary>
	/// Summary description for Struct_1C020.
	/// </summary>
	public class ItemData
	{
        public byte item_slot; //seg600:5D10 unk_1C020 - field_0
		public byte field_1; //seg600:5D11 unk_1C021
        public byte diceCount; //seg600:5D12 unk_1C022
        public byte diceSize; //seg600:5D13 unk_1C023
		public sbyte field_4; //seg600:5D14
		public byte field_5; //seg600:5D15
		public byte field_6; //seg600:5D16 unk_1C026
		public byte field_7; //seg600:5D17 unk_1C027
		public byte field_8; //seg600:5D18
        public byte diceCountX; //seg600:5D19 field_9 maybe ranged 
        public byte diceSizeX; //seg600:5D1A field_A  maybe ranged
		public sbyte field_B; //seg600:5D1B
		public int range; //seg600:5D1C unk_1C02C
		public byte classFlags; //seg600:5D1D field_D
		public byte field_E; //seg600:5D1E unk_1C02E
		public byte field_F; //seg600:5D1F 

        //public ItemData()
        //{
        //}

        public ItemData(byte[] data, int offset)
        {
            item_slot = data[offset + 0];
            field_1 = data[offset + 1];
            diceCount = data[offset + 2];
            diceSize = data[offset + 3];
            field_4 = (sbyte)data[offset + 4];
            field_5 = data[offset + 5];
            field_6 = data[offset + 6];
            field_7 = data[offset + 7];
            field_8 = data[offset + 8];
            diceCountX = data[offset + 9];
            diceSizeX = data[offset + 0xa];
            field_B = (sbyte)data[offset + 0xb];
            range = data[offset + 0xc];
            classFlags = data[offset + 0xd];
            field_E = data[offset + 0xe];
            field_F = data[offset + 0xf];
        }
	}

    enum ItemType
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
        //Mail = 48,
        //Armor = 49,
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
        MagicUserScroll = 61,
        ClericScroll = 62,
        GauntletsOfOgrePowerA = 63,
        GauntletsOfOgrePowerB = 67,
        GemJewel = 70,
        PotionOfHealing = 71,
        Arrow = 73,
        Bracers = 77,
        WandOfMagicMissilesA = 78,
        WandOfMagicMissilesB = 79,
        PotionOfGiantStrengthA = 84,
        PotionOfGiantStrengthB = 92,
        RingOfProtection = 93,
    }
}
