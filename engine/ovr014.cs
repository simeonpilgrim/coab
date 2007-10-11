using Classes;

namespace engine
{
    class ovr014
    {
        internal static void sub_3E000(Player player)
        {
            Action action;

            action = player.actions;

            action.spell_id = 0;
            action.can_cast = 1;
            action.field_2 = 1;
            action.field_8 = 0;
            action.field_4 = 2;

            sub_3EDD4(player);
            gbl.byte_1D2C0 = player.field_11D;
            gbl.byte_1D2C4 = 0;

            ovr024.work_on_00(player, 18);

            player.field_19D = sub_3EF0D(gbl.byte_1D2C0);

            action.field_5 = player.field_DD;

            if (player.in_combat == true)
            {
                action.delay = (sbyte)(ovr024.roll_dice(6, 1) + ovr025.stat_bonus02(player));

                if (action.delay < 1)
                {
                    action.delay = 1;
                }

                if (((player.combat_team + 1) & gbl.area2_ptr.field_596) != 0)
                {
                    action.delay -= 6;
                }

                if (action.delay < 0 ||
                    action.delay > 0x14)
                {
                    action.delay = 0;
                }
            }
            else
            {
                action.delay = 0;
            }

            player.actions.move = sub_3E124(player);
        }


        internal static byte sub_3E124(Player player)
        {
            sbyte var_2;
            byte var_1;

            var_2 = (sbyte)player.initiative;

            if (player.in_combat == false)
            {
                var_2 += (sbyte)gbl.area2_ptr.field_6E4;
            }

            gbl.byte_1D2C4 = 1;

            if (var_2 < 1 || var_2 > 0x60)
            {
                var_2 = 1;
            }

            gbl.byte_1D2C0 = (byte)(var_2 << 1);

            ovr024.work_on_00(player, 18);
            gbl.byte_1D2C4 = 0;
            var_1 = gbl.byte_1D2C0;

            return var_1;
        }


        static void sub_3E192(byte arg_0, Player arg_2, Player arg_6)
        {
            gbl.byte_1D2BE = (byte)ovr024.roll_dice_save(arg_6.field_19FArray(arg_0), (sbyte)arg_6.field_19DArray(arg_0));
            gbl.byte_1D2BE += arg_6.field_1A1Array(arg_0);

            if (gbl.byte_1D2BE < 0)
            {
                gbl.byte_1D2BE = 0;
            }

            if (sub_408D7(arg_2, arg_6) != 0)
            {
                gbl.byte_1D2BE *= (byte)((((arg_6.thief_lvl + (arg_6.field_117 * ovr026.sub_6B3D1(arg_6))) - 1) / 4) + 2);
            }

            gbl.byte_1D2BF = 0;
            ovr024.work_on_00(arg_6, 4);
            ovr024.work_on_00(arg_2, 5);
        }

        static Set unk_3E2EE = new Set(0x0002, new byte[] { 0xC0, 0x01 });

        static void backstab(bool attackHits, byte attackDamge, byte actualDamage, byte arg_6, Player target, Player attacker)
        {
            string text;
            byte var_1;

            if (arg_6 == 2)
            {
                text = "-Backstabs-";
            }
            else if (arg_6 == 3)
            {
                text = "slays	helpless";
            }
            else
            {
                text = "Attacks";
            }

            ovr025.DisplayPlayerStatusString(false, 10, text, attacker);
            var_1 = 0x0C;

            ovr025.displayPlayerName(false, var_1, 0x17, target);
            var_1++;

            if (arg_6 == 1)
            {
                text = "(from behind) ";
            }
            else
            {
                text = string.Empty;
            }

            if (attackHits == true)
            {
                if (arg_6 == 3)
                {
                    text = "with one cruel blow";
                }
                else
                {
                    text += "Hitting for " + attackDamge.ToString();

                    if (attackDamge == 1)
                    {
                        text += " point ";
                    }
                    else
                    {
                        text += " points ";
                    }

                    text += "of damage";

                }

                ovr025.damage_player(actualDamage, target);
            }
            else
            {
                text += "and Misses";
            }

            if (target.health_status != Status.gone)
            {
                seg041.press_any_key(text, true, 0, 10, (byte)(var_1 + 3), 0x26, var_1, 0x17);
            }

            var_1 = (byte)(gbl.textYCol + 1);

            if (actualDamage > 0)
            {
                target.actions.can_cast = 0;
                if (target.actions.spell_id > 0)
                {
                    seg041.GameDelay();

                    ovr025.DisplayPlayerStatusString(true, 12, "lost a spell", target);

                    ovr025.clear_spell(target.actions.spell_id, target);
                    target.actions.spell_id = 0;
                }
                else
                {
                    seg041.GameDelay();
                }
            }
            else
            {
                seg041.GameDelay();
            }

            if (target.in_combat == false)
            {
                ovr025.DisplayPlayerStatusString(false, var_1, "goes down", target);
                var_1 += 2;

                if (target.health_status == Status.dying)
                {
                    seg041.displayString("and is Dying", 0, 10, var_1, 0x17);
                }

                if (unk_3E2EE.MemberOf((byte)target.health_status) == true)
                {
                    ovr025.DisplayPlayerStatusString(false, var_1, "is killed", target);
                }

                var_1 += 2;

                ovr024.sub_645AB(target);

                ovr024.work_on_00(target, 13);

                if (target.in_combat == false)
                {
                    ovr033.sub_74E6F(target);
                }
                else
                {
                    seg041.GameDelay();
                }
            }

            ovr025.ClearPlayerTextArea();
        }


        static void sub_3E65D(Player arg_0)
        {
            byte var_8;
            bool var_7;
            Player var_6;
            byte var_2;
            byte var_1;

            var_1 = ovr025.near_enermy(1, arg_0);
            var_8 = var_1;

            for (var_2 = 1; var_2 <= var_8; var_2++)
            {
                if (arg_0.in_combat == true)
                {
                    var_6 = gbl.player_array[gbl.byte_1D8B9[var_2]];

                    if (var_6.actions.guarding == true &&
                        ovr025.is_held(var_6) == false)
                    {
                        ovr033.sub_749DD(8, 2, ovr033.PlayerMapYPos(arg_0), ovr033.PlayerMapXPos(arg_0));

                        var_6.actions.guarding = false;

                        sub_3F94D(arg_0, var_6);
                        sub_3F9DB(out var_7, null, 0, arg_0, var_6);
                    }
                }
            }
        }


        internal static void sub_3E748(byte direction, Player player)
        {
            byte var_1;

            byte player_index = ovr033.get_player_index(player);

            sbyte oldXPos = (sbyte)gbl.stru_1C9CD[player_index].xPos;
            sbyte oldYPos = (sbyte)gbl.stru_1C9CD[player_index].yPos;

            sbyte newXPos = (sbyte)(oldXPos + gbl.MapDirectionXDelta[direction]);
            sbyte newYPos = (sbyte)(oldYPos + gbl.MapDirectionYDelta[direction]);

            int costToMove = 0;
            if ((direction & 0x01) != 0)
            {
                // Diagonal walking...
                costToMove = gbl.unk_189B4[gbl.stru_1D1BC[newXPos, newYPos]].move_cost * 3;
            }
            else
            {
                costToMove = gbl.unk_189B4[gbl.stru_1D1BC[newXPos, newYPos]].move_cost * 2;
            }

            if (costToMove > player.actions.move)
            {
                player.actions.move = 0;
            }
            else
            {
                player.actions.move -= (byte)costToMove;
            }

            var_1 = 1;

            if (player.field_198 != 0)
            {
                var_1 = 3;

                if (ovr033.CoordOnScreen(newYPos - gbl.stru_1D1BC.mapScreenTopY, newXPos - gbl.stru_1D1BC.mapScreenLeftX) == false &&
                    gbl.byte_1D910 == true)
                {
                    ovr033.sub_749DD(8, 2, oldYPos, oldXPos);
                }
            }

            if (gbl.byte_1D910 == true)
            {
                ovr033.sub_74572(player_index, 0, 0);
            }

            gbl.stru_1C9CD[player_index].xPos = newXPos;
            gbl.stru_1C9CD[player_index].yPos = newYPos;

            ovr033.sub_743E7();

            if (gbl.byte_1D910 == true)
            {
                ovr033.sub_749DD(8, var_1, newYPos, newXPos);
            }

            player.actions.field_F = 0;
            player.actions.field_12 = 0;
            seg044.sound_sub_120E0(gbl.word_188D2);

            sub_3E65D(player);

            if (player.in_combat == false ||
                ovr025.is_held(player) == true)
            {
                player.actions.move = 0;
            }

        }


