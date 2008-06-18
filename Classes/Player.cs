using System;


namespace Classes
{
    public enum Status
    {
        okey = 0x0,
        animated = 0x1,
        tempgone = 0x2,
        running = 0x3,
        unconscious = 0x4,
        dying = 0x5,
        dead = 0x6,
        stoned = 0x7,
        gone = 0x8
    }

    public enum Stat
    {
        STR,
        INT,
        WIS,
        DEX,
        CON,
        CHA
    }

    public enum Race
    {
        monster = 0,
        dwarf = 1,
        elf = 2,
        gnome = 3,
        half_elf = 4,
        halfling = 5,
        half_orc = 6,
        human = 7
    }

    public enum ClassId
    {
        cleric = 0x0,
        druid = 0x1,
        fighter = 0x2,
        paladin = 0x3,
        ranger = 0x4,
        magic_user = 0x5,
        thief = 0x6,
        monk = 0x7,
        mc_c_f = 0x8,
        mc_c_f_m = 0x9,
        mc_c_r = 0xa,
        mc_c_mu = 0xb,
        mc_c_t = 0xc,
        mc_f_mu = 0xd,
        mc_f_t = 0xe,
        mc_f_mu_t = 0xf,
        mc_mu_t = 0x10,
        unknown = 0x11
    }

    public enum Skills
    {
        cleric = 0,
        druid = 1,
        fighter = 2,
        paladin = 3,
        ranger = 4,
        magic_user = 5,
        thief = 6,
        monk = 7
    }

    public struct StatValue
    {
        public byte tmp;
        public byte max;
    }

    public enum CombatTeam
    {
        Ours = 0,
        Enemy = 1
    }

    /// <summary>
    /// Summary description for Player.
    /// </summary>
    public class Player   
    {
        [DataOffset(0x00,DataType.PString,15)]
        public string name;

        [DataOffset(0x10, DataType.Cust1Array, 6)]
        public StatValue[] stats;

        public byte tmp_str // 0x10;
        {
            set { stats[0].tmp = value; }
            get { return stats[0].tmp; }
        }
        public byte strength // 0x11;
        {
            set { stats[0].max = value; }
            get { return stats[0].max; }
        }

        public byte tmp_int // 0x12;
        {
            set { stats[1].tmp = value; }
            get { return stats[1].tmp; }
        }
        public byte _int // 0x13;
        {
            set { stats[1].max = value; }
            get { return stats[1].max; }
        }

        public byte tmp_wis // 0x14;
        {
            set { stats[2].tmp = value; }
            get { return stats[2].tmp; }
        }
        public byte wis // 0x15;
        {
            set { stats[2].max = value; }
            get { return stats[2].max; }
        }

        public byte tmp_dex // 0x16;
        {
            set { stats[3].tmp = value; }
            get { return stats[3].tmp; }
        }
        public byte dex // 0x17;
        {
            set { stats[3].max = value; }
            get { return stats[3].max; }
        }

        public byte tmp_con // 0x18;
        {
            set { stats[4].tmp = value; }
            get { return stats[4].tmp; }
        }
        public byte con // 0x19;
        {
            set { stats[4].max = value; }
            get { return stats[4].max; }
        }
        public byte tmp_cha // 0x1a;
        {
            set { stats[5].tmp = value; }
            get { return stats[5].tmp; }
        }
        public byte charisma // 0x1b;
        {
            set { stats[5].max = value; }
            get { return stats[5].max; }
        }
        [DataOffset(0x1C, DataType.Byte)]
        public byte tmp_str_00; // 0x1c - strength_18_100
        [DataOffset(0x1D, DataType.Byte)]
        public byte max_str_00; // 0x1d - field_1D

        public const int SpellListSize = 84;
        [DataOffset(0x1E, DataType.ByteArray, SpellListSize)]
        public byte[] spell_list = new byte[SpellListSize]; // 0x1e byte[84]
        [DataOffset(0x72, DataType.Byte)]
        public byte spell_to_learn_count; // 0x72;
        [DataOffset(0x73, DataType.SByte)]
        public sbyte field_73; // 0x73;

