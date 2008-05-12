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
            ovr025.ClearPlayerTextArea();

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
                ovr025.DisplayPlayerStatusString(true, 10, "flees in panic", player);
            }

            if (var_2 == true)
            {
                return;
            }

            if (sub_354AA(player))
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


        internal static bool sub_352AF(byte arg_2, int mapY, int mapX)
        {
            sbyte var_3;

            bool result = false;

            if (gbl.player_ptr.combat_team == 0)
            {
                var_3 = -2;
            }
            else
            {
                var_3 = 8;
            }

            ovr032.Rebuild_SortedCombatantList(gbl.mapToBackGroundTile, 1, 0xff, gbl.unk_19AEC[arg_2].field_F, mapY, mapX);
   
            for (int i = 1; i <= gbl.sortedCombatantCount; i++)
            {
                Player tmpPlayer = gbl.player_array[gbl.SortedCombatantList[i].player_index];
                Struct_19AEC tmpS = gbl.unk_19AEC[arg_2];

                if (ovr025.on_our_team(gbl.player_ptr) != tmpPlayer.combat_team &&
                    tmpS.field_8 != 1 &&
                    ovr024.do_saving_throw(var_3, tmpS.field_9, tmpPlayer) == false)
                {
                    result = true;
                }
            }

            return result;
        }


        internal static bool sub_353B1(byte arg_0, byte arg_2, Player arg_4)
        {
            Player var_5;

            bool var_1 = false;

            if (gbl.unk_19AEC[arg_2].field_D < arg_0)
            {
                if ((arg_2 != 3 && gbl.unk_19AEC[arg_2].field_E == 0) ||
                    (arg_2 == 3 && ovr014.sub_3FDFE(out var_5, arg_4)))
                {
                    var_1 = true;
                }
                else
                {
                    int var_6 = ovr025.near_enemy(ovr023.sub_5CDE5(arg_2), arg_4);

                    if (var_6 > 0)
                    {
                        if (gbl.unk_19AEC[arg_2].field_F == 0)
                        {
                            var_1 = true;
                        }
                        else
                        {
                            for (int i = 1; i <= var_6; i++)
                            {
                                if (sub_352AF(arg_2, gbl.CombatMap[gbl.byte_1D8B9[i]].yPos, gbl.CombatMap[gbl.byte_1D8B9[i]].xPos) == true)
                                {
                                    return var_1;
                                }
                            }
                            var_1 = true;
                        }
                    }
                }
            }

            return var_1;
        }


        internal static bool sub_354AA(Player player)
        {
            Item item_ptr;
            byte var_8;
            byte var_4;
            byte var_3;
            byte var_2;

            bool var_1 = false;

            Item var_14 = null;
            var_2 = 7;
            var_3 = ovr024.roll_dice(7, 1);

            int teamCount = (ovr025.on_our_team(player) == 0) ? gbl.friends_count : gbl.foe_count;
            if (player.actions.field_2 != 0 &&
                teamCount > 0 &&
                gbl.area_ptr.can_cast_spells == false)
            {
                for (var_4 = 1; var_4 <= var_3; var_4++)
                {
                    item_ptr = player.itemsPtr;

                    while (item_ptr != null && var_14 == null)
                    {
                        var_8 = (byte)item_ptr.affect_2;
                        byte var_7 = gbl.unk_1C020[item_ptr.type].field_0;


                        if (ovr023.item_is_scroll(item_ptr) == false &&
                            (int)item_ptr.affect_3 < 0x80 &&
                            item_ptr.readied &&
                            var_8 > 0)
                        {
                            if (var_8 > 0x38)
                            {
                                var_8 -= 0x17;
                            }

                            if (sub_353B1(var_2, var_8, player) )
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
                bool var_15 = false; /* simeon */
                ovr020.sub_56478(ref var_15, var_14);
                var_1 = true;
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
            byte[] var_55 = new byte[gbl.max_spells];

            var_5F = 0;

            if (player.actions.can_cast != 0)
            {
                for (var_60 = 1; var_60 < gbl.max_spells; var_60++)
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


            if (var_5F > 0 &&
                (player.field_F7 > 0x7F || gbl.magicOn == true))
            {
                if ((ovr025.on_our_team(player)== 0 ? gbl.friends_count : gbl.foe_count) > 0)
                {
                    while (var_5D <= var_5B && var_62 == 0)
                    {
                        for (var_5E = 1; var_5E < 4 && var_62 == 0; var_5E++)
                        {
                            var_60 = (byte)(ovr024.roll_dice(var_5F, 1) - 1);
                            var_61 = var_55[var_60];

                            if (sub_353B1(var_5A, var_61, player))
                            {
                                var_62 = var_61;
                            }
                        }

                        var_5A--;
                        var_5D++;
                    }
                }
            }

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

        static byte[] data_2B8 = new byte[]{ 
            0, 8, 7, 6, 1, 2, 8, 1, 2, 7,
            6, 7 ,1 ,8 ,6 ,2 ,1 ,7 ,8 ,2 ,
            6, 8 ,7, 6 ,5 ,4 ,8 ,1 ,2 ,3,  
            4 ,8 ,4 ,6 ,2 ,8 ,6, 4 ,0 ,8 ,
            0 ,6 ,2 ,8 ,2 ,0 ,4 ,0, 0 ,2 ,
            6 ,2 ,2 ,0 ,4 ,4 ,4 ,2, 6, 6 };/* actual from seg600:02BD - seg600:02F8 */

        internal static byte sub_3573B(out bool arg_0, byte arg_4, byte arg_6, Player player)
        {
            byte var_D;
            bool isPoisonousCloud;
            bool isNoxiousCloud;
            byte var_A;
            byte groundTile;
            byte playerIndex;
            byte playerDirection;
            byte var_6;
            Affect var_5;

            arg_0 = false;
            var_D = 0;

            var_6 = data_2B8[(player.actions.field_15 * 5) + arg_6];
            playerDirection = (byte)((arg_4 + var_6) % 8);

            ovr033.getGroundInformation(out isPoisonousCloud, out isNoxiousCloud, out groundTile, out playerIndex, playerDirection, player);

            if (groundTile == 0)
            {
                arg_0 = true;
            }
            else
            {
                if (gbl.BackGroundTiles[groundTile].move_cost == 0xff)
                {
                    return 0;
                }

                if ((playerDirection & 1) != 0)
                {
                    var_A = (byte)(gbl.BackGroundTiles[groundTile].move_cost * 3);
                }
                else
                {
                    var_A = (byte)(gbl.BackGroundTiles[groundTile].move_cost * 2);
                }

                if (playerIndex == 0 && var_A < player.actions.move)
                {
                    if (isNoxiousCloud == true &&
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


                    if (isPoisonousCloud == true &&
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
                }
            }

            return var_D;
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
                    if (player.field_F7 < 0x80 ||
                       (player.field_F7 > 0x7F && gbl.enemyHealthPercentage <= (ovr024.roll_dice(100, 1) + gbl.byte_1D2CC)) ||
                        player.combat_team == 1)
                    {
                        if (player.actions.field_14 != 0 ||
                            player.field_159 != null ||
                            player._class != ClassId.magic_user)
                        {
                            if (player.actions.field_14 == 0)
                            {
                                var_1 = ovr014.getTargetDirection(player.actions.target, player);
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

                                ovr033.draw_74B3F(0, 0, var_2, player);
                                ovr014.sub_3E954(player.actions.direction, player);

                                if (player.in_combat == false)
                                {
                                    var_5 = ovr025.clear_actions(player);
                                }
                                else
                                {
                                    if (player.actions.move > 0)
                                    {
                                        ovr014.sub_3E748(player.actions.direction, player);
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
                }

                var_5 = sub_361F7(player);
            }
        }


        internal static bool sub_35DB1(Player player)
        {
            byte var_13;
            Player player01;
            Item var_C;
            byte var_8;
            byte var_7;
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

                    if (player01 != null &&
                        (player01.in_combat == false ||
                        player01.combat_team == 0))
                    {
                        player01 = null;
                    }

                    if (player01 != null &&
                        ovr014.sub_3F143(player01, player) == true)
                    {
                        int tmpX = ovr033.PlayerMapXPos(player01);
                        int tmpY = ovr033.PlayerMapYPos(player01);

                        int var_6 = var_4;

                        gbl.mapToBackGroundTile.field_6 = 0;

                        if (ovr032.canReachTarget(gbl.mapToBackGroundTile, ref var_6, ref tmpY, ref tmpX, ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player)) == true &&
                            (var_6 / 2) <= var_4)
                        {
                            gbl.byte_1D90E = true;
                        }
                    }

                    if (gbl.byte_1D90E == false)
                    {
                        var_7 = (byte)ovr025.near_enemy(var_4, player);

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

                            if (ovr025.is_weapon_ranged(player) == true &&
                                ovr025.is_weapon_ranged_melee(player) == false &&
                                ovr025.near_enemy(1, player) > 0)
                            {
                                sub_36673(player);
                                var_2 = true;
                            }
                            else if (ovr025.getTargetRange(player01, player) == 1 ||
                                ovr014.sub_3F143(player01, player) == true)
                            {
                                gbl.byte_1D90E = true;
                            }
                        }
                    }

                    if (gbl.byte_1D90E == true)
                    {
                        ovr033.redrawCombatArea(ovr014.getTargetDirection(player01, player), 2, ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player));

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

                            if (ovr025.is_weapon_ranged(player) == true)
                            {
                                gbl.byte_1D90E = ovr025.sub_6906C(out var_C, player);

                                if (ovr025.is_weapon_ranged_melee(player) == true &&
                                    ovr025.getTargetRange(player01, player) == 1)
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

            ovr025.ClearPlayerTextArea();

            if (ovr025.is_held(player) == true ||
                ovr025.is_weapon_ranged(player) == true ||
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
                var_6 = seg043.GetInputKey();

                if (var_6 == 0)
                {
                    var_6 = seg043.GetInputKey();
                }

                if (var_6 == 0x32)
                {
                    gbl.magicOn = !gbl.magicOn;

                    if (gbl.magicOn == true)
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
                            player_ptr.quick_fight = 0;
                        }

                        player_ptr = player_ptr.next_player;
                    }

                    if (arg_0.quick_fight == 0)
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
                ovr025.DisplayPlayerStatusString(true, 10, "is forced to flee", player);
            }
            else if (player.field_F7 > 0x7F)
            {

                gbl.byte_1D2CC = (byte)((player.field_F7 & 0x7F) << 1);

                if (gbl.byte_1D2CC > 0x66)
                {
                    gbl.byte_1D2CC = 0;
                }
                ovr024.work_on_00(player, 0x11);


                if (gbl.byte_1D2CC < (100 - ((player.hit_point_current * 100) / player.hit_point_max)) ||
                    gbl.byte_1D2CC == 0)
                {
                    var_3 = gbl.byte_1D2CC;
                    gbl.byte_1D2CC = gbl.enemyHealthPercentage;

                    ovr024.work_on_00(player, 17);

                    if (gbl.byte_1D2CC < (100 - gbl.area2_ptr.field_58C) ||
                        gbl.byte_1D2CC == 0 ||
                        player.combat_team == 0)
                    {
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
                }
            }

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
            byte var_1D;
            byte var_1C;
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
                    (gbl.unk_1C020[var_19].classFlags & arg_0.classFlags) != 0)
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


                if (gbl.unk_1C020[var_19].field_0 == 1)
                {
                    if ((gbl.unk_1C020[var_19].classFlags & arg_0.classFlags) != 0)
                    {
                        if (var_10.exp_value >= 0)
                        {
                            var_18 = (byte)(var_10.exp_value + 1);
                        }
                        else
                        {
                            var_18 = 0;
                        }

                        if (var_18 > var_17)
                        {
                            var_C = var_10;
                            var_17 = var_18;
                        }
                    }
                }
                var_10 = var_10.next;
            }

            bool var_1E = ovr025.item_is_ranged_melee(var_4);
            bool var_1F = false;
            var_14 = null;
            byte var_1A = 0;

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
                var_1F = true;
            }

            if (var_4 != null &&
                var_15 > (var_16 >> 1) &&
                var_1F == true &&
                (var_1E == true || ovr025.near_enemy(1, arg_0) == 0))
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
            
            if (arg_0.field_185 > 2)
            {
                if (arg_0.field_155 == null ||
                    arg_0.field_155.field_36 != 0)
                {
                    ovr020.ready_Item(var_10);
                    var_1D = 1;
                }
                else
                {
                    ovr020.ready_Item(arg_0.field_155);
                    var_1D = 1;
                }
            }
            else if (arg_0.field_185 < 2 && var_1C != 0)
            {
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
            }


            ovr025.sub_66C20(arg_0);

            if (var_1D != 0)
            {
                ovr025.hitpoint_ac(arg_0);
            }
        }
    }
}
