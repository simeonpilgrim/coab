using Classes;

namespace engine
{
    class ovr010
    {
        internal static void sub_3504B(Player player)
        {
            bool var_2;
            byte var_1;

            var_2 = sub_36269(player);
            ovr027.redraw_screen();
            ovr025.sub_6786F();

            if (player.in_combat == false)
            {
                var_2 = ovr025.clear_actions(player);
            }

            var_1 = player.actions.field_15;

            if (var_1 == 0 || var_1 == 4 || ovr024.roll_dice(4, 1) == 1)
            {
                var_1 = ovr024.roll_dice(8, 1);

                if (var_1 != 8)
                {
                    var_1 = (byte)(ovr024.roll_dice(2, 1) + 4);
                }
                else
                {
                    var_1 = ovr024.roll_dice(4, 1);
                }
            }

            player.actions.field_15 = var_1;

            if (var_2 == false)
            {
                var_2 = sub_3637F(player);
            }

            if (player.actions.field_14 != 0 &&
                player.actions.field_10 == 0)
            {
                ovr025.sub_67788(1, 10, "flees in panic", player);
            }

            if (var_2 == true)
            {
                return;
            }

            if (sub_354AA(player) != 0)
            {
                var_2 = ovr025.clear_actions(player);
                return;
            }

            if (player.actions.spell_id > 0)
            {
                ovr023.sub_5D2E1(ref var_2, 1, 1, player.actions.spell_id);

                var_2 = ovr025.clear_actions(player);
                return;
            }

            if (turn_undead(player) != 0)
            {
                var_2 = ovr025.clear_actions(player);
                return;
            }

            if (sub_3560B(player) == true)
            {
                var_2 = true;
                return;
            }

            sub_36673(player);
            var_2 = sub_36269(player);

            while (var_2 == false)
            {
                if (ovr014.sub_41E44(0, 1, 0xff, player) == true &&
                    player.actions.delay > 0 &&
                    player.in_combat == true)
                {
                    var_2 = sub_35DB1(player);
                }
                else
                {
                    var_2 = sub_361F7(player);
                }
            }
        }


        internal static byte turn_undead(Player player)
        {
            Player var_5;
            byte ret_val;

            if (player.actions.field_11 == 0 &&
                (player.cleric_lvl > 0 || player.turn_undead > player.field_E6) &&
                ovr014.sub_3F433(out var_5, player) == true)
            {
                ret_val = 1;
                ovr014.turns_undead(player);
            }
            else
            {
                ret_val = 0;
            }

            return ret_val;
        }


        internal static bool sub_352AF(byte arg_2, sbyte arg_4, sbyte arg_6)
        {
            byte var_4;
            sbyte var_3;
            byte var_2;
            bool var_1;

            var_1 = false;

            if (gbl.player_ptr.combat_team == 0)
            {
                var_3 = -2;
            }
            else
            {
                var_3 = 8;
            }

            ovr032.sub_738D8(gbl.stru_1D1BC, 1, 0xff, gbl.unk_19AEC[arg_2].field_F, arg_4, arg_6);
            var_4 = gbl.byte_1D1C0;

            for (var_2 = 1; var_2 <= var_4; var_2++)
            {
                Player tmpPlayer = gbl.player_array[gbl.unk_1D1C1[var_2].field_0]; // Index is not fixed up.
                Struct_19AEC tmpS = gbl.unk_19AEC[arg_2];

                if (ovr025.on_our_team(gbl.player_ptr) != tmpPlayer.combat_team &&
                    tmpS.field_8 != 1 &&
                    ovr024.do_saving_throw(var_3, tmpS.field_9, tmpPlayer) == false)
                {
                    var_1 = true;
                }
            }

            return var_1;
        }


        internal static byte sub_353B1(byte arg_0, byte arg_2, Player arg_4)
        {
            byte var_8;
            byte var_7;
            byte var_6;
            Player var_5;
            byte var_1;

            var_1 = 0;

            if (gbl.unk_19AEC[arg_2].field_D < arg_0)
            {

                throw new System.NotSupportedException();//cmp	[bp+arg_2], 3
                throw new System.NotSupportedException();//jz	loc_353EA
                throw new System.NotSupportedException();//cmp	gbl.unk_19AEC[ arg_2 ].field_E, 0
                throw new System.NotSupportedException();//jz	loc_35404
                throw new System.NotSupportedException();//loc_353EA:
                throw new System.NotSupportedException();//cmp	[bp+arg_2], 3
                throw new System.NotSupportedException();//jnz	loc_3540B
                ovr014.sub_3FDFE(out var_5, arg_4);
                throw new System.NotSupportedException();//or	al, al
                throw new System.NotSupportedException();//jz	loc_3540B
                throw new System.NotSupportedException();//loc_35404:
                var_1 = 1;
                throw new System.NotSupportedException();//jmp	loc_354A1
                throw new System.NotSupportedException();//loc_3540B:

                var_6 = ovr025.near_enermy(ovr023.sub_5CDE5(arg_2), arg_4);

                if (var_6 > 0)
                {
                    if (gbl.unk_19AEC[arg_2].field_F == 0)
                    {
                        var_1 = 1;
                    }
                    else
                    {
                        var_8 = var_6;

                        for (var_7 = 1; var_7 <= var_8; var_7++)
                        {
                            if (sub_352AF(arg_2, gbl.stru_1C9CD[gbl.byte_1D8B9[var_7]].field_1, gbl.stru_1C9CD[gbl.byte_1D8B9[var_7]].field_0) == true)
                            {
                                return var_1;
                            }
                        }
                        var_1 = 1;
                    }
                }
            }
            throw new System.NotSupportedException();//loc_354A1:
            return var_1;
        }


