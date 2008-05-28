using Classes;

namespace engine
{
    class ovr018
    {
        static Set unk_4C13D = new Set(0x0802, new byte[] { 0x80, 0x80 });
        static Set unk_4C15D = new Set(0x0803, new byte[] { 0x20, 0x00, 0x08 });

        internal static void free_player(ref Player playerBase)
        {
            Player playerPtr;
            Affect affect_ptr;
            Affect next_affect;
            Item itemPtr;
            Item next_item_ptr;

            playerPtr = playerBase;

            if (playerPtr.actions != null)
            {
                playerPtr.actions = null; // FreeMem( action_struct_size, playerPtr.actions );
            }

            itemPtr = playerPtr.itemsPtr;
            playerPtr.itemsPtr = null;

            while (itemPtr != null)
            {
                next_item_ptr = itemPtr;
                itemPtr = next_item_ptr.next;

                next_item_ptr.next = null;
                next_item_ptr = null; // FreeMem( item_struct_size, next_item_ptr );
            }

            affect_ptr = playerPtr.affect_ptr;
            playerPtr.affect_ptr = null;

            while (affect_ptr != null)
            {
                next_affect = affect_ptr;
                affect_ptr = next_affect.next;

                next_affect.next = null;
                next_affect = null; // FreeMem( Affect.StructSize, next_affect );
            }

            playerBase = null; // FreeMem( char_struct_size, playerBase );
        }

        static string[] menuStrings = {   
            "Create New Character", 
            "Drop Character",
            "Modify Character",
            "Train Character",
            "Human Change Classes",
            "View Character",
            "Add Character to Party",
            "Remove Character from Party",
            "Load Saved Game",
            "Save Current Game",
            "BEGIN Adventuring",
            "Exit to DOS"
        };


        const int allow_create = 0;
        const int allow_drop = 1;
        const int allow_modify = 2;
        const int allow_training = 3;
        const int allow_multiclass = 4;
        const int allow_view = 5;
        const int allow_add = 6;
        const int allow_remove = 7;
        const int allow_load = 8;
        const int allow_save = 9;
        const int allow_begin = 10;
        const int allow_exit = 11;


        static bool[] menuFlags = {  
            true, 
            false,
            false,
            false,
            false,
            false,
            true,
            false,
            false,
            false,
            false,
            true
        };

        internal static void startGameMenu()
        {
            string var_111;
            byte var_11;
            byte gameState;
            bool var_F;
            byte var_E;
            bool var_3;
            char inputkey;
            byte loop_cx;

            gameState = gbl.game_state;
            gbl.game_state = 0;
            var_F = true;

            while (true)
            {
                while (var_F == true)
                {
                    seg037.draw8x8_outer_frame();
                    if (gbl.player_ptr != null)
                    {
                        ovr025.Player_Summary(gbl.player_ptr);
                        menuFlags[allow_drop] = true;
                        menuFlags[allow_modify] = true;

                        if (gbl.area2_ptr.field_550 > 0 ||
                            Cheats.free_training == true)
                        {
                            menuFlags[allow_training] = true;
                        }

                        if ((gbl.area2_ptr.field_550 <= 0 && Cheats.free_training == false) ||
                            ovr026.is_human(gbl.player_ptr) == false ||
                            ovr026.getExtraFirstSkill(gbl.player_ptr) != 0x11)
                        {
                            menuFlags[allow_multiclass] = false;
                        }
                        else
                        {
                            menuFlags[allow_multiclass] = true;
                        }

                        menuFlags[allow_view] = true;
                        menuFlags[allow_remove] = true;
                        menuFlags[allow_load] = false;
                        menuFlags[allow_save] = true;
                        menuFlags[allow_begin] = true;
                    }
                    else
                    {
                        menuFlags[allow_drop] = false;
                        menuFlags[allow_modify] = false;
                        menuFlags[allow_training] = false;
                        menuFlags[allow_multiclass] = false;
                        menuFlags[allow_view] = false;
                        menuFlags[allow_remove] = false;
                        menuFlags[allow_load] = true;
                        menuFlags[allow_save] = false;
                        menuFlags[allow_begin] = false;
                    }

                    var_E = 0;
                    for (loop_cx = 0; loop_cx <= 11; loop_cx++)
                    {
                        if (menuFlags[loop_cx] == true)
                        {
                            seg041.displayString(menuStrings[loop_cx][0].ToString(), 0, 15, var_E + 12, 2);

                            seg051.Copy(menuStrings[loop_cx].Length, 1, menuStrings[loop_cx], out var_111);
                            seg041.displayString(var_111, 0, 10, var_E + 12, 3);
                            var_E++;
                        }
                    }

                    var_F = false;
                }

                inputkey = ovr027.displayInput(out var_3, false, 1, 0, 0, 13, "C D M T H V A R L S B E J", "Choose a function ");

                ovr027.redraw_screen();

                if (var_3 == true)
                {
                    if (unk_4C13D.MemberOf((byte)inputkey) == true)
                    {
                        if (ovr026.is_human(gbl.player_ptr) == false ||
                            ovr026.getExtraFirstSkill(gbl.player_ptr) != 0x11)
                        {
                            var_11 = 0;
                        }
                        else
                        {
                            var_11 = 1;
                        }

                        ovr020.scroll_team_list(inputkey);
                        ovr025.Player_Summary(gbl.player_ptr);

                        if (ovr026.is_human(gbl.player_ptr) == false ||
                            ovr026.getExtraFirstSkill(gbl.player_ptr) != 0x11)
                        {
                            var_11 ^= 0;
                        }
                        else
                        {
                            var_11 ^= 0;
                        }

                        if (var_11 == 0 ||
                            gbl.area2_ptr.field_550 <= 0)
                        {
                            var_F = false;
                        }
                        else
                        {
                            var_F = true;
                        }
                    }
                }
                else
                {
                    if (unk_4C15D.MemberOf((byte)inputkey) == false)
                    {
                        gbl.byte_1C01B = 0;
                    }

                    switch (inputkey)
                    {
                        case 'C':
                            if (menuFlags[allow_create] == true)
                            {
                                createPlayer();
                            }
                            break;

                        case 'D':
                            if (menuFlags[allow_drop] == true)
                            {
                                dropPlayer();
                            }
                            break;
                        case 'M':
                            if (menuFlags[allow_modify] == true)
                            {
                                modifyPlayer();
                            }
                            break;
                        case 'T':
                            if (menuFlags[allow_training] == true)
                            {
                                train_player();
                            }
                            break;
                        case 'H':
                            if (menuFlags[allow_multiclass] == true)
                            {
                                ovr026.multiclass(gbl.player_ptr);
                            }
                            break;

                        case 'V':
                            if (menuFlags[allow_view] == true)
                            {
                                bool dummyBool;
                                ovr020.viewPlayer(out dummyBool);
                            }
                            break;

                        case 'A':
                            if (menuFlags[allow_add] == true)
                            {
                                AddPlayer();
                            }
                            break;

                        case 'R':
                            if (menuFlags[allow_remove] == true &&
                                gbl.player_ptr != null)
                            {
                                if (gbl.player_ptr.field_F7 < 0x80)
                                {
                                    ovr017.sub_47DFC(string.Empty, gbl.player_ptr);
                                    free_players(1, false);
                                }
                                else
                                {
                                    dropPlayer();
                                }
                            }
                            break;

                        case 'L':
                            if (menuFlags[allow_load] == true)
                            {
                                ovr017.loadGame();
                            }
                            break;

                        case 'S':
                            if (menuFlags[allow_save] == true &&
                                gbl.player_next_ptr != null)
                            {
                                ovr017.SaveGame();
                            }

                            break;

                        case 'B':
                            if (menuFlags[allow_begin] == true)
                            {
                                if ((gbl.player_next_ptr == null && gbl.inDemo == true) ||
                                    gbl.area_ptr.field_3FA == 0 || gbl.inDemo == true)
                                {
                                    gbl.game_state = gameState;

                                    if (gbl.byte_1B2EB == 0 &&
                                        gbl.lastDaxBlockId != 0x50)
                                    {
                                        if (gbl.game_state == 3)
                                        {
                                            seg037.draw8x8_04();
                                        }
                                        else
                                        {
                                            seg037.draw8x8_03();
                                        }
                                        ovr025.Player_Summary(gbl.player_ptr);
                                    }
                                    else
                                    {
                                        if (gbl.area_ptr.field_1E4 == 0)
                                        {
                                            seg037.draw8x8_03();
                                        }
                                    }

                                    ovr027.redraw_screen();
                                    gbl.area2_ptr.field_550 = 0;

                                    return;
                                }
                            }
                            break;

                        case 'E':
                            if (menuFlags[allow_exit] == true)
                            {
                                inputkey = ovr027.yes_no(15, 10, 14, "Quit to DOS ");

                                if (inputkey == 'Y')
                                {
                                    if (gbl.player_next_ptr != null &&
                                        gbl.byte_1C01B == 0)
                                    {

                                        inputkey = ovr027.yes_no(15, 10, 14, "Game not saved.  Quit anyway? ");
                                        if (inputkey == 'N')
                                        {
                                            ovr017.SaveGame();
                                        }
                                    }

                                    if (inputkey == 'Y')
                                    {
                                        seg043.print_and_exit();
                                    }
                                }
                            }
                            break;
                    }

                    var_F = true;
                }
            }
        }

        internal static byte[] /*seg600:3EA2 */ unk_1A1B2 = { 0x02, 0x10, 0x08, 0x40, 0x40, 0x01, 0x04, 0x20 };

        static byte[] /*seg600:45B3 */ unk_1A8C3 = { 3, 3, 5, 5, 5, 2, 2, 5 };
        static byte[] /*seg600:45B4 */ unk_1A8C4 = { 6, 6, 4, 4, 4, 4, 6, 4 };

        internal static sbyte[] /* seg600:3E3A */ unk_1A14A = { 
            0x28, 0x28, 0x28, 0x28, 0x2A, 0x2A, 0x2A, 0x2C, 0x2C, 0x2C, 0x2E, 0x2E, 0x2E,
            0x28, 0x28, 0x28, 0x28, 0x2A, 0x2A, 0x2A, 0x2C, 0x2C, 0x2C, 0x2E, 0x2E, 0x2E,
            0x27, 0x28, 0x28, 0x2A, 0x2B, 0x2C, 0x2D, 0x2E, 0x2F, 0x30, 0x31, 0x32, 0x33,
            0x28, 0x28, 0x28, 0x2A, 0x2B, 0x2C, 0x2D, 0x2E, 0x2F, 0x30, 0x31, 0x32, 0x33,
            0x28, 0x28, 0x28, 0x2A, 0x2B, 0x2C, 0x2D, 0x2E, 0x2F, 0x30, 0x31, 0x32, 0x33,
            0x27, 0x27, 0x27, 0x27, 0x27, 0x27, 0x29, 0x29, 0x29, 0x29, 0x29, 0x2B, 0x2B,
            0x28, 0x28, 0x28, 0x28, 0x28, 0x29, 0x29, 0x29, 0x29, 0x2C, 0x2C, 0x2C, 0x2C,
            0x28, 0x28, 0x28, 0x28, 0x2A, 0x2A, 0x2A, 0x2C, 0x2C, 0x2C, 0x2E, 0x2E, 0x2E };


        class Struct_1A434
        {
            internal short field_0; // unk_1A434
            internal short field_1; // unk_1A436
            internal short field_2; // unk_1A438
            internal short field_3; // unk_1A43A
            internal short field_4; // unk_1A43C

            internal Struct_1A434(short f0, short f1, short f2, short f3, short f4)
            {
                field_0 = f0;
                field_1 = f1;
                field_2 = f2;
                field_3 = f3;
                field_4 = f4;
            }
        }

        static Struct_1A434[] unk_1A434 = { 
            new Struct_1A434(0x401, 0x18, 0x402, 0x12, 0x401), 
            new Struct_1A434(0x32, 0x96, 0xFA, 0x15E, 0x1C2), 
            new Struct_1A434(0xAF, 0x226, 0x36B, 0x4B0, 0x640),
            new Struct_1A434(0x5A, 0x12C, 0x1C2, 0x258, 0x2EE),
            new Struct_1A434(0x28, 0x64, 0xAF, 0x0FA, 0x145),
            new Struct_1A434(0x21, 0x44, 0x65, 0x90, 0x0C7), 
            new Struct_1A434(0x0F, 0x1E, 0x2D, 0x3C, 0x50),
            new Struct_1A434(0x14, 0x28, 0x3C, 0x5A, 0x78) };