        [DataOffset(0x74, DataType.IByte)]
        public Race race; // 0x74;

        [DataOffset(0x75, DataType.IByte)]
        public ClassId _class; // 0x75;
        [DataOffset(0x76, DataType.SWord)]
        public short age; // 0x76;

        [DataOffset(0x78, DataType.Byte)]
        public byte hit_point_max; // 0x78;
        
        [DataOffset(0x79, DataType.ByteArray,100)]
        public byte[] field_79 = new byte[100]; //78[di]; // 1- 100 or 0x79 - 0xDC

        [DataOffset(0xdd, DataType.Byte)]
        public byte field_DD; // 0xdd;
        [DataOffset(0xde, DataType.Byte)]
        public byte field_DE; // 0xde;
        [DataOffset(0xdf, DataType.Byte)]
        public byte field_DF; // 0xdf;
        public byte field_DFArrayGet(int index)
        {
            switch (index)
            {
                case 0:
                    return field_DF;
                case 1:
                    return field_E0;
                case 2:
                    return field_E1;
                case 3:
                    return field_E2;
                case 4:
                    return field_E3;

                default:
                    throw new System.NotImplementedException();
            }
        }
        public void field_DFArraySet(int index, byte value)
        {
            switch (index)
            {
                case 0:
                    field_DF = value; break;
                case 1:
                    field_E0 = value; break;
                case 2:
                    field_E1 = value; break;
                case 3:
                    field_E2 = value; break;
                case 4:
                    field_E3 = value; break;

                default:
                    throw new System.NotImplementedException();
            }
        }
        [DataOffset(0xe0, DataType.Byte)]
        public byte field_E0; // 0xe0;
        [DataOffset(0xe1, DataType.Byte)]
        public byte field_E1; // 0xe1;
        [DataOffset(0xe2, DataType.Byte)]
        public byte field_E2; // 0xe2;
        [DataOffset(0xe3, DataType.Byte)]
        public byte field_E3; // 0xe3;
        [DataOffset(0xe4, DataType.Byte)]
        public byte field_E4; // 0xe4;
        [DataOffset(0xe5, DataType.Byte)]
        public byte field_E5; // 0xe5;
        [DataOffset(0xe6, DataType.Byte)]
        public byte field_E6; // 0xe6;
        [DataOffset(0xe7, DataType.Byte)]
        public byte field_E7; // 0xe7;
        [DataOffset(0xe8, DataType.Byte)]
        public byte field_E8; // 0xe8;
        [DataOffset(0xe9, DataType.Byte)]
        public byte field_E9; // 0xe9;
        [DataOffset(0xeA, DataType.ByteArray,8)]
        public byte[] field_EA = new byte[8]; // 0xeA; [] was 1 offset @ 0xe9
        public Affect affect_ptr; // f2

        [DataOffset(0xf7, DataType.Byte)]
        public byte field_F7; // 0xf7; // if 0 or 0xB3 you can pool money
        [DataOffset(0xf8, DataType.Byte)]
        public byte field_F8; // 0xf8;
        [DataOffset(0xfa, DataType.Byte)]
        public byte field_FA; // 0xfa;

        [DataOffset(0xfb, DataType.ShortArray, 7)]
        public short[] Money = new short[7];

        public short copper // 0xfb
        {
            get { return Money[0]; }
            set { Money[0] = value; }
        }
        public short electrum // 0xfd
        {
            get { return Money[1]; }
            set { Money[1] = value; }
        }
        public short silver // 0xff
        {
            get { return Money[2]; }
            set { Money[2] = value; }
        }
        public short gold // 0x101
        {
            get { return Money[3]; }
            set { Money[3] = value; }
        }
        public short platinum // 0x103
        {
            get { return Money[4]; }
            set { Money[4] = value; }
        }
        public short gems // 0x105
        {
            get { return Money[5]; }
            set { Money[5] = value; }
        }
        public short jewels // 0x107
        {
            get { return Money[6]; }
            set { Money[6] = value; }
        }

