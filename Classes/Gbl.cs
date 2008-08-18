using System;
using System.Collections.Generic;


namespace Classes
{
    public enum QuickFight 
    {
        False = 0,
        True = 1
    }

    public enum Effect
    {
        Add = 0,
        Remove = 1
    }

    public enum SpellSource
    {
        Cast = 1,
        Memorize = 2,
        Scribe = 3,
        Learn = 4
    }

    public enum ImportSource
    {
        Curse = 0,
        Pool = 1,
        Hillsfar = 2
    }

    public delegate void spellDelegate(out bool arg_0, QuickFight quick_fight, byte arg_6);
    public delegate void spellDelegate2();
    public delegate void affectDelegate(Effect arg_0, object affect, Player player);


    public enum SoundType
    {
        PC,
        None
    }

    public class money
    {
        public const int copper = 0;
        public const int silver = 1;
        public const int electrum = 2;
        public const int gold = 3;
        public const int platum = 4;
        public const int gem = 5;
        public const int jewelry = 6;

        public static string[] names = { "Copper", "Silver", "Electrum", "Gold", "Platinum", "Gems", "Jewelry" };

        public static int[] per_copper = { 1, 10, 100, 200, 1000 };
    }

    public class SortedCombatant /*Struct_1D1C1*/
    {
        public SortedCombatant()
        {
            player_index = 0;
            steps = 0;
            direction = 0;
        }

        public SortedCombatant(SortedCombatant source)
        {
            player_index = source.player_index;
            steps = source.steps;
            direction = source.direction;
        }

        public int player_index; //field_0
        /// <summary>
        /// steps to counted in 2's, so that diagional steps can be 3 (thus 1.5)
        /// </summary>
        public int steps; // field_1
        public int direction; // field_2

        public override string ToString()
        {
            return "index: " + player_index + " dir: " + direction + " steps: " + steps;
        }
    }


    /// <summary>
    /// Summary description for Class1.
    /// </summary>
    public class gbl
    {
        public const ushort action_struct_size = 0x16;
        public const ushort char_struct_size = 0x1a6;

        public static byte byte_1642C;
        public static byte byte_1645A;

        public const sbyte byte_16E1C = 4;
        public const sbyte byte_16E1E = 3;
        public const sbyte byte_16E20 = 3;
        public const sbyte byte_16E22 = 3;
        public const byte byte_16E24 = 1;
        public const sbyte byte_16E26 = 1;
        public const sbyte byte_16E28 = 1;
        public const sbyte byte_16E2A = 0;
        public const byte byte_16E2C = 0;
        public const sbyte byte_16E2E = 4;

        public const int BackGroundTiles_count = 64; /* 64 is a guess */
        public static Struct_189B4[] BackGroundTiles = { /* unk_189B4 */
            new Struct_189B4( 1 , 0 , 0xFF, 0),
            new Struct_189B4( 0xFF , 1 , 2 , 0 ),
            new Struct_189B4( 0xFF , 1 , 2 , 1 ),
            new Struct_189B4( 0xFF , 1 , 2 , 2 ),
            new Struct_189B4(  0xFF,  1,  2, 3 ),
            new Struct_189B4(  1 , 1 ,  0 ,  4 ),
            new Struct_189B4(  0xFF , 1 ,  2 , 5 ),
            new Struct_189B4 ( 0xff, 1, 2, 6 ),
            new Struct_189B4 ( 0xff, 1, 2, 7 ),
            new Struct_189B4 ( 1, 1, 0, 8 ),
            new Struct_189B4 ( 0xff, 1, 2, 9 ),
            new Struct_189B4 ( 1, 1, 0, 0x0A ),
            new Struct_189B4 ( 0xff, 1, 2, 0x0B ),
            new Struct_189B4 ( 1, 1, 0, 0x0C ),
            new Struct_189B4 ( 0xff, 1, 2, 0x0D ),
            new Struct_189B4 ( 1, 1, 0, 0x0E ),
            new Struct_189B4 ( 0xff, 1, 2, 0x0F ),
            new Struct_189B4 ( 1, 1, 0, 0x10 ),
            new Struct_189B4 ( 0xff, 1, 2, 0x11 ),
            new Struct_189B4 ( 0xff, 1, 2, 0x12 ),
            new Struct_189B4 ( 0xff, 1, 2, 0x13 ),
            new Struct_189B4 ( 0xff, 1, 2, 0x14 ),
            new Struct_189B4 ( 0xff, 1, 2, 0x15 ),
            new Struct_189B4 ( 1, 1, 0, 0x16 ),
            new Struct_189B4 ( 1, 1, 0, 0x17 ),
            new Struct_189B4 ( 0xff, 1, 2, 0x18 ),
            new Struct_189B4 ( 2, 2, 0, 0x22 ),
            new Struct_189B4 ( 1, 1, 0, 0x23 ),
            new Struct_189B4 ( 1, 1, 0, 0x24 ),
            new Struct_189B4 ( 1, 1, 0, 0x25 ),
            new Struct_189B4 ( 1, 1, 0, 0x26 ),
            new Struct_189B4 ( 1, 1, 0, 0x27 ),
            new Struct_189B4 ( 0xff, 1, 2, 0 ),
            new Struct_189B4 ( 0xff, 1, 2, 1 ),
            new Struct_189B4 ( 0xff, 1, 2, 2 ),
            new Struct_189B4 ( 0xff, 1, 2, 3 ),
            new Struct_189B4 ( 0xff, 1, 2, 4 ),
            new Struct_189B4 ( 1, 1, 0, 5 ),
            new Struct_189B4 ( 1, 1, 0, 6 ),
            new Struct_189B4 ( 1, 1, 0, 7 ),
            new Struct_189B4 ( 1, 1, 0, 8 ),
            new Struct_189B4 ( 1, 1, 0, 9 ),
            new Struct_189B4 ( 0xff, 1, 2, 0x0A ),
            new Struct_189B4 ( 0xff, 1, 2, 0x0B ),
            new Struct_189B4 ( 1, 1, 0, 0x0C ),
            new Struct_189B4 ( 1, 1, 0, 0x0D ),
            new Struct_189B4 ( 1, 1, 0, 0x0E ),
            new Struct_189B4 ( 1, 1, 0, 0x0F ),
            new Struct_189B4 ( 2, 1, 0, 0x10 ),
            new Struct_189B4 ( 2, 1, 0, 0x11 ),
            new Struct_189B4 ( 2, 1, 0, 0x12 ),
            new Struct_189B4 ( 2, 1, 0, 0x13 ),
            new Struct_189B4 ( 2, 1, 0, 0x14 ),
            new Struct_189B4 ( 2, 1, 0, 0x15 ),
            new Struct_189B4 ( 1, 1, 0, 0x16 ),
            new Struct_189B4 ( 1, 1, 0, 0x17 ),
            new Struct_189B4 ( 1, 1, 0, 0x18 ),
            new Struct_189B4 ( 1, 1, 0, 0x19 ),
            new Struct_189B4 ( 2, 1, 0, 0x1A ),
            new Struct_189B4 ( 2, 1, 0, 0x1B ),
            new Struct_189B4 ( 4, 0, 0, 0x1C ),
            new Struct_189B4 ( 4, 0, 0, 0x1D ),
            new Struct_189B4 ( 4, 0, 0, 0x1E ),
            new Struct_189B4 ( 4, 0, 0, 0x1F ),
            new Struct_189B4 ( 1, 1, 0, 0x20 ),
            new Struct_189B4 ( 1, 1, 0, 0x21 ),
            new Struct_189B4 ( 0, 0, 0xff, 0xff ),
            new Struct_189B4 ( 0xff, 0xff, 0xff, 0xff ),
            new Struct_189B4 ( 0, 0, 0, 1 ),
            new Struct_189B4 ( 0xff, 0xff, 0xff, 0xff ),
            new Struct_189B4 ( 0, 0, 1, 0 ),
            new Struct_189B4 ( 0xff, 0xff, 0xff, 0xff ),
            new Struct_189B4 ( 0, 0, 1, 0 ),
            new Struct_189B4 ( 0, 1, 1, 1 )
        };