        internal static void sub_3E954(byte arg_0, Player player)
        {
            int var_25;
            Player var_23;
            Player player_ptr;
            int var_1B;
            byte var_1A;
            byte var_18;
            byte var_17;
            byte var_16;
            byte var_15;
            byte player_index;
            byte var_12;
            byte var_11;
            Affect var_10;
            byte[] var_D = new byte[12 + 1];


            seg051.FillChar(0, 12 + 1, var_D);

            player_index = ovr033.get_player_index(player);
            var_15 = ovr025.near_enermy(1, player);

            if (var_15 != 0)
            {
                for (var_18 = 1; var_18 <= var_15; var_18++)
                {
                    var_D[var_18] = gbl.byte_1D8B9[var_18];
                }

                gbl.stru_1C9CD[player_index].xPos += gbl.MapDirectionXDelta[arg_0];
                gbl.stru_1C9CD[player_index].yPos += gbl.MapDirectionYDelta[arg_0];

                var_16 = ovr025.near_enermy(1, player);

                gbl.stru_1C9CD[player_index].xPos -= gbl.MapDirectionXDelta[arg_0];
                gbl.stru_1C9CD[player_index].yPos -= gbl.MapDirectionYDelta[arg_0];

                for (var_18 = 1; var_18 <= var_15; var_18++)
                {
                    var_11 = 0;

                    for (var_17 = 1; var_17 <= var_16; var_17++)
                    {
                        if (gbl.byte_1D8B9[var_17] == var_D[var_18])
                        {
                            var_11 = 1;
                        }
                    }

                    if (var_11 != 0)
                    {
                        var_D[var_18] = 0;
                    }
                }

                for (var_18 = 1; var_18 <= var_15; var_18++)
                {
                    if (var_D[var_18] > 0 &&
                        player.in_combat == true)
                    {
                        gbl.byte_1D90F = true;
                        gbl.byte_1D910 = true;
                        var_12 = 0;

                        player_ptr = gbl.player_array[var_D[var_18]];

                        if (ovr025.is_held(player_ptr) == false &&
                            sub_3F143(player, player_ptr) == true &&
                            ovr025.find_affect(out var_10, Affects.affect_4b, player_ptr) == false &&
                            ovr025.find_affect(out var_10, Affects.affect_4a, player_ptr) == false)
                        {
                            var_25 = player_ptr.actions.field_9 + 10;

                            for (var_1B = player_ptr.actions.field_9 + 6; var_1B <= var_25; var_1B++)
                            {
                                if (var_12 != 0)
                                {
                                    if (player_ptr.actions.delay > 0 ||
                                        player_ptr.actions.field_F == 0 ||
                                        ovr032.sub_7354A((byte)(var_1B % 8), ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player), ovr033.PlayerMapYPos(player_ptr), ovr033.PlayerMapXPos(player_ptr)) == true)
                                    {
                                        var_1A = 1;
                                        if (player_ptr.field_11C == 0)
                                        {
                                            var_1A = 2;
                                        }

                                        if (player_ptr.field_19C > 0)
                                        {
                                            var_1A = 1;
                                        }

                                        if (player_ptr.field_19D > 0)
                                        {
                                            var_1A = 2;
                                        }

                                        if (var_1A == 1 && player_ptr.field_19C != 0)
                                        {
                                            player_ptr.field_19C = 1;
                                        }

                                        if (var_1A == 2 && player_ptr.field_19D != 0)
                                        {
                                            player_ptr.field_19D = 1;
                                        }

                                        player_ptr.actions.field_4 = var_1A;

                                        var_23 = player_ptr.actions.target;

                                        sub_3F9DB(out gbl.byte_1D905, null, 1, player, player_ptr);
                                        var_12 = 1;

                                        player_ptr.actions.target = var_23;

                                        if (player.in_combat == true)
                                        {
                                            gbl.byte_1D90F = true;
                                            ovr025.hitpoint_ac(player);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        internal static bool flee_battle(Player player)
        {
            byte var_4;
            byte var_3;
            bool gets_away;
            bool ret_val;

            gets_away = false;

            if (ovr025.near_enermy(0xff, player) == 0)
            {
                gets_away = true;
            }
            else
            {
                var_4 = (byte)(sub_3E124(player) >> 1);
                var_3 = sub_40E8F(player);

                if (var_3 < var_4)
                {
                    gets_away = true;
                }
                else if (var_3 == var_4 && ovr024.roll_dice(2, 1) == 1)
                {
                    gets_away = true;
                }
            }

            if (gets_away == true)
            {

                ovr024.sub_644A7("Got Away", Status.running, player);
            }
            else
            {
                ovr025.string_print01("Escape is blocked");
            }

            ret_val = ovr025.clear_actions(player);

            return ret_val;
        }


        internal static void sub_3EDD4(Player arg_0)
        {
            Item var_9;
            bool var_5;
            byte var_4;
            byte var_3;
            byte var_2;
            byte var_1;

            var_5 = false;
            var_9 = null;
            var_2 = arg_0.field_19C;
            arg_0.field_19C = arg_0.field_11C;

            if (ovr025.offset_above_1(arg_0) == true &&
                ovr025.sub_6906C(out var_9, arg_0) == true)
            {
                var_5 = true;
                var_4 = gbl.unk_1C020[arg_0.field_151.type].field_5;

                if (var_4 < 2)
                {
                    var_4 = 2;
                }

                gbl.byte_1D2C0 = var_4;
            }
            else
            {
                gbl.byte_1D2C0 = arg_0.field_19C;
            }

            gbl.byte_1D2C4 = 0;
            ovr024.work_on_00(arg_0, 18);
            var_1 = sub_3EF0D(gbl.byte_1D2C0);

            if (var_5 == true &&
                var_9 != null)
            {
                var_3 = 1;
                if (var_9 != null &&
                    var_9.count > var_3)
                {
                    var_3 = var_9.count;
                }

                if (var_3 < var_1 &&
                    var_9.count > 0)
                {
                    var_1 = var_3;
                }
            }

            if (arg_0.actions.field_8 == 0 ||
                var_1 < var_2 ||
                (arg_0.actions.field_8 != 0 &&
                  var_1 < (var_2 * 2) &&
                  var_5 == false))
            {
                arg_0.field_19C = var_1;
            }
        }


        internal static byte sub_3EF0D(byte arg_0)
        {
            byte var_2;

            var_2 = arg_0;

            if ((gbl.byte_1D8B7 >> 2) < 1)
            {
                var_2++;
            }

            return (byte)(var_2 >> 1);
        }


        internal static bool sub_3EF3D(Player arg_0, Player arg_4)
        {
            byte var_A;
            Player var_9;
            byte var_5;
            byte var_4;
            byte var_3;
            byte var_2;
            bool var_1;

            var_1 = false;

            if (arg_4.field_19C < arg_4.actions.field_5 &&
                arg_0.field_E5 == 0)
            {
                if (ovr025.sub_68708(arg_0, arg_4) == 1)
                {
                    var_2 = ovr025.near_enermy(1, arg_4);
                    var_3 = 0;
                    var_A = var_2;

                    var_5 = 0xff;

                    for (var_4 = 1; var_4 <= var_A; var_4++)
                    {
                        var_9 = gbl.player_array[gbl.byte_1D8B9[var_4]];

                        if (var_9 == arg_0)
                        {
                            var_5 = var_4;
                        }

                        if (var_9.field_E5 == 0)
                        {
                            var_3++;
                        }
                    }

                    if (var_3 > arg_4.field_19C)
                    {
                        if (var_3 > arg_4.actions.field_5)
                        {
                            var_3 = arg_4.actions.field_5;
                        }

                        ovr025.DisplayPlayerStatusString(true, 10, "sweeps", arg_4);

                        var_9 = gbl.player_array[gbl.byte_1D8B9[1]];

                        if (var_9 != arg_0)
                        {
                            gbl.byte_1D8B9[var_5] = gbl.byte_1D8B9[1];
                            gbl.byte_1D8B9[1] = ovr033.get_player_index(arg_0);
                        }

                        var_A = var_2;

                        for (var_4 = 1; var_4 <= var_A; var_4++)
                        {
                            if (var_3 > 0 &&
                                gbl.player_array[gbl.byte_1D8B9[var_4]].field_E5 == 0)
                            {
                                var_9 = gbl.player_array[gbl.byte_1D8B9[var_4]];

                                sub_3F94D(var_9, arg_4);

                                arg_4.field_19C = 1;

                                sub_3F9DB(out gbl.byte_1D905, null, 0, var_9, arg_4);
                                var_3--;
                            }
                        }
                        var_1 = true;
                    }
                }
            }

            return var_1;
        }


        internal static bool sub_3F143(Player player01, Player player02)
        {
            Player old_target;
            bool ret_val;

            ret_val = false;

            if (player01 != null)
            {
                if (player02 == player01)
                {
                    ret_val = true;
                }
                else
                {
                    gbl.byte_1D2C5 = 0;

                    ovr024.work_on_00(player01, 1);

                    if (gbl.byte_1D2C5 == 0)
                    {
                        old_target = player02.actions.target;

                        player02.actions.target = player01;

                        ovr024.work_on_00(player02, 0);

                        player02.actions.target = old_target;
                    }

                    ret_val = (gbl.byte_1D2C5 == 0);
                }
            }

            return ret_val;
        }

        static sbyte[] /*seg600:0369*/ unk_16679 = { 0, 0x11, 0x11, 0, 0, 1, 0x11, 0, 0, 0x20, 0x20, 0x0A, 7 };

        internal static void turns_undead(Player player)
        {
            byte var_B;
            Player var_A;
            byte var_6;
            byte var_5;
            sbyte var_4;
            byte var_3;
            byte var_2;
            byte var_1;

            ovr025.DisplayPlayerStatusString(false, 10, "turns undead...", player);
            ovr027.redraw_screen();
            seg041.GameDelay();
            var_5 = 0;
            var_6 = 0;

            player.actions.field_11 = 1;

            var_3 = 6;
            var_2 = ovr024.roll_dice(12, 1);

            var_1 = ovr024.roll_dice(20, 1);


            sbyte al = (sbyte)((ovr026.sub_6B3D1(player) * player.turn_undead) + player.cleric_lvl);

            if (al >= 1 && al <= 8)
            {
                var_B = player.cleric_lvl;
            }
            else if (al >= 9 && al <= 0x0d)
            {
                var_B = 9;
            }
            else
            {
                var_B = 0x0A;
            }

            while (sub_3F433(out var_A, player) == true && var_2 > 0 && var_6 == 0)
            {
                var_4 = unk_16679[(var_A.field_E9 * 10) + var_B];

                if (var_1 >= System.Math.Abs(var_4))
                {
                    var_5 = 1;

                    ovr033.sub_75356(false, 3, var_A);
                    gbl.byte_1D90F = true;
                    ovr025.hitpoint_ac(var_A);

                    if (var_4 > 0)
                    {
                        var_A.actions.field_10 = 1;
                        ovr025.sub_6818A("is	turned", 1, var_A);
                    }
                    else
                    {
                        ovr025.DisplayPlayerStatusString(false, 10, "Is destroyed", var_A);
                        ovr033.sub_74E6F(var_A);
                        var_A.health_status = Status.gone;
                        var_A.in_combat = false;
                    }

                    if (var_3 > 0)
                    {
                        var_3 -= 1;
                    }

                    var_2 -= 1;

                    if (var_2 == 0 && var_3 > 0 && var_4 < 0)
                    {
                        var_2++;
                    }

                    ovr025.ClearPlayerTextArea();
                }
                else
                {
                    var_6 = 1;
                }
            }

            if (var_5 == 0)
            {
                ovr025.string_print01("Nothing Happens...");
            }

            ovr025.count_teams();
            gbl.byte_1D905 = ovr025.clear_actions(player);

            ovr025.ClearPlayerTextArea();
        }


        internal static bool sub_3F433(out Player output, Player player)
        {
            Player player_ptr;
            byte var_4;
            byte var_3;
            byte var_2;
            bool var_1;

            var_4 = 0x0D;
            output = null;
            var_2 = ovr025.near_enermy(0xff, player);
            var_1 = false;

            for (var_3 = 1; var_3 <= var_2; var_3++)
            {
                player_ptr = gbl.player_array[gbl.byte_1D8B9[var_3]];

                if (player_ptr.actions.field_10 == 0 &&
                    player_ptr.field_E9 > 0 &&
                    player_ptr.field_E9 < var_4)
                {
                    var_4 = player_ptr.field_E9;
                    output = player_ptr;
                    var_1 = true;
                }
            }

            return var_1;
        }


        internal static void sub_3F4EB(Item arg_0, ref bool arg_4, byte arg_8, Player arg_A, Player arg_E)
        {
            byte var_17;
            byte var_16;
            byte var_15;
            byte var_14;
            byte var_13;
            byte var_12;
            byte var_11;
            Struct_1C020 var_10;

            var_13 = arg_8;
            arg_4 = false;
            gbl.byte_1D2CA = 0;
            gbl.byte_1D2CB = 0;
            gbl.byte_1D901 = 0;
            gbl.byte_1D902 = 0;
            var_11 = 0;
            var_12 = 0;
            gbl.byte_1D2BE = 0;

            arg_E.actions.field_8 = 1;

            if (ovr025.is_held(arg_A) == true)
            {
                seg044.sound_sub_120E0(gbl.word_188CC);

                while (arg_E.field_19BArray(arg_E.actions.field_4) == 0)
                {
                    arg_E.actions.field_4--;
                }

                switch (arg_E.actions.field_4)
                {
                    case 1:
                        gbl.byte_1D901 += 1;
                        break;
                    case 2:
                        gbl.byte_1D902 += 1;
                        break;
                    default:
                        /* byte_1D90x += 1; */
                        throw new System.NotImplementedException();
                }

                backstab(true, 1, (byte)(arg_A.hit_point_current + 5), 3, arg_A, arg_E);
                ovr024.remove_affect_19(arg_E);

                arg_E.field_19C = 0;
                arg_E.field_19D = 0;

                var_11 = 1;
                arg_4 = true;
            }
            else
            {
                if (arg_E.field_151 != null && 
                    (arg_A.field_DE > 0x80 || (arg_A.field_DE & 7) > 1))
                {
                    var_10 = gbl.unk_1C020[arg_E.field_151.type].ShallowClone();

                    arg_E.field_19E = var_10.field_2;
                    arg_E.field_1A0 = var_10.field_3;
                    arg_E.field_1A2 = (sbyte)((arg_E.field_1A2 - var_10.field_B) + var_10.field_4);
                }

                ovr025.sub_66C20(arg_A);
                ovr024.work_on_00(arg_A, 11);

                if (sub_408D7(arg_A, arg_E) != 0)
                {
                    var_14 = (byte)(arg_A.field_19B - 4);
                }
                else
                {
                    if (arg_A.actions.field_F > 1 &&
                        sub_409BC(arg_A, arg_E) == arg_A.actions.field_9 &&
                        arg_A.actions.field_12 > 4)
                    {
                        var_13 = 1;
                    }

                    if (var_13 != 0)
                    {
                        var_14 = arg_A.field_19B;
                    }
                    else
                    {
                        var_14 = arg_A.ac;
                    }
                }

                sub_3FCED(ref var_14, arg_A, arg_E);
                var_17 = 0;
                if (var_13 != 0)
                {
                    var_17 = 1;
                }

                if (sub_408D7(arg_A, arg_E) != 0)
                {
                    var_17 = 2;
                }

                var_16 = arg_E.actions.field_4;
                for (var_15 = var_16; var_15 >= 1; var_15--)
                {
                    while (arg_E.field_19BArray(var_15) > 0 &&
                        var_12 == 0)
                    {
                        arg_E.field_19BArraySet(var_15, (byte)(arg_E.field_19BArray(var_15) - 1));
                        arg_E.actions.field_4 = var_15;

                        switch (var_15)
                        {
                            case 1:
                                gbl.byte_1D901++;
                                break;

                            case 2:
                                gbl.byte_1D902++;
                                break;

                            default:
                                throw new System.NotImplementedException();
                        }

                        if (ovr024.sub_64245(var_14, arg_A, arg_E) != 0 ||
                            ovr025.is_held(arg_A) == true)
                        {
                            switch (var_15)
                            {
                                case 1:
                                    gbl.byte_1D2CA++;
                                    break;

                                case 2:
                                    gbl.byte_1D2CB++;
                                    break;

                                default:
                                    throw new System.NotImplementedException();
                            }

                            seg044.sound_sub_120E0(gbl.word_188CC);
                            var_11 = 1;
                            sub_3E192(var_15, arg_A, arg_E);
                            backstab(true, gbl.byte_1D2BE, gbl.byte_1D2BE, var_17, arg_A, arg_E);

                            if (arg_A.in_combat == true)
                            {
                                ovr024.work_on_00(arg_E, var_15 + 1);
                            }

                            if (arg_A.in_combat == false)
                            {
                                var_12 = 1;
                            }

                            if (arg_E.in_combat == false)
                            {
                                var_16 = 0;
                                arg_E.field_19BArraySet(var_15, 0);
                            }
                        }
                    }
                }

                if (arg_0 != null && arg_0.count == 0 && arg_0.type == 0x64)
                {
                    arg_E.field_19C = 0;
                    arg_E.field_19D = 0;
                }

                if (var_11 == 0)
                {
                    seg044.sound_sub_120E0(gbl.word_188D0);
                    backstab(false, 0, 0, var_17, arg_A, arg_E);
                }

                arg_4 = true;
                for (var_15 = 1; var_15 <= 2; var_15++)
                {
                    if (arg_E.field_19BArray(var_15) > 0)
                    {
                        arg_4 = false;
                    }
                }
                arg_E.actions.field_5 = 0;
            }

            if (arg_E.in_combat == false)
            {
                arg_4 = true;
            }

            if (arg_4 == true)
            {
                arg_4 = ovr025.clear_actions(arg_E);
            }
        }


        internal static void sub_3F94D(Player target, Player attacker)
        {
            byte var_2;
            byte var_1;

            target.actions.field_F++;

            var_2 = sub_409BC(attacker, target);

            var_1 = (byte)(((var_2 - target.actions.field_9) + 8) % 8);

            if (var_1 > 4)
            {
                var_1 = (byte)(8 - var_1);
            }

            target.actions.field_12 = (byte)((target.actions.field_12 + var_1) % 8);
        }


        internal static void sub_3F9DB(out bool arg_0, Item item, byte arg_8, Player player01, Player player02)
        {
            byte var_9 = 0;
            Player playerBase;
            Item item_ptr;

            gbl.byte_1D910 = true;
            gbl.byte_1D90F = true;

            gbl.byte_1D8B8 = (byte)(gbl.byte_1D8B7 + 15);

            if (player01.actions.field_F < 2 &&
                arg_8 == 0)
            {
                var_9 = sub_409BC(player02, player01);

                player01.actions.field_9 = (byte)((var_9 + 4) % 8);
            }
            else if (ovr033.sub_74761(0, player01) == true)
            {
                var_9 = player01.actions.field_9;

                if (arg_8 == 0)
                {
                    player01.actions.field_9 = (byte)((var_9 + 4) % 8);
                }
            }

            if (ovr033.sub_74761(0, player01) == true)
            {
                ovr033.sub_74B3F(0, 0, var_9, player01);
            }

            var_9 = sub_409BC(player01, player02);
            ovr025.hitpoint_ac(player02);

            ovr033.sub_74B3F(0, 1, var_9, player02);

            player02.actions.target = player01;

            seg049.SysDelay(100);

            if (item != null)
            {
                sub_40BF1(item, player01, player02);
            }

            if (player02.field_151 != null && /* added to get passed this point when null, why null, should 151 ever be null? */
                (player02.field_151.type == 0x2f ||
                player02.field_151.type == 0x65))
            {
                sub_40BF1(player02.field_151, player01, player02);
            }

            arg_0 = true;

            if (player02.field_19C > 0 ||
                player02.field_19D > 0)
            {
                playerBase = gbl.player_ptr;

                gbl.player_ptr = player02;

                sub_3F4EB(item, ref arg_0, arg_8, player01, player02);

                if (item != null)
                {
                    if (item.count > 0)
                    {
                        item.count = gbl.byte_1D901;
                    }

                    if (item.count == 0)
                    {
                        if (ovr025.offset_equals_20(player02) == true &&
                            item.affect_3 != Affects.affect_89)
                        {
                            item_ptr = item.ShallowClone();

                            item_ptr.next = gbl.item_pointer;

                            gbl.item_pointer = item_ptr;

                            item_ptr.readied = false;

                            ovr025.lose_item(item, player02);
                            gbl.item_ptr = item_ptr;
                        }
                        else
                        {
                            ovr025.lose_item(item, player02);
                        }
                    }
                }

                ovr025.sub_66C20(player02);
                gbl.player_ptr = playerBase;
            }

            if (arg_0 == true)
            {
                arg_0 = ovr025.clear_actions(player02);
            }

            if (ovr033.sub_74761(0, player02) == true)
            {
                ovr033.sub_74B3F(1, 1, player02.actions.field_9, player02);
                ovr033.sub_74B3F(0, 0, player02.actions.field_9, player02);
            }
        }


        internal static void sub_3FCED(ref byte arg_0, Player arg_4, Player arg_8)
        {
            int var_1;

            int var_2 = ovr025.sub_68708(arg_4, arg_8);

            if (ovr025.offset_above_1(arg_8) == true)
            {
                var_1 = (gbl.unk_1C020[arg_8.field_151.type].field_C - 1) / 3;
            }
            else
            {
                var_1 = var_2;
            }

            if (var_2 > var_1)
            {
                var_2 -= var_1;
                arg_0 += 2;
            }

            if (var_2 > var_1)
            {
                var_2 -= var_1;
                arg_0 += 3;
            }
        }

        static Set word_3FDDE = new Set(0x0002, new byte[] { 0xC0, 0x01 });

        internal static bool sub_3FDFE(out Player arg_0, Player arg_4)
        {
            byte var_D;
            byte var_C;
            byte var_B;
            byte var_A;
            byte var_9;
            byte var_8;
            Player var_5;
            bool var_1;

            var_5 = null;
            var_8 = 0;
            var_9 = 0x0FF;

            for (var_A = 0; var_A <= 8; var_A++)
            {
                int mapX = gbl.MapDirectionXDelta[var_A] + ovr033.PlayerMapXPos(arg_4);
                int mapY = gbl.MapDirectionYDelta[var_A] + ovr033.PlayerMapYPos(arg_4);

                ovr033.sub_74505(out var_D, out var_C, mapY, mapX);

                if (var_C > 0)
                {
                    arg_0 = gbl.player_array[var_C];

                    if (arg_0.combat_team == arg_4.combat_team &&
                        arg_0.hit_point_current >= arg_0.hit_point_max)
                    {
                        if (arg_0.hit_point_current < var_9 ||
                            (arg_0 == arg_4 && arg_0.hit_point_current < (arg_0.hit_point_max / 2)))
                        {
                            var_5 = arg_0;
                            var_9 = arg_0.hit_point_current;
                        }
                    }
                }
                else if (var_D == 0x1F)
                {
                    for (var_B = 1; var_B <= gbl.byte_1D1BB; var_B++)
                    {
                        if (gbl.unk_1D183[var_B].field_0 != null &&
                            word_3FDDE.MemberOf((byte)gbl.unk_1D183[var_B].field_0.health_status) == false &&
                            gbl.unk_1D183[var_B].mapX == mapX &&
                            gbl.unk_1D183[var_B].mapY == mapY)
                        {
                            var_8 = var_B;
                        }
                    }
                }
            }

            if (var_9 < 8 ||
                var_8 == 0)
            {
                arg_0 = var_5;
            }
            else
            {
                arg_0 = gbl.unk_1D183[var_8].field_0;
            }

            if (arg_0 != null)
            {
                var_1 = true;
            }
            else
            {
                var_1 = false;
            }

            return var_1;
        }


        internal static byte sub_4001C(Struct_1D183 arg_0, byte arg_4, byte arg_6, byte arg_8)
        {
            byte var_A;
            byte var_9;
            byte var_8;
            Player var_7;
            byte var_3;
            bool var_2;
            byte var_1;

            var_2 = false;
            if (arg_6 == 0)
            {
                var_A = arg_8 != 0x53 ? (byte)1 : (byte)0;

                sub_41B25(arg_0, out var_2, var_A, arg_4, 0, ovr023.sub_5CDE5(arg_8), gbl.player_ptr);
                gbl.player_ptr.actions.target = arg_0.field_0;
            }
            else
            {
                throw new System.NotSupportedException();//cmp	gbl.unk_19AEC[ arg_8 ].field_E, 0
                throw new System.NotSupportedException();//jnz	loc_400F2
                arg_0.field_0 = gbl.player_ptr;
                throw new System.NotSupportedException();//cmp	[bp+arg_8], 3
                throw new System.NotSupportedException();//jnz	loc_400BF
                sub_3FDFE(out arg_0.field_0, gbl.player_ptr);
                throw new System.NotSupportedException();//or	al, al
                throw new System.NotSupportedException();//jz	loc_400EF
                throw new System.NotSupportedException();//loc_400BF:
                arg_0.mapX = ovr033.PlayerMapXPos(arg_0.field_0);
                arg_0.mapY = ovr033.PlayerMapYPos(arg_0.field_0);
                var_2 = true;
                throw new System.NotSupportedException();//loc_400EF:
                throw new System.NotSupportedException();//jmp	loc_401DD
                throw new System.NotSupportedException();//loc_400F2:
                var_9 = 1;

                while (var_9 > 0 &&
                        var_2 == false)
                {
                    var_3 = 1;

                    if (sub_41E44(1, 0, ovr023.sub_5CDE5(arg_8), gbl.player_ptr) == true)
                    {
                        var_7 = gbl.player_ptr.actions.target;

                        if (ovr025.is_held(var_7) == true)
                        {
                            for (var_8 = 1; var_8 <= 4; var_8++)
                            {
                                throw new System.NotSupportedException();//mov	al, [bp+var_8]
                                throw new System.NotSupportedException();//xor	ah, ah
                                throw new System.NotSupportedException();//mov	di, ax
                                throw new System.NotSupportedException();//mov	dl, [di+27CBh]
                                throw new System.NotSupportedException();//mov	al, gbl.unk_19AEC[ arg_8 ].field_A
                                throw new System.NotSupportedException();//cmp	al, dl
                                throw new System.NotSupportedException();//jnz	loc_40180
                                var_3 = 0;
                                throw new System.NotSupportedException();//loc_40180:
                            }

                        }

                        if (var_3 != 0)
                        {
                            arg_0.field_0 = gbl.player_ptr.actions.target;
                            arg_0.mapX = ovr033.PlayerMapXPos(arg_0.field_0);
                            arg_0.mapY = ovr033.PlayerMapYPos(arg_0.field_0);
                            var_2 = true;
                        }
                    }

                    var_9 -= 1;
                }
            }
            throw new System.NotSupportedException();//loc_401DD:

            if (var_2 == true)
            {
                var_1 = 1;
                gbl.byte_1D883 = arg_0.mapX;
                gbl.byte_1D884 = arg_0.mapY;
            }
            else
            {
                var_1 = 0;
                arg_0.Clear();
            }

            return var_1;
        }

        internal static void target(out bool arg_0, byte arg_4, byte arg_6)
        {
            byte var_F;
            byte var_E;
            byte var_D;
            Struct_1D183 var_C = new Struct_1D183();
            byte var_5;
            byte var_4;
            byte var_3;
            byte var_2;
            byte var_1;

            arg_0 = true;
            gbl.byte_1D75E = 0;
            gbl.byte_1D2C7 = 0;

            gbl.byte_1D883 = ovr033.PlayerMapXPos(gbl.player_ptr);
            gbl.byte_1D884 = ovr033.PlayerMapYPos(gbl.player_ptr);

            byte tmp1 = (byte)(gbl.unk_19AEC[arg_6].field_6 & 0x0F);

            if (tmp1 == 0)
            {
                gbl.sp_target[1] = gbl.player_ptr;
                gbl.byte_1D75E = 1;
            }
            else if (tmp1 == 5)
            {
                var_5 = 0;
                var_2 = 0;

                if (arg_6 == 0x4F)
                {
                    var_4 = ovr025.sub_6886F(0x4F);
                }
                else
                {
                    var_4 = ovr024.roll_dice(4, 2);
                }

                var_E = 0;

                do
                {
                    if (sub_4001C(var_C, 0, arg_4, arg_6) != 0)
                    {
                        var_D = 0;
                        var_F = var_2;

                        for (var_3 = 1; var_3 <= var_F; var_3++)
                        {
                            if (gbl.sp_target[var_3] == var_C.field_0)
                            {
                                var_D = 1;
                            }
                        }

                        if (var_D == 0)
                        {
                            var_2++;

                            gbl.sp_target[var_2] = var_C.field_0;

                            gbl.byte_1D883 = ovr033.PlayerMapXPos(var_C.field_0);
                            gbl.byte_1D884 = ovr033.PlayerMapYPos(var_C.field_0);
                            gbl.byte_1D75E++;

                            if (arg_6 != 0x4f)
                            {
                                byte al = gbl.sp_target[var_2].field_E5;

                                if (al == 0 || al == 1)
                                {
                                    var_5 += 1;
                                }
                                else if (al == 2)
                                {
                                    var_5 += 2;
                                }
                                else if (al == 3)
                                {
                                    var_5 += 4;
                                }
                                else
                                {
                                    var_5 += 8;
                                }
                            }
                            else
                            {
                                byte al = gbl.sp_target[var_2].field_DE;

                                if (al == 1)
                                {
                                    var_5 += 1;
                                }
                                else if (al == 2 || al == 3)
                                {
                                    var_5 += 2;
                                }
                                else if (al == 4)
                                {
                                    var_5 += 4;
                                }
                            }

                            if (var_2 > 1 && var_5 > var_4)
                            {
                                var_E = 1;
                            }
                        }
                        else
                        {
                            if (arg_4 != 0)
                            {
                                var_4 -= 1;
                            }
                            else
                            {
                                ovr025.string_print01("Already been targeted");
                            }
                        }

                        ovr033.sub_7431C(ovr033.PlayerMapYPos(var_C.field_0), ovr033.PlayerMapXPos(var_C.field_0));
                    }
                    else
                    {
                        var_E = 1;
                    }
                } while (var_E == 0 && var_4 != 0);
            }
            else if (tmp1 == 0x0F)
            {

                if (sub_4001C(var_C, 0, arg_4, arg_6) != 0)
                {
                    if (gbl.player_ptr.actions.target != null)
                    {

                        gbl.sp_target[1] = gbl.player_ptr.actions.target;
                        gbl.byte_1D75E = 1;
                    }
                    else
                    {
                        /* TODO it doesn't make sense to mask the low nibble then shift it out */
                        ovr032.sub_738D8(gbl.stru_1D1BC, 1, 0xff, (short)((gbl.unk_19AEC[arg_6].field_6 & 0x0f) >> 4), gbl.byte_1D884, gbl.byte_1D883);
                        var_F = gbl.byte_1D1C0;

                        for (var_2 = 0; var_2 < var_F; var_2++)
                        {
                            gbl.sp_target[var_2 + 1] = gbl.player_array[gbl.unk_1D1C1[var_2].field_0];
                        }

                        gbl.byte_1D75E = gbl.byte_1D1C0;
                        gbl.byte_1D2C7 = 1;
                    }
                }
                else
                {
                    arg_0 = false;
                }
            }
            else if (tmp1 >= 8 && tmp1 <= 0x0E)
            {
                if (sub_4001C(var_C, 1, arg_4, arg_6) != 0)
                {
                    ovr032.sub_738D8(gbl.stru_1D1BC, 1, 0xff, (short)(gbl.unk_19AEC[arg_6].field_6 & 7), gbl.byte_1D884, gbl.byte_1D883);
                    var_F = gbl.byte_1D1C0;

                    for (var_2 = 0; var_2 < var_F; var_2++)
                    {
                        gbl.sp_target[var_2 + 1] = gbl.player_array[gbl.unk_1D1C1[var_2].field_0];
                    }

                    gbl.byte_1D75E = gbl.byte_1D1C0;
                    gbl.byte_1D2C7 = 1;
                }
                else
                {
                    arg_0 = false;
                }
            }
            else
            {
                var_1 = (byte)((gbl.unk_19AEC[arg_6].field_6 & 3) + 1);
                var_2 = 0;

                while (var_1 > 0)
                {

                    if (sub_4001C(var_C, 0, arg_4, arg_6) != 0)
                    {
                        var_D = 0;
                        var_F = var_2;

                        for (var_3 = 1; var_3 <= var_F; var_3++)
                        {
                            if (gbl.sp_target[var_3] == var_C.field_0)
                            {
                                var_D = 1;
                            }
                        }

                        if (var_D == 0)
                        {
                            var_2++;
                            gbl.sp_target[var_2] = var_C.field_0;
                            var_1 -= 1;

                            gbl.byte_1D883 = ovr033.PlayerMapXPos(var_C.field_0);
                            gbl.byte_1D884 = ovr033.PlayerMapYPos(var_C.field_0);
                        }
                        else
                        {
                            if (arg_4 == 0)
                            {
                                ovr025.string_print01("Already been targeted");
                            }
                            else
                            {
                                var_1 -= 1;
                            }
                        }

                        ovr033.sub_7431C(ovr033.PlayerMapYPos(var_C.field_0), ovr033.PlayerMapXPos(var_C.field_0));
                    }
                    else
                    {
                        var_1 = 0;
                    }
                }

                gbl.byte_1D75E = var_2;

                if (var_2 == 0)
                {
                    arg_0 = false;
                }

                gbl.byte_1D883 = ovr033.PlayerMapXPos(gbl.sp_target[var_2]);
                gbl.byte_1D884 = ovr033.PlayerMapYPos(gbl.sp_target[var_2]);

            }
        }


        internal static void spell_menu3(out bool arg_0, byte arg_4, byte arg_6)
        {
            Player var_A;
            bool var_6;
            short var_5;
            sbyte var_3;
            byte var_1;

            var_A = gbl.player_ptr;
            var_6 = true;
            var_5 = -1;
            arg_0 = false;

            var_1 = arg_6;

            if (var_1 == 0)
            {
                var_1 = ovr020.spell_menu2(out var_6, ref var_5, 1, SpellLoc.memory);
            }

            if (var_1 > 0 &&
                gbl.unk_19AEC[var_1].field_B == 0)
            {
                ovr025.string_print01("Camp Only Spell");
                var_1 = 0;
            }

            if (arg_4 == 0)
            {
                ovr025.sub_68DC0();
                gbl.byte_1D910 = true;
                gbl.byte_1D90F = true;

                ovr033.sub_75356(true, 3, var_A);
                ovr025.hitpoint_ac(var_A);
            }

            if (var_1 > 0)
            {
                var_3 = (sbyte)(gbl.unk_19AEC[var_1].field_C / 3);

                if (var_3 == 0)
                {
                    ovr023.sub_5D2E1(ref arg_0, 1, arg_4, var_1);

                    arg_0 = ovr025.clear_actions(var_A);
                }
                else
                {
                    arg_0 = true;
                    ovr025.DisplayPlayerStatusString(true, 10, "Begins Casting", var_A);

                    var_A.actions.spell_id = var_1;

                    if (var_A.actions.delay > var_3)
                    {
                        var_A.actions.delay = var_3;
                    }
                    else
                    {
                        var_A.actions.delay = 1;
                    }
                }
            }
        }


        internal static byte sub_408D7(Player arg_0, Player arg_4)
        {
            byte var_B;
            byte var_A;
            Item var_9;
            Item var_5;
            byte var_1;

            var_B = 0;

            var_5 = arg_4.field_151;

            if (var_5 != null)
            {
                var_A = var_5.type;
            }
            else
            {
                var_A = 0; // Only needed as the compiler doesn't see var_A is only set when var_5 is not null...
            }

            var_9 = arg_4.field_159;


            if (arg_4.thief_lvl > 0 ||
                (arg_4.field_117 > 0 && ovr026.sub_6B3D1(arg_4) != 0))
            {
                if (var_5 == null ||
                    var_A == 0x61 ||
                    var_A == 7 ||
                    var_A == 8 ||
                    (var_A > 0x22 && var_A < 0x26))
                {
                    var_B = 1;
                }
            }

            if (var_B != 0 &&
                arg_0.actions.field_F > 1 &&
                (arg_0.field_DE & 0x7F) <= 1 &&
                sub_409BC(arg_0, arg_4) == arg_0.actions.field_9)
            {
                var_1 = 1;
            }
            else
            {
                var_1 = 0;
            }

            return var_1;
        }


        internal static byte sub_409BC(Player playerB, Player playerA)
        {
            int var_5 = ovr033.PlayerMapXPos(playerA);
            int var_7 = ovr033.PlayerMapYPos(playerA);

            int var_9 = ovr033.PlayerMapXPos(playerB);
            int var_B = ovr033.PlayerMapYPos(playerB);

            int var_D = System.Math.Abs(var_9 - var_5);
            int var_F = System.Math.Abs(var_B - var_7);

            byte var_2 = 0;
            byte var_3 = 0;

            while (var_3 == 0)
            {
                switch (var_2)
                {
                    case 0:
                        if (var_B > var_7 || ((0x26A * var_D) / 0x100) > var_F)
                        {
                            var_3 = 0;
                        }
                        else
                        {
                            var_3 = 1;
                        }
                        break;

                    case 2:
                        if (var_9 < var_5 || ((0x6A * var_D) / 0x100) < var_F)
                        {
                            var_3 = 0;
                        }
                        else
                        {
                            var_3 = 1;
                        }
                        break;

                    case 4:
                        if (var_B < var_7 || ((0x26A * var_D) / 0x100) > var_F)
                        {
                            var_3 = 0;
                        }
                        else
                        {
                            var_3 = 1;
                        }
                        break;

                    case 6:
                        if (var_9 > var_5 || ((0x6A * var_D) / 0x100) < var_F)
                        {
                            var_3 = 0;
                        }
                        else
                        {
                            var_3 = 1;
                        }
                        break;

                    case 1:
                        if (var_B > var_7 ||
                            var_9 < var_5 ||
                            ((0x26A * var_D) / 0x100) < var_F ||
                            ((0x6A * var_D) / 0x100) > var_F)
                        {
                            var_3 = 0;
                        }
                        else
                        {
                            var_3 = 1;
                        }
                        break;

                    case 3:
                        if (var_B < var_7 ||
                            var_9 < var_5 ||
                            ((0x26a * var_D) / 0x100) < var_F ||
                            ((0x6a * var_D) / 0x100) > var_F)
                        {
                            var_3 = 0;
                        }
                        else
                        {
                            var_3 = 1;
                        }
                        break;

                    case 5:
                        if (var_B < var_7 ||
                            var_9 > var_5 ||
                            ((0x26a * var_D) / 0x100) < var_F ||
                            ((0x6a * var_D) / 0x100) > var_F)
                        {
                            var_3 = 0;
                        }
                        else
                        {
                            var_3 = 1;
                        }
                        break;

                    case 7:
                        if (var_B > var_7 ||
                            var_9 > var_5 ||
                            ((0x26a * var_D) / 0x100) < var_F ||
                            ((0x6a * var_D) / 0x100) > var_F)
                        {
                            var_3 = 0;
                        }
                        else
                        {
                            var_3 = 1;
                        }
                        break;
                }

                if (var_3 == 0)
                {
                    var_2++;
                }
            }

            return var_2;
        }


        internal static void sub_40BF1(Item arg_0, Player playerA, Player playerB)
        {
            short var_6;
            byte var_4;
            byte var_3;
            byte var_2;
            byte var_1;

            seg044.sound_sub_120E0(gbl.word_188D6);

            var_3 = sub_409BC(playerA, playerB);

            var_2 = 1;
            var_4 = 0x0A;
            var_1 = 0x0D;

            var_6 = gbl.dword_1D90A.bpp;

            switch (arg_0.type)
            {
                case 9:
                case 0x15:
                case 0x64:
                case 0x1C:
                case 0x1F:
                case 0x49:
                    if ((var_3 & 1) == 1)
                    {
                        if (var_3 == 3 || var_3 == 5)
                        {
                            ovr025.sub_67924((var_3 == 5), 0, 1, (byte)(var_1 + 1));
                        }
                        else
                        {
                            ovr025.sub_67924((var_3 == 7), 0, 0, (byte)(var_1 + 1));
                        }
                    }
                    else
                    {
                        ovr025.sub_67924(false, 0, (byte)(var_3 >> 2), (byte)(var_1 + (var_3 % 4)));
                    }
                    seg044.sound_sub_120E0(gbl.word_188D6);
                    break;

                case 2:
                case 7:
                case 14:
                    ovr025.sub_67A59((byte)(var_1 + 3));
                    var_2 = 4;
                    var_4 = 0x32;
                    seg044.sound_sub_120E0(gbl.word_188D0);
                    break;


                case 0x55:
                case 0x56:
                    ovr025.sub_67A59((byte)(var_1 + 4));
                    var_2 = 4;
                    var_4 = 0x32;
                    seg044.sound_sub_120E0(gbl.word_188CA);
                    break;

                case 0x65:
                case 0x2F:
                case 0x62:
                    var_1++;
                    ovr025.sub_67924(false, 0, 0, (byte)(var_1 + 7));
                    ovr025.sub_67924(false, 1, 1, (byte)(var_1 + 7));
                    var_2 = 2;
                    var_4 = 0x0A;
                    seg044.sound_sub_120E0(gbl.word_188CA);

                    break;

                default:
                    ovr025.sub_67924(false, 0, 0, (byte)(var_1 + 7));
                    ovr025.sub_67924(false, 1, 1, (byte)(var_1 + 7));
                    var_2 = 2;
                    var_4 = 0x14;
                    seg044.sound_sub_120E0(gbl.word_188D0);
                    break;
            }
            
            ovr025.sub_67AA4(var_4, var_2, ovr033.PlayerMapYPos(playerA), ovr033.PlayerMapXPos(playerA),
                ovr033.PlayerMapYPos(playerB), ovr033.PlayerMapXPos(playerB));
        }


        internal static void sub_40E00()
        {
            Player var_8;
            short var_4;
            short var_2;

            var_2 = 0;
            var_4 = 0;
            var_8 = gbl.player_next_ptr;

            while (var_8 != null)
            {

                if (var_8.combat_team == 1)
                {
                    if (var_8.in_combat == true)
                    {
                        var_4 += var_8.hit_point_current;
                    }

                    var_2 += var_8.hit_point_max;
                }

                var_8 = var_8.next_player;
            }

            if (var_2 > 0)
            {
                gbl.byte_1D903 = (byte)(((20 * var_4) / var_2) * 5);
            }
        }


        internal static byte sub_40E8F(Player arg_0)
        {
            Player var_7;
            byte var_3;
            byte var_2;

            var_2 = 0;
            var_7 = gbl.player_next_ptr;

            while (var_7 != null)
            {
                if (ovr025.on_our_team(arg_0) == var_7.combat_team &&
                    var_7.in_combat == true)
                {
                    var_3 = (byte)(sub_3E124(var_7) >> 1);

                    if (var_3 > var_2)
                    {
                        var_2 = var_3;
                    }
                }

                var_7 = var_7.next_player;
            }

            return var_2;
        }


        internal static bool sub_40F1F(Player playerA, Player playerB)
        {
            Player player;
            bool var_1;

            if (ovr025.on_our_team(playerA) == playerB.combat_team ||
                playerB.field_198 != 0)
            {
                var_1 = true;
            }
            else if (ovr027.yes_no(15, 10, 13, "Attack Ally: ") != 'Y')
            {
                var_1 = false;
            }
            else
            {
                var_1 = true;
                gbl.byte_1D8B6 = 1;
                gbl.area2_ptr.field_666 = 1;

                player = gbl.player_next_ptr;

                while (player != null)
                {
                    if (player.health_status == Status.okey &&
                        player.field_F7 > 0x7F)
                    {
                        player.combat_team = 1;
                        player.actions.target = null;
                    }

                    player = player.next_player;
                }

                ovr025.count_teams();
            }

            return var_1;
        }


        internal static char Aim_menu(byte arg_0, byte arg_2, byte arg_4, byte arg_6, Player arg_8, Player arg_C)
        {
            string var_231;
            byte var_31;
            byte var_30;
            Item var_2F;
            bool var_2B;
            string var_2A;
            char var_1;

            var_2A = string.Empty;
            var_30 = ovr025.sub_68708(arg_8, arg_C);
            var_31 = sub_409BC(arg_8, arg_C);

            if (arg_4 != 0)
            {
                var_231 = "Range = " + ovr025.sub_670CC(var_30) + "  ";
                seg041.displayString(var_231, 0, 10, 0x17, 0);
            }

            if (var_30 <= arg_6)
            {
                if (arg_4 == 0)
                {
                    if (arg_0 != 0)
                    {
                        var_2A = "Target ";
                    }
                    else
                    {
                        var_2A = string.Empty;
                    }
                }
                else if (arg_8 != arg_C)
                {
                    if (ovr025.offset_above_1(arg_C) == false)
                    {
                        var_2A = "Target ";
                    }
                    else
                    {
                        if (ovr025.sub_6906C(out var_2F, arg_C) == true &&
                            (ovr025.near_enermy(1, arg_C) == 0 || ovr025.offset_equals_20(arg_C) == true))
                        {
                            var_2A = "Target ";
                        }
                    }
                }
            }
            
            var_2A = "Next Prev Manual " + var_2A + "Center Exit";
            ovr033.sub_75356(true, 3, arg_8);
            gbl.byte_1D90F = true;
            ovr025.hitpoint_ac(arg_8);

            var_1 = ovr027.displayInput(out var_2B, 0, 1, 15, 10, 13, var_2A, "Aim:");

            return var_1;
        }


        internal static void sub_411D8(Struct_1D183 arg_0, ref bool arg_4, byte arg_8, Player arg_A, Player arg_E)
        {
            Item var_5;

            var_5 = null;
            arg_4 = true;

            if (arg_8 == 1 ||
                sub_40F1F(arg_A, arg_E) == false)
            {
                arg_4 = false;
            }

            if (arg_4 == true)
            {
                arg_0.field_0 = arg_A;
                arg_0.mapX = ovr033.PlayerMapXPos(arg_A);
                arg_0.mapY = ovr033.PlayerMapYPos(arg_A);
                gbl.stru_1D1BC.field_4 = false;

                ovr033.sub_749DD(8, 3, gbl.stru_1D1BC.mapScreenTopY + 3, gbl.stru_1D1BC.mapScreenLeftX + 3);

                if (arg_8 == 1)
                {
                    if (sub_3EF3D(arg_A, arg_E) == true)
                    {
                        arg_4 = ovr025.clear_actions(arg_E);
                    }
                    else
                    {
                        sub_3F94D(arg_A, arg_E);

                        if (ovr025.offset_above_1(arg_E) == true &&
                            ovr025.sub_6906C(out var_5, arg_E) == true &&
                            ovr025.offset_equals_20(arg_E) == true &&
                            ovr025.sub_68708(arg_A, arg_E) == 0)
                        {
                            var_5 = null;
                        }

                        sub_3F9DB(out arg_4, var_5, 0, arg_A, arg_E);
                    }
                }
            }
            else
            {
                arg_0.Clear();
            }
        }

        static Set asc_41342 = new Set(0x000A, new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20, 0x00, 0x10 });

        internal static void Target(Struct_1D183 arg_0, out bool arg_4, byte arg_8, byte arg_A, byte arg_C, byte arg_E, Player player02, Player player01)
        {
            string var_239;
            byte var_39;
            short var_38;
            byte var_36;
            byte var_35;
            byte var_30;
            byte var_2F;
            Item var_2E;
            char var_2A;
            string var_29;

            arg_0.Clear();

            int var_33 = ovr033.PlayerMapXPos(player02);
            int var_34 = ovr033.PlayerMapYPos(player02);

            var_2A = ' ';
            var_2F = 8;

            arg_4 = false;

            gbl.stru_1D1BC.field_4 = true;
            gbl.stru_1D1BC.field_5 = 1;

            while (asc_41342.MemberOf(var_2A) == false)
            {
                ovr033.sub_749DD(var_2F, 3, var_34, var_33);
                var_33 += gbl.MapDirectionXDelta[var_2F];
                var_34 += gbl.MapDirectionYDelta[var_2F];

                if (var_33 < 0)
                {
                    var_33 = 0;
                }

                if (var_34 < 0)
                {
                    var_34 = 0;
                }

                if (var_33 > 0x31)
                {
                    var_33 = 0x31;
                }

                if (var_34 > 0x18)
                {
                    var_34 = 0x18;
                }

                ovr033.sub_74505(out var_36, out var_35, var_34, var_33);
                seg043.clear_keyboard();
                var_39 = 0;
                var_38 = 0x0FF;

                int var_31 = var_33;
                int var_32 = var_34;

                if (ovr032.sub_733F1(gbl.stru_1D1BC, ref var_38, ref var_32, ref var_31, ovr033.PlayerMapYPos(player01), ovr033.PlayerMapXPos(player01)) == true)
                {
                    var_39 = 1;

                    if (arg_C != 0)
                    {
                        var_239 = "Range = " + ovr025.ConcatWord(var_38 * 2) + "  ";

                        seg041.displayString(var_239, 0, 10, 0x17, 0);
                    }
                }
                else
                {
                    if (arg_C != 0)
                    {
                        seg037.draw8x8_clear_area(0x17, 0x27, 0x17, 0);
                    }
                }

                var_38 /= 2;
                player02 = null;

                if (var_39 != 0)
                {
                    if (var_35 > 0)
                    {
                        player02 = gbl.player_array[var_35];
                    }
                    else if (var_36 == 0x1f)
                    {
                        for (var_30 = 1; var_30 <= gbl.byte_1D1BB; var_30++)
                        {
                            if (gbl.unk_1D183[var_30].mapX == var_33 &&
                                gbl.unk_1D183[var_30].mapY == var_34)
                            {
                                player02 = gbl.unk_1D183[var_30].field_0;
                            }
                        }
                    }
                }

                if (player02 != null)
                {
                    gbl.byte_1D90F = true;
                    ovr025.hitpoint_ac(player02);
                }
                else
                {
                    seg037.draw8x8_clear_area(0x15, 0x26, 1, 0x17);
                }

                if (arg_E < var_38 ||
                    gbl.unk_189B4[var_36].move_cost == 0xff)
                {
                    var_39 = 0;
                }

                if (player02 != null)
                {
                    if (sub_3F143(player02, player01) == false ||
                        arg_8 == 0)
                    {
                        var_39 = 0;
                    }

                    if (arg_C == 1)
                    {
                        if (player01 == player02 ||
                            (var_35 == 0 && var_36 == 0x1f))
                        {
                            var_39 = 0;
                        }
                        else if (ovr025.offset_above_1(player01) == true &&
                             (ovr025.sub_6906C(out var_2E, player01) == true ||
                             (ovr025.near_enermy(1, player01) >= 0 &&
                                ovr025.offset_equals_20(player01) == false)))
                        {
                            var_39 = 0;

                        }
                    }
                }
                else if (arg_A == 0)
                {
                    var_39 = 0;
                }

                var_29 = "Center Exit";

                if (var_39 != 0)
                {
                    var_29 = "Target " + var_29;
                }

                var_2A = ovr027.displayInput(out gbl.byte_1D905, 0, 1, 15, 10, 13, var_29, "(Use Cursor keys) ");


                switch ((int)var_2A)
                {
                    case 0x0D:
                    case 0x54:
                        gbl.stru_1D1BC.field_4 = false;

                        if (var_39 != 0)
                        {
                            arg_0.mapX = var_33;
                            arg_0.mapY = var_34;

                            if (player02 != null)
                            {
                                arg_0.field_0 = player02;
                            }
                            else
                            {
                                arg_0.field_0 = null;
                            }

                            if (arg_C == 1)
                            {
                                sub_411D8(arg_0, ref arg_4, arg_C, arg_0.field_0, player01);
                            }
                            else
                            {
                                arg_4 = true;
                            }
                        }

                        if (var_39 == 0 ||
                            arg_4 == false)
                        {
                            ovr033.sub_7431C(var_34, var_33);
                            arg_4 = false;
                            arg_0.Clear();
                        }
                        break;

                    case 0x48:
                        var_2F = 0;
                        break;

                    case 0x49:
                        var_2F = 1;
                        break;

                    case 0x4D:
                        var_2F = 2;
                        break;

                    case 0x51:
                        var_2F = 3;
                        break;

                    case 0x50:
                        var_2F = 4;
                        break;

                    case 0x4F:
                        var_2F = 5;
                        break;

                    case 0x4B:
                        var_2F = 6;
                        break;

                    case 0x47:
                        var_2F = 7;
                        break;

                    case 0:
                    case 0x45:
                        ovr033.sub_7431C(var_34, var_33);
                        arg_0.Clear();
                        arg_4 = false;
                        break;

                    case 0x43:
                        ovr033.sub_749DD(8, 0, var_34, var_33);
                        var_2F = 8;
                        break;

                    default:
                        var_2F = 8;
                        break;
                }
            }
        }


        internal static void sub_4188F(Player player01, gbl.Struct_1D1C1[] bp_var_D8, out byte bp_var_DA)
        {
            byte var_1;

            ovr032.sub_738D8(gbl.stru_1D1BC, ovr033.sub_74C82(player01), 0xff, 0x7F, ovr033.PlayerMapYPos(player01), ovr033.PlayerMapXPos(player01));

            bp_var_DA = gbl.byte_1D1C0;

            for (var_1 = 1; var_1 <= bp_var_DA; var_1++)
            {
                bp_var_D8[var_1 - 1] = new gbl.Struct_1D1C1();
                bp_var_D8[var_1 - 1].Copy(gbl.unk_1D1C1[var_1]);
            }
        }


        internal static Player sub_41932(bool arg_2, byte arg_4, byte bp_var_DA, byte bp_var_DB, sbyte bp_var_DE, sbyte bp_var_DF, sbyte bp_var_E0, sbyte bp_var_E1, gbl.Struct_1D1C1[] bp_var_D8)
        {
            Player player_ptr;

            if (arg_2 == true)
            {
                bp_var_DE = (sbyte)gbl.stru_1C9CD[bp_var_D8[bp_var_DB].field_0].xPos;
                bp_var_DF = (sbyte)gbl.stru_1C9CD[bp_var_D8[bp_var_DB].field_0].yPos;
            }
            else
            {
                ovr033.sub_7431C(bp_var_DF, bp_var_DE);
            }

            bp_var_DB = arg_4;

            if (bp_var_DB == 0)
            {
                bp_var_DB = bp_var_DA;
            }

            if (bp_var_DB > bp_var_DA)
            {
                bp_var_DB = 1;
            }

            player_ptr = gbl.player_array[bp_var_D8[bp_var_DB-1].field_0];

            bp_var_E0 = (sbyte)gbl.stru_1C9CD[bp_var_D8[bp_var_DB-1].field_0].xPos;
            bp_var_E1 = (sbyte)gbl.stru_1C9CD[bp_var_D8[bp_var_DB-1].field_0].yPos;

            if (arg_2 == true)
            {
                ovr025.sub_67AA4(0, 1, bp_var_E1, bp_var_E0, bp_var_DF, bp_var_DE);
                bp_var_DE = bp_var_E0;
                bp_var_DF = bp_var_E1;
            }
            return player_ptr;
        }

        static Set unk_41AE5 = new Set(0x0009, new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20 });
        static Set unk_41B05 = new Set(0x0803, new byte[] { 0x80, 0xAB, 3 });

        internal static void sub_41B25(Struct_1D183 arg_0, out bool arg_4, byte arg_8, byte arg_A, byte arg_C, byte arg_E, Player arg_10)
        {
            char var_E6;
            Player player_ptr; /* var_E5 */
            sbyte var_E1 = 0; /* simeon */
            sbyte var_E0 = 0; /* simeon */
            sbyte var_DF = 0; /* simeon */
            sbyte var_DE = 0; /* simeon */
            byte var_DD = 0; /* simeon */
            byte var_DC;
            byte var_DB;
            byte var_DA;
            byte var_D9;
            gbl.Struct_1D1C1[] var_D8 = new gbl.Struct_1D1C1[gbl.unk_1D1C1_count];

            ovr025.sub_67924(false, 0, 0, 0x19);

            arg_0.Clear();

            arg_4 = false;

            var_D9 = arg_E;

            if (var_D9 == 0xff)
            {
                if (arg_10.field_151 != null)
                {
                    var_D9 = (byte)(gbl.unk_1C020[arg_10.field_151.type].field_C - 1);
                }
                else
                {
                    var_D9 = 1;
                }
            }

            if (var_D9 == 0 ||
                var_D9 == 0xff)
            {
                var_D9 = 1;
            }

            sub_4188F(arg_10, var_D8, out var_DA);

            var_DB = 1;
            var_DC = 0;

            player_ptr = sub_41932(true, var_DC, var_DA, var_DB, var_DE, var_DF, var_E0, var_E1, var_D8);

            var_DC = 1;
            var_E6 = ' ';

            while (arg_4 == false && unk_41AE5.MemberOf(var_E6) == false)
            {
                if (sub_3F143(player_ptr, arg_10) == false)
                {

                    player_ptr = sub_41932(false, var_DC, var_DA, var_DB, var_DE, var_DF, var_E0, var_E1, var_D8);
                }
                else
                {
                    var_E6 = Aim_menu(arg_8, arg_A, arg_C, var_D9, player_ptr, arg_10);

                    if (gbl.byte_1D5BF == false)
                    {
                        switch (var_E6)
                        {
                            case 'N':
                                var_DC = 1;
                                var_DD = 1;
                                break;

                            case 'P':
                                var_DC = 0x0FF;
                                var_DD = 0x0FF;
                                break;

                            case 'M': goto case 'I';
                            case 'H': goto case 'I';
                            case 'K': goto case 'I';
                            case 'G': goto case 'I';
                            case 'O': goto case 'I';
                            case 'Q': goto case 'I';

                            case 'I':
                                Target(arg_0, out arg_4, arg_8, arg_A, arg_C, var_D9, player_ptr, arg_10);
                                ovr025.sub_67924(false, 0, 0, 0x19);

                                sub_4188F(arg_10, var_D8, out var_DA);
                                var_DD = 0;
                                break;

                            case 'T':
                                sub_411D8(arg_0, ref arg_4, arg_C, player_ptr, arg_10);
                                ovr025.sub_67924(false, 0, 0, 0x19);

                                sub_4188F(arg_10, var_D8, out var_DA);
                                var_DD = 0;
                                break;

                            case 'C':
                                ovr033.sub_749DD(8, 0, ovr033.PlayerMapYPos(player_ptr), ovr033.PlayerMapXPos(player_ptr));
                                var_DD = 0;
                                break;
                        }
                    }
                    else if (unk_41B05.MemberOf(var_E6) == true)
                    {
                        Target(arg_0, out arg_4, arg_8, arg_A, arg_C, var_D9, player_ptr, arg_10);
                        ovr025.sub_67924(false, 0, 0, 0x19);
                        sub_4188F(arg_10, var_D8, out var_DA);
                        var_DD = 0;
                    }

                    ovr033.sub_7431C(ovr033.PlayerMapYPos(player_ptr), ovr033.PlayerMapXPos(player_ptr));

                    player_ptr = sub_41932((arg_4 == false && unk_41AE5.MemberOf(var_E6) == false), var_DD,
                        var_DA, var_DB, var_DE, var_DF, var_E0, var_E1, var_D8);
                }
            }

            if (arg_C != 0)
            {
                seg037.draw8x8_clear_area(0x17, 0x27, 0x17, 0);
            }
        }


        internal static bool sub_41E44(byte arg_0, byte arg_2, byte arg_4, Player player)
        {
            byte var_C;
            Player target;
            byte var_7;
            bool var_6;
            byte var_5;
            byte var_4;
            byte var_3;
            byte var_2;

            var_7 = 0;
            var_6 = false;
            var_5 = 0;

            target = player.actions.target;

            if (arg_0 != 0 ||
                 (target != null &&
                   (target.combat_team == player.combat_team ||
                    target.in_combat == false ||
                    sub_3F143(target, player) == false)))
            {
                player.actions.target = null;
            }
           
            if (player.actions.target != null)
            {
                var_6 = true;
            }

            while (var_6 == false && var_5 == 0)
            {
                var_5 = var_7;

                if (var_7 != 0 && arg_0 == 0)
                {
                    gbl.stru_1D1BC.field_6 = 1;
                }

                var_3 = 0x14;
                var_2 = ovr025.near_enermy(arg_4, player);

                while (var_3 > 0 && var_6 == false && var_2 > 0)
                {
                    var_3--;
                    var_4 = ovr024.roll_dice(var_2, 1);

                    if (gbl.byte_1D8B9[var_4] > 0)
                    {
                        target = gbl.player_array[gbl.byte_1D8B9[var_4]];

                        if ((arg_2 != 0 && gbl.stru_1D1BC.field_6 != 0) ||
                            sub_3F143(target, player) == true)
                        {
                            var_6 = true;
                            player.actions.target = target;
                        }
                        else
                        {
                            gbl.byte_1D8B9[var_4] = 0;
                            var_C = var_2;
                            throw new System.NotSupportedException();//mov	al, 1
                            throw new System.NotSupportedException();//cmp	al, [bp+var_C]
                            throw new System.NotSupportedException();//ja	loc_41FF1
                            throw new System.NotSupportedException();//mov	[bp+var_4], al
                            throw new System.NotSupportedException();//jmp	short loc_41FD7
                            throw new System.NotSupportedException();//loc_41FD4:
                            var_4++;
                            throw new System.NotSupportedException();//loc_41FD7:
                            throw new System.NotSupportedException();//mov	al, [bp+var_4]
                            throw new System.NotSupportedException();//xor	ah, ah
                            throw new System.NotSupportedException();//mov	di, ax
                            throw new System.NotSupportedException();//cmp	byte_1D8B9[di],	0
                            throw new System.NotSupportedException();//jbe	loc_41FE9
                            var_6 = true;
                            throw new System.NotSupportedException();//loc_41FE9:
                            throw new System.NotSupportedException();//mov	al, [bp+var_4]
                            throw new System.NotSupportedException();//cmp	al, [bp+var_C]
                            throw new System.NotSupportedException();//jnz	loc_41FD4
                            throw new System.NotSupportedException();//loc_41FF1:
                            throw new System.NotSupportedException();//cmp	[bp+var_6], 0
                            throw new System.NotSupportedException();//jz	loc_41FFD
                            var_6 = false;
                            throw new System.NotSupportedException();//jmp	short loc_42001
                            throw new System.NotSupportedException();//loc_41FFD:
                            var_2 = 0;
                        }
                    }
                    //loc_42001:
                }

                if (var_7 != 0)
                {
                    var_7 = 1;
                }
            }

            gbl.stru_1D1BC.field_6 = 0;

            return var_6;
        }


        internal static void engulfs(byte arg_0, object param, Player playerB)
        {
            Player player_ptr;
            Affect var_5;

            player_ptr = playerB.actions.target;

            if (gbl.byte_1D2CA == 2 &&
                player_ptr.in_combat == true &&
                ovr025.find_affect(out var_5, Affects.affect_3a, player_ptr) == false &&
                ovr025.find_affect(out var_5, Affects.affect_0d, player_ptr) == false)
            {
                player_ptr = playerB.actions.target;
                ovr025.DisplayPlayerStatusString(true, 12, "engulfs " + player_ptr.name, playerB);
                ovr024.add_affect(false, ovr033.get_player_index(player_ptr), 0, Affects.affect_3a, player_ptr);

                ovr024.sub_630C7(0, null, player_ptr, Affects.affect_3a);
                ovr024.add_affect(false, ovr024.roll_dice(4, 2), 0, Affects.affect_0d, player_ptr);
                ovr024.add_affect(true, ovr033.get_player_index(player_ptr), 0, Affects.affect_8b, playerB);
            }
        }


        internal static void sub_42159(byte arg_2, Player player_target, Player player)
        {

            ovr025.sub_67A59((byte)(arg_2 + 13));

            ovr025.sub_67AA4(0x1E, 1, ovr033.PlayerMapYPos(player_target), ovr033.PlayerMapXPos(player_target),
                ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player));
        }


        internal static void sub_421C1(byte arg_2, ref short var_3, ref bool var_5, ref Player player)
        {
            var_5 = true;
            if (sub_41E44(arg_2, 0, 0xff, player) == true)
            {
                int tmpX = ovr033.PlayerMapXPos(player.actions.target);
                int tmpY = ovr033.PlayerMapYPos(player.actions.target);

                if (ovr032.sub_733F1(gbl.stru_1D1BC, ref var_3, ref tmpY, ref tmpX, ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player)) == true)
                {
                    var_5 = false;
                }
            }
        }


        internal static void attack_or_kill(byte arg_0, object param, Player player)
        {
            Player player_target;
            bool var_5;
            byte var_4;
            short var_3 = 0; /* simeon */
            byte var_1;

            var_4 = 0;
            var_1 = 4;
            var_5 = false;

            player.actions.target = null;
            sub_421C1(1, ref var_3, ref var_5, ref player);

            do
            {
                player_target = player.actions.target;

                var_3 = ovr025.sub_68708(player_target, player);
                var_1--;

                if (player_target != null)
                {
                    if (var_3 == 2 && (var_4 & 1) == 0)
                    {
                        var_4 |= 1;

                        ovr025.DisplayPlayerStatusString(true, 10, "fires a disintegrate ray", player);
                        sub_42159(5, player_target, player);

                        if (ovr024.do_saving_throw(0, 3, player_target) == false)
                        {
                            ovr024.sub_63014("is disintergrated", Status.gone, player_target);
                        }

                        sub_421C1(0, ref var_3, ref var_5, ref player);
                    }
                    else if (var_3 == 3 && (var_4 & 2) == 0)
                    {
                        var_4 |= 2;

                        ovr025.DisplayPlayerStatusString(true, 10, "fires a stone to flesh ray", player);
                        sub_42159(10, player_target, player);

                        if (ovr024.do_saving_throw(0, 1, player_target) == false)
                        {
                            ovr024.sub_63014("is Stoned", Status.stoned, player_target);
                        }

                        sub_421C1(0, ref var_3, ref var_5, ref player);
                    }
                    else if (var_3 == 4 && (var_4 & 4) == 0)
                    {
                        var_4 |= 4;

                        ovr025.DisplayPlayerStatusString(true, 10, "fires a death ray", player);
                        sub_42159(5, player_target, player);

                        if (ovr024.do_saving_throw(0, 0, player_target) == false)
                        {
                            ovr024.sub_63014("is killed", Status.dead, player_target);
                        }

                        sub_421C1(0, ref var_3, ref var_5, ref player);
                    }
                    else if (var_3 == 5 && (var_4 & 8) == 0)
                    {
                        var_4 |= 8;

                        ovr025.DisplayPlayerStatusString(true, 10, "wounds you", player);
                        sub_42159(5, player_target, player);

                        ovr024.damage_person(false, 0, (sbyte)(ovr024.roll_dice_save(8, 2) + 1), player_target);
                        sub_421C1(0, ref var_3, ref var_5, ref player);
                    }
                    else if ((var_4 & 0x10) == 0)
                    {
                        ovr023.sub_5D2E1(ref var_5, 1, 1, 0x54);
                        var_4 |= 0x10;
                    }
                    else if ((var_4 & 0x20) == 0)
                    {
                        ovr023.sub_5D2E1(ref var_5, 1, 1, 0x37);
                        var_4 |= 0x20;
                    }
                    else if ((var_4 & 0x40) == 0)
                    {
                        ovr023.sub_5D2E1(ref var_5, 1, 1, 0x15);
                        var_4 |= 0x40;
                    }
                }
            } while (var_1 > 0 && player.actions.target != null);
        }


        internal static void sub_425C6(byte arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;
            bool var_1;

            gbl.spell_target = gbl.player_array[affect.field_3];

            if (arg_0 != 0 ||
                player.in_combat == false ||
                gbl.spell_target.in_combat == false)
            {
                ovr024.remove_affect(null, Affects.affect_3a, gbl.spell_target);
                ovr024.remove_affect(null, Affects.affect_0d, gbl.spell_target);

                if (arg_0 == 0)
                {
                    affect.field_4 = false;

                    ovr024.remove_affect(affect, Affects.affect_8b, player);
                }
            }
            else
            {
                player.field_19C = 2;
                player.field_19D = 0;
                player.field_19E = 2;
                player.field_1A0 = 8;

                sub_3F9DB(out var_1, null, 1, gbl.spell_target, player);

                var_1 = ovr025.clear_actions(player);

                if (gbl.spell_target.in_combat == false)
                {
                    ovr024.remove_affect(null, Affects.affect_8b, player);
                    ovr024.remove_affect(null, Affects.affect_3a, gbl.spell_target);
                    ovr024.remove_affect(null, Affects.affect_0d, gbl.spell_target);
                }
            }
        }


        internal static void sub_426FC(byte arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;
            bool var_1;

            gbl.spell_target = gbl.player_array[affect.field_3];

            if (arg_0 != 0 ||
                player.in_combat == false ||
                gbl.spell_target.in_combat == false)
            {
                ovr024.remove_affect(null, Affects.affect_3a, gbl.spell_target);
                if (arg_0 == 0)
                {
                    affect.field_4 = false;
                    ovr024.remove_affect(affect, Affects.affect_90, player);
                }
            }
            else
            {
                player.field_19C = 1;
                player.field_19D = 0;
                player.field_19E = 2;
                player.field_1A0 = 8;

                sub_3F9DB(out var_1, null, 2, gbl.spell_target, player);
                var_1 = ovr025.clear_actions(player);

                if (gbl.spell_target.in_combat == false)
                {
                    ovr024.remove_affect(null, Affects.affect_90, player);
                    ovr024.remove_affect(null, Affects.affect_3a, gbl.spell_target);
                }
            }
        }


        internal static void hugs(byte arg_0, object param, Player player)
        {
            if (gbl.byte_1D2C9 >= 18)
            {
                gbl.spell_target = player.actions.target;
                ovr025.DisplayPlayerStatusString(true, 12, "hugs " + gbl.spell_target.name, player);

                ovr024.add_affect(false, ovr033.get_player_index(gbl.spell_target), 0, Affects.affect_3a, gbl.spell_target);
                ovr024.sub_630C7(0, null, gbl.spell_target, Affects.affect_3a);

                ovr024.add_affect(true, ovr033.get_player_index(gbl.spell_target), 0, Affects.affect_90, player);
            }
        }


        internal static bool god_intervene()
        {
            Player player;
            bool var_2;
            bool var_1;

            var_1 = false;

            if (seg051.ParamStr(2) == gbl.byte_1EFA4)
            {
                var_1 = true;
                ovr025.string_print01("The Gods intervene!");
                player = gbl.player_next_ptr;

                while (player != null)
                {
                    if (player.combat_team == 1)
                    {
                        player.in_combat = false;
                        player.health_status = Status.dead;

                        gbl.stru_1C9CD[ovr033.get_player_index(player)].field_3 = 0;
                    }

                    var_2 = ovr025.clear_actions(player);
                    player = player.next_player;
                }

                ovr033.sub_749DD(8, 0xff, gbl.stru_1D1BC.mapScreenTopY + 3, gbl.stru_1D1BC.mapScreenLeftX + 3);
            }

            return var_1;
        }
    }
}