        internal class stats_ranges
        {
            internal byte[] str_min; // unk_1A298
            internal byte[] str_max; // max_str_val
            internal byte[] str_100_max; // unk_1A29C
            internal byte int_min; // unk_1A29E
            internal byte int_max; // unk_1A29F
            internal byte field_8; // unk_1A2A0
            internal byte field_9; // unk_1A2A1
            internal byte field_A; // unk_1A2A2
            internal byte field_B; // unk_1A2A3
            internal byte field_C; // unk_1A2A4
            internal byte field_D; // unk_1A2A5
            internal byte field_E; // unk_1A2A6
            internal byte field_F; // max_cha_val

            internal stats_ranges(byte v0, byte v1, byte v2, byte v3, byte v4, byte v5, byte v6,
                byte v7, byte v8, byte v9, byte vA, byte vB, byte vC, byte vD, byte vE, byte vF)
            {
                str_min = new byte[] { v0, v1 };
                str_max = new byte[] { v2, v3 };
                str_100_max = new byte[] { v4, v5 };
                int_min = v6;
                int_max = v7;
                field_8 = v8;
                field_9 = v9;
                field_A = vA;
                field_B = vB;
                field_C = vC;
                field_D = vD;
                field_E = vE;
                field_F = vF;

            }
        }

        static Set asc_4C707 = new Set(0x0002, new byte[]{ 0x00, 0x1F } );

        internal static stats_ranges[] stru_1A298 = new stats_ranges[] { 
            new stats_ranges(0, 5, 0xa, 0, 5, 5, 0xa, 0xF, 5, 0xA, 0xA, 0xF, 0x14, 0xA, 0xC, 0xC), 
            new stats_ranges(8, 8, 0x12, 0x11, 0x63, 0, 3, 0x12, 3, 0x12, 3, 0x11, 0xC, 0x13, 3, 0x10), 
            new stats_ranges(3, 3, 0x12, 0x10, 0x4B, 0, 8, 0x12, 3, 0x12, 7, 0x13, 6, 0x12, 8, 0x12), 
            new stats_ranges(6, 6, 0x12, 0xF, 0x32, 0, 7, 0x12, 3, 0x12, 3, 0x12, 8, 0x12, 3, 0x12), 
            new stats_ranges(3, 3, 0x12, 0x11, 0x5A, 0, 4, 0x12, 3, 0x12, 6, 0x12, 6, 0x12, 3, 0x12), 
            new stats_ranges(6, 6, 0x11, 0xE, 0, 0, 6, 0x12, 3, 0x11, 8, 0x12, 0xA, 0x13, 3, 0x12), 
            new stats_ranges(6, 6, 0x12, 0x12, 0x63, 0x4B, 3, 0x11, 3, 0xE, 3, 0x11, 0xD, 0x13, 3, 0xC), 
            new stats_ranges(3, 3, 0x12, 0x12, 0x64, 0x32, 3, 0x12, 3, 0x12, 3, 0x12, 3, 0x12, 3, 0x12) };