        [DataOffset(0x109, DataType.ByteArray,8)]
        public byte[] Skill_A_lvl = new byte[8];

        public byte cleric_lvl // 0x109;
        {
            get { return Skill_A_lvl[0]; }
            set { Skill_A_lvl[0] = value; }
        }
        public byte druid_lvl // 0x10a;
        {
            get { return Skill_A_lvl[1]; }
            set { Skill_A_lvl[1] = value; }
        }
        public byte fighter_lvl // 0x10b;
        {
            get { return Skill_A_lvl[2]; }
            set { Skill_A_lvl[2] = value; }
        }
        public byte paladin_lvl // 0x10c;
        {
            get { return Skill_A_lvl[3]; }
            set { Skill_A_lvl[3] = value; }
        }
        public byte ranger_lvl // 0x10d;
        {
            get { return Skill_A_lvl[4]; }
            set { Skill_A_lvl[4] = value; }
        }
        public byte magic_user_lvl // 0x10e;
        {
            get { return Skill_A_lvl[5]; }
            set { Skill_A_lvl[5] = value; }
        }
        public byte thief_lvl // 0x10f;
        {
            get { return Skill_A_lvl[6]; }
            set { Skill_A_lvl[6] = value; }
        }
        public byte monk_lvl // 0x110;
        {
            get { return Skill_A_lvl[7]; }
            set { Skill_A_lvl[7] = value; }
        }

        [DataOffset(0x111, DataType.ByteArray, 8)]
        public byte[] Skill_B_lvl = new byte[8];


        public byte turn_undead // 0x111;
        {
            get { return Skill_B_lvl[0]; }
            set { Skill_B_lvl[0] = value; }
        }
        public byte field_112 // 0x112;
        {
            get { return Skill_B_lvl[1]; }
            set { Skill_B_lvl[1] = value; }
        }
        public byte field_113 // 0x113;
        {
            get { return Skill_B_lvl[2]; }
            set { Skill_B_lvl[2] = value; }
        }
        public byte field_114 // 0x114;
        {
            get { return Skill_B_lvl[3]; }
            set { Skill_B_lvl[3] = value; }
        }
        public byte field_115 // 0x115;
        {
            get { return Skill_B_lvl[4]; }
            set { Skill_B_lvl[4] = value; }
        }
        public byte field_116 // 0x116;
        {
            get { return Skill_B_lvl[5]; }
            set { Skill_B_lvl[5] = value; }
        }
        public byte field_117 // 0x117;
        {
            get { return Skill_B_lvl[6]; }
            set { Skill_B_lvl[6] = value; }
        }
        public byte field_118 // 0x118;
        {
            get { return Skill_B_lvl[7]; }
            set { Skill_B_lvl[7] = value; }
        }

        [DataOffset(0x119, DataType.Byte)]
        public byte sex; // 0x119;
        [DataOffset(0x11a, DataType.Byte)]
        public byte field_11A; // 0x11a;
        [DataOffset(0x11b, DataType.Byte)]
        public byte alignment; // 0x11b;
        [DataOffset(0x11c, DataType.Byte)]
        public byte field_11C; // 0x11c;
        [DataOffset(0x11d, DataType.Byte)]
        public byte field_11D; // 0x11d;
        [DataOffset(0x11e, DataType.Byte)]
        public byte field_11E; // 0x11e;
        [DataOffset(0x11f, DataType.Byte)]
        public byte field_11F; // 0x11f;
        [DataOffset(0x120, DataType.Byte)]
        public byte field_120; // 0x120;
        [DataOffset(0x121, DataType.Byte)]
        public byte field_121; // 0x121;
        [DataOffset(0x122, DataType.SByte)]
        public sbyte field_122; // 0x122;
        [DataOffset(0x123, DataType.Byte)]
        public byte field_123; // 0x123;
        [DataOffset(0x124, DataType.Byte)]
        public byte field_124; // 0x124;
        [DataOffset(0x125, DataType.Byte)]
        public byte field_125; // 0x125;
        [DataOffset(0x126, DataType.Byte)]
        public byte field_126; // 0x126;
        [DataOffset(0x127, DataType.Int)]
        public int exp; // 0x127
        [DataOffset(0x12b, DataType.Byte)]
        public byte classFlags; // 0x12b;
        [DataOffset(0x12c, DataType.Byte)]
        public byte field_12C; // 0x12c;