        internal static byte sub_354AA(Player player)
        {
            byte var_16;
            bool var_15 = false; /* simeon */
            Item var_14;
            Item item_ptr;
            byte var_8;
            byte var_7;
            byte var_4;
            byte var_3;
            byte var_2;
            byte var_1;

            var_1 = 0;

            var_14 = null;
            var_2 = 7;
            var_3 = ovr024.roll_dice(7, 1);

            int teamCount = (ovr025.on_our_team(player) == 0) ? gbl.friends_count : gbl.foe_count;
            if (player.actions.field_2 != 0 &&
                teamCount > 0 &&
                gbl.area_ptr.can_cast_spells == false)
            {
                var_16 = var_3;

                for (var_4 = 1; var_4 <= var_16; var_4++)
                {
                    item_ptr = player.itemsPtr;

                    while (item_ptr != null && var_14 == null)
                    {
                        var_8 = (byte)item_ptr.affect_2;
                        var_7 = gbl.unk_1C020[item_ptr.type].field_0;


                        if (ovr023.item_is_scroll(item_ptr) == false &&
                            (int)item_ptr.affect_3 < 0x80 &&
                            item_ptr.field_34 != 0 &&
                            var_8 > 0)
                        {
                            if (var_8 > 0x38)
                            {
                                var_8 -= 0x17;
                            }

                            if (sub_353B1(var_2, var_8, player) != 0)
                            {
                                var_14 = item_ptr;
                            }
                        }

                        item_ptr = item_ptr.next;
                    }
                    var_2 -= 1;
                }
            }

            if (var_14 != null)
            {
                ovr020.sub_56478(ref var_15, var_14);
                var_1 = 1;
            }

            return var_1;
        }


        internal static bool sub_3560B(Player player)
        {
            byte var_62;
            byte var_61;
            byte var_60;
            byte var_5F;
            byte var_5E;
            byte var_5D;
            bool spell_id;
            byte var_5B;
            byte var_5A;
            byte[] var_55 = new byte[0x53];

            var_5F = 0;

            if (player.actions.can_cast != 0)
            {
                for (var_60 = 1; var_60 <= 0x53; var_60++)
                {
                    if (player.spell_list[var_60] > 0)
                    {
                        var_55[var_5F] = player.spell_list[var_60];
                        var_5F++;
                    }
                }
            }

            var_62 = 0;
            var_5A = 7;
            var_5B = ovr024.roll_dice(7, 1);
            var_5D = 1;


            throw new System.NotSupportedException();//cmp	[bp+var_5F], 0
            throw new System.NotSupportedException();//ja	loc_35682
            throw new System.NotSupportedException();//jmp	loc_3570F
            throw new System.NotSupportedException();//loc_35682:
            throw new System.NotSupportedException();//cmp	player.field_F7, 0x7F
            throw new System.NotSupportedException();//ja	loc_35694
            throw new System.NotSupportedException();//cmp	byte_1D904, 0
            throw new System.NotSupportedException();//jz	loc_3570F
            throw new System.NotSupportedException();//loc_35694:
            throw new System.NotSupportedException();//push	[bp+player.seg]
            throw new System.NotSupportedException();//push	[bp+player.offset]
            throw new System.NotSupportedException();//call	ovr025.on_our_team(Player *)
            throw new System.NotSupportedException();//cbw
            throw new System.NotSupportedException();//mov	di, ax
            throw new System.NotSupportedException();//cmp	friends_count[di], 0
            throw new System.NotSupportedException();//jbe	loc_3570F
            throw new System.NotSupportedException();//loc_356A9:
            throw new System.NotSupportedException();//mov	al, [bp+var_5D]
            throw new System.NotSupportedException();//cmp	al, [bp+var_5B]
            throw new System.NotSupportedException();//ja	loc_3570F
            throw new System.NotSupportedException();//cmp	[bp+var_62], 0
            throw new System.NotSupportedException();//jnz	loc_3570F
            var_5E = 1;
            throw new System.NotSupportedException();//loc_356BB:
            throw new System.NotSupportedException();//cmp	[bp+var_5E], 4
            throw new System.NotSupportedException();//jnb	loc_35707
            throw new System.NotSupportedException();//cmp	[bp+var_62], 0
            throw new System.NotSupportedException();//jnz	loc_35707
            var_60 = (byte)(ovr024.roll_dice(var_5F, 1) - 1);
            var_61 = var_55[var_60];
            throw new System.NotSupportedException();//push	[bp+player.seg]
            throw new System.NotSupportedException();//push	[bp+player.offset]
            throw new System.NotSupportedException();//mov	al, [bp+var_61]
            throw new System.NotSupportedException();//push	ax
            throw new System.NotSupportedException();//mov	al, [bp+var_5A]
            throw new System.NotSupportedException();//push	ax
            throw new System.NotSupportedException();//push	cs
            throw new System.NotSupportedException();//call	near ptr sub_353B1
            throw new System.NotSupportedException();//or	al, al
            throw new System.NotSupportedException();//jz	loc_35702
            var_62 = var_61;
            throw new System.NotSupportedException();//loc_35702:
            var_5E++;
            throw new System.NotSupportedException();//jmp	short loc_356BB
            throw new System.NotSupportedException();//loc_35707:
            var_5A--;
            var_5D++;
            throw new System.NotSupportedException();//jmp	short loc_356A9
            throw new System.NotSupportedException();//loc_3570F:

            if (var_62 > 0)
            {
                ovr014.spell_menu3(out spell_id, 1, var_62);
            }
            else
            {
                spell_id = false;
            }

            return spell_id;
        }

