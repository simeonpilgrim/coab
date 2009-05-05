using Classes;
using System.Collections.Generic;

namespace engine
{
    internal enum SpellLoc
    {
        memory = 0,
        grimoire = 1,
        scroll = 2,
        scrolls = 3,
        choose = 4,
        memorize = 5,
        scribe = 6
    }

    class ovr020
    {
        internal static string[] sexString = { "Male", "Female" };
        internal static string[] raceString = { "Monster", "Dwarf", "Elf", "Gnome", 
                                         "Half-Elf", "Halfling", "Half-Orc", "Human" };

        internal static string[] alignmentString = { "Lawful Good", "Lawful Neutral", "Lawful Evil",
                                              "Neutral Good", "True Neutral", "Neutral Evil",
                                              "Chaotic Good", "Chaotic Neutral", "Chaotic Evil" };

        internal static string[] classString = { "Cleric", "Druid", "Fighter", "Paladin", "Ranger",
                                          "Magic-User", "Thief", "Monk", "Cleric/Fighter", 
                                          "Cleric/Fighter/Magic-User", "Cleric/Ranger",
                                          "Cleric/Magic-User","Cleric/Thief", "Fighter/Magic-User", 
                                          "Fighter/Thief", "Fighter/Magic-User/Thief",
                                          "Magic-User/Thief" };

        static string[] statShortString = { "STR ", "INT ", "WIS ", "DEX ", "CON ", "CHA " };

        internal static string[] statusString = { "Okay", "Animated", "tempgone", "Running",
                                         "Unconscious", "Dying", "Dead", "Stoned",
                                         "Gone" };

        static string[] moneyString = { "Copper", "Silver", "Electrum", "Gold", "Platinum",
                                        "Gems", "Jewelry" };


        internal static void playerDisplayFull(Player player)
        {
            seg037.DrawFrame_Outer();

            ovr025.displayPlayerName(false, 1, 1, player);

            if (player.field_F7 > 0x7F)
            {
                seg041.displayString("(NPC)", 0, 10, 1, player.name.Length + 3);
            }

            int xCol = 1;

            string text2 = sexString[player.sex];

            seg041.displayString(sexString[player.sex], 0, 15, 3, xCol);

            xCol += (byte)(text2.Length + 1);
            text2 = raceString[(int)player.race];
            seg041.displayString(text2, 0, 15, 3, xCol);

            xCol += (byte)(text2.Length + 1);
            string text = "Age " + player.age.ToString();

            seg041.displayString(text, 0, 15, 3, xCol);

            text2 = alignmentString[player.alignment];
            seg041.displayString(text2, 0, 15, 4, 1);

            text2 = classString[(int)player._class];
            seg041.displayString(text2, 0, 15, 5, 1);

            for (int stat = 0; stat < 6; stat++)
            {
                text2 = statShortString[stat];
                seg041.displayString(text2, 0, 10, stat + 7, 1);
                display_stat(false, stat);
            }

            displayMoney();
            seg041.displayString("Level", 0, 15, 15, 1);

            bool displaySlash = false;
            text2 = string.Empty;

            for (int classIdx = 0; classIdx <= 7; classIdx++)
            {
                byte tmp = player.ClassLevelsOld[classIdx];

                if (player.ClassLevel[classIdx] > 0 ||
                    (tmp < ovr026.HumanCurrentClassLevel_Zero(player) && tmp > 0))
                {
                    if (displaySlash )
                    {
                        text2 += "/";
                    }

                    text2 += (player.ClassLevel[classIdx] + player.ClassLevelsOld[classIdx]).ToString();

                    displaySlash = true;
                }
            }

            seg041.displayString(text2, 0, 15, 15, 7);

            text = "Exp " + player.exp.ToString();
            seg041.displayString(text, 0, 15, 15, 17);

            ovr020.display_player_stats01();
            int yCol = 20;

            if (player.field_151 != null)
            {
                seg041.displayString("Weapon", 0, 15, yCol, 1);
                ovr025.ItemDisplayNameBuild(true, false, yCol, 8, player.field_151);
            }

            yCol++;
            if (player.armor != null)
            {
                seg041.displayString("Armor", 0, 15, yCol, 2);
                ovr025.ItemDisplayNameBuild(true, false, yCol, 8, player.armor);
            }

            yCol++;

            seg041.displayString("Status", 0, 15, yCol, 1);
            seg041.displayString(statusString[(int)player.health_status], 0, 10, yCol, 8);
        }

        internal static void displayMoney()
        {
            seg037.draw8x8_clear_area(14, 26, 7, 12);

            int yCol = 7;

            for (int coinType = 6; coinType >= 0; coinType--)
            {
                if (gbl.player_ptr.Money[coinType] > 0)
                {
                    string text = string.Format("{0,8} {1}", Money.names[coinType], gbl.player_ptr.Money[coinType]);
                    seg041.displayString(text, 0, 10, yCol, 12);

                    yCol++;
                }
            }
        }


