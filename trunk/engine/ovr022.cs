using Classes;

namespace engine
{
    class ovr022
    {
        internal static short get_max_load(Player player)
        {
            short ret_word = 1500;
            ret_word += ovr025.strEncumberance(player);

            return ret_word;
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


        internal static void add_object(byte arg_0, short weight, Player player, Player arg_8)
        {
            if ((player.weight + weight) <= get_max_load(player))
            {
                player.Money[arg_0] -= weight;

                remove_weight(weight, arg_8);

                player.Money[arg_0] += weight;

                add_weight(weight, player);
            }
            else
            {
                ovr025.string_print01("Overloaded");
            }
        }


        internal static void poolMoney()
        {
            Player playerBase;

            playerBase = gbl.player_next_ptr;
            gbl.something01 = true;

            while (playerBase != null)
            {
                if (playerBase.field_F7 == 0 ||
                    playerBase.field_F7 == 0x0B3)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        gbl.pooled_money[i] += playerBase.Money[i];

                        remove_weight(playerBase.Money[i], playerBase);

                        playerBase.Money[i] = 0;
                    }
                }

                playerBase = playerBase.next_player;
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


        internal static void sub_59A19(byte arg_0, short arg_2, Player arg_4)
        {
            arg_4.Money[arg_0] -= arg_2;
            remove_weight(arg_2, arg_4);

            if (gbl.game_state == 6 ||
                gbl.game_state == 1)
            {
                gbl.pooled_money[arg_0] += arg_2;
            }
        }


        internal static void sub_59AA0(byte arg_0, short arg_2, Player arg_4)
        {
            short var_2;

            if (willOverload(out var_2, arg_2, arg_4) == true)
            {
                ovr025.string_print01("Overloaded");
            }
            else
            {
                if (arg_2 > (short)gbl.pooled_money[arg_0])
                {
                    arg_2 = (short)gbl.pooled_money[arg_0];
                }


                gbl.pooled_money[arg_0] -= arg_2;

                arg_4.Money[arg_0] += arg_2;

                add_weight(arg_2, arg_4);
            }
        }


        internal static byte sub_59BAB(out string arg_0, string arg_4)
        {
            int var_2 = 0;
            byte var_1 = 7; // this is outofbounds.
            arg_0 = string.Empty;

            while (arg_4[var_2] == ' ')
            {
                var_2++;
            }

            char ch = arg_4[var_2];
            if (ch == 'G')
            {
                ch = arg_4[var_2 + 1];
                if (char.ToUpper(ch) == 'E')
                {
                    var_1 = 5;
                    arg_0 = "Gems ";
                }
                else
                {
                    arg_0 = "Gold ";
                    var_1 = 3;
                }
            }
            else if (ch == 'P')
            {
                arg_0 = "Platinum ";
                var_1 = 4;
            }
            else if (ch == 'E')
            {
                arg_0 = "Electrum ";
                var_1 = 2;
            }
            else if (ch == 'S')
            {
                arg_0 = "Silver ";
                var_1 = 1;
            }
            else if (ch == 'C')
            {
                arg_0 = "Copper ";
                var_1 = 0;
            }
            else if (ch == 'J')
            {
                arg_0 = "Jewelry ";
                var_1 = 6;
            }

            return var_1;
        }


        internal static void takeItems()
        {
            char var_119;
            bool var_118;
            byte var_117;
            short var_116;
            string var_114;
            short var_14;
            StringList var_10;
            StringList var_C;
            StringList item_ptr;
            byte var_2;
            sbyte var_1;

            seg037.draw8x8_01();
            var_14 = 0;

            do
            {
                var_118 = true;

                item_ptr = null;
                var_C = null;
                for (var_1 = 6; var_1 >= 0; var_1--)
                {
                    if (gbl.pooled_money[var_1] > 0)
                    {
                        var_C = item_ptr;
                        item_ptr = new StringList();
                        item_ptr.next = var_C;

                        seg051.Str(0xff, out var_114, 0, gbl.pooled_money[var_1]);

                        item_ptr.s = money.names[var_1] + " " + var_114;
                        item_ptr.field_29 = 0;

                    }
                }

                var_C = item_ptr;
                var_10 = item_ptr;

                var_119 = ovr027.sl_select_item(out var_C, ref var_14, ref var_118, true, item_ptr,
                    8, 15, 2, 2, 15, 10, 13, "Select", "Select type of coin ");

                if (var_C == null || var_119 == 0)
                {
                    var_117 = 1;
                }
                else
                {
                    var_117 = 0;

                    var_2 = sub_59BAB(out var_114, var_C.s);

                    var_114 = string.Format("How much {0} will you take? ", var_114);

                    var_116 = sub_592AD(10, var_114, (short)gbl.pooled_money[var_2]);

                    sub_59AA0(var_2, var_116, gbl.player_ptr);
                    ovr027.free_stringList(ref var_10);
                    gbl.something01 = false;
                    var_117 = 1;
                    for (var_1 = 0; var_1 <= 6; var_1++)
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

            for (int i = 0; i <= 6; i++)
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
            byte var_2;
            sbyte var_1 = 0;

            var_2 = ovr024.roll_dice(20, 1);

            if (var_2 >= 1 && var_2 <= 14)
            {
                var_1 = 1;
            }
            else if (var_2 >= 15 && var_2 <= 20)
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

        internal static void sub_5A007(out Item arg_0, byte arg_4)
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
            var_9.field_35 = 6;
            var_9.field_36 = 0;

            var_9.type = arg_4;

            var_9.field_2F = 0;
            var_9.field_30 = 0;
            var_9.field_31 = 0;

            byte al = var_9.type;

            if ((al >= 1 && al <= 0x3B) ||
                al == 0x49 || al == 0x4D || al == 0x5D)
            {
                var_9.exp_value = sub_59FCF();

                if (var_9.type == 0x15)
                {

                    var_1 = ovr024.roll_dice(5, 1);
                    if (var_1 == 5)
                    {
                        var_4 = 0x31;
                    }

                    var_9.field_31 = 0x15;

                    var_9.field_30 = (sbyte)(var_9.exp_value + 0xA1);
                }
                else if (var_9.type == 0x1C)
                {
                    var_9.field_31 = 0x1C;
                    var_9.field_30 = (sbyte)(var_9.exp_value + 0xA1);
                }
                else if (var_9.type == 0x32 ||
                    var_9.type == 0x33)
                {
                    var_9.field_31 = var_9.type;
                    var_9.field_30 = 0x31;
                    var_9.field_2F = (sbyte)(var_9.exp_value + 0xA1);
                    var_9.field_35 = 4;
                }
                else if (var_9.type == 0x34)
                {
                    var_9.field_31 = var_9.type;
                    var_9.field_30 = 0x32;
                    var_9.field_2F = (sbyte)(var_9.exp_value + 0xA1);
                    var_9.field_35 = 4;
                }
                else if (var_9.type >= 0x35 &&
                    var_9.type <= 0x3a)
                {
                    var_9.field_31 = var_9.type;
                    var_9.field_30 = 0x30;
                    var_9.field_2F = (sbyte)(var_9.exp_value + 0xA1);
                    var_9.field_35 = 4;
                }
                else if (var_9.type == 0x49)
                {
                    var_9.field_31 = 0x3D;
                    var_9.field_30 = (sbyte)(var_9.exp_value + 0xA1);
                }
                else if (var_9.type == 0x4d)
                {
                    var_9.field_31 = 0x4F;
                    var_9.field_30 = -89;
                    var_9.exp_value = (sbyte)((var_9.exp_value << 1) + 2);

                    if (var_9.exp_value == 4)
                    {
                        var_9.field_2F = -35;
                    }
                    else if (var_9.exp_value == 6)
                    {
                        var_9.field_2F = -34;
                    }
                }
                else if (var_9.type == 0x5d)
                {
                    var_9.field_31 = 0x42;
                    var_9.field_30 = -32;
                    var_9.field_2F = (sbyte)(var_9.exp_value + 0xA1);
                }
                else
                {
                    var_9.field_31 = var_9.type;
                    var_9.field_30 = (sbyte)(var_9.exp_value + 0xA1);
                }

                var_9.field_33 = 0;
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
                    var_9._value = (short)(var_9.exp_value * 2500);
                }
                else if (var_9.type == 0x49 || var_9.type == 0x1c)
                {
                    var_9._value = (short)(var_9.exp_value * 150);
                }
                else if (var_9.type == 0x35 || var_9.type == 0x36)
                {
                    var_9._value = (short)(var_9.exp_value * 3000);
                }
                else if (var_9.type == 0x37 || var_9.type == 0x38)
                {
                    var_9._value = (short)(var_9.exp_value * 3500);
                }
                else if (var_9.type == 0x39)
                {
                    var_9._value = (short)(var_9.exp_value * 4000);

                }
                else if (var_9.type == 0x3a)
                {
                    var_9._value = (short)(var_9.exp_value * 5000);
                
                }
                else if (var_9.type == 0x4d)
                {
                    var_9._value = (short)(var_9.exp_value * 3000);
                }
                else
                {
                    var_9._value = (short)(var_9.exp_value * 2000);
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
                var_9.exp_value = 1;
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

                var_9.exp_value = 1;
                var_9.field_33 = 1;

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
            byte var_B5;
            bool var_B4;
            bool var_B3;
            Item var_B2;
            Item var_AE;
            short var_AA;
            short var_A8;
            byte var_A6;
            string var_A5;
            string var_7C;
            char var_53;
            string var_52;
            string var_29;

            arg_0 = true;

            if (gbl.player_ptr.field_105 == 0 && gbl.player_ptr.field_107 == 0)
            {
                ovr025.string_print01("No Gems or Jewelry");
                arg_0 = false;
            }
            else
            {
                do
                {
                    if (gbl.player_ptr.field_105 == 0 && gbl.player_ptr.field_107 == 0)
                    {
                        var_B3 = true;
                    }
                    else
                    {
                        var_B3 = false;

                        var_29 = gbl.player_ptr.field_105.ToString();
                        var_52 = gbl.player_ptr.field_107.ToString();

                        if (gbl.player_ptr.field_105 == 0)
                        {
                            var_29 = string.Empty;
                        }
                        else if (gbl.player_ptr.field_105 == 1)
                        {
                            var_29 += " Gem";
                        }
                        else
                        {
                            var_29 += " Gems";
                        }

                        if (gbl.player_ptr.field_107 == 0)
                        {
                            var_52 = string.Empty;
                        }
                        else if (gbl.player_ptr.field_107 == 1)
                        {
                            var_52 += " piece of Jewelry";
                        }
                        else
                        {
                            var_52 += " pieces of Jewelry";
                        }

                        seg037.draw8x8_clear_area(0x16, 0x26, 1, 1);
                        ovr025.displayPlayerName(false, 1, 1, gbl.player_ptr);

                        seg041.displayString("You have a fine collection of:", 0, 0xf, 7, 1);
                        seg041.displayString(var_29, 0, 0x0f, 9, 1);
                        seg041.displayString(var_52, 0, 0x0f, 0x0a, 1);
                        var_7C = string.Empty;

                        if (gbl.player_ptr.field_105 != 0)
                        {
                            var_7C = "  Gems";
                        }

                        if (gbl.player_ptr.field_107 != 0)
                        {
                            var_7C += " Jewelry";
                        }

                        var_7C += " Exit";

                        var_53 = ovr027.displayInput(out var_B4, false, 1, 15, 10, 13, var_7C, "Appraise : ");

                        if (var_53 == 'G')
                        {
                            if (gbl.player_ptr.field_105 != 0)
                            {
                                gbl.player_ptr.field_105 -= 1;

                                var_A6 = ovr024.roll_dice(100, 1);

                                if (var_A6 >= 1 && var_A6 <= 25)
                                {
                                    var_A8 = 10;
                                }
                                else if (var_A6 >= 26 && var_A6 <= 50)
                                {
                                    var_A8 = 50;
                                }
                                else if (var_A6 >= 51 && var_A6 <= 70)
                                {
                                    var_A8 = 100;
                                }
                                else if (var_A6 >= 71 && var_A6 <= 90)
                                {
                                    var_A8 = 500;
                                }
                                else if (var_A6 >= 91 && var_A6 <= 99)
                                {
                                    var_A8 = 1000;
                                }
                                else if (var_A6 == 100)
                                {
                                    var_A8 = 5000;
                                }
                                else
                                {
                                    var_A8 = 0;
                                }

                                var_29 = "The Gem is Valued at " + var_A8.ToString() + " gp.";

                                seg041.displayString(var_29, 0, 15, 12, 1);

                                if (willOverload(out var_AA, 1, gbl.player_ptr) == true ||
                                    gbl.player_ptr.field_14C >= 0x10)
                                {
                                    var_A5 = "Sell";
                                    var_B5 = 1;
                                }
                                else
                                {
                                    var_A5 = "Sell Keep";
                                    var_B5 = 0;
                                }

                                var_53 = ovr027.displayInput(out var_B4, false, 1, 15, 10, 13, var_A5, "You can : ");

                                if (var_53 == 'K' && var_B5 == 0)
                                {
                                    var_B2 = new Item();
                                    var_B2.next = null;
                                    var_B2.weight = 1;
                                    var_B2.field_35 = 0;
                                    var_B2.readied = false;
                                    var_B2.field_31 = 0x65;
                                    var_B2.field_30 = 0;
                                    var_B2.field_2F = 0;
                                    var_B2.type = 0x46;
                                    var_B2._value = var_A8;

                                    var_AE = gbl.player_ptr.itemsPtr;

                                    if (var_AE == null)
                                    {
                                        var_AE = var_B2;
                                    }
                                    else
                                    {
                                        while (var_AE.next != null)
                                        {
                                            var_AE = var_AE.next;
                                        }

                                        var_AE.next = var_B2;
                                    }
                                }
                                else
                                {
                                    var_A8 /= 5;
                                    addPlayerGold(var_A8);
                                }
                            }
                        }
                        else if (var_53 == 'J')
                        {
                            if (gbl.player_ptr.field_107 != 0)
                            {
                                gbl.player_ptr.field_107 -= 1;

                                var_A6 = ovr024.roll_dice(100, 1);

                                if (var_A6 >= 1 && var_A6 <= 10)
                                {
                                    var_A8 = (short)(seg051.Random(900) + 100);
                                }
                                else if (var_A6 >= 11 && var_A6 <= 20)
                                {
                                    var_A8 = (short)(seg051.Random(1000) + 200);
                                }
                                else if (var_A6 >= 21 && var_A6 <= 40)
                                {
                                    var_A8 = (short)(seg051.Random(1500) + 300);
                                }
                                else if (var_A6 >= 41 && var_A6 <= 50)
                                {
                                    var_A8 = (short)(seg051.Random(2500) + 500);
                                }
                                else if (var_A6 >= 51 && var_A6 <= 70)
                                {
                                    var_A8 = (short)(seg051.Random(5000) + 1000);
                                }
                                else if (var_A6 >= 0x47 && var_A6 <= 0x5A)
                                {
                                    var_A8 = (short)(seg051.Random(6000) + 2000);
                                }
                                else if (var_A6 >= 0x5B && var_A6 <= 0x64)
                                {
                                    var_A8 = (short)(seg051.Random(10000) + 2000);
                                }
                                else
                                {
                                    throw new System.NotSupportedException();
                                }

                                var_52 = var_A8.ToString();

                                var_29 = "The Jewel is Valued at " + var_52 + " gp.";
                                seg041.displayString(var_29, 0, 15, 12, 1);

                                if (willOverload(out var_AA, 1, gbl.player_ptr) == true ||
                                    gbl.player_ptr.field_14C > 16)
                                {
                                    var_A5 = "Sell";
                                    var_B5 = 1;
                                }
                                else
                                {
                                    var_A5 = "Sell Keep";
                                    var_B5 = 0;
                                }

                                var_53 = ovr027.displayInput(out var_B4, false, 1, 15, 10, 13, var_A5, "You can : ");

                                if (var_53 == 'K' && var_B5 == 0)
                                {
                                    var_B2 = new Item();
                                    var_B2.next = null;
                                    var_B2.readied = false;
                                    var_B2.field_31 = 0xD6;
                                    var_B2.field_30 = 0;
                                    var_B2.type = 0x46;
                                    var_B2.field_2F = 0;

                                    var_B2._value = var_A8;
                                    var_B2.field_35 = 0;
                                    var_B2.weight = 1;


                                    var_AE = gbl.player_ptr.itemsPtr;

                                    if (var_AE == null)
                                    {
                                        var_AE = var_B2;
                                    }
                                    else
                                    {
                                        while (var_AE.next != null)
                                        {
                                            var_AE = var_AE.next;
                                        }

                                        var_AE.next = var_B2;
                                    }
                                }
                                else
                                {
                                    var_A8 /= 5;
                                    addPlayerGold(var_A8);
                                }
                            }
                        }
                        else if (var_53 == 'E' || var_53 == 0)
                        {
                            var_B3 = true;
                        }

                        ovr025.sub_66C20(gbl.player_ptr);
                    }

                } while (var_B3 == false);
            }
        }
    }
}
