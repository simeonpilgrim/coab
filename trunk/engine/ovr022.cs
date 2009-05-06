using Classes;
using System.Collections.Generic;

namespace engine
{
    class ovr022
    {
        internal static int get_max_load(Player player)
        {
            return 1500 + ovr025.max_encumberance(player);
        }


        internal static void remove_weight(short amount, Player player)
        {
            player.weight -= amount;
        }


        internal static void add_weight(short amount, Player player)
        {
            player.weight += amount;
        }

        internal static bool willOverload(int item_weight, Player player)
        {
            short dummyShort;
            return willOverload(out dummyShort, item_weight, player);
        }


        internal static bool willOverload(out short weight, int item_weight, Player player)
        {
            bool ret_val;

            if ((player.weight + item_weight) > get_max_load(player))
            {
                weight = (short)(get_max_load(player) - player.weight);
                ret_val = true;
            }
            else
            {
                weight = 0;
                ret_val = false;
            }

            return ret_val;
        }


        internal static int getPooledGold()
        {
            int total = 0;
            for (int i = 0; i < 5; i++)
            {
                total += Money.per_copper[i] * gbl.pooled_money[i];
            }

            return total / Money.per_copper[Money.gold];
        }


        internal static void setPooledGold(int gold)
        {
            for (int i = 0; i < 5; i++)
            {
                gbl.pooled_money[i] = 0;
            }

            gbl.pooled_money[Money.platum] = gold / 5;
            gbl.pooled_money[Money.gold] = gold % 5;
        }


        internal static void setPlayerMoney(int gold)
        {
            for (int i = 0; i < 5; i++)
            {
                gbl.player_ptr.Money[i] = 0;
            }

            gbl.player_ptr.platinum = (short)(gold / 5);
            gbl.player_ptr.gold = (short)(gold % 5);
        }


        internal static void addPlayerGold(short item_weight)
        {
            short capasity;

            if (willOverload(out capasity, item_weight, gbl.player_ptr) == true)
            {
                ovr025.string_print01("Overloaded. Money will be put in Pool.");
                gbl.player_ptr.platinum += capasity;
                add_weight(capasity, gbl.player_ptr);

                gbl.pooled_money[Money.platum] += item_weight - capasity;
            }
            else
            {
                gbl.player_ptr.platinum += item_weight;
                add_weight(item_weight, gbl.player_ptr);
            }
        }


        internal static short sub_592AD(byte fgColor, string prompt, int maxValue) // sub_592AD
        {
            ovr027.ClearPromptAreaNoUpdate();
            seg041.displayString(prompt, 0, fgColor, 0x18, 0);

            int prompt_width = prompt.Length;
            int xCol = prompt_width;

            char inputKey;
            string maxValueStr = maxValue.ToString();
            string currentValueStr = string.Empty;

            do
            {
                inputKey = (char)seg043.GetInputKey();

                if (inputKey >= 0x30 &&
                    inputKey <= 0x39)
                {
                    currentValueStr += inputKey.ToString();

                    int tmpValue = int.Parse(currentValueStr);

                    if (maxValue >= tmpValue)
                    {
                        xCol++;
                    }
                    else
                    {
                        currentValueStr = maxValueStr;

                        xCol = maxValueStr.Length + prompt_width;
                    }

                    seg041.displayString(currentValueStr, 0, 15, 0x18, prompt_width);
                }
                else if (inputKey == 8 && currentValueStr.Length > 0)
                {
                    int i = currentValueStr.Length - 1;
                    currentValueStr = seg051.Copy(i, 1, currentValueStr);

                    seg041.displaySpaceChar(1, 0, 0x18, xCol);
                    xCol--;
                }
            } while (inputKey != 0x0D && inputKey != 0x1B);

            ovr027.ClearPromptAreaNoUpdate();

            int var_44;
            if (inputKey == 0x1B)
            {
                var_44 = 0;
            }
            else
            {
                var_44 = int.Parse(currentValueStr);
            }

            return (short)var_44;
        }


        internal static void trade_money(int money_slot, short num_coins, Player dest, Player source) /* add_object */
        {
            if ((dest.weight + num_coins) <= get_max_load(dest))
            {
                source.Money[money_slot] -= num_coins;

                remove_weight(num_coins, source);

                dest.Money[money_slot] += num_coins;

                add_weight(num_coins, dest);
            }
            else
            {
                ovr025.string_print01("Overloaded");
            }
        }