        static byte[] data_2B8 = new byte[]{ 0, 8, 7, 6, 1, 2, 8, 1, 2, 7 };/* actual from seg600:02BD */
              


        internal static byte sub_3573B(out bool arg_0, byte arg_4, byte arg_6, Player player)
        {
            byte var_D;
            byte var_C;
            byte var_B;
            byte var_A;
            byte var_9;
            byte var_8;
            byte var_7;
            byte var_6;
            Affect var_5;
            byte var_1;

            arg_0 = false;
            var_D = 0;

            var_6 = data_2B8[(player.actions.field_15 * 5) + arg_6];
            var_7 = (byte)((arg_4 + var_6) % 8);

            ovr033.sub_74D04(out var_C, out var_B, out var_9, out var_8, var_7, player);
            throw new System.NotSupportedException();//cmp	[bp+var_9], 0
            throw new System.NotSupportedException();//jnz	loc_357BF
            arg_0 = true;
            throw new System.NotSupportedException();//jmp	loc_359A6
            throw new System.NotSupportedException();//loc_357BF:
            throw new System.NotSupportedException();//mov	al, [bp+var_9]
            throw new System.NotSupportedException();//xor	ah, ah
            throw new System.NotSupportedException();//mov	di, ax
            throw new System.NotSupportedException();//shl	di, 1
            throw new System.NotSupportedException();//shl	di, 1
            throw new System.NotSupportedException();//cmp	unk_189B4[di].field_0,	0x0FF
            throw new System.NotSupportedException();//jnz	loc_357D8
            var_1 = 0;
            throw new System.NotSupportedException();//jmp	func_end
            throw new System.NotSupportedException();//loc_357D8:
            if ((var_7 & 1) != 0)
            {
                var_A = (byte)(gbl.unk_189B4[var_9].field_0 * 3);
            }
            else
            {
                var_A = (byte)(gbl.unk_189B4[var_9].field_0 * 2);
            }

            throw new System.NotSupportedException();//cmp	[bp+var_8], 0
            throw new System.NotSupportedException();//jz	loc_3581A
            throw new System.NotSupportedException();//jmp	loc_359A6
            throw new System.NotSupportedException();//loc_3581A:
            throw new System.NotSupportedException();//cmp	var_A, player.actions.move
            throw new System.NotSupportedException();//jb	loc_3582E
            throw new System.NotSupportedException();//jmp	loc_359A6
            throw new System.NotSupportedException();//loc_3582E:
            if (var_B != 0 &&
                ovr025.find_affect(out var_5, Affects.funky__32, player) == false &&
                ovr025.find_affect(out var_5, Affects.affect_1e, player) == false &&
                ovr025.find_affect(out var_5, Affects.affect_6f, player) == false &&
                ovr025.find_affect(out var_5, Affects.affect_7d, player) == false &&
                ovr025.find_affect(out var_5, Affects.affect_81, player) == false &&
                ovr025.find_affect(out var_5, Affects.minor_globe_of_invulnerability, player) == false &&
                player.actions.field_10 == 0)
            {
                if (ovr024.do_saving_throw(0, 0, player) == false)
                {
                    var_A = (byte)(player.actions.move + 1);
                }
            }


            if (var_C != 0 &&
                player.field_E5 < 7 &&
                ovr025.find_affect(out var_5, Affects.affect_81, player) == false &&
                ovr025.find_affect(out var_5, Affects.affect_6f, player) == false &&
                ovr025.find_affect(out var_5, Affects.affect_85, player) == false &&
                ovr025.find_affect(out var_5, Affects.affect_7d, player) == false &&
                player.actions.field_10 == 0)
            {
                var_A = (byte)(player.actions.move + 1);
            }

            if (player.actions.move >= var_A)
            {
                var_D = 1;
            }
            throw new System.NotSupportedException();//loc_359A6:
            var_1 = var_D;
            throw new System.NotSupportedException();//func_end:
            return var_1;
        }