        internal static void createPlayer()
        {
            Player var_53;
            byte var_4F;
            string var_4E;
            byte var_25;
            bool var_23;
            bool var_22;
            byte var_21;
            byte var_20;
            short var_1E;
            byte var_1C;
            byte var_1B;
            byte loop4_var;
            byte loop2_var;
            byte loop1_var;
            char reroll_stats;
            byte stat_value;
            byte var_14;
            short var_12;
            StringList var_10;
            Player var_8;
            Player player;

            player = new Player();

            var_53 = player;
            for (int i = 0; i < 6; i++)
            {
                var_53.field_145[i] = (byte)(((gbl.unk_1A1D3[i] + 8) << 4) + gbl.unk_1A1D3[i]);
            }

            var_53.field_124 = 0x32;
            var_53.field_73 = 0x28;
            var_53.health_status = Status.okey;
            var_53.in_combat = true;
            var_53.field_DE = 1;
            var_53.field_126 = (byte)seg051.Random(256);
            var_53.icon_id = 0x0A;

            StringList var_C = ovr027.alloc_stringList(7);
            var_C.field_29 = 1;
            var_C.s = "Pick Race";

            for (loop1_var = 1; loop1_var <= 7; loop1_var++)
            {
                if (loop1_var >= 1 && loop1_var <= 5)
                {
                    var_10 = ovr027.getStringListEntry(var_C, loop1_var);
                    var_10.s = "  " + ovr020.raceString[loop1_var];
                }
                else if (loop1_var == 7)
                {
                    var_10 = ovr027.getStringListEntry(var_C, loop1_var - 1);
                    var_10.s = "  " + ovr020.raceString[loop1_var];
                }
            }

            var_10 = var_C;

            var_12 = 1;
            var_23 = true;
            var_22 = true;

            do
            {

                reroll_stats = ovr027.sl_select_item(out var_10, ref var_12, ref var_23, var_22, var_C,
                    22, 38, 2, 1, 15, 10, 13, "Select", string.Empty);

                if (reroll_stats == '\0')
                {
                    ovr027.free_stringList(ref var_C);
                    player = null;
                    return;
                }

            } while (reroll_stats != 'S');

            if (var_12 == 6)
            {
                var_12++;
            }

            var_53 = player;
            var_53.race = (Race)var_12;

            switch (var_53.race)
            {
                case Race.halfling:
                    var_53.icon_size = 1;
                    ovr024.add_affect(false, 0xff, 0, Affects.affect_61, player);
                    break;

                case Race.dwarf:
                    var_53.icon_size = 1;
                    ovr024.add_affect(false, 0xff, 0, Affects.affect_61, player);
                    ovr024.add_affect(false, 0xff, 0, Affects.affect_1a, player);
                    ovr024.add_affect(false, 0xff, 0, Affects.affect_2f, player);
                    break;

                case Race.gnome:
                    var_53.icon_size = 1;
                    ovr024.add_affect(false, 0xff, 0, Affects.affect_61, player);
                    ovr024.add_affect(false, 0xff, 0, Affects.affect_12, player);
                    ovr024.add_affect(false, 0xff, 0, Affects.affect_2f, player);
                    ovr024.add_affect(false, 0xff, 0, Affects.affect_30, player);
                    break;

                case Race.elf:
                    var_53.icon_size = 2;
                    ovr024.add_affect(false, 0xff, 0, Affects.affect_6b, player);

                    break;

                case Race.half_elf:
                    var_53.icon_size = 2;
                    ovr024.add_affect(false, 0xff, 0, Affects.affect_7c, player);
                    break;

                default:
                    var_53.icon_size = 2;
                    break;
            }

            /* Gender */

            ovr027.free_stringList(ref var_C);

            var_C = ovr027.alloc_stringList(3);
            var_C.field_29 = 1;
            var_C.s = "Pick Gender";

            for (loop2_var = 0; loop2_var <= 1; loop2_var++)
            {
                var_10 = ovr027.getStringListEntry(var_C, loop2_var + 1);
                var_10.s = "  " + ovr020.sexString[loop2_var];
            }

            var_10 = var_C;

            var_12 = 1;
            var_22 = true;
            var_23 = true;

            do
            {
                reroll_stats = ovr027.sl_select_item(out var_10, ref var_12, ref var_23, var_22, var_C,
                    22, 38, 2, 1, 15, 10, 13, "Select", string.Empty);

                if (reroll_stats == '\0')
                {
                    ovr027.free_stringList(ref var_C);
                    player = null;
                    return;
                }

            } while (reroll_stats != 'S');


            player.sex = (byte)(var_12 - 1);
            ovr027.free_stringList(ref var_C);

            var_1C = gbl.unk_1A30A[(int)player.race, 0];

            var_C = ovr027.alloc_stringList(var_1C + 1);

            var_C.field_29 = 1;
            var_C.s = "Pick Class";

            for (int i = 1; i <= var_1C; i++)
            {
                var_10 = ovr027.getStringListEntry(var_C, i);
                var_10.s = "  " + ovr020.classString[gbl.unk_1A30A[(int)player.race, i]];
            }

            var_10 = var_C;
            var_12 = 1;
            var_22 = true;
            var_23 = true;

            do
            {
                reroll_stats = ovr027.sl_select_item(out var_10, ref var_12, ref var_23, var_22, var_C,
                    22, 38, 2, 1, 15, 10, 13, "Select", string.Empty);

                if (reroll_stats == '\0')
                {
                    ovr027.free_stringList(ref var_C);
                    player = null;
                    return;
                }
            } while (reroll_stats != 'S');

            var_53 = player;
            var_53.exp = 25000;
            var_53._class = (ClassId)gbl.unk_1A30A[(int)var_53.race, var_12];
            var_53.field_E5 = 1;

            if (var_53._class >= ClassId.cleric && var_53._class <= ClassId.fighter)
            {
                var_53.Skill_A_lvl[(int)var_53._class] = 1;
            }
            else if (var_53._class >= ClassId.magic_user && var_53._class <= ClassId.monk)
            {
                var_53.Skill_A_lvl[(int)var_53._class] = 1;
            }
            else if (var_53._class == ClassId.paladin)
            {
                player.field_191 = 1;
                var_53.paladin_lvl = 1;
                ovr024.add_affect(false, 0xff, 0, Affects.protection_from_evil, player);
            }
            else if (var_53._class == ClassId.ranger)
            {
                var_53.ranger_lvl = 1;
                ovr024.add_affect(false, 0xff, 0, Affects.affect_86, player);
            }
            else if (var_53._class == ClassId.mc_c_f)
            {
                var_53.cleric_lvl = 1;
                var_53.fighter_lvl = 1;
                var_53.exp = 12500;
            }
            else if (var_53._class == ClassId.mc_c_f_m)
            {
                var_53.cleric_lvl = 1;
                var_53.fighter_lvl = 1;
                var_53.magic_user_lvl = 1;
                var_53.exp = 8333;
            }
            else if (var_53._class == ClassId.mc_c_r)
            {
                var_53.cleric_lvl = 1;
                var_53.ranger_lvl = 1;
                ovr024.add_affect(false, 0xff, 0, Affects.affect_86, player);
                var_53.exp = 12500;
            }
            else if (var_53._class == ClassId.mc_c_mu)
            {
                var_53.cleric_lvl = 1;
                var_53.magic_user_lvl = 1;
                var_53.exp = 12500;
            }
            else if (var_53._class == ClassId.mc_c_t)
            {
                var_53.cleric_lvl = 1;
                var_53.thief_lvl = 1;
                var_53.exp = 12500;
            }
            else if (var_53._class == ClassId.mc_f_mu)
            {
                var_53.fighter_lvl = 1;
                var_53.magic_user_lvl = 1;
                var_53.exp = 12500;
            }
            else if (var_53._class == ClassId.mc_f_t)
            {
                var_53.fighter_lvl = 1;
                var_53.thief_lvl = 1;
                var_53.exp = 12500;
            }
            else if (var_53._class == ClassId.mc_f_mu_t)
            {
                var_53.fighter_lvl = 1;
                var_53.magic_user_lvl = 1;
                var_53.thief_lvl = 1;
                var_53.exp = 8333;
            }
            else if (var_53._class == ClassId.mc_mu_t)
            {
                var_53.magic_user_lvl = 1;
                var_53.thief_lvl = 1;
                var_53.exp = 8333;
            }

            if (var_53.thief_lvl > 0)
            {
                ovr026.sub_6AAEA(player);
            }

            var_53.classFlags = 0;
            var_53.field_73 = 0;

            for (loop4_var = 0; loop4_var <= 7; loop4_var++)
            {
                if (var_53.Skill_A_lvl[loop4_var] > 0)
                {
                    sbyte t = (sbyte)(unk_1A14A[(loop4_var * 0x0D) + var_53.Skill_A_lvl[loop4_var]]);

                    if (t > var_53.field_73)
                    {
                        var_53.field_73 = t;
                    }

                    var_53.classFlags += unk_1A1B2[loop4_var];
                }
            }

            ovr026.sub_6A7FB(player);
            ovr027.free_stringList(ref var_C);

            var_1C = gbl.unk_1A4EA[(int)player._class, 0];

            var_C = ovr027.alloc_stringList(var_1C + 1);

            var_C.field_29 = 1;
            var_C.s = "Pick Alignment";

            for (int i = 1; i <= var_1C; i++)
            {
                var_10 = ovr027.getStringListEntry(var_C, i);
                var_10.s = "  " + ovr020.alignmentString[gbl.unk_1A4EA[(int)player._class, i]];
            }

            var_10 = var_C;
            var_12 = 1;
            var_22 = true;
            var_23 = true;

            do
            {
                reroll_stats = ovr027.sl_select_item(out var_10, ref var_12, ref var_23, var_22, var_C,
                    22, 38, 2, 1, 15, 10, 13, "Select", string.Empty);


                if (reroll_stats == '\0')
                {
                    ovr027.free_stringList(ref var_C);

                    seg051.FreeMem(0x1A6, player);
                    return;
                }
            } while (reroll_stats != 'S');

            player.alignment = gbl.unk_1A4EA[(int)player._class, var_12];

            ovr027.free_stringList(ref var_C);

            if (player._class <= ClassId.monk)
            {
                SubStruct_1A35E v5 = gbl.unk_1A35E[(int)player.race][player._class];

                player.age = (short)(ovr024.roll_dice(v5.field_3, v5.field_2) + v5.field_0);
            }
            else
            {
                var_53 = player;
                int race = (int)var_53.race;

                switch (var_53._class)
                {
                    case ClassId.mc_c_f:
                        var_53.age = (short)(gbl.unk_1A35E[race][0].field_0 + (gbl.unk_1A35E[race][0].field_2 * gbl.unk_1A35E[race][0].field_3));
                        break;

                    case ClassId.mc_c_f_m:
                        var_53.age = (short)(gbl.unk_1A35E[race][0].field_0 + (gbl.unk_1A35E[race][0].field_2 * gbl.unk_1A35E[race][0].field_3));
                        break;

                    case ClassId.mc_c_mu:
                        var_53.age = (short)(gbl.unk_1A35E[race][0].field_0 + (gbl.unk_1A35E[race][0].field_2 * gbl.unk_1A35E[race][0].field_3));
                        break;

                    case ClassId.mc_c_t:
                        var_53.age = (short)(gbl.unk_1A35E[race][0].field_0 + (gbl.unk_1A35E[race][0].field_2 * gbl.unk_1A35E[race][0].field_3));
                        break;

                    case ClassId.mc_f_mu:
                        var_53.age = (short)(gbl.unk_1A35E[race][6].field_0 + (gbl.unk_1A35E[race][6].field_2 * gbl.unk_1A35E[race][6].field_3));
                        break;

                    case ClassId.mc_f_t:
                        var_53.age = (short)(gbl.unk_1A35E[race][2].field_0 + (gbl.unk_1A35E[race][2].field_2 * gbl.unk_1A35E[race][2].field_3));
                        break;

                    case ClassId.mc_f_mu_t:
                        var_53.age = (short)(gbl.unk_1A35E[race][6].field_0 + (gbl.unk_1A35E[race][6].field_2 * gbl.unk_1A35E[race][6].field_3));
                        break;

                    case ClassId.mc_mu_t:
                        var_53.age = (short)(gbl.unk_1A35E[race][6].field_0 + (gbl.unk_1A35E[race][6].field_2 * gbl.unk_1A35E[race][6].field_3));
                        break;

                    case ClassId.mc_c_r:
                        var_53.age = (short)(gbl.unk_1A35E[race][0].field_0 + (gbl.unk_1A35E[race][0].field_2 * gbl.unk_1A35E[race][0].field_3));
                        break;
                }
            }

            var_8 = gbl.player_ptr;
            gbl.player_ptr = player;
            ovr020.playerDisplayFull();

            do
            {
                for (loop4_var = 0; loop4_var <= 7; loop4_var++)
                {
                    if (gbl.player_ptr.Skill_A_lvl[loop4_var] > 0)
                    {
                        gbl.player_ptr.Skill_A_lvl[loop4_var] = 1;
                    }
                }

                player.strength_18_100 = 0;

                for (var_1B = 0; var_1B <= 5; var_1B++)
                {
                    player.stats[var_1B].max = 0;

                    for (var_14 = 1; var_14 <= 6; var_14++)
                    {
                        stat_value = (byte)(ovr024.roll_dice(6, 3) + 1);

                        switch (loop1_var)
                        {
                            case 1:
                                if (var_1B == 4)
                                {
                                    stat_value += 1;
                                }
                                else if (var_1B == 5)
                                {
                                    stat_value -= 1;
                                }
                                break;

                            case 2:
                                if (var_1B == 3)
                                {
                                    stat_value += 1;
                                }
                                else if (var_1B == 4)
                                {
                                    stat_value -= 1;
                                }
                                break;

                            case 5:
                                if (var_1B == 0)
                                {
                                    stat_value -= 1;
                                }
                                else if (var_1B == 3)
                                {
                                    stat_value++;
                                }

                                break;

                            case 6:
                                if (var_1B == 0)
                                {
                                    stat_value++;
                                }
                                else if (var_1B == 4)
                                {
                                    stat_value++;
                                }
                                else if (var_1B == 5)
                                {
                                    stat_value -= 2;
                                }
                                break;
                        }

                        if (player.stats[var_1B].max < stat_value)
                        {
                            player.stats[var_1B].max = stat_value;
                        }
                    }

                    var_53 = player;

                    switch ((Stat)var_1B)
                    {
                        case Stat.STR:
                            if (var_53.stats[var_1B].max > 0 &&
                                unk_1A434[(int)var_53.race].field_0 < var_53.age)
                            {
                                var_53.stats[var_1B].max += 1;
                            }

                            if (var_53.stats[var_1B].max > 0 &&
                                unk_1A434[(int)var_53.race].field_1 < var_53.age)
                            {
                                var_53.stats[var_1B].max -= 1;
                            }

                            if (var_53.stats[var_1B].max > 0 &&
                                unk_1A434[(int)var_53.race].field_2 < var_53.age)
                            {
                                var_53.stats[var_1B].max -= 2;
                            }

                            if (var_53.stats[var_1B].max > 0 &&
                                unk_1A434[(int)var_53.race].field_3 < var_53.age)
                            {
                                var_53.stats[var_1B].max -= 1;
                            }

                            if (var_53.stats[var_1B].max < stru_1A298[(int)var_53.race].str_min[var_53.sex])
                            {
                                var_53.stats[var_1B].max = stru_1A298[(int)var_53.race].str_min[var_53.sex];
                            }

                            if (var_53.stats[var_1B].max > stru_1A298[(int)var_53.race].str_max[var_53.sex])
                            {
                                var_53.stats[var_1B].max = stru_1A298[(int)var_53.race].str_max[var_53.sex];
                            }

                            if (var_53.stats[var_1B].max < gbl.unk_1A484[(int)var_53._class][var_1B])
                            {
                                var_53.stats[var_1B].max = gbl.unk_1A484[(int)var_53._class][var_1B];
                            }

                            if (var_53.strength == 18)
                            {
                                if (var_53.fighter_lvl > 0 ||
                                    var_53.ranger_lvl > 0 ||
                                    var_53.paladin_lvl > 0)
                                {
                                    var_53.strength_18_100 = (byte)(seg051.Random(100) + 1);

                                    if (var_53.strength_18_100 > stru_1A298[(int)var_53.race].str_100_max[var_53.sex])
                                    {
                                        var_53.strength_18_100 = stru_1A298[(int)var_53.race].str_100_max[var_53.sex];
                                    }
                                }
                            }
                            break;

                        case Stat.INT:

                            if (unk_1A434[(int)var_53.race].field_1 < var_53.age)
                            {
                                var_53.stats[var_1B].max += 1;
                            }

                            if (unk_1A434[(int)var_53.race].field_3 < var_53.age)
                            {
                                var_53.stats[var_1B].max += 1;
                            }

                            if (var_53.stats[var_1B].max < stru_1A298[(int)var_53.race].int_min)
                            {
                                var_53.stats[var_1B].max = stru_1A298[(int)var_53.race].int_min;
                            }

                            if (var_53.stats[var_1B].max > stru_1A298[(int)var_53.race].int_max)
                            {
                                var_53.stats[var_1B].max = stru_1A298[(int)var_53.race].int_max;
                            }

                            if (var_53.stats[var_1B].max < gbl.unk_1A484[(int)var_53._class][var_1B])
                            {
                                var_53.stats[var_1B].max = gbl.unk_1A484[(int)var_53._class][var_1B];
                            }
                            break;

                        case Stat.WIS:
                            if (unk_1A434[(int)var_53.race].field_0 >= var_53.age)
                            {
                                var_53.stats[var_1B].max -= 1;
                            }

                            if (unk_1A434[(int)var_53.race].field_1 < var_53.age)
                            {
                                var_53.stats[var_1B].max += 1;
                            }

                            if (unk_1A434[(int)var_53.race].field_2 < var_53.age)
                            {
                                var_53.stats[var_1B].max += 1;
                            }

                            if (unk_1A434[(int)var_53.race].field_3 < var_53.age)
                            {
                                var_53.stats[var_1B].max += 1;
                            }

                            if (var_53.stats[var_1B].max < stru_1A298[(int)var_53.race].field_8)
                            {
                                var_53.stats[var_1B].max = stru_1A298[(int)var_53.race].field_8;
                            }

                            if (var_53.stats[var_1B].max > stru_1A298[(int)var_53.race].field_9)
                            {
                                var_53.stats[var_1B].max = stru_1A298[(int)var_53.race].field_9;
                            }

                            if (var_53.stats[var_1B].max < gbl.unk_1A484[(int)var_53._class][var_1B])
                            {
                                var_53.stats[var_1B].max = gbl.unk_1A484[(int)var_53._class][var_1B];
                            }

                            if (var_53.stats[var_1B].max < 13 &&
                                asc_4C707.MemberOf((byte)var_53._class) == true)
                            {
                                var_53.stats[var_1B].max = 13;
                            }
                            break;

                        case Stat.DEX:

                            if (unk_1A434[(int)var_53.race].field_2 < var_53.age)
                            {
                                var_53.stats[var_1B].max -= 2;
                            }

                            if (unk_1A434[(int)var_53.race].field_3 < var_53.age)
                            {
                                var_53.stats[var_1B].max -= 2;
                            }

                            if (var_53.stats[var_1B].max < stru_1A298[(int)var_53.race].field_A)
                            {
                                var_53.stats[var_1B].max = stru_1A298[(int)var_53.race].field_A;
                            }

                            if (var_53.stats[var_1B].max > stru_1A298[(int)var_53.race].field_B)
                            {
                                var_53.stats[var_1B].max = stru_1A298[(int)var_53.race].field_B;
                            }

                            if (var_53.stats[var_1B].max < gbl.unk_1A484[(int)var_53._class][var_1B])
                            {
                                var_53.stats[var_1B].max = gbl.unk_1A484[(int)var_53._class][var_1B];
                            }
                            break;

                        case Stat.CON:
                            if (unk_1A434[(int)var_53.race].field_1 < var_53.age)
                            {
                                var_53.stats[var_1B].max -= 1;
                            }

                            if (unk_1A434[(int)var_53.race].field_2 < var_53.age)
                            {
                                var_53.stats[var_1B].max -= 1;
                            }

                            if (unk_1A434[(int)var_53.race].field_3 < var_53.age)
                            {
                                var_53.stats[var_1B].max -= 1;
                            }

                            if (var_53.stats[var_1B].max < stru_1A298[(int)var_53.race].field_C)
                            {
                                var_53.stats[var_1B].max = stru_1A298[(int)var_53.race].field_C;
                            }

                            if (var_53.stats[var_1B].max > stru_1A298[(int)var_53.race].field_D)
                            {
                                var_53.stats[var_1B].max = stru_1A298[(int)var_53.race].field_D;
                            }

                            if (var_53.stats[var_1B].max < gbl.unk_1A484[(int)var_53._class][var_1B])
                            {
                                var_53.stats[var_1B].max = gbl.unk_1A484[(int)var_53._class][var_1B];
                            }
                            break;

                        case Stat.CHA:
                            if (var_53.stats[var_1B].max < stru_1A298[(int)var_53.race].field_E)
                            {
                                var_53.stats[var_1B].max = stru_1A298[(int)var_53.race].field_E;
                            }

                            if (var_53.stats[var_1B].max > stru_1A298[(int)var_53.race].field_F)
                            {
                                var_53.stats[var_1B].max = stru_1A298[(int)var_53.race].field_F;
                            }

                            if (var_53.stats[var_1B].max < gbl.unk_1A484[(int)var_53._class][var_1B])
                            {
                                var_53.stats[var_1B].max = gbl.unk_1A484[(int)var_53._class][var_1B];
                            }
                            break;
                    }

                    ovr020.display_stat(false, var_1B);
                }
                var_53 = player;
                var_53.hit_point_current = var_53.hit_point_max;
                var_53.field_11C = 2;
                var_53.field_11E = 1;
                var_53.field_120 = 2;
                var_53.field_125 = 1;
                var_53.field_E4 = 0x0C;
                var_21 = 0;
                var_20 = 0;

                for (int i = 0; i < 5; i++)
                {
                    var_53.field_12D[0, i] = 0;
                    var_53.field_12D[1, i] = 0;
                    var_53.field_12D[2, i] = 0;
                }

                for (loop4_var = 0; loop4_var <= 7; loop4_var++)
                {
                    if (var_53.Skill_A_lvl[loop4_var] > 0)
                    {
                        if (loop4_var == 0)
                        {
                            var_53.field_12D[0, 0] = 1;
                        }
                        else if (loop4_var == 5)
                        {
                            var_53.field_12D[2, 0] = 1;
                        }

                        var_21 += ovr024.roll_dice(unk_1A8C4[loop4_var], unk_1A8C3[loop4_var]);

                        if (loop4_var == 0)
                        {
                            ovr026.calc_cleric_spells(false, player);

                            for (int i = 1; i <= 100; i++)
                            {
                                Struct_19AEC stru = gbl.unk_19AEC[i];

                                if (stru.spellClass == 0 &&
                                    stru.spellLevel == 1)
                                {
                                    var_53.field_79[i - 1] = 1;
                                }
                            }
                        }
                        else if (loop4_var == 5)
                        {
                            var_53.field_79[0xB - 1] = 1;
                            var_53.field_79[0x12 - 1] = 1;
                            var_53.field_79[0xC - 1] = 1;
                            var_53.field_79[0x15 - -1] = 1;
                        }

                        var_20++;
                    }
                }

                var_53.platinum = 300;
                var_53.field_12C = sub_509E0(0xff, player);
                var_53.hit_point_max = var_53.field_12C;

                var_1E = get_con_hp_adj(player);

                if (var_1E < 0)
                {
                    if (var_53.hit_point_max > (System.Math.Abs(var_1E) + var_20))
                    {
                        var_53.hit_point_max = (byte)((var_53.hit_point_max + var_1E) / var_20);
                    }
                    else
                    {
                        var_53.hit_point_max = 1;
                    }
                }
                else
                {
                    var_53.hit_point_max = (byte)((var_53.hit_point_max + var_1E) / var_20);
                }

                var_53.hit_point_current = var_53.hit_point_max;
                var_53.field_12C = (byte)(var_53.field_12C / var_20);
                var_4F = gbl.area2_ptr.field_550;
                gbl.area2_ptr.field_550 = 0xff;
                gbl.byte_1D8B0 = 0;
                gbl.byte_1B2F1 = 1;

                do
                {
                    train_player();
                } while (gbl.byte_1D8B0 == 0);

                gbl.area2_ptr.field_550 = var_4F;
                gbl.byte_1B2F1 = 0;
                var_25 = 0;
                var_4E = string.Empty;

                for (loop4_var = 0; loop4_var <= 7; loop4_var++)
                {
                    if (gbl.player_ptr.Skill_A_lvl[loop4_var] > 0 ||
                        (gbl.player_ptr.Skill_B_lvl[loop4_var] < ovr026.hasAnySkills(gbl.player_ptr) &&
                         gbl.player_ptr.Skill_B_lvl[loop4_var] > 0))
                    {
                        if (var_25 != 0)
                        {
                            var_4E += "/";
                        }

                        byte b = gbl.player_ptr.Skill_B_lvl[loop4_var];
                        b += gbl.player_ptr.Skill_A_lvl[loop4_var];

                        var_4E += b.ToString();

                        var_25 = 1;
                    }
                }

                seg041.displayString(var_4E, 0, 15, 15, 7);
                ovr020.display_player_stats01();
                ovr020.displayMoney();

                reroll_stats = ovr027.yes_no(15, 10, 13, "Reroll stats? ");

            } while (reroll_stats != 'N');

            ovr020.playerDisplayFull();

            do
            {
                player.name = seg041.getUserInputString(15, 0, 13, "Character name: ");
            } while (player.name.Length == 0);

            icon_builder();

            for (var_1B = 0; var_1B <= 5; var_1B++)
            {
                player.stats[var_1B].tmp = player.stats[var_1B].max;
            }

            player.field_1D = player.strength_18_100;

            reroll_stats = ovr027.yes_no(15, 10, 13, "Save " + player.name + "? ");

            if (reroll_stats == 'N')
            {
                seg051.FreeMem(gbl.char_struct_size, player);
                gbl.player_ptr = var_8;
            }
            else
            {
                ovr017.sub_47DFC(string.Empty, player);

                seg051.FreeMem(gbl.char_struct_size, player);
                gbl.player_ptr = var_8;
            }
        }