        //[DataOffset(0x12d, DataType.ByteArray,15)]
        public byte[,] field_12D = new byte[3,5]; // 0x12d - field_12D

        [DataOffset(0x13c, DataType.SWord)]
        public short field_13C; // 0x13c
        [DataOffset(0x13e, DataType.Byte)]
        public byte field_13E; // 0x13e;
        [DataOffset(0x13f, DataType.Byte)]
        public byte field_13F; // 0x13f;
        [DataOffset(0x140, DataType.Byte)]
        public byte field_140; // 0x140;
        [DataOffset(0x141, DataType.Byte)]
        public byte field_141; // 0x141;
        [DataOffset(0x142, DataType.Byte)]
        public byte field_142; // 0x142;
        [DataOffset(0x143, DataType.Byte)]
        public byte icon_id; // 0x143;
        [DataOffset(0x144, DataType.Byte)]
        public byte icon_size; // 0x144; field_144  1 small 2 normal
        [DataOffset(0x145, DataType.ByteArray,6)]
        public byte[] field_145 = new byte[6]; // 0x145 = field_144[1] // byte[6]
        [DataOffset(0x14b, DataType.Byte)]
        public byte field_14B; // 0x14b;
        [DataOffset(0x14c, DataType.Byte)]
        public byte field_14C; // 0x14c; // 0 no item to use?


        public Item itemsPtr; // 0x14d
        public Item[] itemArray = new Item[13]; // 0x151[]
        public Item field_151
        {// 0x151
            get { return itemArray[0]; }
            set { itemArray[0] = value; }
        }
        public Item field_155
        { // 0x155 
            get { return itemArray[1]; }
            set { itemArray[1] = value; }
        }

        public Item field_159
        { // 0x159 
            get { return itemArray[2]; }
            set { itemArray[2] = value; }
        }

        public Item field_15D
        { // 0x15D
            get { return itemArray[3]; }
            set { itemArray[3] = value; }
        }
        public Item field_161
        { // 0x161
            get { return itemArray[4]; }
            set { itemArray[4] = value; }
        }
        public Item field_165
        { // 0x165
            get { return itemArray[5]; }
            set { itemArray[5] = value; }
        }
        public Item field_169
        { // 0x169 
            get { return itemArray[6]; }
            set { itemArray[6] = value; }
        }
        public Item field_16D
        { // 0x16D
            get { return itemArray[7]; }
            set { itemArray[7] = value; }
        }
        public Item field_171
        { // 0x171
            get { return itemArray[8]; }
            set { itemArray[8] = value; }
        }
        public Item Item_ptr_01
        { // 0x175
            get { return itemArray[9]; }
            set { itemArray[9] = value; }
        }

        /// <summary>
        /// 0x179
        /// </summary>
        public Item Item_ptr_02
        {
            get { return itemArray[10]; }
            set { itemArray[10] = value; }
        }

        /// <summary>
        /// 0x17d
        /// </summary>
        public Item Item_ptr_03
        {
            get { return itemArray[11]; }
            set { itemArray[11] = value; }
        }

        /// <summary>
        /// 0x181
        /// </summary>
        public Item Item_ptr_04
        { 
            get { return itemArray[12]; }
            set { itemArray[12] = value; }
        }

        [DataOffset(0x185, DataType.Byte)]
        public byte field_185; // 0x185;
        [DataOffset(0x186, DataType.SByte)]
        public sbyte field_186; // 0x186;
        [DataOffset(0x187, DataType.SWord)]
        public short weight; // 0x187

