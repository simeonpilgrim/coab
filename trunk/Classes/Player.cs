using System;
using System.Collections.Generic;


namespace Classes
{

    public struct StatValue
    {
        public byte tmp;
        public byte max;
    }


    public class Control
    {
        public const byte PC_Base = 0;
        public const byte PC_Mask = 0x7F;
        public const byte NPC_Base = 0x80;
        public const byte NPC_Berzerk = 0xB2;
        public const byte PC_Berzerk = 0xB3;
    }



    /// <summary>
    /// Summary description for Player.
    /// </summary>
    public class Player
    {
        [DataOffset(0x00, DataType.PString, 15)]
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

        //public const int SpellListSize = 84;
        //[DataOffset(0x1E, DataType.ByteArray, SpellListSize)]
        //public byte[] spell_list = new byte[SpellListSize]; // 0x1e byte[84]
        public SpellList spellList; // ox1e was spell_list

        [DataOffset(0x72, DataType.Byte)]
        public byte spell_to_learn_count; // 0x72;
        [DataOffset(0x73, DataType.SByte)]
        public sbyte thac0; // 0x73; field_73

        [DataOffset(0x74, DataType.IByte)]
        public Race race; // 0x74;

        [DataOffset(0x75, DataType.IByte)]
        public ClassId _class; // 0x75;
        [DataOffset(0x76, DataType.SWord)]
        public short age; // 0x76;

        [DataOffset(0x78, DataType.Byte)]
        public byte hit_point_max; // 0x78;

        [DataOffset(0x79, DataType.ByteArray, 100)]
        public byte[] spellBook = new byte[100]; //78[di]; // 1- 100 or 0x79 - 0xDC
        public bool KnowsSpell(Spells spell) { return spellBook[(int)spell - 1] != 0; }
        public void LearnSpell(Spells spell) { spellBook[(int)spell - 1] = 1; }

        [DataOffset(0xdd, DataType.Byte)]
        public byte attackLevel; // 0xdd; field_DD
        [DataOffset(0xde, DataType.Byte)]
        public byte field_DE; // 0xde;
        [DataOffset(0xdf, DataType.ByteArray, 5)]
        public byte[] saveVerse = new byte[5]; // 0xdf; field_DF 

        [DataOffset(0xe4, DataType.Byte)]
        public byte base_movement; // 0xe4;
        [DataOffset(0xe5, DataType.Byte)]
        public byte HitDice; // 0xe5; HitDice?
        [DataOffset(0xe6, DataType.Byte)]
        public byte multiclassLevel; // 0xe6;
        [DataOffset(0xe7, DataType.Byte)]
        public byte lost_lvls; // 0xe7;
        [DataOffset(0xe8, DataType.Byte)]
        public byte lost_hp; // 0xe8;
        [DataOffset(0xe9, DataType.Byte)]
        public byte field_E9; // 0xe9;
        [DataOffset(0xeA, DataType.ByteArray, 8)]
        public byte[] thief_skills = new byte[8]; // 0xeA; [] was 1 offset @ 0xe9, pick_pockets, open_locks, find_remove_traps, move_silently, hide_in_shadows, hear_noise, climb_walls, read_languages
        public List<Affect> affects; // f2 - affect_ptr

        [DataOffset(0xf6, DataType.Byte)]
        public byte field_F6; // 0xf6; field_F6
        [DataOffset(0xf7, DataType.Byte)]
        public byte control_morale; // 0xf7; field_F7
        [DataOffset(0xf8, DataType.Byte)]
        public byte npcTreasureShareCount; // 0xf8;
        [DataOffset(0xf9, DataType.Byte)]
        public byte field_F9; // 0xf9
        [DataOffset(0xfa, DataType.Byte)]
        public byte field_FA; // 0xfa;

        [DataOffset(0xfb, DataType.ShortArray, 7)]
        public MoneySet Money;

        [DataOffset(0x109, DataType.ByteArray, 8)]
        public byte[] ClassLevel = new byte[8]; /* Skill_A_lvl */