        internal static sbyte con_bonus(ClassId classId)
        {
            sbyte bonus;
            int stat = gbl.player_ptr.con;

            if (stat == 3)
            {
                bonus = -2;
            }
            else if (stat >= 4 && stat <= 6)
            {
                bonus = -1;
            }
            else if (stat >= 7 && stat <= 14)
            {
                bonus = 0;
            }
            else if (stat == 15)
            {
                bonus = 1;
            }
            else if (stat == 16)
            {
                bonus = 1;
            }
            else if (classId == ClassId.fighter || classId == ClassId.ranger || classId == ClassId.paladin)
            {
                bonus = (sbyte)(stat - 14);
            }
            else
            {
                bonus = 2;
            }

            return bonus;
        }


        internal static void dropPlayer()
        {
            Player player;

            if (gbl.player_ptr != null)
            {
                player = gbl.player_ptr;

                if (ovr027.yes_no(15, 10, 14, "Drop " + player.name + " forever? ") == 'Y' &&
                    ovr027.yes_no(15, 10, 14, "Are you sure? ") == 'Y')
                {
                    if (player.in_combat == false)
                    {
                        ovr025.string_print01("You dump " + player.name + " out back.");
                    }
                    else
                    {
                        ovr025.string_print01(player.name + " bids you farewell.");
                    }

                    ovr017.remove_player_file(gbl.player_ptr);
                    free_players(1, false);
                }
                else
                {
                    ovr025.string_print01(player.name + " breathes a sigh of relief.");
                }
            }

            ovr025.Player_Summary(gbl.player_ptr);
        }

        /// <summary>
        /// nested function, has not been fix to be not nested.
        /// </summary>
        internal static void draw_highlight_stat(bool arg_2, byte edited_stat, byte name_cursor_pos) /* sub_4E6F2 */
        {
            if (edited_stat >= 0 && edited_stat <= 5)
            {
                ovr020.display_stat(arg_2, edited_stat);
            }
            else if (edited_stat == 6)
            {
                ovr025.display_hp(arg_2, 18, 4, gbl.player_ptr);
            }
            else if (edited_stat == 7)
            {
                if (arg_2 == true)
                {
                    seg041.displaySpaceChar(1, 0, 1, gbl.player_ptr.name.Length + 1);
                    seg041.displayString(gbl.player_ptr.name, 0, 13, 1, 1);

                    if (name_cursor_pos > gbl.player_ptr.name.Length || gbl.player_ptr.name[name_cursor_pos-1] == ' ')
                    {
                        seg041.displayString("%", 0, 15, 1, name_cursor_pos);
                    }
                    else
                    {
                        seg041.displayString(gbl.player_ptr.name[name_cursor_pos-1].ToString(), 0, 15, 1, name_cursor_pos);
                    }
                }
                else
                {
                    seg041.displayString(gbl.player_ptr.name, 0, 10, 1, 1);
                }
            }
        }