        internal static void sub_359D1(Player player)
        {
            string var_209;
            bool var_5;
            bool var_4;
            byte var_3;
            byte var_2 = 0; /* Simeon */
            byte var_1;

            var_209 = string.Format("Move/Attack, Move Left = {0} ", player.actions.move / 2);

            seg041.displayString(var_209, 0, 10, 0x18, 0);

            if (sub_36269(player) == false)
            {
                if ((player.actions.move / 2) > 0 &&
                    player.actions.delay > 0)
                {
                    throw new System.NotSupportedException();//cmp	player.field_F7, 0x80
                    throw new System.NotSupportedException();//jb	loc_35AA3
                    throw new System.NotSupportedException();//cmp	player.field_F7, 0x7F
                    throw new System.NotSupportedException();//jbe	loc_35A95
                    ovr024.roll_dice(100, 1);
                    throw new System.NotSupportedException();//xor	ah, ah
                    throw new System.NotSupportedException();//mov	dx, ax
                    throw new System.NotSupportedException();//mov	al, byte_1D2CC
                    throw new System.NotSupportedException();//xor	ah, ah
                    throw new System.NotSupportedException();//add	ax, dx
                    throw new System.NotSupportedException();//mov	dx, ax
                    throw new System.NotSupportedException();//mov	al, byte_1D903
                    throw new System.NotSupportedException();//xor	ah, ah
                    throw new System.NotSupportedException();//cmp	ax, dx
                    throw new System.NotSupportedException();//jle	loc_35AA3
                    throw new System.NotSupportedException();//loc_35A95:
                    throw new System.NotSupportedException();//cmp	player.combat_team,	1
                    throw new System.NotSupportedException();//jz	loc_35AA3
                    throw new System.NotSupportedException();//jmp	loc_35D9E
                    throw new System.NotSupportedException();//loc_35AA3:
                    if (player.actions.field_14 != 0 ||
                        player.field_159 != null ||
                        player._class != ClassId.magic_user)
                    {
                        if (player.actions.field_14 == 0)
                        {
                            var_1 = ovr014.sub_409BC(player.actions.target, player);
                        }
                        else
                        {
                            player.actions.field_15 = ovr024.roll_dice(2, 1);
                            var_1 = (byte)(gbl.mapDirection - (((gbl.mapDirection + 2) % 4) / 2));

                            if (player.combat_team == 0)
                            {
                                var_1 += 4;
                            }

                            var_1 %= 8;
                        }

                        var_4 = false;
                        var_5 = false;
                        var_3 = 1;

                        while (var_3 < 6 && var_5 == false &&
                            sub_3573B(out var_4, var_1, var_3, player) == 0)
                        {
                            if (player.actions.field_14 != 0 &&
                                var_4 == true)
                            {
                                var_5 = ovr014.flee_battle(player);
                            }
                            else
                            {
                                var_3++;
                            }
                        }

                        if (var_5 == true)
                        {
                            player.actions.move = 0;
                            player.actions.field_14 = 0;
                            var_5 = ovr025.clear_actions(player);
                        }
                        else
                        {
                            var_2 = (byte)((data_2B8[(player.actions.field_15 * 5) + var_3] + var_1) % 8);

                            if (var_3 == 6 || ((var_2 + 4) % 8) == gbl.byte_1AB18)
                            {
                                gbl.byte_1AB19++;
                                player.actions.field_15 %= 6;
                                player.actions.field_15 += 1;

                                if (gbl.byte_1AB19 > 1)
                                {
                                    player.actions.target = null;

                                    if (gbl.byte_1AB19 > 2)
                                    {
                                        player.actions.move = 0;
                                        var_5 = true;
                                    }
                                    else if (ovr014.sub_41E44(0, 1, 0xFF, player) == false)
                                    {
                                        var_5 = sub_361F7(player);
                                    }
                                }
                            }

                            if (var_3 < 6)
                            {
                                gbl.byte_1AB18 = var_2;
                            }
                            else
                            {
                                var_5 = true;
                            }
                        }

                        if (var_5 == false)
                        {
                            gbl.byte_1D910 = (gbl.byte_1D90E || ovr033.sub_74761(0, player) || player.combat_team == 0);

                            ovr033.sub_74B3F(0, 0, var_2, player);
                            ovr014.sub_3E954(player.actions.field_9, player);

                            if (player.in_combat == false)
                            {
                                var_5 = ovr025.clear_actions(player);
                            }
                            else
                            {
                                if (player.actions.move > 0)
                                {
                                    ovr014.sub_3E748(player.actions.field_9, player);
                                }

                                if (player.in_combat == false)
                                {
                                    var_5 = ovr025.clear_actions(player);
                                }

                                ovr024.in_poison_cloud(1, player);
                            }
                        }
                        return;
                    }
                }
                throw new System.NotSupportedException();//loc_35D9E:
                var_5 = sub_361F7(player);
            }
        }