        internal static void display_player_stats01()
        {
            Player player = gbl.player_ptr;

            ovr025.reclac_player_values(player);
            int yCol = 0x11;

            seg041.displayString("AC    ", 0, 15, yCol, 1);
            ovr025.display_AC(yCol, 4, player);

            seg041.displayString("HP    ", 0, 15, yCol + 1, 1);
            ovr025.display_hp(false, yCol + 1, 4, player);

            int xCol = 8;

            seg041.displayString("THAC0   ", 0, 15, yCol, xCol + 1);
            seg041.displayString((0x3c - player.hitBonus).ToString(), 0, 10, yCol, xCol + 7);


            string damage = string.Format("{0}d{1}{2}{3}", player.attack1_DiceCount, player.attack1_DiceSize,
                player.attack1_DamageBonus > 0 ? "+" : "", player.attack1_DamageBonus != 0 ? player.attack1_DamageBonus.ToString() : ""); 

            seg041.displayString("Damage  ", 0, 15, yCol + 1, xCol);
            seg041.displayString(damage, 0, 10, yCol + 1, xCol + 7);
            
            xCol = 0x16;
            seg041.displayString("Encumbrance  ", 0, 15, yCol, xCol);
            seg041.displayString(player.weight.ToString(), 0, 10, yCol, xCol + 12);

            int movement = player.movement;

            if (player.HasAffect(Affects.slow) == true)
            {
                movement *= 2;
            }

            if (player.HasAffect(Affects.haste) == true)
            {
                movement /= 2;
            }

            seg041.displayString("Movement ", 0, 15, yCol + 1, xCol + 3);
            seg041.displayString(movement.ToString(), 0, 10, yCol + 1, xCol + 12);
        }


        internal static void display_stat(bool highlighted, int stat_index)
        {
            int color = highlighted ? 0x0D : 0x0A;
            int col_x = 5;
            seg037.draw8x8_clear_area(stat_index + 7, 0x0b, stat_index + 7, col_x);

            if (gbl.player_ptr.stats[stat_index].max < 10)
            {
                col_x++;
            }

            string s = gbl.player_ptr.stats[stat_index].max.ToString();
            seg041.displayString(s, 0, color, stat_index + 7, col_x);

            if (stat_index == 0 &&
                gbl.player_ptr.strength == 18 &&
                gbl.player_ptr.tmp_str_00 > 0)
            {
                string text = gbl.player_ptr.tmp_str_00.ToString();

                if (gbl.player_ptr.tmp_str_00 < 10)
                {
                    text = "0" + text;
                }

                if (gbl.player_ptr.tmp_str_00 == 100)
                {
                    text = "00";
                }

                seg041.displayString("(" + text + ")", 0, color, 7, 7);
            }
        }

        static Set asc_54B50 = new Set(0x0902, new byte[] { 0x02, 0x18 });
        static Set unk_54B03 = new Set(0x0009, new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20 });

        internal static bool viewPlayer()
        {
            if (gbl.game_state == GameState.Combat)
            {
                ovr033.Color_0_8_normal();
            }

            char input_key = ' ';
            bool arg_0 = false;

            gbl.tradeWith = gbl.player_ptr;

            playerDisplayFull(gbl.player_ptr);

            while (unk_54B03.MemberOf(input_key) == false && arg_0 == false)
            {
                string text = string.Empty;
                bool hasSpells = false;
                bool hasMoney = false;

                for (int i = 0; i < gbl.max_spells; i++)
                {
                    if (gbl.player_ptr.spell_list[i] > 0)
                    {
                        hasSpells = true;
                    }
                }

                for (int i = 0; i <= 6; i++)
                {
                    if (gbl.player_ptr.Money[i] > 0)
                    {
                        hasMoney = true;
                    }
                }

                if (gbl.player_ptr.items.Count > 0)
                {
                    text += "Items ";
                }

                if (hasSpells == true)
                {
                    text += "Spells ";
                }

                if (gbl.player_ptr.field_F7 < 0x80 ||
                    gbl.player_ptr.in_combat == false ||
                    gbl.player_ptr.health_status == Status.animated)
                {
                    if (hasMoney && gbl.game_state != GameState.Combat)
                    {
                        text += "Trade ";
                    }
                }

                if (hasMoney)
                {
                    text += "Drop ";
                }

                if (CanCastHeal(gbl.player_ptr) == true)
                {
                    text += "Heal ";
                }

                if (CanCastCureDiseases(gbl.player_ptr) == true)
                {
                    text += "Cure ";
                }

                text += "Exit";

                input_key = ovr027.displayInput(false, 0, 15, 10, 13, text, string.Empty);

                int index = -1;

                switch (input_key)
                {
                    case 'I':
                        PlayerItemsMenu(ref arg_0);
                        break;

                    case 'S':
                        spell_menu2(out hasSpells, ref index, 0, SpellLoc.memory);
                        break;

                    case 'T':
                        tradeCoin();
                        break;

                    case 'D':
                        drop_coin();
                        displayMoney();
                        break;

                    case 'H':
                        PaladinHeal(gbl.player_ptr);
                        break;

                    case 'C':
                        PaladinCureDisease(gbl.player_ptr);
                        break;
                }

                if (arg_0 == false &&
                    asc_54B50.MemberOf(input_key) == true)
                {
                    playerDisplayFull(gbl.player_ptr);
                }
            }

            if (gbl.game_state == GameState.Combat)
            {
                ovr033.Color_0_8_inverse();
            }
            ovr025.load_pic();

            return arg_0;
        }


        internal static bool CanSellDropTradeItem(Item item) // sub_54EC1
        {
            bool canSellDropTradeItem = false;

            if (item.readied)
            {
                ovr025.string_print01("Must be unreadied");
            }
            else if (ovr023.item_is_scroll(item) == false)
            {
                canSellDropTradeItem = true;
            }
            else if ((int)item.affect_1 > 0x7F || (int)item.affect_2 > 0x7F || (int)item.affect_3 > 0x7F)
            {
                ovr025.displayPlayerName(false, 15, 1, gbl.player_ptr);

                gbl.textXCol = gbl.player_ptr.name.Length + 2;
                gbl.textYCol = 0x15;

                seg041.press_any_key(" was going to scribe from that scroll", false, 0, 0x0E, TextRegion.Normal2);
                if (ovr027.yes_no(15, 10, 13, "is it Okay to lose it? ") == 'Y')
                {
                    canSellDropTradeItem = true;
                }
            }
            else
            {
                canSellDropTradeItem = true;
            }

            seg037.draw8x8_clear_area(TextRegion.Normal2);

            return canSellDropTradeItem;
        }


