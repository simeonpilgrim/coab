using Classes;

namespace engine
{
    class ovr005
    {
        static Affects[] disease_types = {  Affects.helpless,  Affects.cause_disease_1,
                                            Affects.affect_2b, Affects.cause_disease_2,
                                            Affects.funky__32, Affects.affect_39 };

        static string[] temple_sl = { "Cure Blindness", "Cure Disease", "Cure Light Wounds", "Cure Serious Wounds", "Cure Critical Wounds", "Heal", "Neutralize Poison", "Raise Dead", "Remove Curse", "Stone to Flesh", "Exit" };


        internal static char cast_cure_anyway(string arg_0)
        {
            char ret_val;

            ovr025.DisplayPlayerStatusString(false, 0, arg_0, gbl.player_ptr);
            ret_val = ovr027.yes_no(15, 10, 13, "cast cure anyway: ");

            ovr025.ClearPlayerTextArea();

            return ret_val;
        }


        internal static char buy_cure(short cost, string cure_name)
        {
            string text = cure_name + " will only cost	" + cost.ToString() + "	gold pieces.";
            seg041.press_any_key(text, true, 0, 10, 0x16, 0x26, 0x11, 1);

            char input_key = ovr027.yes_no(15, 10, 13, "pay for cure ");

            if (input_key == 'Y')
            {
                int player_money = ovr020.getPlayerGold(gbl.player_ptr);

                if (cost <= player_money)
                {
                    player_money = cost;

                    ovr022.setPlayerMoney(player_money);
                }
                else
                {
                    int pool_money = ovr022.getPooledGold(gbl.pooled_money);

                    if (cost <= pool_money)
                    {
                        ovr022.setPooledGold(pool_money - cost);
                    }
                    else
                    {
                        ovr025.string_print01("Not enough money.");
                        input_key = 'N';
                    }
                }
            }

            if (input_key == 'Y')
            {
                ovr025.ClearPlayerTextArea();
                ovr025.DisplayPlayerStatusString(true, 0, "is cured.", gbl.player_ptr);
            }

            return input_key;
        }


        internal static void cure_blindness()
        {
            Affect dummyAffect;
            char input_key = 'Y';

            if (ovr025.find_affect(out dummyAffect, Affects.blinded, gbl.player_ptr) == false)
            {
                input_key = cast_cure_anyway("is not blind.");
            }

            if (input_key == 'Y')
            {
                input_key = buy_cure(1000, "Cure Blindness");

                if (input_key == 'Y')
                {
                    ovr024.remove_affect(null, Affects.blinded, gbl.player_ptr);
                }
            }
        }


        internal static void cure_disease()
        {
            bool is_diseased = false;

            for (int i = 0; i < 6; i++)
            {
                Affect dummyAffect;
                if (ovr025.find_affect(out dummyAffect, disease_types[i], gbl.player_ptr) == true)
                {
                    is_diseased = true;
                }
            }

            char input_key = 'Y';
            if (is_diseased == false)
            {
                input_key = cast_cure_anyway("is not Diseased.");
            }

            if (input_key == 'Y')
            {
                input_key = buy_cure(1000, "Cure Disease");

                if (input_key == 'Y')
                {
                    gbl.byte_1D2C6 = true;
                    for (int i = 0; i < 6; i++)
                    {
                        ovr024.remove_affect(null, disease_types[i], gbl.player_ptr);
                    }

                    gbl.byte_1D2C6 = false;
                }
            }
        }


        internal static void cure_wounds(int arg_0)
        {
            bool var_4;
            char input_key;
            byte heal_amount;

            switch (arg_0)
            {
                case 1:
                    input_key = buy_cure(100, "Cure Light Wounds");
                    if (input_key == 'Y')
                    {
                        heal_amount = ovr024.roll_dice(8, 1);
                        var_4 = ovr024.heal_player(0, heal_amount, gbl.player_ptr);
                    }
                    break;

                case 2:
                    input_key = buy_cure(350, "Cure Serious Wounds");
                    if (input_key == 'Y')
                    {
                        heal_amount = (byte)(ovr024.roll_dice(8, 2) + 1);
                        var_4 = ovr024.heal_player(0, heal_amount, gbl.player_ptr);
                    }
                    break;

                case 3:
                    input_key = buy_cure(600, "Cure Critical Wounds");
                    if (input_key == 'Y')
                    {
                        heal_amount = (byte)(ovr024.roll_dice(8, 3) + 3);
                        var_4 = ovr024.heal_player(0, heal_amount, gbl.player_ptr);
                    }
                    break;

                case 4:
                    input_key = buy_cure(5000, "Heal");
                    if (input_key == 'Y')
                    {
                        heal_amount = gbl.player_ptr.hit_point_max;
                        heal_amount -= gbl.player_ptr.hit_point_current;
                        heal_amount -= ovr024.roll_dice(4, 1);

                        var_4 = ovr024.heal_player(0, heal_amount, gbl.player_ptr);
                        ovr024.remove_affect(null, Affects.blinded, gbl.player_ptr);

                        for (int i = 0; i < 6; i++)
                        {
                            ovr024.remove_affect(null, disease_types[i], gbl.player_ptr);
                        }

                        ovr024.remove_affect(null, Affects.feeble, gbl.player_ptr);

                        ovr024.sub_648D9(1, gbl.player_ptr);
                        ovr024.sub_648D9(2, gbl.player_ptr);
                    }
                    break;
            }
        }


