using System;
using System.Collections.Generic;


namespace Classes
{
    public struct StatValue : IDataIO
    {
        int[, ,] raceSexMinMax;
        int[] classMin;
        int[] ageEffects;

        public StatValue(int[, ,] _raceSexMinMax, int[] _classMin, int[] _ageEffects)
        {
            raceSexMinMax = _raceSexMinMax;
            classMin = _classMin;
            ageEffects = _ageEffects;
            cur = full = 0;
        }

        public int cur;
        public int full;

        public void Load(int val)
        {
            full = cur = val;
        }

        public void Assign(StatValue sv)
        {
            full = sv.full;
            cur = sv.cur;
        }

        public void Inc()
        {
            full += 1;
            cur += 1;
        }

        public void Dec()
        {
            full -= 1;
            cur -= 1;
        }

        public void EnforceRaceSexLimits(int race, int sex)
        {
            if( raceSexMinMax != null )
            {
                full = Math.Min(raceSexMinMax[race, 1, sex], full);
                full = Math.Max(raceSexMinMax[race, 0, sex], full);
            }
            cur = full;
        }

        public void EnforceClassLimits(int _class)
        {
            if (classMin != null)
            {
                full = Math.Max(classMin[_class], full);
            }
            cur = full;
        }

        public void AgeEffects(int race, int age)
        {
            for (int i = 0; i < 5; i++)
            {
                if (Limits.RaceAgeBrackets[race, i] < age)
                {
                    full += ageEffects[i];
                }
            }
        }

        void IDataIO.Write(byte[] data, int offset)
        {
            data[offset + 0] = (byte)cur;
            data[offset + 1] = (byte)full;
        }

        void IDataIO.Read(byte[] data, int offset)
        {
            // enforce values in valid range
            cur = Math.Min((int)data[offset + 0], 25);
            full = Math.Min((int)data[offset + 1], 25);
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}", cur, full);
        }
    }

    public class PlayerStats : IDataIO
    {
        public StatValue Str = new StatValue(Limits.StrRaceSexMinMax, Limits.StrClassMin, Limits.StrAgeEffect);
        public StatValue Str00 = new StatValue(Limits.Str00RaceSexMinMax, Limits.Str00ClassMin, Limits.Str00AgeEffect);
        public StatValue Con = new StatValue(Limits.ConRaceSexMinMax, Limits.ConClassMin, Limits.ConAgeEffect);
        public StatValue Dex = new StatValue(Limits.DexRaceSexMinMax, Limits.DexClassMin, Limits.DexAgeEffect);
        public StatValue Int = new StatValue(Limits.IntRaceSexMinMax, Limits.IntClassMin, Limits.IntAgeEffect);
        public StatValue Wis = new StatValue(Limits.WisRaceSexMinMax, Limits.WisClassMin, Limits.WisAgeEffect);
        public StatValue Cha = new StatValue(Limits.ChaRaceSexMinMax, Limits.ChaClassMin, Limits.ChaAgeEffect);


        void IDataIO.Write(byte[] data, int offset)
        {
            ((IDataIO)Str).Write(data, 0x00);
            ((IDataIO)Int).Write(data, 0x02);
            ((IDataIO)Wis).Write(data, 0x04);
            ((IDataIO)Dex).Write(data, 0x06);
            ((IDataIO)Con).Write(data, 0x08);
            ((IDataIO)Cha).Write(data, 0x0a);
            ((IDataIO)Str00).Write(data, 0x0c);
        }

        void IDataIO.Read(byte[] data, int offset)
        {
            ((IDataIO)Str).Read(data, 0x00);
            ((IDataIO)Int).Read(data, 0x02);
            ((IDataIO)Wis).Read(data, 0x04);
            ((IDataIO)Dex).Read(data, 0x06);
            ((IDataIO)Con).Read(data, 0x08);
            ((IDataIO)Cha).Read(data, 0x0a);
            ((IDataIO)Str00).Read(data, 0x0c);
        }

        public void Assign(PlayerStats ps)
        {
            Str.Assign(ps.Str);
            Int.Assign(ps.Int);
            Wis.Assign(ps.Wis);
            Dex.Assign(ps.Dex);
            Con.Assign(ps.Con);
            Cha.Assign(ps.Cha);
            Str00.Assign(ps.Str00);
        }

        public StatValue this[int idx]
        {
            get
            {
                switch (idx)
                {
                    case 0: return Str;
                    case 1: return Int;
                    case 2: return Wis;
                    case 3: return Dex;
                    case 4: return Con;
                    case 5: return Cha;
                    default: throw new IndexOutOfRangeException(string.Format("idx {0} not in [0-5]", idx));
                }
            }
        }

        public override string ToString()
        {
            return string.Format("{0} ({6}),{1},{2},{3},{4},{5}", Str, Int, Wis, Con, Dex, Cha, Str00);
        }
    }

    public struct ClassLevels
    {

    }

    public class ActiveItems
    {
        const int ItemSlots = 13;
        Item[] itemArray = new Item[ItemSlots]; // 0x151[]

        public byte PrimaryWeaponHandCount()
        {
            if (primaryWeapon == null) return 0;

            return primaryWeapon.HandsCount();
        }

        public byte SecondaryWeaponHandCount()
        {
            if (secondaryWeapon == null) return 0;

            return secondaryWeapon.HandsCount();
        }     

        public Item primaryWeapon // field_151
        {// 0x151
            get { return itemArray[0]; }
            set { itemArray[0] = value; }
        }
        public Item secondaryWeapon // field_155
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

        public Item this[ItemSlot slot]
        {
            get
            {
                return itemArray[(int)slot];
            }
            set
            {
                itemArray[(int)slot] = value;
            }
        }

        public void Reset()
        {
            for (int slot = 0; slot < ItemSlots; slot++)
            {
                itemArray[slot] = null;
            }
        }

        public void UndreadyAll(int classFlags)
        {
            for (int item_slot = 0; item_slot < ItemSlots; item_slot++)
            {
                if (itemArray[item_slot] != null &&
                    (gbl.ItemDataTable[itemArray[item_slot].type].classFlags & classFlags) == 0 &&
                    itemArray[item_slot].cursed == false)
                {
                    itemArray[item_slot].readied = false;
                }
            }
        }
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

        //[DataOffset(0x10, DataType.Cust1Array, 6)]
        //public StatValue[] stats;

        [DataOffset(0x10, DataType.CustSaveLoad, 0x12)]
        public PlayerStats stats2;

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

        public ActiveItems activeItems = new ActiveItems();


        [DataOffset(0x185, DataType.Byte)]
        public byte weaponsHandsUsed; // 0x185;
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
            //stats = new StatValue[6];
            stats2 = new PlayerStats();

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