        internal static void ItemDisplayStats(Item arg_0) /*sub_550A6*/
        {
            seg037.DrawFrame_Outer();

            seg041.displayString("itemptr:      ", 0, 10, 1, 1);
            seg041.displayString(arg_0.type.ToString(), 0, 10, 1, 0x14);
            
            seg041.displayString("namenum(1):   ", 0, 10, 2, 1);
            seg041.displayString(arg_0.namenum1.ToString(), 0, 10, 2, 0x14);
            
            seg041.displayString("namenum(2):   ", 0, 10, 3, 1);
            seg041.displayString(arg_0.namenum2.ToString(), 0, 10, 3, 0x14);
            
            seg041.displayString("namenum(3):   ", 0, 10, 4, 1);
            seg041.displayString(arg_0.namenum3.ToString(), 0, 10, 4, 0x14);
            
            seg041.displayString("plus:         ", 0, 10, 5, 1);
            seg041.displayString(arg_0.plus.ToString(), 0, 10, 5, 0x14);
            
            seg041.displayString("plussave:     ", 0, 10, 6, 1);
            seg041.displayString(arg_0.plus_save.ToString(), 0, 10, 6, 0x14);
            
            seg041.displayString("ready:        ", 0, 10, 7, 1);
            seg041.displayString(arg_0.readied.ToString(), 0, 10, 7, 0x14);
            
            seg041.displayString("identified:   ", 0, 10, 8, 1);
            seg041.displayString(arg_0.hidden_names_flag.ToString(), 0, 10, 8, 0x14);
            
            seg041.displayString("cursed:       ", 0, 10, 9, 1);
            seg041.displayString(arg_0.cursed.ToString(), 0, 10, 9, 0x14);
            
            seg041.displayString("value:        ", 0, 10, 10, 1);
            seg041.displayString(arg_0._value.ToString(), 0, 10, 10, 0x14);
            
            seg041.displayString("special(1):   ", 0, 10, 11, 1);
            seg041.displayString(arg_0.affect_1.ToString(), 0, 10, 11, 0x14);
            
            seg041.displayString("special(2):   ", 0, 10, 12, 1);
            seg041.displayString(arg_0.affect_2.ToString(), 0, 10, 12, 0x14);
            
            seg041.displayString("special(3):   ", 0, 10, 13, 1);
            seg041.displayString(arg_0.affect_3.ToString(), 0, 10, 13, 0x14);
            
            seg041.displayString("dice large:   ", 0, 10, 14, 1);
            seg041.displayString(gbl.ItemDataTable[arg_0.type].diceCountLarge.ToString(), 0, 10, 14, 0x14);
            
            seg041.displayString("sides large:  ", 0, 10, 15, 1);
            seg041.displayString(gbl.ItemDataTable[arg_0.type].diceSizeLarge.ToString(), 0, 10, 15, 0x14);

            seg041.displayAndDebug("press a key", 0, 10);
        }

        static Set unk_554EE = new Set(0x0009, new byte[] { 0x01, 0, 0, 0, 0, 0, 0, 0, 0x20 });