        internal static bool sub_35DB1(Player player)
        {
            byte var_13;
            sbyte var_12;
            sbyte var_11;
            Player player01;
            Item var_C;
            byte var_8;
            byte var_7;
            short var_6;
            byte var_4;
            bool var_3;
            bool var_2;


            gbl.byte_1AB18 = 8;
            gbl.byte_1AB19 = 0;
            gbl.byte_1AB1A = 0;

            var_13 = 0;
            var_3 = true;
            var_2 = false;

            ovr024.work_on_00(player, 14);

            if (player.combat_team == 0 &&
                ovr025.bandage(true) == true)
            {
                player.actions.delay = 0;
            }

            if (player.actions.delay == 0)
            {
                var_3 = false;
            }

            while (var_2 == false && var_3 == true)
            {
                if (player.actions.field_14 != 0)
                {
                    while (player.actions.move > 0 &&
                        player.actions.delay > 0 &&
                        player.actions.delay < 20)
                    {
                        sub_359D1(player);
                    }
                }

                if (player.actions.delay == 0 ||
                    player.actions.delay == 20)
                {
                    var_3 = false;
                }
                else
                {
                    var_2 = false;
                }

                if (var_2 == false && var_3 == true)
                {
                    var_13++;

                    if (var_13 > 20)
                    {
                        var_2 = true;

                        var_3 = false;
                        if (sub_361F7(player) == false)
                        {
                            var_3 = true;
                        }
                    }

                    gbl.byte_1D90E = false;
                    var_4 = 1;

                    if (player.field_151 != null)
                    {
                        var_4 = (byte)(gbl.unk_1C020[player.field_151.type].field_C - 1);
                    }

                    if (var_4 == 0 || var_4 == 0xff)
                    {
                        var_4 = 1;
                    }

                    player01 = player.actions.target;

                    if (player01.in_combat == false ||
                        player01.combat_team == 0)
                    {
                        player01 = null;
                    }

                    if (player01 != null &&
                        ovr014.sub_3F143(player01, player) == true)
                    {
                        var_11 = ovr033.sub_74C32(player01);
                        var_12 = ovr033.sub_74C5A(player01);

                        var_6 = var_4;

                        gbl.stru_1D1BC.field_6 = 0;

                        if (ovr032.sub_733F1(gbl.stru_1D1BC, ref var_6, ref var_12, ref var_11, ovr033.sub_74C5A(player), ovr033.sub_74C32(player)) == true &&
                            (var_6 / 2) <= var_4)
                        {
                            gbl.byte_1D90E = true;
                        }
                    }

                    if (gbl.byte_1D90E == false)
                    {
                        var_7 = ovr025.near_enermy(var_4, player);

                        if (var_7 == 0)
                        {
                            if (ovr014.sub_41E44(0, 0, 0xff, player) == true)
                            {
                                sub_359D1(player);
                            }
                            else
                            {
                                var_2 = sub_361F7(player);
                            }
                        }
                        else
                        {
                            var_8 = ovr024.roll_dice(var_7, 1);

                            player01 = gbl.player_array[gbl.byte_1D8B9[var_8]];

                            if (ovr025.offset_above_1(player) == true &&
                                ovr025.offset_equals_20(player) == false &&
                                ovr025.near_enermy(1, player) > 0)
                            {
                                sub_36673(player);
                                var_2 = true;
                            }
                            else if (ovr025.sub_68708(player01, player) == 1 ||
                                ovr014.sub_3F143(player01, player) == true)
                            {
                                gbl.byte_1D90E = true;
                            }
                        }
                    }

                    if (gbl.byte_1D90E == true)
                    {
                        ovr033.sub_749DD(ovr014.sub_409BC(player01, player), 2, ovr033.sub_74C5A(player), ovr033.sub_74C32(player));

                    }

                    if (gbl.byte_1D90E == true)
                    {
                        if (ovr014.sub_3EF3D(player01, player) == true)
                        {
                            var_2 = ovr025.clear_actions(player);
                        }
                        else
                        {
                            ovr014.sub_3F94D(player01, player);

                            var_C = null;

                            if (ovr025.offset_above_1(player) == true)
                            {
                                gbl.byte_1D90E = ovr025.sub_6906C(out var_C, player);

                                if (ovr025.offset_equals_20(player) == true &&
                                    ovr025.sub_68708(player01, player) == 1)
                                {

                                    var_C = null;
                                }
                            }

                            ovr014.sub_3F9DB(out var_2, var_C, 0, player01, player);

                            if (var_2 == true)
                            {
                                var_3 = false;
                            }
                            else if (player01.in_combat == false)
                            {
                                var_2 = true;
                            }
                        }
                    }
                }
            }

            return (var_3 == false);
        }