        internal static void raise_dead()
        {
            Player player01;
            byte var_109;
            byte var_108;
            byte var_107;
            byte var_106;
            char var_105;

            player01 = gbl.player_ptr;
            var_106 = 0;
            var_105 = 'Y';

            if (player01.health_status == Status.dead ||
                player01.health_status == Status.animated)
            {
                var_106 = 1;
            }

            if (var_106 == 0)
            {
                var_105 = cast_cure_anyway("is not dead.");
            }


            if (var_105 == 'Y')
            {
                var_105 = buy_cure(5500, "Raise Dead");

                if (var_105 == 'Y' &&
                    var_106 != 0)
                {
                    gbl.byte_1D2C6 = true;

                    ovr024.remove_affect(null, Affects.funky__32, player01);
                    ovr024.remove_affect(null, Affects.poisoned, player01);

                    gbl.byte_1D2C6 = false;

                    player01.hit_point_current = 1;
                    player01.health_status = Status.okey;
                    player01.in_combat = true;

                    if (player01.con <= 0)
                    {
                        player01.con--;
                    }

                    if (player01.hit_point_max > player01.field_12C)
                    {
                        var_107 = (byte)(player01.hit_point_max - player01.field_12C);
                    }
                    else
                    {
                        var_107 = 0;
                    }

                    var_108 = 0;

                    if (player01.con >= 14)
                    {
                        for (var_109 = 0; var_109 <= 7; var_109++)
                        {
                            if (player01.Skill_A_lvl[var_109] > 0)
                            {
                                if (var_109 == 2)
                                {
                                    var_108 += (byte)((player01.con - 14) * player01.fighter_lvl);
                                }
                                else if (player01.con > 15)
                                {
                                    var_108 += (byte)(player01.Skill_A_lvl[var_109] * 2);
                                }
                                else
                                {
                                    var_108 += player01.Skill_A_lvl[var_109];
                                }
                            }
                        }

                        if (var_108 > 0)
                        {
                            var_107 /= var_108;
                        }

                        if (player01.con < 17 ||
                            player01.fighter_lvl > 0 ||
                            player01.fighter_lvl > player01.field_E6)
                        {
                            player01.hit_point_max = var_107;
                        }
                    }
                }
            }
        }


        internal static void cure_poison2()
        {
            byte var_106;
            char var_105;
            Affect var_4;

            var_106 = 0;

            var_105 = 'Y';

            if (ovr025.find_affect(out var_4, Affects.poisoned, gbl.player_ptr) == true)
            {
                var_106 = 1;
            }

            if (var_106 == 0)
            {
                var_105 = cast_cure_anyway("is not poisoned.");
            }

            if (var_105 == 'Y')
            {
                var_105 = buy_cure(1000, "Neutralize Poison");

                if (var_105 == 'Y')
                {
                    gbl.byte_1D2C6 = true;

                    ovr024.remove_affect(null, Affects.poisoned, gbl.player_ptr);
                    ovr024.remove_affect(null, Affects.slow_poison, gbl.player_ptr);
                    ovr024.remove_affect(null, Affects.affect_0f, gbl.player_ptr);

                    gbl.byte_1D2C6 = false;
                }
            }
        }


        internal static void remove_curse()
        {
            char input_key = 'Y';
            bool cursed_item_found = false;

            Item item = gbl.player_ptr.itemsPtr;

            while (item != null && cursed_item_found == false)
            {
                if (item.cursed)
                {
                    cursed_item_found = true;
                }

                item = item.next;
            }

            Affect dummyAffect;

            if (cursed_item_found == false &&
                ovr025.find_affect(out dummyAffect, Affects.bestow_curse, gbl.player_ptr) == false)
            {
                input_key = cast_cure_anyway("is not cursed.");
            }

            if (input_key == 'Y')
            {
                input_key = buy_cure(3500, "Remove Curse");

                if (input_key == 'Y')
                {
                    gbl.sp_targets[1] = gbl.player_ptr;
                    ovr023.uncurse();
                }
            }
        }


        internal static void stone_to_flesh()
        {
            char input_key = 'Y';

            if (gbl.player_ptr.health_status != Status.stoned)
            {
                input_key = cast_cure_anyway("is not stoned.");
            }

            if (input_key == 'Y')
            {
                input_key = buy_cure(2000, "Stone to Flesh");

                if (input_key == 'Y' &&
                    gbl.player_ptr.health_status == Status.stoned)
                {
                    gbl.player_ptr.health_status = Status.okey;
                    gbl.player_ptr.in_combat = true;
                    gbl.player_ptr.hit_point_current = 1;
                }
            }
        }