        internal static void modifyPlayer()
        {
            byte var_45;
            Player player_ptr;
            byte var_40;
            byte name_cursor_pos;
            byte edited_stat;
            bool var_36;
            char var_35;
            byte stat_var;
            byte var_33;
            string var_31;
            byte var_8;
            byte var_7;
            byte[] var_6 = new byte[6];

            if (Cheats.allow_player_modify == false &&
                (gbl.player_ptr.exp != 0 &&
                gbl.player_ptr.exp != 8333 &&
                gbl.player_ptr.exp != 12500 &&
                gbl.player_ptr.exp != 25000) ||
                gbl.player_ptr.field_E6 != 0)
            {
                seg041.DisplayStatusText(0, 14, gbl.player_ptr.name + " can't be modified.");
                return;
            }

            ovr020.playerDisplayFull();

            for (stat_var = 0; stat_var <= 5; stat_var++)
            {
                var_6[stat_var] = gbl.player_ptr.stats[stat_var].max;
            }

            var_7 = gbl.player_ptr.strength_18_100;
            var_8 = gbl.player_ptr.hit_point_max;

            var_31 = gbl.player_ptr.name;

            name_cursor_pos = 1;
            edited_stat = 7;
            draw_highlight_stat(false, edited_stat, name_cursor_pos);
            edited_stat = 0;
            draw_highlight_stat(true, edited_stat, name_cursor_pos);
            player_ptr = gbl.player_ptr;

            do
            {
                if (edited_stat == 7)
                {
                    while (seg049.KEYPRESSED() == false)
                    {
                        /* empty */
                    }

                    var_35 = (char)seg043.GetInputKey();

                    if (var_35 == 0)
                    {
                        var_35 = (char)seg043.GetInputKey();
                        var_36 = true;
                    }
                    else
                    {
                        var_36 = false;
                    }

                    if (var_35 == 0x1B)
                    {
                        var_35 = '\0';
                    }
                }
                else
                {
                    var_35 = ovr027.displayInput(out var_36, false, 1, 15, 10, 13, "Keep Exit", "Modify: ");
                }

                draw_highlight_stat(false, edited_stat, name_cursor_pos);

                if (var_36 == true)
                {
                    switch (var_35)
                    {
                        case 'S':
                            if (edited_stat == 7 && gbl.player_ptr.name.Length > 1)
                            {
                                if (name_cursor_pos == gbl.player_ptr.name.Length)
                                {
                                    throw new System.NotSupportedException();
                                    //gbl.player_ptr.name.Length -= 1;
                                    //var_3F = gbl.player_ptr.name.Length;
                                }
                                else
                                {
                                    var_45 = (byte)(gbl.player_ptr.name.Length - 1);

                                    for (var_40 = name_cursor_pos; var_40 <= var_45; var_40++)
                                    {

                                        throw new System.NotSupportedException();//mov	al, [bp+var_40]
                                        throw new System.NotSupportedException();//xor	ah, ah
                                        throw new System.NotSupportedException();//inc	ax
                                        throw new System.NotSupportedException();//les	di, int ptr player_ptr.offset
                                        throw new System.NotSupportedException();//add	di, ax
                                        throw new System.NotSupportedException();//mov	dl, es:[di]
                                        throw new System.NotSupportedException();//mov	al, [bp+var_40]
                                        throw new System.NotSupportedException();//xor	ah, ah
                                        throw new System.NotSupportedException();//les	di, int ptr player_ptr.offset
                                        throw new System.NotSupportedException();//add	di, ax
                                        throw new System.NotSupportedException();//mov	es:[di], dl
                                    }
                                    throw new System.NotSupportedException();
                                    //gbl.player_ptr.name.Length -= 1; 
                                }
                            }
                            break;

                        case 'O':
                            edited_stat++;

                            if (edited_stat > 7)
                            {
                                edited_stat = 0;
                            }
                            break;

                        case 'G':
                            edited_stat -= 1;

                            if (edited_stat == 0xff)
                            {
                                edited_stat = 7;
                            }
                            break;

                        case 'K':
                            if (edited_stat < 6)
                            {
                                stat_var = edited_stat;
                                player_ptr.stats[stat_var].max -= 1;

                                switch ((Stat)stat_var)
                                {
                                    case Stat.STR:
                                        if (player_ptr.strength_18_100 > 0)
                                        {
                                            player_ptr.strength_18_100 -= 1;

                                            player_ptr.stats[stat_var].max += 1;
                                        }
                                        else
                                        {
                                            if (player_ptr.stats[stat_var].max < stru_1A298[(int)player_ptr.race].str_min[player_ptr.sex])
                                            {
                                                player_ptr.stats[stat_var].max = stru_1A298[(int)player_ptr.race].str_min[player_ptr.sex];
                                            }
                                        }

                                        if (player_ptr.stats[stat_var].max < gbl.unk_1A484[(int)player_ptr._class].field_0)
                                        {
                                            player_ptr.stats[stat_var].max = gbl.unk_1A484[(int)player_ptr._class].field_0;
                                        }
                                        break;

                                    case Stat.INT:
                                        if (player_ptr.stats[stat_var].max < stru_1A298[(int)player_ptr.race].int_min)
                                        {
                                            player_ptr.stats[stat_var].max = stru_1A298[(int)player_ptr.race].int_min;
                                        }

                                        if (player_ptr.stats[stat_var].max < gbl.unk_1A484[(int)player_ptr._class].field_1)
                                        {
                                            player_ptr.stats[stat_var].max = gbl.unk_1A484[(int)player_ptr._class].field_1;
                                        }
                                        break;

                                    case Stat.WIS:
                                        if (player_ptr.stats[stat_var].max < stru_1A298[(int)player_ptr.race].field_8)
                                        {
                                            player_ptr.stats[stat_var].max = stru_1A298[(int)player_ptr.race].field_8;
                                        }

                                        if (player_ptr.stats[stat_var].max < gbl.unk_1A484[(int)player_ptr._class].field_2)
                                        {
                                            player_ptr.stats[stat_var].max = gbl.unk_1A484[(int)player_ptr._class].field_2;
                                        }
                                        
                                        if (player_ptr.field_12D[0,0] > 0)
                                        {
                                            player_ptr.field_12D[0,0] = 1;
                                        }
                                        break;

                                    case Stat.DEX:
                                        if (player_ptr.stats[stat_var].max < stru_1A298[(int)player_ptr.race].field_A)
                                        {
                                            player_ptr.stats[stat_var].max = stru_1A298[(int)player_ptr.race].field_A;
                                        }

                                        if (player_ptr.stats[stat_var].max < gbl.unk_1A484[(int)player_ptr._class].field_3)
                                        {
                                            player_ptr.stats[stat_var].max = gbl.unk_1A484[(int)player_ptr._class].field_3;
                                        }
                                        break;

                                    case Stat.CON:
                                        if (player_ptr.stats[stat_var].max < stru_1A298[(int)player_ptr.race].field_C)
                                        {
                                            player_ptr.stats[stat_var].max = stru_1A298[(int)player_ptr.race].field_C;
                                        }

                                        if (player_ptr.stats[stat_var].max < gbl.unk_1A484[(int)player_ptr._class].field_4)
                                        {

                                            player_ptr.stats[stat_var].max = gbl.unk_1A484[(int)player_ptr._class].field_4;
                                        }

                                        if (sub_50793(gbl.player_ptr) < player_ptr.hit_point_max)
                                        {
                                            player_ptr.hit_point_max = sub_50793(player_ptr);
                                        }

                                        player_ptr.hit_point_current = player_ptr.hit_point_max;
                                        edited_stat = 6;
                                        draw_highlight_stat(false, edited_stat, name_cursor_pos);
                                        edited_stat = 4;

                                        break;

                                    case Stat.CHA:
                                        if (player_ptr.stats[stat_var].max < stru_1A298[(int)player_ptr.race].field_E)
                                        {
                                            player_ptr.stats[stat_var].max = stru_1A298[(int)player_ptr.race].field_E;
                                        }

                                        if (player_ptr.stats[stat_var].max < gbl.unk_1A484[(int)player_ptr._class].field_5)
                                        {
                                            player_ptr.stats[stat_var].max = gbl.unk_1A484[(int)player_ptr._class].field_5;
                                        }
                                        break;
                                }
                            }
                            else if (edited_stat == 6)
                            {
                                player_ptr.hit_point_max -= 1;

                               if (sub_506BA(gbl.player_ptr) > player_ptr.hit_point_max)
                                {
                                    player_ptr.hit_point_max = sub_506BA(player_ptr);
                                }

                                player_ptr.hit_point_current = player_ptr.hit_point_max; ;
                            }
                            else
                            {
                                if (name_cursor_pos == 1)
                                {
                                    name_cursor_pos = (byte)gbl.player_ptr.name.Length;
                                }
                                else
                                {
                                    name_cursor_pos -= 1;
                                }
                            }
                            break;

                        case 'M':
                            if (edited_stat < 6)
                            {
                                stat_var = edited_stat;

                                player_ptr.stats[stat_var].max += 1;
                                switch ((Stat)stat_var)
                                {
                                    case Stat.STR:
                                        if (player_ptr.stats[stat_var].max > stru_1A298[(int)player_ptr.race].str_max[player_ptr.sex])
                                        {
                                            if( player_ptr.stats[stat_var].max > 18 &&
                                                (player_ptr.fighter_lvl > 0 || player_ptr.paladin_lvl > 0 || player_ptr.ranger_lvl > 0 ) &&
                                                stru_1A298[(int)player_ptr.race].str_100_max[player_ptr.sex] > player_ptr.strength_18_100 )
                                            {
                                                player_ptr.strength_18_100 += 1;
                                            }

                                            player_ptr.stats[stat_var].max = stru_1A298[(int)player_ptr.race].str_max[player_ptr.sex];
                                        }
                                        break;

                                    case Stat.INT:
                                        if (player_ptr.stats[stat_var].max > stru_1A298[(int)player_ptr.race].int_max)
                                        {
                                            player_ptr.stats[stat_var].max = stru_1A298[(int)player_ptr.race].int_max;
                                        }
                                        break;

                                    case Stat.WIS:
                                        if (player_ptr.stats[stat_var].max > stru_1A298[(int)player_ptr.race].field_9)
                                        {
                                            player_ptr.stats[stat_var].max = stru_1A298[(int)player_ptr.race].field_9;
                                        }

                                        if (player_ptr.field_12D[0,0] > 0)
                                        {
                                            player_ptr.field_12D[0,0] = 1;
                                        }
                                        break;

                                    case Stat.DEX:
                                        if (player_ptr.stats[stat_var].max > stru_1A298[(int)player_ptr.race].field_B)
                                        {
                                            player_ptr.stats[stat_var].max = stru_1A298[(int)player_ptr.race].field_B;
                                        }
                                        break;

                                    case Stat.CON:
                                        if (player_ptr.stats[stat_var].max > stru_1A298[(int)player_ptr.race].field_D)
                                        {
                                            player_ptr.stats[stat_var].max = stru_1A298[(int)player_ptr.race].field_D;
                                        }

                                        if (sub_506BA(gbl.player_ptr) > player_ptr.hit_point_max)
                                        {
                                            player_ptr.hit_point_max = sub_506BA(player_ptr);
                                        }

                                        player_ptr.hit_point_current = player_ptr.hit_point_max;
                                        edited_stat = 6;
                                        draw_highlight_stat(false, edited_stat, name_cursor_pos);
                                        edited_stat = 4;
                                        break;

                                    case Stat.CHA:
                                        if (player_ptr.stats[stat_var].max > stru_1A298[(int)player_ptr.race].field_F)
                                        {
                                            player_ptr.stats[stat_var].max = stru_1A298[(int)player_ptr.race].field_F;
                                        }
                                        break;
                                }
                            }
                            else
                            {
                                if (edited_stat == 6)
                                {
                                    player_ptr.hit_point_max += 1;

                                    if (sub_50793(gbl.player_ptr) < player_ptr.hit_point_max)
                                    {
                                        player_ptr.hit_point_max = sub_50793(gbl.player_ptr);
                                    }

                                    player_ptr.hit_point_current = player_ptr.hit_point_max;
                                }
                                else
                                {
                                    if (name_cursor_pos == player_ptr.name.Length+1)
                                    {
                                        name_cursor_pos = 1;
                                    }
                                    else
                                    {
                                        name_cursor_pos++;
                                    }
                                }
                            }
                            break;
                    }
                }
                else
                {
                    if (var_35 == 0x0d)
                    {
                        edited_stat++;

                        if (edited_stat > 7)
                        {
                            edited_stat = 0;
                        }
                    }
                    else if (var_35 == 0x08)
                    {
                        if (name_cursor_pos > 1 && edited_stat > 6)
                        {
                            int len = gbl.player_ptr.name.Length;
                            int del = name_cursor_pos - 1;

                             
                            /* delete char from name */
                            string s = string.Empty;
                            if (del > 0)
                            {
                                s = gbl.player_ptr.name.Substring(0, del);
                            }

                            if ((len - del) > 0)
                            {
                                s += gbl.player_ptr.name.Substring(del+1);
                            }

                            gbl.player_ptr.name = s;

                            if (name_cursor_pos > gbl.player_ptr.name.Length)
                            {
                                name_cursor_pos = (byte)gbl.player_ptr.name.Length;
                            }
                        }
                    }
                    else if (var_35 >= 0x20 && var_35 <= 0x7A)
                    {
                        if (edited_stat > 6)
                        {
                            if (name_cursor_pos <= 15)
                            {
                                string s = string.Empty;
                                int len = player_ptr.name.Length;
                                int insert = name_cursor_pos - 1;

                                if (insert > 0)
                                {
                                    s = player_ptr.name.Substring(0, insert);
                                }
                                s += var_35;
                                if (len - insert > 0)
                                {
                                    s += player_ptr.name.Substring(insert + 1);
                                }

                                player_ptr.name = s;

                                name_cursor_pos++;
                                if (name_cursor_pos > 15)
                                {
                                    name_cursor_pos = 15;
                                }

                                if (name_cursor_pos > player_ptr.name.Length)
                                {
                                   player_ptr.name.PadRight(name_cursor_pos, ' ');
                                }
                                var_35 = '\0';
                            }
                        }
                        else if( var_35 == 0x45 )
                        {
                            for (stat_var = 0; stat_var <= 5; stat_var++)
                            {
                                player_ptr.stats[stat_var].max = var_6[stat_var];
                            }

                            gbl.player_ptr.strength_18_100 = var_7;
                            gbl.player_ptr.hit_point_max = var_8;
                            gbl.player_ptr.hit_point_current = gbl.player_ptr.hit_point_max;

                            gbl.player_ptr.name = var_31;

                            ovr025.sub_66C20(gbl.player_ptr);
                            return;
                        }
                    }
                    else if (var_35 == 0)
                    {

                        for (stat_var = 0; stat_var <= 5; stat_var++)
                        {
                            gbl.player_ptr.stats[stat_var].max = var_6[stat_var];
                        }

                        gbl.player_ptr.strength_18_100 = var_7;
                        gbl.player_ptr.hit_point_max = var_8;
                        gbl.player_ptr.name = var_31;

                        gbl.player_ptr.hit_point_current = gbl.player_ptr.hit_point_max;
                        ovr025.sub_66C20(gbl.player_ptr);
                        return;

                    }
                }

                ovr025.sub_66C20(gbl.player_ptr);
                ovr020.display_player_stats01();

                draw_highlight_stat(true, edited_stat, name_cursor_pos);
            } while (var_36 == true || var_35 != 0x4B);

            ovr026.calc_cleric_spells(true, gbl.player_ptr);

            gbl.player_ptr.field_F8 = 1;

            player_ptr = gbl.player_ptr;
            var_8 = 0;
            var_40 = 0;

            for (var_33 = 0; var_33 <= 7; var_33++)
            {
                if (player_ptr.Skill_A_lvl[var_33] > 0)
                {
                    if (player_ptr.Skill_A_lvl[var_33] < gbl.byte_1A1CB[var_33])
                    {
                        if ((ClassId)var_33 == ClassId.ranger)
                        {
                            var_8 += (byte)((player_ptr.Skill_A_lvl[var_33] + 1) * (con_bonus((ClassId)var_33)));
                        }
                        else
                        {
                            var_8 += (byte)(player_ptr.Skill_A_lvl[var_33] * (con_bonus((ClassId)var_33)));
                        }
                    }
                    else
                    {
                        var_8 += (byte)((gbl.byte_1A1CB[var_33] - 1) * con_bonus((ClassId)var_33));
                    }
                    var_40++;
                }
            }

            var_8 /= var_40;

            player_ptr.field_12C = (byte)(player_ptr.hit_point_max - var_8);

            for (stat_var = 0; stat_var <= 5; stat_var++)
            {
                gbl.player_ptr.stats[stat_var].tmp = gbl.player_ptr.stats[stat_var].max;
            }

            gbl.player_ptr.strength_18_100 = gbl.player_ptr.field_1D;
        }


