using Classes;
using System.Collections.Generic;

namespace engine
{
    class ovr018
    {
        static Set unk_4C13D = new Set(0x0802, new byte[] { 0x80, 0x80 });
        static Set unk_4C15D = new Set(0x0803, new byte[] { 0x20, 0x00, 0x08 });

        internal static void FreePlayer(Player player) // free_player
        {
            if (player.actions != null)
            {
                player.actions = null;
            }

            player.items.Clear();
            player.affects.Clear();
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
            var gameStateBackup = gbl.game_state;
            gbl.game_state = 0;
            bool reclac_menus = true;

            while (true)
            {
                if (reclac_menus == true)
                {
                    seg037.DrawFrame_Outer();
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
                            ovr026.IsHuman(gbl.player_ptr) &&
                            ovr026.HumanFirstOldClass_Unknown(gbl.player_ptr) == ClassId.unknown)
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

                    reclac_menus = false;
                }

                bool controlKey;

                char inputkey = ovr027.displayInput(out controlKey, false, 1, 0, 0, 13, "C D M T H V A R L S B E J", "Choose a function ");

                ovr027.ClearPromptArea();

                if (controlKey == true)
                {
                    if (unk_4C13D.MemberOf(inputkey) == true)
                    {
                        bool var_11 = (ovr026.IsHuman(gbl.player_ptr) && ovr026.HumanFirstOldClass_Unknown(gbl.player_ptr) == ClassId.unknown);

                        ovr020.scroll_team_list(inputkey);
                        ovr025.PartySummary(gbl.player_ptr);

                        if (ovr026.IsHuman(gbl.player_ptr) == false ||
                            ovr026.HumanFirstOldClass_Unknown(gbl.player_ptr) != ClassId.unknown)
                        {
                            var_11 ^= false;
                        }
                        else
                        {
                            var_11 ^= true;
                        }

                        reclac_menus = var_11 && gbl.area2_ptr.training_class_mask > 0;
                    }
                }
                else
                {
                    if (unk_4C15D.MemberOf(inputkey) == false)
                    {
                        gbl.gameSaved = false;
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
                                ovr020.viewPlayer();
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
                                if (gbl.player_ptr.control_morale < Control.NPC_Base)
                                {
                                    ovr017.SavePlayer(string.Empty, gbl.player_ptr);
                                    gbl.player_ptr = FreeCurrentPlayer(gbl.player_ptr, true, false);
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

                                    if (gbl.reload_ecl_and_pictures == false &&
                                        gbl.lastDaxBlockId != 0x50)
                                    {
                                        if (gbl.game_state == GameState.WildernessMap)
                                        {
                                            seg037.DrawFrame_WildernessMap();
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

                                    ovr027.ClearPromptArea();
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
                                        gbl.gameSaved == false)
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

                    reclac_menus = true;
                }
            }
        }

        internal static byte[] /*seg600:3EA2 */ unk_1A1B2 = { 0x02, 0x10, 0x08, 0x40, 0x40, 0x01, 0x04, 0x20 };

        //static byte[] /*seg600:45B3 */ unk_1A8C3 = { 3, 3, 5, 5, 5, 2, 2, 5 };
        //static byte[] /*seg600:45B4 */ unk_1A8C4 = { 6, 6, 4, 4, 4, 4, 6, 4 };

        internal static sbyte[,] /* seg600:3E3A unk_1A14A */ thac0_table = { 
            {40, 40, 40, 40, 0x2A, 0x2A, 0x2A, 0x2C, 0x2C, 0x2C, 0x2E, 0x2E, 0x2E},
            {40, 40, 40, 40, 0x2A, 0x2A, 0x2A, 0x2C, 0x2C, 0x2C, 0x2E, 0x2E, 0x2E},
            {0x27, 40, 40, 0x2A, 0x2B, 0x2C, 0x2D, 0x2E, 0x2F, 0x30, 0x31, 0x32, 0x33},
            {40, 40, 40, 0x2A, 0x2B, 0x2C, 0x2D, 0x2E, 0x2F, 0x30, 0x31, 0x32, 0x33},
            {40, 40, 40, 0x2A, 0x2B, 0x2C, 0x2D, 0x2E, 0x2F, 0x30, 0x31, 0x32, 0x33},
            {0x27, 0x27, 0x27, 0x27, 0x27, 0x27, 0x29, 0x29, 0x29, 0x29, 0x29, 0x2B, 0x2B},
            {40, 40, 40, 40, 40, 0x29, 0x29, 0x29, 0x29, 0x2C, 0x2C, 0x2C, 0x2C},
            {40, 40, 40, 40, 0x2A, 0x2A, 0x2A, 0x2C, 0x2C, 0x2C, 0x2E, 0x2E, 0x2E} };


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
            bool menuRedraw;
            bool showExit;
            byte var_20;
            short var_1E;
            byte var_1B;

            char input_key;
            byte stat_value;
            byte var_14;
            int index;
            MenuItem selected;

            Player player = new Player();

            for (int i = 0; i < 6; i++)
            {
                player.icon_colours[i] = (byte)(((gbl.default_icon_colours[i] + 8) << 4) + gbl.default_icon_colours[i]);
            }

            player.base_ac = 50;
            player.thac0 = 40;
            player.health_status = Status.okey;
            player.in_combat = true;
            player.field_DE = 1;
            player.mod_id = (byte)seg051.Random(256);
            player.icon_id = 0x0A;

            List<MenuItem> var_C = new List<MenuItem>();
            var_C.Add(new MenuItem("Pick Race", true));

            var_C.Add(new MenuItem("  " + ovr020.raceString[1]));
            var_C.Add(new MenuItem("  " + ovr020.raceString[2]));
            var_C.Add(new MenuItem("  " + ovr020.raceString[3]));
            var_C.Add(new MenuItem("  " + ovr020.raceString[4]));
            var_C.Add(new MenuItem("  " + ovr020.raceString[5]));
            var_C.Add(new MenuItem("  " + ovr020.raceString[7]));

            index = 1;
            menuRedraw = true;
            showExit = true;

            do
            {
                input_key = ovr027.sl_select_item(out selected, ref index, ref menuRedraw, showExit, var_C,
                    22, 38, 2, 1, 15, 10, 13, "Select", string.Empty);

                if (input_key == '\0')
                {
                    var_C.Clear();
                    return;
                }
            } while (input_key != 'S');

            if (index == 6)
            {
                index++;
            }

            player.race = (Race)index;

            switch (player.race)
            {
                case Race.halfling:
                    player.icon_size = 1;
                    ovr024.add_affect(false, 0xff, 0, Affects.con_saving_bonus, player);
                    break;

                case Race.dwarf:
                    player.icon_size = 1;
                    ovr024.add_affect(false, 0xff, 0, Affects.con_saving_bonus, player);
                    ovr024.add_affect(false, 0xff, 0, Affects.dwarf_vs_orc, player);
                    ovr024.add_affect(false, 0xff, 0, Affects.dwarf_and_gnome_vs_giants, player);
                    break;

                case Race.gnome:
                    player.icon_size = 1;
                    ovr024.add_affect(false, 0xff, 0, Affects.con_saving_bonus, player);
                    ovr024.add_affect(false, 0xff, 0, Affects.gnome_vs_man_sized_giant, player);
                    ovr024.add_affect(false, 0xff, 0, Affects.dwarf_and_gnome_vs_giants, player);
                    ovr024.add_affect(false, 0xff, 0, Affects.affect_30, player);
                    break;

                case Race.elf:
                    player.icon_size = 2;
                    ovr024.add_affect(false, 0xff, 0, Affects.elf_resist_sleep, player);

                    break;

                case Race.half_elf:
                    player.icon_size = 2;
                    ovr024.add_affect(false, 0xff, 0, Affects.halfelf_resistance, player);
                    break;

                default:
                    player.icon_size = 2;
                    break;
            }

            /* Gender */

            var_C.Clear();

            var_C.Add(new MenuItem("Pick Gender", true));
            var_C.Add(new MenuItem("  " + ovr020.sexString[0]));
            var_C.Add(new MenuItem("  " + ovr020.sexString[1]));

            index = 1;
            showExit = true;
            menuRedraw = true;

            do
            {
                input_key = ovr027.sl_select_item(out selected, ref index, ref menuRedraw, showExit, var_C,
                    22, 38, 2, 1, 15, 10, 13, "Select", string.Empty);

                if (input_key == '\0')
                {
                    var_C.Clear();
                    player = null;
                    return;
                }

            } while (input_key != 'S');


            player.sex = (byte)(index - 1);
            var_C.Clear();

            var_C.Add(new MenuItem("Pick Class", true));

            var ClassList = gbl.RaceClasses[(int)player.race];
            if (player.race != Race.human && Cheats.no_race_class_restrictions)
            {
                ClassList = gbl.RaceClasses[(int)Race.human + 1];
            }

            foreach (var _class in ClassList)
            {
                var_C.Add(new MenuItem("  " + ovr020.classString[(int)_class]));
            }

            index = 1;
            showExit = true;
            menuRedraw = true;

            do
            {
                input_key = ovr027.sl_select_item(out selected, ref index, ref menuRedraw, showExit, var_C,
                    22, 38, 2, 1, 15, 10, 13, "Select", string.Empty);

                if (input_key == '\0')
                {
                    var_C.Clear();
                    player = null;
                    return;
                }
            } while (input_key != 'S');

            player.exp = 25000;
            player._class = ClassList[index-1];
            player.HitDice = 1;

            if (player._class >= ClassId.cleric && player._class <= ClassId.fighter)
            {
                player.ClassLevel[(int)player._class] = 1;
            }
            else if (player._class >= ClassId.magic_user && player._class <= ClassId.monk)
            {
                player.ClassLevel[(int)player._class] = 1;
            }
            else if (player._class == ClassId.paladin)
            {
                player.paladinCuresLeft = 1;
                player.paladin_lvl = 1;
                ovr024.add_affect(false, 0xff, 0, Affects.protection_from_evil, player);
            }
            else if (player._class == ClassId.ranger)
            {
                player.ranger_lvl = 1;
                ovr024.add_affect(false, 0xff, 0, Affects.ranger_vs_giant, player);
            }
            else if (player._class == ClassId.mc_c_f)
            {
                player.cleric_lvl = 1;
                player.fighter_lvl = 1;
                player.exp = 12500;
            }
            else if (player._class == ClassId.mc_c_f_m)
            {
                player.cleric_lvl = 1;
                player.fighter_lvl = 1;
                player.magic_user_lvl = 1;
                player.exp = 8333;
            }
            else if (player._class == ClassId.mc_c_r)
            {
                player.cleric_lvl = 1;
                player.ranger_lvl = 1;
                ovr024.add_affect(false, 0xff, 0, Affects.ranger_vs_giant, player);
                player.exp = 12500;
            }
            else if (player._class == ClassId.mc_c_mu)
            {
                player.cleric_lvl = 1;
                player.magic_user_lvl = 1;
                player.exp = 12500;
            }
            else if (player._class == ClassId.mc_c_t)
            {
                player.cleric_lvl = 1;
                player.thief_lvl = 1;
                player.exp = 12500;
            }
            else if (player._class == ClassId.mc_f_mu)
            {
                player.fighter_lvl = 1;
                player.magic_user_lvl = 1;
                player.exp = 12500;
            }
            else if (player._class == ClassId.mc_f_t)
            {
                player.fighter_lvl = 1;
                player.thief_lvl = 1;
                player.exp = 12500;
            }
            else if (player._class == ClassId.mc_f_mu_t)
            {
                player.fighter_lvl = 1;
                player.magic_user_lvl = 1;
                player.thief_lvl = 1;
                player.exp = 8333;
            }
            else if (player._class == ClassId.mc_mu_t)
            {
                player.magic_user_lvl = 1;
                player.thief_lvl = 1;
                player.exp = 8333;
            }

            if (player.thief_lvl > 0)
            {
                ovr026.sub_6AAEA(player);
            }

            player.classFlags = 0;
            player.thac0 = 0;

            for (int class_idx = 0; class_idx <= 7; class_idx++)
            {
                if (player.ClassLevel[class_idx] > 0)
                {
                    int skill_lvl = player.ClassLevel[class_idx];

                    if (thac0_table[class_idx, skill_lvl] > player.thac0)
                    {
                        player.thac0 = thac0_table[class_idx, skill_lvl];
                    }

                    player.classFlags += unk_1A1B2[class_idx];
                }
            }

            ovr026.sub_6A7FB(player);
            var_C.Clear();

            int alignments = gbl.class_alignments[(int)player._class, 0];

            var_C.Add(new MenuItem("Pick Alignment", true)); 
                
            for (int i = 1; i <= alignments; i++)
            {
                var_C.Add(new MenuItem("  " + ovr020.alignmentString[gbl.class_alignments[(int)player._class, i]]));
            }

            index = 1;
            showExit = true;
            menuRedraw = true;

            do
            {
                input_key = ovr027.sl_select_item(out selected, ref index, ref menuRedraw, showExit, var_C,
                    22, 38, 2, 1, 15, 10, 13, "Select", string.Empty);


                if (input_key == '\0')
                {
                    var_C.Clear();

                    seg051.FreeMem(Player.StructSize, player);
                    return;
                }
            } while (input_key != 'S');

            player.alignment = gbl.class_alignments[(int)player._class, index];

            var_C.Clear();

            if (player._class <= ClassId.monk)
            {
                SubStruct_1A35E v5 = gbl.race_ages[(int)player.race][player._class];

                player.age = (short)(ovr024.roll_dice(v5.dice_size, v5.dice_count) + v5.base_age);
            }
            else
            {
                int race = (int)player.race;

                switch (player._class)
                {
                    case ClassId.mc_c_f:
                    case ClassId.mc_c_f_m:
                    case ClassId.mc_c_t:
                    case ClassId.mc_c_r:
                        player.age = (short)(gbl.race_ages[race][0].base_age + (gbl.race_ages[race][0].dice_count * gbl.race_ages[race][0].dice_size));
                        break;

                    case ClassId.mc_f_mu:
                    case ClassId.mc_f_mu_t:
                    case ClassId.mc_mu_t:
                        player.age = (short)(gbl.race_ages[race][6].base_age + (gbl.race_ages[race][6].dice_count * gbl.race_ages[race][6].dice_size));
                        break;

                    case ClassId.mc_f_t:
                        player.age = (short)(gbl.race_ages[race][2].base_age + (gbl.race_ages[race][2].dice_count * gbl.race_ages[race][2].dice_size));
                        break;
                }
            }

            Player gblPlayerPtrBkup = gbl.player_ptr;
            gbl.player_ptr = player;
            ovr020.playerDisplayFull(player);

            do
            {
                for (int class_idx = 0; class_idx <= 7; class_idx++)
                {
                    if (player.ClassLevel[class_idx] > 0)
                    {
                        player.ClassLevel[class_idx] = 1;
                    }
                }

                player.tmp_str_00 = 0;

                for (var_1B = 0; var_1B <= 5; var_1B++)
                {
                    player.stats[var_1B].max = 0;

                    for (var_14 = 1; var_14 <= 6; var_14++)
                    {
                        stat_value = (byte)(ovr024.roll_dice(6, 3) + 1);

                        switch (7) // was loop1_var but that's always 7 after the race building loop.
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

                    switch ((Stat)var_1B)
                    {
                        case Stat.STR:
                            if (player.stats[var_1B].max > 0 &&
                                race_age_brackets[player.race].age_0 < player.age)
                            {
                                player.stats[var_1B].max += 1;
                            }

                            if (player.stats[var_1B].max > 0 &&
                                race_age_brackets[player.race].age_1 < player.age)
                            {
                                player.stats[var_1B].max -= 1;
                            }

                            if (player.stats[var_1B].max > 0 &&
                                race_age_brackets[player.race].age_2 < player.age)
                            {
                                player.stats[var_1B].max -= 2;
                            }

                            if (player.stats[var_1B].max > 0 &&
                                race_age_brackets[player.race].age_3 < player.age)
                            {
                                player.stats[var_1B].max -= 1;
                            }

                            if (player.stats[var_1B].max < racial_stats_limits[(int)player.race].str_min[player.sex])
                            {
                                player.stats[var_1B].max = racial_stats_limits[(int)player.race].str_min[player.sex];
                            }

                            if (player.stats[var_1B].max > racial_stats_limits[(int)player.race].str_max[player.sex])
                            {
                                player.stats[var_1B].max = racial_stats_limits[(int)player.race].str_max[player.sex];
                            }

                            if (player.stats[var_1B].max < gbl.class_stats_min[(int)player._class][var_1B])
                            {
                                player.stats[var_1B].max = gbl.class_stats_min[(int)player._class][var_1B];
                            }

                            if (player.strength == 18)
                            {
                                if (player.fighter_lvl > 0 ||
                                    player.ranger_lvl > 0 ||
                                    player.paladin_lvl > 0)
                                {
                                    player.tmp_str_00 = (byte)(seg051.Random(100) + 1);

                                    if (player.tmp_str_00 > racial_stats_limits[(int)player.race].str_100_max[player.sex])
                                    {
                                        player.tmp_str_00 = racial_stats_limits[(int)player.race].str_100_max[player.sex];
                                    }
                                }
                            }
                            break;

                        case Stat.INT:

                            if (race_age_brackets[player.race].age_1 < player.age)
                            {
                                player.stats[var_1B].max += 1;
                            }

                            if (race_age_brackets[player.race].age_3 < player.age)
                            {
                                player.stats[var_1B].max += 1;
                            }

                            if (player.stats[var_1B].max < racial_stats_limits[(int)player.race].int_min)
                            {
                                player.stats[var_1B].max = racial_stats_limits[(int)player.race].int_min;
                            }

                            if (player.stats[var_1B].max > racial_stats_limits[(int)player.race].int_max)
                            {
                                player.stats[var_1B].max = racial_stats_limits[(int)player.race].int_max;
                            }

                            if (player.stats[var_1B].max < gbl.class_stats_min[(int)player._class][var_1B])
                            {
                                player.stats[var_1B].max = gbl.class_stats_min[(int)player._class][var_1B];
                            }
                            break;

                        case Stat.WIS:
                            if (race_age_brackets[player.race].age_0 >= player.age)
                            {
                                player.stats[var_1B].max -= 1;
                            }

                            if (race_age_brackets[player.race].age_1 < player.age)
                            {
                                player.stats[var_1B].max += 1;
                            }

                            if (race_age_brackets[player.race].age_2 < player.age)
                            {
                                player.stats[var_1B].max += 1;
                            }

                            if (race_age_brackets[player.race].age_3 < player.age)
                            {
                                player.stats[var_1B].max += 1;
                            }

                            if (player.stats[var_1B].max < racial_stats_limits[(int)player.race].wis_min)
                            {
                                player.stats[var_1B].max = racial_stats_limits[(int)player.race].wis_min;
                            }

                            if (player.stats[var_1B].max > racial_stats_limits[(int)player.race].wis_max)
                            {
                                player.stats[var_1B].max = racial_stats_limits[(int)player.race].wis_max;
                            }

                            if (player.stats[var_1B].max < gbl.class_stats_min[(int)player._class][var_1B])
                            {
                                player.stats[var_1B].max = gbl.class_stats_min[(int)player._class][var_1B];
                            }

                            if (player.stats[var_1B].max < 13 &&
                                player._class >= ClassId.mc_c_f && player._class <= ClassId.mc_c_t)
                            {
                                // Multi-Class Cleric
                                player.stats[var_1B].max = 13;
                            }
                            break;

                        case Stat.DEX:
                            if (race_age_brackets[player.race].age_2 < player.age)
                            {
                                player.stats[var_1B].max -= 2;
                            }

                            if (race_age_brackets[player.race].age_3 < player.age)
                            {
                                player.stats[var_1B].max -= 2;
                            }

                            if (player.stats[var_1B].max < racial_stats_limits[(int)player.race].dex_min)
                            {
                                player.stats[var_1B].max = racial_stats_limits[(int)player.race].dex_min;
                            }

                            if (player.stats[var_1B].max > racial_stats_limits[(int)player.race].dex_max)
                            {
                                player.stats[var_1B].max = racial_stats_limits[(int)player.race].dex_max;
                            }

                            if (player.stats[var_1B].max < gbl.class_stats_min[(int)player._class][var_1B])
                            {
                                player.stats[var_1B].max = gbl.class_stats_min[(int)player._class][var_1B];
                            }
                            break;

                        case Stat.CON:
                            if (race_age_brackets[player.race].age_1 < player.age)
                            {
                                player.stats[var_1B].max -= 1;
                            }

                            if (race_age_brackets[player.race].age_2 < player.age)
                            {
                                player.stats[var_1B].max -= 1;
                            }

                            if (race_age_brackets[player.race].age_3 < player.age)
                            {
                                player.stats[var_1B].max -= 1;
                            }

                            if (player.stats[var_1B].max < racial_stats_limits[(int)player.race].con_min)
                            {
                                player.stats[var_1B].max = racial_stats_limits[(int)player.race].con_min;
                            }

                            if (player.stats[var_1B].max > racial_stats_limits[(int)player.race].con_max)
                            {
                                player.stats[var_1B].max = racial_stats_limits[(int)player.race].con_max;
                            }

                            if (player.stats[var_1B].max < gbl.class_stats_min[(int)player._class][var_1B])
                            {
                                player.stats[var_1B].max = gbl.class_stats_min[(int)player._class][var_1B];
                            }
                            break;

                        case Stat.CHA:
                            if (player.stats[var_1B].max < racial_stats_limits[(int)player.race].cha_min)
                            {
                                player.stats[var_1B].max = racial_stats_limits[(int)player.race].cha_min;
                            }

                            if (player.stats[var_1B].max > racial_stats_limits[(int)player.race].cha_max)
                            {
                                player.stats[var_1B].max = racial_stats_limits[(int)player.race].cha_max;
                            }

                            if (player.stats[var_1B].max < gbl.class_stats_min[(int)player._class][var_1B])
                            {
                                player.stats[var_1B].max = gbl.class_stats_min[(int)player._class][var_1B];
                            }
                            break;
                    }

                    ovr020.display_stat(false, var_1B);
                }

                player.hit_point_current = player.hit_point_max;
                player.attacksCount = 2;
                player.attack1_DiceCountBase = 1;
                player.attack1_DiceSizeBase = 2;
                player.field_125 = 1;
                player.base_movement = 12;
                var_20 = 0;

                for (int i = 0; i < 5; i++)
                {
                    player.field_12D[0, i] = 0;
                    player.field_12D[1, i] = 0;
                    player.field_12D[2, i] = 0;
                }

                for (int class_idx = 0; class_idx <= 7; class_idx++)
                {
                    if (player.ClassLevel[class_idx] > 0)
                    {
                        if (class_idx == 0)
                        {
                            player.field_12D[0, 0] = 1;
                        }
                        else if (class_idx == 5)
                        {
                            player.field_12D[2, 0] = 1;
                        }

                        //var_21 += ovr024.roll_dice(unk_1A8C4[class_idx], unk_1A8C3[class_idx]);
                        //TODO this was not used in original code.

                        if (class_idx == 0)
                        {
                            ovr026.calc_cleric_spells(false, player);

                            foreach (Spells spell in System.Enum.GetValues(typeof(Spells)))
                            {
                                SpellEntry stru = gbl.spell_table[(int)spell];

                                if (stru.spellClass == 0 && stru.spellLevel == 1)
                                {
                                    player.LearnSpell(spell);
                                }
                            }
                        }
                        else if (class_idx == 5)
                        {
                            player.LearnSpell(Spells.detect_magic_MU);
                            player.LearnSpell(Spells.read_magic);
                            player.LearnSpell(Spells.enlarge);
                            player.LearnSpell(Spells.sleep);
                        }

                        var_20++;
                    }
                }

                player.platinum = 300;
                player.field_12C = sub_509E0(0xff, player);
                player.hit_point_max = player.field_12C;

                var_1E = get_con_hp_adj(player);

                if (var_1E < 0)
                {
                    if (player.hit_point_max > (System.Math.Abs(var_1E) + var_20))
                    {
                        player.hit_point_max = (byte)((player.hit_point_max + var_1E) / var_20);
                    }
                    else
                    {
                        player.hit_point_max = 1;
                    }
                }
                else
                {
                    player.hit_point_max = (byte)((player.hit_point_max + var_1E) / var_20);
                }

                player.hit_point_current = player.hit_point_max;
                player.field_12C = (byte)(player.field_12C / var_20);
                byte trainingClassMaskBackup = gbl.area2_ptr.training_class_mask;

                ovr017.SilentTrainPlayer();

                gbl.area2_ptr.training_class_mask = trainingClassMaskBackup;
                bool first_lvl = true;
                string text = string.Empty;

                for (int class_idx = 0; class_idx <= 7; class_idx++)
                {
                    if (player.ClassLevel[class_idx] > 0 ||
                        (player.ClassLevelsOld[class_idx] < ovr026.HumanCurrentClassLevel_Zero(player) &&
                         player.ClassLevelsOld[class_idx] > 0))
                    {
                        if (first_lvl == false)
                        {
                            text += "/";
                        }

                        byte b = player.ClassLevelsOld[class_idx];
                        b += player.ClassLevel[class_idx];

                        text += b.ToString();

                        first_lvl = false;
                    }
                }

                seg041.displayString(text, 0, 15, 15, 7);
                ovr020.display_player_stats01();
                ovr020.displayMoney();

                input_key = ovr027.yes_no(15, 10, 13, "Reroll stats? ");

            } while (input_key != 'N');

            ovr020.playerDisplayFull(player);

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

            input_key = ovr027.yes_no(15, 10, 13, "Save " + player.name + "? ");

            if (input_key == 'N')
            {
                seg051.FreeMem(Player.StructSize, player);
                gbl.player_ptr = gblPlayerPtrBkup;
            }
            else
            {
                ovr017.SavePlayer(string.Empty, player);

                seg051.FreeMem(Player.StructSize, player);
                gbl.player_ptr = gblPlayerPtrBkup;
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

                    ovr017.remove_player_file(player);
                    gbl.player_ptr = FreeCurrentPlayer(gbl.player_ptr, true, false);
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
                gbl.player_ptr.multiclassLevel != 0)
            {
                seg041.DisplayStatusText(0, 14, gbl.player_ptr.name + " can't be modified.");
                return;
            }

            ovr020.playerDisplayFull(gbl.player_ptr);

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

            gbl.player_ptr.npcTreasureShareCount = 1;

            player_ptr = gbl.player_ptr;
            var_8 = 0;
            byte var_40 = 0;

            for (int var_33 = 0; var_33 < 8; var_33++)
            {
                if (player_ptr.ClassLevel[var_33] > 0)
                {
                    if (player_ptr.ClassLevel[var_33] < gbl.max_class_hit_dice[var_33])
                    {
                        if ((ClassId)var_33 == ClassId.ranger)
                        {
                            var_8 += (byte)((player_ptr.ClassLevel[var_33] + 1) * (con_bonus((ClassId)var_33)));
                        }
                        else
                        {
                            var_8 += (byte)(player_ptr.ClassLevel[var_33] * (con_bonus((ClassId)var_33)));
                        }
                    }
                    else
                    {
                        var_8 += (byte)((gbl.max_class_hit_dice[var_33] - 1) * con_bonus((ClassId)var_33));
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

            char input_key = ovr027.displayInput(false, 0, 15, 10, 13, "Curse Pool Hillsfar Exit", "Add from where? ");

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

            List<MenuItem> strList;
            List<MenuItem> nameList;
            ovr017.BuildLoadablePlayersLists(out strList, out nameList);

            if (nameList.Count > 0 )
            {
                int pc_count = 0;

                int strList_index = 0;
                MenuItem select_sl;
                bool menuRedraw = true;

                do
                {
                    bool showExit = true;
                    input_key = ovr027.sl_select_item(out select_sl, ref strList_index, ref menuRedraw, showExit, nameList,
                        22, 38, 2, 1, 15, 10, 13, "Add", "Add a character: ");

                    if ((input_key == 13 || input_key == 'A') &&
                        select_sl.Text[0] != '*')
                    {
                        ovr027.ClearPromptArea();

                        Player new_player = new Player();

                        MenuItem var_10 = ovr027.getStringListEntry(strList, strList_index);

                        ovr017.import_char01(ref new_player, var_10.Text);

                        select_sl.Text = "* " + select_sl.Text;
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

                                if (tmp_player.control_morale < Control.NPC_Base)
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
                                ((new_player.control_morale < Control.NPC_Base && pc_count < 6) ||
                                 (new_player.control_morale >= Control.NPC_Base && gbl.area2_ptr.party_size < 8)) &&
                                (new_player.paladin_lvl == 0 || evil_present == false) &&
                                (new_player.ranger_lvl == 0 || ranger_count < 3) &&
                                (((new_player.alignment + 1) % 3) != 0 || paladin_present == false))
                            {
                                ovr017.AssignPlayerIconId(new_player);
                                ovr017.LoadPlayerCombatIcon(true);

                                if (new_player.control_morale < Control.NPC_Base)
                                {
                                    pc_count++;
                                }
                            }
                            else
                            {
                                select_sl.Text = select_sl.Text.Substring(2);

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

                                new_player = null; // FreeMem( Player.StructSize, player_ptr1 );
                            }
                        }
                    }

                } while (input_key != 0x45 && input_key != '\0' && pc_count <= 5 && gbl.area2_ptr.party_size <= 7);

                nameList.Clear();
            }
        }


        internal static Player FreeCurrentPlayer(Player player, bool free_icon, bool leave_party_size) // free_players
        {
            int index = gbl.player_next_ptr.IndexOf(player);

            if (index >= 0)
            {
                gbl.player_next_ptr.RemoveAt(index);

                if (free_icon)
                {
                    ovr034.ReleaseCombatIcon(player.icon_id);
                }

                if (leave_party_size == false)
                {
                    gbl.area2_ptr.party_size--;
                }

                FreePlayer(player);

                index = index > 0 ? index - 1 : 0;
                if (gbl.player_next_ptr.Count > 0)
                {
                    return gbl.player_next_ptr[index];
                }
            }

            return null;
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
                for (byte i = 0; i < 16; i++)
                {
                    oldColors[i] = i;
                    newColors[i] = i;
                }

                Player player = gbl.player_ptr;
                for (byte i = 0; i < 6; i++)
                {
                    newColors[gbl.default_icon_colours[i]] = (byte)(player.icon_colours[i] & 0x0F);
                    newColors[gbl.default_icon_colours[i] + 8] = (byte)((player.icon_colours[i] & 0xF0) >> 4);
                }

                seg040.DaxBlockRecolor(gbl.combat_icons[destIndex, 0], false, newColors, oldColors);
                seg040.DaxBlockRecolor(gbl.combat_icons[destIndex, 1], false, newColors, oldColors);
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
            byte weaponIcon;
            byte headIcon;
            char inputKey;

            string[] iconStrings = {   "", 
									   "Parts 1st-color 2nd-color Size Exit", 
									   "Head Weapon Exit", 
									   "Weapon Body xxxx Shield Arm Leg Exit", 
									   " Keep Exit", 
									   "Next Prev Keep Exit" };

            seg037.DrawFrame_Outer();
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

                headIcon = player_ptr.head_icon;
                weaponIcon = player_ptr.weapon_icon;
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
                            text = "Small" + iconStrings[4];
                        }
                        else
                        {
                            text = "Large" + iconStrings[4];
                        }
                    }
                    else
                    {
                        text = iconStrings[var_8];
                    }

                    bool specialKey;

                    inputKey = ovr027.displayInput(out specialKey, false, 0, 15, 10, 13, text, string.Empty);

                    if (specialKey == false)
                    {
                        switch (var_8)
                        {
                            case 1:
                                var_1A = 1;

                                switch (inputKey)
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
                                if (unk_4FE94.MemberOf(inputKey) == true)
                                {
                                    var_8 = 1;
                                }
                                else
                                {
                                    var_1B = inputKey;
                                    var_8 = 5;
                                }
                                break;

                            case 3:
                                var_1A = 3;

                                switch (inputKey)
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

                                if (unk_4FE94.MemberOf(inputKey) == true)
                                {
                                    var_8 = 1;
                                }
                                else
                                {
                                    var_8 = 5;
                                }
                                break;

                            case 4:
                                switch (inputKey)
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
                                        inputKey = ' ';
                                        break;

                                    case 'E':
                                        goto case '\0';

                                    case '\0':
                                        player_ptr.icon_size = bkup_size;
                                        var_8 = 1;
                                        inputKey = ' ';
                                        break;
                                }

                                ovr017.LoadPlayerCombatIcon(false);
                                break;

                            case 5:
                                if (var_1A == 2)
                                {
                                    if (var_1B == 'H')
                                    {
                                        if (inputKey == 0x50)
                                        {
                                            player_ptr.head_icon = (byte)Sys.WrapMinMax(player_ptr.head_icon - 1, 0, 13);
                                        }
                                        else if (inputKey == 'N')
                                        {
                                            player_ptr.head_icon = (byte)Sys.WrapMinMax(player_ptr.head_icon + 1, 0, 13);
                                        }
                                        else if (inputKey == 'K')
                                        {
                                            player_ptr2 = gbl.player_ptr;
                                            headIcon = player_ptr2.head_icon;
                                            var_8 = var_1A;
                                            inputKey = ' ';
                                        }
                                        else if (inputKey == 'E' || inputKey == '\0')
                                        {
                                            player_ptr.head_icon = headIcon;
                                            var_8 = var_1A;
                                            inputKey = ' ';
                                        }

                                        ovr017.LoadPlayerCombatIcon(false);
                                    }
                                    else if (var_1B == 'W')
                                    {
                                        if (inputKey == 'P')
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
                                        else if (inputKey == 'N')
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
                                        else if (inputKey == 'K')
                                        {
                                            player_ptr2 = gbl.player_ptr;
                                            weaponIcon = player_ptr2.weapon_icon;
                                            var_8 = var_1A;
                                            inputKey = ' ';
                                        }
                                        else if (inputKey == 'E' || inputKey == '\0')
                                        {
                                            player_ptr.weapon_icon = weaponIcon;
                                            var_8 = var_1A;
                                            inputKey = ' ';
                                        }

                                        ovr017.LoadPlayerCombatIcon(false);
                                    }
                                }
                                else if (var_1A == 3)
                                {
                                    byte low_color = (byte)(player_ptr.icon_colours[color_index] & 0x0F);
                                    byte high_color = (byte)((player_ptr.icon_colours[color_index] & 0xF0) >> 4);

                                    if (inputKey == 'N')
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
                                    else if (inputKey == 'P')
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
                                    else if (inputKey == 'K')
                                    {
                                        System.Array.Copy(player_ptr.icon_colours, bkup_colours, 6);
                                        var_8 = var_1A;
                                        inputKey = ' ';
                                    }
                                    else if (inputKey == 'E' || inputKey == '\0')
                                    {
                                        System.Array.Copy(bkup_colours, player_ptr.icon_colours, 6);
                                        var_8 = var_1A;
                                        inputKey = ' ';
                                    }
                                }
                                break;
                        }
                    }

                    duplicateCombatIcon(true, 12, player_ptr.icon_id);

                } while (var_1A != 0 || unk_4FE94.MemberOf(inputKey) == false);