        internal static bool sub_361F7(Player player)
        {
            bool var_1;

            ovr025.sub_6786F();

            if (ovr025.is_held(player) == true ||
                ovr025.offset_above_1(player) ||
                player.actions.delay == 0)
            {
                var_1 = ovr025.clear_actions(player);
            }
            else
            {
                var_1 = ovr025.guarding(player);
            }

            return var_1;
        }


        internal static bool sub_36269(Player arg_0)
        {
            byte var_6;
            Player player_ptr;
            bool var_1;

            var_1 = false;

            if (seg049.KEYPRESSED() == true)
            {
                var_6 = seg043.debug_txt();

                if (var_6 == 0)
                {
                    var_6 = seg043.debug_txt();
                }

                if (var_6 == 0x32)
                {
                    gbl.byte_1D904 = !gbl.byte_1D904;

                    if (gbl.byte_1D904 == true)
                    {
                        ovr025.string_print01("Magic On");
                    }
                    else
                    {
                        ovr025.string_print01("Magic Off");
                    }
                }
                else if (var_6 == 0x20)
                {
                    player_ptr = gbl.player_next_ptr;

                    while (player_ptr != null)
                    {
                        if (player_ptr.field_F7 < 0x80 &&
                            player_ptr.health_status != Status.animated)
                        {
                            player_ptr.field_198 = 0;
                        }

                        player_ptr = player_ptr.next_player;
                    }

                    if (arg_0.field_198 == 0)
                    {
                        arg_0.actions.delay = 0x14;
                        var_1 = true;
                    }
                }
                else if (var_6 == 0x2d)
                {
                    ovr014.god_intervene();
                }
            }

            seg043.clear_keyboard();

            return var_1;
        }


        internal static bool sub_3637F(Player player)
        {
            byte var_3;
            byte var_2;
            bool var_1;

            var_1 = false;
            player.actions.field_14 = 0;

            ovr024.sub_6460D(player);

            if (player.actions.field_10 != 0)
            {
                player.actions.field_14 = 1;
                ovr025.sub_67788(1, 10, "is forced to flee", player);
            }
            else if (player.field_F7 > 0x7F)
            {

                gbl.byte_1D2CC = (byte)((player.field_F7 & 0x7F) << 1);

                if (gbl.byte_1D2CC > 0x66)
                {
                    gbl.byte_1D2CC = 0;
                }
                ovr024.work_on_00(player, 0x11);
                throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                throw new System.NotSupportedException();//mov	al, es:[di+charStruct.hit_point_max]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//mov	cx, ax
                throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                throw new System.NotSupportedException();//mov	al, es:[di+charStruct.hit_point_current]
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//mov	dx, 0x64
                throw new System.NotSupportedException();//mul	dx
                throw new System.NotSupportedException();//cwd
                throw new System.NotSupportedException();//div	cx
                throw new System.NotSupportedException();//mov	dx, ax
                throw new System.NotSupportedException();//mov	ax, 0x64
                throw new System.NotSupportedException();//sub	ax, dx
                throw new System.NotSupportedException();//mov	dx, ax
                throw new System.NotSupportedException();//mov	al, byte_1D2CC
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//cmp	ax, dx
                throw new System.NotSupportedException();//jl	loc_36452
                throw new System.NotSupportedException();//cmp	byte_1D2CC, 0
                throw new System.NotSupportedException();//jz	loc_36452
                throw new System.NotSupportedException();//jmp	func_end
                throw new System.NotSupportedException();//loc_36452:
                var_3 = gbl.byte_1D2CC;
                gbl.byte_1D2CC = gbl.byte_1D903;

                ovr024.work_on_00(player, 17);
                throw new System.NotSupportedException();//mov	ax, 0x64
                throw new System.NotSupportedException();//les	di, int ptr area2_ptr.offset
                throw new System.NotSupportedException();//sub	ax, es:[di+area2.field_58C]
                throw new System.NotSupportedException();//mov	dx, ax
                throw new System.NotSupportedException();//mov	al, byte_1D2CC
                throw new System.NotSupportedException();//xor	ah, ah
                throw new System.NotSupportedException();//cmp	ax, dx
                throw new System.NotSupportedException();//jb	loc_36498
                throw new System.NotSupportedException();//cmp	byte_1D2CC, 0
                throw new System.NotSupportedException();//jz	loc_36498
                throw new System.NotSupportedException();//les	di, int ptr [bp+player.offset]
                throw new System.NotSupportedException();//cmp	es:[di+charStruct.combat_team],	0
                throw new System.NotSupportedException();//jz	loc_36498
                throw new System.NotSupportedException();//jmp	func_end
                throw new System.NotSupportedException();//loc_36498:

                var_2 = ovr014.sub_40E8F(player);

                if (var_2 <= (ovr014.sub_3E124(player) >> 1))
                {
                    player.actions.field_14 = 1;
                    ovr024.remove_affect(null, Affects.affect_4a, player);
                    ovr024.remove_affect(null, Affects.affect_4b, player);
                }
                else if (player._int > 5)
                {
                    ovr024.sub_644A7("Surrenders", Status.unconscious, player);

                    var_1 = ovr025.clear_actions(player);
                }
            }
            throw new System.NotSupportedException();//func_end:

            return var_1;
        }