        public byte cleric_lvl // 0x109;
        {
            get { return ClassLevel[0]; }
            set { ClassLevel[0] = value; }
        }
        public byte druid_lvl // 0x10a;
        {
            get { return ClassLevel[1]; }
            set { ClassLevel[1] = value; }
        }
        public byte fighter_lvl // 0x10b;
        {
            get { return ClassLevel[2]; }
            set { ClassLevel[2] = value; }
        }
        public byte paladin_lvl // 0x10c;
        {
            get { return ClassLevel[3]; }
            set { ClassLevel[3] = value; }
        }
        public byte ranger_lvl // 0x10d;
        {
            get { return ClassLevel[4]; }
            set { ClassLevel[4] = value; }
        }
        public byte magic_user_lvl // 0x10e;
        {
            get { return ClassLevel[5]; }
            set { ClassLevel[5] = value; }
        }
        public byte thief_lvl // 0x10f;
        {
            get { return ClassLevel[6]; }
            set { ClassLevel[6] = value; }
        }
        public byte monk_lvl // 0x110;
        {
            get { return ClassLevel[7]; }
            set { ClassLevel[7] = value; }
        }

        [DataOffset(0x111, DataType.ByteArray, 8)]
        public byte[] ClassLevelsOld = new byte[8];


        public byte cleric_old_lvl // 0x111;
        {
            get { return ClassLevelsOld[0]; }
            set { ClassLevelsOld[0] = value; }
        }
        //public byte druid_old_lvl // 0x112;
        //{
        //    get { return ClassLevelsOld[1]; }
        //    set { ClassLevelsOld[1] = value; }
        //}
        public byte fighter_old_lvl // 0x113;
        {
            get { return ClassLevelsOld[2]; }
            set { ClassLevelsOld[2] = value; }
        }
        public byte paladin_old_lvl // 0x114;
        {
            get { return ClassLevelsOld[3]; }
            set { ClassLevelsOld[3] = value; }
        }
        public byte ranger_old_lvl // 0x115;
        {
            get { return ClassLevelsOld[4]; }
            set { ClassLevelsOld[4] = value; }
        }
        public byte magic_user_old_lvl // 0x116;
        {
            get { return ClassLevelsOld[5]; }
            set { ClassLevelsOld[5] = value; }
        }
        public byte thief_old_lvl // 0x117;
        {
            get { return ClassLevelsOld[6]; }
            set { ClassLevelsOld[6] = value; }
        }
        //public byte monk_old_level // 0x118;
        //{
        //    get { return ClassLevelsOld[7]; }
        //    set { ClassLevelsOld[7] = value; }
        //}

        public int SkillLevel(SkillType skill)
        {
            return ClassLevel[(int)skill] + (ClassLevelsOld[(int)skill] * DualClassExceedsPreviousLevel());
        }

        [DataOffset(0x119, DataType.Byte)]
        public byte sex; // 0x119;
        [DataOffset(0x11a, DataType.IByte)]
        public MonsterType monsterType; // 0x11a;
        [DataOffset(0x11b, DataType.Byte)]
        public byte alignment; // 0x11b;
        /// <summary>
        /// half-attacks count
        /// </summary>
        [DataOffset(0x11c, DataType.Byte)]
        public byte attacksCount; // 0x11c;
        [DataOffset(0x11d, DataType.Byte)]
        public byte baseHalfMoves; // 0x11d;
        [DataOffset(0x11e, DataType.Byte)]
        public byte attack1_DiceCountBase; // 0x11e;
        [DataOffset(0x11f, DataType.Byte)]
        public byte attack2_DiceCountBase; // 0x11f;
        [DataOffset(0x120, DataType.Byte)]
        public byte attack1_DiceSizeBase; // 0x120;
        [DataOffset(0x121, DataType.Byte)]
        public byte attack2_DiceSizeBase; // 0x121;
        [DataOffset(0x122, DataType.Byte)]
        public byte attack1_DamageBonusBase; // 0x122;
        [DataOffset(0x123, DataType.Byte)]
        public byte attack2_DamageBonusBase; // 0x123;
        [DataOffset(0x124, DataType.Byte)]
        public byte base_ac; // 0x124;
        [DataOffset(0x125, DataType.Byte)]
        public byte field_125; // 0x125;
        [DataOffset(0x126, DataType.Byte)]
        public byte mod_id; // 0x126; field_126
        [DataOffset(0x127, DataType.Int)]
        public int exp; // 0x127
        [DataOffset(0x12b, DataType.Byte)]
        public byte classFlags; // 0x12b;
        [DataOffset(0x12c, DataType.Byte)]
        public byte hit_point_rolled; // 0x12c;

