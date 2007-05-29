using Classes;

namespace engine
{
    class ovr022
    {
        internal static short get_max_load(Player player)
        {
            short ret_word;

            ret_word = (short)(ovr025.strength_bonus(player) + 1500);

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


        internal static byte sub_595FF()
        {
            byte var_6;
            Player var_5;

            var_5 = gbl.player_next_ptr;
            var_6 = 0;

            while (var_5 != null)
            {
                if (var_5.field_F7 == 0 ||
                    var_5.field_F7 == 0xB3)
                {
                    var_6++;
                }

                var_5 = var_5.next_player;
            }

            return var_6;
        }


        internal static void share_pooled()
        {
            byte var_29;
            short[] var_28;
            short[] var_1A;
            short var_C;
            byte var_9;
            Player var_4;


            var_4 = gbl.player_next_ptr;
            var_9 = sub_595FF();

            var_1A = new short[7];
            var_28 = new short[7];

            for (var_29 = 0; var_29 <= 6; var_29++)
            {
                if (gbl.pooled_money[var_29] > 0)
                {
                    var_1A[var_29] = (short)(gbl.pooled_money[var_29] / var_9);
                    var_28[var_29] = (short)(gbl.pooled_money[var_29] % var_9);
                }
                else
                {
                    var_1A[var_29] = 0;
                    var_28[var_29] = 0;
                }
            }

            while (var_4 != null)
            {
                if (var_4.field_F7 < 0x80)
                {
                    for (var_29 = 6; var_29 >= 0; var_29--)
                    {
                        if (willOverload(out var_C, var_1A[var_29], var_4) == false)
                        {
                            var_4.Money[var_29] += var_1A[var_29];

                            add_weight(var_1A[var_29], var_4);

                            if (var_28[var_29] > 0 &&
                                willOverload(out var_C, 1, var_4) == false)
                            {
                                var_4.Money[var_29] += 1;

                                add_weight(1, var_4);
                                var_28[var_29] -= 1;
                            }
                        }
                        else
                        {
                            var_4.Money[var_29] += var_C;

                            var_28[var_29] += (short)(var_1A[var_29] - var_C);

                            add_weight(var_C, var_4);
                        }
                    }
                }

                var_4 = var_4.next_player;
            }

            var_29 = 6;
            throw new System.NotSupportedException();//jmp	short loc_598AF
            throw new System.NotSupportedException();//loc_598AC:
            throw new System.NotSupportedException();//dec	[bp+var_29]
            throw new System.NotSupportedException();//loc_598AF:
            throw new System.NotSupportedException();//mov	al, [bp+var_29]
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//mov	di, ax
            throw new System.NotSupportedException();//shl	di, 1
            throw new System.NotSupportedException();//cmp	[bp+di+var_28],	0
            throw new System.NotSupportedException();//ja	loc_598C0
            throw new System.NotSupportedException();//jmp	loc_599BC
            throw new System.NotSupportedException();//loc_598C0:
            var_4 = gbl.player_next_ptr;
            throw new System.NotSupportedException();//loc_598CD:
            throw new System.NotSupportedException();//mov	ax, short ptr [bp+var_4]
            throw new System.NotSupportedException();//or	ax, short ptr [bp+var_4+2]
            throw new System.NotSupportedException();//jnz	loc_598D8
            throw new System.NotSupportedException();//jmp	loc_599BC
            throw new System.NotSupportedException();//loc_598D8:
            var_C = (short)(get_max_load(var_4) - var_4.weight);

            throw new System.NotSupportedException();//cmp	[bp+var_C], 0
            throw new System.NotSupportedException();//ja	loc_598F6
            throw new System.NotSupportedException();//jmp	loc_599A6
            throw new System.NotSupportedException();//loc_598F6:
            throw new System.NotSupportedException();//mov	al, [bp+var_29]
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//mov	di, ax
            throw new System.NotSupportedException();//shl	di, 1
            throw new System.NotSupportedException();//mov	ax, [bp+di+var_28]
            throw new System.NotSupportedException();//cmp	ax, [bp+var_C]
            throw new System.NotSupportedException();//jbe	loc_59955
            throw new System.NotSupportedException();//mov	al, [bp+var_29]
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//shl	ax, 1
            throw new System.NotSupportedException();//les	di, [bp+var_4]
            throw new System.NotSupportedException();//add	di, ax
            throw new System.NotSupportedException();//mov	ax, es:[di+0FBh]
            throw new System.NotSupportedException();//add	ax, [bp+var_C]
            throw new System.NotSupportedException();//mov	dx, ax
            throw new System.NotSupportedException();//mov	al, [bp+var_29]
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//shl	ax, 1
            throw new System.NotSupportedException();//les	di, [bp+var_4]
            throw new System.NotSupportedException();//add	di, ax
            throw new System.NotSupportedException();//mov	es:[di+0FBh], dx
            add_weight(var_C, var_4);
            throw new System.NotSupportedException();//mov	al, [bp+var_29]
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//mov	di, ax
            throw new System.NotSupportedException();//shl	di, 1
            throw new System.NotSupportedException();//mov	ax, [bp+di+var_28]
            throw new System.NotSupportedException();//sub	ax, [bp+var_C]
            throw new System.NotSupportedException();//mov	dx, ax
            throw new System.NotSupportedException();//mov	al, [bp+var_29]
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//mov	di, ax
            throw new System.NotSupportedException();//shl	di, 1
            throw new System.NotSupportedException();//mov	[bp+di+var_28],	dx
            throw new System.NotSupportedException();//jmp	short loc_599A6
            throw new System.NotSupportedException();//loc_59955:
            throw new System.NotSupportedException();//mov	al, [bp+var_29]
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//mov	di, ax
            throw new System.NotSupportedException();//shl	di, 1
            throw new System.NotSupportedException();//mov	dx, [bp+di+var_28]
            throw new System.NotSupportedException();//mov	al, [bp+var_29]
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//shl	ax, 1
            throw new System.NotSupportedException();//les	di, [bp+var_4]
            throw new System.NotSupportedException();//add	di, ax
            throw new System.NotSupportedException();//mov	ax, es:[di+0FBh]
            throw new System.NotSupportedException();//add	ax, dx
            throw new System.NotSupportedException();//mov	dx, ax
            throw new System.NotSupportedException();//mov	al, [bp+var_29]
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//shl	ax, 1
            throw new System.NotSupportedException();//les	di, [bp+var_4]
            throw new System.NotSupportedException();//add	di, ax
            throw new System.NotSupportedException();//mov	es:[di+0FBh], dx
            add_weight(var_28[var_29], var_4);
            throw new System.NotSupportedException();//mov	al, [bp+var_29]
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//mov	di, ax
            throw new System.NotSupportedException();//shl	di, 1
            throw new System.NotSupportedException();//xor	ax, ax
            throw new System.NotSupportedException();//mov	[bp+di+var_28],	ax
            throw new System.NotSupportedException();//loc_599A6:
            var_4 = var_4.next_player;
            throw new System.NotSupportedException();//jmp	loc_598CD
            throw new System.NotSupportedException();//loc_599BC:
            throw new System.NotSupportedException();//cmp	[bp+var_29], 0
            throw new System.NotSupportedException();//jz	loc_599C5
            throw new System.NotSupportedException();//jmp	loc_598AC
            throw new System.NotSupportedException();//loc_599C5:
            gbl.something01 = false;

            for (var_29 = 0; var_29 <= 6; var_29++)
            {
                gbl.pooled_money[var_29] = var_28[var_29];

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

        static byte[] /*seg600:082E*/	unk_16B3E = { 0x32, 0x39, 0x0B9, 0, 0x0BB, 0, 0x40, 0, 1, 0, 0x20, 3, 3, 0, 0x63, 0, 0, 0, 0x0EF, 0, 0x0A7, 0, 0x40 };

        internal static void sub_5A007(Item arg_0, byte arg_4)
        {
            byte var_A;
            Item var_9;
            byte var_5 = 0; /* Simeon */
            byte var_4;
            byte var_3;
            byte var_2;
            byte var_1;

            arg_0.Clear();
            var_4 = 0;
            var_9 = arg_0;

            var_9.affect_1 = 0;
            var_9.affect_2 = 0;
            var_9.affect_3 = 0;

            var_9.next = null;
            var_9.field_34 = 0;
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
                    throw new System.NotSupportedException();//les	di, [bp+var_9]
                    throw new System.NotSupportedException();//mov	al, es:[di+32h]
                    throw new System.NotSupportedException();//cbw
                    throw new System.NotSupportedException();//shl	ax, 1
                    throw new System.NotSupportedException();//inc	ax
                    throw new System.NotSupportedException();//inc	ax
                    throw new System.NotSupportedException();//les	di, [bp+var_9]
                    throw new System.NotSupportedException();//mov	es:[di+32h], al
                    throw new System.NotSupportedException();//les	di, [bp+var_9]
                    throw new System.NotSupportedException();//mov	al, es:[di+32h]
                    throw new System.NotSupportedException();//cmp	al, 4
                    throw new System.NotSupportedException();//jnz	loc_5A218
                    var_9.field_2F = -35;
                    throw new System.NotSupportedException();//jmp	short loc_5A224
                    throw new System.NotSupportedException();//loc_5A218:
                    throw new System.NotSupportedException();//cmp	al, 6
                    throw new System.NotSupportedException();//jnz	loc_5A224
                    var_9.field_2F = -34;
                    throw new System.NotSupportedException();//loc_5A224:
                }
                else if (var_9.type == 0x5d)
                {
                    var_9.field_31 = 0x42;
                    var_9.field_30 = -32;
                    throw new System.NotSupportedException();//les	di, [bp+var_9]
                    throw new System.NotSupportedException();//mov	al, es:[di+32h]
                    throw new System.NotSupportedException();//cbw
                    throw new System.NotSupportedException();//add	ax, 0x0A1
                    throw new System.NotSupportedException();//les	di, [bp+var_9]
                    throw new System.NotSupportedException();//mov	es:[di+2Fh], al
                }
                else
                {
                    throw new System.NotSupportedException();//les	di, [bp+var_9]
                    throw new System.NotSupportedException();//mov	al, es:[di+2Eh]
                    throw new System.NotSupportedException();//les	di, [bp+var_9]
                    throw new System.NotSupportedException();//mov	es:[di+31h], al
                    throw new System.NotSupportedException();//les	di, [bp+var_9]
                    throw new System.NotSupportedException();//mov	al, es:[di+32h]
                    throw new System.NotSupportedException();//cbw
                    throw new System.NotSupportedException();//add	ax, 0x0A1
                    throw new System.NotSupportedException();//les	di, [bp+var_9]
                    throw new System.NotSupportedException();//mov	es:[di+30h], al
                }

                var_9.field_33 = 0;
                var_9.count = 0;
                throw new System.NotSupportedException();//les	di, [bp+var_9]
                throw new System.NotSupportedException();//mov	al, es:[di+2Eh]
                throw new System.NotSupportedException();//cmp	al, 1
                throw new System.NotSupportedException();//jz	loc_5A295
                throw new System.NotSupportedException();//cmp	al, 0x0D
                throw new System.NotSupportedException();//jz	loc_5A295
                throw new System.NotSupportedException();//cmp	al, 0x0E
                throw new System.NotSupportedException();//jz	loc_5A295
                throw new System.NotSupportedException();//cmp	al, 0x23
                throw new System.NotSupportedException();//jnz	loc_5A2A1
                throw new System.NotSupportedException();//loc_5A295:
                var_9.weight = 0x4B;
                throw new System.NotSupportedException();//jmp	loc_5A48B
                throw new System.NotSupportedException();//loc_5A2A1:
                throw new System.NotSupportedException();//cmp	al, 2
                throw new System.NotSupportedException();//jz	loc_5A2CD
                throw new System.NotSupportedException();//cmp	al, 0x14
                throw new System.NotSupportedException();//jz	loc_5A2CD
                throw new System.NotSupportedException();//cmp	al, 0x1D
                throw new System.NotSupportedException();//jz	loc_5A2CD
                throw new System.NotSupportedException();//cmp	al, 0x1F
                throw new System.NotSupportedException();//jz	loc_5A2CD
                throw new System.NotSupportedException();//cmp	al, 0x20
                throw new System.NotSupportedException();//jz	loc_5A2CD
                throw new System.NotSupportedException();//cmp	al, 0x21
                throw new System.NotSupportedException();//jz	loc_5A2CD
                throw new System.NotSupportedException();//cmp	al, 0x27
                throw new System.NotSupportedException();//jz	loc_5A2CD
                throw new System.NotSupportedException();//cmp	al, 0x2A
                throw new System.NotSupportedException();//jz	loc_5A2CD
                throw new System.NotSupportedException();//cmp	al, 0x2C
                throw new System.NotSupportedException();//jz	loc_5A2CD
                throw new System.NotSupportedException();//cmp	al, 0x2E
                throw new System.NotSupportedException();//jz	loc_5A2CD
                throw new System.NotSupportedException();//cmp	al, 0x3B
                throw new System.NotSupportedException();//jnz	loc_5A2D9
                throw new System.NotSupportedException();//loc_5A2CD:
                var_9.weight = 0x32;
                throw new System.NotSupportedException();//jmp	loc_5A48B
                throw new System.NotSupportedException();//loc_5A2D9:
                throw new System.NotSupportedException();//cmp	al, 3
                throw new System.NotSupportedException();//jz	loc_5A2E5
                throw new System.NotSupportedException();//cmp	al, 0x18
                throw new System.NotSupportedException();//jz	loc_5A2E5
                throw new System.NotSupportedException();//cmp	al, 0x28
                throw new System.NotSupportedException();//jnz	loc_5A2F1
                throw new System.NotSupportedException();//loc_5A2E5:
                var_9.weight = 0x7D;
                throw new System.NotSupportedException();//jmp	loc_5A48B
                throw new System.NotSupportedException();//loc_5A2F1:
                throw new System.NotSupportedException();//cmp	al, 4
                throw new System.NotSupportedException();//jz	loc_5A30D
                throw new System.NotSupportedException();//cmp	al, 0x0F
                throw new System.NotSupportedException();//jz	loc_5A30D
                throw new System.NotSupportedException();//cmp	al, 0x17
                throw new System.NotSupportedException();//jz	loc_5A30D
                throw new System.NotSupportedException();//cmp	al, 0x22
                throw new System.NotSupportedException();//jz	loc_5A30D
                throw new System.NotSupportedException();//cmp	al, 0x2B
                throw new System.NotSupportedException();//jz	loc_5A30D
                throw new System.NotSupportedException();//cmp	al, 0x2D
                throw new System.NotSupportedException();//jz	loc_5A30D
                throw new System.NotSupportedException();//cmp	al, 0x33
                throw new System.NotSupportedException();//jnz	loc_5A319
                throw new System.NotSupportedException();//loc_5A30D:
                var_9.weight = 0x64;
                throw new System.NotSupportedException();//jmp	loc_5A48B
                throw new System.NotSupportedException();//loc_5A319:
                throw new System.NotSupportedException();//cmp	al, 5
                throw new System.NotSupportedException();//jz	loc_5A32D
                throw new System.NotSupportedException();//cmp	al, 0x0C
                throw new System.NotSupportedException();//jz	loc_5A32D
                throw new System.NotSupportedException();//cmp	al, 0x11
                throw new System.NotSupportedException();//jz	loc_5A32D
                throw new System.NotSupportedException();//cmp	al, 0x13
                throw new System.NotSupportedException();//jz	loc_5A32D
                throw new System.NotSupportedException();//cmp	al, 0x32
                throw new System.NotSupportedException();//jnz	loc_5A339
                throw new System.NotSupportedException();//loc_5A32D:
                var_9.weight = 0x96;
                throw new System.NotSupportedException();//jmp	loc_5A48B
                throw new System.NotSupportedException();//loc_5A339:
                throw new System.NotSupportedException();//cmp	al, 6
                throw new System.NotSupportedException();//jnz	loc_5A349
                var_9.weight = 0x0F;
                throw new System.NotSupportedException();//jmp	loc_5A48B
                throw new System.NotSupportedException();//loc_5A349:
                throw new System.NotSupportedException();//cmp	al, 7
                throw new System.NotSupportedException();//jnz	loc_5A359
                var_9.weight = 0x1E;
                throw new System.NotSupportedException();//jmp	loc_5A48B
                throw new System.NotSupportedException();//loc_5A359:
                throw new System.NotSupportedException();//cmp	al, 8
                throw new System.NotSupportedException();//jz	loc_5A361
                throw new System.NotSupportedException();//cmp	al, 0x4D
                throw new System.NotSupportedException();//jnz	loc_5A36D
                throw new System.NotSupportedException();//loc_5A361:
                var_9.weight = 0x0A;
                throw new System.NotSupportedException();//jmp	loc_5A48B
                throw new System.NotSupportedException();//loc_5A36D:
                throw new System.NotSupportedException();//cmp	al, 9
                throw new System.NotSupportedException();//jnz	loc_5A385
                var_9.weight = 0x19;
                var_9.count = 5;
                throw new System.NotSupportedException();//jmp	loc_5A48B
                throw new System.NotSupportedException();//loc_5A385:
                throw new System.NotSupportedException();//cmp	al, 0x0A
                throw new System.NotSupportedException();//jz	loc_5A391
                throw new System.NotSupportedException();//cmp	al, 0x1A
                throw new System.NotSupportedException();//jz	loc_5A391
                throw new System.NotSupportedException();//cmp	al, 0x24
                throw new System.NotSupportedException();//jnz	loc_5A39D
                throw new System.NotSupportedException();//loc_5A391:
                var_9.weight = 0x3C;
                throw new System.NotSupportedException();//jmp	loc_5A48B
                throw new System.NotSupportedException();//loc_5A39D:
                throw new System.NotSupportedException();//cmp	al, 0x0B
                throw new System.NotSupportedException();//jz	loc_5A3B5
                throw new System.NotSupportedException();//cmp	al, 0x10
                throw new System.NotSupportedException();//jz	loc_5A3B5
                throw new System.NotSupportedException();//cmp	al, 0x19
                throw new System.NotSupportedException();//jz	loc_5A3B5
                throw new System.NotSupportedException();//cmp	al, 0x1B
                throw new System.NotSupportedException();//jz	loc_5A3B5
                throw new System.NotSupportedException();//cmp	al, 0x29
                throw new System.NotSupportedException();//jz	loc_5A3B5
                throw new System.NotSupportedException();//cmp	al, 0x2F
                throw new System.NotSupportedException();//jnz	loc_5A3C1
                throw new System.NotSupportedException();//loc_5A3B5:
                var_9.weight = 0x50;
                throw new System.NotSupportedException();//jmp	loc_5A48B
                throw new System.NotSupportedException();//loc_5A3C1:
                throw new System.NotSupportedException();//cmp	al, 0x12
                throw new System.NotSupportedException();//jnz	loc_5A3D1
                var_9.weight = 0xAF;
                throw new System.NotSupportedException();//jmp	loc_5A48B
                throw new System.NotSupportedException();//loc_5A3D1:
                throw new System.NotSupportedException();//cmp	al, 0x15
                throw new System.NotSupportedException();//jnz	loc_5A3E1
                var_9.weight = 0x14;
                throw new System.NotSupportedException();//jmp	loc_5A48B
                throw new System.NotSupportedException();//loc_5A3E1:
                throw new System.NotSupportedException();//cmp	al, 0x16
                throw new System.NotSupportedException();//jz	loc_5A3E9
                throw new System.NotSupportedException();//cmp	al, 0x1E
                throw new System.NotSupportedException();//jnz	loc_5A3F5
                throw new System.NotSupportedException();//loc_5A3E9:
                var_9.weight = 0x28;
                throw new System.NotSupportedException();//jmp	loc_5A48B
                throw new System.NotSupportedException();//loc_5A3F5:
                throw new System.NotSupportedException();//cmp	al, 0x25
                throw new System.NotSupportedException();//jnz	loc_5A405
                var_9.weight = 0x23;
                throw new System.NotSupportedException();//jmp	loc_5A48B
                throw new System.NotSupportedException();//loc_5A405:
                throw new System.NotSupportedException();//cmp	al, 0x26
                throw new System.NotSupportedException();//jz	loc_5A40D
                throw new System.NotSupportedException();//cmp	al, 0x35
                throw new System.NotSupportedException();//jnz	loc_5A418
                throw new System.NotSupportedException();//loc_5A40D:
                var_9.weight = 0x0FA;
                throw new System.NotSupportedException();//jmp	short loc_5A48B
                throw new System.NotSupportedException();//loc_5A418:
                throw new System.NotSupportedException();//cmp	al, 0x34
                throw new System.NotSupportedException();//jnz	loc_5A427
                var_9.weight = 0x0C8;
                throw new System.NotSupportedException();//jmp	short loc_5A48B
                throw new System.NotSupportedException();//loc_5A427:
                throw new System.NotSupportedException();//cmp	al, 0x36
                throw new System.NotSupportedException();//jz	loc_5A42F
                throw new System.NotSupportedException();//cmp	al, 0x38
                throw new System.NotSupportedException();//jnz	loc_5A43A
                throw new System.NotSupportedException();//loc_5A42F:
                var_9.weight = 0x190;
                throw new System.NotSupportedException();//jmp	short loc_5A48B
                throw new System.NotSupportedException();//loc_5A43A:
                throw new System.NotSupportedException();//cmp	al, 0x37
                throw new System.NotSupportedException();//jnz	loc_5A449
                var_9.weight = 0x12C;
                throw new System.NotSupportedException();//jmp	short loc_5A48B
                throw new System.NotSupportedException();//loc_5A449:
                throw new System.NotSupportedException();//cmp	al, 0x39
                throw new System.NotSupportedException();//jnz	loc_5A458
                var_9.weight = 0x15E;
                throw new System.NotSupportedException();//jmp	short loc_5A48B
                throw new System.NotSupportedException();//loc_5A458:
                throw new System.NotSupportedException();//cmp	al, 0x3A
                throw new System.NotSupportedException();//jnz	loc_5A467
                var_9.weight = 0x1C2;
                throw new System.NotSupportedException();//jmp	short loc_5A48B
                throw new System.NotSupportedException();//loc_5A467:
                throw new System.NotSupportedException();//cmp	al, 0x2F
                throw new System.NotSupportedException();//jz	loc_5A46F
                throw new System.NotSupportedException();//cmp	al, 0x5D
                throw new System.NotSupportedException();//jnz	loc_5A47A
                throw new System.NotSupportedException();//loc_5A46F:
                var_9.weight = 1;
                throw new System.NotSupportedException();//jmp	short loc_5A48B
                throw new System.NotSupportedException();//loc_5A47A:
                var_9.weight = 0x28;
                var_9.count = 0x0A;
                throw new System.NotSupportedException();//loc_5A48B:
                throw new System.NotSupportedException();//les	di, [bp+var_9]
                throw new System.NotSupportedException();//mov	al, es:[di+2Eh]
                throw new System.NotSupportedException();//cmp	al, 0x3B
                throw new System.NotSupportedException();//jnz	loc_5A4AD
                throw new System.NotSupportedException();//les	di, [bp+var_9]
                throw new System.NotSupportedException();//mov	al, es:[di+32h]
                throw new System.NotSupportedException();//cbw
                throw new System.NotSupportedException();//mov	dx, 0x9C4
                throw new System.NotSupportedException();//mul	dx
                throw new System.NotSupportedException();//les	di, [bp+var_9]
                throw new System.NotSupportedException();//mov	es:[di+3Ah], ax
                throw new System.NotSupportedException();//jmp	loc_5A56B
                throw new System.NotSupportedException();//loc_5A4AD:
                throw new System.NotSupportedException();//cmp	al, 0x49
                throw new System.NotSupportedException();//jz	loc_5A4B5
                throw new System.NotSupportedException();//cmp	al, 0x1C
                throw new System.NotSupportedException();//jnz	loc_5A4CC
                throw new System.NotSupportedException();//loc_5A4B5:
                throw new System.NotSupportedException();//les	di, [bp+var_9]
                throw new System.NotSupportedException();//mov	al, es:[di+32h]
                throw new System.NotSupportedException();//cbw
                throw new System.NotSupportedException();//mov	dx, 0x96
                throw new System.NotSupportedException();//mul	dx
                throw new System.NotSupportedException();//les	di, [bp+var_9]
                throw new System.NotSupportedException();//mov	es:[di+3Ah], ax
                throw new System.NotSupportedException();//jmp	loc_5A56B
                throw new System.NotSupportedException();//loc_5A4CC:
                throw new System.NotSupportedException();//cmp	al, 0x35
                throw new System.NotSupportedException();//jz	loc_5A4D4
                throw new System.NotSupportedException();//cmp	al, 0x36
                throw new System.NotSupportedException();//jnz	loc_5A4EB
                throw new System.NotSupportedException();//loc_5A4D4:
                throw new System.NotSupportedException();//les	di, [bp+var_9]
                throw new System.NotSupportedException();//mov	al, es:[di+32h]
                throw new System.NotSupportedException();//cbw
                throw new System.NotSupportedException();//mov	dx, 0x0BB8
                throw new System.NotSupportedException();//mul	dx
                throw new System.NotSupportedException();//les	di, [bp+var_9]
                throw new System.NotSupportedException();//mov	es:[di+3Ah], ax
                throw new System.NotSupportedException();//jmp	loc_5A56B
                throw new System.NotSupportedException();//loc_5A4EB:
                throw new System.NotSupportedException();//cmp	al, 0x37
                throw new System.NotSupportedException();//jz	loc_5A4F3
                throw new System.NotSupportedException();//cmp	al, 0x38
                throw new System.NotSupportedException();//jnz	loc_5A509
                throw new System.NotSupportedException();//loc_5A4F3:
                throw new System.NotSupportedException();//les	di, [bp+var_9]
                throw new System.NotSupportedException();//mov	al, es:[di+32h]
                throw new System.NotSupportedException();//cbw
                throw new System.NotSupportedException();//mov	dx, 0x0DAC
                throw new System.NotSupportedException();//mul	dx
                throw new System.NotSupportedException();//les	di, [bp+var_9]
                throw new System.NotSupportedException();//mov	es:[di+3Ah], ax
                throw new System.NotSupportedException();//jmp	short loc_5A56B
                throw new System.NotSupportedException();//loc_5A509:
                throw new System.NotSupportedException();//cmp	al, 0x39
                throw new System.NotSupportedException();//jnz	loc_5A523
                throw new System.NotSupportedException();//les	di, [bp+var_9]
                throw new System.NotSupportedException();//mov	al, es:[di+32h]
                throw new System.NotSupportedException();//cbw
                throw new System.NotSupportedException();//mov	dx, 0x0FA0
                throw new System.NotSupportedException();//mul	dx
                throw new System.NotSupportedException();//les	di, [bp+var_9]
                throw new System.NotSupportedException();//mov	es:[di+3Ah], ax
                throw new System.NotSupportedException();//jmp	short loc_5A56B
                throw new System.NotSupportedException();//loc_5A523:
                throw new System.NotSupportedException();//cmp	al, 0x3A
                throw new System.NotSupportedException();//jnz	loc_5A53D
                throw new System.NotSupportedException();//les	di, [bp+var_9]
                throw new System.NotSupportedException();//mov	al, es:[di+32h]
                throw new System.NotSupportedException();//cbw
                throw new System.NotSupportedException();//mov	dx, 0x1388
                throw new System.NotSupportedException();//mul	dx
                throw new System.NotSupportedException();//les	di, [bp+var_9]
                throw new System.NotSupportedException();//mov	es:[di+3Ah], ax
                throw new System.NotSupportedException();//jmp	short loc_5A56B
                throw new System.NotSupportedException();//loc_5A53D:
                throw new System.NotSupportedException();//cmp	al, 0x4D
                throw new System.NotSupportedException();//jnz	loc_5A557
                throw new System.NotSupportedException();//les	di, [bp+var_9]
                throw new System.NotSupportedException();//mov	al, es:[di+32h]
                throw new System.NotSupportedException();//cbw
                throw new System.NotSupportedException();//mov	dx, 0x0BB8
                throw new System.NotSupportedException();//mul	dx
                throw new System.NotSupportedException();//les	di, [bp+var_9]
                throw new System.NotSupportedException();//mov	es:[di+3Ah], ax
                throw new System.NotSupportedException();//jmp	short loc_5A56B
                throw new System.NotSupportedException();//loc_5A557:
                throw new System.NotSupportedException();//les	di, [bp+var_9]
                throw new System.NotSupportedException();//mov	al, es:[di+32h]
                throw new System.NotSupportedException();//cbw
                throw new System.NotSupportedException();//mov	dx, 0x7D0
                throw new System.NotSupportedException();//mul	dx
                throw new System.NotSupportedException();//les	di, [bp+var_9]
                throw new System.NotSupportedException();//mov	es:[di+3Ah], ax
                throw new System.NotSupportedException();//loc_5A56B:
                throw new System.NotSupportedException();//jmp	loc_5A797
            }
            throw new System.NotSupportedException();//loc_5A56E:
            throw new System.NotSupportedException();//cmp	al, 0x3D
            throw new System.NotSupportedException();//jz	loc_5A579
            throw new System.NotSupportedException();//cmp	al, 0x3E
            throw new System.NotSupportedException();//jz	loc_5A579
            throw new System.NotSupportedException();//jmp	loc_5A73E
            throw new System.NotSupportedException();//loc_5A579:
            var_2 = ovr024.roll_dice(3, 1);
            throw new System.NotSupportedException();//les	di, [bp+var_9]
            throw new System.NotSupportedException();//cmp	byte ptr es:[di+2Eh], 0x3D
            throw new System.NotSupportedException();//jnz	loc_5A59B
            var_9.field_31 = 0xD1;
            throw new System.NotSupportedException();//jmp	short loc_5A5A3
            throw new System.NotSupportedException();//loc_5A59B:
            var_9.field_31 = 0xD0;
            throw new System.NotSupportedException();//loc_5A5A3:
            throw new System.NotSupportedException();//mov	al, [bp+var_2]
            throw new System.NotSupportedException();//xor	ah, ah
            throw new System.NotSupportedException();//add	ax, 0x0D1
            throw new System.NotSupportedException();//les	di, [bp+var_9]
            throw new System.NotSupportedException();//mov	es:[di+30h], al
            var_9.field_2F = 0;
            var_9.exp_value = 1;
            var_9.weight = 0x19;
            var_9.count = 0;
            throw new System.NotSupportedException();//les	di, [bp+var_9]
            throw new System.NotSupportedException();//xor	ax, ax
            throw new System.NotSupportedException();//mov	es:[di+3Ah], ax
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
            throw new System.NotSupportedException();//jmp	short loc_5A797
            throw new System.NotSupportedException();//loc_5A73E:
            throw new System.NotSupportedException();//cmp	al, 0x3F
            throw new System.NotSupportedException();//jz	loc_5A746
            throw new System.NotSupportedException();//cmp	al, 0x43
            throw new System.NotSupportedException();//jnz	loc_5A74C
            throw new System.NotSupportedException();//loc_5A746:
            var_4 = 0x29;
            throw new System.NotSupportedException();//jmp	short loc_5A797
            throw new System.NotSupportedException();//loc_5A74C:
            throw new System.NotSupportedException();//cmp	al, 0x4E
            throw new System.NotSupportedException();//jz	loc_5A754
            throw new System.NotSupportedException();//cmp	al, 0x4F
            throw new System.NotSupportedException();//jnz	loc_5A75A
            throw new System.NotSupportedException();//loc_5A754:
            var_4 = 0x21;
            throw new System.NotSupportedException();//jmp	short loc_5A797
            throw new System.NotSupportedException();//loc_5A75A:
            throw new System.NotSupportedException();//cmp	al, 0x54
            throw new System.NotSupportedException();//jz	loc_5A762
            throw new System.NotSupportedException();//cmp	al, 0x5C
            throw new System.NotSupportedException();//jnz	loc_5A768
            throw new System.NotSupportedException();//loc_5A762:
            var_4 = 9;
            throw new System.NotSupportedException();//jmp	short loc_5A797
            throw new System.NotSupportedException();//loc_5A768:
            throw new System.NotSupportedException();//cmp	al, 0x47
            throw new System.NotSupportedException();//jnz	loc_5A797
            ovr024.roll_dice(8, 1);
            throw new System.NotSupportedException();//mov	[bp+var_1], al
            throw new System.NotSupportedException();//mov	al, [bp+var_1]
            throw new System.NotSupportedException();//cmp	al, 1
            throw new System.NotSupportedException();//jb	loc_5A78B
            throw new System.NotSupportedException();//cmp	al, 5
            throw new System.NotSupportedException();//ja	loc_5A78B
            var_4 = 0x11;
            throw new System.NotSupportedException();//jmp	short loc_5A797
            throw new System.NotSupportedException();//loc_5A78B:
            throw new System.NotSupportedException();//cmp	al, 6
            throw new System.NotSupportedException();//jb	loc_5A797
            throw new System.NotSupportedException();//cmp	al, 8
            throw new System.NotSupportedException();//ja	loc_5A797
            var_4 = 1;


            throw new System.NotSupportedException();//loc_5A797:


            throw new System.NotSupportedException();//cmp	[bp+var_4], 0
            throw new System.NotSupportedException();//jnz	loc_5A7A0
            throw new System.NotSupportedException();//jmp	loc_5A850
            throw new System.NotSupportedException();//loc_5A7A0:
            for (var_3 = 0; var_3 <= 2; var_3++)
            {
                throw new System.NotSupportedException();//mov	al, [bp+var_3]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//mov	dx, ax
                throw new System.NotSupportedException();//mov	al, [bp+var_4]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//add	ax, dx
                throw new System.NotSupportedException();//mov	di, ax
                throw new System.NotSupportedException();//shl	di, 1
                throw new System.NotSupportedException();//mov	dl, [di+82Eh]
                throw new System.NotSupportedException();//mov	al, [bp+var_3]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//inc	ax
                throw new System.NotSupportedException();//les	di, [bp+var_9]
                throw new System.NotSupportedException();//add	di, ax
                throw new System.NotSupportedException();//mov	es:[di+2Eh], dl
            }
            var_9.exp_value = 1;
            var_9.field_33 = 1;

            throw new System.NotSupportedException();//mov	al, [bp+var_4]
            throw new System.NotSupportedException();//xor	ah, ah
            throw new System.NotSupportedException();//add	ax, 3
            throw new System.NotSupportedException();//mov	di, ax
            throw new System.NotSupportedException();//shl	di, 1
            throw new System.NotSupportedException();//mov	ax, [di+82Eh]
            throw new System.NotSupportedException();//les	di, [bp+var_9]
            throw new System.NotSupportedException();//mov	es:[di+37h], ax
            var_9.count = 0;
            throw new System.NotSupportedException();//mov	al, [bp+var_4]
            throw new System.NotSupportedException();//xor	ah, ah
            throw new System.NotSupportedException();//add	ax, 4
            throw new System.NotSupportedException();//mov	di, ax
            throw new System.NotSupportedException();//shl	di, 1
            throw new System.NotSupportedException();//mov	ax, [di+82Eh]
            throw new System.NotSupportedException();//les	di, [bp+var_9]
            throw new System.NotSupportedException();//mov	es:[di+3Ah], ax

            for (var_3 = 1; var_3 <= 3; var_3++)
            {
                var_9.setAffect(var_3, (Affects)unk_16B3E[(var_4 + 4 + var_3) * 2]);
            }

            throw new System.NotSupportedException();//loc_5A850:
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
                        ovr025.sub_678A2(0, 1, 1, gbl.player_ptr);

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

                        var_53 = ovr027.displayInput(out var_B4, 0, 1, 15, 10, 13, var_7C, "Appraise : ");

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

                                var_53 = ovr027.displayInput(out var_B4, 0, 1, 15, 10, 13, var_A5, "You can : ");

                                if (var_53 == 'K' && var_B5 == 0)
                                {
                                    var_B2 = new Item();
                                    var_B2.next = null;
                                    var_B2.weight = 1;
                                    var_B2.field_35 = 0;
                                    var_B2.field_34 = 0;
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

                                var_53 = ovr027.displayInput(out var_B4, 0, 1, 15, 10, 13, var_A5, "You can : ");

                                if (var_53 == 'K' && var_B5 == 0)
                                {
                                    var_B2 = new Item();
                                    var_B2.next = null;
                                    var_B2.field_34 = 0;
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
