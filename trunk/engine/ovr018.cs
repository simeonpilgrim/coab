using Classes;

namespace engine
{
    class ovr018
    {
        static Set unk_4C13D = new Set(0x0802, new byte[] { 0x80, 0x80 });
        static Set unk_4C15D = new Set(0x0803, new byte[] { 0x20, 0x00, 0x08 });

        internal static void free_player(ref Player player)
        {
            if (player.actions != null)
            {
                player.actions = null; // FreeMem( action_struct_size, playerPtr.actions );
            }

            Item item = player.itemsPtr;
            player.itemsPtr = null;

            while (item != null)
            {
                Item tmpItem = item;
                item = tmpItem.next;

                tmpItem.next = null;
                tmpItem = null; // FreeMem( item_struct_size, next_item_ptr );
            }

            player.affects.Clear();

            player = null; // FreeMem( char_struct_size, playerBase );
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
            byte gameStateBackup = gbl.game_state;
            gbl.game_state = 0;
            bool var_F = true;

            while (true)
            {
                while (var_F == true)
                {
                    seg037.draw8x8_outer_frame();
                    if (gbl.player_ptr != null)
                    {
                        ovr025.PartySummary(gbl.player_ptr);
                        menuFlags[allow_drop] = true;
                        menuFlags[allow_modify] = true;

                        if (gbl.area2_ptr.training_class_mask > 0 || Cheats.free_training == true)
                        {
                            menuFlags[allow_training] = true;
                        }

                        if ((gbl.area2_ptr.training_class_mask > 0 || Cheats.free_training == true) &&
                            ovr026.is_human(gbl.player_ptr) &&
                            ovr026.getExtraFirstSkill(gbl.player_ptr) == 0x11)
                        {
                            menuFlags[allow_multiclass] = true;
                        }
                        else
                        {
                            menuFlags[allow_multiclass] = false;
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

                    int yCol = 0;
                    for (int i = 0; i <= 11; i++)
                    {
                        if (menuFlags[i] == true)
                        {
                            seg041.displayString(menuStrings[i][0].ToString(), 0, 15, yCol + 12, 2);

                            string var_111 = seg051.Copy(menuStrings[i].Length, 1, menuStrings[i]);
                            seg041.displayString(var_111, 0, 10, yCol + 12, 3);
                            yCol++;
                        }
                    }

                    var_F = false;
                }

                bool controlKey;

                char inputkey = ovr027.displayInput(out controlKey, false, 1, 0, 0, 13, "C D M T H V A R L S B E J", "Choose a function ");

                ovr027.redraw_screen();

                if (controlKey == true)
                {
                    if (unk_4C13D.MemberOf(inputkey) == true)
                    {
                        bool var_11 = (ovr026.is_human(gbl.player_ptr) && ovr026.getExtraFirstSkill(gbl.player_ptr) == 0x11);

                        ovr020.scroll_team_list(inputkey);
                        ovr025.PartySummary(gbl.player_ptr);

                        if (ovr026.is_human(gbl.player_ptr) == false ||
                            ovr026.getExtraFirstSkill(gbl.player_ptr) != 0x11)
                        {
                            var_11 ^= false;
                        }
                        else
                        {
                            var_11 ^= true;
                        }

                        var_F = var_11 && gbl.area2_ptr.training_class_mask > 0;
                    }
                }
                else
                {
                    if (unk_4C15D.MemberOf(inputkey) == false)
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
                                    free_players(true, false);
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
                                ovr017.loadGameMenu();
                            }
                            break;

                        case 'S':
                            if (menuFlags[allow_save] == true &&
                                gbl.player_next_ptr.Count > 0)
                            {
                                ovr017.SaveGame();
                            }

                            break;

                        case 'B':
                            if (menuFlags[allow_begin] == true)
                            {
                                if ((gbl.player_next_ptr.Count > 0 && gbl.inDemo == true) ||
                                    gbl.area_ptr.field_3FA == 0 || gbl.inDemo == true)
                                {
                                    gbl.game_state = gameStateBackup;

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
                                        ovr025.PartySummary(gbl.player_ptr);
                                    }
                                    else
                                    {
                                        if (gbl.area_ptr.field_1E4 == 0)
                                        {
                                            seg037.draw8x8_03();
                                        }
                                    }

                                    ovr027.redraw_screen();
                                    gbl.area2_ptr.training_class_mask = 0;

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
                                    if (gbl.player_next_ptr.Count > 0 &&
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


        class AgeBrackets
        {
            internal int age_0; // unk_1A434
            internal int age_1; // unk_1A436
            internal int age_2; // unk_1A438
            internal int age_3; // unk_1A43A
            internal int age_4; // unk_1A43C

            internal AgeBrackets(int age0, int age1, int age2, int age3, int age4)
            {
                age_0 = age0;
                age_1 = age1;
                age_2 = age2;
                age_3 = age3;
                age_4 = age4;
            }
        }

        class RaceAgeBrackets
        {
            static AgeBrackets[] data = { // unk_1A434
            null, // monster 
            new AgeBrackets(0x32, 0x96, 0xFA, 0x15E, 0x1C2), // dwarf
            new AgeBrackets(0xAF, 0x226, 0x36B, 0x4B0, 0x640), //elf
            new AgeBrackets(0x5A, 0x12C, 0x1C2, 0x258, 0x2EE), // gnome
            new AgeBrackets(0x28, 0x64, 0xAF, 0x0FA, 0x145), // half elf
            new AgeBrackets(0x21, 0x44, 0x65, 0x90, 0x0C7), // halfling  
            new AgeBrackets(0x0F, 0x1E, 0x2D, 0x3C, 0x50), // half orc
            new AgeBrackets(0x14, 0x28, 0x3C, 0x5A, 0x78) }; // human

            internal AgeBrackets this[Race race]
            {
                get { return data[(int)race]; }
            }
        }

        static RaceAgeBrackets race_age_brackets = new RaceAgeBrackets();


        internal class stats_ranges
        {
            internal byte[] str_min; // unk_1A298
            internal byte[] str_max; // max_str_val
            internal byte[] str_100_max; // unk_1A29C
            internal byte int_min; // unk_1A29E
            internal byte int_max; // unk_1A29F
            internal byte wis_min; // unk_1A2A0
            internal byte wis_max; // unk_1A2A1
            internal byte dex_min; // unk_1A2A2
            internal byte dex_max; // unk_1A2A3
            internal byte con_min; // unk_1A2A4
            internal byte con_max; // unk_1A2A5
            internal byte cha_min; // unk_1A2A6
            internal byte cha_max; // max_cha_val

            internal stats_ranges(byte v0, byte v1, byte v2, byte v3, byte v4, byte v5, byte v6,
                byte v7, byte v8, byte v9, byte vA, byte vB, byte vC, byte vD, byte vE, byte vF)
            {
                str_min = new byte[] { v0, v1 };
                str_max = new byte[] { v2, v3 };
                str_100_max = new byte[] { v4, v5 };
                int_min = v6;
                int_max = v7;
                wis_min = v8;
                wis_max = v9;
                dex_min = vA;
                dex_max = vB;
                con_min = vC;
                con_max = vD;
                cha_min = vE;
                cha_max = vF;

            }
        }


        internal static stats_ranges[] racial_stats_limits = new stats_ranges[] { // stru_1A298
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
            bool var_23;
            bool var_22;
            byte var_21;
            byte var_20;
            short var_1E;
            byte var_1B;
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
                var_53.icon_colours[i] = (byte)(((gbl.default_icon_colours[i] + 8) << 4) + gbl.default_icon_colours[i]);
            }

            var_53.field_124 = 0x32;
            var_53.field_73 = 0x28;
            var_53.health_status = Status.okey;
            var_53.in_combat = true;
            var_53.field_DE = 1;
            var_53.mod_id = (byte)seg051.Random(256);
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

            int classes = gbl.race_classes[(int)player.race, 0];

            var_C = ovr027.alloc_stringList(classes + 1);

            var_C.field_29 = 1;
            var_C.s = "Pick Class";

            for (int index = 1; index <= classes; index++)
            {
                var_10 = ovr027.getStringListEntry(var_C, index);
                var_10.s = "  " + ovr020.classString[gbl.race_classes[(int)player.race, index]];
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
            var_53._class = (ClassId)gbl.race_classes[(int)var_53.race, var_12];
            var_53.field_E5 = 1;

            if (var_53._class >= ClassId.cleric && var_53._class <= ClassId.fighter)
            {
                var_53.class_lvls[(int)var_53._class] = 1;
            }
            else if (var_53._class >= ClassId.magic_user && var_53._class <= ClassId.monk)
            {
                var_53.class_lvls[(int)var_53._class] = 1;
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

            for (int class_idx = 0; class_idx <= 7; class_idx++)
            {
                if (var_53.class_lvls[class_idx] > 0)
                {
                    sbyte t = (sbyte)(unk_1A14A[(class_idx * 0x0D) + var_53.class_lvls[class_idx]]);

                    if (t > var_53.field_73)
                    {
                        var_53.field_73 = t;
                    }

                    var_53.classFlags += unk_1A1B2[class_idx];
                }
            }

            ovr026.sub_6A7FB(player);
            ovr027.free_stringList(ref var_C);

            int alignments = gbl.class_alignments[(int)player._class, 0];

            var_C = ovr027.alloc_stringList(alignments + 1);

            var_C.field_29 = 1;
            var_C.s = "Pick Alignment";

            for (int i = 1; i <= alignments; i++)
            {
                var_10 = ovr027.getStringListEntry(var_C, i);
                var_10.s = "  " + ovr020.alignmentString[gbl.class_alignments[(int)player._class, i]];
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

                    seg051.FreeMem(Player.StructSize, player);
                    return;
                }
            } while (reroll_stats != 'S');

            player.alignment = gbl.class_alignments[(int)player._class, var_12];

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
                for (int class_idx = 0; class_idx <= 7; class_idx++)
                {
                    if (gbl.player_ptr.class_lvls[class_idx] > 0)
                    {
                        gbl.player_ptr.class_lvls[class_idx] = 1;
                    }
                }

                player.tmp_str_00 = 0;

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
                                race_age_brackets[var_53.race].age_0 < var_53.age)
                            {
                                var_53.stats[var_1B].max += 1;
                            }

                            if (var_53.stats[var_1B].max > 0 &&
                                race_age_brackets[var_53.race].age_1 < var_53.age)
                            {
                                var_53.stats[var_1B].max -= 1;
                            }

                            if (var_53.stats[var_1B].max > 0 &&
                                race_age_brackets[var_53.race].age_2 < var_53.age)
                            {
                                var_53.stats[var_1B].max -= 2;
                            }

                            if (var_53.stats[var_1B].max > 0 &&
                                race_age_brackets[var_53.race].age_3 < var_53.age)
                            {
                                var_53.stats[var_1B].max -= 1;
                            }

                            if (var_53.stats[var_1B].max < racial_stats_limits[(int)var_53.race].str_min[var_53.sex])
                            {
                                var_53.stats[var_1B].max = racial_stats_limits[(int)var_53.race].str_min[var_53.sex];
                            }

                            if (var_53.stats[var_1B].max > racial_stats_limits[(int)var_53.race].str_max[var_53.sex])
                            {
                                var_53.stats[var_1B].max = racial_stats_limits[(int)var_53.race].str_max[var_53.sex];
                            }

                            if (var_53.stats[var_1B].max < gbl.class_stats_min[(int)var_53._class][var_1B])
                            {
                                var_53.stats[var_1B].max = gbl.class_stats_min[(int)var_53._class][var_1B];
                            }

                            if (var_53.strength == 18)
                            {
                                if (var_53.fighter_lvl > 0 ||
                                    var_53.ranger_lvl > 0 ||
                                    var_53.paladin_lvl > 0)
                                {
                                    var_53.tmp_str_00 = (byte)(seg051.Random(100) + 1);

                                    if (var_53.tmp_str_00 > racial_stats_limits[(int)var_53.race].str_100_max[var_53.sex])
                                    {
                                        var_53.tmp_str_00 = racial_stats_limits[(int)var_53.race].str_100_max[var_53.sex];
                                    }
                                }
                            }
                            break;

                        case Stat.INT:

                            if (race_age_brackets[var_53.race].age_1 < var_53.age)
                            {
                                var_53.stats[var_1B].max += 1;
                            }

                            if (race_age_brackets[var_53.race].age_3 < var_53.age)
                            {
                                var_53.stats[var_1B].max += 1;
                            }

                            if (var_53.stats[var_1B].max < racial_stats_limits[(int)var_53.race].int_min)
                            {
                                var_53.stats[var_1B].max = racial_stats_limits[(int)var_53.race].int_min;
                            }

                            if (var_53.stats[var_1B].max > racial_stats_limits[(int)var_53.race].int_max)
                            {
                                var_53.stats[var_1B].max = racial_stats_limits[(int)var_53.race].int_max;
                            }

                            if (var_53.stats[var_1B].max < gbl.class_stats_min[(int)var_53._class][var_1B])
                            {
                                var_53.stats[var_1B].max = gbl.class_stats_min[(int)var_53._class][var_1B];
                            }
                            break;

                        case Stat.WIS:
                            if (race_age_brackets[var_53.race].age_0 >= var_53.age)
                            {
                                var_53.stats[var_1B].max -= 1;
                            }

                            if (race_age_brackets[var_53.race].age_1 < var_53.age)
                            {
                                var_53.stats[var_1B].max += 1;
                            }

                            if (race_age_brackets[var_53.race].age_2 < var_53.age)
                            {
                                var_53.stats[var_1B].max += 1;
                            }

                            if (race_age_brackets[var_53.race].age_3 < var_53.age)
                            {
                                var_53.stats[var_1B].max += 1;
                            }

                            if (var_53.stats[var_1B].max < racial_stats_limits[(int)var_53.race].wis_min)
                            {
                                var_53.stats[var_1B].max = racial_stats_limits[(int)var_53.race].wis_min;
                            }

                            if (var_53.stats[var_1B].max > racial_stats_limits[(int)var_53.race].wis_max)
                            {
                                var_53.stats[var_1B].max = racial_stats_limits[(int)var_53.race].wis_max;
                            }

                            if (var_53.stats[var_1B].max < gbl.class_stats_min[(int)var_53._class][var_1B])
                            {
                                var_53.stats[var_1B].max = gbl.class_stats_min[(int)var_53._class][var_1B];
                            }

                            if (var_53.stats[var_1B].max < 13 &&
                                var_53._class >= ClassId.mc_c_f && var_53._class <= ClassId.mc_c_t)
                            {
                                // Multi-Class Cleric
                                var_53.stats[var_1B].max = 13;
                            }
                            break;

                        case Stat.DEX:

                            if (race_age_brackets[var_53.race].age_2 < var_53.age)
                            {
                                var_53.stats[var_1B].max -= 2;
                            }

                            if (race_age_brackets[var_53.race].age_3 < var_53.age)
                            {
                                var_53.stats[var_1B].max -= 2;
                            }

                            if (var_53.stats[var_1B].max < racial_stats_limits[(int)var_53.race].dex_min)
                            {
                                var_53.stats[var_1B].max = racial_stats_limits[(int)var_53.race].dex_min;
                            }

                            if (var_53.stats[var_1B].max > racial_stats_limits[(int)var_53.race].dex_max)
                            {
                                var_53.stats[var_1B].max = racial_stats_limits[(int)var_53.race].dex_max;
                            }

                            if (var_53.stats[var_1B].max < gbl.class_stats_min[(int)var_53._class][var_1B])
                            {
                                var_53.stats[var_1B].max = gbl.class_stats_min[(int)var_53._class][var_1B];
                            }
                            break;

                        case Stat.CON:
                            if (race_age_brackets[var_53.race].age_1 < var_53.age)
                            {
                                var_53.stats[var_1B].max -= 1;
                            }

                            if (race_age_brackets[var_53.race].age_2 < var_53.age)
                            {
                                var_53.stats[var_1B].max -= 1;
                            }

                            if (race_age_brackets[var_53.race].age_3 < var_53.age)
                            {
                                var_53.stats[var_1B].max -= 1;
                            }

                            if (var_53.stats[var_1B].max < racial_stats_limits[(int)var_53.race].con_min)
                            {
                                var_53.stats[var_1B].max = racial_stats_limits[(int)var_53.race].con_min;
                            }

                            if (var_53.stats[var_1B].max > racial_stats_limits[(int)var_53.race].con_max)
                            {
                                var_53.stats[var_1B].max = racial_stats_limits[(int)var_53.race].con_max;
                            }

                            if (var_53.stats[var_1B].max < gbl.class_stats_min[(int)var_53._class][var_1B])
                            {
                                var_53.stats[var_1B].max = gbl.class_stats_min[(int)var_53._class][var_1B];
                            }
                            break;

                        case Stat.CHA:
                            if (var_53.stats[var_1B].max < racial_stats_limits[(int)var_53.race].cha_min)
                            {
                                var_53.stats[var_1B].max = racial_stats_limits[(int)var_53.race].cha_min;
                            }

                            if (var_53.stats[var_1B].max > racial_stats_limits[(int)var_53.race].cha_max)
                            {
                                var_53.stats[var_1B].max = racial_stats_limits[(int)var_53.race].cha_max;
                            }

                            if (var_53.stats[var_1B].max < gbl.class_stats_min[(int)var_53._class][var_1B])
                            {
                                var_53.stats[var_1B].max = gbl.class_stats_min[(int)var_53._class][var_1B];
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
                var_53.base_movement = 0x0C;
                var_21 = 0;
                var_20 = 0;

                for (int i = 0; i < 5; i++)
                {
                    var_53.field_12D[0, i] = 0;
                    var_53.field_12D[1, i] = 0;
                    var_53.field_12D[2, i] = 0;
                }

                for (int class_idx = 0; class_idx <= 7; class_idx++)
                {
                    if (var_53.class_lvls[class_idx] > 0)
                    {
                        if (class_idx == 0)
                        {
                            var_53.field_12D[0, 0] = 1;
                        }
                        else if (class_idx == 5)
                        {
                            var_53.field_12D[2, 0] = 1;
                        }

                        var_21 += ovr024.roll_dice(unk_1A8C4[class_idx], unk_1A8C3[class_idx]);

                        if (class_idx == 0)
                        {
                            ovr026.calc_cleric_spells(false, player);

                            for (int i = 1; i <= 100; i++)
                            {
                                SpellEntry stru = gbl.spell_table[i];

                                if (stru.spellClass == 0 &&
                                    stru.spellLevel == 1)
                                {
                                    var_53.field_79[i - 1] = 1;
                                }
                            }
                        }
                        else if (class_idx == 5)
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
                byte trainingClassMaskBackup = gbl.area2_ptr.training_class_mask;
                gbl.area2_ptr.training_class_mask = 0xff;
                gbl.byte_1D8B0 = 0;
                gbl.byte_1B2F1 = 1;

                do
                {
                    train_player();
                } while (gbl.byte_1D8B0 == 0);

                gbl.area2_ptr.training_class_mask = trainingClassMaskBackup;
                gbl.byte_1B2F1 = 0;
                bool first_lvl = true;
                string text = string.Empty;

                for (int class_idx = 0; class_idx <= 7; class_idx++)
                {
                    if (gbl.player_ptr.class_lvls[class_idx] > 0 ||
                        (gbl.player_ptr.Skill_B_lvl[class_idx] < ovr026.HumanFirstClassLevelOrZero(gbl.player_ptr) &&
                         gbl.player_ptr.Skill_B_lvl[class_idx] > 0))
                    {
                        if (first_lvl == false)
                        {
                            text += "/";
                        }

                        byte b = gbl.player_ptr.Skill_B_lvl[class_idx];
                        b += gbl.player_ptr.class_lvls[class_idx];

                        text += b.ToString();

                        first_lvl = false;
                    }
                }

                seg041.displayString(text, 0, 15, 15, 7);
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

            player.max_str_00 = player.tmp_str_00;

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


        internal static int con_bonus(ClassId classId)
        {
            int bonus;
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
                bonus = stat - 14;
            }
            else
            {
                bonus = 2;
            }

            return bonus;
        }


        internal static void dropPlayer()
        {
            if (gbl.player_ptr != null)
            {
                Player player = gbl.player_ptr;

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
                    free_players(true, false);
                }
                else
                {
                    ovr025.string_print01(player.name + " breathes a sigh of relief.");
                }
            }

            ovr025.PartySummary(gbl.player_ptr);
        }

        /// <summary>
        /// nested function, has not been fix to be not nested.
        /// </summary>
        internal static void draw_highlight_stat(bool highlighted, byte edited_stat, byte name_cursor_pos) /* sub_4E6F2 */
        {
            if (edited_stat >= 0 && edited_stat <= 5)
            {
                ovr020.display_stat(highlighted, edited_stat);
            }
            else if (edited_stat == 6)
            {
                ovr025.display_hp(highlighted, 18, 4, gbl.player_ptr);
            }
            else if (edited_stat == 7)
            {
                if (highlighted == true)
                {
                    seg041.displaySpaceChar(1, 0, 1, gbl.player_ptr.name.Length + 1);
                    seg041.displayString(gbl.player_ptr.name, 0, 13, 1, 1);

                    if (name_cursor_pos > gbl.player_ptr.name.Length || gbl.player_ptr.name[name_cursor_pos - 1] == ' ')
                    {
                        seg041.displayString("%", 0, 15, 1, name_cursor_pos);
                    }
                    else
                    {
                        seg041.displayString(gbl.player_ptr.name[name_cursor_pos - 1].ToString(), 0, 15, 1, name_cursor_pos);
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
            byte name_cursor_pos;
            bool var_36;
            char var_35;

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

            byte[] stats_bkup = new byte[6];
            for (int stat_var = 0; stat_var < 6; stat_var++)
            {
                stats_bkup[stat_var] = gbl.player_ptr.stats[stat_var].max;
            }

            byte var_7 = gbl.player_ptr.tmp_str_00;
            byte var_8 = gbl.player_ptr.hit_point_max;

            string nameBackup = gbl.player_ptr.name;

            name_cursor_pos = 1;
            byte edited_stat = 7;
            draw_highlight_stat(false, edited_stat, name_cursor_pos);
            edited_stat = 0;
            draw_highlight_stat(true, edited_stat, name_cursor_pos);
            Player player_ptr = gbl.player_ptr;

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
                                    gbl.player_ptr.name = gbl.player_ptr.name.Substring(0, gbl.player_ptr.name.Length - 1);
                                    name_cursor_pos = (byte)gbl.player_ptr.name.Length;
                                }
                                else
                                {
                                    string part_a = gbl.player_ptr.name.Substring(0, name_cursor_pos);
                                    string part_b = gbl.player_ptr.name.Substring(name_cursor_pos + 1, gbl.player_ptr.name.Length - name_cursor_pos);
                                    gbl.player_ptr.name = part_a + part_b;
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
                                int stat_var = edited_stat;
                                player_ptr.stats[stat_var].max -= 1;

                                switch ((Stat)stat_var)
                                {
                                    case Stat.STR:
                                        if (player_ptr.tmp_str_00 > 0)
                                        {
                                            player_ptr.tmp_str_00 -= 1;

                                            player_ptr.stats[stat_var].max += 1;
                                        }
                                        else
                                        {
                                            if (player_ptr.stats[stat_var].max < racial_stats_limits[(int)player_ptr.race].str_min[player_ptr.sex])
                                            {
                                                player_ptr.stats[stat_var].max = racial_stats_limits[(int)player_ptr.race].str_min[player_ptr.sex];
                                            }
                                        }

                                        if (player_ptr.stats[stat_var].max < gbl.class_stats_min[(int)player_ptr._class].str_min)
                                        {
                                            player_ptr.stats[stat_var].max = gbl.class_stats_min[(int)player_ptr._class].str_min;
                                        }
                                        break;

                                    case Stat.INT:
                                        if (player_ptr.stats[stat_var].max < racial_stats_limits[(int)player_ptr.race].int_min)
                                        {
                                            player_ptr.stats[stat_var].max = racial_stats_limits[(int)player_ptr.race].int_min;
                                        }

                                        if (player_ptr.stats[stat_var].max < gbl.class_stats_min[(int)player_ptr._class].int_min)
                                        {
                                            player_ptr.stats[stat_var].max = gbl.class_stats_min[(int)player_ptr._class].int_min;
                                        }
                                        break;

                                    case Stat.WIS:
                                        if (player_ptr.stats[stat_var].max < racial_stats_limits[(int)player_ptr.race].wis_min)
                                        {
                                            player_ptr.stats[stat_var].max = racial_stats_limits[(int)player_ptr.race].wis_min;
                                        }

                                        if (player_ptr.stats[stat_var].max < gbl.class_stats_min[(int)player_ptr._class].wis_min)
                                        {
                                            player_ptr.stats[stat_var].max = gbl.class_stats_min[(int)player_ptr._class].wis_min;
                                        }

                                        if (player_ptr.field_12D[0, 0] > 0)
                                        {
                                            player_ptr.field_12D[0, 0] = 1;
                                        }
                                        break;

                                    case Stat.DEX:
                                        if (player_ptr.stats[stat_var].max < racial_stats_limits[(int)player_ptr.race].dex_min)
                                        {
                                            player_ptr.stats[stat_var].max = racial_stats_limits[(int)player_ptr.race].dex_min;
                                        }

                                        if (player_ptr.stats[stat_var].max < gbl.class_stats_min[(int)player_ptr._class].dex_min)
                                        {
                                            player_ptr.stats[stat_var].max = gbl.class_stats_min[(int)player_ptr._class].dex_min;
                                        }
                                        break;

                                    case Stat.CON:
                                        if (player_ptr.stats[stat_var].max < racial_stats_limits[(int)player_ptr.race].con_min)
                                        {
                                            player_ptr.stats[stat_var].max = racial_stats_limits[(int)player_ptr.race].con_min;
                                        }

                                        if (player_ptr.stats[stat_var].max < gbl.class_stats_min[(int)player_ptr._class].con_min)
                                        {
                                            player_ptr.stats[stat_var].max = gbl.class_stats_min[(int)player_ptr._class].con_min;
                                        }

                                        int max_hp = calc_max_hp(gbl.player_ptr);
                                        if (max_hp < player_ptr.hit_point_max)
                                        {
                                            player_ptr.hit_point_max = (byte)max_hp;
                                        }

                                        player_ptr.hit_point_current = player_ptr.hit_point_max;
                                        edited_stat = 6;
                                        draw_highlight_stat(false, edited_stat, name_cursor_pos);
                                        edited_stat = 4;

                                        break;

                                    case Stat.CHA:
                                        if (player_ptr.stats[stat_var].max < racial_stats_limits[(int)player_ptr.race].cha_min)
                                        {
                                            player_ptr.stats[stat_var].max = racial_stats_limits[(int)player_ptr.race].cha_min;
                                        }

                                        if (player_ptr.stats[stat_var].max < gbl.class_stats_min[(int)player_ptr._class].cha_min)
                                        {
                                            player_ptr.stats[stat_var].max = gbl.class_stats_min[(int)player_ptr._class].cha_min;
                                        }
                                        break;
                                }
                            }
                            else if (edited_stat == 6)
                            {
                                player_ptr.hit_point_max -= 1;

                                if (sub_506BA(gbl.player_ptr) > player_ptr.hit_point_max)
                                {
                                    player_ptr.hit_point_max = (byte)sub_506BA(player_ptr);
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
                                int stat_var = edited_stat;

                                player_ptr.stats[stat_var].max += 1;
                                switch ((Stat)stat_var)
                                {
                                    case Stat.STR:
                                        if (player_ptr.stats[stat_var].max > racial_stats_limits[(int)player_ptr.race].str_max[player_ptr.sex])
                                        {
                                            if (player_ptr.stats[stat_var].max > 18 &&
                                                (player_ptr.fighter_lvl > 0 || player_ptr.paladin_lvl > 0 || player_ptr.ranger_lvl > 0) &&
                                                racial_stats_limits[(int)player_ptr.race].str_100_max[player_ptr.sex] > player_ptr.tmp_str_00)
                                            {
                                                player_ptr.tmp_str_00 += 1;
                                            }

                                            player_ptr.stats[stat_var].max = racial_stats_limits[(int)player_ptr.race].str_max[player_ptr.sex];
                                        }
                                        break;

                                    case Stat.INT:
                                        if (player_ptr.stats[stat_var].max > racial_stats_limits[(int)player_ptr.race].int_max)
                                        {
                                            player_ptr.stats[stat_var].max = racial_stats_limits[(int)player_ptr.race].int_max;
                                        }
                                        break;

                                    case Stat.WIS:
                                        if (player_ptr.stats[stat_var].max > racial_stats_limits[(int)player_ptr.race].wis_max)
                                        {
                                            player_ptr.stats[stat_var].max = racial_stats_limits[(int)player_ptr.race].wis_max;
                                        }

                                        if (player_ptr.field_12D[0, 0] > 0)
                                        {
                                            player_ptr.field_12D[0, 0] = 1;
                                        }
                                        break;

                                    case Stat.DEX:
                                        if (player_ptr.stats[stat_var].max > racial_stats_limits[(int)player_ptr.race].dex_max)
                                        {
                                            player_ptr.stats[stat_var].max = racial_stats_limits[(int)player_ptr.race].dex_max;
                                        }
                                        break;

                                    case Stat.CON:
                                        if (player_ptr.stats[stat_var].max > racial_stats_limits[(int)player_ptr.race].con_max)
                                        {
                                            player_ptr.stats[stat_var].max = racial_stats_limits[(int)player_ptr.race].con_max;
                                        }

                                        if (sub_506BA(gbl.player_ptr) > player_ptr.hit_point_max)
                                        {
                                            player_ptr.hit_point_max = (byte)sub_506BA(player_ptr);
                                        }

                                        player_ptr.hit_point_current = player_ptr.hit_point_max;
                                        edited_stat = 6;
                                        draw_highlight_stat(false, edited_stat, name_cursor_pos);
                                        edited_stat = 4;
                                        break;

                                    case Stat.CHA:
                                        if (player_ptr.stats[stat_var].max > racial_stats_limits[(int)player_ptr.race].cha_max)
                                        {
                                            player_ptr.stats[stat_var].max = racial_stats_limits[(int)player_ptr.race].cha_max;
                                        }
                                        break;
                                }
                            }
                            else
                            {
                                if (edited_stat == 6)
                                {
                                    player_ptr.hit_point_max += 1;

                                    if (calc_max_hp(gbl.player_ptr) < player_ptr.hit_point_max)
                                    {
                                        player_ptr.hit_point_max = (byte)calc_max_hp(gbl.player_ptr);
                                    }

                                    player_ptr.hit_point_current = player_ptr.hit_point_max;
                                }
                                else
                                {
                                    if (name_cursor_pos == player_ptr.name.Length + 1)
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
                                s += gbl.player_ptr.name.Substring(del + 1);
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
                        else if (var_35 == 0x45)
                        {
                            for (int stat_var = 0; stat_var < 6; stat_var++)
                            {
                                player_ptr.stats[stat_var].max = stats_bkup[stat_var];
                            }

                            gbl.player_ptr.tmp_str_00 = var_7;
                            gbl.player_ptr.hit_point_max = var_8;
                            gbl.player_ptr.hit_point_current = gbl.player_ptr.hit_point_max;

                            gbl.player_ptr.name = nameBackup;

                            ovr025.reclac_player_values(gbl.player_ptr);
                            return;
                        }
                    }
                    else if (var_35 == 0)
                    {
                        for (int stat_var = 0; stat_var < 6; stat_var++)
                        {
                            gbl.player_ptr.stats[stat_var].max = stats_bkup[stat_var];
                        }

                        gbl.player_ptr.tmp_str_00 = var_7;
                        gbl.player_ptr.hit_point_max = var_8;
                        gbl.player_ptr.name = nameBackup;

                        gbl.player_ptr.hit_point_current = gbl.player_ptr.hit_point_max;
                        ovr025.reclac_player_values(gbl.player_ptr);
                        return;
                    }
                }

                ovr025.reclac_player_values(gbl.player_ptr);
                ovr020.display_player_stats01();

                draw_highlight_stat(true, edited_stat, name_cursor_pos);
            } while (var_36 == true || var_35 != 0x4B);

            ovr026.calc_cleric_spells(true, gbl.player_ptr);

            gbl.player_ptr.field_F8 = 1;

            player_ptr = gbl.player_ptr;
            var_8 = 0;
            byte var_40 = 0;

            for (int var_33 = 0; var_33 < 8; var_33++)
            {
                if (player_ptr.class_lvls[var_33] > 0)
                {
                    if (player_ptr.class_lvls[var_33] < gbl.max_class_levels[var_33])
                    {
                        if ((ClassId)var_33 == ClassId.ranger)
                        {
                            var_8 += (byte)((player_ptr.class_lvls[var_33] + 1) * (con_bonus((ClassId)var_33)));
                        }
                        else
                        {
                            var_8 += (byte)(player_ptr.class_lvls[var_33] * (con_bonus((ClassId)var_33)));
                        }
                    }
                    else
                    {
                        var_8 += (byte)((gbl.max_class_levels[var_33] - 1) * con_bonus((ClassId)var_33));
                    }
                    var_40++;
                }
            }

            var_8 /= var_40;

            player_ptr.field_12C = (byte)(player_ptr.hit_point_max - var_8);

            for (int stat_var = 0; stat_var <= 5; stat_var++)
            {
                gbl.player_ptr.stats[stat_var].tmp = gbl.player_ptr.stats[stat_var].max;
            }

            gbl.player_ptr.tmp_str_00 = gbl.player_ptr.max_str_00;
        }


        internal static void AddPlayer()
        {
            seg037.draw8x8_clear_area(0x16, 0x26, 1, 1);

            bool dummy_bool;
            char input_key = ovr027.displayInput(out dummy_bool, false, 0, 15, 10, 13, "Curse Pool Hillsfar Exit", "Add from where? ");

            switch (input_key)
            {
                case 'C':
                    gbl.import_from = ImportSource.Curse;
                    break;

                case 'P':
                    gbl.import_from = ImportSource.Pool;
                    break;

                case 'H':
                    gbl.import_from = ImportSource.Hillsfar;
                    break;

                case 'E':
                case '\0':
                    return;
            }

            StringList strList;
            StringList var_4;
            ovr017.sub_47465(out strList, out var_4);

            if (var_4 != null)
            {
                int pc_count = 0;

                short strList_index = 0;
                StringList select_sl = var_4;
                bool var_1D = true;

                do
                {
                    bool showExit = true;
                    input_key = ovr027.sl_select_item(out select_sl, ref strList_index, ref var_1D, showExit, var_4,
                        22, 38, 2, 1, 15, 10, 13, "Add", "Add a character: ");

                    if ((input_key == 13 || input_key == 'A') &&
                        select_sl.s[0] != '*')
                    {
                        ovr027.redraw_screen();

                        Player new_player = new Player();

                        StringList var_10 = ovr027.getStringListEntry(strList, strList_index);

                        ovr017.import_char01(1, 0, ref new_player, var_10.s);

                        select_sl.s = "* " + select_sl.s;
                        pc_count = 0;

                        if (gbl.player_next_ptr.Count == 0)
                        {
                            gbl.area2_ptr.party_size = 0;
                            ovr017.AssignPlayerIconId(new_player);

                            ovr017.LoadPlayerCombatIcon(true);
                        }
                        else
                        {
                            bool paladin_present = false;
                            string paladins_name = "";
                            bool evil_present = false;
                            int ranger_count = 0;
                            bool found = false;

                            foreach (Player tmp_player in gbl.player_next_ptr)
                            {
                                if (tmp_player.name == new_player.name &&
                                    tmp_player.mod_id == new_player.mod_id)
                                {
                                    found = true;
                                    break;
                                }

                                if (tmp_player.field_F7 < 0x80)
                                {
                                    pc_count++;
                                }

                                if (tmp_player.ranger_lvl > 0)
                                {
                                    ranger_count++;
                                }

                                if ((tmp_player.alignment + 1) % 3 == 0)
                                {
                                    evil_present = true;
                                }

                                if (tmp_player.paladin_lvl > 0)
                                {
                                    paladin_present = true;
                                    paladins_name = tmp_player.name;
                                }
                            }

                            if (found == false &&
                                ((new_player.field_F7 < 0x80 && pc_count < 6) ||
                                    (new_player.field_F7 > 0x7F && gbl.area2_ptr.party_size < 8)) &&
                                (new_player.paladin_lvl == 0 || evil_present == false) &&
                                (new_player.ranger_lvl == 0 || ranger_count < 3) &&
                                (((new_player.alignment + 1) % 3) != 0 || paladin_present == false))
                            {
                                ovr017.AssignPlayerIconId(new_player);
                                ovr017.LoadPlayerCombatIcon(true);

                                if (new_player.field_F7 < 0x80)
                                {
                                    pc_count++;
                                }
                            }
                            else
                            {
                                seg051.Delete(2, 1, ref select_sl.s);

                                if (new_player.paladin_lvl > 0 && evil_present == true)
                                {
                                    ovr025.string_print01("paladins do not join with evil scum");
                                    seg041.GameDelay();
                                }
                                else if (new_player.ranger_lvl > 0 && ranger_count > 2)
                                {
                                    ovr025.string_print01("too many rangers in party");
                                }
                                else if (((new_player.alignment + 1) % 3) == 0 &&
                                        paladin_present == true)
                                {
                                    ovr025.string_print01(paladins_name + " will tolerate no evil!");
                                }

                                new_player = null; // FreeMem( char_struct_size, player_ptr1 );
                            }
                        }
                    }

                } while (input_key != 0x45 && input_key != '\0' && pc_count <= 5 && gbl.area2_ptr.party_size <= 7);

                ovr027.free_stringList(ref var_4);
            }
        }


        internal static void free_players(bool free_icon, bool leave_party_size)
        {
            int index = gbl.player_next_ptr.IndexOf(gbl.player_ptr);

            if (index >= 0)
            {
                gbl.player_next_ptr.RemoveAt(index);

                if (free_icon)
                {
                    ovr034.free_icon(gbl.player_ptr.icon_id);
                }

                if (leave_party_size == false)
                {
                    gbl.area2_ptr.party_size--;
                }

                free_player(ref gbl.player_ptr);

                index = index > 0 ? index - 1 : 0;
                if (gbl.player_next_ptr.Count > 0)
                {
                    gbl.player_ptr = gbl.player_next_ptr[index];
                }
                else
                {
                    gbl.player_ptr = null;
                }
            }
        }


        internal static void drawIconEditorIcons(sbyte titleY, sbyte titleX) /* sub_4FB7C */
        {
            seg040.DrawColorBlock(0, 24, 12, titleY * 24, titleX * 3);

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

            int bitPerPixel = gbl.combat_icons[destIndex, 0].bpp;

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
                    newColors[gbl.default_icon_colours[i]] = (byte)(var_28.icon_colours[i] & 0x0F);
                    newColors[gbl.default_icon_colours[i] + 8] = (byte)((var_28.icon_colours[i] & 0xF0) >> 4);
                }

                seg040.DaxBlockRecolor(gbl.combat_icons[destIndex, 0], 0, 0, newColors, oldColors);
                seg040.DaxBlockRecolor(gbl.combat_icons[destIndex, 1], 0, 0, newColors, oldColors);
            }
        }

        static Set unk_4FE94 = new Set(0x0009, new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20 });

        internal static void icon_builder()
        {
            Player player_ptr2;
            Player player_ptr;
            char var_1B = '\0'; /* Simeon */
            byte var_1A = 0; /* Simeon */
            bool second_color = false;
            byte color_index = 0;
            byte[] bkup_colours = new byte[6];
            byte var_8;
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
                System.Array.Copy(player_ptr.icon_colours, bkup_colours, 6);

                byte bkup_icon_id = player_ptr.icon_id;
                player_ptr.icon_id = 0x0C;
                ovr017.LoadPlayerCombatIcon(false);
                player_ptr.icon_id = bkup_icon_id;

                var_4 = player_ptr.head_icon;
                var_5 = player_ptr.weapon_icon;
                byte bkup_size = player_ptr.icon_size;

                duplicateCombatIcon(true, 12, player_ptr.icon_id);
                drawIconEditorIcons(2, 1);

                seg041.displayString("old", 0, 15, 6, 8);
                seg041.displayString("ready   action", 0, 15, 10, 3);
                seg041.displayString("new", 0, 15, 12, 8);
                seg041.displayString("ready   action", 0, 15, 16, 3);

                do
                {

                    drawIconEditorIcons(4, 1);

                    string text;
                    if (var_8 == 4)
                    {
                        if (player_ptr.icon_size == 2)
                        {
                            text = "Small" + iconStrings[var_8];
                        }
                        else
                        {
                            text = "Large" + iconStrings[var_8];
                        }
                    }
                    else
                    {
                        text = iconStrings[var_8];
                    }

                    var_2 = ovr027.displayInput(out var_3, false, 0, 15, 10, 13, text, string.Empty);

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
                                        second_color = false;

                                        iconStrings[3] = "Weapon Body Hair Shield Arm Leg Exit";
                                        break;

                                    case '2':
                                        var_8 = 3;
                                        second_color = true;

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
                                        color_index = 5;
                                        break;

                                    case 'B':
                                        color_index = 0;
                                        break;

                                    case 'H':
                                        color_index = 3;
                                        break;

                                    case 'F':
                                        color_index = 3;
                                        break;

                                    case 'S':
                                        color_index = 4;
                                        break;

                                    case 'A':
                                        color_index = 1;
                                        break;

                                    case 'L':
                                        color_index = 2;
                                        break;

                                    default:
                                        color_index = 0;
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
                                        bkup_size = player_ptr.icon_size;
                                        var_8 = 1;
                                        var_2 = ' ';
                                        break;

                                    case 'E':
                                        goto case '\0';

                                    case '\0':
                                        player_ptr.icon_size = bkup_size;
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
                                            if (player_ptr.head_icon > 0)
                                            {
                                                player_ptr.head_icon -= 1;
                                            }
                                            else
                                            {
                                                player_ptr.head_icon = 13;
                                            }
                                        }
                                        else if (var_2 == 0x4E)
                                        {
                                            if (player_ptr.head_icon < 13)
                                            {
                                                player_ptr.head_icon += 1;
                                            }
                                            else
                                            {

                                                player_ptr.head_icon = 0;
                                            }
                                        }
                                        else if (var_2 == 0x4B)
                                        {
                                            player_ptr2 = gbl.player_ptr;
                                            var_4 = player_ptr2.head_icon;
                                            var_8 = var_1A;
                                            var_2 = ' ';
                                        }
                                        else if (var_2 == 0x45 || var_2 == '\0')
                                        {
                                            player_ptr.head_icon = var_4;
                                            var_8 = var_1A;
                                            var_2 = ' ';
                                        }

                                        ovr017.LoadPlayerCombatIcon(false);
                                    }
                                    else if (var_1B == 0x57)
                                    {
                                        if (var_2 == 0x50)
                                        {
                                            if (player_ptr.weapon_icon > 0)
                                            {
                                                player_ptr.weapon_icon -= 1;
                                            }
                                            else
                                            {
                                                player_ptr.weapon_icon = 0x1F;
                                            }
                                        }
                                        else if (var_2 == 0x4E)
                                        {
                                            if (player_ptr.weapon_icon < 0x1F)
                                            {
                                                player_ptr.weapon_icon += 1;
                                            }
                                            else
                                            {
                                                player_ptr.weapon_icon = 0;
                                            }
                                        }
                                        else if (var_2 == 0x4B)
                                        {
                                            player_ptr2 = gbl.player_ptr;
                                            var_5 = player_ptr2.weapon_icon;
                                            var_8 = var_1A;
                                            var_2 = ' ';
                                        }
                                        else if (var_2 == 0x45 || var_2 == '\0')
                                        {
                                            player_ptr.weapon_icon = var_5;
                                            var_8 = var_1A;
                                            var_2 = ' ';
                                        }

                                        ovr017.LoadPlayerCombatIcon(false);
                                    }
                                }
                                else if (var_1A == 3)
                                {
                                    byte low_color = (byte)(player_ptr.icon_colours[color_index] & 0x0F);
                                    byte high_color = (byte)((player_ptr.icon_colours[color_index] & 0xF0) >> 4);

                                    if (var_2 == 0x4E)
                                    {
                                        if (second_color == true)
                                        {
                                            high_color = (byte)((high_color + 1) % 16);
                                        }
                                        else
                                        {
                                            low_color = (byte)((low_color + 1) % 16);
                                        }

                                        player_ptr.icon_colours[color_index] = (byte)(low_color + (high_color << 4));
                                    }
                                    else if (var_2 == 0x50)
                                    {
                                        if (second_color == true)
                                        {
                                            high_color = (byte)((high_color - 1) & 0x0F);
                                        }
                                        else
                                        {
                                            low_color = (byte)((low_color - 1) & 0x0F);
                                        }

                                        player_ptr.icon_colours[color_index] = (byte)(low_color + (high_color << 4));
                                    }
                                    else if (var_2 == 0x4B)
                                    {
                                        System.Array.Copy(player_ptr.icon_colours, bkup_colours, 6);
                                        var_8 = var_1A;
                                        var_2 = ' ';
                                    }
                                    else if (var_2 == 0x45 || var_2 == '\0')
                                    {
                                        System.Array.Copy(bkup_colours, player_ptr.icon_colours, 6);
                                        var_8 = var_1A;
                                        var_2 = ' ';
                                    }
                                }
                                break;
                        }
                    }

                    duplicateCombatIcon(true, 12, player_ptr.icon_id);

                } while (var_1A != 0 || unk_4FE94.MemberOf(var_2) == false);

                player_ptr.head_icon = var_4;
                player_ptr.weapon_icon = var_5;
                player_ptr.icon_size = bkup_size;

                System.Array.Copy(bkup_colours, player_ptr.icon_colours, 6);

                duplicateCombatIcon(true, 12, player_ptr.icon_id);
                duplicateCombatIcon(false, player_ptr.icon_id, 12);

                ovr027.redraw_screen();
                ovr034.free_icon(12);

                var_2 = ovr027.yes_no(15, 10, 13, "Is this icon ok? ");

            } while (var_2 != 'Y');

            ovr033.Color_0_8_normal();
        }