                player_ptr.head_icon = headIcon;
                player_ptr.weapon_icon = weaponIcon;
                player_ptr.icon_size = bkup_size;

                System.Array.Copy(bkup_colours, player_ptr.icon_colours, 6);

                duplicateCombatIcon(true, 12, player_ptr.icon_id);
                duplicateCombatIcon(false, player_ptr.icon_id, 12);

                ovr027.ClearPromptArea();
                ovr034.ReleaseCombatIcon(12);

                inputKey = ovr027.yes_no(15, 10, 13, "Is this icon ok? ");

            } while (inputKey != 'Y');

            ovr033.Color_0_8_normal();
        }

        /// <summary> seg600:4281 </summary>
        static sbyte[] con_hp_adj = { 0, 0, 0, -2, -1, -1, -1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };

        internal static sbyte get_con_hp_adj(Player player)
        {
            sbyte hp_adj = 0;

            for (int class_index = 0; class_index <= (byte)ClassId.monk; class_index++)
            {
                if (player.ClassLevel[class_index] > 0 &&
                    player.ClassLevel[class_index] < gbl.max_class_hit_dice[class_index])
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
                        player.ClassLevel[class_index] == 1)
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
                if (player.ClassLevel[class_index] > 0)
                {
                    levels_total += player.ClassLevel[class_index] + hp_calc_table[class_index].lvl_bonus;
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
                if (player.ClassLevel[class_index] > 0)
                {
                    hp_calc hpt = hp_calc_table[class_index];

                    int var_4 = con_bonus((ClassId)class_index);

                    if (player.ClassLevel[class_index] < gbl.max_class_hit_dice[class_index])
                    {
                        class_count++;
                        max_hp += (var_4 + hpt.dice) * (player.ClassLevel[class_index] + hpt.lvl_bonus);
                    }
                    else
                    {
                        class_count++;
                        int over_count = (player.ClassLevel[class_index] - gbl.max_class_hit_dice[class_index]) + 1;

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
                if (player.ClassLevel[_class] > 0 &&
                    (classMasks[_class] & arg_0) != 0)
                {
                    if (player.ClassLevel[_class] < gbl.max_class_hit_dice[_class])
                    {
                        int var_5 = unk_16B2A[_class];

                        if (player.ClassLevel[_class] > 1)
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
                short var_2 = (short)((coppers / Money.per_copper[var_3]) + 1);

                if (player.Money[var_3] < var_2)
                {
                    var_2 = player.Money[var_3];
                }

                coppers -= Money.per_copper[var_3] * var_2;

                player.Money[var_3] -= var_2;

                var_3 += 1;
            }

            if (coppers < 0)
            {
                coppers = System.Math.Abs(coppers);
                var_3 = 4;

                while (coppers > 0)
                {
                    short var_2 = (short)(coppers / Money.per_copper[var_3]);
                    coppers -= Money.per_copper[var_3] * var_2;

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
                gbl.silent_training == false &&
                gbl.gameWon == false)
            {
                seg041.DisplayStatusText(0, 14, "Training costs 1000 gp.");
                return;
            }


            byte classesExpTrainMask = 0;
            byte classesToTrainMask = 0;
            byte class_lvl = 123; /* Simeon */

            byte trainerClassMask = gbl.area2_ptr.training_class_mask;
            Player player_ptr = gbl.player_ptr;

            int var_5 = 0;

            for (int _class = 0; _class <= 7; _class++)
            {
                if (player_ptr.ClassLevel[_class] > 0)
                {
                    classesToTrainMask += classMasks[_class];
                    class_lvl = player_ptr.ClassLevel[_class];

                    bool race_limited = RaceClassLimit(class_lvl, player_ptr, (ClassId)_class);

                    if (race_limited == false || 
                        Cheats.no_race_level_limits == true)
                    {
                        if ((exp_table[_class, class_lvl] > 0) &&
                            (exp_table[_class, class_lvl] <= player_ptr.exp ||
                             Cheats.free_training == true))
                        {
                            if (Cheats.free_training == true)
                            {
                                int tmpExp = exp_table[_class, class_lvl];
                                if (tmpExp > 0)
                                {
                                    if (tmpExp > player_ptr.exp)
                                    {
                                        player_ptr.exp = tmpExp;
                                    }
                                }
                            }

                            classesExpTrainMask += classMasks[_class];

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

            if (gbl.silent_training == false)
            {
                int max_class = 0;
                int max_exp = 0;

                for (int _class = 0; _class <= 7; _class++)
                {
                    if ((classMasks[_class] & classesExpTrainMask) != 0)
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
                    classesExpTrainMask = classMasks[max_class];
                    int var_9 = exp_table[max_class, class_lvl + 1];

                    if (var_9 > 0 &&
                        player_ptr.exp >= var_9 &&
                        var_9 > var_5)
                    {
                        var_5 = var_9 - 1;
                    }
                }
            }

            if (var_5 > 0 && gbl.silent_training == false)
            {
                //player_ptr.exp = var_5;
            }

            if (Cheats.free_training == false)
            {
                if ((classesToTrainMask & trainerClassMask) == 0 &&
                    gbl.silent_training == false)
                {
                    seg041.DisplayStatusText(0, 14, "We don't train that class here");
                    return;
                }

                if ((classesExpTrainMask & trainerClassMask) == 0)
                {
                    gbl.can_train_no_more = true;

                    if (gbl.silent_training == false)
                    {
                        seg041.DisplayStatusText(0, 14, "Not Enough Experience");
                        return;
                    }
                }
            }

            byte actualTrainingClassesMask;
            if (Cheats.free_training == false)
            {
                actualTrainingClassesMask = (byte)(classesExpTrainMask & trainerClassMask);
            }
            else
            {
                actualTrainingClassesMask = classesExpTrainMask;
            }

            bool skipBits = false;
            if (gbl.silent_training == true)
            {
                skipBits = true;
            }

            if (skipBits == false)
            {
                seg037.draw8x8_clear_area(0x16, 0x26, 1, 1);

                int y_offset = 4;

                ovr025.displayPlayerName(false, y_offset, 4, gbl.player_ptr);

                seg041.displayString(" will become:", 0, 10, y_offset, player_ptr.name.Length + 4);

                for (int _class = 0; _class <= 7; _class++)
                {
                    if (player_ptr.ClassLevel[_class] > 0 &&
                        (classMasks[_class] & actualTrainingClassesMask) != 0)
                    {
                        y_offset++;

                        if (y_offset == 5)
                        {
                            string text = System.String.Format("    a level {0} {1}",
                                player_ptr.ClassLevel[_class] + 1, ovr020.classString[_class]);

                            seg041.displayString(text, 0, 10, y_offset, 6);
                        }
                        else
                        {
                            string text = System.String.Format("and a level {0} {1}",
                                player_ptr.ClassLevel[_class] + 1, ovr020.classString[_class]);

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
                byte oldMagicUserLvl = player_ptr.magic_user_lvl;
                byte oldRangeLevel = player_ptr.ranger_lvl;
                player_ptr.classFlags = 0;

                for (int _class = 0; _class <= 7; _class++)
                {
                    if (player_ptr.ClassLevel[_class] > 0)
                    {
                        class_count++;

                        if ((classMasks[_class] & actualTrainingClassesMask) != 0)
                        {
                            player_ptr.ClassLevel[_class] += 1;
                            if (player_ptr.field_E7 > 0)
                            {
                                player_ptr.field_E8 -= (byte)(player_ptr.field_E8 / player_ptr.field_E7);
                                player_ptr.field_E7 -= 1;
                            }
                        }
                    }
                }

                ovr026.sub_6A3C6(gbl.player_ptr);

                if (gbl.silent_training == false)
                {
                    if (player_ptr.magic_user_lvl > oldMagicUserLvl ||
                        player_ptr.ranger_lvl > 8)
                    {
                        int index = -1;
                        byte newSpellId;
                        bool var_1D;

                        do
                        {
                            newSpellId = ovr020.spell_menu2(out var_1D, ref index, SpellSource.Learn, SpellLoc.choose);
                        } while (newSpellId <= 0 && var_1D == true);

                        if (newSpellId > 0)
                        {
                            player_ptr.LearnSpell((Spells)newSpellId);
                        }
                    }
                }

                if (gbl.silent_training == true)
                {
                    switch (player_ptr.magic_user_lvl)
                    {
                        case 2:
                            player_ptr.LearnSpell(Spells.magic_missile);
                            break;

                        case 3:
                            player_ptr.LearnSpell(Spells.stinking_cloud);
                            player_ptr.LearnSpell(Spells.protect_from_evil_MU);
                            break;

                        case 4:
                            player_ptr.LearnSpell(Spells.knock);
                            break;

                        case 5:
                            player_ptr.LearnSpell(Spells.fireball);
                            break;
                    }
                }

                if (player_ptr.HitDice <= player_ptr.multiclassLevel)
                {
                    return;
                }

                short var_F = sub_509E0(actualTrainingClassesMask, gbl.player_ptr);

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