        internal static void PlayerItemsMenu(ref bool arg_0) /*use_item*/
        {
            Player player = gbl.player_ptr;
            char inputKey = ' ';

            bool redraw_items = true;
            bool redraw_player = true;
            int dummy_index = 0;

            while (unk_554EE.MemberOf(inputKey) == false &&
                arg_0 == false &&
                player.items.Count > 0)
            {
                int oldItemCount = player.items.Count;

                if (player.items.Count > 0)
                {
                    string text = "Ready";

                    if (Cheats.view_item_stats)
                    {
                        text += " View";
                    }

                    if (player.in_combat == true &&
                        gbl.area_ptr.field_1CA == 0 &&
                        (gbl.game_state == GameState.Camping || gbl.game_state == GameState.WildernessMap ||
                         gbl.game_state == GameState.State4 || gbl.game_state == GameState.Combat ||
                         (player.actions != null && player.actions.can_use == true)))
                    {
                        text += " Use";
                    }

                    if (player.field_F7 < 0x80 ||
                        player.in_combat == false ||
                        player.health_status == Status.animated)
                    {
                        if (gbl.game_state != GameState.Combat)
                        {
                            text += " Trade";
                        }
                    }

                    text += " Drop";

                    if (player.items.Count < Player.MaxItems)
                    {
                        text += " Halve";
                    }

                    text += " Join";

                    if (gbl.game_state == GameState.Shop)
                    {
                        if (player.field_F7 < 0x80 ||
                            player.in_combat == false ||
                            player.health_status == Status.animated)
                        {
                            text += " Sell";
                        }

                        text += " Id";
                    }

                    player.items.ForEach(item => ovr025.ItemDisplayNameBuild(false, true, 0, 0, item));


                    if (redraw_player == true || gbl.byte_1D2C8 == true)
                    {
                        seg037.draw8x8_07();

                        ovr025.displayPlayerName(true, 1, 1, player);

                        seg041.displayString("Items", 0, 10, 1, player.name.Length + 4);
                        seg041.displayString("Ready Item", 0, 15, 3, 1);

                        redraw_items = true;
                        redraw_player = false;
                        gbl.byte_1D2C8 = false;
                    }

                    var menulist = player.items.ConvertAll<MenuItem>(item => new MenuItem(item.name, item));
                    MenuItem menuitem;

                    inputKey = ovr027.sl_select_item(out menuitem, ref dummy_index, ref redraw_items, true,
                        menulist, 0x16, 0x26, 5, 1, 15, 10, 13, text, string.Empty);

                    Item curr_item = menuitem != null ? menuitem.Item : null;

                    if (curr_item != null)
                    {
                        switch (inputKey)
                        {
                            case 'V':
                                ItemDisplayStats(curr_item);
                                redraw_items = true;
                                redraw_player = true;
                                break;

                            case 'R':
                                ready_Item(curr_item);
                                break;

                            case 'U':
                                if (curr_item.readied == false)
                                {
                                    ovr025.string_print01("Must be Readied");
                                    inputKey = ' ';
                                }
                                else if (ovr023.item_is_scroll(curr_item) == true ||
                                    (curr_item.affect_2 > 0 && (int)curr_item.affect_3 < 0x80))
                                {
                                    sub_56478(ref arg_0, curr_item);
                                    if (gbl.game_state != GameState.Combat)
                                    {
                                        arg_0 = false;
                                    }

                                    if (arg_0 == false)
                                    {
                                        redraw_player = true;
                                    }
                                }
                                break;

                            case 'T':
                                if (CanSellDropTradeItem(curr_item) == true)
                                {
                                    trade_item(curr_item);
                                }
                                else
                                {
                                    inputKey = ' ';
                                }
                                redraw_player = true;
                                break;

                            case 'D':
                                if (CanSellDropTradeItem(curr_item) == true)
                                {
                                    ovr025.ItemDisplayNameBuild(false, false, 0, 0, curr_item);

                                    seg041.press_any_key("Your " + curr_item.name + "will be gone forever", true, 0, 14, 22, 0x26, 21, 1);

                                    if (ovr027.yes_no(15, 10, 13, "Drop It? ") == 'Y')
                                    {
                                        ovr025.lose_item(curr_item, gbl.player_ptr);
                                        redraw_items = true;
                                    }

                                    seg037.draw8x8_clear_area(TextRegion.Normal2);
                                }
                                else
                                {
                                    inputKey = ' ';
                                }
                                break;

                            case 'H':
                                halve_items(curr_item);
                                break;

                            case 'J':
                                join_items(curr_item);
                                break;

                            case 'S':
                                if (CanSellDropTradeItem(curr_item) == true)
                                {
                                    sell_Item(curr_item);
                                }
                                else
                                {
                                    inputKey = ' ';
                                }
                                break;

                            case 'I':
                                IdentifyItem(ref redraw_items, curr_item);
                                break;
                        }
                    }

                    ovr025.reclac_player_values(player);
                }

                if (player.items.Count != oldItemCount)
                {
                    redraw_items = true;
                }
            }
        }


        /*seg600:44B6 unk_1A7C6*/
        public readonly static byte[,] unk_1A7C6 = { 
            {1, 0, 0, 0, 0},
            {0, 1, 0, 0, 0},
            {1, 1, 0, 0, 0},
            {1, 0, 1, 0, 0},
            {0, 0, 1, 0, 0},
            {0, 1, 0, 1, 0},
            {0, 0, 1, 1, 0},
            {0, 0, 0, 0, 1},
            {0, 1, 0, 0, 1},
            {0, 0, 1, 1, 1},
            {0, 0, 0, 1, 1}  };

        internal static void calc_items_effects(bool add_item, Item item) /*sub_55B04*/
        {
            Player player = gbl.player_ptr;

            int masked_affect = (int)item.affect_3 & 0x7F;

            switch (masked_affect)
            {
                case 0:
                    gbl.applyItemAffect = true;
                    ovr013.CallAffectTable((add_item) ? Effect.Add : Effect.Remove, item, player, item.affect_3);
                    break;

                case 1:
                    if (add_item == true)
                    {
                        player.field_12D[2,0] *= 2;
                        player.field_12D[2,1] *= 2;
                        player.field_12D[2,2] *= 2;
                    }
                    else
                    {
                        int var_9 = player.magic_user_lvl + (player.magic_user_old_lvl * ovr026.sub_6B3D1(player));

                        player.field_12D[2,0] = 0;
                        player.field_12D[2,1] = 0;
                        player.field_12D[2,2] = 0;
                        player.field_12D[2,3] = 0;
                        player.field_12D[2,4] = 0;

                        player.field_12D[2,0] = 1;

                        for (int sp_lvl = 0; sp_lvl <= (var_9 - 2); sp_lvl++)
                        {
                            /* unk_1A7C6 = seg600:44B6 */
                            player.field_12D[2,0] += unk_1A7C6[sp_lvl, 0];
                            player.field_12D[2,1] += unk_1A7C6[sp_lvl, 1];
                            player.field_12D[2,2] += unk_1A7C6[sp_lvl, 2];
                            player.field_12D[2,3] += unk_1A7C6[sp_lvl, 3];
                            player.field_12D[2,4] += unk_1A7C6[sp_lvl, 4];
                        }

                        byte[] var_11 = new byte[5];
                        seg051.FillChar(0, 5, var_11);

                        for (int i = 0; i < gbl.max_spells; i++)
                        {
                            if (player.spell_list[i] != 0 &&
                                gbl.spell_table[player.spell_list[i]].spellClass == SpellClass.MagicUser)
                            {
                                int var_C = gbl.spell_table[player.spell_list[i]].spellLevel;
                                var_11[var_C - 1] += 1;

                                if (var_11[var_C - 1] > player.field_12D[2, var_C - 1])
                                {
                                    player.spell_list[i] = 0;
                                }
                            }
                        }
                    }
                    break;

                case 2:
                    ovr024.CalcStatBonuses(Stat.DEX, player);
                    ovr026.sub_6AAEA(player);
                    break;

                case 4:
                    if (((int)item.affect_2 & 0x0f) != player.alignment)
                    {
                        item.readied = false;
                        int damage = (int)item.affect_2 << 4;

                        gbl.damage_flags = DamageType.Magic;
                        if (gbl.game_state == GameState.Combat)
                        {
                            ovr025.RedrawCombatScreen();
                        }

                        ovr024.damage_person(false, 0, damage, player);
                        gbl.byte_1D2C8 = true;
                    }
                    break;

                case 5:
                    ovr024.CalcStatBonuses(Stat.STR, player);
                    break;

                case 6:
                    ovr024.CalcStatBonuses(Stat.CON, player);
                    ovr024.CalcStatBonuses(Stat.CHA, player);
                    break;

                case 8:
                    switch ((int)item.affect_2)
                    {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            ovr024.CalcStatBonuses((Stat)item.affect_2, player);
                            break;
                    }
                    break;

                case 9:
                    if (add_item == false)
                    {
                        ovr024.remove_affect(null, Affects.spiritual_hammer, player);
                    }
                    break;

                case 10:
                    ovr024.CalcStatBonuses(Stat.DEX, player);
                    break;

                case 11:
                    ovr026.sub_6AAEA(player);
                    break;

                case 12:
                    ovr024.CalcStatBonuses(Stat.INT, player);
                    break;

                case 13:
                    ovr024.CalcStatBonuses(Stat.STR, player);
                    ovr024.CalcStatBonuses(Stat.INT, player);
                    break;
            }
        }


