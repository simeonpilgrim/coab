using System;
using System.Collections.Generic;
using Classes.Combat;


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

    public enum ImportSource
    {
        Curse = 0,
        Pool = 1,
        Hillsfar = 2
    }

    public enum GameState
    {
        StartGameMenu = 0,
        Shop = 1,
        Camping = 2,
        WildernessMap = 3,
        DungeonMap = 4,
        Combat = 5,
        AfterCombat = 6,
        EndGame = 7
    }

    public enum CombatType
    {
        normal = 0,
        duel = 1
    }

    public enum Sound
    {
        sound_FF = -1,
        sound_0 = 0,
        sound_1 = 1,
        sound_2 = 2,
        sound_3 = 3,
        sound_4 = 4,
        sound_5 = 5,
        sound_6 = 6,
        sound_7 = 7,
        sound_8 = 8,
        sound_9 = 9,
        sound_a = 0xa,
        sound_b = 0xb,
        sound_c = 0xc,
        sound_d = 0xd,
        sound_e = 0xe,
        sound_f = 0xf
    }

    public delegate void spellDelegate(out bool arg_0, QuickFight quick_fight, byte arg_6);
    public delegate void spellDelegate2();
    public delegate void affectDelegate(Effect arg_0, object affect, Player player);

    [Flags]
    public enum DamageType
    {
        Fire = 0x01,
        Cold = 0x02,
        Electricity = 0x04,
        Magic = 0x08,
        Acid = 0x10,
        DragonBreath = 0x20,
        Unknown40 = 0x40
    }

    public enum DamageOnSave
    {
        Normal = 0,
        Zero = 1,
        Half = 2,
        Unknown_3 = 3,
        Unknown_1E = 0x1e
    }

    public enum SoundType
    {
        PC,
        None
    }

    public struct Point
    {
        public int x;
        public int y;

        public const int MapMaxX = 50;
        public const int MapMaxY = 25;
        public const int MapMinX = 0;
        public const int MapMinY = 0;

        public const int ScreenMaxX = 6;
        public const int ScreenMaxY = 6;
        public const int ScreenHalfX = ScreenMaxX / 2;
        public const int ScreenHalfY = ScreenMaxY / 2;
        public static readonly Point ScreenCenter = new Point(ScreenHalfX, ScreenHalfY);

        public Point(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public Point(Point old)
        {
            x = old.x;
            y = old.y;
        }

        public static Point operator +(Point a, Point b)
        {
            return new Point(a.x + b.x, a.y + b.y);
        }

        public static Point operator -(Point a, Point b)
        {
            return new Point(a.x - b.x, a.y - b.y);
        }

        public static Point operator *(Point a, int b)
        {
            return new Point(a.x * b, a.y * b);
        }

        public static Point operator /(Point a, int b)
        {
            return new Point(a.x / b, a.y / b);
        }

        public static bool operator ==(Point a, Point b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(Point a, Point b)
        {
            return a.x != b.x || a.y != b.y;
        }

        public void MapBoundaryTrunc()
        {
            x = Math.Max(Math.Min(x, MapMaxX - 1), MapMinX);
            y = Math.Max(Math.Min(y, MapMaxY - 1), MapMinY);
        }

        public bool MapInBounds()
        {
            return x < MapMaxX && x >= MapMinX && y < MapMaxY && y >= MapMinY;
        }

        public override string ToString()
        {
            return string.Format("x: {0} y: {1}", x, y);
        }
    }



    public class Money
    {
        public const int copper = 0;
        public const int silver = 1;
        public const int electrum = 2;
        public const int gold = 3;
        public const int platum = 4;
        public const int gem = 5;
        public const int jewelry = 6;

        public const int maxNameLen = 8;
        public static string[] names = { "Copper", "Silver", "Electrum", "Gold", "Platinum", "Gems", "Jewelry" };

        public static int[] per_copper = { 1, 10, 100, 200, 1000 };
    }

 

    public class gbl
    {
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
            new Struct_189B4 ( 0xff, 1, 2, 10 ),
            new Struct_189B4 ( 0xff, 1, 2, 11 ),
            new Struct_189B4 ( 1, 1, 0, 12 ),
            new Struct_189B4 ( 1, 1, 0, 13 ),
            new Struct_189B4 ( 1, 1, 0, 14 ),
            new Struct_189B4 ( 1, 1, 0, 15 ),
            new Struct_189B4 ( 2, 1, 0, 16 ),
            new Struct_189B4 ( 2, 1, 0, 17 ),
            new Struct_189B4 ( 2, 1, 0, 18 ),
            new Struct_189B4 ( 2, 1, 0, 19 ),
            new Struct_189B4 ( 2, 1, 0, 20 ),
            new Struct_189B4 ( 2, 1, 0, 21 ),
            new Struct_189B4 ( 1, 1, 0, 22 ),
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




        public readonly static byte[] max_class_hit_dice =  { 10, 15, 10, 10, 11, 12, 11, 13 }; // byte_1A1CB seg600:3EBB
        public readonly static byte[] default_icon_colours = { 1, 2, 3, 4, 6, 7 }; // unk_1A1D3[0] == unk_1A1D2[1];

        public readonly static ClassId[][] RaceClasses = { 
                new ClassId[] /*MonsterType*/{ },
                new ClassId[] /*Dwarf*/{ ClassId.fighter, ClassId.thief, ClassId.mc_f_t},
                new ClassId[] /*Elf*/{ ClassId.fighter, ClassId.magic_user, ClassId.thief, ClassId.mc_f_mu, ClassId.mc_f_t, ClassId.mc_f_mu_t, ClassId.mc_mu_t},
                new ClassId[] /*Gnome*/{ ClassId.fighter, ClassId.thief, ClassId.mc_f_t},
                new ClassId[] /*Half-Elf*/{ ClassId.cleric, ClassId.fighter, ClassId.magic_user, ClassId.thief, ClassId.ranger,ClassId.mc_c_f, ClassId.mc_c_r, ClassId.mc_c_f_m, ClassId.mc_c_mu, ClassId.mc_f_mu, ClassId.mc_f_t, ClassId.mc_f_mu_t, ClassId.mc_mu_t},
                new ClassId[] /*Halfling*/{ ClassId.fighter, ClassId.thief, ClassId.mc_f_t},
                new ClassId[] /*Half-Orc*/{ ClassId.cleric, ClassId.fighter, ClassId.thief, ClassId.mc_c_f, ClassId.mc_c_t,ClassId.mc_f_t},
                new ClassId[] /*Human*/{ ClassId.cleric, ClassId.fighter, ClassId.magic_user, ClassId.thief, ClassId.paladin, ClassId.ranger},
                new ClassId[] /*Cheaters*/{ ClassId.cleric, ClassId.fighter, ClassId.magic_user, ClassId.thief, ClassId.ranger,ClassId.mc_c_f, ClassId.mc_c_r, ClassId.mc_c_f_m, ClassId.mc_c_mu, ClassId.mc_f_mu, ClassId.mc_f_t, ClassId.mc_f_mu_t, ClassId.mc_mu_t}};


        public static bool stopVM = false; //byte_1AB08
        public static bool vmFlag01; // byte_1AB09 
        public static bool restore_player_ptr; // byte_1AB0A
        public static bool byte_1AB0B;
        public static bool byte_1AB0C;
        public static bool filesLoaded; // byte_1AB0D
        public static byte numLoadedMonsters; // byte_1AB0E
        public static bool byte_1AB14;
        public static bool shopRedrawMenuItems; // byte_1AB16
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
        public static byte byte_1ADFA;
        public static byte byte_1AE0A;
        public static byte byte_1AE1B;
        public static bool[] affects_timed_out = new bool[0x48]; /* unk_1AE24 */

        public static bool reload_ecl_and_pictures; // byte_1B2EB
        public static byte head_block_id; // byte_1B2EE
        public static byte body_block_id; // byte_1B2EF
        public static bool party_killed; // byte_1B2F0
        
        public static bool can_train_no_more; // byte_1D8B0
        public static bool silent_training; // byte_1B2F1
        public static bool DelayBetweenCharacters; // byte_1B2F2

        public static byte byte_1BF12; // TODO remove or workout what it's was for?

        public static SoundType soundType = SoundType.None; // byte_1BF14

        public static string save_path = "Save\\"; // byte_1BF1A

        public static string SavePath
        {
            get
            {
                return save_path;
            }
        }
        public static bool gameSaved; // byte_1C01B
        public static byte[] monoCharData = new byte[8]; // byte_1C8C2
        public static int textXCol; // byte_1C8CA
        public static int textYCol; // byte_1C8CB
        public static int sortedCombatantCount; // byte_1D1C0
        public static Affects current_affect; // byte_1D2BD
        public static int damage; // byte_1D2BE
        public static DamageType damage_flags; // byte_1D2BF
        public static int halfActionsLeft; // byte_1D2C0
        public static bool resetMovesLeft; // byte_1D2C4, reset_byte_1D2C0
        public static byte spell_id; // byte_1D2C1
        public static int dice_count; // byte_1D2C2
        public static bool targetInvisible; // byte_1D2C5
        public static bool cureSpell; // byte_1D2C6
        public static bool byte_1D2C7;
        public static bool byte_1D2C8;
        public static int attack_roll; // byte_1D2C9
        public static byte byte_1D2CA;
        public static byte byte_1D2CB; // not used.
        public static int monster_morale; // byte_1D2CC
        public static int sky_colour; // byte_1D534

        public static bool mapAreaDisplay; //byte_1D538, Show Area Map
        public static int mapPosX; // byte_1D539, 0 map left, + map right
        public static int mapPosY; // byte_1D53A, 0 map top, +map bottom
        public static byte mapDirection; // byte_1D53B , 0 N, 2 E, 4 S, 6 W
        public static byte mapWallType; // byte_1D53C
        public static byte mapWallRoof; // byte_1D53D

        public class SetBlock
        {
            public SetBlock() { setId = -1; blockId = -1; }
            public SetBlock(int _setId, int _blockId) { setId = _setId; blockId = _blockId; }
            public int blockId; // byte_1D53A[di] 1*4
            public int setId; // byte_1D53C[di] 1*4
        }

        public static SetBlock[] setBlocks = new SetBlock[3]; 

        public static DaxArray byte_1D556;
        public static string lastDaxFile;
        public static string byte_1D5AB;
        public static byte lastDaxBlockId; // byte_1D5B4
        public static byte byte_1D5B5;
        public static byte bigpic_block_id; /* byte_1D5BA */
        public static int menuSelectedWord; // byte_1D5BE
        public static bool displayInput_specialKeyPressed; // byte_1D5BF displayInput
        public static Point targetPos; // byte_1D883 & byte_1D884
        public static bool spell_from_item; // byte_1D88D
        public static bool displayPlayerStatusLine18; /* byte_1D8A8 */
        public static bool can_draw_bigpic; // byte_1D8AA
        public static bool applyItemAffect; // byte_1D8AC
        public static int combat_round; // byte_1D8B7
        public static int combat_round_no_action_limit; // byte_1D8B8
        public const int combat_round_no_action_value = 15;

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
        public static int enemyHealthPercentage; /* byte_1D903 */
        public static bool magicOn; /* byte_1D904 */
        public static bool byte_1D90E;
        public static bool display_hitpoints_ac; /* byte_1D90F */
        public static bool focusCombatAreaOnPlayer; // byte_1D910
        public static byte sprite_block_id; /* byte_1D92B */
        public static byte pic_block_id; /* byte_1D92C */
        public static byte monster_icon_id; // byte_1D92D

        public static bool byte_1DA70;

        public static bool[] encounter_flags = new bool[2]; /* byte_1EE72 */
        public static bool redrawPartySummary1; // byte_1EE7C
        public static bool redrawPartySummary2; // byte_1EE7D
        public static bool redrawBoarder; // byte_1EE7E
        public static int partyAnimatedCount; // byte_1EE81
        public static bool battleWon; // byte_1EE86
        public static byte byte_1EE88;
        public static int search_flag_bkup; // byte_1EE89
        public static bool byte_1EE8C;
        public static bool byte_1EE8D;
        public static bool displayPlayerSprite; /* byte_1EE8F */
        public static bool bottomTextHasBeenCleared; // byte_1EE90
        public static bool byte_1EE91;
        public static bool positionChanged; // byte_1EE92 
        public static bool monstersLoaded; // byte_1EE93 
        public static bool byte_1EE94;
        public static bool byte_1EE95;
        public static byte byte_1EE96;
        public static bool player_not_found; // byte_1EE97
        public static bool byte_1EE98;
        public static bool gameWon; // byte_1EE99





        public readonly static short[] symbol_set_fix = { 0x0001, 0x002E, 0x0074, 0x00BA, 0x0100 };
        public const short word_1899C = 0x2D;

        public static ushort word_1AE0F;
        public static ushort word_1AE11;
        public static ushort word_1AE13;
        public static short word_1AE15;
        public static ushort word_1AE17;
        public static short word_1AE19;


        public static ushort vm_run_addr_1; // word_1B2D3
        public static ushort vm_run_addr_2; // word_1B2D5
        public static ushort vm_run_addr_3; // word_1B2D7
        public static ushort CampInterruptedAddr; // word_1B2D9 vm_run_addr_4
        public static ushort ecl_initial_entryPoint; // word_1B2DB
        public static short rest_incounter_count;
        public static DaxBlock dword_1C8FC; //TODO - overlay dax block, not currently used.
        public static DaxBlock bigpic_dax; /* word_1D5B6 */
        public static int menuScreenIndex;
        public static int displayInputCentiSecondWait; // word_1D5C0 & word_1D5C2
        public static char displayInputTimeoutValue; // byte_1D5C4

        public static Dictionary<Spells, spellDelegate2> spellTable;

        public static Player lastSelectetSpellTarget; // dword_1D87F

        public static RestTime timeToRest = new RestTime(); // unk_1D890
        public static int rest_10_seconds; // word_1D8A6 seg600:7596
        public static short word_1D914;
        public static short word_1D916;
        public static short word_1D918;

        public static ushort word_1EE76;
        public static ushort word_1EE78;
        public static ushort word_1EE7A;

        public static short FIND_result; // word_1EFBC

        public static Item[] scribeScrolls = new Item[0x30]; // array 01-0x30; seg600:4C08 unk_1AF18
        public static byte scribeScrollsCount; // byte_1AFDC

        public const int max_spells = 0x54;
        public static byte[] memorize_spell_id = new byte[max_spells]; // unk_1AEC4 seg600:4BB4
        public static int[] memorize_count = new int[max_spells]; /* unk_1AE70 */
        
        public static Struct_1ADF6[] dword_1ADF6;
        public static List<MenuItem> spell_string_list = new List<MenuItem>(); // dword_1AE6C
        public static DaxBlock cursor_bkup; // dword_1C8F4
        public static DaxBlock dax24x24Set; //dword_1C8F8;
        public static Item currentScroll; // dword_1D5C6
        public static spellDelegate dword_1D5CA;
        public static DaxBlock missile_dax; /* */
        public static int exp_to_add;

        public static Stack<ushort> vmCallStack = new Stack<ushort>(); // dword_1D91A

        public static DaxBlock cursor; // dword_1EFA0

        public static Player player_ptr2;
        public static Player player_ptr;

        public static GameState game_state; // 1- shop, 5 - combat
        public static GameState last_game_state; // byte_1B2E4


        public static CombatType combat_type;
        public static ushort ecl_offset;



        public static byte command;
        public static List<Player> TeamList = new List<Player>(); // player_next_ptr
    
        public static Item item_ptr; // rename current item
        public static Player spell_target;
        public static List<Player> spellTargets = new List<Player>(); /* sp_target */ 
        // 744Bh[1] == sp_target[0]
        public static Player[] player_array = new Player[256];

        public const int MaxCombatantCount = 0xff; /* stru_1C9CD_count */
        public static int CombatantCount; // gbl.stru_1C9CD[0].field_3
        public static CombatantMap[] CombatMap; // seg600:66BD stru_1C9CD

        public static Player tradeWith; //player_ptr01

        public static byte game_area;
        public static byte game_area_backup;

        public static Area1 area_ptr;
        public static Area2 area2_ptr;

        public static Struct_1B2CA stru_1B2CA;
        public static EclBlock ecl_ptr;
        public static byte[,] dax_8x8d1_201;
        public static byte[][] stru_1D52C;
        public static Struct_1D530 stru_1D530 = new Struct_1D530();

        public static CombatIcon[] combat_icons;

        public static DaxBlock[] symbol_8x8_set; // seg600:65D0 - seg600:65E3 DaxBlock[5]

        public static List<Item> items_pointer;

        public static int[] pooled_money = new int[7];

        /// <summary>
        /// 0 ==, 1 !=, 2 &lt;, 3 &gt;, 4 &lt;=, 5 &gt;=
        /// </summary>
        public static bool[] compare_flags = new bool[6]; /*item_find*/

        public static int game_speed_var;

        public static bool inDemo;
        public static bool AnimationsOn;
        public static bool PicsOn;

        public static byte current_head_id;
        public static DaxBlock headX_dax;
        public static byte current_body_id;
        public static DaxBlock bodyX_dax;

        public static bool can_bash_door;
        public static bool can_pick_door;
        public static bool can_knock_door;


        public static int savingThrowRoll;
        public static bool savingThrowMade;
        public static SaveVerseType saveVerseType; //byte_1D2D1

        public static bool printCommands = false;

        public static DaxBlock sky_dax_250;
        public static DaxBlock sky_dax_251;
        public static DaxBlock sky_dax_252;

        public static string[] unk_1D972 = new string[15] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };

        public const int cmdOppsLimit = 0x40;
        public static Opperation[] cmd_opps = new Opperation[cmdOppsLimit];

        public static SpellEntry[] spell_table = { /* seg600:37DC asc_19AEC */
            null, 
            new SpellEntry(SpellClass.Cleric,    1, 6, 0, 6, 0, 10, 4, DamageOnSave.Normal, SaveVerseType.type4, Affects.bless, SpellWhen.Both, 10, 1, 0, 0), 
            new SpellEntry(SpellClass.Cleric,    1, 6, 0, 6, 0, 10, 0, DamageOnSave.Normal,  SaveVerseType.type4, Affects.cursed, SpellWhen.Combat, 10, 3, 1, 0), 
            new SpellEntry(SpellClass.Cleric,    1, 0, 0, 0, 0, 4, 2, DamageOnSave.Normal,   SaveVerseType.type4, Affects.none, SpellWhen.Both, 5, 1, 0, 0), 
            new SpellEntry(SpellClass.Cleric,    1, -1, 0, 0, 0, 4, 0, DamageOnSave.Normal,  SaveVerseType.type4, Affects.none, SpellWhen.Combat, 5, 2, 1, 0), 
            new SpellEntry(SpellClass.Cleric,    1, 3, 0, 10, 0, 0, 1, DamageOnSave.Normal,  SaveVerseType.type4, Affects.detect_magic, SpellWhen.Both, 1, 0, 0, 0), 
            new SpellEntry(SpellClass.Cleric,    1, 0, 0, 0, 3, 4, 2, DamageOnSave.Normal,   SaveVerseType.type4, Affects.protection_from_evil, SpellWhen.Both, 4, 1, 0, 0), 
            new SpellEntry(SpellClass.Cleric,    1, 0, 0, 0, 3, 4, 2, DamageOnSave.Normal,   SaveVerseType.type4, Affects.protection_from_good, SpellWhen.Both, 4, 1, 0, 0), 
            new SpellEntry(SpellClass.Cleric,    1, 0, 0, 0, 10, 4, 2, DamageOnSave.Normal,  SaveVerseType.type4, Affects.resist_cold, SpellWhen.Both, 10, 0, 0, 0), 
            new SpellEntry(SpellClass.MagicUser, 1, 0, 0, 0, 0, 4, 0, DamageOnSave.Normal, SaveVerseType.type4, Affects.none, SpellWhen.Combat, 1, 2, 1, 0), 
            new SpellEntry(SpellClass.MagicUser, 1, 12, 0, 0, 0, 4, 0, DamageOnSave.Zero, SaveVerseType.type4, Affects.charm_person, SpellWhen.Combat, 1, 4, 1, 0), 
            new SpellEntry(SpellClass.MagicUser, 1, 0, 0, 0, 2, 0, 1, DamageOnSave.Normal, SaveVerseType.type4, Affects.detect_magic, SpellWhen.Both, 1, 0, 0, 0), 
            new SpellEntry(SpellClass.MagicUser, 1, 0, 2, 0, 10, 4, 2, DamageOnSave.Normal, SaveVerseType.type4, Affects.enlarge, SpellWhen.Both, 1, 0, 0, 0), 
            new SpellEntry(SpellClass.MagicUser, 1, 0, 2, 0, 10, 4, 2, DamageOnSave.Zero, SaveVerseType.type4, Affects.reduce, SpellWhen.Both, 1, 0, 1, 0), 
            new SpellEntry(SpellClass.MagicUser, 1, 0, 0, 0, 1, 0, 1, DamageOnSave.Normal, SaveVerseType.type4, Affects.friends, SpellWhen.Camp, 1, 0, 0, 0), 
            new SpellEntry(SpellClass.MagicUser, 1, 6, 4, 0, 0, 4, 0, DamageOnSave.Normal, SaveVerseType.type4, Affects.none, SpellWhen.Combat, 1, 4, 1, 0), 
            new SpellEntry(SpellClass.MagicUser, 1, 0, 0, 0, 2, 4, 2, DamageOnSave.Normal, SaveVerseType.type4, Affects.protection_from_evil, SpellWhen.Both, 1, 1, 0, 0), 
            new SpellEntry(SpellClass.MagicUser, 1, 0, 0, 0, 2, 4, 2, DamageOnSave.Normal, SaveVerseType.type4, Affects.protection_from_good, SpellWhen.Both, 1, 1, 0, 0), 
            new SpellEntry(SpellClass.MagicUser, 1, 0, 0, 0, 2, 0, 1, DamageOnSave.Normal, SaveVerseType.type4, Affects.read_magic, SpellWhen.Camp, 10, 0, 0, 0), 
            new SpellEntry(SpellClass.MagicUser, 1, 0, 0, 0, 5, 0, 1, DamageOnSave.Normal, SaveVerseType.type4, Affects.shield, SpellWhen.Both, 1, 2, 0, 0), 
            new SpellEntry(SpellClass.MagicUser, 1, -1, 0, 0, 0, 4, 0, DamageOnSave.Normal, SaveVerseType.type4, Affects.none, SpellWhen.Combat, 1, 2, 1, 0), 
            new SpellEntry(SpellClass.MagicUser, 1, 3, 4, 0, 5, 9, 0, DamageOnSave.Normal, SaveVerseType.type4, Affects.sleep, SpellWhen.Combat, 1, 2, 1, 1), 
            new SpellEntry(SpellClass.Cleric,    2, 0, 0, 30, 0, 0, 1, DamageOnSave.Normal, SaveVerseType.type4, Affects.find_traps, SpellWhen.Camp, 5, 0, 0, 0), 
            new SpellEntry(SpellClass.Cleric,    2, 6, 0, 4, 1, 6, 0, DamageOnSave.Zero, SaveVerseType.type4, Affects.paralyze, SpellWhen.Combat, 5, 6, 1, 0), 
            new SpellEntry(SpellClass.Cleric,    2, 0, 0, 0, 10, 4, 2, DamageOnSave.Normal, SaveVerseType.type4, Affects.resist_fire, SpellWhen.Both, 5, 1, 0, 0), 
            new SpellEntry(SpellClass.Cleric,    2, 12, 0, 0, 2, 31, 0, DamageOnSave.Unknown_3, SaveVerseType.type4, Affects.silence_15_radius, SpellWhen.Combat, 5, 4, 1, 1), /* 396C - */            
            new SpellEntry(SpellClass.Cleric,    2, 0, 0, 0, 60, 4, 2, DamageOnSave.Normal, SaveVerseType.type4, Affects.slow_poison, SpellWhen.Both, 1, 0, 0, 0), 
            new SpellEntry(SpellClass.Cleric,    2, 3, 0, 0, 0, 240, 0, DamageOnSave.Normal, SaveVerseType.type4, Affects.snake_charm, SpellWhen.Combat, 5, 0, 1, 0), 
            new SpellEntry(SpellClass.Cleric,    2, 3, 0, 0, 1, 0, 1, DamageOnSave.Normal, SaveVerseType.type4, Affects.spiritual_hammer, SpellWhen.Both, 5, 1, 0, 0), 
            new SpellEntry(SpellClass.MagicUser, 2, 0, 4, 0, 5, 0, 1, DamageOnSave.Normal, SaveVerseType.type4, Affects.detect_invisibility, SpellWhen.Both, 2, 1, 0, 0), 
            new SpellEntry(SpellClass.MagicUser, 2, 0, 0, 0, 0, 4, 2, DamageOnSave.Normal, SaveVerseType.type4, Affects.invisibility, SpellWhen.Both, 2, 2, 0, 0), 
            new SpellEntry(SpellClass.MagicUser, 2, 0, 0, 0, 0, 0, 0, DamageOnSave.Normal, SaveVerseType.type4, Affects.none, SpellWhen.Camp, 1, 0, 0, 0), 
            new SpellEntry(SpellClass.MagicUser, 2, 0, 0, 0, 2, 0, 1, DamageOnSave.Normal, SaveVerseType.type4, Affects.mirror_image, SpellWhen.Both, 2, 3, 0, 0), 
            new SpellEntry(SpellClass.MagicUser, 2, 1, 1, 0, 1, 4, 0, DamageOnSave.Zero, SaveVerseType.type4, Affects.ray_of_enfeeblement, SpellWhen.Combat, 2, 2, 1, 0), 
            new SpellEntry(SpellClass.MagicUser, 2, 3, 0, 0, 1, 9, 0, DamageOnSave.Unknown_3, SaveVerseType.Poison, Affects.stinking_cloud, SpellWhen.Combat, 2, 5, 1, 1), 
            new SpellEntry(SpellClass.MagicUser, 2, 0, 0, 0, 60, 0, 2, DamageOnSave.Normal, SaveVerseType.type4, Affects.strength, SpellWhen.Camp, 10, 0, 0, 0), 
            new SpellEntry(SpellClass.Monster,   7, 4, 0, 0, 0, 8, 4, DamageOnSave.Zero, SaveVerseType.type4, Affects.none, SpellWhen.Both, 0, 2, 0, 0), 
            new SpellEntry(SpellClass.Cleric,    3, 0, 0, 0, 0, 4, 2, DamageOnSave.Normal, SaveVerseType.type4, Affects.none, SpellWhen.Both, 10, 0, 0, 0), 
            new SpellEntry(SpellClass.Cleric,    3, -1, 0, 0, 0, 4, 0, DamageOnSave.Zero, SaveVerseType.type4, Affects.blinded, SpellWhen.Combat, 10, 3, 1, 0), 
            new SpellEntry(SpellClass.Cleric,    3, 0, 0, 0, 0, 0, 2, DamageOnSave.Normal, SaveVerseType.type4, Affects.none, SpellWhen.Camp, 100, 0, 0, 0), 
            new SpellEntry(SpellClass.Cleric,    3, -1, 0, 0, 0, 4, 0, DamageOnSave.Zero, SaveVerseType.type4, Affects.cause_disease_1, SpellWhen.Combat, 100, 4, 1, 0), 
            new SpellEntry(SpellClass.Cleric,    3, 6, 0, 0, 0, 9, 2, DamageOnSave.Normal, SaveVerseType.type4, Affects.none, SpellWhen.Both, 4, 3, 1, 1), 
            new SpellEntry(SpellClass.Cleric,    3, 0, 0, 0, 1, 0, 4, DamageOnSave.Normal, SaveVerseType.type4, Affects.prayer, SpellWhen.Both, 6, 5, 0, 0), 
            new SpellEntry(SpellClass.Cleric,    3, 0, 0, 0, 0, 4, 2, DamageOnSave.Normal, SaveVerseType.type4, Affects.none, SpellWhen.Both, 6, 0, 0, 0), 
            new SpellEntry(SpellClass.Cleric,    3, -1, 0, 0, 10, 4, 0, DamageOnSave.Zero, SaveVerseType.type4, Affects.bestow_curse, SpellWhen.Combat, 6, 5, 1, 0), 
            new SpellEntry(SpellClass.MagicUser, 3, 0, 0, 0, 1, 0, 0, DamageOnSave.Normal, SaveVerseType.type4, Affects.blink, SpellWhen.Combat, 1, 2, 0, 0), 
            new SpellEntry(SpellClass.MagicUser, 3, 12, 0, 0, 1, 9, 2, DamageOnSave.Normal, SaveVerseType.type4, Affects.none, SpellWhen.Both, 3, 2, 1, 1), 
            new SpellEntry(SpellClass.MagicUser, 3, 10, 1, 0, 0, 11, 0, DamageOnSave.Half, SaveVerseType.type4, Affects.none,SpellWhen.Combat, 3, 7, 1, 3), 
            new SpellEntry(SpellClass.MagicUser, 3, 6, 0, 3, 1, 10, 4, DamageOnSave.Normal, SaveVerseType.type4, Affects.haste, SpellWhen.Both, 3, 3, 0, 0), 
            new SpellEntry(SpellClass.MagicUser, 3, 12, 0, 0, 2, 7, 0, DamageOnSave.Zero, SaveVerseType.type4, Affects.paralyze, SpellWhen.Combat, 3, 6, 1, 0), 
            new SpellEntry(SpellClass.MagicUser, 3, 0, 0, 0, 0, 9, 4, DamageOnSave.Normal, SaveVerseType.type4, Affects.invisibility, SpellWhen.Both, 3, 1, 0, 0), 
            new SpellEntry(SpellClass.MagicUser, 3, 4, 1, 0, 0, 8, 0, DamageOnSave.Half, SaveVerseType.type4, Affects.none, SpellWhen.Combat, 3, 6, 1, 0), 
            new SpellEntry(SpellClass.MagicUser, 3, 0, 0, 0, 2, 4, 2, DamageOnSave.Normal, SaveVerseType.type4, Affects.prot_from_evil_10_radius, SpellWhen.Both, 3, 1, 0, 0), 
            new SpellEntry(SpellClass.MagicUser, 3, 0, 0, 0, 2, 4, 2, DamageOnSave.Normal, SaveVerseType.type4, Affects.prot_from_good_10_radius, SpellWhen.Both, 3, 2, 0, 0), 
            new SpellEntry(SpellClass.MagicUser, 3, 0, 0, 0, 10, 4, 2, DamageOnSave.Normal, SaveVerseType.type4, Affects.prot_from_normal_missiles, SpellWhen.Both, 3, 3, 0, 0), 
            new SpellEntry(SpellClass.MagicUser, 3, 9, 1, 3, 1, 10, 0, DamageOnSave.Normal, SaveVerseType.type4, Affects.slow, SpellWhen.Combat, 3, 4, 1, 0), 
            new SpellEntry(SpellClass.Cleric,    7, 0, 0, 0, 0, 4, 2, DamageOnSave.Normal, SaveVerseType.type4, Affects.none, SpellWhen.Both, 6, 0, 0, 0), 
            new SpellEntry(SpellClass.Monster,   6, 0, 0, 0, 0, 0, 1, DamageOnSave.Normal, SaveVerseType.type4, Affects.haste, SpellWhen.Both, 0, 3, 0, 0), 
            new SpellEntry(SpellClass.Cleric,    4, 0, 0, 0, 0, 4, 2, DamageOnSave.Normal, SaveVerseType.type4, Affects.none, SpellWhen.Both, 7, 1, 0, 0), 
            new SpellEntry(SpellClass.Monster,   6, 0, 0, 0, 0, 0, 1, DamageOnSave.Normal, SaveVerseType.type4, Affects.strength, SpellWhen.Both, 0, 1, 0, 0), 
            new SpellEntry(SpellClass.Monster,   6, 4, 4, 0, 0, 4, 0, DamageOnSave.Half, SaveVerseType.type4, Affects.none, SpellWhen.Combat, 0, 7, 1, 0), 
            new SpellEntry(SpellClass.Monster,   6, 6, 0, 0, 0, 4, 0, DamageOnSave.Zero, SaveVerseType.Poison, Affects.paralyze, SpellWhen.Combat, 0, 7, 1, 0), 
            new SpellEntry(SpellClass.Monster,   6, 0, 0, 0, 0, 0, 1, DamageOnSave.Normal, SaveVerseType.type4, Affects.haste, SpellWhen.Both, 0, 1, 0, 0), 
            new SpellEntry(SpellClass.Monster,   6, 0, 0, 0, 0, 7, 4, DamageOnSave.Normal, SaveVerseType.type4, Affects.invisible, SpellWhen.Both, 0, 2, 0, 0), 
            new SpellEntry(SpellClass.Monster,   6, 7, 0, 0, 0, 11, 0, DamageOnSave.Half, SaveVerseType.type4, Affects.none, SpellWhen.Combat, 0, 7, 1, 3), 
            new SpellEntry(SpellClass.Monster,   6, 12, 0, 0, 0, 4, 0, DamageOnSave.Normal, SaveVerseType.type4, Affects.none, SpellWhen.Combat, 0, 6, 1, 0), 
            new SpellEntry(SpellClass.Cleric,    4, 0, 0, 0, 0, 4, 0, DamageOnSave.Normal, SaveVerseType.type4, Affects.none, SpellWhen.Combat, 7, 5, 1, 0), 
            new SpellEntry(SpellClass.Cleric,    4, 0, 0, 0, 0, 0, 2, DamageOnSave.Normal, SaveVerseType.type4, Affects.none, SpellWhen.Camp, 7, 0, 0, 0), 
            new SpellEntry(SpellClass.Cleric,    4, 0, 0, 0, 0, 4, 0, DamageOnSave.Zero, SaveVerseType.Poison, Affects.none, SpellWhen.Combat, 7, 6, 1, 0), 
            new SpellEntry(SpellClass.Cleric,    4, 3, 0, 0, 10, 4, 2, DamageOnSave.Normal, SaveVerseType.type4, Affects.prot_from_evil_10_radius, SpellWhen.Both, 7, 2, 0, 0), 
            new SpellEntry(SpellClass.Cleric,    4, 3, 0, 0, 2, 4, 0, DamageOnSave.Normal, SaveVerseType.type4, Affects.sticks_to_snakes, SpellWhen.Combat, 7, 4, 1, 0), 
            new SpellEntry(SpellClass.Cleric,    5, 0, 0, 0, 0, 4, 2, DamageOnSave.Normal, SaveVerseType.type4, Affects.none, SpellWhen.Both, 8, 2, 0, 0), 
            new SpellEntry(SpellClass.Cleric,    5, -1, 0, 0, 0, 4, 0, DamageOnSave.Zero, SaveVerseType.type4, Affects.none, SpellWhen.Combat, 8, 6, 1, 0), 
            new SpellEntry(SpellClass.Cleric,    5, 0, 0, 0, 1, 0, 0, DamageOnSave.Normal, SaveVerseType.type4, Affects.sp_dispel_evil, SpellWhen.Combat, 8, 3, 0, 0), 
            new SpellEntry(SpellClass.Cleric,    5, 6, 0, 0, 0, 4, 0, DamageOnSave.Half, SaveVerseType.type4, Affects.none, SpellWhen.Combat, 8, 6, 1, 0), 
            new SpellEntry(SpellClass.Cleric,    5, 0, 0, 0, 0, 0, 2, DamageOnSave.Normal, SaveVerseType.type4, Affects.none, SpellWhen.Camp, 10, 1, 0, 0), 
            new SpellEntry(SpellClass.Cleric,    5, 3, 0, 0, 0, 4, 0, DamageOnSave.Zero, SaveVerseType.type4, Affects.none, SpellWhen.Combat, 10, 7, 1, 0), 
            new SpellEntry(SpellClass.Druid,     1, 3, 0, 12, 0, 0, 1, DamageOnSave.Normal, SaveVerseType.type4, Affects.detect_magic, SpellWhen.Both, 3, 1, 0, 0), 
            new SpellEntry(SpellClass.Druid,     1, 8, 0, 10, 0, 11, 0, DamageOnSave.Zero, SaveVerseType.type4, Affects.entangle, SpellWhen.Combat, 3, 3, 1, 0), 
            new SpellEntry(SpellClass.Druid,     1, 8, 0, 0, 4, 5, 0, DamageOnSave.Zero, SaveVerseType.type4, Affects.faerie_fire, SpellWhen.Combat, 3, 4, 1, 0), 
            new SpellEntry(SpellClass.Druid,     1, -1, 0, 10, 1, 4, 2, DamageOnSave.Normal, SaveVerseType.type4, Affects.invisible_to_animals, SpellWhen.Both, 4, 1, 1, 0), 
            new SpellEntry(SpellClass.MagicUser, 4, 6, 0, 0, 0, 5, 0, DamageOnSave.Zero, SaveVerseType.type4, Affects.charm_person, SpellWhen.Combat, 4, 6, 1, 0), 
            new SpellEntry(SpellClass.MagicUser, 4, 12, 0, 2, 1, 11, 0, DamageOnSave.Zero, SaveVerseType.type4, Affects.confuse, SpellWhen.Combat, 4, 7, 1, 0), 
            new SpellEntry(SpellClass.MagicUser, 4, 0, 3, 0, 0, 8, 0, DamageOnSave.Normal, SaveVerseType.type4, Affects.none, SpellWhen.Combat, 1, 0, 1, 0), 
            new SpellEntry(SpellClass.MagicUser, 4, 6, 0, 0, 1, 8, 0, DamageOnSave.Zero, SaveVerseType.type4, Affects.fear, SpellWhen.Combat, 4, 6, 1, 0), 
            new SpellEntry(SpellClass.MagicUser, 4, 0, 0, 2, 1, 0, 1, DamageOnSave.Normal, SaveVerseType.type4, Affects.none, SpellWhen.Both, 4, 8, 0, 0), 
            new SpellEntry(SpellClass.MagicUser, 4, 0, 1, 0, 1, 4, 0, DamageOnSave.Zero, SaveVerseType.type4, Affects.fumbling, SpellWhen.Combat, 4, 4, 1, 0), 
            new SpellEntry(SpellClass.MagicUser, 4, 0, 1, 0, 0, 10, 0, DamageOnSave.Normal, SaveVerseType.type4, Affects.none, SpellWhen.Combat, 4, 7, 1, 0), 
            new SpellEntry(SpellClass.MagicUser, 4, 0, 1, 0, 1, 0, 1, DamageOnSave.Normal, SaveVerseType.type4, Affects.minor_globe_of_invulnerability, SpellWhen.Both, 4, 5, 0, 0), 
            new SpellEntry(SpellClass.MagicUser, 4, 0, 0, 0, 0, 4, 2, DamageOnSave.Normal, SaveVerseType.type4, Affects.none, SpellWhen.Both, 4, 0, 0, 0), 
            new SpellEntry(SpellClass.Monster,   5, 1, 0, 0, 0, 240, 4, DamageOnSave.Normal, SaveVerseType.type4, Affects.animate_dead, SpellWhen.Both, 5, 0, 0, 0), 
            new SpellEntry(SpellClass.MagicUser, 5, 2, 0, 0, 1, 9, 0, DamageOnSave.Normal, SaveVerseType.type4, Affects.none, SpellWhen.Both, 5, 5, 1, 0), 
            new SpellEntry(SpellClass.MagicUser, 5, 6, 0, 0, 0, 8, 0, DamageOnSave.Half, SaveVerseType.type4, Affects.none, SpellWhen.Combat, 5, 6, 1, 0), 
            new SpellEntry(SpellClass.MagicUser, 5, 16, 0, 0, 0, 4, 0, DamageOnSave.Zero, SaveVerseType.type4, Affects.feeblemind, SpellWhen.Combat, 5, 6, 1, 0), 
            new SpellEntry(SpellClass.MagicUser, 5, 0, 1, 0, 1, 7, 0, DamageOnSave.Zero, SaveVerseType.type4, Affects.paralyze, SpellWhen.Combat, 5, 7, 1, 0), 
            new SpellEntry(SpellClass.Monster,   6, 0, 0, 0, 0, 0, 1, DamageOnSave.Normal, SaveVerseType.type4, Affects.prot_drag_breath, SpellWhen.Both, 10, 1, 0, 0), 
            new SpellEntry(SpellClass.Monster,   6, 0, 0, 0, 0, 0, 1, DamageOnSave.Normal, SaveVerseType.type4, Affects.affect_6d, SpellWhen.Both, 10, 1, 0, 0), 
            new SpellEntry(SpellClass.Monster,   6, 0, 0, 0, 0, 0, 1, DamageOnSave.Normal, SaveVerseType.type4, Affects.invisibility, SpellWhen.Both, 0, 1, 0, 0), 
            new SpellEntry(SpellClass.Monster,   6, 3, 0, 0, 0, 11, 0, DamageOnSave.Zero, SaveVerseType.type4, Affects.none, SpellWhen.Combat, 0, 1, 1, 0), 
            new SpellEntry(SpellClass.Monster,   6, 0, 0, 0, 0, 0, 1, DamageOnSave.Normal, SaveVerseType.type4, Affects.none, SpellWhen.Both, 0, 1, 0, 0), 
            new SpellEntry(SpellClass.MagicUser, 4, 0, 0, 0, 10, 4, 0, DamageOnSave.Zero, SaveVerseType.type4, Affects.none, SpellWhen.Combat, 4, 4, 1, 0), 
            new SpellEntry(SpellClass.Unknown10, 0, 10, 0, 6, 0, 24, 0, DamageOnSave.Unknown_1E, SaveVerseType.Poison, Affects.enlarge, SpellWhen.Camp, 0, 1, 0x28, 0x28) };

        public static ItemDataTable ItemDataTable; // unk_1C020

        public static List<Struct_1D183> downedPlayers; // unk_1D183 

        public static Struct_1D1BC mapToBackGroundTile; // stru_1D1BC


        public readonly static byte[] /*seg600:27D9*/ unk_18AE9 = { 0, 8, 2, 3, 4 };
        public readonly static byte[] /*seg600:27DA*/ unk_18AEA = { 8, 2, 3, 4, 8 };

        public readonly static byte[] /* seg600:27DD */ unk_18AED = { 4, 8, 0, 1, 2, 3, 4, 5, 6, 7 };

        public static List<GasCloud> NoxiousCloud; // stru_1D885
        public static List<GasCloud> PoisonousCloud; // stru_1D889 

        public static Point[] MapDirectionDelta = { new Point(0, -1), new Point(1, -1), new Point(1, 0), new Point(1, 1), new Point(0, 1), new Point(-1, 1), new Point(-1, 0), new Point(-1, -1), new Point(0, 0) };
        public readonly static sbyte[] MapDirectionXDelta = /*unk_189A6 seg600:2696*/ { 0, 1, 1, 1, 0, -1, -1, -1, 0 }; //TODO remove
        public readonly static sbyte[] MapDirectionYDelta = /*unk_189AF seg600:269F*/ { -1, -1, 0, 1, 1, 1, 0, -1, 0 };//TODO remove

        public static ImportSource import_from;
        public static bool party_fled;

        public static int friends_count;
        public static int foe_count;


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


        static Struct_1A35E monster_ages = new Struct_1A35E(new SubStruct_1A35E[] {
            new SubStruct_1A35E( 6, 2, 6), 
            new SubStruct_1A35E( 0x0c08, 0xe, 0),
            new SubStruct_1A35E( 0, 0, 0), 
            new SubStruct_1A35E( 0, 6, 0), 
            new SubStruct_1A35E( 0x502, 6, 3), 
            new SubStruct_1A35E( 4, 0, 0), 
            new SubStruct_1A35E( 0, 0, 0) });

        static Struct_1A35E dwarf_ages = new Struct_1A35E(new SubStruct_1A35E[] {
            new SubStruct_1A35E( 0xfa, 2, 0x14), 
            new SubStruct_1A35E( 0, 0, 0),
            new SubStruct_1A35E( 0x28, 5, 4), 
            new SubStruct_1A35E( 0, 0, 0), 
            new SubStruct_1A35E( 0, 0, 0), 
            new SubStruct_1A35E( 0, 0, 0), 
            new SubStruct_1A35E( 0x4B, 3, 6) });

        static Struct_1A35E elf_ages = new Struct_1A35E(new SubStruct_1A35E[] {
            new SubStruct_1A35E( 0x28a, 10, 10), 
            new SubStruct_1A35E( 0, 0, 0),
            new SubStruct_1A35E( 0x82, 5, 6), 
            new SubStruct_1A35E( 0, 0, 0), 
            new SubStruct_1A35E( 0, 0, 0), 
            new SubStruct_1A35E( 0x96, 5, 6), 
            new SubStruct_1A35E( 100, 5, 6) });

        static Struct_1A35E gnome_ages = new Struct_1A35E(new SubStruct_1A35E[] {
            new SubStruct_1A35E( 0x12C, 3, 0xC),
            new SubStruct_1A35E( 0, 0, 0),
            new SubStruct_1A35E( 0x3C, 5, 4),
            new SubStruct_1A35E( 0, 0, 0),
            new SubStruct_1A35E( 0, 0, 0), 
            new SubStruct_1A35E( 100, 2, 0x0C),
            new SubStruct_1A35E( 0x50, 5, 4)});

        static Struct_1A35E halfelf_ages = new Struct_1A35E(new SubStruct_1A35E[] {
            new SubStruct_1A35E( 0x28, 2, 4),
            new SubStruct_1A35E( 0, 0, 0),
            new SubStruct_1A35E( 0x16, 3, 4),
            new SubStruct_1A35E( 0, 0, 0),
            new SubStruct_1A35E( 0, 0, 0),
            new SubStruct_1A35E( 0x1E, 2,  8),
            new SubStruct_1A35E( 0x16, 3, 8)});

        static Struct_1A35E halfling_ages = new Struct_1A35E(new SubStruct_1A35E[] {
            new SubStruct_1A35E( 0, 0, 0),
            new SubStruct_1A35E( 0, 0, 0),
            new SubStruct_1A35E( 20, 3, 4),
            new SubStruct_1A35E( 0, 0, 0),
            new SubStruct_1A35E( 0, 0, 0),
            new SubStruct_1A35E( 0, 0, 0),
            new SubStruct_1A35E( 0x28, 2, 4)});

        static Struct_1A35E halforc_ages = new Struct_1A35E(new SubStruct_1A35E[] {
            new SubStruct_1A35E( 20, 1, 4),
            new SubStruct_1A35E( 0, 0, 0),
            new SubStruct_1A35E( 0xD, 1, 4),
            new SubStruct_1A35E( 0, 0, 0),
            new SubStruct_1A35E( 0, 0, 0),
            new SubStruct_1A35E( 0, 0, 0),
            new SubStruct_1A35E( 20, 2, 4)});

        static Struct_1A35E human_ages = new Struct_1A35E(new SubStruct_1A35E[] {
            new SubStruct_1A35E( 18, 1, 4),
            new SubStruct_1A35E( 18, 1, 4),
            new SubStruct_1A35E( 15, 1, 4),
            new SubStruct_1A35E( 17, 1, 4),
            new SubStruct_1A35E( 20, 1, 4),
            new SubStruct_1A35E( 0x18, 2, 4),
            new SubStruct_1A35E( 18 , 1, 4)});

        public static Struct_1A35E[] race_ages = new Struct_1A35E[] { monster_ages, dwarf_ages, elf_ages, gnome_ages, halfelf_ages, halfling_ages, halforc_ages, human_ages }; // unk_1A35E

        public static byte[] unk_1AE0B = new byte[3];


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