        internal static void poolMoney()
        {
            foreach (Player player in gbl.player_next_ptr)
            {
                if (player.control_morale == Control.PC_Base ||
                    player.control_morale == Control.PC_Berzerk)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        gbl.pooled_money[i] += player.Money[i];

                        remove_weight(player.Money[i], player);

                        player.Money[i] = 0;
                    }
                }
            }
        }


        internal static int GetPartyCount() /* sub_595FF */
        {
            int count = 0;
            
            foreach (Player player in gbl.player_next_ptr)
            {
                if (player.control_morale == Control.PC_Base ||
                    player.control_morale == Control.PC_Berzerk)
                {
                    count++;
                }
            }

            return count;
        }


        internal static void share_pooled()
        {
            short[] money_remander;
            short[] money_each;

            int partySize = GetPartyCount();

            money_each = new short[7];
            money_remander = new short[7];

            for (int var_29 = 0; var_29 <= 6; var_29++)
            {
                if (gbl.pooled_money[var_29] > 0)
                {
                    money_each[var_29] = (short)(gbl.pooled_money[var_29] / partySize);
                    money_remander[var_29] = (short)(gbl.pooled_money[var_29] % partySize);
                }
                else
                {
                    money_each[var_29] = 0;
                    money_remander[var_29] = 0;
                }
            }

            foreach (Player var_4 in gbl.player_next_ptr)
            {
                if (var_4.control_morale < Control.NPC_Base)
                {
                    for (int var_29 = 6; var_29 >= 0; var_29--)
                    {
                        short var_C;
                        if (willOverload(out var_C, money_each[var_29], var_4) == false)
                        {
                            var_4.Money[var_29] += money_each[var_29];

                            add_weight(money_each[var_29], var_4);

                            if (money_remander[var_29] > 0 &&
                                willOverload(1, var_4) == false)
                            {
                                var_4.Money[var_29] += 1;

                                add_weight(1, var_4);
                                money_remander[var_29] -= 1;
                            }
                        }
                        else
                        {
                            var_4.Money[var_29] += var_C;

                            money_remander[var_29] += (short)(money_each[var_29] - var_C);

                            add_weight(var_C, var_4);
                        }
                    }
                }
            }

            for (int var_29 = 6; var_29 >= 0; var_29--)
            {
                if (money_remander[var_29] > 0)
                {
                    foreach (Player var_4 in gbl.player_next_ptr)
                    {
                        short var_C = (short)(get_max_load(var_4) - var_4.weight);

                        if (var_C > 0)
                        {
                            if (money_remander[var_29] > var_C)
                            {
                                var_4.Money[var_29] += var_C;
                                add_weight(var_C, var_4);
                                money_remander[var_29] -= var_C;
                            }
                            else
                            {
                                var_4.Money[var_29] += money_remander[var_29];
                                add_weight(money_remander[var_29], var_4);
                                money_remander[var_29] = 0;
                            }
                        }
                    }
                }
            }

            for (int var_29 = 0; var_29 <= 6; var_29++)
            {
                gbl.pooled_money[var_29] = money_remander[var_29];
            }
        }


        internal static void drop_coins(int money_slot, short num_coins, Player player) /* sub_59A19 */
        {
            player.Money[money_slot] -= num_coins;
            remove_weight(num_coins, player);

            if (gbl.game_state == GameState.State6 ||
                gbl.game_state == GameState.Shop)
            {
                gbl.pooled_money[money_slot] += num_coins;
            }
        }


        internal static void sub_59AA0(int money_slot, short num_coins, Player player) /* sub_59AA0 */
        {
            short dummy_short;

            if (willOverload(out dummy_short, num_coins, player) == true)
            {
                ovr025.string_print01("Overloaded");
            }
            else
            {
                if (num_coins > gbl.pooled_money[money_slot])
                {
                    num_coins = (short)gbl.pooled_money[money_slot];
                }

                gbl.pooled_money[money_slot] -= num_coins;

                player.Money[money_slot] += num_coins;

                add_weight(num_coins, player);
            }
        }


        internal static int GetMoneyIndexFromString(out string displayText, string input) // sub_59BAB
        {
            int offset = 0;
            int index = 7; // this is outofbounds.
            displayText = string.Empty;

            while (input[offset] == ' ')
            {
                offset++;
            }

            char ch = input[offset];
            if (ch == 'G')
            {
                ch = input[offset + 1];
                if (char.ToUpper(ch) == 'E')
                {
                    index = 5;
                    displayText = "Gems ";
                }
                else
                {
                    displayText = "Gold ";
                    index = 3;
                }
            }
            else if (ch == 'P')
            {
                displayText = "Platinum ";
                index = 4;
            }
            else if (ch == 'E')
            {
                displayText = "Electrum ";
                index = 2;
            }
            else if (ch == 'S')
            {
                displayText = "Silver ";
                index = 1;
            }
            else if (ch == 'C')
            {
                displayText = "Copper ";
                index = 0;
            }
            else if (ch == 'J')
            {
                displayText = "Jewelry ";
                index = 6;
            }

            return index;
        }


        internal static void TakePoolMoney() // takeItems
        {
            bool noMoneyLeft;

            List<MenuItem> money = new List<MenuItem>();
            sbyte var_1;

            seg037.DrawFrame_Outer();

            do
            {
                bool var_118 = true;

                money.Clear();

                for (var_1 = 6; var_1 >= 0; var_1--)
                {
                    if (gbl.pooled_money[var_1] > 0)
                    {
                        money.Add(new MenuItem(string.Format("{0} {1}", Money.names[var_1], gbl.pooled_money[var_1])));
                    }
                }

                int dummyIndex = 0;

                MenuItem var_C;
                char input_key = ovr027.sl_select_item(out var_C, ref dummyIndex, ref var_118, true, money,
                    8, 15, 2, 2, 15, 10, 13, "Select", "Select type of coin ");

                if (var_C == null || input_key == 0)
                {
                    noMoneyLeft = true;
                }
                else
                {
                    noMoneyLeft = false;
                    string text;

                    int money_slot = GetMoneyIndexFromString(out text, var_C.Text);

                    text = string.Format("How much {0} will you take? ", text);

                    short num_coins = sub_592AD(10, text, gbl.pooled_money[money_slot]);

                    sub_59AA0(money_slot, num_coins, gbl.player_ptr);
                    money.Clear();

                    noMoneyLeft = true;
                    for (var_1 = 0; var_1 < 7; var_1++)
                    {
                        if (gbl.pooled_money[var_1] > 0)
                        {
                            noMoneyLeft = false;
                        }
                    }
                }
            } while (noMoneyLeft == false);
        }


        internal static void treasureOnGround(out bool items, out bool money)
        {
            money = false;

            for (int i = 0; i < 7; i++)
            {
                if (gbl.pooled_money[i] != 0)
                {
                    money = true;
                }
            }

            items = gbl.items_pointer.Count > 0;
        }


        internal static sbyte randomBonus() // sub_59FCF
        {
            sbyte bonus = 0;

            int roll = ovr024.roll_dice(20, 1);

            if (roll >= 1 && roll <= 14)
            {
                bonus = 1;
            }
            else if (roll >= 15 && roll <= 20)
            {
                bonus = 2;
            }

            return bonus;
        }

        static short[,] /*seg600:082E unk_16B3E */	preconfiiguredItems = { 
            {0x00B9, 0x00BB, 0x0040, 0x0001, 0x0320, 0x0003, 0x0063, 0x0000},
            {0x00EF, 0x00A7, 0x0040, 0x0001, 0x044C, 0x0001, 0x003b, 0x0000},
            {0x00b9, 0x00a7, 0x0040, 0x0001, 0x0190, 0x0001, 0x0003, 0x0000},
            {0x00AD, 0x00a7, 0x0040, 0x0001, 0x01C2, 0x0001, 0x0030, 0x0000},
            {0x00CE, 0x00A7, 0x0045, 0x0001, 0x2AF8, 0x001E, 0x000F, 0x0000},
            {0x00E2, 0x00A7, 0x0064, 0x000A, 0x3A98, 0x0000, 0x0026, 0x0083},
            {0x009d, 0x00a7, 0x0015, 0x0014, 0x0bb8, 0x0001, 0x0033, 0x0000} };

        internal static Item create_item(int item_type) /* sub_5A007 */
        {
            byte var_5 = 0; /* Simeon */

            Item item = new Item();
            int var_4 = -1;

            item.affect_1 = 0;
            item.affect_2 = 0;
            item.affect_3 = 0;

            item.readied = false;
            item.hidden_names_flag = 6;
            item.cursed = false;

            item.type = (byte)item_type;

            item.namenum1 = 0;
            item.namenum2 = 0;
            item.namenum3 = 0;

            byte type = item.type;

            if ((type >= 1 && type <= 0x3B) ||
                type == 0x49 || type == 0x4D || type == 0x5D)
            {
                item.plus = randomBonus();

                if (item.type == 21) // Javelin
                {
                    int var_1 = ovr024.roll_dice(5, 1);
                    if (var_1 == 5)
                    {
                        var_4 = 6;
                    }

                    item.namenum3 = 0x15;
                    item.namenum2 = item.plus + 0xA1;
                }
                else if (item.type == 28) // ItemType.Quarrel
                {
                    item.namenum3 = 0x1C;
                    item.namenum2 = item.plus + 0xA1;
                }
                else if (item.type == 0x32 ||
                         item.type == 0x33)
                {
                    item.namenum3 = item.type;
                    item.namenum2 = 0x31;
                    item.namenum1 = item.plus + 0xA1;
                    item.hidden_names_flag = 4;
                }
                else if (item.type == 0x34)
                {
                    item.namenum3 = item.type;
                    item.namenum2 = 0x32;
                    item.namenum1 =item.plus + 0xA1;
                    item.hidden_names_flag = 4;
                }
                else if (item.type >= 0x35 &&
                         item.type <= 0x3a)
                {
                    item.namenum3 = item.type;
                    item.namenum2 = 0x30;
                    item.namenum1 = item.plus + 0xA1;
                    item.hidden_names_flag = 4;
                }
                else if (item.type == 0x49)
                {
                    item.namenum3 = 0x3D;
                    item.namenum2 = item.plus + 0xA1;
                }
                else if (item.type == 0x4d)
                {
                    item.namenum3 = 0x4F;
                    item.namenum2 = 0xA7;
                    item.plus = (sbyte)((item.plus << 1) + 2);

                    if (item.plus == 4)
                    {
                        item.namenum1 = 0xdd;
                    }
                    else if (item.plus == 6)
                    {
                        item.namenum1 = 0xde;
                    }
                }
                else if (item.type == 0x5d)
                {
                    item.namenum3 = 0x42;
                    item.namenum2 = 0xE0;
                    item.namenum1 = item.plus + 0xA1;
                }
                else
                {
                    item.namenum3 = item.type;
                    item.namenum2 = item.plus + 0xA1;
                }

                item.plus_save = 0;
                item.count = 0;

                switch (item.type)
                {
                    case 1: // BattleAxe
                    case 13: // MilitaryFork
                    case 14: // Glaive
                    case 35: // BroadSword
                        item.weight = 75;
                        break;

                    case 2: // HandAxe
                    case 0x14:
                    case 0x1d:
                    case 0x1f:
                    case 0x20:
                    case 0x21:
                    case 0x27:
                    case 0x2a:
                    case 0x2c:
                    case 0x2e:
                    case 0x3b:
                        item.weight = 50;
                        break;

                    case 3: // Bardiche
                    case 24: // MorningStar
                    case 0x28:
                        item.weight = 0x7D;
                        break;

                    case 4:
                    case 0x0f:
                    case 0x17:
                    case 0x22:
                    case 0x2b:
                    case 0x2d:
                    case 0x33:
                        item.weight = 100;
                        break;

                    case 5:
                    case 0x0c:
                    case 0x11:
                    case 0x13:
                    case 0x32:
                        item.weight = 0x96;
                        break;

                    case 6:
                        item.weight = 15;
                        break;

                    case 7:
                        item.weight = 0x1E;
                        break;

                    case 8:
                    case 0x4d:
                        item.weight = 10;
                        break;

                    case 9:
                        item.weight = 0x19;
                        item.count = 5;
                        break;

                    case 10: // Fauchard
                    case 0x1a:
                    case 0x24:
                        item.weight = 0x3C;
                        break;

                    case 11: // FauchardFork
                    case 16: // Guisarme
                    case 25: // Partisan
                    case 0x1b:
                    case 0x29:
                    case 47: // Sling
                        item.weight = 80;
                        break;

                    case 18: // Halberd
                        item.weight = 175;
                        break;

                    case 21: // Javelin
                        item.weight = 20;
                        break;

                    case 22: // JoStick
                    case 30: // Scimitar
                        item.weight = 40;
                        break;

                    case 37: // ShortSword
                        item.weight = 35;
                        break;

                    case 38: // TwoHandedSword
                    case 53: // RingMail
                        item.weight = 250;
                        break;

                    case 52: // StuddedLeather
                        item.weight = 0x0C8;
                        break;

                    case 54: // ScaleMail
                    case 56: // SplintMail
                        item.weight = 400;
                        break;

                    case 0x37:
                        item.weight = 0x12C;
                        break;
                    case 0x39:
                        item.weight = 0x15E;
                        break;

                    case 58: // PlateMail
                        item.weight = 450;
                        break;

                    //case 0x2f: //wonder if this should have been 0x3f
                    case 93: // RingOfProtection
                        item.weight = 1;
                        break;

                    default:
                        item.weight = 40;
                        item.count = 10;
                        break;
                }

                if (item.type == 0x3b)
                {
                    item._value = (short)(item.plus * 2500);
                }
                else if (item.type == 0x49 || item.type == 0x1c)
                {
                    item._value = (short)(item.plus * 150);
                }
                else if (item.type == 0x35 || item.type == 0x36)
                {
                    item._value = (short)(item.plus * 3000);
                }
                else if (item.type == 0x37 || item.type == 0x38)
                {
                    item._value = (short)(item.plus * 3500);
                }
                else if (item.type == 57) // ItemType.BandedMail
                {
                    item._value = (short)(item.plus * 4000);

                }
                else if (item.type == 58) // ItemType.PlateMail
                {
                    item._value = (short)(item.plus * 5000);
                }
                else if (item.type == 77) // ItemType.Bracers
                {
                    item._value = (short)(item.plus * 3000);
                }
                else
                {
                    item._value = (short)(item.plus * 2000);
                }
            }
            else if (type == 61 || type == 62) // MagicUserScrol || ClericScroll
            {
                byte spellsCount = ovr024.roll_dice(3, 1);

                if (item.type == 61) // MagicUserScrol
                {
                    item.namenum3 = 0xD1;
                }
                else
                {
                    item.namenum3 = 0xD0;
                }

                item.namenum2 = spellsCount + 0xd1;
                item.namenum1 = 0;
                item.plus = 1;
                item.weight = 0x19;
                item.count = 0;
                item._value = 0;

                for (int var_3 = 1; var_3 <= spellsCount; var_3++)
                {
                    int var_1 = ovr024.roll_dice(5, 1);

                    if (item.type == 0x3D)
                    {
                        switch (var_1)
                        {
                            case 1:
                                var_5 = (byte)(ovr024.roll_dice(13, 1) + 8);
                                break;

                            case 2:
                                var_5 = (byte)(ovr024.roll_dice(7, 1) + 28);
                                break;

                            case 3:
                                var_5 = (byte)(ovr024.roll_dice(0x0B, 1) + 44);
                                break;

                            case 4:
                                var_5 = (byte)(ovr024.roll_dice(9, 1) + 80);
                                break;

                            case 5:
                                var_5 = (byte)(ovr024.roll_dice(4, 1) + 90);
                                break;
                        }
                    }
                    else
                    {
                        switch (var_1)
                        {
                            case 1:
                                var_5 = ovr024.roll_dice(8, 1);
                                break;

                            case 2:
                                var_5 = (byte)(ovr024.roll_dice(7, 1) + 0x15);
                                break;

                            case 3:
                                var_5 = (byte)(ovr024.roll_dice(8, 1) + 0x24);
                                break;

                            case 4:
                                var_5 = (byte)(ovr024.roll_dice(5, 1) + 0x41);
                                break;

                            case 5:
                                var_5 = (byte)(ovr024.roll_dice(6, 1) + 0x46);
                                break;
                        }
                    }

                    item.setAffect(var_3, (Affects)var_5);
                    item._value += (short)(var_1 * 300);
                }
            }
            else if (type == 0x3f || type == 0x43)
            {
                var_4 = 5;
            }
            else if (type == 0x4E || type == 0x4F)
            {
                var_4 = 4;
            }
            else if (type == 0x54 || type == 0x5C)
            {
                var_4 = 1;
            }
            else if (type == 71) // PotionOfHealing
            {
                int var_1 = ovr024.roll_dice(8, 1);

                if (var_1 >= 1 && var_1 <= 5)
                {
                    var_4 = 2;
                }
                else if (var_1 >= 6 && var_1 <= 8)
                {
                    var_4 = 0;
                }
            }

            if (var_4 > -1)
            {
                item.namenum1 = preconfiiguredItems[var_4, 0];
                item.namenum2 = preconfiiguredItems[var_4, 1];
                item.namenum3 = preconfiiguredItems[var_4, 2];

                item.plus = 1;
                item.plus_save = 1;

                item.weight = preconfiiguredItems[var_4, 3];
                item.count = 0;

                item._value = preconfiiguredItems[var_4, 4];

                for (int var_3 = 1; var_3 <= 3; var_3++)
                {
                    item.setAffect(var_3, (Affects)(byte)preconfiiguredItems[var_4, 4 + var_3]);
                }
            }

            return item;
        }

    
        /// <summary>
        /// Turns if pictures need re-loading
        /// </summary>
        internal static bool appraiseGemsJewels()
        {
            bool special_key;
            short value;
            string sell_text;

            if (gbl.player_ptr.gems == 0 && gbl.player_ptr.jewels == 0)
            {
                ovr025.string_print01("No Gems or Jewelry");
                return false;
            }

            bool stop_loop;

            do
            {
                if (gbl.player_ptr.gems == 0 && gbl.player_ptr.jewels == 0)
                {
                    stop_loop = true;
                }
                else
                {
                    stop_loop = false;

                    string gem_text = gbl.player_ptr.gems.ToString();
                    string jewel_text = gbl.player_ptr.jewels.ToString();

                    if (gbl.player_ptr.gems == 0)
                    {
                        gem_text = string.Empty;
                    }
                    else if (gbl.player_ptr.gems == 1)
                    {
                        gem_text += " Gem";
                    }
                    else
                    {
                        gem_text += " Gems";
                    }

                    if (gbl.player_ptr.jewels == 0)
                    {
                        jewel_text = string.Empty;
                    }
                    else if (gbl.player_ptr.jewels == 1)
                    {
                        jewel_text += " piece of Jewelry";
                    }
                    else
                    {
                        jewel_text += " pieces of Jewelry";
                    }

                    seg037.draw8x8_clear_area(0x16, 0x26, 1, 1);
                    ovr025.displayPlayerName(false, 1, 1, gbl.player_ptr);

                    seg041.displayString("You have a fine collection of:", 0, 0xf, 7, 1);
                    seg041.displayString(gem_text, 0, 0x0f, 9, 1);
                    seg041.displayString(jewel_text, 0, 0x0f, 0x0a, 1);
                    string prompt = string.Empty;

                    if (gbl.player_ptr.gems != 0)
                    {
                        prompt = "  Gems";
                    }

                    if (gbl.player_ptr.jewels != 0)
                    {
                        prompt += " Jewelry";
                    }

                    prompt += " Exit";

                    char input_key = ovr027.displayInput(out special_key, false, 1, 15, 10, 13, prompt, "Appraise : ");

                    if (input_key == 'G')
                    {
                        if (gbl.player_ptr.gems != 0)
                        {
                            gbl.player_ptr.gems -= 1;

                            int roll = ovr024.roll_dice(100, 1);

                            if (roll >= 1 && roll <= 25)
                            {
                                value = 10;
                            }
                            else if (roll >= 26 && roll <= 50)
                            {
                                value = 50;
                            }
                            else if (roll >= 51 && roll <= 70)
                            {
                                value = 100;
                            }
                            else if (roll >= 71 && roll <= 90)
                            {
                                value = 500;
                            }
                            else if (roll >= 91 && roll <= 99)
                            {
                                value = 1000;
                            }
                            else if (roll == 100)
                            {
                                value = 5000;
                            }
                            else
                            {
                                value = 0;
                            }

                            string value_text = "The Gem is Valued at " + value.ToString() + " gp.";

                            seg041.displayString(value_text, 0, 15, 12, 1);

                            bool must_sell;

                            if (willOverload(1, gbl.player_ptr) == true ||
                                gbl.player_ptr.items.Count >= Player.MaxItems)
                            {
                                sell_text = "Sell";
                                must_sell = true;
                            }
                            else
                            {
                                sell_text = "Sell Keep";
                                must_sell = false;
                            }

                            input_key = ovr027.displayInput(out special_key, false, 1, 15, 10, 13, sell_text, "You can : ");

                            if (input_key == 'K' && must_sell == false)
                            {
                                Item gem_item = new Item();
                                gem_item.weight = 1;
                                gem_item.hidden_names_flag = 0;
                                gem_item.readied = false;
                                gem_item.namenum3 = 0x65;
                                gem_item.namenum2 = 0;
                                gem_item.namenum1 = 0;
                                gem_item.type = 70; // ItemType.GemJewel
                                gem_item._value = value;

                                gbl.player_ptr.items.Add(gem_item);
                            }
                            else
                            {
                                value /= 5;
                                addPlayerGold(value);
                            }
                        }
                    }
                    else if (input_key == 'J')
                    {
                        if (gbl.player_ptr.jewels != 0)
                        {
                            gbl.player_ptr.jewels -= 1;

                            int roll = ovr024.roll_dice(100, 1);

                            if (roll >= 1 && roll <= 10)
                            {
                                value = (short)(seg051.Random(900) + 100);
                            }
                            else if (roll >= 11 && roll <= 20)
                            {
                                value = (short)(seg051.Random(1000) + 200);
                            }
                            else if (roll >= 21 && roll <= 40)
                            {
                                value = (short)(seg051.Random(1500) + 300);
                            }
                            else if (roll >= 41 && roll <= 50)
                            {
                                value = (short)(seg051.Random(2500) + 500);
                            }
                            else if (roll >= 51 && roll <= 70)
                            {
                                value = (short)(seg051.Random(5000) + 1000);
                            }
                            else if (roll >= 0x47 && roll <= 0x5A)
                            {
                                value = (short)(seg051.Random(6000) + 2000);
                            }
                            else if (roll >= 0x5B && roll <= 0x64)
                            {
                                value = (short)(seg051.Random(10000) + 2000);
                            }
                            else
                            {
                                value = 0;
                            }

                            string value_text = string.Format("The Jewel is Valued at {0} gp.", value);
                            seg041.displayString(value_text, 0, 15, 12, 1);

                            bool must_sell;
                            if (willOverload(1, gbl.player_ptr) == true ||
                                gbl.player_ptr.items.Count >= Player.MaxItems)
                            {
                                sell_text = "Sell";
                                must_sell = true;
                            }
                            else
                            {
                                sell_text = "Sell Keep";
                                must_sell = false;
                            }

                            input_key = ovr027.displayInput(out special_key, false, 1, 15, 10, 13, sell_text, "You can : ");

                            if (input_key == 'K' && must_sell == false)
                            {
                                Item jewel_item = new Item();
                                jewel_item.readied = false;
                                jewel_item.namenum3 = 0xD6;
                                jewel_item.namenum2 = 0;
                                jewel_item.type = 70; // ItemType.GemJewel
                                jewel_item.namenum1 = 0;

                                jewel_item._value = value;
                                jewel_item.hidden_names_flag = 0;
                                jewel_item.weight = 1;

                                gbl.player_ptr.items.Add(jewel_item);
                            }
                            else
                            {
                                value /= 5;
                                addPlayerGold(value);
                            }
                        }
                    }
                    else if (input_key == 'E' || input_key == 0)
                    {
                        stop_loop = true;
                    }

                    ovr025.reclac_player_values(gbl.player_ptr);
                }

            } while (stop_loop == false);

            return true;
        }
    }
}