        internal static void AddPlayer()
        {
            bool var_4B;
            string var_4A = string.Empty;
            byte var_21;
            byte var_20;
            byte var_1F;
            byte var_1E = 0; /* Simeon */
            bool var_1D;
            bool showExit = true; /* Simeon */
            char var_1B;
            short strList_index;
            Player player_ptr2;
            Player player_ptr1;
            StringList var_10;
            StringList var_C;
            StringList strList;
            StringList var_4;

            var_20 = 0;
            var_21 = 0;
            var_1F = 0;
            seg037.draw8x8_clear_area(0x16, 0x26, 1, 1);

            var_1B = ovr027.displayInput(out var_4B, false, 0, 15, 10, 13, "Curse Pool Hillsfar Exit", "Add from where? ");

            switch (var_1B)
            {
                case 'C':
                    gbl.import_from = 0;
                    break;

                case 'P':
                    gbl.import_from = 1;
                    break;

                case 'H':
                    gbl.import_from = 2;
                    break;

                case 'E':
                    goto case '\0';

                case '\0':
                    return;
            }

            ovr017.sub_47465(out strList, out var_4);

            if (var_4 != null)
            {
                strList_index = 0;
                var_C = var_4;
                var_1D = true;

                do
                {
                    var_1B = ovr027.sl_select_item(out var_C, ref strList_index, ref var_1D, showExit, var_4,
                        22, 38, 2, 1, 15, 10, 13, "Add", "Add a character: ");

                    if ((var_1B == 13 || var_1B == 0x41) &&
                        var_C.s[0] != 0x2A)
                    {
                        ovr027.redraw_screen();

                        player_ptr1 = new Player();

                        var_10 = ovr027.getStringListEntry(strList, strList_index);

                        ovr017.import_char01(1, 0, ref player_ptr1, var_10.s);

                        var_C.s = "* " + var_C.s;
                        var_1E = 0;

                        if (gbl.player_next_ptr == null)
                        {
                            gbl.area2_ptr.field_67C = 0;
                            ovr017.sub_4A60A(player_ptr1);

                            ovr017.LoadPlayerCombatIcon(true);
                        }
                        else
                        {
                            player_ptr2 = gbl.player_next_ptr;
                            var_1F = 0;

                            while (player_ptr2 != null &&
                                (player_ptr2.name != player_ptr1.name || player_ptr2.field_126 != player_ptr1.field_126))
                            {
                                if (player_ptr2.field_F7 < 0x80)
                                {
                                    var_1E++;
                                }

                                if (player_ptr2.ranger_lvl > 0)
                                {
                                    var_1F++;
                                }

                                if ((player_ptr2.alignment + 1) % 3 == 0)
                                {
                                    var_20 = 1;
                                }

                                if (player_ptr2.paladin_lvl > 0)
                                {
                                    var_21 = 1;
                                    var_4A = player_ptr2.name;
                                }

                                player_ptr2 = player_ptr2.next_player;
                            }

                            if (player_ptr2 == null &&
                                ((player_ptr1.field_F7 < 0x80 && var_1E < 6) ||
                                    (player_ptr1.field_F7 > 0x7F && gbl.area2_ptr.field_67C < 8)) &&
                                (player_ptr1.paladin_lvl == 0 || var_20 == 0) &&
                                (player_ptr1.ranger_lvl == 0 || var_1F < 3) &&
                                (((player_ptr1.alignment + 1) % 3) != 0 || var_21 == 0))
                            {
                                ovr017.sub_4A60A(player_ptr1);
                                ovr017.LoadPlayerCombatIcon(true);

                                if (player_ptr1.field_F7 < 0x80)
                                {
                                    var_1E++;
                                }
                            }
                            else
                            {
                                seg051.Delete(2, 1, ref var_C.s);

                                if (player_ptr1.paladin_lvl > 0 && var_20 != 0)
                                {
                                    ovr025.string_print01("paladins do not join with evil scum");
                                    seg041.GameDelay();
                                }
                                else if (player_ptr1.ranger_lvl > 0 && var_1F > 2)
                                {
                                    ovr025.string_print01("too many rangers in party");
                                }
                                else if (((player_ptr1.alignment + 1) % 3) == 0 &&
                                        var_21 != 0)
                                {
                                    ovr025.string_print01(var_4A + " will tolerate no evil!");
                                }

                                player_ptr1 = null; // FreeMem( char_struct_size, player_ptr1 );
                            }
                        }
                    }

                } while (var_1B != 0x45 && var_1B != '\0' && var_1E <= 5 && gbl.area2_ptr.field_67C <= 7);

                ovr027.free_stringList(ref var_4);
            }
        }


        internal static void free_players(byte arg_0, bool arg_2)
        {
            Player player_ptr;

            if (gbl.player_next_ptr != null)
            {
                player_ptr = gbl.player_next_ptr;

                if (player_ptr != gbl.player_ptr)
                {
                    while (player_ptr.next_player != gbl.player_ptr && player_ptr.next_player != null)
                    {
                        player_ptr = player_ptr.next_player;
                    }

                    if (player_ptr.next_player != null)
                    {
                        if (arg_0 != 0)
                        {
                            ovr034.free_icon(gbl.player_ptr.icon_id);
                        }

                        player_ptr.next_player = gbl.player_ptr.next_player;

                        free_player(ref gbl.player_ptr);
                        gbl.player_ptr = player_ptr;

                        if (arg_2 == false)
                        {
                            gbl.area2_ptr.field_67C--;
                        }
                    }
                }
                else
                {
                    if (arg_0 != 0)
                    {
                        ovr034.free_icon(gbl.player_ptr.icon_id);
                    }

                    gbl.player_next_ptr = gbl.player_ptr.next_player;
                    player_ptr = gbl.player_ptr.next_player;

                    free_player(ref gbl.player_ptr);
                    gbl.player_ptr = player_ptr;

                    if (arg_2 == false)
                    {
                        gbl.area2_ptr.field_67C--;
                    }
                }

                if (gbl.player_ptr == null)
                {
                    gbl.player_ptr = gbl.player_next_ptr;
                }
            }
        }


        internal static void drawIconEditorIcons(sbyte titleY, sbyte titleX) /* sub_4FB7C */
        {
            seg040.DrawColorBlock( 0, 24, 12, titleY * 24, titleX * 3);

            ovr034.draw_combat_icon(25, 0, 0, titleY, titleX);
            ovr034.draw_combat_icon(25, 1, 0, titleY, titleX + 3);

            ovr034.draw_combat_icon(12, 0, 0, titleY, titleX);
            ovr034.draw_combat_icon(12, 1, 0, titleY, titleX + 3);

            seg040.DrawOverlay();
        }


        internal static void duplicateCombatIcon(bool recolour, byte destIndex, byte sourceIndex) /* sub_4FC5B */
        {
            byte[] newColors = new byte[16];
            byte[] oldColors = new byte[16];

            short bitPerPixel = gbl.combat_icons[destIndex, 0].bpp;

            System.Array.Copy(gbl.combat_icons[sourceIndex, 0].data, gbl.combat_icons[destIndex, 0].data, gbl.combat_icons[sourceIndex, 0].data.Length);
            System.Array.Copy(gbl.combat_icons[sourceIndex, 1].data, gbl.combat_icons[destIndex, 1].data, gbl.combat_icons[sourceIndex, 1].data.Length);

            System.Array.Copy(gbl.combat_icons[sourceIndex, 0].data_ptr, gbl.combat_icons[destIndex, 0].data_ptr, gbl.combat_icons[sourceIndex, 0].data_ptr.Length);
            System.Array.Copy(gbl.combat_icons[sourceIndex, 1].data_ptr, gbl.combat_icons[destIndex, 1].data_ptr, gbl.combat_icons[sourceIndex, 1].data_ptr.Length);

            if (recolour)
            {
                Player var_28 = gbl.player_ptr;

                for (byte i = 0; i < 16; i++)
                {
                    oldColors[i] = i;
                    newColors[i] = i;
                }

                for (byte i = 0; i < 6; i++)
                {
                    newColors[gbl.unk_1A1D3[i]] = (byte)(var_28.field_145[i] & 0x0F);
                    newColors[gbl.unk_1A1D3[i] + 8] = (byte)((var_28.field_145[i] & 0xF0) >> 4);
                }

                seg040.DaxBlockRecolor(gbl.combat_icons[destIndex, 0], 0, 0, newColors, oldColors);
                seg040.DaxBlockRecolor(gbl.combat_icons[destIndex, 1], 0, 0, newColors, oldColors);
            }
        }

        static Set unk_4FE94 = new Set( 0x0009, new byte [] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20 });

        internal static void icon_builder()
        {
            Player player_ptr2;
            Player player_ptr;
            char var_1B = '\0'; /* Simeon */
            byte var_1A = 0; /* Simeon */
            byte var_19;
            byte var_18;
            byte var_17 = 0; /* Simeon */
            byte var_16 = 0; /* Simeon */
            byte[] var_15;
            string var_E;
            byte var_8;
            byte var_7;
            byte var_6;
            byte var_5;
            byte var_4;
            bool var_3;
            char var_2;

            string[] iconStrings = {   "", 
									   "Parts 1st-color 2nd-color Size Exit", 
									   "Head Weapon Exit", 
									   "Weapon Body xxxx Shield Arm Leg Exit", 
									   " Keep Exit", 
									   "Next Prev Keep Exit" };

            seg037.draw8x8_outer_frame();
            ovr033.Color_0_8_inverse();

            do
            {
                ovr017.LoadPlayerCombatIcon(false);

                player_ptr = gbl.player_ptr;

                var_8 = 1;

                var_15 = player_ptr.field_145;

                var_7 = player_ptr.icon_id;
                player_ptr.icon_id = 0x0C;
                ovr017.LoadPlayerCombatIcon(false);
                player_ptr.icon_id = var_7;
                var_4 = player_ptr.field_141;
                var_5 = player_ptr.field_142;
                var_6 = player_ptr.icon_size;

                duplicateCombatIcon(true, 12, player_ptr.icon_id);
                drawIconEditorIcons(2, 1);

                seg041.displayString("old", 0, 15, 6, 8);
                seg041.displayString("ready   action", 0, 15, 10, 3);
                seg041.displayString("new", 0, 15, 12, 8);
                seg041.displayString("ready   action", 0, 15, 16, 3);

                do
                {

                    drawIconEditorIcons(4, 1);

                    if (var_8 == 4)
                    {
                        if (player_ptr.icon_size == 2)
                        {
                            var_E = "Small" + iconStrings[var_8];
                        }
                        else
                        {
                            var_E = "Large" + iconStrings[var_8];
                        }
                    }
                    else
                    {
                        var_E = iconStrings[var_8];
                    }

                    var_2 = ovr027.displayInput(out var_3, false, 0, 15, 10, 13, var_E, string.Empty);

                    if (var_3 == false)
                    {
                        switch (var_8)
                        {
                            case 1:
                                var_1A = 1;

                                switch (var_2)
                                {
                                    case 'P':
                                        var_8 = 2;
                                        break;

                                    case '1':
                                        var_8 = 3;
                                        var_17 = 0;

                                        iconStrings[3] = "Weapon Body Hair Shield Arm Leg Exit";
                                        break;

                                    case '2':
                                        var_8 = 3;
                                        var_17 = 1;

                                        iconStrings[3] = "Weapon Body Face Shield Arm Leg Exit";
                                        break;

                                    case 'S':
                                        var_8 = 4;
                                        break;

                                    case 'E':
                                        var_1A = 0;
                                        break;
                                }

                                break;

                            case 2:
                                var_1A = 2;
                                if (unk_4FE94.MemberOf(var_2) == true)
                                {
                                    var_8 = 1;
                                }
                                else
                                {
                                    var_1B = var_2;
                                    var_8 = 5;
                                }
                                break;

                            case 3:
                                var_1A = 3;

                                switch (var_2)
                                {
                                    case 'W':
                                        var_16 = 6;
                                        break;

                                    case 'B':
                                        var_16 = 1;
                                        break;

                                    case 'H':
                                        var_16 = 4;
                                        break;

                                    case 'F':
                                        var_16 = 4;
                                        break;

                                    case 'S':
                                        var_16 = 5;
                                        break;

                                    case 'A':
                                        var_16 = 2;
                                        break;

                                    case 'L':
                                        var_16 = 3;
                                        break;

                                    default:
                                        var_16 = 1;
                                        break;
                                }

                                if (unk_4FE94.MemberOf(var_2) == true)
                                {
                                    var_8 = 1;
                                }
                                else
                                {
                                    var_8 = 5;
                                }
                                break;

                            case 4:
                                switch (var_2)
                                {
                                    case 'L':
                                        player_ptr.icon_size = 2;
                                        ovr017.LoadPlayerCombatIcon(false);
                                        break;

                                    case 'S':
                                        player_ptr.icon_size = 1;
                                        ovr017.LoadPlayerCombatIcon(false);
                                        break;

                                    case 'K':
                                        var_6 = player_ptr.icon_size;
                                        var_8 = 1;
                                        var_2 = ' ';
                                        break;

                                    case 'E':
                                        goto case '\0';

                                    case '\0':
                                        player_ptr.icon_size = var_6;
                                        var_8 = 1;
                                        var_2 = ' ';
                                        break;
                                }

                                ovr017.LoadPlayerCombatIcon(false);
                                break;

                            case 5:
                                if (var_1A == 2)
                                {
                                    if (var_1B == 0x48)
                                    {
                                        if (var_2 == 0x50)
                                        {
                                            if (player_ptr.field_141 > 0)
                                            {
                                                player_ptr.field_141 -= 1;
                                            }
                                            else
                                            {
                                                player_ptr.field_141 = 13;
                                            }
                                        }
                                        else if (var_2 == 0x4E)
                                        {
                                            if (player_ptr.field_141 < 13)
                                            {
                                                player_ptr.field_141 += 1;
                                            }
                                            else
                                            {

                                                player_ptr.field_141 = 0;
                                            }
                                        }
                                        else if (var_2 == 0x4B)
                                        {
                                            player_ptr2 = gbl.player_ptr;
                                            var_4 = player_ptr2.field_141;
                                            var_8 = var_1A;
                                            var_2 = ' ';
                                        }
                                        else if (var_2 == 0x45 || var_2 == '\0')
                                        {
                                            player_ptr.field_141 = var_4;
                                            var_8 = var_1A;
                                            var_2 = ' ';
                                        }

                                        ovr017.LoadPlayerCombatIcon(false);
                                    }
                                    else if (var_1B == 0x57)
                                    {
                                        if (var_2 == 0x50)
                                        {
                                            if (player_ptr.field_142 > 0)
                                            {
                                                player_ptr.field_142 -= 1;
                                            }
                                            else
                                            {
                                                player_ptr.field_142 = 0x1F;
                                            }
                                        }
                                        else if (var_2 == 0x4E)
                                        {
                                            if (player_ptr.field_142 < 0x1F)
                                            {
                                                player_ptr.field_142 += 1;
                                            }
                                            else
                                            {
                                                player_ptr.field_142 = 0;
                                            }
                                        }
                                        else if (var_2 == 0x4B)
                                        {
                                            player_ptr2 = gbl.player_ptr;
                                            var_5 = player_ptr2.field_142;
                                            var_8 = var_1A;
                                            var_2 = ' ';
                                        }
                                        else if (var_2 == 0x45 || var_2 == '\0')
                                        {
                                            player_ptr.field_142 = var_5;
                                            var_8 = var_1A;
                                            var_2 = ' ';
                                        }

                                        ovr017.LoadPlayerCombatIcon(false);
                                    }
                                }
                                else if (var_1A == 3)
                                {
                                    var_18 = (byte)(player_ptr.field_145[var_16 - 1] & 0x0F);
                                    var_19 = (byte)((player_ptr.field_145[var_16 - 1] & 0xF0) >> 4);

                                    if (var_2 == 0x4E)
                                    {
                                        if (var_17 != 0)
                                        {
                                            var_19 = (byte)((var_19 + 1) % 16);
                                        }
                                        else
                                        {
                                            var_18 = (byte)((var_18 + 1) % 16);
                                        }

                                        player_ptr.field_145[var_16 - 1] = (byte)(var_18 + (var_19 << 4));
                                    }
                                    else if (var_2 == 0x50)
                                    {
                                        if (var_17 != 0)
                                        {
                                            var_19 = (byte)((var_19 - 1) & 0x0F);
                                        }
                                        else
                                        {
                                            var_18 = (byte)((var_18 - 1) & 0x0F);
                                        }

                                        player_ptr.field_145[var_16 - 1] = (byte)(var_18 + (var_19 << 4));
                                    }
                                    else if (var_2 == 0x4B)
                                    {
                                        var_15 = player_ptr.field_145;
                                        var_8 = var_1A;
                                        var_2 = ' ';
                                    }
                                    else if (var_2 == 0x45 || var_2 == '\0')
                                    {
                                        player_ptr.field_145 = var_15;
                                        var_8 = var_1A;
                                        var_2 = ' ';
                                    }
                                }
                                break;
                        }
                    }

                    duplicateCombatIcon(true, 12, player_ptr.icon_id);

                } while (var_1A != 0 || unk_4FE94.MemberOf(var_2) == false);

                player_ptr.field_141 = var_4;
                player_ptr.field_142 = var_5;
                player_ptr.icon_size = var_6;
                player_ptr.field_145 = var_15;
                duplicateCombatIcon(true, 12, player_ptr.icon_id);
                duplicateCombatIcon(false, player_ptr.icon_id, 12);

                ovr027.redraw_screen();
                ovr034.free_icon(12);

                var_2 = ovr027.yes_no(15, 10, 13, "Is this icon ok? ");

            } while (var_2 != 'Y');

            ovr033.Color_0_8_normal();
        }

