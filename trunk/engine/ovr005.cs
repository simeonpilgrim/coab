using Classes;
using System;
using System.Collections.Generic;

namespace engine
{
    class ovr005
    {
        static Affects[] disease_types = {  Affects.helpless,  Affects.cause_disease_1,
                                            Affects.affect_2b, Affects.cause_disease_2,
                                            Affects.animate_dead, Affects.affect_39 };

        static string[] temple_sl = { "Cure Blindness", "Cure Disease", "Cure Light Wounds", "Cure Serious Wounds", "Cure Critical Wounds", "Heal", "Neutralize Poison", "Raise Dead", "Remove Curse", "Stone to Flesh", "Exit" };


        static char CastCureAnyway(string text)
        {
            ovr025.DisplayPlayerStatusString(false, 0, text, gbl.player_ptr);
            char ret_val = ovr027.yes_no(15, 10, 13, "cast cure anyway: ");

            ovr025.ClearPlayerTextArea();

            return ret_val;
        }


        internal static char buy_cure(short cost, string cure_name)
        {
            string text = string.Format("{0} will only cost {1} gold pieces.", cure_name, cost);
            seg041.press_any_key(text, true, 0, 10, TextRegion.NormalBottom);

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
                    int pool_money = ovr022.getPooledGold();

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
            char input_key = 'Y';

            if (gbl.player_ptr.HasAffect(Affects.blinded) == false)
            {
                input_key = CastCureAnyway("is not blind.");
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
            bool is_diseased = Array.Exists(disease_types, aff => gbl.player_ptr.HasAffect(aff));

            char input_key = 'Y';
            if (is_diseased == false)
            {
                input_key = CastCureAnyway("is not diseased.");
            }

            if (input_key == 'Y')
            {
                input_key = buy_cure(1000, "Cure Disease");

                if (input_key == 'Y')
                {
                    gbl.cureSpell = true;
                    for (int i = 0; i < 6; i++)
                    {
                        ovr024.remove_affect(null, disease_types[i], gbl.player_ptr);
                    }

                    gbl.cureSpell = false;
                }
            }
        }


        internal static void cure_wounds(int arg_0)
        {
            bool var_4;
            char input_key;

            switch (arg_0)
            {
                case 1:
                    input_key = buy_cure(100, "Cure Light Wounds");
                    if (input_key == 'Y')
                    {
                        int heal_amount = ovr024.roll_dice(8, 1);
                        var_4 = ovr024.heal_player(0, heal_amount, gbl.player_ptr);
                    }
                    break;

                case 2:
                    input_key = buy_cure(350, "Cure Serious Wounds");
                    if (input_key == 'Y')
                    {
                        int heal_amount = ovr024.roll_dice(8, 2) + 1;
                        var_4 = ovr024.heal_player(0, heal_amount, gbl.player_ptr);
                    }
                    break;

                case 3:
                    input_key = buy_cure(600, "Cure Critical Wounds");
                    if (input_key == 'Y')
                    {
                        int heal_amount = ovr024.roll_dice(8, 3) + 3;
                        var_4 = ovr024.heal_player(0, heal_amount, gbl.player_ptr);
                    }
                    break;

                case 4:
                    input_key = buy_cure(5000, "Heal");
                    if (input_key == 'Y')
                    {
                        int heal_amount = gbl.player_ptr.hit_point_max;
                        heal_amount -= gbl.player_ptr.hit_point_current;
                        heal_amount -= ovr024.roll_dice(4, 1);

                        var_4 = ovr024.heal_player(0, heal_amount, gbl.player_ptr);
                        ovr024.remove_affect(null, Affects.blinded, gbl.player_ptr);

                        for (int i = 0; i < 6; i++)
                        {
                            ovr024.remove_affect(null, disease_types[i], gbl.player_ptr);
                        }

                        ovr024.remove_affect(null, Affects.feeblemind, gbl.player_ptr);

                        ovr024.sub_648D9(1, gbl.player_ptr);
                        ovr024.sub_648D9(2, gbl.player_ptr);
                    }
                    break;
            }
        }


        internal static void raise_dead()
        {
            Player player = gbl.player_ptr;
            bool player_dead = false;
            char input_key = 'Y';

            if (player.health_status == Status.dead ||
                player.health_status == Status.animated)
            {
                player_dead = true;
            }

            if (player_dead == false)
            {
                input_key = CastCureAnyway("is not dead.");
            }

            if (input_key == 'Y')
            {
                input_key = buy_cure(5500, "Raise Dead");

                if (input_key == 'Y' &&
                    player_dead ==true)
                {
                    gbl.cureSpell = true;

                    ovr024.remove_affect(null, Affects.animate_dead, player);
                    ovr024.remove_affect(null, Affects.poisoned, player);

                    gbl.cureSpell = false;

                    player.hit_point_current = 1;
                    player.health_status = Status.okey;
                    player.in_combat = true;

                    if (player.con <= 0)
                    {
                        player.con--;
                    }

                    int var_107;
                    if (player.hit_point_max > player.field_12C)
                    {
                        var_107 = player.hit_point_max - player.field_12C;
                    }
                    else
                    {
                        var_107 = 0;
                    }

                    int var_108 = 0;

                    if (player.con >= 14)
                    {
                        for (int classIdx = 0; classIdx <= 7; classIdx++)
                        {
                            if (player.class_lvls[classIdx] > 0)
                            {
                                if (classIdx == 2)
                                {
                                    var_108 += (player.con - 14) * player.fighter_lvl;
                                }
                                else if (player.con > 15)
                                {
                                    var_108 += player.class_lvls[classIdx] * 2;
                                }
                                else
                                {
                                    var_108 += player.class_lvls[classIdx];
                                }
                            }
                        }

                        if (var_108 > 0)
                        {
                            var_107 /= var_108;
                        }

                        if (player.con < 17 ||
                            player.fighter_lvl > 0 ||
                            player.fighter_lvl > player.field_E6)
                        {
                            player.hit_point_max = (byte)var_107;
                        }
                    }
                }
            }
        }


        internal static void cure_poison2()
        {
            bool isPoisoned = gbl.player_ptr.HasAffect(Affects.poisoned);

            char inutKey = 'Y';
            if (isPoisoned == false)
            {
                inutKey = CastCureAnyway("is not poisoned.");
            }

            if (inutKey == 'Y')
            {
                inutKey = buy_cure(1000, "Neutralize Poison");

                if (inutKey == 'Y')
                {
                    gbl.cureSpell = true;

                    ovr024.remove_affect(null, Affects.poisoned, gbl.player_ptr);
                    ovr024.remove_affect(null, Affects.slow_poison, gbl.player_ptr);
                    ovr024.remove_affect(null, Affects.affect_0f, gbl.player_ptr);

                    gbl.cureSpell = false;
                }
            }
        }


        internal static void remove_curse()
        {
            char input_key = 'Y';

            bool has_curse_items = gbl.player_ptr.items.Find(item => item.cursed) != null;

            if (has_curse_items == false &&
                gbl.player_ptr.HasAffect(Affects.bestow_curse) == false)
            {
                input_key = CastCureAnyway("is not cursed.");
            }

            if (input_key == 'Y')
            {
                input_key = buy_cure(3500, "Remove Curse");

                if (input_key == 'Y')
                {
                    gbl.spellTargets.Clear();
                    gbl.spellTargets.Add(gbl.player_ptr);
                    ovr023.SpellRemoveCurse();
                }
            }
        }


        internal static void stone_to_flesh()
        {
            char input_key = 'Y';

            if (gbl.player_ptr.health_status != Status.stoned)
            {
                input_key = CastCureAnyway("is not stoned.");
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
            int sl_index = 0;

            bool end_shop = false;

            List<MenuItem> stringList = new List<MenuItem>(10);

            for (int i = 0; i < 10; i++)
            {
                stringList.Add(new MenuItem(temple_sl[i]));
            }

            ovr027.ClearPromptAreaNoUpdate();
            bool redrawMenuItems = true;
            seg037.DrawFrame_WildernessMap();

            do
            {
                string text = gbl.player_ptr.name + ", how can we help you?";
                seg041.displayString(text, 0, 15, 1, 1);
                MenuItem dummySelected;

                char sl_output = ovr027.sl_select_item(out dummySelected, ref sl_index, ref redrawMenuItems, false,
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

            stringList.Clear();

            ovr025.load_pic();
            ovr025.PartySummary(gbl.player_ptr);
        }


        internal static void temple_shop()
        {
            bool reloadPics = false;

            gbl.game_state = GameState.Shop;
            gbl.redrawBoarder = (gbl.area_ptr.inDungeon == 0);

            ovr025.load_pic();
            gbl.redrawBoarder = true;
            ovr025.PartySummary(gbl.player_ptr);

            for (int i = 0; i < 7; i++)
            {
                gbl.pooled_money[i] = 0;
            }

            bool stop_loop = false;

            do
            {
                bool items_present;
                bool money_present;

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
                        ovr020.viewPlayer();
                        break;

                    case 'T':
                        ovr022.TakePoolMoney();
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
                        reloadPics = ovr022.appraiseGemsJewels();
                        break;

                    case 'E':
                        ovr022.treasureOnGround(out items_present, out money_present);

                        if (money_present == true)
                        {
                            string prompt = "~Yes ~No";

                            seg041.press_any_key("As you leave a priest says, \"Excuse me but you have left some money here\" ", true, 0, 10, TextRegion.NormalBottom);
                            seg041.press_any_key("Do you want to go back and retrieve your money?", true, 0, 10,  TextRegion.NormalBottom);
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

                if (input_key == 'B' ||
                    input_key == 'T')
                {
                    ovr025.load_pic();
                }
                else if (reloadPics == true)
                {
                    ovr025.load_pic();
                    reloadPics = false;
                }

                ovr025.PartySummary(gbl.player_ptr);
            } while (stop_loop == false);
        }
    }
}