        internal static byte sub_36535(Item arg_0, Player arg_4)
        {
            Struct_1C020 var_12;

            byte var_2;

            var_12 = gbl.unk_1C020[arg_0.type].ShallowClone();

            var_2 = (byte)(var_12.field_A * var_12.field_9);

            if (arg_0.exp_value > 0)
            {
                var_2 += (byte)(arg_0.exp_value << 3);
            }

            if (var_12.field_B > 0)
            {
                var_2 += (byte)(var_12.field_B << 1);
            }

            if (arg_0.type == 0x55 &&
                arg_4.actions.target != null &&
                arg_4.actions.target.field_E9 > 0)
            {
                var_2 = 8;
            }

            if ((var_12.field_E & 8) > 0)
            {
                var_2 += (byte)((var_12.field_5 - 1) << 1);
            }

            if (var_12.field_1 <= 1)
            {
                var_2 += 3;
            }

            if ((var_12.field_1 + arg_4.field_185) > 3)
            {
                var_2 = 0;
            }

            if (arg_0.affect_3 == Affects.affect_84 &&
                ((int)arg_0.affect_2 & 0x0f) != arg_4.alignment)
            {
                var_2 = 0;
            }

            if (arg_0.affect_2 == Affects.affect_53)
            {
                var_2 = 0;
            }

            if (arg_0.field_36 != 0)
            {
                var_2 = 0;
            }

            return var_2;
        }