        public Player next_player; // 0x189
        public Action actions; // 0x18d
        [DataOffset(0x191, DataType.Byte)]
        public byte field_191; // 0x191
        [DataOffset(0x192, DataType.Byte)]
        public byte field_192; // 0x192
        [DataOffset(0x193, DataType.Byte)]
        public byte field_193; // 0x193
        [DataOffset(0x194, DataType.Byte)]
        public byte field_194; // 0x194
        [DataOffset(0x195, DataType.IByte)]
        public Status health_status; // 0x195
        [DataOffset(0x196, DataType.Bool)]
        public bool in_combat; // 0x196
        [DataOffset(0x197, DataType.IByte)]
        public CombatTeam combat_team; // 0x197 0 - our team, 1 - enemy
        [DataOffset(0x198, DataType.IByte)]
        public QuickFight quick_fight; // 0x198
        [DataOffset(0x199, DataType.SByte)]
        public sbyte hitBonus; // 0x199 field_199
        [DataOffset(0x19a, DataType.Byte)]
        public byte ac; // 0x19a
        [DataOffset(0x19b, DataType.Byte)]
        public byte field_19B; // 0x19b

        public byte field_19BArray(int index)
        {
            switch (index)
            {
                case 1:
                    return field_19C;
                case 2:
                    return field_19D;
                default:
                throw new System.NotImplementedException();
            }
        }

        public void field_19BArraySet(int index, byte value)
        {
            switch (index)
            {
                case 1:
                    field_19C = value; 
                    break;
                case 2:
                    field_19D = value;
                    break;
                default:
                    throw new System.NotImplementedException();
            }
        }

        [DataOffset(0x19c, DataType.Byte)]
        public byte field_19C; // 0x19c
        [DataOffset(0x19d, DataType.Byte)]
        public byte field_19D; // 0x19d
        public byte field_19DArray(int index)
        {
            switch (index)
            {
                case 1:
                    return field_19E;
                case 2:
                    return field_19F;
                default:
                    throw new System.NotImplementedException();
            }
        }

        [DataOffset(0x19E, DataType.Byte)]
        public byte field_19E; // 0x19e
        [DataOffset(0x19F, DataType.Byte)]
        public byte field_19F; // 0x19f
        public byte field_19FArray(int index)
        {
            switch (index)
            {
                case 1:
                    return field_1A0;
                case 2:
                    return field_1A1;
                default:
                    throw new System.NotImplementedException();
            }
        }
        [DataOffset(0x1A0, DataType.Byte)]
        public byte field_1A0; // 0x1a0
        [DataOffset(0x1A1, DataType.Byte)]
        public byte field_1A1; // 0x1a1
        public byte field_1A1Array(int index)
        {
            switch (index)
            {
                case 1:
                    return (byte)damageBonus;
                case 2:
                    return field_1A3;
                default:
                    throw new System.NotImplementedException();
            }
        }
        [DataOffset(0x1a2, DataType.SByte)]
        public sbyte damageBonus; // 0x1a2
        [DataOffset(0x1a3, DataType.Byte)]
        public byte field_1A3; // 0x1a3
        [DataOffset(0x1a4, DataType.Byte)]
        public byte hit_point_current; // 0x1a4


        [DataOffsetAttribute(0x1A5, DataType.Byte)]
        public byte movement; // 0x1a5 initiative

        public const int StructSize = 0x1A6;

        public Player()
        {
            Init();
        }

        public Player(byte[] data, int offset)
        {
            Init();

            DataIO.ReadObject(this, data, offset);

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 5; j++)
                    field_12D[i, j] = data[0x12d + j + (i * i)];
        }

        private void Init()
        {
            field_12D = new byte[3, 5];
            stats = new StatValue[6];

            name = string.Empty;
            itemsPtr = null;
            affect_ptr = null;
            next_player = null;
            actions = null;
        }


        public Player ShallowClone()
        {
            Player p = (Player)this.MemberwiseClone();
            return p;
        }


        public byte[] ToByteArray()
        {
            byte[] data = new byte[StructSize];

            DataIO.WriteObject(this, data);

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 5; j++)
                    data[0x12d + j + (i * i)] = field_12D[i, j];

            return data;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
