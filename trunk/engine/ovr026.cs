using Classes;
using System.Collections.Generic;

namespace engine
{
    class ovr026
    {
        static byte[,] /*seg600:42BC*/ unk_1A5CC = { 
            {1, 0, 0, 0, 0},
            {0, 1, 0, 0, 0},
            {1, 1, 0, 0, 0},
            {0, 1, 1, 0, 0},
            {0, 0, 1, 0, 0}, 
            {0, 0, 0, 1, 0},    
            {0, 0, 1, 1, 0}, 
            {1, 1, 0, 0, 1},  //seg600:42EE
            {0, 0, 0, 1, 1},  //seg600:42F3
            {1, 0, 1, 0, 0},  //seg600:42F8
            {1, 1, 1, 0, 0},  //seg600:42FD
        };

        static byte[,] /*seg600:43E5*/ unk_1A6F5 = { 
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0}, 
            {0, 0, 0, 0, 0},  
            {0, 0, 0, 0, 0}, 
            {1, 0, 0, 0, 0}, 
            {1, 0, 0, 0, 0}, 
            {0, 1, 0, 0, 0}, 
            {0, 1, 0, 0, 0}
        };

        static byte[,] /*seg600:4448*/ unk_1A758 = { 
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0},
            {1, 0, 0, 0, 0},
            {0, 0, 0, 1, 0},
            {1, 0, 0, 0, 0},
            {0, 0, 0, 1, 0},
            {0, 1, 0, 0, 0} 
        };



        internal static void sub_6A00F(Player player)
        {
            for (int i = 0; i < 5; i++)
            {
                player.spellCastCount[0, i] = 0;
                player.spellCastCount[1, i] = 0;
                player.spellCastCount[2, i] = 0;
            }

            for (SkillType skill = SkillType.Cleric; skill <= SkillType.Monk; skill++)
            {
                int var_2 = player.SkillLevel(skill);

                if (var_2 > 0)
                {
                    switch (skill)
                    {
                        case SkillType.Cleric:
                            player.spellCastCount[0, 0] += 1;

                            for (int sp_class = 0; sp_class <= (var_2 - 2); sp_class++)
                            {
                                for (int sp_lvl = 0; sp_lvl < 5; sp_lvl++)
                                {
                                    player.spellCastCount[0, sp_lvl] += unk_1A5CC[sp_class, sp_lvl];
                                }
                            }

                            calc_cleric_spells(false, player);
                                                            
                            foreach (Spells spell in System.Enum.GetValues(typeof(Spells)))
                            {
                                SpellEntry se = gbl.spell_table[(int)spell];

                                int sp_class = (se.spellLevel - 1) / 5;
                                int sp_lvl = (se.spellLevel - 1) % 5;

                                if (se.spellClass == 0 &&
                                    player.spellCastCount[sp_class, sp_lvl] > 0 &&
                                    spell != Spells.animate_dead)
                                {
                                    player.LearnSpell(spell);
                                }
                            }
                            break;

                        case SkillType.Paladin:
                            if (var_2 > 8)
                            {
                                for (int var_3 = 8; var_3 < var_2; var_3++)
                                {
                                    for (int sp_lvl = 0; sp_lvl < 5; sp_lvl++)
                                    {
                                        player.spellCastCount[0, sp_lvl] += unk_1A6F5[var_3, sp_lvl];
                                    }
                                }

                                foreach (Spells spell in System.Enum.GetValues(typeof(Spells)))
                                {
                                    SpellEntry se = gbl.spell_table[(int)spell];
                                    int sp_class = (se.spellLevel - 1) / 5;
                                    int sp_lvl = (se.spellLevel - 1) % 5;

                                    if (se.spellClass == 0 &&
                                        player.spellCastCount[sp_class, sp_lvl] > 0)
                                    {
                                        player.LearnSpell(spell);
                                    }
                                }
                            }
                            break;

                        case SkillType.Ranger:
                            if (var_2 > 7)
                            {
                                for (int var_3 = 8; var_3 <= var_2; var_3++)
                                {
                                    for (int sp_lvl = 0; sp_lvl < 3; sp_lvl++)
                                    {
                                        player.spellCastCount[1, sp_lvl] += unk_1A758[var_3, sp_lvl];
                                    }

                                    for (int sp_lvl = 3; sp_lvl < 5; sp_lvl++)
                                    {
                                        player.spellCastCount[2, sp_lvl - 3] += unk_1A758[var_3, sp_lvl];
                                    }
                                }

                                foreach (Spells spell in System.Enum.GetValues(typeof(Spells)))
                                {
                                    if (gbl.spell_table[(int)spell].spellClass == SpellClass.Druid)
                                    {
                                        player.LearnSpell(spell);
                                    }
                                }
                            }

                            break;

                        case SkillType.MagicUser:
                            player.spellCastCount[2, 0] += 1;

                            for (int lvl = 0; lvl <= (var_2 - 2); lvl++)
                            {
                                /* unk_1A7C6 = seg600:44B6 */
                                player.spellCastCount[2, 0] += ovr020.MU_spell_lvl_learn[lvl, 0];
                                player.spellCastCount[2, 1] += ovr020.MU_spell_lvl_learn[lvl, 1];
                                player.spellCastCount[2, 2] += ovr020.MU_spell_lvl_learn[lvl, 2];
                                player.spellCastCount[2, 3] += ovr020.MU_spell_lvl_learn[lvl, 3];
                                player.spellCastCount[2, 4] += ovr020.MU_spell_lvl_learn[lvl, 4];
                            }
                            break;
                    }
                }
            }

            foreach (Item item in player.items)
            {
                if (item.affect_3 == Affects.protect_magic && item.readied)
                {
                    for (int sp_lvl = 0; sp_lvl < 3; sp_lvl++)
                    {
                        player.spellCastCount[2, sp_lvl] *= 2;
                    }
                }
            }
        }


        internal static void sub_6A3C6(Player player)
        {
            player.thac0 = 0;

            for (int _class = 0; _class <= 7; _class++)
            {
                byte class_lvl = player.ClassLevel[_class];

                if (ovr018.thac0_table[_class, class_lvl] > player.thac0)
                {
                    player.thac0 = ovr018.thac0_table[_class, class_lvl];
                }

                if (player.HitDice < class_lvl)
                {
                    player.HitDice = class_lvl;
                }

                if (_class == 2 || _class == 3)
                {
                    if (class_lvl >= 7)
                    {
                        player.attacksCount = 3;
                    }
                }
                else if (_class == 4)
                {
                    if (class_lvl >= 8)
                    {
                        player.attacksCount = 3;
                    }
                }
            }

            sub_6A00F(player);
            sub_6A7FB(player);

            if (player.thief_lvl > 0)
            {
                sub_6AAEA(player);
            }

            player.classFlags = 0;

            for (int skill = 0; skill <= 7; skill++)
            {
                if (player.ClassLevel[skill] > 0 ||
                    (player.ClassLevelsOld[skill] > 0 && player.ClassLevelsOld[skill] < player.HitDice))
                {
                    player.classFlags += ovr018.unk_1A1B2[skill];
                }
            }

            for (int item_slot = 0; item_slot <= 12; item_slot++)
            {
                if (player.itemArray[item_slot] != null &&
                    (gbl.ItemDataTable[player.itemArray[item_slot].type].classFlags & player.classFlags) == 0 &&
                    player.itemArray[item_slot].cursed == false)
                {
                    player.itemArray[item_slot].readied = false;
                }
            }

            if (DualClassExceedLastLevel(player) == true)
            {
                for (int class_index = 0; class_index <= 7; class_index++)
                {
                    byte skill_lvl = player.ClassLevelsOld[class_index];

                    if (class_index == 2 || class_index == 3)
                    {
                        if (skill_lvl > 6)
                        {
                            player.attacksCount = 3;
                        }
                    }
                    else if (class_index == 4)
                    {
                        if (skill_lvl > 7)
                        {
                            player.attacksCount = 3;
                        }
                    }

                    if (ovr018.thac0_table[class_index, skill_lvl] > player.thac0)
                    {
                        player.thac0 = ovr018.thac0_table[class_index, skill_lvl];
                    }
                }

                if (player.fighter_old_lvl > 6 ||
                    player.ranger_old_lvl > 7 ||
                    player.paladin_old_lvl > 6)
                {
                    player.attacksCount = 3;
                }

                if (player.thief_old_lvl > 0)
                {
                    sub_6AAEA(player);
                }
            }
        }


        internal static void calc_cleric_spells(bool arg_0, Player player) /* sub_6A686 */
        {
            int clericLvl = player.SkillLevel(SkillType.Cleric);

            if (clericLvl > 0)
            {
                if (arg_0 == true)
                {
                    for (int sp_lvl = 1; sp_lvl < 5; sp_lvl++)
                    {
                        player.spellCastCount[0, sp_lvl] = 0;
                    }

                    player.spellCastCount[0, 0] = 1;

                    for (int sp_class = 0; sp_class <= (clericLvl - 2); sp_class++)
                    {
                        for (int sp_lvl = 0; sp_lvl < 5; sp_lvl++)
                        {
                            player.spellCastCount[0, sp_lvl] += unk_1A5CC[sp_class, sp_lvl];
                        }
                    }
                }

                if (player.wis > 12 && player.spellCastCount[0, 0] > 0)
                {
                    player.spellCastCount[0, 0] += 1;
                }

                if (player.wis > 13 && player.spellCastCount[0, 0] > 0)
                {
                    player.spellCastCount[0, 0] += 1;
                }

                if (player.wis > 14 && player.spellCastCount[0, 1] > 0)
                {
                    player.spellCastCount[0, 1] += 1;
                }

                if (player.wis > 15 && player.spellCastCount[0, 1] > 0)
                {
                    player.spellCastCount[0, 1] += 1;
                }

                if (player.wis > 16 && player.spellCastCount[0, 2] > 0)
                {
                    player.spellCastCount[0, 2] += 1;
                }

                if (player.wis > 17 && player.spellCastCount[0, 3] > 0)
                {
                    player.spellCastCount[0, 3] += 1;
                }
            }
        }

        static byte[, ,] byte_1A8CE = { // [8,13,5]       
            {{20, 20, 20, 20, 20}, {10, 13, 14, 16, 15}, {10, 13, 14, 16, 15}, {10, 13, 14, 16, 15}, {9, 12, 13, 15, 14}, {9, 12, 13, 15, 14}, {9, 12, 13, 15, 14}, {7, 10, 11, 13, 12}, {7, 10, 11, 13, 12}, {7, 10, 11, 13, 12}, {6, 9, 10, 12, 11}, {6, 9, 10, 12, 11}, {6, 9, 10, 12, 11}},
            {{20, 20, 20, 20, 20}, {10, 13, 14, 16, 15}, {10, 13, 14, 16, 15}, {10, 13, 14, 16, 15}, {9, 12, 13, 15, 14}, {9, 12, 13, 15, 14}, {9, 12, 13, 15, 14}, {7, 10, 11, 13, 12}, {7, 10, 11, 13, 12}, {7, 10, 11, 13, 12}, {6, 9, 10, 12, 11}, {6, 9, 10, 12, 11}, {6, 9, 10, 12, 11}},
            {{20, 20, 20, 20, 20}, {14, 15, 16, 17, 17}, {14, 15, 16, 17, 17}, {13, 14, 15, 16, 16}, {13, 14, 15, 16, 16}, {11, 12, 13, 13, 14}, {11, 12, 13,13, 14}, {10, 11, 12, 12, 13}, {10, 11, 12, 12, 13}, {8, 9, 10, 9, 11}, {8, 9, 10, 9, 11}, {7, 8, 9, 8, 10}, {7, 8, 9, 8, 10}},                      
            {{20, 20, 20, 20, 20}, {12, 13, 14, 15, 15}, {12, 13, 14, 15, 15}, {11, 12, 13, 14, 14}, {11, 12, 13, 14, 14}, {9, 9, 11, 11, 12}, {9, 9, 11, 11, 12}, {8, 9, 10, 10, 11}, {9, 9, 10, 10, 11}, {6, 7, 8, 7, 9}, {6, 7, 8, 7, 9}, {5, 6, 7, 6, 8}, {5, 6, 7, 6, 8}},
            {{20, 20, 20, 20, 20}, {14, 15, 16, 17, 17}, {14, 15, 16, 17, 17}, {13, 14, 15, 16, 16}, {13, 14, 15, 16, 16}, {11, 12, 13, 13, 14}, {11, 12, 13, 13, 14}, {10, 11, 12, 12, 13}, {10, 11, 12, 12, 13}, {8, 9, 10, 9, 11}, {8, 9, 10, 9, 11}, {7, 8, 9, 8, 10}, {7, 8, 9, 8, 10}},
            {{20, 20, 20, 20, 20}, {14, 13, 11, 15, 12}, {14, 13, 11, 15, 12}, {14, 13, 11, 15, 12}, {14, 13, 11, 15, 12}, {14, 13, 11, 15, 12}, {13, 11, 9, 13, 10}, {13, 11, 9, 13, 10}, {13, 11, 9, 13, 10}, {13, 11, 9, 13, 10}, {13, 11, 9, 13, 10}, {11, 9, 7, 11, 8}, {11, 9, 7, 11, 8}}, 
            {{20, 20, 20, 20, 20}, {13, 12, 14, 16, 15}, {13, 12, 14, 16, 15}, {13, 12, 14, 16, 15}, {13, 12, 14, 16, 15}, {12, 11, 12, 15, 13}, {12, 11, 12, 15, 13}, {12, 11, 12, 15, 13}, {12, 11, 12, 15, 13}, {11, 10, 10, 14, 11}, {11, 10, 10, 14, 11}, {11, 10, 10, 14, 11}, {11, 10, 10, 14, 11}},
            {{20, 20, 20, 20, 20}, {13, 12, 14, 16, 15}, {13, 12, 14, 16, 15}, {13, 12, 14, 16, 15}, {13, 12, 14, 16, 15}, {12, 11, 12, 15, 13}, {12, 11, 12, 15, 13}, {12, 11, 12, 15, 13}, {12, 11, 12, 15, 13}, {11, 10, 10, 14, 11}, {13, 11, 9, 13, 10}, {11, 9, 7, 11, 8}, {11, 9, 7, 11, 8}}};


        internal static void sub_6A7FB(Player player)
        {
            Item item = player.items.Find(i => (int)i.affect_3 > 0x80 && i.readied && ((int)i.affect_3 & 0x7F) == 6);
            bool var_9 = item != null && ((int)item.affect_3 & 0x7F) == 6;

            for (int var_1 = 0; var_1 < 5; var_1++)
            {
                int var_2;

                player.saveVerse[var_1] = 20;
                for (var_2 = 0; var_2 <= 7; var_2++)
                {
                    if (player.ClassLevel[var_2] > 0)
                    {
                        byte dl = byte_1A8CE[var_2, player.ClassLevel[var_2], var_1];

                        if (player.saveVerse[var_1] > dl)
                        {
                            player.saveVerse[var_1] = dl;
                        }
                    }
                }

                var_2 = 7; /* Orignal code had a post use test and would exit on 7,
                            * but this for loops uses are pre-test increment so fix var_2 */

                if (player.ClassLevel[var_2] > player.ClassLevelsOld[var_2])
                {
                    byte dl = byte_1A8CE[var_2 ,player.ClassLevelsOld[var_2] , var_1];

                    if (player.saveVerse[var_1] > dl)
                    {
                        player.saveVerse[var_1] = dl;
                    }
                }

                if (var_1 == 0)
                {
                    if (player.race == Race.dwarf || 
                        player.race == Race.halfling ||
                        var_9 == true)
                    {
                        if (player.con >= 4 && player.con <= 6)
                        {
                            player.saveVerse[var_1] += 1;
                        }
                        else if (player.con >= 7 && player.con <= 10)
                        {
                            player.saveVerse[var_1] += 2;
                        }
                        else if (player.con >= 11 && player.con <= 13)
                        {
                            player.saveVerse[var_1] += 3;
                        }
                        else if (player.con >= 14 && player.con <= 17)
                        {
                            player.saveVerse[var_1] += 4;
                        }
                        else if (player.con == 18)
                        {
                            player.saveVerse[var_1] += 5;
                        }
                    }

                    if (player.con == 19 || player.con == 20)
                    {
                        player.saveVerse[var_1] += 1;
                    }
                    else if (player.con == 21 || player.con == 22)
                    {
                        player.saveVerse[var_1] += 2;
                    }
                    else if (player.con == 23 || player.con == 24)
                    {
                        player.saveVerse[var_1] += 3;
                    }
                    else if (player.con == 25)
                    {
                        player.saveVerse[var_1] += 4;
                    }
                }
            }
        }


        static sbyte[,] /*seg600:3F20 */ unk_1A230 = { 
            { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 10, 15, 0, 0, 0, -10, -5 },
            { 0, 5, -5, 0, 5, 10, 5, 0, 0 },
            { 0, 0, 5, 10, 5, 5, 10, -15, 0 }, 
            { 0, 10, 0, 0, 0, 5, 0, 0, 0 }, 
            { 0, 5, 5, 5, 10, 15, 5, -15, -5 }, 
            { 0, -5, 5, 5, 0, 0, 5, 5, -10 }, 
            { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 
            { 0, -15, -10, -10, -20, -10, -19, -5, 10 }, 
            { 0, -15, -5, -5, 0, -5, -10, 0, 0 }, 
            { 0, 0, 0, -5, 0, 0, 0, 0, 0 }, 
            { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, -5, 0, 0, 0 } };

        static sbyte[,] /*seg600:3F33 */ unk_1A243 = { 
            { 0, 5, 10, 5, 0, 0 }, 
            { 0, 0, 5, 10, 5, 5 }, 
            { 5, 10, -15, 0, 10, 0 }, 
            { 0, 0, 0, 5, 0, 0 }, 
            { 0, 0, 5, 5, 5, 10 }, 
            { 10, 15, 5, -15, -5, -5 }, 
            { -5, 5, 5, 0, 0, 5 },
            { 5, 5, -10, 0, 0, 0 }, 
            { 0, 0, 0, 0, 0, 0 }, 
            { 0, -15, -10, -10, -20, -10 }, 
            { -10, -19, -5, -10, -15, -5 }, 
            { -5, -5, 0, -5, -10, 0 }, 
            { 0, 0, 0, 0, -5, 0 }, 
            { 0, 0, 0, 0, 0, 0 }, 
            { 0, 0, 0, 0, 0, 0 }, 
            { 0, 0, 0, 0, 0, 0 }, 
            { 0, 0, -5, 0, 0, 0 }, 
            { 0, 5, 10, 0, 5, 5 }, 
            { 5, 10, 15, 5, 10, 10 }, 
            { 10, 15, 20, 10, 12, 12 }, 
            { 12, 8, 8, 18, 17, 99 }, 
            { 99, 0, 3, 18, 3, 18 } };

        static byte[,] /*seg600:3EC0 */ unk_1A1D0 = {
            { 0, 0, 0, 0 , 0 , 0 ,0 , 0, 0 },
            { 0, 0x1E, 0x19, 0x14 ,0x0F, 0x0A, 0x0A, 0x55, 0x00 },
            { 0, 0x23, 0x1D, 0x19, 0x15, 0x0F, 0x0A, 0x56, 0x00 },
            { 0, 0x28, 0x21, 0x1E, 0x1B, 0x14, 0x0F, 0x57, 0x00 },
            { 0, 0x2D, 0x25, 0x23, 0x21, 0x19, 0x0F, 0x58, 0x14 },
            { 0, 0x32, 0x2A, 0x28, 0x28, 0x1F, 0x14, 0x5A, 0x19 },
            { 0, 0x37, 0x2F, 0x2D, 0x25, 0x14, 0x5C, 0x1E, 0x3C },
            { 0, 0x34, 0x32, 0x37, 0x2B, 0x19, 0x5E, 0x23, 0x41},
            { 0, 0x39, 0x37, 0x3E, 0x31, 0x19, 0x60, 0x28, 0x46},
            { 0, 0x3E, 0x3C, 0x46, 0x38, 0x1E, 0x62, 0x2D, 0x50},
            { 0, 0x43, 0x41, 0x4E, 0x3F, 0x1E, 0x63, 0x32, 0x5A},
            { 0, 0x48, 0x46, 0x56, 0x46, 0x23, 0x63, 0x3C, 0x64},
            { 0, 0x64, 0x4D, 0x4B, 0x5E, 0x4D, 0x23, 0x63, 0x41 } };



        internal static void sub_6AAEA(Player player)
        {
            byte var_2 = 0; //Simeon

            var item_found = player.items.Find(item => item.readied && (item.ScrollLearning(3, 2) || item.ScrollLearning(3, 11)));
            var var_A = item_found != null && item_found.ScrollLearning(3, 11);
            var var_B = item_found != null && item_found.ScrollLearning(3, 2);

            int thiefLvl = player.SkillLevel(SkillType.Thief);

            if (thiefLvl < 4 && var_B == true)
            {
                thiefLvl = 4;
                var_B = false;
            }

            int var_5 = thiefLvl;

            for (int var_1 = 1; var_1 <= 8; var_1++)
            {
                if (var_A == true)
                {
                    switch (var_1)
                    {
                        case 1:
                            if (thiefLvl < 5)
                            {
                                thiefLvl = 5;
                                var_2 = 0;
                            }
                            else
                            {
                                var_2 = 5;
                            }
                            break;

                        case 2:
                            if (thiefLvl < 7)
                            {
                                thiefLvl = 7;
                                var_2 = 0;
                            }
                            else
                            {
                                var_2 = 5;
                            }
                            break;
                    }
                }

                if (unk_1A230[(int)player.race, var_1] < 0 &&
                    unk_1A1D0[thiefLvl, var_1] < (System.Math.Abs(unk_1A230[(int)player.race, var_1]) + var_2))
                {
                    player.field_EA[var_1 - 1] = 0;
                }
                else
                {
                    player.field_EA[var_1 - 1] = (byte)(var_2 + unk_1A1D0[thiefLvl, var_1] + unk_1A230[(int)player.race, var_1]);

                    if (var_1 < 6)
                    {
                        player.field_EA[var_1 - 1] += (byte)unk_1A243[player.dex, var_1];
                    }

                }

                if (var_B == true)
                {
                    player.field_EA[var_1 - 1] += 10;
                }

                thiefLvl = var_5;
            }
        }


        internal static bool player_can_be_class(ClassId _class, Player player) /* sub_6AD3E */
        {
            var firstClass = HumanCurrentClass_Unknown(player);
            bool var_2 = _class != firstClass;
            int var_3 = 0;


            while (var_3 <= 5 &&
                (gbl.class_stats_min[(int)firstClass][var_3] < 9 || player.stats[var_3].tmp > 14))
            {
                var_3++;
            }

            var_2 = (var_2 == true && var_3 > 5);
            var_3 = 0;

            while (var_3 <= 5 &&
                (gbl.class_stats_min[(int)_class][var_3] < 9 || player.stats[var_3].tmp > 16))
            {
                var_3++;
            }

            var_2 = (var_2 == true && var_3 > 5);

            byte var_4 = 1;

            while (gbl.class_alignments[(int)_class, 0] >= var_4 &&
                gbl.class_alignments[(int)_class, var_4] != player.alignment)
            {
                var_4++;
            }

            if (var_2 == false ||
                gbl.class_alignments[(int)_class, 0] < var_4)
            {
                var_2 = false;
            }
            else
            {
                var_2 = true;
            }

            return var_2;
        }


        internal static void DuelClass(Player player)
        {
            List<MenuItem> list = new List<MenuItem>();

            list.Add(new MenuItem("Pick New Class", true));

            foreach (var _class in gbl.RaceClasses[(int)player.race])
            {
                if (player_can_be_class(_class, player) == true)
                {
                    list.Add(new MenuItem(ovr020.classString[(int)_class]));
                }
            }

            if (list.Count == 1)
            {
                seg041.DisplayStatusText(15, 4, player.name + " doesn't qualify.");
                list.Clear();
                return;
            }

            MenuItem list_ptr;
            int index = 1;
            bool show_exit = true;
            bool var_F = true;

            char input_key;

            do
            {
                input_key = ovr027.sl_select_item(out list_ptr, ref index, ref var_F, show_exit, list,
                    0x16, 0x26, 2, 1, 15, 10, 13, "Select", string.Empty);

                if (input_key == 0)
                {
                    return;
                }
            } while (input_key != 0x53);

            player.exp = 0;
            player.attacksCount = 2;
            int newClass = 0;

            while (newClass <= 7 && ovr020.classString[newClass] != list_ptr.Text)
            {
                newClass++;
            }

            list.Clear();

            player.ClassLevelsOld[(int)HumanCurrentClass_Unknown(player)] = HumanCurrentClassLevel_Zero(player);

            player.multiclassLevel = player.HitDice;
            player.HitDice = 1;

            player.ClassLevel[(int)HumanCurrentClass_Unknown(player)] = 0;
            player.ClassLevel[newClass] = 1;

            for (int i = 0; i < 5; i++)
            {
                player.spellCastCount[0, i] = 0;
                player.spellCastCount[1, i] = 0;
                player.spellCastCount[2, i] = 0;
            }

            if (newClass == 0)
            {
                player.spellCastCount[0,0] = 1;
            }
            else if (newClass == 5)
            {
                player.spellCastCount[2,0] = 1;
                player.LearnSpell(Spells.detect_magic_MU);
                player.LearnSpell(Spells.read_magic);
                player.LearnSpell(Spells.sleep);
            }

            player._class = (ClassId)newClass;

            seg041.DisplayStatusText(0, 10, player.name + " is now a 1st level " + ovr020.classString[newClass] + ".");

            seg051.FillChar(0, 0x54, player.spell_list);

            sub_6A3C6(player);
            calc_cleric_spells(true, player);
            sub_6A7FB(player);
            sub_6AAEA(player);

            foreach(var item in player.items)
            {
                if ((gbl.ItemDataTable[item.type].classFlags & player.classFlags) == 0 &&
                    item.cursed == false)
                {
                    item.readied = false;
                }
            }
        }


        internal static ClassId HumanCurrentClass_Unknown(Player player) // getFirstSkill
        {
            if (player.race != Race.human)
            {
                return ClassId.unknown;
            }

            for (ClassId index = ClassId.cleric; index <= ClassId.monk; index++)
            {
                if (player.ClassLevel[(int)index] > 0)
                {
                    return index;
                }
            }

            return ClassId.unknown;
        }

        internal static byte HumanCurrentClassLevel_Zero(Player player) /* hasAnySkills */
        {
            if (player.race != Race.human)
            {
                return 0;
            }

            int loop_var = 0;

            while (loop_var < 7 &&
                player.ClassLevel[loop_var] == 0)
            {
                loop_var++;
            }

            return player.ClassLevel[loop_var];
        }


        internal static bool DualClassExceedLastLevel(Player player) // sub_6B3D1
        {
            return HumanCurrentClassLevel_Zero(player) > player.multiclassLevel;
        }
    }
}