        /// <summary> seg600:4281 </summary>
        static sbyte[] con_hp_adj = { 
            8, 0, 0, -2, -1, -1, -1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 2,2,2,2,2,2,2,2,2 };

        internal static sbyte get_con_hp_adj(Player player)
        {
            Player player_ptr;
            byte loop1_var;
            sbyte hp_adj;

            hp_adj = 0;
            player_ptr = player;

            for (loop1_var = 0; loop1_var <= (byte)ClassId.monk; loop1_var++)
            {
                if (player_ptr.Skill_A_lvl[loop1_var] > 0 &&
                    player_ptr.Skill_A_lvl[loop1_var] < gbl.byte_1A1CB[loop1_var])
                {
                    hp_adj += con_hp_adj[player_ptr.con];

                    if (player_ptr._class == ClassId.fighter ||
                        player_ptr._class == ClassId.paladin ||
                        player_ptr._class == ClassId.ranger)
                    {
                        byte con = player_ptr.con;

                        if (con == 17)
                        {
                            hp_adj++;
                        }
                        else if (con == 18)
                        {
                            hp_adj += 2;
                        }
                        else if (con == 19 || con == 20)
                        {
                            hp_adj += 3;
                        }
                        else if (con >= 21 && con <= 23)
                        {
                            hp_adj += 4;
                        }
                        else if (con == 24 || con == 25)
                        {
                            hp_adj += 5;
                        }
                    }

                    if (loop1_var == (byte)ClassId.ranger &&
                        player_ptr.Skill_A_lvl[loop1_var] == 1)
                    {
                        hp_adj *= 2;
                    }
                }
            }

            return hp_adj;
        }


        internal static byte sub_506BA(Player player)
        {
            Player player_ptr;
            byte loop1_var;
            byte levels_total;
            sbyte con_adj;
            byte class_count;
            byte ret_val;

            player_ptr = player;

            class_count = 0;
            levels_total = 0;
            for (loop1_var = 0; loop1_var <= (byte)ClassId.monk; loop1_var++)
            {
                if (player_ptr.Skill_A_lvl[loop1_var] > 0)
                {
                    levels_total += (byte)player_ptr.Skill_A_lvl[loop1_var];
                    class_count++;

                    if (loop1_var == (byte)ClassId.ranger)
                    {
                        levels_total++;
                    }
                    else if (loop1_var == 7)
                    {
                        levels_total++;
                    }
                }
            }

            con_adj = get_con_hp_adj(player);

            if (con_adj < 0)
            {
                if (levels_total > (System.Math.Abs(con_adj) + class_count))
                {
                    levels_total = (byte)((levels_total + con_adj) / class_count);
                }
                else
                {
                    levels_total = 1;
                }
            }
            else
            {
                levels_total = (byte)((levels_total + con_adj) / class_count);
            }

            ret_val = levels_total;

            return ret_val;
        }


        internal static byte sub_50793(Player player)
        {
            Player player_ptr;
            byte var_6;
            byte loop_var;
            sbyte var_4;
            byte var_3;
            byte var_2;
            byte ret_val;

            player_ptr = player;

            var_2 = 0;
            var_3 = 0;

            for (loop_var = 0; loop_var <= 7; loop_var++)
            {
                if (player_ptr.Skill_A_lvl[loop_var] > 0)
                {
                    var_4 = con_bonus((ClassId)loop_var);

                    if (player_ptr.Skill_A_lvl[loop_var] < gbl.byte_1A1CB[loop_var])
                    {
                        var_2++;

                        switch (loop_var)
                        {
                            case 0:
                                var_3 += (byte)((var_4 + 8) * player_ptr.Skill_A_lvl[loop_var]);
                                break;

                            case 1:
                                var_3 += (byte)((var_4 + 8) * player_ptr.Skill_A_lvl[loop_var]);
                                break;

                            case 2:
                            case 3:
                                var_3 += (byte)((var_4 + 10) * player_ptr.Skill_A_lvl[loop_var]);
                                break;

                            case 4:
                                var_3 += (byte)((var_4 + 8) * (player_ptr.Skill_A_lvl[loop_var] + 1));
                                break;

                            case 5:
                                var_3 += (byte)((var_4 + 4) * player_ptr.Skill_A_lvl[loop_var]);
                                break;

                            case 6:
                                var_3 += (byte)((var_4 + 6) * player_ptr.Skill_A_lvl[loop_var]);
                                break;

                            case 7:
                                var_3 += (byte)((var_4 + 4) * (player_ptr.Skill_A_lvl[loop_var] + 1));
                                break;
                        }
                    }
                    else
                    {
                        var_2++;
                        var_6 = (byte)((player_ptr.Skill_A_lvl[loop_var] - gbl.byte_1A1CB[loop_var]) + 1);

                        if (loop_var == 0)
                        {
                            var_3 = (byte)((var_6 * 2) + 0x48);
                        }
                        else if (loop_var == 1)
                        {
                            var_3 = 0x70;
                        }
                        else if (loop_var == 2 || loop_var == 3)
                        {
                            var_3 = (byte)((var_6 * 3) + 0x5A);
                        }
                        else if (loop_var == 4)
                        {
                            var_3 = (byte)((var_6 * 2) + 0x58);
                        }
                        else if (loop_var == 5)
                        {
                            var_3 = (byte)(var_6 + 0x2C);
                        }
                        else if (loop_var == 6)
                        {
                            var_3 = (byte)((var_6 * 2) + 0x3C);
                        }
                        else if (loop_var == 7)
                        {
                            var_3 = 0x48;
                        }
                    }
                }
            }

            var_3 /= var_2;
            ret_val = var_3 ;

            return ret_val;
        }

        static byte[ ] /* seg600:081A */ unk_16B2A = { 1, 1, 1, 1, 2, 1, 1, 2 } ;
        static byte[ ] /* seg600:0822 */ unk_16B32 = { 8, 8, 0xA, 0xA, 8, 4, 6, 4 } ;
        static byte[ ] /* seg600:3EAA */ unk_1A1BA = { 2, 2, 8, 0x10, 0x20, 1, 4, 4 };


        internal static byte sub_509E0(byte arg_0, Player player)
        {
            byte loop_var;
            byte var_5;
            byte var_4;
            byte var_3;
            byte var_2;
            byte var_1;

            var_4 = 0;

            for (loop_var = 0; loop_var <= 7; loop_var++)
            {
                if (player.Skill_A_lvl[loop_var] > 0 &&
                    (unk_1A1BA[loop_var] & arg_0) != 0)
                {
                    if (player.Skill_A_lvl[loop_var] < gbl.byte_1A1CB[loop_var])
                    {
                        var_5 = unk_16B2A[loop_var];

                        if (player.Skill_A_lvl[loop_var] > 1)
                        {
                            var_5 = 1;
                        }

                        var_2 = ovr024.roll_dice(unk_16B32[loop_var], var_5);

                        var_3 = ovr024.roll_dice(unk_16B32[loop_var], var_5);

                        if (var_3 > var_2)
                        {
                            var_2 = var_3;
                        }

                        var_4 += var_2;
                    }
                    else
                    {
                        if (loop_var == 2 || loop_var == 3)
                        {
                            var_4 = 3;
                        }
                        else if (loop_var == 4 || loop_var == 0 || loop_var == 6)
                        {
                            var_4 = 2;
                        }
                        else if (loop_var == 5)
                        {
                            var_4 = 1;
                        }
                    }
                }
            }

            var_1 = var_4;

            return var_1;
        }


        internal static void subtract_gold(Player player, int longint)
        {
            Player player_ptr;
            byte var_3;
            short var_2;

            longint *= 200;

            var_3 = 0;
            player_ptr = player;

            while (longint > 0)
            {
                var_2 = (short)((longint / money.per_copper[var_3]) + 1);

                if (player_ptr.Money[var_3] < var_2)
                {
                    var_2 = player_ptr.Money[var_3];
                }

                longint -= money.per_copper[var_3] * var_2;

                player_ptr.Money[var_3] -= var_2;

                var_3 += 1;
            }

            if (longint < 0)
            {

                longint = System.Math.Abs(longint);
                var_3 = 4;

                while (longint > 0)
                {
                    var_2 = (short)(longint / money.per_copper[var_3]);
                    longint -= money.per_copper[var_3] * var_2;

                    player_ptr.Money[var_3] += var_2;
                    var_3 -= 1;
                }
            }
        }

