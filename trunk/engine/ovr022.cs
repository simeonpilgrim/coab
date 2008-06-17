using Classes;

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


        internal static bool willOverload(out short weight, int item_weight, Player player)
        {
            bool ret_val;

            if ((player.weight + item_weight) > get_max_load(player))
            {
                ret_val = true;

                weight = (short)(get_max_load(player) - player.weight);
            }
            else
            {
                ret_val = false;
                weight = 0;
            }

            return ret_val;
        }


        internal static int getPooledGold(int[] arg_0)
        {
            int total = 0;
            for (int i = 0; i <= 4; i++)
            {
                total += money.per_copper[i] * arg_0[i];
            }

            return total / money.per_copper[money.gold];
        }


        internal static void setPlayerMoney(int arg_0)
        {
            for (int i = 0; i <= 4; i++)
            {
                gbl.player_ptr.Money[i] = 0;
            }

            gbl.player_ptr.platinum = (short)(arg_0 / 5);
            gbl.player_ptr.gold = (short)(arg_0 % 5);
        }


        internal static void setPooledGold(int arg_0)
        {
            for (int i = 0; i < 5; i++)
            {
                gbl.pooled_money[i] = 0;
            }

            gbl.pooled_money[money.platum] = arg_0 / 5;
            gbl.pooled_money[money.gold] = arg_0 % 5;
        }


        internal static void addPlayerGold(short item_weight)
        {
            short capasity;

            if (willOverload(out capasity, item_weight, gbl.player_ptr) == true)
            {
                ovr025.string_print01("Overloaded. Money will be put in Pool.");
                gbl.player_ptr.platinum += capasity;
                add_weight(capasity, gbl.player_ptr);

                gbl.pooled_money[money.platum] += item_weight - capasity;
            }
            else
            {
                gbl.player_ptr.platinum += item_weight;
                add_weight(item_weight, gbl.player_ptr);
            }
        }


        internal static short sub_592AD(byte arg_0, string arg_2, short coinAvailable)
        {
            string var_144;
            short var_44;
            short var_40;
            char var_3C;
            string var_3B;
            string var_34;
            int var_2E;
            int var_2C;
            string var_2B;

            var_2B = arg_2;

            seg041.displaySpaceChar(0x28, 0, 0x18, 0);
            seg041.displayString(var_2B, 0, arg_0, 0x18, 0);

            var_2E = var_2B.Length;
            var_2C = var_2E;

            var_34 = coinAvailable.ToString();

            var_3B = string.Empty;

            do
            {
                var_3C = (char)seg043.GetInputKey();

                if (var_3C >= 0x30 &&
                    var_3C <= 0x39)
                {
                    var_3B += var_3C.ToString();

                    var_40 = (short)(int.Parse(var_3B));

                    if (coinAvailable >= var_40)
                    {
                        var_2C++;
                    }
                    else
                    {
                        var_3B = var_34;

                        var_2C = var_34.Length + var_2E;
                    }

                    seg041.displayString(var_3B, 0, 15, 0x18, (byte)var_2E);
                }
                else if (var_3C == 8 && var_3B.Length > 0)
                {
                    int i = var_3B.Length - 1;
                    var_3B = seg051.Copy(i, 1, var_3B, out var_144);

                    seg041.displaySpaceChar(1, 0, 0x18, var_2C);
                    var_2C--;
                }
            } while (var_3C != 0x0D && var_3C != 0x1B);

            seg041.displaySpaceChar(0x28, 0, 0x18, 0);

            if (var_3C == 0x1B)
            {
                var_44 = 0;
            }
            else
            {
                var_44 = (short)(int.Parse(var_3B));
            }

            return var_44;
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
            Player player = gbl.player_next_ptr;
            gbl.something01 = true;

            while (player != null)
            {
                if (player.field_F7 == 0 ||
                    player.field_F7 == 0x0B3)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        gbl.pooled_money[i] += player.Money[i];

                        remove_weight(player.Money[i], player);

                        player.Money[i] = 0;
                    }
                }

                player = player.next_player;
            }
        }


        internal static int GetPartyCount() /* sub_595FF */
        {
            int count;
            Player player;

            player = gbl.player_next_ptr;
            count = 0;

            while (player != null)
            {
                if (player.field_F7 == 0 ||
                    player.field_F7 == 0xB3)
                {
                    count++;
                }

                player = player.next_player;
            }

            return count;
        }


        internal static void share_pooled()
        {
            short[] money_remander;
            short[] money_each;
            short var_C;
            Player var_4;


            var_4 = gbl.player_next_ptr;
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

            while (var_4 != null)
            {
                if (var_4.field_F7 < 0x80)
                {
                    for (int var_29 = 6; var_29 >= 0; var_29--)
                    {
                        if (willOverload(out var_C, money_each[var_29], var_4) == false)
                        {
                            var_4.Money[var_29] += money_each[var_29];

                            add_weight(money_each[var_29], var_4);

                            if (money_remander[var_29] > 0 &&
                                willOverload(out var_C, 1, var_4) == false)
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

                var_4 = var_4.next_player;
            }

            for (int var_29 = 6; var_29 >= 0; var_29--)
            {
                if (money_remander[var_29] > 0)
                {
                    var_4 = gbl.player_next_ptr;
                    while (var_4 != null)
                    {
                        var_C = (short)(get_max_load(var_4) - var_4.weight);

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

                        var_4 = var_4.next_player;
                    }
                }
            }

            gbl.something01 = false;

            for (int var_29 = 0; var_29 <= 6; var_29++)
            {
                gbl.pooled_money[var_29] = money_remander[var_29];

                if (gbl.pooled_money[var_29] != 0)
                {
                    gbl.something01 = true;
                }
            }
        }


        internal static void drop_coins(int money_slot, short num_coins, Player player) /* sub_59A19 */
        {
            player.Money[money_slot] -= num_coins;
            remove_weight(num_coins, player);

            if (gbl.game_state == 6 ||
                gbl.game_state == 1)
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


        internal static int sub_59BAB(out string arg_0, string input)
        {
            int offset = 0;
            int index = 7; // this is outofbounds.
            arg_0 = string.Empty;

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
                    arg_0 = "Gems ";
                }
                else
                {
                    arg_0 = "Gold ";
                    index = 3;
                }
            }
            else if (ch == 'P')
            {
                arg_0 = "Platinum ";
                index = 4;
            }
            else if (ch == 'E')
            {
                arg_0 = "Electrum ";
                index = 2;
            }
            else if (ch == 'S')
            {
                arg_0 = "Silver ";
                index = 1;
            }
            else if (ch == 'C')
            {
                arg_0 = "Copper ";
                index = 0;
            }
            else if (ch == 'J')
            {
                arg_0 = "Jewelry ";
                index = 6;
            }

            return index;
        }


        internal static void takeItems()
        {
            byte var_117;
            string var_114;
            short var_14;

            StringList var_C;
            StringList item_ptr;
            sbyte var_1;

            seg037.draw8x8_outer_frame();
            var_14 = 0;

            do
            {
                bool var_118 = true;

                item_ptr = null;
                var_C = null;
                for (var_1 = 6; var_1 >= 0; var_1--)
                {
                    if (gbl.pooled_money[var_1] > 0)
                    {
                        var_C = item_ptr;
                        item_ptr = new StringList();
                        item_ptr.next = var_C;

                        item_ptr.s = money.names[var_1] + " " + gbl.pooled_money[var_1].ToString();
                        item_ptr.field_29 = 0;

                    }
                }

                var_C = item_ptr;
                StringList var_10 = item_ptr;

                char input_key = ovr027.sl_select_item(out var_C, ref var_14, ref var_118, true, item_ptr,
                    8, 15, 2, 2, 15, 10, 13, "Select", "Select type of coin ");

                if (var_C == null || input_key == 0)
                {
                    var_117 = 1;
                }
                else
                {
                    var_117 = 0;

                    int money_slot = sub_59BAB(out var_114, var_C.s);

                    var_114 = string.Format("How much {0} will you take? ", var_114);

                    short num_coins = sub_592AD(10, var_114, (short)gbl.pooled_money[money_slot]);

                    sub_59AA0(money_slot, num_coins, gbl.player_ptr);
                    ovr027.free_stringList(ref var_10);
                    gbl.something01 = false;
                    var_117 = 1;
                    for (var_1 = 0; var_1 < 7; var_1++)
                    {
                        if (gbl.pooled_money[var_1] > 0)
                        {
                            gbl.something01 = true;
                            var_117 = 0;
                        }
                    }
                }

            } while (var_117 == 0);
        }


        internal static void treasureOnGround(out bool items, out bool money)
        {
            money = false;
            items = false;

            for (int i = 0; i < 7; i++)
            {
                if (gbl.pooled_money[i] != 0)
                {
                    money = true;
                }
            }

            if (gbl.item_pointer != null)
            {
                items = true;
            }
        }


        internal static sbyte sub_59FCF()
        {
            sbyte var_1 = 0;

            int roll = ovr024.roll_dice(20, 1);

            if (roll >= 1 && roll <= 14)
            {
                var_1 = 1;
            }
            else if (roll >= 15 && roll <= 20)
            {
                var_1 = 2;
            }

            return var_1;
        }

        static short[] /*seg600:082E*/	unk_16B3E = { 
            0x3932, 
            0x00B9, 0x00BB, 0x0040, 0x0001, 0x0320, 0x0003, 0x0063, 0x0000,
            0x00EF, 0x00A7, 0x0040, 0x0001, 0x044C, 0x0001, 0x003b, 0x0000,
            0x00b9, 0x00a7, 0x0040, 0x0001, 0x0190, 0x0001, 0x0003, 0x0000,
            0x00AD, 0x00a7, 0x0040, 0x0001, 0x01C2, 0x0001, 0x0030, 0x0000,
            0x00CE, 0x00A7, 0x0045, 0x0001, 0x2AF8, 0x001E, 0x000F, 0x0000,
            0x00E2, 0x00A7, 0x0064, 0x000A, 0x3A98, 0x0000, 0x0026, 0x0083,
            0x009d, 0x00a7, 0x0015, 0x0014, 0x0bb8, 0x0001, 0x0033, 0x0000 };

        internal static void create_item(out Item arg_0, int item_type) /* sub_5A007 */
        {
            byte var_A;
            Item var_9;
            byte var_5 = 0; /* Simeon */
            byte var_4;
            byte var_3;
            byte var_2;
            byte var_1;

            arg_0 = new Item();
            var_4 = 0;
            var_9 = arg_0;

            var_9.affect_1 = 0;
            var_9.affect_2 = 0;
            var_9.affect_3 = 0;

            var_9.next = null;
            var_9.readied = false;
            var_9.hidden_names_flag = 6;
            var_9.cursed = false;

            var_9.type = (byte)item_type;

            var_9.field_2F = 0;
            var_9.field_30 = 0;
            var_9.field_31 = 0;

            byte al = var_9.type;

            if ((al >= 1 && al <= 0x3B) ||
                al == 0x49 || al == 0x4D || al == 0x5D)
            {
                var_9.plus = sub_59FCF();

                if (var_9.type == 0x15)
                {

                    var_1 = ovr024.roll_dice(5, 1);
                    if (var_1 == 5)
                    {
                        var_4 = 0x31;
                    }

                    var_9.field_31 = 0x15;

                    var_9.field_30 = (sbyte)(var_9.plus + 0xA1);
                }
                else if (var_9.type == 0x1C)
                {
                    var_9.field_31 = 0x1C;
                    var_9.field_30 = (sbyte)(var_9.plus + 0xA1);
                }
                else if (var_9.type == 0x32 ||
                    var_9.type == 0x33)
                {
                    var_9.field_31 = var_9.type;
                    var_9.field_30 = 0x31;
                    var_9.field_2F = (sbyte)(var_9.plus + 0xA1);
                    var_9.hidden_names_flag = 4;
                }
                else if (var_9.type == 0x34)
                {
                    var_9.field_31 = var_9.type;
                    var_9.field_30 = 0x32;
                    var_9.field_2F = (sbyte)(var_9.plus + 0xA1);
                    var_9.hidden_names_flag = 4;
                }
                else if (var_9.type >= 0x35 &&
                    var_9.type <= 0x3a)
                {
                    var_9.field_31 = var_9.type;
                    var_9.field_30 = 0x30;
                    var_9.field_2F = (sbyte)(var_9.plus + 0xA1);
                    var_9.hidden_names_flag = 4;
                }
                else if (var_9.type == 0x49)
                {
                    var_9.field_31 = 0x3D;
                    var_9.field_30 = (sbyte)(var_9.plus + 0xA1);
                }
                else if (var_9.type == 0x4d)
                {
                    var_9.field_31 = 0x4F;
                    var_9.field_30 = -89;
                    var_9.plus = (sbyte)((var_9.plus << 1) + 2);

                    if (var_9.plus == 4)
                    {
                        var_9.field_2F = -35;
                    }
                    else if (var_9.plus == 6)
                    {
                        var_9.field_2F = -34;
                    }
                }
                else if (var_9.type == 0x5d)
                {
                    var_9.field_31 = 0x42;
                    var_9.field_30 = -32;
                    var_9.field_2F = (sbyte)(var_9.plus + 0xA1);
                }
                else
                {
                    var_9.field_31 = var_9.type;
                    var_9.field_30 = (sbyte)(var_9.plus + 0xA1);
                }

                var_9.plus_save = 0;
                var_9.count = 0;

                switch (var_9.type)
                {
                    case 1:
                    case 0x0d:
                    case 0x0e:
                    case 0x23:
                        var_9.weight = 0x4B;
                        break;

                    case 2:
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

                        var_9.weight = 0x32;
                        break;

                    case 3:
                    case 0x18:
                    case 0x28:
                        var_9.weight = 0x7D;
                        break;

                    case 4:
                    case 0x0f:
                    case 0x17:
                    case 0x22:
                    case 0x2b:
                    case 0x2d:
                    case 0x33:
                        var_9.weight = 0x64;
                        break;

                    case 5:
                    case 0x0c:
                    case 0x11:
                    case 0x13:
                    case 0x32:
                        var_9.weight = 0x96;
                        break;

                    case 6:
                        var_9.weight = 0x0F;
                        break;

                    case 7:
                        var_9.weight = 0x1E;
                        break;

                    case 8:
                    case 0x4d:
                        var_9.weight = 0x0A;
                        break;

                    case 9:
                        var_9.weight = 0x19;
                        var_9.count = 5;
                        break;

                    case 0x0a:
                    case 0x1a:
                    case 0x24:
                        var_9.weight = 0x3C;
                        break;

                    case 0x0b:
                    case 0x10:
                    case 0x19:
                    case 0x1b:
                    case 0x29:
                    case 0x2f:
                        var_9.weight = 0x50;
                        break;
                    case 0x12:
                        var_9.weight = 0xAF;
                        break;

                    case 0x15:
                        var_9.weight = 0x14;
                        break;

                    case 0x16:
                    case 0x1e:
                        var_9.weight = 0x28;
                        break;
                    case 0x25:
                        var_9.weight = 0x23;
                        break;
                    case 0x26:
                    case 0x35:
                        var_9.weight = 0x0FA;
                        break;
                    case 0x34:
                        var_9.weight = 0x0C8;
                        break;
                    case 0x36:
                    case 0x38:
                        var_9.weight = 0x190;
                        break;
                    case 0x37:
                        var_9.weight = 0x12C;
                        break;
                    case 0x39:
                        var_9.weight = 0x15E;
                        break;
                    case 0x3a:
                        var_9.weight = 0x1C2;
                        break;
                    //case 0x2f: //wonder if this should have been 0x3f
                    case 0x5d:
                        var_9.weight = 1;
                        break;

                    default:
                        var_9.weight = 0x28;
                        var_9.count = 0x0A;
                        break;
                }

                if (var_9.type == 0x3b)
                {
                    var_9._value = (short)(var_9.plus * 2500);
                }
                else if (var_9.type == 0x49 || var_9.type == 0x1c)
                {
                    var_9._value = (short)(var_9.plus * 150);
                }
                else if (var_9.type == 0x35 || var_9.type == 0x36)
                {
                    var_9._value = (short)(var_9.plus * 3000);
                }
                else if (var_9.type == 0x37 || var_9.type == 0x38)
                {
                    var_9._value = (short)(var_9.plus * 3500);
                }
                else if (var_9.type == 0x39)
                {
                    var_9._value = (short)(var_9.plus * 4000);

                }
                else if (var_9.type == 0x3a)
                {
                    var_9._value = (short)(var_9.plus * 5000);
                
                }
                else if (var_9.type == 0x4d)
                {
                    var_9._value = (short)(var_9.plus * 3000);
                }
                else
                {
                    var_9._value = (short)(var_9.plus * 2000);
                }
            }
            else if (al == 0x3d || al == 0x3e)
            {

                var_2 = ovr024.roll_dice(3, 1);

                if (var_9.type == 0x3d)
                {
                    var_9.field_31 = 0xD1;
                }
                else
                {
                    var_9.field_31 = 0xD0;
                }

                var_9.field_30 = (sbyte)(var_2 + 0xd1);
                var_9.field_2F = 0;
                var_9.plus = 1;
                var_9.weight = 0x19;
                var_9.count = 0;
                var_9._value = 0;
                var_A = var_2;

                for (var_3 = 1; var_3 <= var_A; var_3++)
                {
                    var_1 = ovr024.roll_dice(5, 1);

                    if (var_9.type == 0x3D)
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

                    var_9.setAffect(var_3, (Affects)var_5);
                    var_9._value += (short)(var_1 * 300);
                }
            }
            else if (al == 0x3f || al == 0x43)
            {
                var_4 = 0x29;
            }
            else if (al == 0x4E || al == 0x4F)
            {
                var_4 = 0x21;
            }
            else if (al == 0x54 || al == 0x5C)
            {
                var_4 = 9;
            }
            else if (al == 0x47)
            {
                var_1 = ovr024.roll_dice(8, 1);

                if (var_1 >= 1 && var_1 <= 5)
                {
                    var_4 = 17;
                }
                else if (var_1 >= 6 && var_1 <= 8)
                {
                    var_4 = 1;
                }
            }

            if (var_4 != 0)
            {
                var_9.field_2F = (sbyte)unk_16B3E[var_4 + 0];
                var_9.field_30 = (sbyte)unk_16B3E[var_4 + 1];
                var_9.field_31 = (byte)unk_16B3E[var_4 + 2];

                var_9.plus = 1;
                var_9.plus_save = 1;

                var_9.weight = unk_16B3E[var_4 + 3];
                var_9.count = 0;

                var_9._value = unk_16B3E[var_4 + 4];

                for (var_3 = 1; var_3 <= 3; var_3++)
                {
                    var_9.setAffect(var_3, (Affects)(byte)unk_16B3E[(var_4 + 4 + var_3)]);
                }
            }
        }


        internal static void appraiseGemsJewels(out bool arg_0)
        {
            bool special_key;
            bool stop_loop;
            short value;
            string sell_text;

            arg_0 = true;

            if (gbl.player_ptr.gems == 0 && gbl.player_ptr.jewels == 0)
            {
                ovr025.string_print01("No Gems or Jewelry");
                arg_0 = false;
            }
            else
            {
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

                                short dummy_short;
                                bool must_sell;

                                if (willOverload(out dummy_short, 1, gbl.player_ptr) == true ||
                                    gbl.player_ptr.field_14C >= 0x10)
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
                                    gem_item.next = null;
                                    gem_item.weight = 1;
                                    gem_item.hidden_names_flag = 0;
                                    gem_item.readied = false;
                                    gem_item.field_31 = 0x65;
                                    gem_item.field_30 = 0;
                                    gem_item.field_2F = 0;
                                    gem_item.type = 0x46;
                                    gem_item._value = value;

                                    Item item = gbl.player_ptr.itemsPtr;

                                    if (item == null)
                                    {
                                        gbl.player_ptr.itemsPtr = gem_item;
                                    }
                                    else
                                    {
                                        while (item.next != null)
                                        {
                                            item = item.next;
                                        }

                                        item.next = gem_item;
                                    }
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

                                short dummy_short;
                                bool must_sell;
                                if (willOverload(out dummy_short, 1, gbl.player_ptr) == true ||
                                    gbl.player_ptr.field_14C > 16)
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
                                    jewel_item.next = null;
                                    jewel_item.readied = false;
                                    jewel_item.field_31 = 0xD6;
                                    jewel_item.field_30 = 0;
                                    jewel_item.type = 0x46;
                                    jewel_item.field_2F = 0;

                                    jewel_item._value = value;
                                    jewel_item.hidden_names_flag = 0;
                                    jewel_item.weight = 1;

                                    Item var_AE = gbl.player_ptr.itemsPtr;

                                    if (var_AE == null)
                                    {
                                        gbl.player_ptr.itemsPtr = jewel_item;
                                    }
                                    else
                                    {
                                        while (var_AE.next != null)
                                        {
                                            var_AE = var_AE.next;
                                        }

                                        var_AE.next = jewel_item;
                                    }
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
            }
        }
    }
}