        public const byte byte_1A114 = 1;

        public readonly static byte[] max_class_levels =  { 10, 15, 10, 10, 11, 12, 11, 13 }; // byte_1A1CB seg600:3EBB
        public readonly static byte[] default_icon_colours = { 1, 2, 3, 4, 6, 7 }; // unk_1A1D3[0] == unk_1A1D2[1];

        public readonly static int[,] race_classes = { // unk_1A30A - seg600:3FFA
            { 0,    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, // Monster
            { 3,    2, 6, 14, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 7,    2, 5, 6, 13, 14, 15, 16, 0, 0, 0, 0, 0, 0},
            { 3,    2, 6, 14, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 13,   0, 2, 5, 6, 4, 8, 10, 9, 11, 13, 14, 15, 16},
            { 3,    2, 6, 14, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            { 6,    0, 2, 6, 8, 12, 14, 0, 0, 0, 0, 0, 0, 0},
            { 6,    0, 2, 5, 6, 3, 4, 0, 0, 0, 0, 0, 0, 0} };


        public static char byte_1AB06;
        public static bool stopVM = false; //byte_1AB08
        public static byte byte_1AB09;
        public static byte byte_1AB0A;
        public static byte byte_1AB0B;
        public static byte byte_1AB0C;
        public static byte byte_1AB0D;
        public static byte byte_1AB0E;
        public static byte byte_1AB14 = 0x73;
        public static bool byte_1AB16;
        public static byte byte_1AB18;
        public static byte byte_1AB19 = 0x40;
        public static byte byte_1AB1A; // not sure what's this is for.
        public static int[] team_start_x = { 0, 0 }; /* byte_1AD2C */
        public static int[] team_start_y = { 0, 0 }; /* byte_1AD2E */
        public static int[] half_team_count = { 0, 0 }; /* unk_1AD30 */
        public static int[] team_direction = { 0, 0 }; /* byte_1AD32 */
        public static sbyte byte_1AD34 = 0x1A;
        public static sbyte byte_1AD35;
        public static int dir_0_flags; // byte_1AD36
        public static int dir_6_flags; // byte_1AD37
        public static int dir_2_flags; // byte_1AD38
        public static int dir_4_flags; // byte_1AD39
        public static sbyte currentTeam; // field_197
        public static byte current_city;
        public static byte byte_1AD3D;
        public static byte byte_1AD3E;
        public static byte byte_1AD44;
        public static bool byte_1AD48;
        public static byte byte_1ADFA;
        public static byte byte_1AE0A;
        public static byte byte_1AE1B;
        public static bool[] affects_timed_out = new bool[0x48]; /* unk_1AE24 */
        public static byte byte_1AFDC;
        public static byte byte_1AFDD;
        public static byte byte_1AFDE;

        public static byte last_game_state; // byte_1B2E4
        public static byte byte_1B2E9 = 0;
        public static byte byte_1B2EB;
        public static byte head_block_id; // byte_1B2EE
        public static byte body_block_id; // byte_1B2EF
        public static bool party_killed; // byte_1B2F0
        public static byte byte_1B2F1;
        public static bool DelayBetweenCharacters; // byte_1B2F2

        public static byte byte_1BF12; // ??

        public static SoundType soundType = SoundType.None; // byte_1BF14
        public static SoundType soundTypeBackup; // byte_1BF15

        public static string save_path = "Save\\"; // byte_1BF1A

        public static string SavePath
        {
            get
            {
                return save_path;
            }
        }
        public static byte byte_1C01B;
        public static byte[] byte_1C8C2 = new byte[8];
        public static int textXCol; // byte_1C8CA
        public static int textYCol; // byte_1C8CB
        public static byte byte_1D1BB;
        public static int sortedCombatantCount; // byte_1D1C0
        //public static byte byte_1D1C4;
        public static Affects current_affect; // byte_1D2BD
        public static int damage; // byte_1D2BE
        public static byte damage_flags; // byte_1D2BF
        public static byte byte_1D2C0;
        public static byte spell_id; // byte_1D2C1
        public static int dice_count; // byte_1D2C2
        public static bool reset_byte_1D2C0; // byte_1D2C4
        public static byte byte_1D2C5;
        public static bool byte_1D2C6;
        public static bool byte_1D2C7;
        public static bool byte_1D2C8;
        public static int attack_roll; // byte_1D2C9
        public static byte byte_1D2CA;
        public static byte byte_1D2CB; // not used.
        public static byte byte_1D2CC;
        public static byte byte_1D2D1;
        public static int sky_colour; /* byte_1D534 */

        public static bool mapAreaDisplay; //byte_1D538, Show Area Map
        public static sbyte mapPosX; // byte_1D539, 0 map left, + map right
        public static sbyte mapPosY; // byte_1D53A, 0 map top, +map bottom
        public static byte mapDirection; // byte_1D53B , 0 N, 2 E, 4 S, 6 W
        public static byte mapWallType; // byte_1D53C
        public static byte mapWallRoof; // byte_1D53D

        public static void wordSetArray_1D53A(int index, short value)
        {
            switch (index)
            {
                case 1: word_1D53E = value; break;
                case 2: word_1D542 = value; break;
                case 3: word_1D546 = value; break;
            }
        }

        public static short wordGetArray_1D53A(int index)
        {
            switch (index)
            {
                case 1: return word_1D53E;
                case 2: return word_1D542;
                case 3: return word_1D546;
                default: throw new System.NotSupportedException();
            }
        }

        public static void wordSetArray_1D53C(int index, short value)
        {
            switch (index)
            {
                case 1: word_1D540 = value; break;
                case 2: word_1D544 = value; break;
                case 3: word_1D548 = value; break;
                default: throw new System.NotSupportedException();
            }
        }

        public static short wordGetArray_1D53C(int index)
        {
            switch (index)
            {
                case 1: return word_1D540;
                case 2: return word_1D544;
                case 3: return word_1D548;
                default: throw new System.NotSupportedException();
            }
        }

        public static short word_1D53E; // byte_1D53A[di] 1*4
        public static short word_1D540; // byte_1D53C[di] 1*4
        public static short word_1D542; // byte_1D53A[di] 2*4
        public static short word_1D544; // byte_1D53C[di] 2*4
        public static short word_1D546; // byte_1D53A[di] 3*4
        public static short word_1D548; // byte_1D53C[di] 3*4

        public static DaxArray byte_1D556;
        public static string lastDaxFile;
        public static string byte_1D5AB;
        public static byte lastDaxBlockId; // byte_1D5B4
        public static byte byte_1D5B5;
        public static byte bigpic_block_id; /* byte_1D5BA */
        public static byte byte_1D5BE;
        public static bool displayInput_specialKeyPressed; // byte_1D5BF displayInput
        public static int sp_target_count; // byte_1D75E
        public static int targetX; // byte_1D883
        public static int targetY; // byte_1D884
        public static bool spell_from_item; // byte_1D88D
        public static bool displayPlayerStatusLine18; /* byte_1D8A8 */
        public static bool can_draw_bigpic; // byte_1D8AA
        public static bool byte_1D8AC; // byte_1D8AC
        public static byte byte_1D8B0;
        //public static byte byte_1D8B6; // not used.
        public static byte byte_1D8B7;
        public static byte byte_1D8B8;
        public static int[] near_targets = new int[0x48]; // byte_1D8B9

        public static void inc_byte_byte_1D90x(int index)
        {
            switch (index)
            {
                case 1:
                    byte_1D901 += 1;
                    break;
                case 2:
                    byte_1D902 += 1;
                    break;
                default:
                    /* byte_1D90x += 1; */
                    throw new System.NotImplementedException();
            }
        }

        public static byte byte_1D901;
        public static byte byte_1D902;
        public static byte enemyHealthPercentage; /* byte_1D903 */
        public static bool magicOn; /* byte_1D904 */
        public static bool byte_1D905;
        public static bool byte_1D90E;
        public static bool display_hitpoints_ac; /* byte_1D90F */
        public static bool byte_1D910;
        public static byte sprite_block_id; /* byte_1D92B */
        public static byte pic_block_id; /* byte_1D92C */
        public static byte byte_1D92D;

        public static bool byte_1DA70;
        public static byte global_index; // byte_1DA71

        public static bool[] encounter_flags = new bool[2]; /* byte_1EE72 */
        public static bool byte_1EE7C;
        public static bool byte_1EE7D;
        public static bool byte_1EE7E;
        public static byte byte_1EE81;
        public static byte byte_1EE86;
        public static byte byte_1EE88;
        public static int search_flag_bkup; // byte_1EE89
        public static byte byte_1EE8A;
        public static byte byte_1EE8B;
        public static bool byte_1EE8C;
        public static bool byte_1EE8D;
        public static byte byte_1EE8E;
        public static bool displayPlayerSprite; /* byte_1EE8F */
        public static byte byte_1EE90;
        public static bool byte_1EE91;
        public static byte byte_1EE92;
        public static byte byte_1EE93;
        public static byte byte_1EE94;
        public static byte byte_1EE95;
        public static byte byte_1EE96;
        public static bool player_not_found; // byte_1EE97
        public static byte byte_1EE98;
        public static bool gameWon; // byte_1EE99
        public static byte byte_1EF9A;
        public static byte byte_1EF9B;

        public static byte byte_1EFBA;

        public const short word_16E08 = 5;
        public const short word_16E0A = 4;
        public const short word_16E0C = 6;
        public const short word_16E0E = 4;
        public const short word_16E10 = 2;
        public const short word_16E12 = 7;
        public const short word_16E14 = 2;
        public const short word_16E16 = 0;
        public const short word_16E18 = 9;
        public const short word_16E1A = 5;

        public const short sound_FF_188BC = 0x00ff;
        public const short sound_0_188BE = 0;
        public const short sound_1_188C0 = 1;
        public const short sound_2_188C2 = 2;
        public const short sound_3_188C4 = 3;
        public const short sound_4_188C6 = 4;
        public const short sound_5_188C8 = 5;
        public const short sound_6_188CA = 6;
        public const short sound_7_188CC = 7;
        public const short sound_8_188CE = 8;
        public const short sound_9_188D0 = 9;
        public const short sound_a_188D2 = 0xa;
        public const short sound_b_188D4 = 0xb;
        public const short sound_c_188D6 = 0xc;
        public const short sound_d_188D8 = 0xd;

        public readonly static short[] symbol_set_fix = { 0x0001, 0x002E, 0x0074, 0x00BA, 0x0100 };
        public const short word_1899C = 0x2D;

        public static RestTime word_1A13C = new RestTime( 10, 10, 6, 24, 30, 12, 0x100 );

        public static ushort word_1AE0F;
        public static ushort word_1AE11;
        public static ushort word_1AE13;
        public static short word_1AE15;
        public static ushort word_1AE17;
        public static short word_1AE19;
        public static short word_1AFE0;


        public static ushort vm_run_addr_1; // word_1B2D3
        public static ushort vm_run_addr_2; // word_1B2D5
        public static ushort vm_run_addr_3; // word_1B2D7
        public static ushort vm_run_addr_4; // word_1B2D9
        public static ushort ecl_initial_entryPoint; // word_1B2DB
        public static short rest_incounter_count;
        public static DaxBlock dword_1C8FC;
        public static DaxBlock bigpic_dax; /* word_1D5B6 */
        public static short word_1D5BC;
        public static int displayInputCentiSecondWait; // word_1D5C0 & word_1D5C2
         public static char displayInputTimeoutValue; // byte_1D5C4

         public static spellDelegate2[] spells_func_table = new spellDelegate2[101]; /* word_1D5CE */

        public static Player dword_1D87F;

        public static RestTime unk_1D890 = new RestTime();
        public static int rest_10_seconds; // word_1D8A6 seg600:7596
        public static short word_1D914;
        public static short word_1D916;
        public static short word_1D918;

        public static ushort word_1EE76;
        public static ushort word_1EE78;
        public static ushort word_1EE7A;

        public static short FIND_result; // word_1EFBC

        public static object dword_1AAC8;
        public static Item[] unk_1AF18 = new Item[0x30]; // array 01-0x30; seg600:4C08
        
        public const int max_spells = 0x54;
        public static byte[] memorize_spell_id = new byte[max_spells]; // unk_1AEC4 seg600:4BB4
        public static int[] memorize_count = new int[max_spells]; /* unk_1AE70 */
        
        public static Struct_1ADF6[] dword_1ADF6;
        public static StringList spell_string_list; // dword_1AE6C
        public static DaxBlock cursor_bkup; // dword_1C8F4
        public static DaxBlock dax24x24Set; //dword_1C8F8;
        public static Item dword_1D5C6;
        public static spellDelegate dword_1D5CA;
        public static DaxBlock missile_dax; /* */
        public static int exp_to_add;

        public static Stack<ushort> vmCallStack = new Stack<ushort>(); // dword_1D91A

        public static DaxBlock cursor; // dword_1EFA0

        public static Player player_ptr2;
        public static Player player_ptr;

        public static byte game_state; // 1- shop, 5 - combat
        public enum combatType
        {
            normal = 0,
            duel = 1
        }
        public static combatType combat_type;
        public static ushort ecl_offset;



        public static byte command;
        public static List<Player> player_next_ptr;
        //public static Player player_next_ptr;
        public static Item item_ptr;
        public static Player spell_target;
        public static Player[] sp_targets = new Player[256]; /* sp_target */ 
        // 744Bh[1] == sp_target[0]
        public static Player[] player_array = new Player[256];

        public const int MaxSortedCombatantCount = 72; /*unk_1D1C1_count*/
        public static SortedCombatant[] SortedCombatantList; // seg600:6EB1 - 6EAEh[1] == unk_1D1C1[0]

        public class Struct_1C9CD
        {
            public int xPos; // 0x00
            public int yPos; // 0x01
            public int player_index;  // field_2
            public byte size; // field_3
        }

        public const int MaxCombatantCount = 0xff; /* stru_1C9CD_count */
        public static int CombatantCount; // gbl.stru_1C9CD[0].field_3
        public static Struct_1C9CD[] CombatMap; // seg600:66BD stru_1C9CD

        public static Player player_ptr01;
        public static Player player_ptr02;

        public static byte game_area;
        public static byte game_area_backup;

        public static Area1 area_ptr;
        public static Area2 area2_ptr;

        public static Struct_1B2CA stru_1B2CA;
        public static EclBlock ecl_ptr;
        public static byte[,] dax_8x8d1_201;
        public static byte[][] stru_1D52C;
        public static Struct_1D530 stru_1D530 = new Struct_1D530();

        public static DaxBlock[,] combat_icons;

        public static DaxBlock[] symbol_8x8_set; // seg600:65D0 - seg600:65E3 DaxBlock[5]

        public static Item item_pointer;

        public static int[] pooled_money = new int[7];

        /// <summary>
        /// 0 ==, 1 !=, 2 &lt;, 3 &gt;, 4 &lt;=, 5 &gt;=
        /// </summary>
        public static bool[] compare_flags = new bool[6]; /*item_find*/

        public static int game_speed_var;

        public static bool inDemo;
        public static bool AnimationsOn;
        public static bool PicsOn;
        public static bool something01;

        public static byte current_head_id;
        public static DaxBlock headX_dax;
        public static byte current_body_id;
        public static DaxBlock bodyX_dax;

        public static bool can_bash_door;
        public static bool can_pick_door;
        public static bool can_knock_door;


        public static int saving_throw_roll;
        public static bool save_made;
        public static bool gameFlag01;
        public static bool printCommands = false;

        public static DaxBlock sky_dax_250;
        public static DaxBlock sky_dax_251;
        public static DaxBlock sky_dax_252;

        public static string[] unk_1D972 = new string[15] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };

        public const int cmdOppsLimit = 0x40;
        public static Opperation[] cmd_opps = new Opperation[cmdOppsLimit];

        public static SpellEntry[] spell_table = { /* seg600:37DC asc_19AEC */
            null, 
            new SpellEntry(0, 1, 6, 0, 6, 0, 10, 4, 0, 4, Affects.bless, 2, 10, 1, 0, 0), 
            new SpellEntry(0, 1, 6, 0, 6, 0, 10, 0, 0, 4, Affects.cursed, 1, 10, 3, 1, 0), 
            new SpellEntry(0, 1, 0, 0, 0, 0, 4, 2, 0, 4, 0, 2, 5, 1, 0, 0), 
            new SpellEntry(0, 1, -1, 0, 0, 0, 4, 0, 0, 4, 0, 1, 5, 2, 1, 0), 
            new SpellEntry(0, 1, 3, 0, 10, 0, 0, 1, 0, 4, Affects.detect_magic, 2, 1, 0, 0, 0), 
            new SpellEntry(0, 1, 0, 0, 0, 3, 4, 2, 0, 4, Affects.protection_from_evil, 2, 4, 1, 0, 0), 
            new SpellEntry(0, 1, 0, 0, 0, 3, 4, 2, 0, 4, Affects.protection_from_good, 2, 4, 1, 0, 0), 
            new SpellEntry(0, 1, 0, 0, 0, 10, 4, 2, 0, 4, Affects.resist_cold, 2, 10, 0, 0, 0), 
            new SpellEntry(2, 1, 0, 0, 0, 0, 4, 0, 0, 4, 0, 1, 1, 2, 1, 0), 
            new SpellEntry(2, 1, 12, 0, 0, 0, 4, 0, 1, 4, Affects.charm_person, 1, 1, 4, 1, 0), 
            new SpellEntry(2, 1, 0, 0, 0, 2, 0, 1, 0, 4, Affects.detect_magic, 2, 1, 0, 0, 0), 
            new SpellEntry(2, 1, 0, 2, 0, 10, 4, 2, 0, 4, Affects.enlarge, 2, 1, 0, 0, 0), 
            new SpellEntry(2, 1, 0, 2, 0, 10, 4, 2, 1, 4, Affects.reduce, 2, 1, 0, 1, 0), 
            new SpellEntry(2, 1, 0, 0, 0, 1, 0, 1, 0, 4, Affects.friends, 0, 1, 0, 0, 0), 
            new SpellEntry(2, 1, 6, 4, 0, 0, 4, 0, 0, 4, 0, 1, 1, 4, 1, 0), 
            new SpellEntry(2, 1, 0, 0, 0, 2, 4, 2, 0, 4, Affects.protection_from_evil, 2, 1, 1, 0, 0), 
            new SpellEntry(2, 1, 0, 0, 0, 2, 4, 2, 0, 4, Affects.protection_from_good, 2, 1, 1, 0, 0), 
            new SpellEntry(2, 1, 0, 0, 0, 2, 0, 1, 0, 4, Affects.read_magic, 0, 10, 0, 0, 0), 
            new SpellEntry(2, 1, 0, 0, 0, 5, 0, 1, 0, 4, Affects.shield, 2, 1, 2, 0, 0), 
            new SpellEntry(2, 1, -1, 0, 0, 0, 4, 0, 0, 4, 0, 1, 1, 2, 1, 0), 
            new SpellEntry(2, 1, 3, 4, 0, 5, 9, 0, 0, 4, Affects.sleep, 1, 1, 2, 1, 1), 
            new SpellEntry(0, 2, 0, 0, 0x1E, 0, 0, 1, 0, 4, Affects.find_traps, 0, 5, 0, 0, 0), 
            new SpellEntry(0, 2, 6, 0, 4, 1, 6, 0, 1, 4, Affects.paralyze, 1, 5, 6, 1, 0), 
            new SpellEntry(0, 2, 0, 0, 0, 10, 4, 2, 0, 4, Affects.resist_fire, 2, 5, 1, 0, 0), 
            new SpellEntry(0, 2, 12, 0, 0, 2, 0x1F, 0, 3, 4, Affects.silence_15_radius, 1, 5, 4, 1, 1), /* 396C - */            
            new SpellEntry(0, 2, 0, 0, 0, 0x3C, 4, 2, 0, 4, Affects.slow_poison, 2, 1, 0, 0, 0), 
            new SpellEntry(0, 2, 3, 0, 0, 0, 0xF0, 0, 0, 4, Affects.snake_charm, 1, 5, 0, 1, 0), 
            new SpellEntry(0, 2, 3, 0, 0, 1, 0, 1, 0, 4, Affects.spiritual_hammer, 2, 5, 1, 0, 0), 
            new SpellEntry(2, 2, 0, 4, 0, 5, 0, 1, 0, 4, Affects.detect_invisibility, 2, 2, 1, 0, 0), 
            new SpellEntry(2, 2, 0, 0, 0, 0, 4, 2, 0, 4, Affects.invisibility, 2, 2, 2, 0, 0), 
            new SpellEntry(2, 2, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 1, 0, 0, 0), 
            new SpellEntry(2, 2, 0, 0, 0, 2, 0, 1, 0, 4, Affects.mirror_image, 2, 2, 3, 0, 0), 
            new SpellEntry(2, 2, 1, 1, 0, 1, 4, 0, 1, 4, Affects.ray_of_enfeeblement, 1, 2, 2, 1, 0), 
            new SpellEntry(2, 2, 3, 0, 0, 1, 9, 0, 3, 0, Affects.stinking_cloud, 1, 2, 5, 1, 1), 
            new SpellEntry(2, 2, 0, 0, 0, 0x3C, 0, 2, 0, 4, Affects.strength, 0, 10, 0, 0, 0), 
            new SpellEntry(3, 7, 4, 0, 0, 0, 8, 4, 1, 4, 0, 2, 0, 2, 0, 0), 
            new SpellEntry(0, 3, 0, 0, 0, 0, 4, 2, 0, 4, 0, 2, 10, 0, 0, 0), 
            new SpellEntry(0, 3, -1, 0, 0, 0, 4, 0, 1, 4, Affects.blinded, 1, 10, 3, 1, 0), 
            new SpellEntry(0, 3, 0, 0, 0, 0, 0, 2, 0, 4, 0, 0, 100, 0, 0, 0), 
            new SpellEntry(0, 3, -1, 0, 0, 0, 4, 0, 1, 4, Affects.cause_disease_1, 1, 100, 4, 1, 0), 
            new SpellEntry(0, 3, 6, 0, 0, 0, 9, 2, 0, 4, 0, 2, 4, 3, 1, 1), 
            new SpellEntry(0, 3, 0, 0, 0, 1, 0, 4, 0, 4, Affects.prayer, 2, 6, 5, 0, 0), 
            new SpellEntry(0, 3, 0, 0, 0, 0, 4, 2, 0, 4, 0, 2, 6, 0, 0, 0), 
            new SpellEntry(0, 3, -1, 0, 0, 10, 4, 0, 1, 4, Affects.bestow_curse, 1, 6, 5, 1, 0), 
            new SpellEntry(2, 3, 0, 0, 0, 1, 0, 0, 0, 4, Affects.blink, 1, 1, 2, 0, 0), 
            new SpellEntry(2, 3, 12, 0, 0, 1, 9, 2, 0, 4, 0, 2, 3, 2, 1, 1), 
            new SpellEntry(2, 3, 10, 1, 0, 0, 11, 0, 2, 4, 0, 1, 3, 7, 1, 3), 
            new SpellEntry(2, 3, 6, 0, 3, 1, 10, 4, 0, 4, Affects.haste, 2, 3, 3, 0, 0), 
            new SpellEntry(2, 3, 12, 0, 0, 2, 7, 0, 1, 4, Affects.paralyze, 1, 3, 6, 1, 0), 
            new SpellEntry(2, 3, 0, 0, 0, 0, 9, 4, 0, 4, Affects.invisibility, 2, 3, 1, 0, 0), 
            new SpellEntry(2, 3, 4, 1, 0, 0, 8, 0, 2, 4, 0, 1, 3, 6, 1, 0), 
            new SpellEntry(2, 3, 0, 0, 0, 2, 4, 2, 0, 4, Affects.prot_from_evil_10_radius, 2, 3, 1, 0, 0), 
            new SpellEntry(2, 3, 0, 0, 0, 2, 4, 2, 0, 4, Affects.prot_from_good_10_radius, 2, 3, 2, 0, 0), 
            new SpellEntry(2, 3, 0, 0, 0, 10, 4, 2, 0, 4, Affects.prot_from_normal_missiles, 2, 3, 3, 0, 0), 
            new SpellEntry(2, 3, 9, 1, 3, 1, 10, 0, 0, 4, Affects.slow, 1, 3, 4, 1, 0), 
            new SpellEntry(0, 7, 0, 0, 0, 0, 4, 2, 0, 4, 0, 2, 6, 0, 0, 0), 
            new SpellEntry(3, 6, 0, 0, 0, 0, 0, 1, 0, 4, Affects.haste, 2, 0, 3, 0, 0), 
            new SpellEntry(0, 4, 0, 0, 0, 0, 4, 2, 0, 4, 0, 2, 7, 1, 0, 0), 
            new SpellEntry(3, 6, 0, 0, 0, 0, 0, 1, 0, 4, Affects.strength, 2, 0, 1, 0, 0), 
            new SpellEntry(3, 6, 4, 4, 0, 0, 4, 0, 2, 4, 0, 1, 0, 7, 1, 0), 
            new SpellEntry(3, 6, 6, 0, 0, 0, 4, 0, 1, 0, Affects.paralyze, 1, 0, 7, 1, 0), 
            new SpellEntry(3, 6, 0, 0, 0, 0, 0, 1, 0, 4, Affects.haste, 2, 0, 1, 0, 0), 
            new SpellEntry(3, 6, 0, 0, 0, 0, 7, 4, 0, 4, Affects.invisible, 2, 0, 2, 0, 0), 
            new SpellEntry(3, 6, 7, 0, 0, 0, 11, 0, 2, 4, 0, 1, 0, 7, 1, 3), 
            new SpellEntry(3, 6, 12, 0, 0, 0, 4, 0, 0, 4, 0, 1, 0, 6, 1, 0), 
            new SpellEntry(0, 4, 0, 0, 0, 0, 4, 0, 0, 4, 0, 1, 7, 5, 1, 0), 
            new SpellEntry(0, 4, 0, 0, 0, 0, 0, 2, 0, 4, 0, 0, 7, 0, 0, 0), 
            new SpellEntry(0, 4, 0, 0, 0, 0, 4, 0, 1, 0, 0, 1, 7, 6, 1, 0), 
            new SpellEntry(0, 4, 3, 0, 0, 10, 4, 2, 0, 4, Affects.prot_from_evil_10_radius, 2, 7, 2, 0, 0), 
            new SpellEntry(0, 4, 3, 0, 0, 2, 4, 0, 0, 4, Affects.sticks_to_snakes, 1, 7, 4, 1, 0), 
            new SpellEntry(0, 5, 0, 0, 0, 0, 4, 2, 0, 4, 0, 2, 8, 2, 0, 0), 
            new SpellEntry(0, 5, -1, 0, 0, 0, 4, 0, 1, 4, 0, 1, 8, 6, 1, 0), 
            new SpellEntry(0, 5, 0, 0, 0, 1, 0, 0, 0, 4, Affects.sp_dispel_evil, 1, 8, 3, 0, 0), 
            new SpellEntry(0, 5, 6, 0, 0, 0, 4, 0, 2, 4, 0, 1, 8, 6, 1, 0), 
            new SpellEntry(0, 5, 0, 0, 0, 0, 0, 2, 0, 4, 0, 0, 10, 1, 0, 0), 
            new SpellEntry(0, 5, 3, 0, 0, 0, 4, 0, 1, 4, 0, 1, 10, 7, 1, 0), 
            new SpellEntry(1, 1, 3, 0, 12, 0, 0, 1, 0, 4, Affects.detect_magic, 2, 3, 1, 0, 0), 
            new SpellEntry(1, 1, 8, 0, 10, 0, 11, 0, 1, 4, Affects.entangle, 1, 3, 3, 1, 0), 
            new SpellEntry(1, 1, 8, 0, 0, 4, 5, 0, 1, 4, Affects.faerie_fire, 1, 3, 4, 1, 0), 
            new SpellEntry(1, 1, -1, 0, 10, 1, 4, 2, 0, 4, Affects.invisible_to_animals, 2, 4, 1, 1, 0), 
            new SpellEntry(2, 4, 6, 0, 0, 0, 5, 0, 1, 4, Affects.charm_person, 1, 4, 6, 1, 0), 
            new SpellEntry(2, 4, 12, 0, 2, 1, 11, 0, 1, 4, Affects.confuse, 1, 4, 7, 1, 0), 
            new SpellEntry(2, 4, 0, 3, 0, 0, 8, 0, 0, 4, 0, 1, 1, 0, 1, 0), 
            new SpellEntry(2, 4, 6, 0, 0, 1, 8, 0, 1, 4, Affects.fear, 1, 4, 6, 1, 0), 
            new SpellEntry(2, 4, 0, 0, 2, 1, 0, 1, 0, 4, 0, 2, 4, 8, 0, 0), 
            new SpellEntry(2, 4, 0, 1, 0, 1, 4, 0, 1, 4, Affects.fumbling, 1, 4, 4, 1, 0), 
            new SpellEntry(2, 4, 0, 1, 0, 0, 10, 0, 0, 4, 0, 1, 4, 7, 1, 0), 
            new SpellEntry(2, 4, 0, 1, 0, 1, 0, 1, 0, 4, Affects.minor_globe_of_invulnerability, 2, 4, 5, 0, 0), 
            new SpellEntry(2, 4, 0, 0, 0, 0, 4, 2, 0, 4, 0, 2, 4, 0, 0, 0), 
            new SpellEntry(3, 5, 1, 0, 0, 0, 0xF0, 4, 0, 4, Affects.animate_dead, 2, 5, 0, 0, 0), 
            new SpellEntry(2, 5, 2, 0, 0, 1, 9, 0, 0, 4, 0, 2, 5, 5, 1, 0), 
            new SpellEntry(2, 5, 6, 0, 0, 0, 8, 0, 2, 4, 0, 1, 5, 6, 1, 0), 
            new SpellEntry(2, 5, 16, 0, 0, 0, 4, 0, 1, 4, Affects.feeblemind, 1, 5, 6, 1, 0), 
            new SpellEntry(2, 5, 0, 1, 0, 1, 7, 0, 1, 4, Affects.paralyze, 1, 5, 7, 1, 0), 
            new SpellEntry(3, 6, 0, 0, 0, 0, 0, 1, 0, 4, Affects.prot_drag_breath, 2, 10, 1, 0, 0), 
            new SpellEntry(3, 6, 0, 0, 0, 0, 0, 1, 0, 4, Affects.affect_6d, 2, 10, 1, 0, 0), 
            new SpellEntry(3, 6, 0, 0, 0, 0, 0, 1, 0, 4, Affects.invisibility, 2, 0, 1, 0, 0), 
            new SpellEntry(3, 6, 3, 0, 0, 0, 11, 0, 1, 4, 0, 1, 0, 1, 1, 0), 
            new SpellEntry(3, 6, 0, 0, 0, 0, 0, 1, 0, 4, 0, 2, 0, 1, 0, 0), 
            new SpellEntry(2, 4, 0, 0, 0, 10, 4, 0, 1, 4, 0, 1, 4, 4, 1, 0), 
            new SpellEntry(10, 0, 10, 0, 6, 0, 0x18, 0, 0x1E, 0, Affects.enlarge, 0, 0, 1, 0x28, 0x28) };


        public static byte[] unk_1D89D = new byte[9]; // seg600:758D

 
        public static bool party_fled;

        public static Struct_1C020[] unk_1C020;
        public static short unk_1C8BC;
        public static Struct_1D183[] unk_1D183; // array[8] but 1 offset.
        public static Struct_1D1BC mapToBackGroundTile; // stru_1D1BC


        public readonly static byte[] /*seg600:27D9*/ unk_18AE9 = { 0, 8, 2, 3, 4 };
        public readonly static byte[] /*seg600:27DA*/ unk_18AEA = { 8, 2, 3, 4, 8 };

        public readonly static byte[] /* seg600:27DD */ unk_18AED = { 4, 8, 0, 1, 2, 3, 4, 5, 6, 7 };

        public static Struct_1D885 stru_1D885;
        public static Struct_1D885 stru_1D889;

        public readonly static sbyte[] MapDirectionXDelta = /*unk_189A6 seg600:2696*/ {  0,  1, 1, 1, 0, -1, -1, -1, 0 };
        public readonly static sbyte[] MapDirectionYDelta = /*unk_189AF seg600:269F*/ { -1, -1, 0, 1, 1,  1,  0, -1, 0 };

        public static ImportSource import_from;

        public static byte friends_count;
        public static byte foe_count;

        public static affectDelegate[] affect_jump_list; /* spell_jump_list */


        public static ClassStatsMin[] class_stats_min = new ClassStatsMin[] { // unk_1A484
            new ClassStatsMin(6, 6, 9, 0, 0, 0), 
            new ClassStatsMin(0, 0, 0xC, 0, 0, 0xF), 
            new ClassStatsMin(9, 0, 6, 6, 7, 0), 
            new ClassStatsMin(0xC, 9, 0xD, 0, 9, 0x11),
            new ClassStatsMin(0xD, 0xD, 0xE, 0, 0xE, 0),
            new ClassStatsMin(0, 9, 6, 6, 0, 0),
            new ClassStatsMin(6, 6, 0, 9, 0, 0),
            new ClassStatsMin(0xF, 0, 0xF, 0xF, 0xB, 0),
            new ClassStatsMin(9, 0, 9, 0, 0, 0),
            new ClassStatsMin(9, 9, 9, 0, 0, 0), 
            new ClassStatsMin(0, 0xD, 0xE, 0, 0xE, 0),
            new ClassStatsMin(0, 9, 9, 0, 0, 0),
            new ClassStatsMin(0, 0, 9, 9, 0, 0), 
            new ClassStatsMin(9, 9, 0, 0, 0, 0), 
            new ClassStatsMin(9, 0, 0, 9, 0, 0), 
            new ClassStatsMin(9, 9, 0, 9, 0, 0), 
            new ClassStatsMin(0, 9, 0, 9, 0, 0) };

        public static string exe_path; // unk_1B21A
        public static string data_path; // unk_1B26A


        static Struct_1A35E stru_1A35E_0 = new Struct_1A35E(new SubStruct_1A35E[] {
            new SubStruct_1A35E(6, 2, 6), 
            new SubStruct_1A35E( 0x0c08, 0xe, 0),
            new SubStruct_1A35E(0, 0, 0), 
            new SubStruct_1A35E(0, 6, 0), 
            new SubStruct_1A35E(0x502, 6, 3), 
            new SubStruct_1A35E(4, 0, 0), 
            new SubStruct_1A35E(0, 0, 0) });

        static Struct_1A35E stru_1A35E_1 = new Struct_1A35E(new SubStruct_1A35E[] {
            new SubStruct_1A35E(0xfa, 2, 0x14), 
            new SubStruct_1A35E( 0, 0, 0),
            new SubStruct_1A35E(0x28, 5, 4), 
            new SubStruct_1A35E(0, 0, 0), 
            new SubStruct_1A35E(0, 0, 0), 
            new SubStruct_1A35E(0, 0, 0), 
            new SubStruct_1A35E(0x4B, 3, 6) });

        static Struct_1A35E stru_1A35E_2 = new Struct_1A35E(new SubStruct_1A35E[] {
            new SubStruct_1A35E(0x28a, 10, 10), 
            new SubStruct_1A35E( 0, 0, 0),
            new SubStruct_1A35E(0x82, 5, 6), 
            new SubStruct_1A35E(0, 0, 0), 
            new SubStruct_1A35E(0, 0, 0), 
            new SubStruct_1A35E(0x96, 5, 6), 
            new SubStruct_1A35E(0x64, 5, 6) });

        static Struct_1A35E stru_1A35E_3 = new Struct_1A35E(new SubStruct_1A35E[] {
            new SubStruct_1A35E(0x12C, 3, 0xC),
            new SubStruct_1A35E(0, 0, 0),
            new SubStruct_1A35E(0x3C, 5, 4),
            new SubStruct_1A35E(0, 0, 0),
            new SubStruct_1A35E(0, 0, 0), 
            new SubStruct_1A35E(0x64, 2, 0x0C),
            new SubStruct_1A35E(0x50, 5, 4)});

        static Struct_1A35E stru_1A35E_4 = new Struct_1A35E(new SubStruct_1A35E[] {
            new SubStruct_1A35E( 0x28, 2, 4),
            new SubStruct_1A35E( 0, 0, 0),
            new SubStruct_1A35E( 0x16, 3, 4),
            new SubStruct_1A35E( 0, 0, 0),
            new SubStruct_1A35E( 0, 0, 0),
            new SubStruct_1A35E( 0x1E, 2,  8),
            new SubStruct_1A35E( 0x16, 3, 8)});

        static Struct_1A35E stru_1A35E_5 = new Struct_1A35E(new SubStruct_1A35E[] {
            new SubStruct_1A35E(0, 0, 0),
            new SubStruct_1A35E( 0, 0, 0),
            new SubStruct_1A35E( 0x14, 3, 4),
            new SubStruct_1A35E( 0, 0, 0),
            new SubStruct_1A35E( 0, 0, 0),
            new SubStruct_1A35E( 0, 0, 0),
            new SubStruct_1A35E( 0x28, 2, 4)});

        static Struct_1A35E stru_1A35E_6 = new Struct_1A35E(new SubStruct_1A35E[] {
            new SubStruct_1A35E(0x14, 1, 4),
            new SubStruct_1A35E( 0, 0, 0),
            new SubStruct_1A35E( 0xD, 1, 4),
            new SubStruct_1A35E( 0, 0, 0),
            new SubStruct_1A35E( 0, 0, 0),
            new SubStruct_1A35E( 0, 0, 0),
            new SubStruct_1A35E( 0x14, 2, 4)});

        static Struct_1A35E stru_1A35E_7 = new Struct_1A35E(new SubStruct_1A35E[] {
            new SubStruct_1A35E( 0x12, 1, 4),
            new SubStruct_1A35E( 0x12, 1, 4),
            new SubStruct_1A35E( 0xF, 1, 4),
            new SubStruct_1A35E( 0x11, 1, 4),
            new SubStruct_1A35E( 0x14 , 1, 4),
            new SubStruct_1A35E( 0x18 , 2, 4),
            new SubStruct_1A35E( 0x12 , 1, 4)});

        public static Struct_1A35E[] unk_1A35E = new Struct_1A35E[] { stru_1A35E_0, stru_1A35E_1, stru_1A35E_2, stru_1A35E_3, stru_1A35E_4, stru_1A35E_5, stru_1A35E_6, stru_1A35E_7 };

        public static int[] playerScreenX = new int[256]; /* unk_1CAF0 */
        public static int[] playerScreenY = new int[256]; /* unk_1CB38 */

        public static byte[] unk_1AE0B = new byte[3];

        public static byte[] unk_16E30 = new byte[16]; // seg600:0B20
        public static byte[] unk_16E40 = new byte[16]; // seg600:0B30
        public static byte[] unk_16E50 = new byte[16]; // seg600:0B40

        public readonly static byte[,] class_alignments = { // unk_1A4EA
            { 9,0,1,2,3,4,5,6,7,8},
            { 5,1,3,4,5,7,0,0,0,0},
            { 9,0,1,2,3,4,5,6,7,8},
            { 1,0,0,0,0,0,0,0,0,0},
            { 3,0,3,6,0,0,0,0,0,0},
            { 9,0,1,2,3,4,5,6,7,8},
            { 7,1,2,3,4,5,7,8,0,0},
            { 9,0,1,2,3,4,5,6,7,8},
            { 9,0,1,2,3,4,5,6,7,8},
            { 9,0,1,2,3,4,5,6,7,8},
            { 3,0,3,6,0,0,0,0,0,0},
            { 9,0,1,2,3,4,5,6,7,8},
            { 9,0,1,2,3,4,5,6,7,8},
            { 9,0,1,2,3,4,5,6,7,8},
            { 7,1,2,3,4,5,7,8,0,0},
            { 7,1,2,3,4,5,7,8,0,0},
            { 7,1,2,3,4,5,7,8,0,0} };

    }
}