        //[DataOffset(0x12d, DataType.ByteArray,15)]
        public byte[,] spellCastCount = new byte[3, 5]; // 0x12d - field_12D

        [DataOffset(0x13c, DataType.SWord)]
        public short field_13C; // 0x13c
        [DataOffset(0x13e, DataType.Byte)]
        public byte field_13E; // 0x13e;
        [DataOffset(0x13f, DataType.Byte)]
        public byte field_13F; // 0x13f;
        [DataOffset(0x140, DataType.Byte)]
        public byte field_140; // 0x140;
        [DataOffset(0x141, DataType.Byte)]
        public byte head_icon; // 0x141;
        [DataOffset(0x142, DataType.Byte)]
        public byte weapon_icon; // 0x142;
        [DataOffset(0x143, DataType.Byte)]
        public byte icon_id; // 0x143;
        [DataOffset(0x144, DataType.Byte)]
        public byte icon_size; // 0x144; field_144  1 small 2 normal
        [DataOffset(0x145, DataType.ByteArray, 6)]
        public byte[] icon_colours = new byte[6]; // 0x145 = field_144[1] // byte[6]
        [DataOffset(0x14b, DataType.Byte)]
        public byte field_14B; // 0x14b;

        //[DataOffset(0x14c, DataType.Byte)]
        //public byte field_14C; // 0x14c; // items.Count
        public const int MaxItems = 16;


        public List<Item> items; // 0x14d
        //public Item itemsPtr; // 0x14d

        public const int ItemSlots = 13;
        public Item[] itemArray = new Item[ItemSlots]; // 0x151[]
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

        public Item armor // field_159
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
        public Item Item_ptr_02
        {// 0x179
            get { return itemArray[10]; }
            set { itemArray[10] = value; }
        }
        public Item arrows
        {// 0x17d
            get { return itemArray[11]; }
            set { itemArray[11] = value; }
        }
        public Item quarrels
        { // 0x181
            get { return itemArray[12]; }
            set { itemArray[12] = value; }
        }

        [DataOffset(0x185, DataType.Byte)]
        public byte field_185; // 0x185;
        [DataOffset(0x186, DataType.SByte)]
        public sbyte field_186; // 0x186;
        [DataOffset(0x187, DataType.SWord)]
        public short weight; // 0x187

        public Action actions; // 0x18d
        [DataOffset(0x191, DataType.Byte)]
        public byte paladinCuresLeft; // 0x191 field_191
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
        [DataOffset(0x199, DataType.IByte)]
        public int hitBonus; // 0x199 field_199
        [DataOffset(0x19a, DataType.Byte)]
        public byte ac; // 0x19a
        public int DisplayAc { get { return 0x3C - ac; } }

        [DataOffset(0x19b, DataType.Byte)]
        public byte ac_behind; // 0x19b field_19B

        public byte AttacksLeft(int index)
        {
            switch (index)
            {
                case 1:
                    return attack1_AttacksLeft;
                case 2:
                    return attack2_AttacksLeft;
                default:
                    throw new System.NotImplementedException();
            }
        }

        public void AttacksLeftSet(int index, byte value)
        {
            switch (index)
            {
                case 1:
                    attack1_AttacksLeft = value;
                    break;
                case 2:
                    attack2_AttacksLeft = value;
                    break;
                default:
                    throw new System.NotImplementedException();
            }
        }