        internal static void ready_Item(Item item)
        {
            bool magic_item = ((int)item.affect_3 > 0x7f);

            Player player = gbl.player_ptr;

            if (item.readied)
            {
                // Remove
                if (item.cursed == true)
                {
                    ovr025.string_print01("It's Cursed");
                }
                else
                {
                    item.readied = false;

                    if (magic_item == true)
                    {
                        calc_items_effects(false, item);
                    }
                }
                return;
            }
            else
            {
                // Weld
                int var_2 = 0;

                if ((player.field_185 + gbl.ItemDataTable[item.type].handsCount) > 2)
                {
                    var_2 = 3;
                }
   
                int item_slot = gbl.ItemDataTable[item.type].item_slot;

                if (item_slot >= 0 && item_slot <= 8)
                {
                    if (player.itemArray[item_slot] != null)
                    {
                        var_2 = 2;
                    }
                }
                else if (item_slot == 9)
                {
                    if (player.Item_ptr_02 != null)
                    {
                        var_2 = 2;
                    }
                }

                if (item.type == 73) // ItemType.Arrow
                {
                    if (player.arrows != null)
                    {
                        var_2 = 2;
                        item_slot = 0x0B;
                    }
                }

                if (item.type == 28) // ItemType.Quarrel
                {
                    if (player.quarrels != null)
                    {
                        var_2 = 2;
                        item_slot = 0x0C;
                    }
                }

                if ((player.classFlags & gbl.ItemDataTable[item.type].classFlags) == 0)
                {
                    var_2 = 1;
                }

                switch (var_2)
                {
                    case 0:
                        item.readied = true;
                        if (magic_item == true)
                        {
                            calc_items_effects(true, item);
                        }
                        break;

                    case 1:
                        ovr025.string_print01("Wrong Class");
                        break;

                    case 2:
                        ovr025.ItemDisplayNameBuild(false, false, 0, 0, player.itemArray[item_slot]);
                        ovr025.string_print01("already using " + player.itemArray[item_slot].name);
                        break;

                    case 3:
                        if (gbl.game_state != GameState.Combat ||
                            player.quick_fight == QuickFight.False)
                        {
                            ovr025.string_print01("Your hands are full!");
                        }
                        break;
                }
            }
        }


        internal static void trade_item(Item item)
        {
            Player player = gbl.tradeWith;
            ovr025.load_pic();

            ovr025.selectAPlayer(ref player, true, "Trade with Whom?");

            if (player != null)
            {
                gbl.tradeWith = player;
                if (canCarry(item, player) == true)
                {
                    ovr025.string_print01("Overloaded");
                }
                else
                {
                    player.items.Add(item);
                    ovr025.lose_item(item, gbl.player_ptr);
                    ovr025.reclac_player_values(player);
                }
            }
        }


        internal static void halve_items(Item item)
        {
            int half_number = item.count / 2;

            if (half_number > 0)
            {
                int half_and_remander = item.count - half_number;

                Item new_item = item.ShallowClone();
                item.count = half_and_remander;

                new_item.count = half_number;
                new_item.readied = false;

                gbl.player_ptr.items.Add(new_item);
            }
            else
            {
                ovr025.string_print01("Can't halve that");
            }
        }


        internal static void join_items(Item item) /*sub_56285*/
        {
            var match = gbl.player_ptr.items.FindAll(i =>
                {
                    return (i != item &&
                    i.count > 0 &&
                    i.namenum1 == item.namenum1 &&
                    i.namenum2 == item.namenum2 &&
                    i.namenum3 == item.namenum3 &&
                    i.type == item.type &&
                    i.plus == item.plus &&
                    i.plus_save == item.plus_save &&
                    i.cursed == item.cursed &&
                    i.weight == item.weight &&
                    i.affect_1 == item.affect_1 &&
                    (int)i.affect_1 < 2 &&
                    i.affect_2 == item.affect_2 &&
                    i.affect_3 == item.affect_3);
                });

            Item this_item = item;
            int items_count = this_item.count;

            foreach (var item_ptr in match)
            {
                if (item_ptr.count + items_count <= 255)
                {
                    items_count += item_ptr.count;
                    ovr025.lose_item(item_ptr, gbl.player_ptr);
                }
                else
                {
                    this_item.count = 255;
                    item_ptr.count -= 255 - items_count;

                    items_count = item_ptr.count;
                    this_item = item_ptr;
                }
            }
            this_item.count = items_count;
        }