        internal static int[,] /* seg600:4293 */ unk_1A5A3 = { 
            { 0x2020202, 0x2020202, 0x5DD, 0xBB9, 0x1771, 0x32C9, 0x6B6D, 0xD6D9, 0x1ADB1, 0x36EE9, 0x6DDD1, -1, -1, 1, 0x10000, 0x1010000, 0, 0x101, 0x10000, 0, 1, 0x1000101, 0x1000001, 0x1000000 },
            { 0x1000001, 0x101, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0x7D1, 0xFA1, 0x1F41, 0x4651, 0x88B9, 0x11171, 0x1E849, 0x3D091, 0x7A121, 0xB71B1, 0xF4241, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0xABF, 0x157D, 0x2EE1, 0x5DC1, 0xAFC9, 0x17319, 0x2AB99, 0x55731, 0xAAE61, 0x100591, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0x1000000, 0, 1 },
            { 0, 1, 0x8CB, 0x1195, 0x2711, 0x4E21, 0x9C41, 0x15F91, 0x249F1, 0x36EE9, 0x4F589, 0x9EB11, -1, 0, 0, 0, 0, 0, 0, 0, 0x10000, 0, 0x10000, 1 },
            { 0x100, 1, 0x9C5, 0x1389, 0x2711, 0x57E5, 0x9C41, 0xEA61, 0x15F91, 0x20F59, 0x3D091, 0x5B8D9, -1, 1, 0x10000, 0x1010000, 0x1000000, 0x100, 0x10000, 0x10000, 1, 0x101, 0x1000000, 0x100 }, 
            { 0x10101, 0x1010000, 0x4E3, 0x9C5, 0x1389, 0x2711, 0x4E21, 0xA605, 0x11171, 0x1ADB1, 0x27101, 0x35B61, 0x6B6C1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 
            { 0, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };

        internal static void train_player()
        {
            Player player_ptr;
            byte var_1F;
            bool var_1D;
            short var_1C;
            byte var_1A;
            byte var_19 = 123; /* Simeon */
            byte var_18;
            byte var_17;
            byte var_16;
            short var_15;
            byte var_13;
            byte var_12;
            short var_11;
            short var_F;
            byte var_D;
            byte var_C;
            byte var_B;
            byte var_A;
            int var_9;
            int var_5;
            byte var_1;

            if (gbl.player_ptr.health_status != Status.okey &&
                Cheats.free_training == false)
            {
                seg041.DisplayStatusText(0, 14, "we only train conscious people");
            }
            else if (ovr020.getPlayerGold(gbl.player_ptr) < 1000 &&
                Cheats.free_training == false &&
                gbl.byte_1B2F1 == 0 &&
                gbl.gameWon == false)
            {
                seg041.DisplayStatusText(0, 14, "Training costs 1000 gp.");
            }
            else
            {
                var_A = 0;
                var_B = 0;

                var_5 = 0;

                var_1F = gbl.area2_ptr.field_550;
                player_ptr = gbl.player_ptr;

                var_5 = 0;

                for (var_13 = 0; var_13 <= 7; var_13++)
                {
                    if (player_ptr.Skill_A_lvl[var_13] > 0)
                    {
                        var_B += unk_1A1BA[var_13];
                        var_1 = 0;
                        var_19 = player_ptr.Skill_A_lvl[var_13];

                        switch (player_ptr.race)
                        {
                            case Race.dwarf:
                                if (var_13 == 2)
                                {
                                    if (var_19 == 9 ||
                                        (var_19 == 8 && player_ptr.strength == 0x11) ||
                                        (var_19 == 7 && player_ptr.strength < 0x11))
                                    {

                                        var_1 = 1;
                                    }
                                }
                                break;

                            case Race.elf:
                                if (var_13 == 2)
                                {
                                    if (var_19 == 7 ||
                                        (var_19 == 6 && player_ptr.strength == 0x11) ||
                                        (var_19 == 5 && player_ptr.strength < 0x11))
                                    {
                                        var_1 = 1;
                                    }
                                }

                                if (var_13 == 5)
                                {
                                    if (var_19 == 0x0B ||
                                        (var_19 == 9 && player_ptr._int < 0x11) ||
                                        (var_19 == 0x0A && player_ptr._int == 0x11))
                                    {
                                        var_1 = 1;
                                    }
                                }
                                break;

                            case Race.gnome:
                                if (var_13 == 2)
                                {
                                    if (var_19 == 6 ||
                                        (var_19 == 5 && player_ptr.strength < 18))
                                    {
                                        var_1 = 1;
                                    }
                                }
                                break;

                            case Race.half_elf:
                                if (var_13 == 0 && var_19 == 5)
                                {
                                    var_1 = 1;
                                }
                                else
                                {
                                    if (var_13 == 2 || var_13 == 4)
                                    {
                                        if (var_19 == 8 ||
                                            (var_19 == 7 && player_ptr.strength == 0x11) ||
                                            (var_19 == 6 && player_ptr.strength < 0x11))
                                        {
                                            var_1 = 1;
                                        }
                                    }

                                    if (var_13 == 5)
                                    {
                                        if (var_19 == 8 ||
                                            (var_19 == 7 && player_ptr.strength == 0x11) ||
                                            (var_19 == 6 && player_ptr.strength < 0x11))
                                        {
                                            var_1 = 1;
                                        }
                                    }
                                } 
                                break;

                            case Race.halfling:
                                if (var_13 == 2)
                                {
                                    if (var_19 == 6 ||
                                        (var_19 == 5 && player_ptr.strength == 0x11) ||
                                        (var_19 == 4 && player_ptr.strength < 0x11))
                                    {
                                        var_1 = 1;
                                    }
                                }

                                break;

                        }

                        if (var_1 == 0)
                        {
                            if (unk_1A5A3[var_13, var_19 + 1] <= player_ptr.exp ||
                                Cheats.free_training == true)
                            {
                                if (unk_1A5A3[var_13, var_19 + 1] > 0)
                                {
                                    if (Cheats.free_training == true)
                                    {
                                        if (unk_1A5A3[var_13, var_19 + 1] > 0)
                                        {
                                            if (unk_1A5A3[var_13, var_19 + 1] > player_ptr.exp)
                                            {
                                                player_ptr.exp = unk_1A5A3[var_13, var_19 + 1];
                                            }
                                        }
                                    }

                                    var_A += unk_1A1BA[var_13];

                                    var_9 = unk_1A5A3[var_13, var_19 + 2];

                                    if (var_9 > 0)
                                    {
                                        if (player_ptr.exp >= var_9 &&
                                            var_9 > var_5)
                                        {
                                            var_5 = var_9 - 1;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (gbl.byte_1B2F1 == 0)
                {
                    byte var_24 = 0;
                    int var_23 = 0;
                    var_9 = 0;
                    for (var_13 = 0; var_13 <= 7; var_13++)
                    {
                        if ((unk_1A1BA[var_13] & var_A) != 0)
                        {
                            if (unk_1A5A3[var_13, var_19 + 1] > var_23)
                            {
                                var_23 = unk_1A5A3[var_13, var_19 + 1];
                                var_24 = var_13;
                            }
                        }
                    }


                    if (var_23 > 0)
                    {
                        var_A = unk_1A1BA[var_24];
                        var_9 = unk_1A5A3[var_24, var_19 + 2];

                        if (var_9 > 0 &&
                            player_ptr.exp >= var_9 &&
                            var_9 > var_5)
                        {
                            var_5 = var_9 - 1;
                        }
                    }
                }

                if (var_5 > 0 && gbl.byte_1B2F1 == 0)
                {
                    player_ptr.exp = var_5;
                }

                if (Cheats.free_training == false)
                {
                    if ((var_B & var_1F) == 0 &&
                        gbl.byte_1B2F1 == 0)
                    {
                        seg041.DisplayStatusText(0, 14, "We don't train that class here");
                        return;
                    }

                    if ((var_A & var_1F) == 0)
                    {
                        gbl.byte_1D8B0 = 1;

                        if (gbl.byte_1B2F1 == 0)
                        {
                            seg041.DisplayStatusText(0, 14, "Not Enough Experience");
                            return;
                        }
                    }
                }

                if (Cheats.free_training == false)
                {
                    var_C = (byte)(var_A & var_1F);
                }
                else
                {
                    var_C = var_A;
                }

                bool skipBits = false;
                if (gbl.byte_1B2F1 != 0)
                {
                    skipBits = true;
                }

                if (skipBits == false)
                {
                    seg037.draw8x8_clear_area(0x16, 0x26, 1, 1);

                    var_16 = 4;

                    ovr025.displayPlayerName(false, var_16, 4, gbl.player_ptr);

                    seg041.displayString(" will become:", 0, 10, var_16, player_ptr.name.Length + 4);

                    for (var_13 = 0; var_13 <= 7; var_13++)
                    {
                        if (player_ptr.Skill_A_lvl[var_13] > 0 &&
                            (unk_1A1BA[var_13] & var_C) != 0)
                        {
                            var_16++;

                            if (var_16 == 5)
                            {
                                string text = System.String.Format("    a level {0} {1}",
                                    player_ptr.Skill_A_lvl[var_13] + 1, ovr020.classString[var_13]);

                                seg041.displayString(text, 0, 10, var_16, 6);
                            }
                            else
                            {
                                string text = System.String.Format("and a level {0} {1}",
                                    player_ptr.Skill_A_lvl[var_13] + 1, ovr020.classString[var_13]);

                                seg041.displayString(text, 0, 10, var_16, 6);
                            }
                        }
                    }
                }

                if (skipBits || ovr027.yes_no(15, 10, 13, "Do you wish to train? ") == 'Y')
                {
                    if (skipBits == false)
                    {
                        ovr025.string_print01("Congratulations...");

                        if (Cheats.free_training == false &&
                            gbl.gameWon == false)
                        {
                            subtract_gold(player_ptr, 1000);
                        }
                    }

                    var_D = 0;
                    var_17 = player_ptr.magic_user_lvl;
                    var_18 = player_ptr.ranger_lvl;
                    player_ptr.classFlags = 0;

                    for (var_13 = 0; var_13 <= 7; var_13++)
                    {
                        if (player_ptr.Skill_A_lvl[var_13] > 0)
                        {
                            var_D++;

                            if ((unk_1A1BA[var_13] & var_C) != 0)
                            {
                                player_ptr.Skill_A_lvl[var_13] += 1;
                                if (player_ptr.field_E7 > 0)
                                {
                                    player_ptr.field_E8 -= (byte)(player_ptr.field_E8 / player_ptr.field_E7);
                                    player_ptr.field_E7 -= 1;
                                }
                            }
                        }
                    }

                    ovr026.sub_6A3C6(gbl.player_ptr);

                    if (gbl.byte_1B2F1 == 0)
                    {
                        if (player_ptr.magic_user_lvl > var_17 ||
                            player_ptr.ranger_lvl > 8)
                        {
                            var_1C = -1;

                            do
                            {
                                var_1A = ovr020.spell_menu2(out var_1D, ref var_1C, 4, SpellLoc.choose);
                            } while (var_1A <= 0 && var_1D == true);

                            if (var_1A > 0)
                            {
                                player_ptr.field_79[var_1A - 1] = 1;
                            }
                        }
                    }

                    if (gbl.byte_1B2F1 != 0)
                    {
                        switch (player_ptr.magic_user_lvl)
                        {
                            case 2:
                                player_ptr.field_79[0xF - 1] = 1;
                                break;

                            case 3:
                                player_ptr.field_79[0x22 - 1] = 1;
                                player_ptr.field_79[0x10 - 1] = 1;
                                break;

                            case 4:
                                player_ptr.field_79[0x1F - 1] = 1;
                                break;

                            case 5:
                                player_ptr.field_79[0x2F - 1] = 1;
                                break;
                        }
                    }

                    if (player_ptr.field_E5 <= player_ptr.field_E6)
                    {
                        return;
                    }

                    var_F = sub_509E0(var_C, gbl.player_ptr);

                    var_11 = (short)(var_F / var_D);

                    if (var_11 == 0)
                    {
                        var_11 = 1;
                    }

                    player_ptr.field_12C += (byte)var_11;

                    var_15 = get_con_hp_adj(gbl.player_ptr);

                    var_11 = (short)((var_F + var_15) / var_D);

                    if (var_11 < 1)
                    {
                        var_11 = 1;
                    }

                    var_12 = (byte)(player_ptr.hit_point_max - player_ptr.hit_point_current);

                    player_ptr.hit_point_max += (byte)var_11;

                    player_ptr.hit_point_current = (byte)(player_ptr.hit_point_max - var_12);
                }
            }
            //func_end:
        }
    }
}