        internal static void temple_heal()
        {
            short sl_index = 0;

            bool end_shop = false;
            StringList stringListPtr = null;
            StringList stringList = ovr027.alloc_stringList(10);

            stringListPtr = stringList;
            int temple_index = 0;

            while (stringList != null)
            {
                stringList.s = temple_sl[temple_index];
                stringList.field_29 = 0;

                stringList = stringList.next;
                temple_index++;
            }

            stringList = stringListPtr;

            seg037.draw8x8_clear_area(0x18, 0x27, 0x18, 0);
            bool var_9 = true;
            seg037.draw8x8_04();

            do
            {
                string text = gbl.player_ptr.name + ", how can we help you?";
                seg041.displayString(text, 0, 15, 1, 1);

                char sl_output = ovr027.sl_select_item(out stringListPtr, ref sl_index, ref var_9, false,
                    stringList, 15, 0x26, 4, 2, 15, 10, 13, "Heal Exit", string.Empty);

                if (sl_output == 'H' || sl_output == 0x0d)
                {
                    switch (sl_index)
                    {
                        case 0:
                            cure_blindness();
                            break;

                        case 1:
                            cure_disease();
                            break;

                        case 2:
                            cure_wounds(1);
                            break;

                        case 3:
                            cure_wounds(2);
                            break;

                        case 4:
                            cure_wounds(3);
                            break;

                        case 5:
                            cure_wounds(4);
                            break;

                        case 6:
                            cure_poison2();
                            break;

                        case 7:
                            raise_dead();
                            break;

                        case 8:
                            remove_curse();
                            break;

                        case 9:
                            stone_to_flesh();
                            break;

                        case 10:
                            end_shop = true;
                            break;
                    }
                }
                else if (sl_output == 0)
                {
                    end_shop = true;
                }

            } while (end_shop == false);

            ovr027.free_stringList(ref stringList);

            ovr025.load_pic();
            ovr025.Player_Summary(gbl.player_ptr);
        }


        internal static void temple_shop()
        {
            bool var_30 = false; /* Simeon */
            bool money_present;

            gbl.game_state = 1;
            gbl.byte_1EE7E = false;

            if (gbl.area_ptr.field_1CC == 0)
            {
                gbl.byte_1EE7E = true;
            }

            ovr025.load_pic();
            gbl.byte_1EE7E = true;
            ovr025.Player_Summary(gbl.player_ptr);

            for (int i = 0; i < 7; i++)
            {
                gbl.pooled_money[i] = 0;
            }

            gbl.something01 = false;
            bool stop_loop = false;

            do
            {
                bool items_present;
                ovr022.treasureOnGround(out items_present, out money_present);
                string text;
                if (money_present == true)
                {
                    text = "Heal View Take Pool Share Appraise Exit";
                }
                else
                {
                    text = "Heal View Pool Appraise Exit";
                }

                bool ctrl_key;
                char input_key = ovr027.displayInput(out ctrl_key, false, 1, 15, 10, 13, text, string.Empty);

                switch (input_key)
                {
                    case 'H':
                        if (ctrl_key == false)
                        {
                            temple_heal();
                        }
                        break;

                    case 'V':
                        bool dummyBool;
                        ovr020.viewPlayer(out dummyBool);
                        break;

                    case 'T':
                        ovr022.takeItems();
                        break;

                    case 'P':
                        if (ctrl_key == false)
                        {
                            ovr022.poolMoney();
                        }
                        break;

                    case 'S':
                        ovr022.share_pooled();
                        break;

                    case 'A':
                        ovr022.appraiseGemsJewels(out var_30);
                        break;

                    case 'E':
                        ovr022.treasureOnGround(out items_present, out money_present);

                        if (money_present == true)
                        {
                            string prompt = "~Yes ~No";

                            seg041.press_any_key("As you leave a priest says, \"Excuse me but you have left some money here\" ", true, 0, 10, 0x16, 0x26, 0x11, 1);
                            seg041.press_any_key("Do you want to go back and retrieve your money?", true, 0, 10, 0x16, 0x26, 0x11, 1);
                            int menu_selected = ovr008.sub_317AA(false, 0, 15, 10, 13, prompt, string.Empty);

                            if (menu_selected == 1)
                            {
                                stop_loop = true;
                            }
                            else
                            {
                                seg037.draw8x8_clear_area(0x16, 0x26, 17, 1);
                            }
                        }
                        else
                        {
                            stop_loop = true;
                        }

                        break;

                    case 'G':
                        ovr020.scroll_team_list(input_key);
                        break;

                    case 'O':
                        ovr020.scroll_team_list(input_key);
                        break;
                }

                if (input_key == 0x42 ||
                    input_key == 0x54)
                {
                    ovr025.load_pic();
                }
                else if (input_key == 'A' && var_30 == true)
                {
                    ovr025.load_pic();
                }

                ovr025.Player_Summary(gbl.player_ptr);
            } while (stop_loop == false);
        }
    }
}
