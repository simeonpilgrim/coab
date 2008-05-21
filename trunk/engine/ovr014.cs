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
            gbl.reset_byte_1D2C0 = false;

            ovr024.work_on_00(player, 18);

            player.field_19D = sub_3EF0D(gbl.byte_1D2C0);

            action.field_5 = player.field_DD;

            if (player.in_combat == true)
            {
                action.delay = (sbyte)(ovr024.roll_dice(6, 1) + ovr025.dexReactionAdj(player));

                if (action.delay < 1)
                {
                    action.delay = 1;
                }

                if ((((int)player.combat_team + 1) & gbl.area2_ptr.field_596) != 0)
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
            byte var_2 = player.initiative;

            if (player.in_combat == false)
            {
                var_2 += (byte)gbl.area2_ptr.field_6E4;
            }

            gbl.reset_byte_1D2C0 = true;

            if (var_2 < 1 || var_2 > 0x60)
            {
                var_2 = 1;
            }

            gbl.byte_1D2C0 = (byte)(var_2 *2);

            ovr024.work_on_00(player, 18);

            gbl.reset_byte_1D2C0 = false;

            return gbl.byte_1D2C0;
        }


        static void sub_3E192(byte arg_0, Player target, Player attacker)
        {
            gbl.damage = ovr024.roll_dice_save(attacker.field_19FArray(arg_0), attacker.field_19DArray(arg_0));
            gbl.damage += attacker.field_1A1Array(arg_0);

            if (gbl.damage < 0)
            {
                gbl.damage = 0;
            }

            if (sub_408D7(target, attacker) != 0)
            {
                gbl.damage *= (((attacker.thief_lvl + (attacker.field_117 * ovr026.sub_6B3D1(attacker))) - 1) / 4) + 2;
            }

            gbl.damage_flags = 0;
            ovr024.work_on_00(attacker, 4);
            ovr024.work_on_00(target, 5);
        }

        static Set unk_3E2EE = new Set(0x0002, new byte[] { 0xC0, 0x01 });

        static void backstab(bool attackHits, int attackDamge, int actualDamage, byte arg_6, Player target, Player attacker)
        {
            string text;

            if (arg_6 == 2)
            {
                text = "-Backstabs-";
            }
            else if (arg_6 == 3)
            {
                text = "slays helpless";
            }
            else
            {
                text = "Attacks";
            }

            ovr025.DisplayPlayerStatusString(false, 10, text, attacker);
            int line = 12;

            ovr025.displayPlayerName(false, line, 0x17, target);
            line++;

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
                seg041.press_any_key(text, true, 0, 10, line + 3, 0x26, line, 0x17);
            }

            line = gbl.textYCol + 1;

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
                ovr025.DisplayPlayerStatusString(false, line, "goes down", target);
                line += 2;

                if (target.health_status == Status.dying)
                {
                    seg041.displayString("and is Dying", 0, 10, line, 0x17);
                }

                if (unk_3E2EE.MemberOf((byte)target.health_status) == true)
                {
                    ovr025.DisplayPlayerStatusString(false, line, "is killed", target);
                }

                line += 2;

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

            int var_8 = ovr025.near_enemy(1, arg_0);

            for (int var_2 = 1; var_2 <= var_8; var_2++)
            {
                if (arg_0.in_combat == true)
                {
                    Player var_6 = gbl.player_array[gbl.byte_1D8B9[var_2]];

                    if (var_6.actions.guarding == true &&
                        ovr025.is_held(var_6) == false)
                    {
                        ovr033.redrawCombatArea(8, 2, ovr033.PlayerMapYPos(arg_0), ovr033.PlayerMapXPos(arg_0));

                        var_6.actions.guarding = false;

                        sub_3F94D(arg_0, var_6);

                        bool tmpBool;
                        sub_3F9DB(out tmpBool, null, 0, arg_0, var_6);
                    }
                }
            }
        }


        internal static void sub_3E748(byte direction, Player player)
        {
            byte player_index = ovr033.get_player_index(player);

            sbyte oldXPos = (sbyte)gbl.CombatMap[player_index].xPos;
            sbyte oldYPos = (sbyte)gbl.CombatMap[player_index].yPos;

            sbyte newXPos = (sbyte)(oldXPos + gbl.MapDirectionXDelta[direction]);
            sbyte newYPos = (sbyte)(oldYPos + gbl.MapDirectionYDelta[direction]);

            int costToMove = 0;
            if ((direction & 0x01) != 0)
            {
                // Diagonal walking...
                costToMove = gbl.BackGroundTiles[gbl.mapToBackGroundTile[newXPos, newYPos]].move_cost * 3;
            }
            else
            {
                costToMove = gbl.BackGroundTiles[gbl.mapToBackGroundTile[newXPos, newYPos]].move_cost * 2;
            }

            if (costToMove > player.actions.move)
            {
                player.actions.move = 0;
            }
            else
            {
                player.actions.move -= (byte)costToMove;
            }

            byte radius = 1;

            if (player.quick_fight != 0)
            {
                radius = 3;

                if (ovr033.CoordOnScreen(newYPos - gbl.mapToBackGroundTile.mapScreenTopY, newXPos - gbl.mapToBackGroundTile.mapScreenLeftX) == false &&
                    gbl.byte_1D910 == true)
                {
                    ovr033.redrawCombatArea(8, 2, oldYPos, oldXPos);
                }
            }

            if (gbl.byte_1D910 == true)
            {
                ovr033.draw_74572(player_index, 0, 0);
            }

            gbl.CombatMap[player_index].xPos = newXPos;
            gbl.CombatMap[player_index].yPos = newYPos;

            ovr033.sub_743E7();

            if (gbl.byte_1D910 == true)
            {
                ovr033.redrawCombatArea(8, radius, newYPos, newXPos);
            }

            player.actions.field_F = 0;
            player.actions.field_12 = 0;
            seg044.sound_sub_120E0(gbl.sound_a_188D2);

            sub_3E65D(player);

            if (player.in_combat == false ||
                ovr025.is_held(player) == true)
            {
                player.actions.move = 0;
            }

        }


        internal static void sub_3E954(int direction, Player player)
        {
            int var_25;
            Player var_23;
            Player player_ptr;
            int var_1B;
            byte var_1A;
            byte var_18;
            byte var_17;
            byte player_index;
            byte var_12;
            byte var_11;
            Affect var_10;
            const int var_D_size = 12 + 1;
            int[] var_D = new int[var_D_size];

            System.Array.Clear(var_D, 0, var_D_size);
            //seg051.FillChar(0, var_D_size, var_D);

            player_index = ovr033.get_player_index(player);
            int var_15 = ovr025.near_enemy(1, player);

            if (var_15 != 0)
            {
                for (var_18 = 1; var_18 <= var_15; var_18++)
                {
                    var_D[var_18] = gbl.byte_1D8B9[var_18];
                }

                gbl.CombatMap[player_index].xPos += gbl.MapDirectionXDelta[direction];
                gbl.CombatMap[player_index].yPos += gbl.MapDirectionYDelta[direction];

                int var_16 = ovr025.near_enemy(1, player);

                gbl.CombatMap[player_index].xPos -= gbl.MapDirectionXDelta[direction];
                gbl.CombatMap[player_index].yPos -= gbl.MapDirectionYDelta[direction];

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
                            var_25 = player_ptr.actions.direction + 10;

                            for (var_1B = player_ptr.actions.direction + 6; var_1B <= var_25; var_1B++)
                            {
                                if (var_12 != 0)
                                {
                                    if (player_ptr.actions.delay > 0 ||
                                        player_ptr.actions.field_F == 0 ||
                                        ovr032.CanSeeCombatant((byte)(var_1B % 8), ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player), ovr033.PlayerMapYPos(player_ptr), ovr033.PlayerMapXPos(player_ptr)) == true)
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
            bool ret_val;

            bool gets_away = false;

            if (ovr025.near_enemy(0xff, player) == 0)
            {
                gets_away = true;
            }
            else
            {
                int var_4 = sub_3E124(player) >> 1;
                int var_3 = sub_40E8F(player);

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
            bool var_5 = false;
            Item var_9 = null;
            byte var_2 = arg_0.field_19C;
            arg_0.field_19C = arg_0.field_11C;

            if (ovr025.is_weapon_ranged(arg_0) == true &&
                ovr025.sub_6906C(out var_9, arg_0) == true)
            {
                var_5 = true;
                byte var_4 = gbl.unk_1C020[arg_0.field_151.type].field_5;

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

            gbl.reset_byte_1D2C0 = false;
            ovr024.work_on_00(arg_0, 18);
            byte var_1 = sub_3EF0D(gbl.byte_1D2C0);

            if (var_5 == true &&
                var_9 != null)
            {
                int var_3 = 1;
                if (var_9 != null &&
                    var_9.count > var_3)
                {
                    var_3 = var_9.count;
                }

                if (var_3 < var_1 &&
                    var_9.count > 0)
                {
                    var_1 = (byte)var_3;
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
            if ((gbl.byte_1D8B7 & 1) == 1)
            {
                arg_0++;
            }

            return (byte)(arg_0 / 2);
        }


        internal static bool sub_3EF3D(Player target, Player attacker)
        {
            Player var_9;
            bool var_1;

            var_1 = false;

            if (attacker.field_19C < attacker.actions.field_5 &&
                target.field_E5 == 0)
            {
                if (ovr025.getTargetRange(target, attacker) == 1)
                {
                    int var_A = ovr025.near_enemy(1, attacker);
                    byte var_3 = 0;

                    int var_5 = 0xff;

                    for (int var_4 = 1; var_4 <= var_A; var_4++)
                    {
                        var_9 = gbl.player_array[gbl.byte_1D8B9[var_4]];

                        if (var_9 == target)
                        {
                            var_5 = var_4;
                        }

                        if (var_9.field_E5 == 0)
                        {
                            var_3++;
                        }
                    }

                    if (var_3 > attacker.field_19C)
                    {
                        if (var_3 > attacker.actions.field_5)
                        {
                            var_3 = attacker.actions.field_5;
                        }

                        ovr025.DisplayPlayerStatusString(true, 10, "sweeps", attacker);

                        var_9 = gbl.player_array[gbl.byte_1D8B9[1]];

                        if (var_9 != target)
                        {
                            gbl.byte_1D8B9[var_5] = gbl.byte_1D8B9[1];
                            gbl.byte_1D8B9[1] = ovr033.get_player_index(target);
                        }


                        for (int var_4 = 1; var_4 <= var_A; var_4++)
                        {
                            if (var_3 > 0 &&
                                gbl.player_array[gbl.byte_1D8B9[var_4]].field_E5 == 0)
                            {
                                var_9 = gbl.player_array[gbl.byte_1D8B9[var_4]];

                                sub_3F94D(var_9, attacker);

                                attacker.field_19C = 1;

                                sub_3F9DB(out gbl.byte_1D905, null, 0, var_9, attacker);
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
            bool ret_val = false;

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
                        Player old_target = player02.actions.target;

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
            output = null;

            byte var_4 = 0x0D;
            int var_2 = ovr025.near_enemy(0xff, player);
            bool result = false;

            for (int i = 1; i <= var_2; i++)
            {
                Player player_ptr = gbl.player_array[gbl.byte_1D8B9[i]];

                if (player_ptr.actions.field_10 == 0 &&
                    player_ptr.field_E9 > 0 &&
                    player_ptr.field_E9 < var_4)
                {
                    var_4 = player_ptr.field_E9;
                    output = player_ptr;
                    result = true;
                }
            }

            return result;
        }


        internal static void sub_3F4EB(Item arg_0, ref bool arg_4, byte arg_8, Player target, Player attacker)
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
            gbl.damage = 0;

            attacker.actions.field_8 = 1;

            if (ovr025.is_held(target) == true)
            {
                seg044.sound_sub_120E0(gbl.sound_7_188CC);

                while (attacker.field_19BArray(attacker.actions.field_4) == 0)
                {
                    attacker.actions.field_4--;
                }

                switch (attacker.actions.field_4)
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

                backstab(true, 1, (byte)(target.hit_point_current + 5), 3, target, attacker);
                ovr024.remove_invisibility(attacker);

                attacker.field_19C = 0;
                attacker.field_19D = 0;

                var_11 = 1;
                arg_4 = true;
            }
            else
            {
                if (attacker.field_151 != null && 
                    (target.field_DE > 0x80 || (target.field_DE & 7) > 1))
                {
                    var_10 = gbl.unk_1C020[attacker.field_151.type].ShallowClone();

                    attacker.field_19E = var_10.diceCount;
                    attacker.field_1A0 = var_10.diceSize;
                    attacker.damageBonus = (sbyte)((attacker.damageBonus - var_10.field_B) + var_10.field_4);
                }

                ovr025.sub_66C20(target);
                ovr024.work_on_00(target, 11);

                if (sub_408D7(target, attacker) != 0)
                {
                    var_14 = (byte)(target.field_19B - 4);
                }
                else
                {
                    if (target.actions.field_F > 1 &&
                        getTargetDirection(target, attacker) == target.actions.direction &&
                        target.actions.field_12 > 4)
                    {
                        var_13 = 1;
                    }

                    if (var_13 != 0)
                    {
                        var_14 = target.field_19B;
                    }
                    else
                    {
                        var_14 = target.ac;
                    }
                }

                sub_3FCED(ref var_14, target, attacker);
                var_17 = 0;
                if (var_13 != 0)
                {
                    var_17 = 1;
                }

                if (sub_408D7(target, attacker) != 0)
                {
                    var_17 = 2;
                }

                var_16 = attacker.actions.field_4;
                for (var_15 = var_16; var_15 >= 1; var_15--)
                {
                    while (attacker.field_19BArray(var_15) > 0 &&
                        var_12 == 0)
                    {
                        attacker.field_19BArraySet(var_15, (byte)(attacker.field_19BArray(var_15) - 1));
                        attacker.actions.field_4 = var_15;

                        switch (var_15)
                        {
                            case 1:
                                gbl.byte_1D901 += 1;
                                break;

                            case 2:
                                gbl.byte_1D902 += 1;
                                break;

                            default:
                                throw new System.NotImplementedException();
                        }

                        if (ovr024.sub_64245(var_14, target, attacker) ||
                            ovr025.is_held(target) == true)
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

                            seg044.sound_sub_120E0(gbl.sound_7_188CC);
                            var_11 = 1;
                            sub_3E192(var_15, target, attacker);
                            backstab(true, gbl.damage, gbl.damage, var_17, target, attacker);

                            if (target.in_combat == true)
                            {
                                ovr024.work_on_00(attacker, var_15 + 1);
                            }

                            if (target.in_combat == false)
                            {
                                var_12 = 1;
                            }

                            if (attacker.in_combat == false)
                            {
                                var_16 = 0;
                                attacker.field_19BArraySet(var_15, 0);
                            }
                        }
                    }
                }

                if (arg_0 != null && arg_0.count == 0 && arg_0.type == 0x64)
                {
                    attacker.field_19C = 0;
                    attacker.field_19D = 0;
                }

                if (var_11 == 0)
                {
                    seg044.sound_sub_120E0(gbl.sound_9_188D0);
                    backstab(false, 0, 0, var_17, target, attacker);
                }

                arg_4 = true;
                for (var_15 = 1; var_15 <= 2; var_15++)
                {
                    if (attacker.field_19BArray(var_15) > 0)
                    {
                        arg_4 = false;
                    }
                }
                attacker.actions.field_5 = 0;
            }

            if (attacker.in_combat == false)
            {
                arg_4 = true;
            }

            if (arg_4 == true)
            {
                arg_4 = ovr025.clear_actions(attacker);
            }
        }


        internal static void sub_3F94D(Player target, Player attacker)
        {
            byte var_2;
            byte var_1;

            target.actions.field_F++;

            var_2 = getTargetDirection(attacker, target);

            var_1 = (byte)(((var_2 - target.actions.direction) + 8) % 8);

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
                var_9 = getTargetDirection(player02, player01);

                player01.actions.direction = (byte)((var_9 + 4) % 8);
            }
            else if (ovr033.sub_74761(0, player01) == true)
            {
                var_9 = player01.actions.direction;

                if (arg_8 == 0)
                {
                    player01.actions.direction = (byte)((var_9 + 4) % 8);
                }
            }

            if (ovr033.sub_74761(0, player01) == true)
            {
                ovr033.draw_74B3F(0, 0, var_9, player01);
            }

            var_9 = getTargetDirection(player01, player02);
            ovr025.hitpoint_ac(player02);

            ovr033.draw_74B3F(0, 1, var_9, player02);

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
                        if (ovr025.is_weapon_ranged_melee(player02) == true &&
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
                ovr033.draw_74B3F(1, 1, player02.actions.direction, player02);
                ovr033.draw_74B3F(0, 0, player02.actions.direction, player02);
            }
        }


        internal static void sub_3FCED(ref byte arg_0, Player target, Player attacker)
        {
            int weapon_range;
            int range = ovr025.getTargetRange(target, attacker);

            if (ovr025.is_weapon_ranged(attacker) == true)
            {
                weapon_range = (gbl.unk_1C020[attacker.field_151.type].field_C - 1) / 3;
            }
            else
            {
                weapon_range = range;
            }

            if (range > weapon_range)
            {
                range -= weapon_range;
                arg_0 += 2;
            }

            if (range > weapon_range)
            {
                range -= weapon_range;
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

                ovr033.AtMapXY(out var_D, out var_C, mapY, mapX);

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

        static Affects[] unk_18ADB = { Affects.bless, Affects.snake_charm, Affects.paralyze, Affects.sleep, Affects.helpless }; // seg600:27CB first is filler (off by 1)

        internal static bool sub_4001C(Struct_1D183 arg_0, byte arg_4, QuickFight quick_fight, byte arg_8)
        {
            byte var_9;
            Player var_7;

            bool var_2 = false;
            if (quick_fight == QuickFight.False)
            {
                byte var_A = arg_8 != 0x53 ? (byte)1 : (byte)0;

                aim_menu(arg_0, out var_2, var_A, arg_4, 0, ovr023.sub_5CDE5(arg_8), gbl.player_ptr);
                gbl.player_ptr.actions.target = arg_0.field_0;
            }
            else if (gbl.unk_19AEC[arg_8].field_E == 0)
            {
                arg_0.field_0 = gbl.player_ptr;

                if (arg_8 != 3 || sub_3FDFE(out arg_0.field_0, gbl.player_ptr))
                {
                    arg_0.mapX = ovr033.PlayerMapXPos(arg_0.field_0);
                    arg_0.mapY = ovr033.PlayerMapYPos(arg_0.field_0);
                    var_2 = true;
                }
            }
            else
            {
                var_9 = 1;

                while (var_9 > 0 &&
                        var_2 == false)
                {
                    bool var_3 = true;

                    if (sub_41E44(1, 0, ovr023.sub_5CDE5(arg_8), gbl.player_ptr) == true)
                    {
                        var_7 = gbl.player_ptr.actions.target;

                        if (ovr025.is_held(var_7) == true)
                        {
                            for (int i = 1; i <= 4; i++)
                            {
                                if (gbl.unk_19AEC[arg_8].field_A == unk_18ADB[i])
                                {
                                    var_3 = false;
                                }
                            }
                        }

                        if (var_3 == true)
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


            if (var_2 == true)
            {
                gbl.targetX = arg_0.mapX;
                gbl.targetY = arg_0.mapY;
            }
            else
            {
                arg_0.Clear();
            }

            return var_2;
        }

        internal static void target(out bool arg_0, QuickFight quick_fight, byte arg_6)
        {
            byte var_E;
            Struct_1D183 var_C = new Struct_1D183();
            byte var_5;
            int var_4;
            byte var_1;

            arg_0 = true;
            gbl.sp_target_count = 0;
            gbl.byte_1D2C7 = 0;

            gbl.targetX = ovr033.PlayerMapXPos(gbl.player_ptr);
            gbl.targetY = ovr033.PlayerMapYPos(gbl.player_ptr);

            byte tmp1 = (byte)(gbl.unk_19AEC[arg_6].field_6 & 0x0F);

            if (tmp1 == 0)
            {
                gbl.sp_targets[1] = gbl.player_ptr;
                gbl.sp_target_count = 1;
            }
            else if (tmp1 == 5)
            {
                var_5 = 0;
                int sp_target_index = 0;

                if (arg_6 == 0x4F)
                {
                    var_4 = ovr025.spell_target_count(0x4F);
                }
                else
                {
                    var_4 = ovr024.roll_dice(4, 2);
                }

                var_E = 0;

                do
                {
                    if (sub_4001C(var_C, 0, quick_fight, arg_6) == true)
                    {
                        bool found = false;

                        for (int index = 1; index <= sp_target_index; index++)
                        {
                            if (gbl.sp_targets[index] == var_C.field_0)
                            {
                                found = true;
                            }
                        }

                        if (found == false)
                        {
                            sp_target_index++;

                            gbl.sp_targets[sp_target_index] = var_C.field_0;

                            gbl.targetX = ovr033.PlayerMapXPos(var_C.field_0);
                            gbl.targetY = ovr033.PlayerMapYPos(var_C.field_0);
                            gbl.sp_target_count++;

                            if (arg_6 != 0x4f)
                            {
                                byte al = gbl.sp_targets[sp_target_index].field_E5;

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
                                byte al = gbl.sp_targets[sp_target_index].field_DE;

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

                            if (sp_target_index > 1 && var_5 > var_4)
                            {
                                var_E = 1;
                            }
                        }
                        else
                        {
                            if (quick_fight != 0)
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

                if (sub_4001C(var_C, 0, quick_fight, arg_6) == true)
                {
                    if (gbl.player_ptr.actions.target != null)
                    {
                        gbl.sp_targets[1] = gbl.player_ptr.actions.target;
                        gbl.sp_target_count = 1;
                    }
                    else
                    {
                        /* TODO it doesn't make sense to mask the low nibble then shift it out */
                        ovr032.Rebuild_SortedCombatantList(gbl.mapToBackGroundTile, 1, 0xff, (short)((gbl.unk_19AEC[arg_6].field_6 & 0x0f) >> 4), gbl.targetY, gbl.targetX);
                        // test with it how it would make sense...
						//ovr032.Rebuild_SortedCombatantList(gbl.mapToBackGroundTile, 1, 0xff, (short)((gbl.unk_19AEC[arg_6].field_6 & 0xf0) >> 4), gbl.targetY, gbl.targetX);

                        for (int i = 1; i <= gbl.sortedCombatantCount; i++)
                        {
                            gbl.sp_targets[i] = gbl.player_array[gbl.SortedCombatantList[i].player_index];
                        }

                        gbl.sp_target_count = gbl.sortedCombatantCount;
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
                if (sub_4001C(var_C, 1, quick_fight, arg_6) == true)
                {
                    ovr032.Rebuild_SortedCombatantList(gbl.mapToBackGroundTile, 1, 0xff, (short)(gbl.unk_19AEC[arg_6].field_6 & 7), gbl.targetY, gbl.targetX);

                    for (int i = 1; i <= gbl.sortedCombatantCount; i++)
                    {
                        gbl.sp_targets[i] = gbl.player_array[gbl.SortedCombatantList[i].player_index];
                    }

                    gbl.sp_target_count = gbl.sortedCombatantCount;
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
                int sp_target_index = 0;

                while (var_1 > 0)
                {
                    if (sub_4001C(var_C, 0, quick_fight, arg_6) == true)
                    {
                        bool found = false;

                        for (int index = 1; index <= sp_target_index; index++)
                        {
                            if (gbl.sp_targets[index] == var_C.field_0)
                            {
                                found = true;
                            }
                        }

                        if (found == false)
                        {
                            sp_target_index++;
                            gbl.sp_targets[sp_target_index] = var_C.field_0;
                            var_1 -= 1;

                            gbl.targetX = ovr033.PlayerMapXPos(var_C.field_0);
                            gbl.targetY = ovr033.PlayerMapYPos(var_C.field_0);
                        }
                        else
                        {
                            if (quick_fight == 0)
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

                gbl.sp_target_count = sp_target_index;

                if (sp_target_index == 0)
                {
                    arg_0 = false;
                }

                gbl.targetX = ovr033.PlayerMapXPos(gbl.sp_targets[sp_target_index]);
                gbl.targetY = ovr033.PlayerMapYPos(gbl.sp_targets[sp_target_index]);
            }
        }


        internal static void spell_menu3(out bool arg_0, QuickFight quick_fight, byte arg_6)
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

            if (quick_fight == QuickFight.False)
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
                    ovr023.sub_5D2E1(ref arg_0, 1, quick_fight, var_1);

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
                getTargetDirection(arg_0, arg_4) == arg_0.actions.direction)
            {
                var_1 = 1;
            }
            else
            {
                var_1 = 0;
            }

            return var_1;
        }


        internal static byte getTargetDirection(Player playerB, Player playerA) /* sub_409BC */
        {
            int plyr_a_x = ovr033.PlayerMapXPos(playerA);
            int plyr_a_y = ovr033.PlayerMapYPos(playerA);

            int plyr_b_x = ovr033.PlayerMapXPos(playerB);
            int plyr_b_y = ovr033.PlayerMapYPos(playerB);

            int diff_x = System.Math.Abs(plyr_b_x - plyr_a_x);
            int diff_y = System.Math.Abs(plyr_b_y - plyr_a_y);

            byte direction = 0;
            bool solved = false;

            while (solved == false)
            {
                switch (direction)
                {
                    case 0:
                        if (plyr_b_y > plyr_a_y ||
                            ((0x26A * diff_x) / 0x100) > diff_y)
                        {
                            solved = false;
                        }
                        else
                        {
                            solved = true;
                        }
                        break;

                    case 2:
                        if (plyr_b_x < plyr_a_x ||
                            ((0x6A * diff_x) / 0x100) < diff_y)
                        {
                            solved = false;
                        }
                        else
                        {
                            solved = true;
                        }
                        break;

                    case 4:
                        if (plyr_b_y < plyr_a_y ||
                            ((0x26A * diff_x) / 0x100) > diff_y)
                        {
                            solved = false;
                        }
                        else
                        {
                            solved = true;
                        }
                        break;

                    case 6:
                        if (plyr_b_x > plyr_a_x ||
                            ((0x6A * diff_x) / 0x100) < diff_y)
                        {
                            solved = false;
                        }
                        else
                        {
                            solved = true;
                        }
                        break;

                    case 1:
                        if (plyr_b_y > plyr_a_y ||
                            plyr_b_x < plyr_a_x ||
                            ((0x26A * diff_x) / 0x100) < diff_y ||
                            ((0x6A * diff_x) / 0x100) > diff_y)
                        {
                            solved = false;
                        }
                        else
                        {
                            solved = true;
                        }
                        break;

                    case 3:
                        if (plyr_b_y < plyr_a_y ||
                            plyr_b_x < plyr_a_x ||
                            ((0x26a * diff_x) / 0x100) < diff_y ||
                            ((0x6a * diff_x) / 0x100) > diff_y)
                        {
                            solved = false;
                        }
                        else
                        {
                            solved = true;
                        }
                        break;

                    case 5:
                        if (plyr_b_y < plyr_a_y ||
                            plyr_b_x > plyr_a_x ||
                            ((0x26a * diff_x) / 0x100) < diff_y ||
                            ((0x6a * diff_x) / 0x100) > diff_y)
                        {
                            solved = false;
                        }
                        else
                        {
                            solved = true;
                        }
                        break;

                    case 7:
                        if (plyr_b_y > plyr_a_y ||
                            plyr_b_x > plyr_a_x ||
                            ((0x26a * diff_x) / 0x100) < diff_y ||
                            ((0x6a * diff_x) / 0x100) > diff_y)
                        {
                            solved = false;
                        }
                        else
                        {
                            solved = true;
                        }
                        break;
                }

                if (solved == false)
                {
                    direction++;
                }
            }

            return direction;
        }


        internal static void sub_40BF1(Item item, Player playerA, Player playerB) /* sub_40BF1 */
        {
            byte var_3;
            byte var_1;

            seg044.sound_sub_120E0(gbl.sound_c_188D6);

            var_3 = getTargetDirection(playerA, playerB);

            int frame_count = 1;
            int delay = 10;
            var_1 = 0x0D;

            switch (item.type)
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
                            ovr025.load_missile_dax((var_3 == 5), 0, 1, var_1 + 1);
                        }
                        else
                        {
                            ovr025.load_missile_dax((var_3 == 7), 0, 0, var_1 + 1);
                        }
                    }
                    else
                    {
                        ovr025.load_missile_dax(false, 0, var_3 >> 2, var_1 + (var_3 % 4));
                    }
                    seg044.sound_sub_120E0(gbl.sound_c_188D6);
                    break;

                case 2:
                case 7:
                case 14:
                    ovr025.sub_67A59(var_1 + 3);
                    frame_count = 4;
                    delay = 50;
                    seg044.sound_sub_120E0(gbl.sound_9_188D0);
                    break;


                case 0x55:
                case 0x56:
                    ovr025.sub_67A59(var_1 + 4);
                    frame_count = 4;
                    delay = 50;
                    seg044.sound_sub_120E0(gbl.sound_6_188CA);
                    break;

                case 0x65:
                case 0x2F:
                case 0x62:
                    var_1++;
                    ovr025.load_missile_dax(false, 0, 0, var_1 + 7);
                    ovr025.load_missile_dax(false, 1, 1, var_1 + 7);
                    frame_count = 2;
                    delay = 10;
                    seg044.sound_sub_120E0(gbl.sound_6_188CA);

                    break;

                default:
                    ovr025.load_missile_dax(false, 0, 0, var_1 + 7);
                    ovr025.load_missile_dax(false, 1, 1, var_1 + 7);
                    frame_count = 2;
                    delay = 20;
                    seg044.sound_sub_120E0(gbl.sound_9_188D0);
                    break;
            }
            
            ovr025.draw_missile_attack(delay, frame_count, ovr033.PlayerMapYPos(playerA), ovr033.PlayerMapXPos(playerA),
                ovr033.PlayerMapYPos(playerB), ovr033.PlayerMapXPos(playerB));
        }


        internal static void calc_enemy_health_percentage() /* sub_40E00 */
        {
            int maxTotal = 0;
            int currentTotal = 0;
            Player player = gbl.player_next_ptr;

            while (player != null)
            {
                if (player.combat_team == CombatTeam.Enemy)
                {
                    if (player.in_combat == true)
                    {
                        currentTotal += player.hit_point_current;
                    }

                    maxTotal += player.hit_point_max;
                }

                player = player.next_player;
            }

            if (maxTotal > 0)
            {
                gbl.enemyHealthPercentage = (byte)(((20 * currentTotal) / maxTotal) * 5);
            }
        }


        internal static int sub_40E8F(Player arg_0)
        {
            int var_2 = 0;
            Player player = gbl.player_next_ptr;

            while (player != null)
            {
                if (ovr025.opposite_team(arg_0) == player.combat_team &&
                    player.in_combat == true)
                {
                    int var_3 = sub_3E124(player) /2;

                    if (var_3 > var_2)
                    {
                        var_2 = var_3;
                    }
                }

                player = player.next_player;
            }

            return var_2;
        }


        internal static bool sub_40F1F(Player playerA, Player playerB)
        {
            Player player;
            bool var_1;

            if (ovr025.opposite_team(playerA) == playerB.combat_team ||
                playerB.quick_fight == QuickFight.True)
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
                        player.combat_team = CombatTeam.Enemy;
                        player.actions.target = null;
                    }

                    player = player.next_player;
                }

                ovr025.count_teams();
            }

            return var_1;
        }


        internal static char aim_sub_menu(byte arg_0, byte arg_2, byte arg_4, byte arg_6, Player target, Player playerA) /* Aim_menu */
        {
            Item var_2F;
            bool var_2B;
            char var_1;

            string text = string.Empty;
            int range = ovr025.getTargetRange(target, playerA);
            int direction = getTargetDirection(target, playerA);

            if (arg_4 != 0)
            {
                string range_txt = "Range = " + range.ToString() + "  ";
                seg041.displayString(range_txt, 0, 10, 0x17, 0);
            }

            if (range <= arg_6)
            {
                if (arg_4 == 0)
                {
                    if (arg_0 != 0)
                    {
                        text = "Target ";
                    }
                    else
                    {
                        text = string.Empty;
                    }
                }
                else if (target != playerA)
                {
                    if (ovr025.is_weapon_ranged(playerA) == false)
                    {
                        text = "Target ";
                    }
                    else
                    {
                        if (ovr025.sub_6906C(out var_2F, playerA) == true &&
                            (ovr025.near_enemy(1, playerA) == 0 || ovr025.is_weapon_ranged_melee(playerA) == true))
                        {
                            text = "Target ";
                        }
                    }
                }
            }
            
            text = "Next Prev Manual " + text + "Center Exit";
            ovr033.sub_75356(true, 3, target);
            gbl.byte_1D90F = true;
            ovr025.hitpoint_ac(target);

            var_1 = ovr027.displayInput(out var_2B, false, 1, 15, 10, 13, text, "Aim:");

            return var_1;
        }


        internal static void sub_411D8(Struct_1D183 arg_0, out bool arg_4, byte arg_8, Player arg_A, Player arg_E)
        {
            Item var_5 = null;
            arg_4 = true;

            if (arg_8 == 1 &&
                sub_40F1F(arg_A, arg_E) == false)
            {
                arg_4 = false;
            }

            if (arg_4 == true)
            {
                arg_0.field_0 = arg_A;
                arg_0.mapX = ovr033.PlayerMapXPos(arg_A);
                arg_0.mapY = ovr033.PlayerMapYPos(arg_A);
                gbl.mapToBackGroundTile.field_4 = false;

                ovr033.redrawCombatArea(8, 3, gbl.mapToBackGroundTile.mapScreenTopY + 3, gbl.mapToBackGroundTile.mapScreenLeftX + 3);

                if (arg_8 == 1)
                {
                    if (sub_3EF3D(arg_A, arg_E) == true)
                    {
                        arg_4 = ovr025.clear_actions(arg_E);
                    }
                    else
                    {
                        sub_3F94D(arg_A, arg_E);

                        if (ovr025.is_weapon_ranged(arg_E) == true &&
                            ovr025.sub_6906C(out var_5, arg_E) == true &&
                            ovr025.is_weapon_ranged_melee(arg_E) == true &&
                            ovr025.getTargetRange(arg_A, arg_E) == 0)
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

        static Set asc_41342 = new Set(0x000B, new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20, 0x00, 0x10 });

        internal static void Target(Struct_1D183 arg_0, out bool arg_4, byte arg_8, byte arg_A, byte arg_C, byte arg_E, Player player02, Player player01)
        {
            Item dummyItem;

            arg_0.Clear();

            int posX = ovr033.PlayerMapXPos(player02);
            int posY = ovr033.PlayerMapYPos(player02);

            char input_key = ' ';
            byte dir = 8;

            arg_4 = false;

            gbl.mapToBackGroundTile.field_4 = true;
            gbl.mapToBackGroundTile.size = 1;

            while (asc_41342.MemberOf(input_key) == false)
            {
                ovr033.redrawCombatArea(dir, 3, posY, posX);
                posX += gbl.MapDirectionXDelta[dir];
                posY += gbl.MapDirectionYDelta[dir];

                if (posX < 0)
                {
                    posX = 0;
                }

                if (posY < 0)
                {
                    posY = 0;
                }

                if (posX > 0x31)
                {
                    posX = 0x31;
                }

                if (posY > 0x18)
                {
                    posY = 0x18;
                }

                byte groundTile;
                byte playerAtXY;

                ovr033.AtMapXY(out groundTile, out playerAtXY, posY, posX);
                seg043.clear_keyboard();
                bool can_target = false;
                int range = 255;

                int tmpX = posX;
                int tmpY = posY;

                if (ovr032.canReachTarget(gbl.mapToBackGroundTile, ref range, ref tmpY, ref tmpX, ovr033.PlayerMapYPos(player01), ovr033.PlayerMapXPos(player01)) == true)
                {
                    can_target = true;

                    if (arg_C != 0)
                    {
                        string range_text = "Range = " + (range /2).ToString() + "  ";

                        seg041.displayString(range_text, 0, 10, 0x17, 0);
                    }
                }
                else
                {
                    if (arg_C != 0)
                    {
                        seg037.draw8x8_clear_area(0x17, 0x27, 0x17, 0);
                    }
                }

                range /= 2;
                player02 = null;

                if (can_target)
                {
                    if (playerAtXY > 0)
                    {
                        player02 = gbl.player_array[playerAtXY];
                    }
                    else if (groundTile == 0x1f)
                    {
                        for (int i = 1; i <= gbl.byte_1D1BB; i++)
                        {
                            if (gbl.unk_1D183[i].mapX == posX &&
                                gbl.unk_1D183[i].mapY == posY)
                            {
                                player02 = gbl.unk_1D183[i].field_0;
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

                if (arg_E < range ||
                    gbl.BackGroundTiles[groundTile].move_cost == 0xff)
                {
                    can_target = false;
                }

                if (player02 != null)
                {
                    if (sub_3F143(player02, player01) == false ||
                        arg_8 == 0)
                    {
                        can_target = false;
                    }

                    if (arg_C == 1)
                    {
                        if (player01 == player02 ||
                            (playerAtXY == 0 && groundTile == 0x1f))
                        {
                            can_target = false;
                        }
                        else if (ovr025.is_weapon_ranged(player01) == true &&
                             (ovr025.sub_6906C(out dummyItem, player01) == true ||
                             (ovr025.near_enemy(1, player01) >= 0 &&
                                ovr025.is_weapon_ranged_melee(player01) == false)))
                        {
                            can_target = false;
                        }
                    }
                }
                else if (arg_A == 0)
                {
                    can_target = false;
                }

                string var_29 = "Center Exit";

                if (can_target)
                {
                    var_29 = "Target " + var_29;
                }

                input_key = ovr027.displayInput(out gbl.byte_1D905, false, 1, 15, 10, 13, var_29, "(Use Cursor keys) ");

                switch (input_key)
                {
                    case '\r':
                    case 'T':
                        gbl.mapToBackGroundTile.field_4 = false;

                        if (can_target)
                        {
                            arg_0.mapX = posX;
                            arg_0.mapY = posY;

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
                                sub_411D8(arg_0, out arg_4, arg_C, arg_0.field_0, player01);
                            }
                            else
                            {
                                arg_4 = true;
                            }
                        }

                        if (can_target == false ||
                            arg_4 == false)
                        {
                            ovr033.sub_7431C(posY, posX);
                            arg_4 = false;
                            arg_0.Clear();
                        }
                        break;

                    case 'H':
                        dir = 0;
                        break;

                    case 'I':
                        dir = 1;
                        break;

                    case 'M':
                        dir = 2;
                        break;

                    case 'Q':
                        dir = 3;
                        break;

                    case 'P':
                        dir = 4;
                        break;

                    case 'O':
                        dir = 5;
                        break;

                    case 'K':
                        dir = 6;
                        break;

                    case 'G':
                        dir = 7;
                        break;

                    case '\0':
                    case 'E':
                        ovr033.sub_7431C(posY, posX);
                        arg_0.Clear();
                        arg_4 = false;
                        break;

                    case 'C':
                        ovr033.redrawCombatArea(8, 0, posY, posX);
                        dir = 8;
                        break;

                    default:
                        dir = 8;
                        break;
                }
            }
        }


        internal static void copy_sorted_players(Player player, SortedCombatant[] sorted_list, out int sorted_count) /* sub_4188F */
        {
            ovr032.Rebuild_SortedCombatantList(gbl.mapToBackGroundTile, 
                ovr033.PlayerMapSize(player), 0xff, 0x7F,
                ovr033.PlayerMapYPos(player),
                ovr033.PlayerMapXPos(player));

            sorted_count = gbl.sortedCombatantCount;

            for (int i = 1; i <= sorted_count; i++)
            {
                sorted_list[i - 1] = new SortedCombatant(gbl.SortedCombatantList[i]);
            }
        }


        internal static Player step_combat_list(bool arg_2, int step,
            int sorted_count, ref int list_index, 
            ref int attacker_x, ref int attacker_y,
            SortedCombatant[] sorted_list) /* sub_41932 */
        {
            Player player_ptr;

            if (arg_2 == true)
            {
                attacker_x = gbl.CombatMap[sorted_list[list_index-1].player_index].xPos;
                attacker_y = gbl.CombatMap[sorted_list[list_index-1].player_index].yPos;
            }
            else
            {
                ovr033.sub_7431C(attacker_y, attacker_x);
            }

            list_index += step;

            if (list_index == 0)
            {
                list_index = sorted_count;
            }

            if (list_index > sorted_count)
            {
                list_index = 1;
            }

            player_ptr = gbl.player_array[sorted_list[list_index-1].player_index];

            int target_y = gbl.CombatMap[sorted_list[list_index-1].player_index].xPos;
            int target_x = gbl.CombatMap[sorted_list[list_index-1].player_index].yPos;

            if (arg_2 == true)
            {
                ovr025.draw_missile_attack(0, 1, target_x, target_y, attacker_y, attacker_x);
                attacker_x = target_y;
                attacker_y = target_x;
            }

            return player_ptr;
        }

        static Set unk_41AE5 = new Set(0x0009, new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20 });
        static Set unk_41B05 = new Set(0x0803, new byte[] { 0x80, 0xAB, 3 });

        internal static void aim_menu(Struct_1D183 arg_0, out bool arg_4, byte arg_8, byte arg_A, byte arg_C, byte arg_E, Player arg_10) /* sub_41B25 */
        {
            Player player_ptr; /* var_E5 */ 
            int attacker_y = 0;
            int attacker_x = 0;
            int sorted_count;
            byte var_D9;
            SortedCombatant[] sorted_list = new SortedCombatant[gbl.MaxSortedCombatantCount];

            ovr025.load_missile_dax(false, 0, 0, 0x19);

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

            copy_sorted_players(arg_10, sorted_list, out sorted_count);

            int list_index = 1;
            int next_prev_step = 0;
            int target_step = 0;

            player_ptr = step_combat_list(true, next_prev_step, sorted_count, ref list_index, ref attacker_x, ref attacker_y, sorted_list);

            next_prev_step = 1;
            char input = ' ';

            while (arg_4 == false && unk_41AE5.MemberOf(input) == false)
            {
                if (sub_3F143(player_ptr, arg_10) == false)
                {
                    player_ptr = step_combat_list(false, next_prev_step, sorted_count, ref list_index, ref attacker_x, ref attacker_y, sorted_list);
                }
                else
                {
                    input = aim_sub_menu(arg_8, arg_A, arg_C, var_D9, player_ptr, arg_10);

                    if (gbl.displayInput_specialKeyPressed == false)
                    {
                        switch (input)
                        {
                            case 'N':
                                next_prev_step = 1;
                                target_step = 1;
                                break;

                            case 'P':
                                next_prev_step = -1;                                
                                target_step = -1;
                                break;

                            case 'M':
                            case 'H':
                            case 'K':
                            case 'G':
                            case 'O':
                            case 'Q':
                            case 'I':
                                Target(arg_0, out arg_4, arg_8, arg_A, arg_C, var_D9, player_ptr, arg_10);
                                ovr025.load_missile_dax(false, 0, 0, 0x19);

                                copy_sorted_players(arg_10, sorted_list, out sorted_count);
                                target_step = 0;
                                break;

                            case 'T':
                                sub_411D8(arg_0, out arg_4, arg_C, player_ptr, arg_10);
                                ovr025.load_missile_dax(false, 0, 0, 0x19);

                                copy_sorted_players(arg_10, sorted_list, out sorted_count);
                                target_step = 0;
                                break;

                            case 'C':
                                ovr033.redrawCombatArea(8, 0, ovr033.PlayerMapYPos(player_ptr), ovr033.PlayerMapXPos(player_ptr));
                                target_step = 0;
                                break;
                        }
                    }
                    else if (unk_41B05.MemberOf(input) == true)
                    {
                        Target(arg_0, out arg_4, arg_8, arg_A, arg_C, var_D9, player_ptr, arg_10);
                        ovr025.load_missile_dax(false, 0, 0, 0x19);
                        copy_sorted_players(arg_10, sorted_list, out sorted_count);
                        target_step = 0;
                    }

                    ovr033.sub_7431C(ovr033.PlayerMapYPos(player_ptr), ovr033.PlayerMapXPos(player_ptr));

                    player_ptr = step_combat_list((arg_4 == false && unk_41AE5.MemberOf(input) == false), target_step,
                        sorted_count, ref list_index, ref attacker_x, ref attacker_y, sorted_list);
                }
            }

            if (arg_C != 0)
            {
                seg037.draw8x8_clear_area(0x17, 0x27, 0x17, 0);
            }
        }


        internal static bool sub_41E44(byte arg_0, byte arg_2, byte arg_4, Player player)
        {
            byte var_7 = 0;
            bool var_6 = false;
            byte var_5 = 0;

            Player target = player.actions.target;

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
                    gbl.mapToBackGroundTile.field_6 = 1;
                }

                int var_3 = 0x14;
                int var_2 = ovr025.near_enemy(arg_4, player);

                while (var_3 > 0 && var_6 == false && var_2 > 0)
                {
                    var_3--;
                    int roll = ovr024.roll_dice(var_2, 1);

                    if (gbl.byte_1D8B9[roll] > 0)
                    {
                        target = gbl.player_array[gbl.byte_1D8B9[roll]];

                        if ((arg_2 != 0 && gbl.mapToBackGroundTile.field_6 != 0) ||
                            sub_3F143(target, player) == true)
                        {
                            var_6 = true;
                            player.actions.target = target;
                        }
                        else
                        {
                            gbl.byte_1D8B9[roll] = 0;
                            
                            for (int i = 1; i <= var_2; i++)
                            {
                                if (gbl.byte_1D8B9[i] > 0)
                                {
                                    var_6 = true;
                                }
                            }

                            if (var_6 == true)
                            {
                                var_6 = false;
                            }
                            else
                            {
                                var_2 = 0;
                            }
                        }
                    }
                }

                if (var_7 == 0)
                {
                    var_7 = 1;
                }
            }

            gbl.mapToBackGroundTile.field_6 = 0;

            return var_6;
        }


        internal static void engulfs(byte arg_0, object param, Player attacker)
        {
            Affect dummyAffect;
            Player target = attacker.actions.target;

            if (gbl.byte_1D2CA == 2 &&
                target.in_combat == true &&
                ovr025.find_affect(out dummyAffect, Affects.affect_3a, target) == false &&
                ovr025.find_affect(out dummyAffect, Affects.affect_0d, target) == false)
            {
                target = attacker.actions.target;
                ovr025.DisplayPlayerStatusString(true, 12, "engulfs " + target.name, attacker);
                ovr024.add_affect(false, ovr033.get_player_index(target), 0, Affects.affect_3a, target);

                ovr024.CallSpellJumpTable(0, null, target, Affects.affect_3a);
                ovr024.add_affect(false, ovr024.roll_dice(4, 2), 0, Affects.affect_0d, target);
                ovr024.add_affect(true, ovr033.get_player_index(target), 0, Affects.affect_8b, attacker);
            }
        }


        internal static void sub_42159(int icon_id, Player target, Player attacker)
        {
            ovr025.sub_67A59(icon_id + 13);

            ovr025.draw_missile_attack(0x1E, 1, 
                ovr033.PlayerMapYPos(target), ovr033.PlayerMapXPos(target),
                ovr033.PlayerMapYPos(attacker), ovr033.PlayerMapXPos(attacker));
        }


        internal static void sub_421C1(byte arg_2, ref int range, out bool var_5, ref Player player)
        {
            var_5 = true;
            if (sub_41E44(arg_2, 0, 0xff, player) == true)
            {
                int tmpX = ovr033.PlayerMapXPos(player.actions.target);
                int tmpY = ovr033.PlayerMapYPos(player.actions.target);

                if (ovr032.canReachTarget(gbl.mapToBackGroundTile, ref range, ref tmpY, ref tmpX, ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player)) == true)
                {
                    var_5 = false;
                }
            }
        }


        internal static void attack_or_kill(byte arg_0, object param, Player attacker)
        {
            Player target;
            int range = 0; /* simeon */

            byte var_4 = 0;
            byte var_1 = 4;
            bool var_5 = false;

            attacker.actions.target = null;
            sub_421C1(1, ref range, out var_5, ref attacker);

            do
            {
                target = attacker.actions.target;

                range = ovr025.getTargetRange(target, attacker);
                var_1--;

                if (target != null)
                {
                    if (range == 2 && (var_4 & 1) == 0)
                    {
                        var_4 |= 1;

                        ovr025.DisplayPlayerStatusString(true, 10, "fires a disintegrate ray", attacker);
                        sub_42159(5, target, attacker);

                        if (ovr024.do_saving_throw(0, 3, target) == false)
                        {
                            ovr024.sub_63014("is disintergrated", Status.gone, target);
                        }

                        sub_421C1(0, ref range, out var_5, ref attacker);
                    }
                    else if (range == 3 && (var_4 & 2) == 0)
                    {
                        var_4 |= 2;

                        ovr025.DisplayPlayerStatusString(true, 10, "fires a stone to flesh ray", attacker);
                        sub_42159(10, target, attacker);

                        if (ovr024.do_saving_throw(0, 1, target) == false)
                        {
                            ovr024.sub_63014("is Stoned", Status.stoned, target);
                        }

                        sub_421C1(0, ref range, out var_5, ref attacker);
                    }
                    else if (range == 4 && (var_4 & 4) == 0)
                    {
                        var_4 |= 4;

                        ovr025.DisplayPlayerStatusString(true, 10, "fires a death ray", attacker);
                        sub_42159(5, target, attacker);

                        if (ovr024.do_saving_throw(0, 0, target) == false)
                        {
                            ovr024.sub_63014("is killed", Status.dead, target);
                        }

                        sub_421C1(0, ref range, out var_5, ref attacker);
                    }
                    else if (range == 5 && (var_4 & 8) == 0)
                    {
                        var_4 |= 8;

                        ovr025.DisplayPlayerStatusString(true, 10, "wounds you", attacker);
                        sub_42159(5, target, attacker);

                        ovr024.damage_person(false, 0, (sbyte)(ovr024.roll_dice_save(8, 2) + 1), target);
                        sub_421C1(0, ref range, out var_5, ref attacker);
                    }
                    else if ((var_4 & 0x10) == 0)
                    {
                        ovr023.sub_5D2E1(ref var_5, 1, QuickFight.True, 0x54);
                        var_4 |= 0x10;
                    }
                    else if ((var_4 & 0x20) == 0)
                    {
                        ovr023.sub_5D2E1(ref var_5, 1, QuickFight.True, 0x37);
                        var_4 |= 0x20;
                    }
                    else if ((var_4 & 0x40) == 0)
                    {
                        ovr023.sub_5D2E1(ref var_5, 1, QuickFight.True, 0x15);
                        var_4 |= 0x40;
                    }
                }
            } while (var_1 > 0 && attacker.actions.target != null);
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
                    affect.call_spell_jump_list = false;

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
                    affect.call_spell_jump_list = false;
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
                ovr024.CallSpellJumpTable(0, null, gbl.spell_target, Affects.affect_3a);

                ovr024.add_affect(true, ovr033.get_player_index(gbl.spell_target), 0, Affects.affect_90, player);
            }
        }


        internal static bool god_intervene()
        {
            bool intervened = false;

            if (Cheats.allow_gods_intervene)
            {
                intervened = true;
                ovr025.string_print01("The Gods intervene!");
                Player player = gbl.player_next_ptr;

                while (player != null)
                {
                    if (player.combat_team == CombatTeam.Enemy)
                    {
                        player.in_combat = false;
                        player.health_status = Status.dead;

                        gbl.CombatMap[ovr033.get_player_index(player)].size = 0;
                    }

                    ovr025.clear_actions(player);
                    player = player.next_player;
                }

                ovr033.redrawCombatArea(8, 0xff, gbl.mapToBackGroundTile.mapScreenTopY + 3, gbl.mapToBackGroundTile.mapScreenLeftX + 3);
            }

            return intervened;
        }
    }
}