        public void AttacksLeftDec(int index)
        {
            switch (index)
            {
                case 1:
                    attack1_AttacksLeft -= 1;
                    break;
                case 2:
                    attack2_AttacksLeft -= 1;
                    break;
                default:
                    throw new System.NotImplementedException();
            }
        }

        [DataOffset(0x19c, DataType.Byte)]
        public byte attack1_AttacksLeft; // 0x19c - field_19C
        [DataOffset(0x19d, DataType.Byte)]
        public byte attack2_AttacksLeft; // 0x19d - field_19D

        public byte attackDiceCount(int index)
        {
            switch (index)
            {
                case 1:
                    return attack1_DiceCount;
                case 2:
                    return attack2_DiceCount;
                default:
                    throw new System.NotImplementedException();
            }
        }
        [DataOffset(0x19E, DataType.Byte)]
        public byte attack1_DiceCount; // 0x19e field_19E
        [DataOffset(0x19F, DataType.Byte)]
        public byte attack2_DiceCount; // 0x19f

        public byte attackDiceSize(int index)
        {
            switch (index)
            {
                case 1:
                    return attack1_DiceSize;
                case 2:
                    return attack2_DiceSize;
                default:
                    throw new System.NotImplementedException();
            }
        }
        [DataOffset(0x1A0, DataType.Byte)]
        public byte attack1_DiceSize; // 0x1a0 field_1A0
        [DataOffset(0x1A1, DataType.Byte)]
        public byte attack2_DiceSize; // 0x1a1

        public byte attackDamageBonus(int index)
        {
            switch (index)
            {
                case 1:
                    return (byte)attack1_DamageBonus;
                case 2:
                    return attack2_DamageBonus;
                default:
                    throw new System.NotImplementedException();
            }
        }
        [DataOffset(0x1a2, DataType.SByte)]
        public sbyte attack1_DamageBonus; // 0x1a2
        [DataOffset(0x1a3, DataType.Byte)]
        public byte attack2_DamageBonus; // 0x1a3

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

            spellList.Load(data, offset + 0x1e);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    spellCastCount[i, j] = data[0x12d + j + (i * i)];
                }
            }
        }

        private void Init()
        {
            spellCastCount = new byte[3, 5];
            stats = new StatValue[6];

            name = string.Empty;
            items = new List<Item>();
            affects = new List<Affect>();

            actions = null;
            Money = new MoneySet();
            spellList = new SpellList();
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

            spellList.Save(data, 0x1e);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    data[0x12d + j + (i * i)] = spellCastCount[i, j];
                }
            }

            return data;
        }

        public override string ToString()
        {
            return name;
        }


        public bool CanDuelClass()
        {
            if (race != Race.human)
            {
                return false;
            }

            for (ClassId index = ClassId.cleric; index <= ClassId.monk; index++)
            {
                if (ClassLevelsOld[(int)index] > 0)
                {
                    return false;
                }
            }

            return true;
        }

        int DualClassExceedsPreviousLevel() // sub_6B3D1
        {
            if (DuelClassCurrentLevel() > multiclassLevel)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        int DuelClassCurrentLevel()
        {
            if (race != Race.human)
            {
                return 0;
            }

            int loop_var = 0;

            while (loop_var < 7 &&
                ClassLevel[loop_var] == 0)
            {
                loop_var++;
            }

            return ClassLevel[loop_var];
        }

        public CombatTeam OppositeTeam()
        {
            return (combat_team == CombatTeam.Ours) ? CombatTeam.Enemy : CombatTeam.Ours;
        }

        public bool HasAffect(Affects type)
        {
            return affects.Exists(aff => aff.type == type);
        }

        public Affect GetAffect(Affects type)
        {
            return affects.Find(aff => aff.type == type);
        }

        static Affects[] held_affects = { Affects.snake_charm, Affects.paralyze, Affects.sleep, Affects.helpless };

        public bool IsHeld()
        {
            return Array.Exists(held_affects, affect => HasAffect(affect));
        }


        public void RemoveWeight(int amount)
        {
            weight -= (short)amount;
        }


        public void AddWeight(int amount)
        {
            weight += (short)amount;
        }

    }
}