        internal static void sub_56478(ref bool arg_0, Item item)
        {
            gbl.spell_from_item = false;
            byte var_1 = 0;

            if (ovr023.item_is_scroll(item) == true)
            {
                gbl.currentScroll = item;

                bool dummy_bool;
                int dummy_index = -1;
                var_1 = spell_menu2(out dummy_bool, ref dummy_index, SpellSource.Cast, SpellLoc.scroll);
            }
            else if( item.affect_2 > 0 && (int)item.affect_3 < 0x80 )
            {
                gbl.spell_from_item = true;
                var_1 = (byte)((int)item.affect_2 & 0x7F);
            }

            if (var_1 == 0)
            {
                arg_0 = false;
            }
            else
            {
                if (gbl.game_state == GameState.Combat &&
                    gbl.player_ptr.quick_fight == QuickFight.False)
                {
                    ovr025.RedrawCombatScreen();
                }

                if (gbl.spell_from_item == true)
                {
                    ovr025.DisplayPlayerStatusString(false, 10, "uses an item", gbl.player_ptr);

                    if (gbl.game_state == GameState.Combat)
                    {
                        seg041.displayString("Item:", 0, 10, 0x17, 0);

                        ovr025.ItemDisplayNameBuild(true, false, 0x17, 5, item);
                    }
                    else
                    {
                        ovr025.ItemDisplayNameBuild(true, false, 0x16, 1, item);
                    }

                    seg041.GameDelay();
                    ovr025.ClearPlayerTextArea();
                }

                gbl.spell_from_item = true;

                if (ovr023.item_is_scroll(item) == true)
                {
                    if (ovr026.HumanFirstOldClass_Unknown(gbl.player_ptr) == ClassId.magic_user ||
                        ovr026.HumanCurrentClass_Unknown(gbl.player_ptr) == ClassId.magic_user ||
                        ovr026.HumanFirstOldClass_Unknown(gbl.player_ptr) == ClassId.cleric ||
                        ovr026.HumanCurrentClass_Unknown(gbl.player_ptr) == ClassId.cleric ||
                        gbl.player_ptr.magic_user_lvl > 0 ||
                        gbl.player_ptr.cleric_lvl > 0)
                    {
                        ovr023.sub_5D2E1(ref arg_0, 0, gbl.player_ptr.quick_fight, var_1);
                    }
                    else
                    {
                        if (gbl.player_ptr.thief_lvl > 9 &&
                            ovr024.roll_dice(100, 1) <= 0x4b)
                        {
                            ovr023.sub_5D2E1(ref arg_0, 0, gbl.player_ptr.quick_fight, var_1);
                        }
                        else
                        {
                            ovr025.DisplayPlayerStatusString(true, gbl.textYCol, "oops!", gbl.player_ptr);
                        }
                    }
                }
                else
                {
                    ovr023.sub_5D2E1(ref arg_0, 0, gbl.player_ptr.quick_fight, var_1);
                }

                gbl.spell_from_item = false;

                if (gbl.game_state == GameState.Combat &&
                    gbl.spell_table[var_1].whenCast != SpellWhen.Camp)
                {
                    arg_0 = true;
                    ovr025.clear_actions(gbl.player_ptr);
                }
            }

            if (arg_0 == true)
            {
                if (ovr023.item_is_scroll(item) == true)
                {
                    ovr023.remove_spell_from_scroll(var_1, item, gbl.player_ptr);
                }
                else if ( item.affect_1 > 0 )
                {
                    if (item.count > 1)
                    {
                        item.count -= 1;
                    }
                    else
                    {
                        item.affect_1 -= 1;
                        if (item.affect_1 == 0)
                        {
                            ovr025.lose_item(item, gbl.player_ptr);
                        }
                    }
                }
            }
        }


        internal static void sell_Item(Item item)
        {
            int item_value = 0;

            if (item._value > 0)
            {
                item_value = item._value / 2;
            }

            if (item.count > 1)
            {
                if (item.type != 73 && // ItemType.Arrow 
                    item.type != 28) // ItemType.Quarrel
                {
                    item_value = (item.count * item_value) / 20;
                }
                else
                {
                    item_value *= item.count;
                }
            }

            ovr025.ItemDisplayNameBuild(false, false, 0, 0, item);

            string offer = "I'll give you " + item_value.ToString() + " gold pieces for your " + item.name;

            seg041.press_any_key(offer, true, 0, 14, TextRegion.Normal2);

            if (ovr027.yes_no(15, 10, 13, "Is It a Deal? ") == 'Y')
            {
                ovr025.string_print01("Sold!");

                ovr025.lose_item(item, gbl.player_ptr);

                short plat = (short)(item_value / 5);
                short gold = (short)(item_value % 5);
                short var_8;

                if (ovr022.willOverload(out var_8, plat + gold, gbl.player_ptr) == true)
                {
                    ovr025.string_print01("Overloaded. Money will be put in pool.");

                    if (var_8 > plat)
                    {
                        gbl.player_ptr.platinum += plat;
                    }
                    else
                    {
                        gbl.player_ptr.platinum += var_8;
                        gbl.pooled_money[Money.platum] += plat - var_8;
                    }

                    gbl.player_ptr.gold += gold;
                }
                else
                {
                    gbl.player_ptr.platinum += plat;
                    gbl.player_ptr.gold += gold;
                }
            }

            seg037.draw8x8_clear_area(TextRegion.Normal2);
        }


