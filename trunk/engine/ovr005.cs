using Classes;
using System;
using System.Collections.Generic;

namespace engine
{
    class ovr005
    {
        static Affects[] disease_types = {  Affects.helpless,  Affects.cause_disease_1,
                                            Affects.weaken, Affects.cause_disease_2,
                                            Affects.animate_dead, Affects.affect_39 };

        static string[] temple_sl = { "Cure Blindness", "Cure Disease", "Cure Light Wounds", "Cure Serious Wounds", "Cure Critical Wounds", "Heal", "Neutralize Poison", "Raise Dead", "Remove Curse", "Stone to Flesh", "Exit" };


        static bool CastCureAnyway(string text)
        {
            ovr025.DisplayPlayerStatusString(false, 0, text, gbl.SelectedPlayer);

            char ret_val = ovr027.yes_no(gbl.defaultMenuColors, "cast cure anyway: ");

            ovr025.ClearPlayerTextArea();

            return ret_val == 'Y';
        }


        internal static bool buy_cure(int cost, string cure_name) /* buy_cure */
        {
            string text = string.Format("{0} will only cost {1} gold pieces.", cure_name, cost);
            seg041.press_any_key(text, true, 10, TextRegion.NormalBottom);

            bool buy = false;

            if ('Y' == ovr027.yes_no(gbl.defaultMenuColors, "pay for cure "))
            {
                if (cost <= gbl.SelectedPlayer.Money.GetGoldWorth())
                {
                    gbl.SelectedPlayer.Money.SubtractGoldWorth(cost);
                    buy = true;
                }
                else if (cost <= gbl.pooled_money.GetGoldWorth())
                {
                    gbl.pooled_money.SubtractGoldWorth(cost);
                    buy = true;
                }
                else
                {
                    ovr025.string_print01("Not enough money.");
                    buy = false;
                }
            }

            if (buy)
            {
                ovr025.ClearPlayerTextArea();
                ovr025.DisplayPlayerStatusString(true, 0, "is cured.", gbl.SelectedPlayer);
            }

            return buy;
        }


        internal static void cure_blindness()
        {
            bool cast = true;

            if (gbl.SelectedPlayer.HasAffect(Affects.blinded) == false)
            {
                cast = CastCureAnyway("is not blind.");
            }

            if (cast)
            {
                if (buy_cure(1000, "Cure Blindness"))
                {
                    ovr024.remove_affect(null, Affects.blinded, gbl.SelectedPlayer);
                }
            }
        }


        internal static void cure_disease()
        {
            bool is_diseased = Array.Exists(disease_types, aff => gbl.SelectedPlayer.HasAffect(aff));

            bool cast = true;
            if (is_diseased == false)
            {
                cast = CastCureAnyway("is not diseased.");
            }

            if (cast)
            {
                if (buy_cure(1000, "Cure Disease"))
                {
                    gbl.cureSpell = true;
                    for (int i = 0; i < 6; i++)
                    {
                        ovr024.remove_affect(null, disease_types[i], gbl.SelectedPlayer);
                    }

                    gbl.cureSpell = false;
                }
            }
        }


        internal static void cure_wounds(int healType)
        {
            switch (healType)
            {
                case 1:
                    if (buy_cure(100, "Cure Light Wounds"))
                    {
                        int heal_amount = ovr024.roll_dice(8, 1);
                        ovr024.heal_player(0, heal_amount, gbl.SelectedPlayer);
                    }
                    break;

                case 2:
                    if (buy_cure(350, "Cure Serious Wounds"))
                    {
                        int heal_amount = ovr024.roll_dice(8, 2) + 1;
                        ovr024.heal_player(0, heal_amount, gbl.SelectedPlayer);
                    }
                    break;

                case 3:
                    if (buy_cure(600, "Cure Critical Wounds"))
                    {
                        int heal_amount = ovr024.roll_dice(8, 3) + 3;
                        ovr024.heal_player(0, heal_amount, gbl.SelectedPlayer);
                    }
                    break;

                case 4:
                    if (buy_cure(5000, "Heal"))
                    {
                        int heal_amount = gbl.SelectedPlayer.hit_point_max;
                        heal_amount -= gbl.SelectedPlayer.hit_point_current;
                        heal_amount -= ovr024.roll_dice(4, 1);

                        ovr024.heal_player(0, heal_amount, gbl.SelectedPlayer);
                        ovr024.remove_affect(null, Affects.blinded, gbl.SelectedPlayer);

                        for (int i = 0; i < 6; i++)
                        {
                            ovr024.remove_affect(null, disease_types[i], gbl.SelectedPlayer);
                        }

                        ovr024.remove_affect(null, Affects.feeblemind, gbl.SelectedPlayer);

                        ovr024.CalcStatBonuses(Stat.INT, gbl.SelectedPlayer);
                        ovr024.CalcStatBonuses(Stat.WIS, gbl.SelectedPlayer);
                    }
                    break;
            }
        }


