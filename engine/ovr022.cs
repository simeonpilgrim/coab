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

        internal static bool willOverload(int item_weight, Player player)
        {
            int dummyInt;
            return willOverload(out dummyInt, item_weight, player);
        }


        internal static bool willOverload(out int weight, int item_weight, Player player)
        {
            bool ret_val;

            if ((player.weight + item_weight) > get_max_load(player))
            {
                weight = get_max_load(player) - player.weight;
                ret_val = true;
            }
            else
            {
                weight = 0;
                ret_val = false;
            }

            return ret_val;
        }

        internal static void addPlayerGold(int item_weight)
        {
            int capasity;

            if (willOverload(out capasity, item_weight, gbl.SelectedPlayer) == true)
            {
                ovr025.string_print01("Overloaded. Money will be put in Pool.");
                gbl.SelectedPlayer.Money.AddCoins(Money.Platinum, capasity);
                gbl.SelectedPlayer.AddWeight(capasity);

                gbl.pooled_money.AddCoins(Money.Platinum, item_weight - capasity);
            }
            else
            {
                gbl.SelectedPlayer.Money.AddCoins(Money.Platinum, item_weight);
                gbl.SelectedPlayer.AddWeight(item_weight);
            }
        }


        internal static short AskNumberValue(byte fgColor, string prompt, int maxValue) // sub_592AD
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
                    currentValueStr = seg051.Copy(i, 0, currentValueStr);

					seg041.displaySpaceChar(0x18, xCol-1);
                    xCol--;
                }
            } while (inputKey != 0x0D && inputKey != 0x1B);

            ovr027.ClearPromptAreaNoUpdate();

            int var_44;
            if (inputKey == 0x1B || 
                (inputKey == 0x0D && currentValueStr.Length == 0))
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
                source.Money.AddCoins(money_slot, -num_coins);
                source.RemoveWeight(num_coins);

                dest.Money.AddCoins(money_slot, num_coins);
                dest.AddWeight(num_coins);
            }
            else
            {
                ovr025.string_print01("Overloaded");
            }
        }


        internal static void poolMoney()
        {
            foreach (Player player in gbl.TeamList)
            {
                if (player.control_morale == Control.PC_Base ||
                    player.control_morale == Control.PC_Berzerk)
                {
                    gbl.pooled_money += player.Money;
                    for (int coin = 0; coin < 7; coin++)
                    {
                        player.RemoveWeight(player.Money.GetCoins(coin));
                    }
                    player.Money.ClearAll();
                }
            }
        }


        internal static int GetPartyCount() /* sub_595FF */
        {
            int count = 0;

            foreach (Player player in gbl.TeamList)
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
            int[] money_remander = new int[7];
            int[] money_each = new int[7];

            int partySize = GetPartyCount();

            for (int coin = 0; coin <= 6; coin++)
            {
                if (gbl.pooled_money.GetCoins(coin) > 0)
                {
                    money_each[coin] = gbl.pooled_money.GetCoins(coin) / partySize;
                    money_remander[coin] = gbl.pooled_money.GetCoins(coin) % partySize;
                }
                else
                {
                    money_each[coin] = 0;
                    money_remander[coin] = 0;
                }
            }

            foreach (Player player in gbl.TeamList)
            {
                if (player.control_morale < Control.NPC_Base)
                {
                    for (int coin = 6; coin >= 0; coin--)
                    {
                        int overflow;
                        if (willOverload(out overflow, money_each[coin], player) == false)
                        {
                            player.Money.AddCoins(coin, money_each[coin]);
                            player.AddWeight(money_each[coin]);

                            if (money_remander[coin] > 0 &&
                                willOverload(1, player) == false)
                            {
                                player.Money.AddCoins(coin, 1);
                                player.AddWeight(1);
                                money_remander[coin] -= 1;
                            }
                        }
                        else
                        {
                            player.Money.AddCoins(coin, overflow);

                            money_remander[coin] += money_each[coin] - overflow;

                            player.AddWeight(overflow);
                        }
                    }
                }
            }

            for (int coin = 6; coin >= 0; coin--)
            {
                if (money_remander[coin] > 0)
                {
                    foreach (Player player in gbl.TeamList)
                    {
                        int capacity = get_max_load(player) - player.weight;

                        if (capacity > 0)
                        {
                            if (money_remander[coin] > capacity)
                            {
                                player.Money.AddCoins(coin, capacity);
                                player.AddWeight(capacity);
                                money_remander[coin] -= capacity;
                            }
                            else
                            {
                                player.Money.AddCoins(coin, money_remander[coin]);
                                player.AddWeight(money_remander[coin]);
                                money_remander[coin] = 0;
                            }
                        }
                    }
                }
            }

            for (int coin = Money.Copper; coin <= Money.Jewelry; coin++)
            {
                gbl.pooled_money.SetCoins(coin, money_remander[coin]);
            }
        }


        internal static void DropCoins(int money_slot, int num_coins, Player player) /* sub_59A19 */
        {
            player.Money.AddCoins(money_slot, -num_coins);
            player.RemoveWeight(num_coins);

            if (gbl.game_state == GameState.AfterCombat ||
                gbl.game_state == GameState.Shop)
            {
                gbl.pooled_money.AddCoins(money_slot, num_coins);
            }
        }


        internal static void PickupCoins(int money_slot, int num_coins, Player player) /* sub_59AA0 */
        {
            if (willOverload(num_coins, player) == true)
            {
                ovr025.string_print01("Overloaded");
            }
            else
            {
                if (num_coins > gbl.pooled_money.GetCoins(money_slot))
                {
                    num_coins = gbl.pooled_money.GetCoins(money_slot);
                }

                gbl.pooled_money.AddCoins(money_slot, -num_coins);

                player.Money.AddCoins(money_slot, num_coins);
                player.AddWeight(num_coins);
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

			seg037.DrawFrame_Outer();

			do
			{
				bool var_118 = true;

				money.Clear();

				for (int coin = 6; coin >= 0; coin--)
				{
					if (gbl.pooled_money.GetCoins(coin) > 0)
					{
						money.Add(new MenuItem(string.Format("{0} {1}", Money.names[coin], gbl.pooled_money.GetCoins(coin))));
					}
				}

				int dummyIndex = 0;

				MenuItem var_C;
				char input_key = ovr027.sl_select_item(out var_C, ref dummyIndex, ref var_118, true, money,
					8, 15, 2, 2, gbl.defaultMenuColors, "Select", "Select type of coin ");

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

					int num_coins = AskNumberValue(10, text, gbl.pooled_money.GetCoins(money_slot));

					PickupCoins(money_slot, num_coins, gbl.SelectedPlayer);
					money.Clear();

					noMoneyLeft = true;
					for (int coin = 0; coin < 7; coin++)
					{
						if (gbl.pooled_money.GetCoins(coin) > 0)
						{
							noMoneyLeft = false;
						}
					}
				}
			} while (noMoneyLeft == false);
		}


        internal static void treasureOnGround(out bool items, out bool money)
        {
            money = gbl.pooled_money.AnyMoney();
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

        static short[,] /*seg600:082E unk_16B3E */	preconfiguredItems = {
            {185, 187,  64,  1,   800,  3, 99,   0}, // potion extra healing
            {239, 167,  64,  1,  1100,  1, 59,   0}, // potion of giant strength
            {185, 167,  64,  1,   400,  1,  3,   0}, // potion of healing
            {173, 167,  64,  1,   450,  1, 48,   0}, // potion of speed (unused)
            {206, 167,  69,  1, 11000, 30, 15,   0}, // wand of magic missile
            {226, 167, 100, 10, 15000,  0, 38, 131}, // gauntlets of ogre power (unused)
            {157, 167,  21, 20,  3000,  1, 51,   0}, // javelin of lightning
        };

        internal static Item create_item(ItemType item_type) /* sub_5A007 */
        {
            int preconfig = -1;

            Item item = new Item(0, 0, 0, false, 6, false, 0, 0, 0, 0, 0, item_type, false);

            var type = item.type;

            if ((type >= ItemType.BattleAxe && type <= ItemType.Shield) ||
                type == ItemType.Arrow ||
                type == ItemType.Bracers ||
                type == ItemType.RingOfProt)
            {
                item.plus = randomBonus();

                if (item.type == ItemType.Javelin)
                {
                    int roll = ovr024.roll_dice(5, 1);
                    if (roll == 5)
                    {
                        preconfig = 6;
                    }
                    else
                    {
                        item.namenum3 = (int)item.type;
                        item.namenum2 = item.plus + 161;
                    }
                }
                else if (item.type == ItemType.Quarrel)
                {
                    item.namenum3 = (int)item.type;
                    item.namenum2 = item.plus + 161;
                }
                else if (item.type == ItemType.LeatherArmor ||
                         item.type == ItemType.PaddedArmor)
                {
                    item.namenum3 = (int)item.type;
                    item.namenum2 = 49; // Armor
                    item.namenum1 = item.plus + 161;
                    item.hidden_names_flag = 4;
                }
                else if (item.type == ItemType.StuddedLeather)
                {
                    item.namenum3 = (int)item.type;
                    item.namenum2 = 50; // Leather
                    item.namenum1 = item.plus + 161;
                    item.hidden_names_flag = 4;
                }
                else if (item.type >= ItemType.RingMail &&
                         item.type <= ItemType.PlateMail)
                {
                    item.namenum3 = (int)item.type;
                    item.namenum2 = 48; // Mail
                    item.namenum1 = item.plus + 161;
                    item.hidden_names_flag = 4;
                }
                else if (item.type == ItemType.Arrow)
                {
                    item.namenum3 = 61; // Arrow
                    item.namenum2 = item.plus + 161;
                }
                else if (item.type == ItemType.Bracers)
                {
                    item.namenum3 = 79; // Bracers
                    item.namenum2 = 167; // of
                    item.plus = (item.plus << 1) + 2;

                    if (item.plus == 4)
                    {
                        item.namenum1 = 221; // AC 6
                    }
                    else if (item.plus == 6)
                    {
                        item.namenum1 = 222; // AC 4
                    }
                }
                else if (item.type == ItemType.RingOfProt)
                {
                    item.namenum3 = 66; // Ring
                    item.namenum2 = 224; // of Prot.
                    item.namenum1 = item.plus + 161;
                }
                else
                {
                    item.namenum3 = (int)item.type;
                    item.namenum2 = item.plus + 161;
                }

                item.plus_save = 0;
                item.count = 0;

                switch (item.type)
                {
                    case ItemType.BattleAxe:
                    case ItemType.MilitaryFork:
                    case ItemType.Glaive:
                    case ItemType.BroadSword:
                        item.weight = 75;
                        break;

                    case ItemType.HandAxe:
                    case ItemType.Hammer:
                    case ItemType.Ranseur:
                    case ItemType.Spear:
                    case ItemType.Spetum:
                    case ItemType.QuarterStaff:
                    case ItemType.Trident:
                    case ItemType.CompositeShortBow:
                    case ItemType.ShortBow:
                    case ItemType.LightCrossbow:
                    case ItemType.Shield:
                        item.weight = 50;
                        break;

                    case ItemType.Bardiche:
                    case ItemType.MorningStar:
                    case ItemType.Voulge:
                        item.weight = 125;
                        break;

                    case ItemType.BecDeCorbin:
                    case ItemType.GlaiveGuisarme:
                    case ItemType.Mace:
                    case ItemType.BastardSword:
                    case ItemType.LongBow:
                    case ItemType.HeavyCrossbow:
                    case ItemType.PaddedArmor:
                        item.weight = 100;
                        break;

                    case ItemType.BillGuisarme:
                    case ItemType.Flail:
                    case ItemType.GuisarmeVoulge:
                    case ItemType.LucernHammer:
                    case ItemType.LeatherArmor:
                        item.weight = 150;
                        break;

                    case ItemType.BoStick:
                        item.weight = 15;
                        break;

                    case ItemType.Club:
                        item.weight = 30;
                        break;

                    case ItemType.Dagger:
                    case ItemType.Bracers:
                        item.weight = 10;
                        break;

                    case ItemType.Dart:
                        item.weight = 25;
                        item.count = 5;
                        break;

                    case ItemType.Fauchard:
                    case ItemType.MilitaryPick:
                    case ItemType.LongSword:
                        item.weight = 60;
                        break;

                    case ItemType.FauchardFork:
                    case ItemType.Guisarme:
                    case ItemType.Partisan:
                    case ItemType.AwlPike:
                    case ItemType.CompositeLongBow:
                    // case ItemType.Sling: - this exists twice. other seems correct (weight 80 for a sling seems off)
                        item.weight = 80;
                        break;

                    case ItemType.Halberd:
                        item.weight = 175;
                        break;

                    case ItemType.Javelin:
                        item.weight = 20;
                        break;

                    case ItemType.JoStick:
                    case ItemType.Scimitar:
                        item.weight = 40;
                        break;

                    case ItemType.ShortSword:
                        item.weight = 35;
                        break;

                    case ItemType.TwoHandedSword:
                    case ItemType.RingMail:
                        item.weight = 250;
                        break;

                    case ItemType.StuddedLeather:
                        item.weight = 200;
                        break;

                    case ItemType.ScaleMail:
                    case ItemType.SplintMail:
                        item.weight = 400;
                        break;

                    case ItemType.ChainMail:
                        item.weight = 300;
                        break;

                    case ItemType.BandedMail:
                        item.weight = 350;
                        break;

                    case ItemType.PlateMail:
                        item.weight = 450;
                        break;

                    case ItemType.Sling:
                    case ItemType.RingOfProt:
                        item.weight = 1;
                        break;

                    default:
                        item.weight = 40;
                        item.count = 10;
                        break;
                }

                if (item.type == ItemType.Shield)
                {
                    item._value = (short)(item.plus * 2500);
                }
                else if (item.type == ItemType.Arrow || item.type == ItemType.Quarrel)
                {
                    item._value = (short)(item.plus * 150);
                }
                else if (item.type == ItemType.RingMail || item.type == ItemType.ScaleMail)
                {
                    item._value = (short)(item.plus * 3000);
                }
                else if (item.type == ItemType.ChainMail || item.type == ItemType.SplintMail)
                {
                    item._value = (short)(item.plus * 3500);
                }
                else if (item.type == ItemType.BandedMail)
                {
                    item._value = (short)(item.plus * 4000);
                }
                else if (item.type == ItemType.PlateMail)
                {
                    item._value = (short)(item.plus * 5000);
                }
                else if (item.type == ItemType.Bracers)
                {
                    item._value = (short)(item.plus * 3000);
                }
                else
                {
                    item._value = (short)(item.plus * 2000);
                }
            }
            else if (type == ItemType.MUScroll || type == ItemType.ClrcScroll)
            {
                byte spellsCount = ovr024.roll_dice(3, 1);

                if (item.type == ItemType.MUScroll)
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

                for (int affect = 1; affect <= spellsCount; affect++)
                {
                    int roll = ovr024.roll_dice(5, 1);
                    Spells spell;

                    if (item.type == ItemType.MUScroll)
                    {
                        switch (roll)
                        {
                            case 1:
                                spell = Spells.burning_hands + ovr024.roll_dice(13, 1) - 1;
                                break;

                            case 2:
                                spell = Spells.detect_invisibility + ovr024.roll_dice(7, 1) - 1;
                                break;

                            case 3:
                                spell = Spells.blink + ovr024.roll_dice(11, 1) - 1;
                                break;

                            case 4:
                                spell = Spells.charm_monsters + ovr024.roll_dice(9, 1) - 1;
                                break;

                            default: // 5
                                spell = Spells.cloud_kill + ovr024.roll_dice(4, 1) - 1;
                                break;
                        }
                    }
                    else
                    {
                        switch (roll)
                        {
                            case 1:
                                spell = Spells.bless + ovr024.roll_dice(8, 1) - 1;
                                break;

                            case 2:
                                spell = Spells.find_traps + ovr024.roll_dice(7, 1) - 1;
                                break;

                            case 3:
                                spell = Spells.cure_blindness + ovr024.roll_dice(8, 1) - 1;
                                break;

                            case 4:
                                spell = Spells.cause_serious_wounds_CL + ovr024.roll_dice(5, 1) - 1;
                                break;

                            default: // 5
                                spell = Spells.cure_critical_wounds + ovr024.roll_dice(6, 1) - 1;
                                break;
                        }
                    }

                    item.setAffect(affect, (Affects)spell);
                    item._value += (short)(roll * 300);
                }
            }
            else if (type == ItemType.Gauntlets || type == ItemType.CloakOfProt) // Gauntlets and CloakOfProt unused
            {
                preconfig = 5;
            }
            else if (type == ItemType.WandA || type == ItemType.WandB) // WandA unused
            {
                preconfig = 4;
            }
            else if (type == ItemType.PotionOfGiantStr || type == ItemType.Cloak) // Cloak unused
            {
                preconfig = 1;
            }
            else if (type == ItemType.Potion)
            {
                int roll = ovr024.roll_dice(8, 1);

                if (roll >= 1 && roll <= 5)
                {
                    preconfig = 2;
                }
                else if (roll >= 6 && roll <= 8)
                {
                    preconfig = 0;
                }
            }

            if (preconfig > -1)
            {
                item.namenum1 = preconfiguredItems[preconfig, 0];
                item.namenum2 = preconfiguredItems[preconfig, 1];
                item.namenum3 = preconfiguredItems[preconfig, 2];

                item.plus = 1;
                item.plus_save = 1;

                item.weight = preconfiguredItems[preconfig, 3];
                item.count = 0;

                item._value = preconfiguredItems[preconfig, 4];

                for (int affect = 1; affect <= 3; affect++)
                {
                    item.setAffect(affect, (Affects)(byte)preconfiguredItems[preconfig, 4 + affect]);
                }
            }

            ItemLibrary.Add(item);
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

            if (gbl.SelectedPlayer.Money.Gems == 0 && gbl.SelectedPlayer.Money.Jewels == 0)
            {
                ovr025.string_print01("No Gems or Jewelry");
                return false;
            }

            bool stop_loop;

            do
            {
                if (gbl.SelectedPlayer.Money.Gems == 0 && gbl.SelectedPlayer.Money.Jewels == 0)
                {
                    stop_loop = true;
                }
                else
                {
                    stop_loop = false;

                    string gem_text = gbl.SelectedPlayer.Money.Gems.ToString();
                    string jewel_text = gbl.SelectedPlayer.Money.Jewels.ToString();

                    if (gbl.SelectedPlayer.Money.Gems == 0)
                    {
                        gem_text = string.Empty;
                    }
                    else if (gbl.SelectedPlayer.Money.Gems == 1)
                    {
                        gem_text += " Gem";
                    }
                    else
                    {
                        gem_text += " Gems";
                    }

                    if (gbl.SelectedPlayer.Money.Jewels == 0)
                    {
                        jewel_text = string.Empty;
                    }
                    else if (gbl.SelectedPlayer.Money.Jewels == 1)
                    {
                        jewel_text += " piece of Jewelry";
                    }
                    else
                    {
                        jewel_text += " pieces of Jewelry";
                    }

                    seg037.draw8x8_clear_area(0x16, 0x26, 1, 1);
                    ovr025.displayPlayerName(false, 1, 1, gbl.SelectedPlayer);

                    seg041.displayString("You have a fine collection of:", 0, 0xf, 7, 1);
                    seg041.displayString(gem_text, 0, 0x0f, 9, 1);
                    seg041.displayString(jewel_text, 0, 0x0f, 0x0a, 1);
                    string prompt = string.Empty;

                    if (gbl.SelectedPlayer.Money.Gems != 0)
                    {
                        prompt = "  Gems";
                    }

                    if (gbl.SelectedPlayer.Money.Jewels != 0)
                    {
                        prompt += " Jewelry";
                    }

                    prompt += " Exit";

                    char input_key = ovr027.displayInput(out special_key, false, 1, gbl.defaultMenuColors, prompt, "Appraise : ");

                    if (input_key == 'G')
                    {
                        if (gbl.SelectedPlayer.Money.Gems > 0)
                        {
                            gbl.SelectedPlayer.Money.AddCoins(Money.Gems, -1);

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

                            if (willOverload(1, gbl.SelectedPlayer) == true ||
                                gbl.SelectedPlayer.items.Count >= Player.MaxItems)
                            {
                                sell_text = "Sell";
                                must_sell = true;
                            }
                            else
                            {
                                sell_text = "Sell Keep";
                                must_sell = false;
                            }

                            input_key = ovr027.displayInput(out special_key, false, 1, gbl.defaultMenuColors, sell_text, "You can : ");

                            if (input_key == 'K' && must_sell == false)
                            {
                                Item gem_item = new Item(value, 0, 1, false, 0, false, 0, 0, 0x65, 0, 0, ItemType.GemsJewelry, true);

                                gbl.SelectedPlayer.items.Add(gem_item);
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
                        if (gbl.SelectedPlayer.Money.Jewels > 0)
                        {
                            gbl.SelectedPlayer.Money.AddCoins(Money.Jewelry, -1);

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
                            if (willOverload(1, gbl.SelectedPlayer) == true ||
                                gbl.SelectedPlayer.items.Count >= Player.MaxItems)
                            {
                                sell_text = "Sell";
                                must_sell = true;
                            }
                            else
                            {
                                sell_text = "Sell Keep";
                                must_sell = false;
                            }

                            input_key = ovr027.displayInput(out special_key, false, 1, gbl.defaultMenuColors, sell_text, "You can : ");

                            if (input_key == 'K' && must_sell == false)
                            {
                                Item jewel_item = new Item(value, 0, 1, false, 0, false, 0, 0, 0xd6, 0, 0, ItemType.GemsJewelry, true);

                                gbl.SelectedPlayer.items.Add(jewel_item);
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

                    ovr025.reclac_player_values(gbl.SelectedPlayer);
                }

            } while (stop_loop == false);

            return true;
        }
    }
}