        internal static void IdentifyItem(ref bool arg_0, Item item)
        {
            bool id_item = false;
            ovr025.ItemDisplayNameBuild(false, false, 0, 0, item);

            seg041.press_any_key("For 200 gold pieces I'll identify your " + item.name, true, 0, 0x0e, TextRegion.Normal2);

            if (ovr027.yes_no(15, 10, 13, "Is It a Deal? ") == 'Y')
            {
                int player_gold = getPlayerGold(gbl.player_ptr);

                if (player_gold >= 200)
                {
                    id_item = true;

                    ovr022.setPlayerMoney(player_gold - 200);
                }
                else
                {
                    int pooled_gold = ovr022.getPooledGold();

                    if (pooled_gold > 200)
                    {
                        id_item = true;

                        ovr022.setPooledGold(pooled_gold - 200);
                    }
                    else
                    {
                        ovr025.string_print01("Not Enough Money");
                    }
                }
            }

            if (id_item == true)
            {
                if (item.hidden_names_flag == 0)
                {
                    seg041.press_any_key("I can't tell anything new about your " + item.name, true, 0, 0x0e, TextRegion.Normal2);
                }
                else
                {
                    item.hidden_names_flag = 0;
                    ovr025.ItemDisplayNameBuild(false, false, 0, 0, item);

                    seg041.press_any_key("It looks like some sort of " + item.name, true, 0, 0x0e, TextRegion.Normal2);

                    arg_0 = true;
                }

                seg041.GameDelay();
            }

            seg037.draw8x8_clear_area(TextRegion.Normal2);
        }


        internal static void tradeCoin()
        {
            bool finished = false;
            do
            {
                Player dest = gbl.tradeWith;
                ovr025.load_pic();

                ovr025.selectAPlayer(ref dest, true, "Trade to?");

                if (dest == null)
                {
                    finished = true;
                }
                else
                {
                    bool noMoneyLeft;

                    playerDisplayFull(gbl.player_ptr);
                    do
                    {
                        displayMoney();
                        gbl.tradeWith = dest;

                        List<MenuItem> list = new List<MenuItem>();

                        for (int coin = 0; coin <= 6; coin++)
                        {
                            if (gbl.player_ptr.Money[coin] > 0)
                            {
                                list.Add(new MenuItem(string.Format("{0,8} {1}", moneyString[coin], gbl.player_ptr.Money[coin])));
                            }
                        }

                        int dummyIndex = 0;
                        bool dummyBool = true;
                        MenuItem selected;

                        ovr027.sl_select_item(out selected, ref dummyIndex, ref dummyBool, true,
                            list, 13, 0x19, 7, 12, 15, 10, 13, " Select", "Select type of coin ");

                        if (selected == null)
                        {
                            noMoneyLeft = true;
                        }
                        else
                        {
                            string text;

                            int money_slot = ovr022.GetMoneyIndexFromString(out text, selected.Text);

                            text = "How much " + text + "will you trade? ";

                            short var_130 = ovr022.sub_592AD(10, text, gbl.player_ptr.Money[money_slot]);

                            ovr022.trade_money(money_slot, var_130, dest, gbl.player_ptr);
                            noMoneyLeft = true;
                            finished = true;

                            for (int coin = 0; coin <= 6; coin++)
                            {
                                if (gbl.player_ptr.Money[coin] > 0)
                                {
                                    finished = false;
                                    noMoneyLeft = false;
                                }
                            }
                        }

                        list.Clear();

                    } while (noMoneyLeft == false);
                }
            } while (finished == false);
        }


        internal static void drop_coin()
        {
            bool noMoreMoney;

            do
            {
                displayMoney();
                List<MenuItem> menuList = new List<MenuItem>();

                for (int coin = 0; coin < 7; coin++)
                {
                    if (gbl.player_ptr.Money[coin] != 0)
                    {
                        menuList.Add(new MenuItem(string.Format("{0,8} {1}", moneyString[coin], gbl.player_ptr.Money[coin])));
                    }
                }

                int index = 0;
                bool redrawMenuItems = true;

                MenuItem selected;
                ovr027.sl_select_item(out selected, ref index, ref redrawMenuItems, true, menuList, 13, 0x19, 7,
                    12, 15, 10, 13, " Select", "Select type of coin ");

                if (selected == null)
                {
                    noMoreMoney = true;
                }
                else
                {
                    string text;

                    int money_slot = ovr022.GetMoneyIndexFromString(out text, selected.Text);

                    text = "How much " + text + "will you drop? ";

                    short num_coins = ovr022.sub_592AD(10, text, gbl.player_ptr.Money[money_slot]);

                    ovr022.drop_coins(money_slot, num_coins, gbl.player_ptr);
                    noMoreMoney = true;

                    for (int coin = 0; coin < 7; coin++)
                    {
                        if (gbl.player_ptr.Money[coin] > 0)
                        {
                            noMoreMoney = false;
                        }
                    }
                }

                menuList.Clear();
            } while (noMoreMoney == false);
        }


        internal static bool canCarry(Item item, Player player)
        {
            ovr025.reclac_player_values(player);
            bool too_heavy = false;

            if (player.items.Count >= Player.MaxItems)
            {
                too_heavy = true;
            }

            int item_weight = item.weight;

            if (item.count > 0)
            {
                item_weight *= item.count;
            }

            if ((player.weight + item_weight) > (ovr025.max_encumberance(player) + 1500))
            {
                too_heavy = true;
            }

            return too_heavy;
        }