        internal static void raise_dead()
        {
            Player player = gbl.SelectedPlayer;
            bool player_dead = false;

            if (player.health_status == Status.dead ||
                player.health_status == Status.animated)
            {
                player_dead = true;
            }

            if (player_dead == true ||
                (player_dead == false && CastCureAnyway("is not dead.")))
            {
                if (buy_cure(5500, "Raise Dead") && player_dead == true)
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
                    if (player.hit_point_max > player.hit_point_rolled)
                    {
                        var_107 = player.hit_point_max - player.hit_point_rolled;
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
                            if (player.ClassLevel[classIdx] > 0)
                            {
                                if (classIdx == 2)
                                {
                                    var_108 += (player.con - 14) * player.fighter_lvl;
                                }
                                else if (player.con > 15)
                                {
                                    var_108 += player.ClassLevel[classIdx] * 2;
                                }
                                else
                                {
                                    var_108 += player.ClassLevel[classIdx];
                                }
                            }
                        }

                        if (var_108 > 0)
                        {
                            var_107 /= var_108;
                        }

                        if (player.con < 17 ||
                            player.fighter_lvl > 0 ||
                            player.fighter_lvl > player.multiclassLevel)
                        {
                            player.hit_point_max = (byte)var_107;
                        }
                    }
                }
            }
        }


        internal static void cure_poison2()
        {
            bool isPoisoned = gbl.SelectedPlayer.HasAffect(Affects.poisoned);

            if (isPoisoned == true ||
                (isPoisoned == false && CastCureAnyway("is not poisoned.")))
            {
                if (buy_cure(1000, "Neutralize Poison"))
                {
                    gbl.cureSpell = true;

                    ovr024.remove_affect(null, Affects.poisoned, gbl.SelectedPlayer);
                    ovr024.remove_affect(null, Affects.slow_poison, gbl.SelectedPlayer);
                    ovr024.remove_affect(null, Affects.poison_damage, gbl.SelectedPlayer);

                    gbl.cureSpell = false;
                }
            }
        }


        internal static void remove_curse()
        {
            bool has_curse_items = gbl.SelectedPlayer.items.Find(item => item.cursed) != null;
            bool cast = true;

            if (has_curse_items == false &&
                gbl.SelectedPlayer.HasAffect(Affects.bestow_curse) == false)
            {
                cast = CastCureAnyway("is not cursed.");
            }

            if (cast && buy_cure(3500, "Remove Curse"))
            {
                gbl.spellTargets.Clear();
                gbl.spellTargets.Add(gbl.SelectedPlayer);
                ovr023.SpellRemoveCurse();
            }
        }


        internal static void stone_to_flesh()
        {
            if (gbl.SelectedPlayer.health_status == Status.stoned ||
                (gbl.SelectedPlayer.health_status != Status.stoned && CastCureAnyway("is not stoned.")))
            {
                if (buy_cure(2000, "Stone to Flesh") &&
                    gbl.SelectedPlayer.health_status == Status.stoned)
                {
                    gbl.SelectedPlayer.health_status = Status.okey;
                    gbl.SelectedPlayer.in_combat = true;
                    gbl.SelectedPlayer.hit_point_current = 1;
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
                string text = gbl.SelectedPlayer.name + ", how can we help you?";
                seg041.displayString(text, 0, 15, 1, 1);
                MenuItem dummySelected;

                char sl_output = ovr027.sl_select_item(out dummySelected, ref sl_index, ref redrawMenuItems, false,
                    stringList, 15, 0x26, 4, 2, gbl.defaultMenuColors, "Heal Exit", string.Empty);

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
            ovr025.PartySummary(gbl.SelectedPlayer);
        }


        internal static void temple_shop()
        {
            bool reloadPics = false;

            gbl.game_state = GameState.Shop;
            gbl.redrawBoarder = (gbl.area_ptr.inDungeon == 0);

            ovr025.load_pic();
            gbl.redrawBoarder = true;
            ovr025.PartySummary(gbl.SelectedPlayer);

            gbl.pooled_money.ClearAll();

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
                char input_key = ovr027.displayInput(out ctrl_key, false, 1, gbl.defaultMenuColors, text, string.Empty);

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

                            seg041.press_any_key("As you leave a priest says, \"Excuse me but you have left some money here\" ", true, 10, TextRegion.NormalBottom);
                            seg041.press_any_key("Do you want to go back and retrieve your money?", true, 10, TextRegion.NormalBottom);
                            int menu_selected = ovr008.sub_317AA(false, false, gbl.defaultMenuColors, prompt, "");

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

                ovr025.PartySummary(gbl.SelectedPlayer);
            } while (stop_loop == false);
        }
    }
}