        internal static void sub_36673(Player arg_0)
        {
            byte var_1F;
            byte var_1E;
            byte var_1D;
            byte var_1C;
            byte var_1A;
            byte var_19;
            byte var_18;
            byte var_17;
            byte var_16;
            byte var_15;
            Item var_14;
            Item var_10;
            Item var_C;
            Item var_8;
            Item var_4;

            if (arg_0.field_151 != null)
            {
                arg_0.field_185 -= gbl.unk_1C020[arg_0.field_151.type].field_1;
            }

            if (arg_0.field_155 != null)
            {
                arg_0.field_185 -= gbl.unk_1C020[arg_0.field_155.type].field_1;
            }

            var_4 = null;
            var_8 = null;
            var_C = null;
            var_15 = 1;

            var_16 = (byte)(arg_0.field_120 * arg_0.field_11E);

            if (arg_0.field_122 > 0)
            {
                var_16 += (byte)(arg_0.field_122 * 2);
            }

            var_17 = 0;

            var_10 = arg_0.itemsPtr;

            while (var_10 != null)
            {
                var_19 = var_10.type;

                if (gbl.unk_1C020[var_19].field_0 == 0 &&
                    (gbl.unk_1C020[var_19].field_D & arg_0.field_12B) != 0)
                {
                    var_18 = sub_36535(var_10, arg_0);

                    if ((gbl.unk_1C020[var_19].field_E & 8) != 0 ||
                        (gbl.unk_1C020[var_19].field_E & 0x10) != 0)
                    {
                        if (var_18 > var_15)
                        {
                            var_4 = var_10;
                            var_15 = var_18;
                        }
                    }

                    if ((gbl.unk_1C020[var_19].field_E & 8) == 0 &&
                        var_18 > var_16)
                    {
                        var_8 = var_10;
                        var_16 = var_18;
                    }
                }

                throw new System.NotSupportedException();//cmp	gbl.unk_1C020[ var_19 ].field_0, 1
                throw new System.NotSupportedException();//jnz	loc_36898
                throw new System.NotSupportedException();//mov	al, gbl.unk_1C020[ var_19 ].field_D
                throw new System.NotSupportedException();//les	di, [bp+arg_0]
                throw new System.NotSupportedException();//and	al, es:[di+12Bh]
                throw new System.NotSupportedException();//or	al, al
                throw new System.NotSupportedException();//jbe	loc_36898
                throw new System.NotSupportedException();//les	di, [bp+var_10]
                throw new System.NotSupportedException();//cmp	byte ptr es:[di+32h], 0
                throw new System.NotSupportedException();//jl	loc_3687A
                throw new System.NotSupportedException();//les	di, [bp+var_10]
                throw new System.NotSupportedException();//mov	al, es:[di+32h]
                throw new System.NotSupportedException();//cbw
                throw new System.NotSupportedException();//inc	ax
                throw new System.NotSupportedException();//mov	[bp+var_18], al
                throw new System.NotSupportedException();//jmp	short loc_3687E
                throw new System.NotSupportedException();//loc_3687A:
                var_18 = 0;
                throw new System.NotSupportedException();//loc_3687E:
                if (var_18 > var_17)
                {
                    var_C = var_10;
                    var_17 = var_18;
                }

                throw new System.NotSupportedException();//loc_36898:
                var_10 = var_10.next;
            }

            if (var_4 != null &&
                gbl.unk_1C020[var_4.type].field_C > 1 &&
                (gbl.unk_1C020[var_4.type].field_E & 0x14) == 0x14)
            {
                var_1E = 1;
            }
            else
            {
                var_1E = 0;
            }

            var_1F = 0;
            var_14 = null;
            var_1A = 0;

            if (var_4 != null)
            {
                var_1A = gbl.unk_1C020[var_4.type].field_E;

                if ((var_1A & 0x10) != 0)
                {
                    var_14 = var_4;
                }

                if ((var_1A & 8) != 0)
                {
                    if ((var_1A & 0x01) != 0)
                    {
                        var_14 = arg_0.Item_ptr_03;
                    }

                    if ((var_1A & 0x80) != 0)
                    {
                        var_14 = arg_0.Item_ptr_04;
                    }
                }
            }

            if (var_14 != null ||
                var_1A == 10)
            {
                var_1F = 1;
            }


            if (var_4 != null &&
                var_15 > (var_16 >> 1) &&
                var_1F != 0 &&
                (var_1E != 0 || ovr025.near_enermy(1, arg_0) == 0))
            {
                var_10 = var_4;
            }
            else
            {
                var_10 = var_8;
            }

            var_1D = 0;
            var_1C = 1;

            if (arg_0.field_151 != null &&
                (arg_0.field_151 == var_10 ||
                 arg_0.field_151.field_36 != 0))
            {
                var_1C = 0;
            }

            if (var_1C != 0)
            {
                if (arg_0.field_151 != null)
                {
                    ovr020.ready_Item(arg_0.field_151);
                }

                ovr025.sub_66C20(arg_0);

                if (arg_0.field_155 != null &&
                    arg_0.field_155.field_36 == 0)
                {
                    arg_0.field_185 -= gbl.unk_1C020[arg_0.field_155.type].field_1;
                }

                if (var_10 != null)
                {
                    ovr020.ready_Item(var_10);
                }

                var_1D = 1;
            }

            ovr025.sub_66C20(arg_0);
            ovr014.sub_3EDD4(arg_0);
            var_1C = 1;

            if (arg_0.field_155 != null &&
                (arg_0.field_155 == var_C || arg_0.field_155.field_36 != 0))
            {
                var_1C = 0;
            }
            throw new System.NotSupportedException();//les	di, [bp+arg_0]
            throw new System.NotSupportedException();//cmp	byte ptr es:[di+185h], 2
            throw new System.NotSupportedException();//jbe	loc_36B55
            throw new System.NotSupportedException();//les	di, [bp+arg_0]
            throw new System.NotSupportedException();//mov	ax, es:[di+155h]
            throw new System.NotSupportedException();//or	ax, es:[di+157h]
            throw new System.NotSupportedException();//jz	loc_36B2C
            throw new System.NotSupportedException();//les	di, [bp+arg_0]
            throw new System.NotSupportedException();//les	di, es:[di+155h]
            throw new System.NotSupportedException();//cmp	byte ptr es:[di+36h], 0
            throw new System.NotSupportedException();//jz	loc_36B3D
            throw new System.NotSupportedException();//loc_36B2C:
            ovr020.ready_Item(var_10);
            var_1D = 1;
            throw new System.NotSupportedException();//jmp	short loc_36B53
            throw new System.NotSupportedException();//loc_36B3D:
            ovr020.ready_Item(arg_0.field_155);
            var_1D = 1;
            throw new System.NotSupportedException();//loc_36B53:
            throw new System.NotSupportedException();//jmp	short loc_36BA9
            throw new System.NotSupportedException();//loc_36B55:
            throw new System.NotSupportedException();//les	di, [bp+arg_0]
            throw new System.NotSupportedException();//cmp	byte ptr es:[di+185h], 2
            throw new System.NotSupportedException();//jnb	loc_36BA9
            throw new System.NotSupportedException();//cmp	[bp+var_1C], 0
            throw new System.NotSupportedException();//jz	loc_36BA9
            if (arg_0.field_155 != null)
            {
                ovr020.ready_Item(arg_0.field_155);
            }
            ovr025.sub_66C20(arg_0);

            if (var_C != null)
            {
                ovr020.ready_Item(var_C);
            }

            var_1D = 1;
            throw new System.NotSupportedException();//loc_36BA9:

            ovr025.sub_66C20(arg_0);

            if (var_1D != 0)
            {
                ovr025.hitpoint_ac(arg_0);
            }
        }
    }
}