        internal static void scroll_team_list(char input_key)
        {
            int index = gbl.player_next_ptr.IndexOf(gbl.player_ptr);

            if (gbl.player_next_ptr.Count > 0)
            {
                if (input_key == 'O')
                {
                    //next
                    index = (index + 1) % gbl.player_next_ptr.Count;
                    gbl.player_ptr = gbl.player_next_ptr[index];
                }
                else if (input_key == 'G')
                {
                    // previous
                    index = (index - 1 + gbl.player_next_ptr.Count) % gbl.player_next_ptr.Count;
                    gbl.player_ptr = gbl.player_next_ptr[index];
                }
            }
        }


        internal static int getPlayerGold(Player player)
        {
            int coppers = 0;

            for (int i = 0; i < 5; i++)
            {
                coppers += player.Money[i] * Money.per_copper[i];
            }

            int gold = (coppers + 100) / Money.per_copper[Money.gold];

            return gold;
        }

        internal static byte spell_menu2(out bool arg_0, ref int index, SpellSource arg_8, SpellLoc spl_location)
        {
            string text;
            byte result;

            switch (spl_location)
            {
                case SpellLoc.memory:

                    text = "in Memory";
                    break;

                case SpellLoc.grimoire:

                    text = "in Grimoire";
                    break;

                case SpellLoc.scroll:
                    text = "on Scroll";
                    break;

                case SpellLoc.scrolls:
                    text = "on Scrolls";
                    break;

                case SpellLoc.choose:
                    text = "to Choose";
                    break;

                case SpellLoc.memorize:
                    text = "to Memorize";
                    break;

                case SpellLoc.scribe:
                    text = "to Scribe";
                    break;

                default:
                    text = string.Empty;
                    break;
            }

            arg_0 = ovr023.sub_5CA74(spl_location);

            if (arg_0 == true )
            {
                if (index < 0 ||
                    arg_8 == SpellSource.Cast)
                {
                    if (gbl.game_state != GameState.Combat)
                    {
                        if (arg_8 == SpellSource.Memorize)
                        {
                            seg037.draw8x8_05();
                        }
                        else
                        {
                            seg037.draw8x8_07();
                        }
                    }
                    else
                    {
                        seg037.DrawFrame_Outer();
                    }
                }

                ovr025.displayPlayerName(true, 1, 1, gbl.player_ptr);

                seg041.displayString("Spells " + text, 0, 10, 1, gbl.player_ptr.name.Length + 4);

                result = ovr023.spell_menu(ref index, arg_8);
            }
            else
            {
                result = 0;
            }

            return result;
        }


        internal static bool CanCastHeal(Player player) /* sub_575F0 */
        {
            bool result;

            if ((player._class == ClassId.paladin || (player.paladin_old_lvl > 0 && ovr026.sub_6B3D1(player) != 0)) &&
                 gbl.game_state != GameState.Combat &&
                    player.health_status == Status.okey &&
                    player.HasAffect(Affects.paladinDailyHealCast) == false)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }


        internal static bool CanCastCureDiseases(Player player) /* sub_57655 */
        {
            return ((player._class == ClassId.paladin || (player.paladin_old_lvl > 0 && ovr026.sub_6B3D1(player) != 0)) &&
                gbl.game_state != GameState.Combat &&
                player.health_status == Status.okey &&
                player.paladinCuresLeft > 0);
        }


        internal static void PaladinHeal(Player player)
        {
            ovr025.load_pic();
            Player target = gbl.player_next_ptr[0];

            ovr025.selectAPlayer(ref target, true, "Heal whom? ");

            if (target == null)
            {
                playerDisplayFull(gbl.player_ptr);
                return;
            }

            int dx = player.paladin_old_lvl * ovr026.sub_6B3D1(player);
            int healAmount = (player.paladin_lvl + dx) * 2;

            if (ovr024.heal_player(0, healAmount, target) == true)
            {
                ovr025.string_print01(target.name + " feels better");
            }
            else
            {
                ovr025.string_print01(target.name + " is unaffected");
            }

            ovr024.add_affect(false, 0, 1440, Affects.paladinDailyHealCast, player);
            playerDisplayFull(gbl.player_ptr);
        }

        static Affects[] paladinCureableDiseases = { // unk_16B39
            Affects.helpless, 
            Affects.cause_disease_1, 
            Affects.affect_2b, 
            Affects.cause_disease_2, 
            Affects.hot_fire_shield, 
            Affects.affect_39 };

        internal static void PaladinCureDisease(Player player) /* sub_577EC */
        {
            ovr025.load_pic();
            Player target = gbl.player_next_ptr[0];

            ovr025.selectAPlayer(ref target, true, "Cure whom? ");

            if (target == null)
            {
                playerDisplayFull(gbl.player_ptr);
            }
            else
            {
                bool is_diseased = System.Array.Exists(paladinCureableDiseases, affect => target.HasAffect(affect));

                char input = 'Y';

                if (is_diseased == false)
                {
                    ovr025.DisplayPlayerStatusString(false, 0, "is not diseased", target);

                    input = ovr027.yes_no(15, 10, 13, "cure anyway: ");

                    ovr025.ClearPlayerTextArea();
                }

                if (input == 'Y')
                {
                    gbl.cureSpell = true;
                    System.Array.ForEach(paladinCureableDiseases, affect => ovr024.remove_affect(null, affect, target));

                    gbl.cureSpell = false;

                    if (player.paladinCuresLeft > 0)
                    {
                        player.paladinCuresLeft--;
                    }

                    if (player.HasAffect(Affects.paladinDailyCureRefresh) == false)
                    {
                        ovr024.add_affect(true, 0, 0x2760, Affects.paladinDailyCureRefresh, player);
                    }

                    ovr025.string_print01(target.name + " is cured");
                }

                playerDisplayFull(gbl.player_ptr);
            }
        }
    }
}