        /// <summary> seg600:4281 </summary>
        static sbyte[] con_hp_adj = { 8, 0, 0, -2, -1, -1, -1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };

        internal static sbyte get_con_hp_adj(Player player)
        {
            sbyte hp_adj = 0;

            for (int class_index = 0; class_index <= (byte)ClassId.monk; class_index++)
            {
                if (player.class_lvls[class_index] > 0 &&
                    player.class_lvls[class_index] < gbl.max_class_levels[class_index])
                {
                    hp_adj += con_hp_adj[player.con];

                    if (player._class == ClassId.fighter ||
                        player._class == ClassId.paladin ||
                        player._class == ClassId.ranger)
                    {
                        byte con = player.con;

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

                    if (class_index == (byte)ClassId.ranger &&
                        player.class_lvls[class_index] == 1)
                    {
                        hp_adj *= 2;
                    }
                }
            }

            return hp_adj;
        }


        internal static int sub_506BA(Player player)
        {
            int class_count = 0;
            int levels_total = 0;

            for (int class_index = 0; class_index <= (int)ClassId.monk; class_index++)
            {
                if (player.class_lvls[class_index] > 0)
                {
                    levels_total += player.class_lvls[class_index] + hp_calc_table[class_index].lvl_bonus;
                    class_count++;
                }
            }

            int con_adj = get_con_hp_adj(player);

            if (con_adj < 0)
            {
                if (levels_total > (System.Math.Abs(con_adj) + class_count))
                {
                    levels_total = (levels_total + con_adj) / class_count;
                }
                else
                {
                    levels_total = 1;
                }
            }
            else
            {
                levels_total = (levels_total + con_adj) / class_count;
            }

            return levels_total;
        }

        class hp_calc
        {
            public hp_calc(int _dice, int _lvl, int _base, int _mult) { dice = _dice; lvl_bonus = _lvl; max_base = _base; max_mult = _mult; }

            public int dice;
            public int lvl_bonus;
            public int max_base;
            public int max_mult;
        }

        static hp_calc[] hp_calc_table = { 
            new hp_calc(8, 0, 0x48, 2), // Cleric
            new hp_calc(8, 0, 0x70, 0), // Druid
            new hp_calc(10, 0, 0x5A, 3), // Fighter
            new hp_calc(10, 0, 0x5A, 3), // Paladin
            new hp_calc(8, 1, 0x58, 2), // Ranger
            new hp_calc(4, 0, 0x2c, 1), // Magic User
            new hp_calc(6, 0, 0x3c, 2), // Thief
            new hp_calc(4, 1, 0x48, 0), // Monk
        };

        internal static int calc_max_hp(Player player) /* sub_50793 */
        {
            int class_count = 0;
            int max_hp = 0;

            for (int class_index = 0; class_index <= 7; class_index++)
            {
                if (player.class_lvls[class_index] > 0)
                {
                    hp_calc hpt = hp_calc_table[class_index];

                    int var_4 = con_bonus((ClassId)class_index);

                    if (player.class_lvls[class_index] < gbl.max_class_levels[class_index])
                    {
                        class_count++;
                        max_hp += (var_4 + hpt.dice) * (player.class_lvls[class_index] + hpt.lvl_bonus);
                    }
                    else
                    {
                        class_count++;
                        int over_count = (player.class_lvls[class_index] - gbl.max_class_levels[class_index]) + 1;

                        max_hp = hpt.max_base + (over_count * hpt.max_mult);
                    }
                }
            }

            max_hp /= class_count;

            return max_hp;
        }

        static byte[] /* seg600:081A */ unk_16B2A = { 1, 1, 1, 1, 2, 1, 1, 2 };
        static byte[] /* seg600:0822 */ unk_16B32 = { 8, 8, 0xA, 0xA, 8, 4, 6, 4 };
        static byte[] /* seg600:3EAA unk_1A1BA */ classMasks = { 2, 2, 8, 0x10, 0x20, 1, 4, 4 };


        internal static byte sub_509E0(byte arg_0, Player player)
        {
            byte var_4 = 0;

            for (int _class = 0; _class <= 7; _class++)
            {
                if (player.class_lvls[_class] > 0 &&
                    (classMasks[_class] & arg_0) != 0)
                {
                    if (player.class_lvls[_class] < gbl.max_class_levels[_class])
                    {
                        int var_5 = unk_16B2A[_class];

                        if (player.class_lvls[_class] > 1)
                        {
                            var_5 = 1;
                        }

                        byte var_2 = ovr024.roll_dice(unk_16B32[_class], var_5);
                        byte var_3 = ovr024.roll_dice(unk_16B32[_class], var_5);

                        if (var_3 > var_2)
                        {
                            var_2 = var_3;
                        }

                        var_4 += var_2;
                    }
                    else
                    {
                        if (_class == 2 || _class == 3)
                        {
                            var_4 = 3;
                        }
                        else if (_class == 4 || _class == 0 || _class == 6)
                        {
                            var_4 = 2;
                        }
                        else if (_class == 5)
                        {
                            var_4 = 1;
                        }
                    }
                }
            }

            return var_4;
        }


        internal static void subtract_gold(Player player, int gold)
        {
            int coppers = gold * 200;

            byte var_3 = 0;

            while (coppers > 0)
            {
                short var_2 = (short)((coppers / money.per_copper[var_3]) + 1);

                if (player.Money[var_3] < var_2)
                {
                    var_2 = player.Money[var_3];
                }

                coppers -= money.per_copper[var_3] * var_2;

                player.Money[var_3] -= var_2;

                var_3 += 1;
            }

            if (coppers < 0)
            {
                coppers = System.Math.Abs(coppers);
                var_3 = 4;

                while (coppers > 0)
                {
                    short var_2 = (short)(coppers / money.per_copper[var_3]);
                    coppers -= money.per_copper[var_3] * var_2;

                    player.Money[var_3] += var_2;
                    var_3 -= 1;
                }
            }
        }

        internal static int[,] exp_table = { /* seg600:4293 unk_1A5A3 */ 
            /* Cleric */    { 0, 1501, 3001,  6001, 13001, 27501, 55001, 110001, 225001, 450001, -1, -1, -1 },
            /* Druid */     { 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 },
            /* Fighter */   { 0, 2001, 4001,  8001, 18001, 35001, 70001, 125001, 250001, 500001,  750001, 1000001, -1  },
            /* Paladin */   { 0, 2751, 5501, 12001, 24001, 45001, 95001, 175001, 350001, 700001, 1050001, -1, -1 },
            /* Ranger */    { 0, 2251, 4501, 10001, 20001, 40001, 90001, 150001, 225001, 325001,  650001, -1, -1 },
            /* MU */        { 0, 2501, 5001, 10001, 22501, 40001, 60001,  90001, 135001, 250001,  375001, -1, -1 }, 
            /* Thief */     { 0, 1251, 2501,  5001, 10001, 20001, 42501,  70001, 110001, 160001,  220001, 440001, -1}, 
            /* Monk */      { 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 }};

        internal static void train_player()
        {
            if (gbl.player_ptr.health_status != Status.okey &&
                Cheats.free_training == false)
            {
                seg041.DisplayStatusText(0, 14, "we only train conscious people");
                return;
            }

            if (ovr020.getPlayerGold(gbl.player_ptr) < 1000 &&
                Cheats.free_training == false &&
                gbl.byte_1B2F1 == 0 &&
                gbl.gameWon == false)
            {
                seg041.DisplayStatusText(0, 14, "Training costs 1000 gp.");
                return;
            }


            byte var_A = 0;
            byte classesToTrainMask = 0;
            byte class_lvl = 123; /* Simeon */

            byte training_class_mask = gbl.area2_ptr.training_class_mask;
            Player player_ptr = gbl.player_ptr;

            int var_5 = 0;

            for (int _class = 0; _class <= 7; _class++)
            {
                if (player_ptr.class_lvls[_class] > 0)
                {
                    classesToTrainMask += classMasks[_class];
                    class_lvl = player_ptr.class_lvls[_class];

                    bool race_limited = RaceClassLimit(class_lvl, player_ptr, (ClassId)_class);

                    if (race_limited == false || 
                        Cheats.no_race_class_limits == true)
                    {
                        if ((exp_table[_class, class_lvl] > 0) &&
                            (exp_table[_class, class_lvl] <= player_ptr.exp ||
                             Cheats.free_training == true))
                        {
                            if (Cheats.free_training == true)
                            {
                                if (exp_table[_class, class_lvl] > 0)
                                {
                                    if (exp_table[_class, class_lvl] > player_ptr.exp)
                                    {
                                        player_ptr.exp = exp_table[_class, class_lvl];
                                    }
                                }
                            }

                            var_A += classMasks[_class];

                            int next_lvl_exp = exp_table[_class, (class_lvl + 1)];

                            if (next_lvl_exp > 0)
                            {
                                if (player_ptr.exp >= next_lvl_exp &&
                                    next_lvl_exp > var_5)
                                {
                                    var_5 = next_lvl_exp - 1;
                                }
                            }
                        }
                    }
                }
            }

            if (gbl.byte_1B2F1 == 0)
            {
                int max_class = 0;
                int max_exp = 0;

                for (int _class = 0; _class <= 7; _class++)
                {
                    if ((classMasks[_class] & var_A) != 0)
                    {
                        if (exp_table[_class, class_lvl] > max_exp)
                        {
                            max_exp = exp_table[_class, class_lvl];
                            max_class = _class;
                        }
                    }
                }


                if (max_exp > 0)
                {
                    var_A = classMasks[max_class];
                    int var_9 = exp_table[max_class, class_lvl + 1];

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
                if ((classesToTrainMask & training_class_mask) == 0 &&
                    gbl.byte_1B2F1 == 0)
                {
                    seg041.DisplayStatusText(0, 14, "We don't train that class here");
                    return;
                }

                if ((var_A & training_class_mask) == 0)
                {
                    gbl.byte_1D8B0 = 1;

                    if (gbl.byte_1B2F1 == 0)
                    {
                        seg041.DisplayStatusText(0, 14, "Not Enough Experience");
                        return;
                    }
                }
            }

            byte var_C;
            if (Cheats.free_training == false)
            {
                var_C = (byte)(var_A & training_class_mask);
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

                int y_offset = 4;

                ovr025.displayPlayerName(false, y_offset, 4, gbl.player_ptr);

                seg041.displayString(" will become:", 0, 10, y_offset, player_ptr.name.Length + 4);

                for (int var_13 = 0; var_13 <= 7; var_13++)
                {
                    if (player_ptr.class_lvls[var_13] > 0 &&
                        (classMasks[var_13] & var_C) != 0)
                    {
                        y_offset++;

                        if (y_offset == 5)
                        {
                            string text = System.String.Format("    a level {0} {1}",
                                player_ptr.class_lvls[var_13] + 1, ovr020.classString[var_13]);

                            seg041.displayString(text, 0, 10, y_offset, 6);
                        }
                        else
                        {
                            string text = System.String.Format("and a level {0} {1}",
                                player_ptr.class_lvls[var_13] + 1, ovr020.classString[var_13]);

                            seg041.displayString(text, 0, 10, y_offset, 6);
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

                byte class_count = 0;
                byte var_17 = player_ptr.magic_user_lvl;
                byte var_18 = player_ptr.ranger_lvl;
                player_ptr.classFlags = 0;

                for (int _class = 0; _class <= 7; _class++)
                {
                    if (player_ptr.class_lvls[_class] > 0)
                    {
                        class_count++;

                        if ((classMasks[_class] & var_C) != 0)
                        {
                            player_ptr.class_lvls[_class] += 1;
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
                        short var_1C = -1;
                        byte var_1A;
                        bool var_1D;

                        do
                        {
                            var_1A = ovr020.spell_menu2(out var_1D, ref var_1C, SpellSource.Learn, SpellLoc.choose);
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

                short var_F = sub_509E0(var_C, gbl.player_ptr);

                int max_hp_increase = var_F / class_count;

                if (max_hp_increase == 0)
                {
                    max_hp_increase = 1;
                }

                player_ptr.field_12C += (byte)max_hp_increase;

                int var_15 = get_con_hp_adj(gbl.player_ptr);

                max_hp_increase = (var_F + var_15) / class_count;

                if (max_hp_increase < 1)
                {
                    max_hp_increase = 1;
                }

                int hp_lost = player_ptr.hit_point_max - player_ptr.hit_point_current;

                player_ptr.hit_point_max += (byte)max_hp_increase;
                player_ptr.hit_point_current = (byte)(player_ptr.hit_point_max - hp_lost);
            }
        }

        private static bool RaceClassLimit(int class_lvl, Player player, ClassId _class )
        {
            bool race_limited = false;

            switch (player.race)
            {
                case Race.dwarf:
                    if (_class == ClassId.fighter)
                    {
                        if (class_lvl == 9 ||
                            (class_lvl == 8 && player.strength == 17) ||
                            (class_lvl == 7 && player.strength < 17))
                        {
                            race_limited = true;
                        }
                    }
                    break;

                case Race.elf:
                    if (_class == ClassId.fighter)
                    {
                        if (class_lvl == 7 ||
                            (class_lvl == 6 && player.strength == 17) ||
                            (class_lvl == 5 && player.strength < 17))
                        {
                            race_limited = true;
                        }
                    }

                    if (_class == ClassId.magic_user)
                    {
                        if (class_lvl == 11 ||
                            (class_lvl == 9 && player._int < 17) ||
                            (class_lvl == 10 && player._int == 17))
                        {
                            race_limited = true;
                        }
                    }
                    break;

                case Race.gnome:
                    if (_class == ClassId.fighter)
                    {
                        if (class_lvl == 6 ||
                            (class_lvl == 5 && player.strength < 18))
                        {
                            race_limited = true;
                        }
                    }
                    break;

                case Race.half_elf:
                    if (_class == ClassId.cleric && class_lvl == 5)
                    {
                        race_limited = true;
                    }
                    else
                    {
                        if (_class == ClassId.fighter || _class == ClassId.ranger)
                        {
                            if (class_lvl == 8 ||
                                (class_lvl == 7 && player.strength == 17) ||
                                (class_lvl == 6 && player.strength < 17))
                            {
                                race_limited = true;
                            }
                        }

                        if (_class == ClassId.magic_user)
                        {
                            if (class_lvl == 8 ||
                                (class_lvl == 7 && player.strength == 17) ||
                                (class_lvl == 6 && player.strength < 17))
                            {
                                race_limited = true;
                            }
                        }
                    }
                    break;

                case Race.halfling:
                    if (_class == ClassId.fighter)
                    {
                        if (class_lvl == 6 ||
                            (class_lvl == 5 && player.strength == 17) ||
                            (class_lvl == 4 && player.strength < 17))
                        {
                            race_limited = true;
                        }
                    }

                    break;

            }

            return race_limited;
        }
    }
}
