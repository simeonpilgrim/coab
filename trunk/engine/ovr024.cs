using Classes;

namespace engine
{
    class ovr024
    {
        internal static void sub_63014(string arg_0, Classes.Status arg_4, Player player)
        {
            ovr025.DisplayPlayerStatusString(false, 10, arg_0, player);

            if (player.health_status != Status.stoned &&
                player.health_status != Status.dead &&
                player.health_status != Status.gone)
            {

                player.health_status = arg_4;
                player.in_combat = false;
                player.hit_point_current = 0;

                sub_645AB(player);
                work_on_00(player, 13);

                if (player.in_combat == false)
                {
                    ovr033.sub_74E6F(player);
                }

                seg041.GameDelay();
                ovr025.ClearPlayerTextArea();

                if (gbl.game_state != 5)
                {
                    ovr025.Player_Summary(gbl.player_ptr);
                }
            }
        }


        internal static void sub_630C7(byte arg_0, object parameter, Player player, Affects affect)
        {
            if (gbl.byte_1D8AC != 0)
            {
                gbl.spell_jump_list[147](arg_0, parameter, player);
            }
            else
            {
                gbl.spell_jump_list[(int)affect](arg_0, parameter, player);
            }
        }

        internal static void remove_affect(Affect arg_0, Affects affect_id, Player arg_6)
        {
            Affect var_4;
            Affect var_8;

            var_8 = arg_0;

            if (var_8 == null)
            {
                var_8 = arg_6.affect_ptr;
                while (var_8 != null && var_8.type != affect_id)
                {
                    var_8 = var_8.next;
                }
            }

            if (var_8 != null)
            {
                if (var_8.call_spell_jump_list == true)
                {
                    sub_630C7(1, var_8, arg_6, affect_id);
                }

                if (arg_6.affect_ptr == var_8)
                {
                    arg_6.affect_ptr = var_8.next;
                }
                else
                {
                    var_4 = arg_6.affect_ptr;

                    while (var_4 != null && var_4.next != var_8)
                    {
                        var_4 = var_4.next;
                    }

                    var_4.next = var_8.next;
                }

                seg051.FreeMem(9, var_8);

                if (affect_id == Affects.resist_fire)
                {
                    sub_648D9(5, arg_6);
                }

                if (affect_id == Affects.enlarge || affect_id == Affects.strength || affect_id == Affects.affect_92)
                {
                    sub_648D9(0, arg_6);
                }
            }
        }

        static Set unk_6325A = new Set(0x0205, new byte[] { 0x20, 0x00, 0x00, 0x60, 0x02 });

        internal static void calc_affect_effect(Affects affect_type, Player player)
        {
            byte var_E3;
            byte var_E1;
            Affect affect;
            Player player_base;
            SortedCombatant[] var_D8 = new SortedCombatant[gbl.MaxSortedCombatantCount];

            var_E1 = 0;
            player_base = gbl.player_next_ptr;

            if (ovr025.find_affect(out affect, affect_type, player) == true)
            {
                var_E1 = 1;
            }
            else if (unk_6325A.MemberOf((byte)affect_type) == true)
            {
                System.Array.Copy(gbl.SortedCombatantList, var_D8, gbl.MaxSortedCombatantCount);

                while (player_base != null &&
                    var_E1 == 0)
                {
                    if (ovr025.find_affect(out affect, affect_type, player_base) == true)
                    {
                        if (gbl.game_state == 5)
                        {
                            if (affect_type == Affects.prayer)
                            {
                                var_E3 = 6;
                            }
                            else
                            {
                                var_E3 = 1;
                            }

                            ovr032.Rebuild_SortedCombatantList(gbl.mapToBackGroundTile, ovr033.PlayerMapSize(player_base), 0xff,
                                var_E3, ovr033.PlayerMapYPos(player_base), ovr033.PlayerMapXPos(player_base));

                            for (int i = 0; i < gbl.sortedCombatantCount; i++)
                            {
                                if (gbl.SortedCombatantList[i].player_index == ovr033.get_player_index(player))
                                {
                                    var_E1 = 1;
                                }
                            }
                        }
                        else
                        {
                            var_E1 = 1;
                        }
                    }

                    player_base = player_base.next_player;
                }

                System.Array.Copy(var_D8, gbl.SortedCombatantList, gbl.MaxSortedCombatantCount);
            }

            if (var_E1 != 0)
            {
                sub_630C7(0, affect, player, affect_type);
            }
        }

        static internal void work_on_00(Player player, int arg_4)
        {
            switch (arg_4)
            {
                case 0:
                    break;

                case 1:
                    calc_affect_effect(Affects.blink, player);
                    calc_affect_effect(Affects.invisibility, player);
                    calc_affect_effect(Affects.invisible, player);
                    calc_affect_effect(Affects.invisible_to_animals, player);
                    break;

                case 2:
                    calc_affect_effect(Affects.affect_4f, player);
                    calc_affect_effect(Affects.affect_50, player);
                    calc_affect_effect(Affects.affect_91, player);
                    calc_affect_effect(Affects.affect_39, player);
                    calc_affect_effect(Affects.affect_60, player);
                    calc_affect_effect(Affects.affect_7a, player);
                    calc_affect_effect(Affects.affect_7b, player);
                    break;

                case 3:
                    calc_affect_effect(Affects.affect_40, player);
                    calc_affect_effect(Affects.affect_41, player);
                    calc_affect_effect(Affects.affect_42, player);
                    calc_affect_effect(Affects.affect_43, player);
                    calc_affect_effect(Affects.affect_46, player);
                    calc_affect_effect(Affects.affect_4f, player);
                    calc_affect_effect(Affects.affect_57, player);
                    break;

                case 4:
                    calc_affect_effect(Affects.ray_of_enfeeblement, player);
                    calc_affect_effect(Affects.affect_06, player);
                    calc_affect_effect(Affects.affect_67, player);
                    calc_affect_effect(Affects.affect_4b, player);
                    calc_affect_effect(Affects.affect_4c, player);
                    calc_affect_effect(Affects.affect_86, player);
                    break;

                case 5:
                    calc_affect_effect(Affects.mirror_image, player);
                    calc_affect_effect(Affects.prot_from_normal_missiles, player);
                    calc_affect_effect(Affects.affect_68, player);
                    calc_affect_effect(Affects.affect_78, player);
                    calc_affect_effect(Affects.affect_65, player);
                    calc_affect_effect(Affects.affect_73, player);
                    calc_affect_effect(Affects.affect_74, player);
                    calc_affect_effect(Affects.affect_77, player);
                    calc_affect_effect(Affects.affect_5e, player);
                    calc_affect_effect(Affects.affect_75, player);
                    calc_affect_effect(Affects.affect_3c, player);
                    calc_affect_effect(Affects.affect_51, player);
                    calc_affect_effect(Affects.affect_52, player);
                    calc_affect_effect(Affects.affect_55, player);
                    calc_affect_effect(Affects.affect_82, player);
                    calc_affect_effect(Affects.affect_8f, player);
                    break;

                case 6:
                    calc_affect_effect(Affects.affect_71, player);
                    calc_affect_effect(Affects.fire_resist, player);
                    calc_affect_effect(Affects.resist_cold, player);
                    calc_affect_effect(Affects.resist_fire, player);
                    calc_affect_effect(Affects.affect_69, player);
                    calc_affect_effect(Affects.affect_6a, player);
                    calc_affect_effect(Affects.affect_70, player);
                    calc_affect_effect(Affects.affect_72, player);
                    calc_affect_effect(Affects.affect_76, player);
                    calc_affect_effect(Affects.shield, player);
                    calc_affect_effect(Affects.affect_5d, player);
                    calc_affect_effect(Affects.affect_65, player);
                    calc_affect_effect(Affects.mirror_image, player);
                    calc_affect_effect(Affects.affect_6e, player);
                    calc_affect_effect(Affects.prot_drag_breath, player);
                    calc_affect_effect(Affects.affect_52, player);
                    calc_affect_effect(Affects.affect_54, player);
                    calc_affect_effect(Affects.affect_81, player);
                    calc_affect_effect(Affects.affect_85, player);
                    calc_affect_effect(Affects.affect_87, player);
                    calc_affect_effect(Affects.minor_globe_of_invulnerability, player);
                    break;

                case 7:
                    calc_affect_effect(Affects.snake_charm, player);
                    calc_affect_effect(Affects.paralyze, player);
                    calc_affect_effect(Affects.sleep, player);
                    calc_affect_effect(Affects.helpless, player);
                    calc_affect_effect(Affects.affect_03, player);
                    calc_affect_effect(Affects.fumbling, player);
                    calc_affect_effect(Affects.affect_88, player);
                    break;

                case 8:
                    calc_affect_effect(Affects.affect_63, player);
                    calc_affect_effect(Affects.affect_52, player);
                    calc_affect_effect(Affects.displace, player);
                    calc_affect_effect(Affects.camouflage, player);
                    calc_affect_effect(Affects.affect_38, player);
                    break;

                case 9:
                    calc_affect_effect(Affects.affect_69, player);
                    calc_affect_effect(Affects.affect_6a, player);
                    calc_affect_effect(Affects.affect_6b, player);
                    calc_affect_effect(Affects.affect_6c, player);
                    calc_affect_effect(Affects.affect_6d, player);
                    calc_affect_effect(Affects.affect_6e, player);
                    calc_affect_effect(Affects.affect_6f, player);
                    calc_affect_effect(Affects.affect_70, player);
                    calc_affect_effect(Affects.affect_7c, player);
                    calc_affect_effect(Affects.affect_7d, player);
                    calc_affect_effect(Affects.minor_globe_of_invulnerability, player);
                    calc_affect_effect(Affects.affect_81, player);
                    break;

                case 10:
                    calc_affect_effect(Affects.bless, player);
                    calc_affect_effect(Affects.cursed, player);
                    calc_affect_effect(Affects.blinded, player);
                    calc_affect_effect(Affects.bestow_curse, player);
                    calc_affect_effect(Affects.prayer, player);
                    calc_affect_effect(Affects.affect_06, player);
                    calc_affect_effect(Affects.affect_12, player);
                    calc_affect_effect(Affects.affect_1a, player);
                    calc_affect_effect(Affects.affect_4b, player);
                    calc_affect_effect(Affects.affect_4c, player);
                    break;

                case 11:
                    calc_affect_effect(Affects.blinded, player);
                    calc_affect_effect(Affects.shield, player);
                    calc_affect_effect(Affects.protection_from_evil, player);
                    calc_affect_effect(Affects.protection_from_good, player);
                    calc_affect_effect(Affects.prot_from_evil_10_radius, player);
                    calc_affect_effect(Affects.prot_from_good_10_radius, player);
                    calc_affect_effect(Affects.affect_1e, player);
                    calc_affect_effect(Affects.faerie_fire, player);
                    break;

                case 12:
                    calc_affect_effect(Affects.protection_from_evil, player);
                    calc_affect_effect(Affects.protection_from_good, player);
                    calc_affect_effect(Affects.resist_cold, player);
                    calc_affect_effect(Affects.shield, player);
                    calc_affect_effect(Affects.resist_fire, player);
                    calc_affect_effect(Affects.blinded, player);
                    calc_affect_effect(Affects.bestow_curse, player);
                    calc_affect_effect(Affects.prot_from_evil_10_radius, player);
                    calc_affect_effect(Affects.prot_from_good_10_radius, player);
                    calc_affect_effect(Affects.prayer, player);
                    calc_affect_effect(Affects.fire_resist, player);
                    calc_affect_effect(Affects.affect_6f, player);
                    calc_affect_effect(Affects.affect_7d, player);
                    calc_affect_effect(Affects.affect_61, player);
                    calc_affect_effect(Affects.hot_fire_shield, player);
                    calc_affect_effect(Affects.cold_fire_shield, player);
                    break;

                case 13:
                    calc_affect_effect(Affects.affect_63, player);
                    calc_affect_effect(Affects.affect_64, player);
                    calc_affect_effect(Affects.affect_4b, player);
                    break;

                case 14:
                    calc_affect_effect(Affects.affect_53, player);
                    calc_affect_effect(Affects.affect_58, player);
                    calc_affect_effect(Affects.affect_79, player);
                    calc_affect_effect(Affects.affect_56, player);
                    calc_affect_effect(Affects.affect_57, player);
                    calc_affect_effect(Affects.affect_5a, player);
                    calc_affect_effect(Affects.affect_7e, player);
                    calc_affect_effect(Affects.affect_80, player);
                    calc_affect_effect(Affects.affect_83, player);
                    calc_affect_effect(Affects.affect_84, player);
                    calc_affect_effect(Affects.affect_8b, player);
                    break;

                case 15:
                    calc_affect_effect(Affects.silence_15_radius, player);
                    calc_affect_effect(Affects.affect_1e, player);
                    calc_affect_effect(Affects.charm_person, player);
                    calc_affect_effect(Affects.affect_0d, player);
                    calc_affect_effect(Affects.berserk, player);
                    break;

                case 16:
                    calc_affect_effect(Affects.invisibility, player);
                    calc_affect_effect(Affects.invisible, player);
                    calc_affect_effect(Affects.blink, player);
                    calc_affect_effect(Affects.affect_2f, player);
                    calc_affect_effect(Affects.affect_30, player);
                    calc_affect_effect(Affects.displace, player);
                    calc_affect_effect(Affects.dispel_evil, player);
                    break;

                case 17:
                    calc_affect_effect(Affects.bless, player);
                    calc_affect_effect(Affects.cursed, player);
                    calc_affect_effect(Affects.charm_person, player);
                    break;

                case 18:
                    calc_affect_effect(Affects.haste, player);
                    calc_affect_effect(Affects.slow, player);
                    calc_affect_effect(Affects.affect_3a, player);
                    break;

                case 19:
                    calc_affect_effect(Affects.affect_62, player);
                    calc_affect_effect(Affects.spiritual_hammer, player);
                    calc_affect_effect(Affects.camouflage, player);
                    calc_affect_effect(Affects.affect_38, player);
                    calc_affect_effect(Affects.charm_person, player);
                    break;

                case 20:
                    calc_affect_effect(Affects.hot_fire_shield, player);
                    calc_affect_effect(Affects.cold_fire_shield, player);
                    break;

                case 21:
                    calc_affect_effect(Affects.confuse, player);
                    break;

                case 22:
                    calc_affect_effect(Affects.affect_8a, player);
                    break;

                case 23:
                    calc_affect_effect(Affects.affect_4a, player);
                    break;
            }
        }


        static Player sub_63D03(byte[] arg_0, byte arg_4, Struct_1D885 arg_6, int arg_A, int arg_C)
        {
            Struct_1D885 var_8;
            Player player_base;

            var_8 = arg_6;
            bool found = false;

            while (var_8 != null && found == false)
            {
                for (int i = 1; i <= arg_4; i++)
                {
                    if (var_8.field_10[i] != 0 &&
                        var_8.field_1A + gbl.MapDirectionXDelta[arg_0[i]] == arg_C &&
                        var_8.field_1B + gbl.MapDirectionYDelta[arg_0[i]] == arg_A)
                    {
                        found = true;
                    }
                }

                if (found == false)
                {
                    var_8 = var_8.next;
                }
            }

            if (found)
            {
                player_base = var_8.player;
            }
            else
            {
                player_base = gbl.player_next_ptr;
            }

            return player_base;
        }


        internal static void in_poison_cloud(byte arg_0, Player player)
        {
            Player playerbase_ptr; 
            byte var_2;

            if (player.in_combat == true)
            {
                bool isPoisonousCloud;
                bool isNoxiouxCloud;
                byte dummyByte;
                ovr033.sub_74D04(out isPoisonousCloud, out isNoxiouxCloud, out dummyByte, out var_2, 8, player);

                Affect affect;

                if (isNoxiouxCloud && arg_0 != 0 &&
                    ovr025.find_affect(out affect, Affects.helpless, player) == false &&
                    ovr025.find_affect(out affect, Affects.funky__32, player) == false &&
                    ovr025.find_affect(out affect, Affects.affect_6f, player) == false &&
                    ovr025.find_affect(out affect, Affects.affect_7d, player) == false)
                {
                    bool save_passed = do_saving_throw(0, 0, player);

                    if (save_passed == true)
                    {
                        playerbase_ptr = gbl.player_ptr;

                        gbl.player_ptr = sub_63D03(gbl.unk_18AEA, 4, gbl.stru_1D885,
                            ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player));

                        is_unaffected("starts to cough", save_passed, 0, false, 0xff, 1, Affects.affect_1e, player);

                        Affect var_A = null;

                        if (ovr025.find_affect(out var_A, Affects.affect_1e, player) == true)
                        {
                            sub_630C7(0, affect, player, Affects.affect_1e);
                        }

                        gbl.player_ptr = playerbase_ptr;
                    }
                    else
                    {
                        playerbase_ptr = gbl.player_ptr;

                        gbl.player_ptr = sub_63D03(gbl.unk_18AEA, 4, gbl.stru_1D885,
                            ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player));

                        is_unaffected("chokes and gags from nausea", save_passed, 0, false, 0xff, (ushort)(roll_dice(4, 1) + 1), Affects.helpless, player);

                        if (ovr025.find_affect(out affect, Affects.helpless, player) == true)
                        {
                            sub_630C7(0, affect, player, Affects.helpless);
                        }

                        gbl.player_ptr = playerbase_ptr;
                    }
                }

                if (isPoisonousCloud == true &&
                    player.in_combat == true)
                {
                    if (player.field_E5 >= 0 && player.field_E5 <= 4)
                    {
                        ovr025.DisplayPlayerStatusString(false, 10, "is Poisoned", player);
                        seg041.GameDelay();
                        add_affect(false, 0xff, 0, Affects.minor_globe_of_invulnerability, player);
                        sub_63014("is killed", Status.dead, player);
                    }
                    else if (player.field_E5 == 5)
                    {
                        if (do_saving_throw(-4, 0, player) == false)
                        {
                            ovr025.DisplayPlayerStatusString(false, 10, "is Poisoned", player);
                            seg041.GameDelay();
                            add_affect(false, 0xff, 0, Affects.poisoned, player);
                            sub_63014("is killed", Status.dead, player);
                        }
                    }
                    else if (player.field_E5 == 6)
                    {
                        if (do_saving_throw(0, 0, player) == false)
                        {
                            ovr025.DisplayPlayerStatusString(false, 10, "is Poisoned", player);
                            seg041.GameDelay();
                            add_affect(false, 0xff, 0, Affects.poisoned, player);
                            sub_63014("is killed", Status.dead, player);
                        }
                    }
                }
            }
        }


        internal static bool sub_641DD(byte arg_0, Player arg_2)
        {
            bool var_1;

            var_1 = false;
            gbl.byte_1D2C9 = (sbyte)roll_dice(20, 1);

            if (gbl.byte_1D2C9 > 1)
            {
                if (gbl.byte_1D2C9 == 0x14)
                {
                    gbl.byte_1D2C9 = 0x64;
                }

                work_on_00(arg_2, 16);

                if (gbl.byte_1D2C9 >= 0)
                {
                    if ((gbl.byte_1D2C9 + arg_0) > arg_2.ac)
                    {
                        var_1 = true;
                    }
                }
            }

            return var_1;
        }


        internal static bool sub_64245(byte arg_0, Player arg_2, Player arg_6)
        {
            bool var_1 = false;
            short var_2;

            remove_invisibility(arg_6);
            gbl.byte_1D2C9 = (sbyte)roll_dice(20, 1);

            if (gbl.byte_1D2C9 > 1)
            {
                if (gbl.byte_1D2C9 == 0x14)
                {
                    gbl.byte_1D2C9 = 100;
                }

                work_on_00(arg_6, 0x0a);
                work_on_00(arg_2, 0x10);

                if (arg_6.combat_team == 0)
                {
                    var_2 = gbl.area2_ptr.field_6E2;
                }
                else
                {
                    var_2 = gbl.area2_ptr.field_6E0;
                }

                if (gbl.byte_1D2C9 >= 0)
                {
                    if ((gbl.byte_1D2C9 + arg_6.hitBonus + var_2) >= arg_0)
                    {
                        var_1 = true;
                    }
                }
            }
            return var_1;
        }


        internal static bool do_saving_throw(sbyte arg_0, byte arg_2, Player player)
        {
            gbl.save_made = true;

            gbl.saving_throw_roll = roll_dice(20, 1);

            if (gbl.saving_throw_roll == 1)
            {
                gbl.save_made = false;
            }
            else if (gbl.saving_throw_roll == 20)
            {
                gbl.save_made = true;
            }
            else
            {
                gbl.saving_throw_roll += (byte)(arg_0 + player.field_186);
                gbl.byte_1D2D1 = arg_2;

                work_on_00(player, 12);
                if (player.field_DFArrayGet(arg_2) > gbl.saving_throw_roll)
                {
                    gbl.save_made = false;
                }
                else
                {
                    gbl.save_made = true;
                }
            }

            return gbl.save_made;
        }


        internal static byte roll_dice(int dice_size, int dice_count)
        {
            int roll_total = 0;

            for (int i = 0; i < dice_count; i++)
            {
                roll_total += seg051.Random(dice_size) + 1;
            }

            byte byte_total = (byte)roll_total;

            return byte_total;
        }


        internal static sbyte roll_dice_save(int dice_size, int dice_count)
        {
            gbl.dice_count = dice_count;

            byte var_1 = roll_dice(dice_size, dice_count);

            return (sbyte)var_1;
        }


        internal static void add_affect(bool arg_0, byte arg_2, ushort arg_4, Affects type, Player player)
        {
            Affect affect_ptr;
            Affect affect_ptr2;

            affect_ptr2 = new Affect();

            if (player.affect_ptr == null)
            {
                player.affect_ptr = affect_ptr2;
            }
            else
            {
                affect_ptr = player.affect_ptr;

                while (affect_ptr.next != null)
                {
                    affect_ptr = affect_ptr.next;

                }

                affect_ptr.next = affect_ptr2;
            }

            affect_ptr2.next = null;

            affect_ptr2.type = type;
            affect_ptr2.field_1 = arg_4;
            affect_ptr2.field_3 = arg_2;
            affect_ptr2.call_spell_jump_list = arg_0;
        }


        internal static void sub_644A7(string msg, Status health_status, Player player)
        {
            byte player_index;

            if (player.in_combat == true)
            {
                player_index = ovr033.get_player_index(player);

                ovr033.sub_75356(false, 3, player);

                ovr025.DisplayPlayerStatusString(true, 10, msg, player);

                player.in_combat = false;

                player.health_status = health_status;

                if (player.health_status != Status.running)
                {
                    player.hit_point_current = 0;
                }

                ovr033.sub_74572(player_index, 0, 0);
                seg040.DrawOverlay();

                gbl.CombatMap[player_index].size = 0;

                ovr033.sub_743E7();

                if (ovr025.clear_actions(player) == true)
                {
                    sub_645AB(player);
                }
            }
        }


        internal static void remove_invisibility(Player player)
        {
            Affect affect;

            while (ovr025.find_affect(out affect, Affects.invisibility, player) == true)
            {
                remove_affect(affect, Affects.invisibility, player);
            }
        }


        internal static void sub_645AB(Player player)
        {
            Affect var_5;
            byte loop_var;

            Affects[] table = { 
								  Affects.faerie_fire,
								  Affects.charm_person,
								  Affects.affect_0d,
								  Affects.silence_15_radius,
								  Affects.spiritual_hammer,
								  Affects.affect_1e,
								  Affects.helpless,
								  Affects.funky__32,
								  Affects.snake_charm,
								  Affects.paralyze,
								  Affects.sleep,
								  Affects.affect_3a,
								  Affects.regenerate,
								  Affects.affect_5F,
								  Affects.affect_62,
								  Affects.affect_88,
								  Affects.affect_89,
								  Affects.affect_8b,
								  Affects.affect_90
							  };

            for (loop_var = 0; loop_var < 19; loop_var++)
            {
                remove_affect(null, table[loop_var], player);
            }

            if (ovr025.find_affect(out var_5, Affects.berserk, player) == true &&
                player.field_F7 == 0xB3)
            {
                player.combat_team = 0;
            }
        }


        internal static void sub_6460D(Player arg_0)
        {
            byte var_1;

            Affects[] table = {   Affects.affect_0d, 
								  Affects.affect_3a, 
								  Affects.affect_8b, 
								  Affects.affect_90 };

            for (var_1 = 0; var_1 < 4; var_1++)
            {
                remove_affect(null, table[var_1], arg_0);
            }
        }


        internal static bool is_cured(Affects arg_0, Player arg_2)
        {
            Affect var_5;
            bool ret_val;

            ret_val = false;

            if (ovr025.find_affect(out var_5, arg_0, arg_2) == true)
            {
                ovr025.DisplayPlayerStatusString(true, 10, "is Cured", arg_2);

                remove_affect(var_5, arg_0, arg_2);
                ret_val = true;
            }

            return ret_val;
        }


        internal static byte odd_math(byte arg_0, byte arg_2)
        {
            byte ret_val;

            ret_val = (byte)(arg_2 + 100);

            if (arg_2 == 18)
            {
                ret_val = (byte)(arg_0 + 1);
            }

            return ret_val;
        }


        internal static void sub_646D9(ref byte arg_0, ref byte arg_4, Affect arg_8)
        {
            arg_0 = 0;

            arg_4 = (byte)(arg_8.field_3 & 0x7F);

            if (arg_4 <= 101)
            {
                arg_0 = (byte)(arg_4 - 1);
                arg_4 = 18;
            }
            else
            {
                arg_4 -= 100;
            }
        }


        internal static bool sub_64728(out byte arg_0, byte arg_4, byte arg_6, Player arg_8)
        {
            bool ret_val;

            if (arg_6 > arg_8.tmp_str ||
                (arg_6 == 18 && arg_4 > arg_8.field_1D))
            {
                ret_val = true;

                arg_0 = odd_math(arg_4, arg_6);
            }
            else
            {
                ret_val = false;
                arg_0 = 0;
            }

            return ret_val;
        }

        /// <summary>
        /// Appears to be a strength MAX function
        /// </summary>
        /// <param name="var_1"></param>
        /// <param name="var_2"></param>
        /// <param name="var_3"></param>
        /// <param name="var_4"></param>
        internal static void sub_64771(ref byte var_1, byte var_2, ref byte var_3, byte var_4)
        {
            if (var_2 > var_1 ||
                (var_2 == 18 && var_4 > var_3))
            {
                var_1 = var_2;
                var_3 = var_4;
            }
        }


        internal static void sub_647BE(byte arg_2, sbyte arg_4, byte bp_var_1, ref byte bp_var_A, Player bp_arg_2)
        {
            if (gbl.byte_1A1CB[arg_4] <= arg_2)
            {
                arg_2 = (byte)(gbl.byte_1A1CB[arg_4] - 1);
            }

            if (arg_4 == 4 &&
                (bp_arg_2.field_E6 == 0 || bp_arg_2.field_115 == bp_arg_2.field_E6))
            {
                arg_2 += 1;
            }

            if (arg_4 == 2 || arg_4 == 3 || arg_4 == 4)
            {
                if (bp_var_1 >= 0x0F && bp_var_1 <= 0x13)
                {
                    bp_var_A += (byte)(arg_2 * (bp_var_1 - 0x0E));
                }
                else if (bp_var_1 == 0x14)
                {
                    bp_var_A += (byte)(arg_2 * 5);
                }
                else if (bp_var_1 >= 0x15 && bp_var_1 <= 0x17)
                {
                    bp_var_A += (byte)(arg_2 * 6);
                }
                else if (bp_var_1 >= 0x18 && bp_var_1 <= 0x19)
                {
                    bp_var_A += (byte)(arg_2 * 7);
                }
            }
            else
            {
                if (bp_var_1 > 0x0F)
                {
                    bp_var_A += (byte)(arg_2 << 1);
                }
                else if (bp_var_1 == 0x0F)
                {
                    bp_var_A += arg_2;
                }
            }
        }


        internal static void sub_648D9(byte arg_0, Player player)
        {
            byte var_13;
            byte var_12;
            byte var_11;
            Item item;
            sbyte var_C;
            byte var_B;
            byte var_A;
            byte var_9;
            Affect affect_ptr;
            byte var_4;
            byte var_3;
            byte var_2;
            byte var_1;

            var_A = 0;
            var_2 = 0;
            var_4 = 0;
            var_11 = 0x0FF;

            item = player.itemsPtr;

            var_1 = player.stats[arg_0].tmp;
            var_3 = player.field_1D;

            while (item != null)
            {
                if ((int)item.affect_3 > 0x80 &&
                    item.readied == false)
                {
                    var_12 = (byte)((int)item.affect_3 & 0x7F);

                    if (arg_0 == 0)
                    {
                        if (var_12 == 5)
                        {
                            switch ((int)item.affect_2)
                            {
                                case 0:
                                    var_2 = 0x12;
                                    var_4 = 0x64;
                                    break;

                                case 1:
                                    var_2 = 0x13;
                                    break;

                                case 2:
                                    var_2 = 0x14;
                                    break;

                                case 3:
                                    var_2 = 0x15;
                                    break;

                                case 4:
                                    var_2 = 0x16;
                                    break;

                                case 5:
                                    var_2 = 0x17;
                                    break;

                                case 6:
                                    var_2 = 0x18;
                                    break;
                            }
                        }
                        else if (var_12 == 8)
                        {
                            if (player.tmp_str < 18 &&
                                item.affect_2 == 0)
                            {
                                var_2 = (byte)(player.tmp_str + 1);
                                var_4 = 0;
                            }
                        }
                        else if (var_12 == 13)
                        {
                            var_11 = 3;
                        }

                        sub_64771(ref var_1, var_2, ref var_3, var_4);
                    }
                    else if (arg_0 == 4)
                    {
                        if (var_12 == 6)
                        {
                            var_1++;
                        }
                        else if (var_12 == 8 &&
                            player.stats[(int)Stat.CON].tmp < 18 &&
                            (int)item.affect_2 == 4)
                        {
                            var_1++;
                        }
                    }
                    else if (arg_0 == 1)
                    {
                        if (var_12 == 8)
                        {
                            if (player.stats[(int)Stat.INT].tmp < 0x18 &&
                                (int)item.affect_2 == 1)
                            {
                                var_1++;
                            }
                        }
                        else if (var_12 == 12)
                        {
                            var_11 = 7;
                        }
                        else if (var_12 == 13)
                        {
                            var_11 = 3;
                        }
                    }
                    else if (arg_0 == 2)
                    {
                        if (var_12 == 8 &&
                            (int)item.affect_2 == 2 &&
                            player.tmp_wis < 18)
                        {
                            var_1++;
                        }
                    }
                    else if (arg_0 == 3)
                    {
                        if (var_12 == 2)
                        {
                            if (player.tmp_dex >= 0 && player.tmp_dex <= 6)
                            {
                                var_1 += 4;
                            }
                            else if (player.tmp_dex >= 7 && player.tmp_dex <= 13)
                            {
                                var_1 += 2;
                            }
                            else
                            {
                                var_1++;
                            }
                        }
                        else if (var_12 == 8)
                        {
                            if (player.tmp_dex < 18 &&
                                (int)item.affect_2 == 3)
                            {
                                var_1++;
                            }
                        }
                        else if (var_12 == 10)
                        {
                            var_1 -= 2;
                        }
                    }
                    else if (arg_0 == 5)
                    {
                        if (var_12 == 6)
                        {
                            var_1 -= 1;
                        }
                        else if (var_12 == 8 &&
                            player.tmp_cha < 18 &&
                            (int)item.affect_2 == 5)
                        {
                            var_1 += 1;
                        }
                    }
                }

                item = item.next;
            }

            if (arg_0 == 0)
            {

                if (ovr025.find_affect(out affect_ptr, Affects.strength, player) == true)
                {
                    sub_646D9(ref var_4, ref var_2, affect_ptr);

                    if (var_1 <= 18 &&
                        var_3 < 100)
                    {
                        var_2 = (byte)(var_1 + var_2);

                        if (var_2 > 18)
                        {
                            if (player.fighter_lvl > 0 ||
                                player.field_113 > 0 ||
                                player.paladin_lvl > 0 ||
                                player.field_114 > 0 ||
                                player.ranger_lvl > 0 ||
                                player.field_115 > 0)
                            {
                                var_4 = (byte)(player.strength_18_100 + ((var_2 - 18) * 10));

                                if (var_4 > 100)
                                {
                                    var_4 = 100;
                                }

                                var_2 = 18;
                            }
                            else
                            {
                                var_2 = 18;
                            }
                        }
                    }


                    sub_64771(ref var_1, var_2, ref var_3, var_4);
                }

                if (ovr025.find_affect(out affect_ptr, Affects.affect_92, player) == true)
                {
                    sub_646D9(ref var_4, ref var_2, affect_ptr);
                    sub_64771(ref var_1, var_2, ref var_3, var_4);
                }

                if (ovr025.find_affect(out affect_ptr, Affects.enlarge, player) == true)
                {
                    sub_646D9(ref var_4, ref var_2, affect_ptr);
                    sub_64771(ref var_1, var_2, ref var_3, var_4);

                }

                if (var_11 != 0xff)
                {
                    player.strength = var_11;
                    player.strength_18_100 = 0;
                }
                else
                {
                    player.strength = var_1;
                    player.strength_18_100 = var_3;
                }
            }
            else if (arg_0 == 4)
            {
                var_13 = 0;
                var_9 = player.hit_point_max;
                player.hit_point_max = player.field_12C;

                for (var_C = 0; var_C <= 7; var_C++)
                {
                    var_B = player.Skill_B_lvl[var_C];

                    if (var_B > 0)
                    {
                        sub_647BE(var_B, var_C, var_1, ref var_A, player);
                    }

                    var_B = player.Skill_A_lvl[var_C];

                    if (var_B > 0)
                    {
                        var_13++;
                    }

                    if (gbl.byte_1A1CB[var_C] < var_B)
                    {
                        var_B = gbl.byte_1A1CB[var_C];
                    }

                    if (var_B > player.field_E6)
                    {
                        var_B = player.field_E6;

                        sub_647BE(var_B, var_C, var_1, ref var_A, player);
                    }
                }

                var_A /= var_13;
                player.hit_point_max += var_A;

                if (player.hit_point_max > var_9)
                {
                    player.hit_point_current = (byte)(player.hit_point_max - var_9);
                }

                if (player.hit_point_max < var_9)
                {
                    if (player.hit_point_current > var_9 - player.hit_point_max)
                    {
                        player.hit_point_current = (byte)(var_9 - player.hit_point_max);
                    }
                    else
                    {
                        player.hit_point_current = 0;
                    }
                }

                player.con = var_1;

                if (player.con > 20)
                {
                    if (ovr025.find_affect(out affect_ptr, Affects.affect_3e, player) == true)
                    {
                        add_affect(false, 0xff, 0x3c, Affects.affect_3e, player);
                    }
                }
                else
                {
                    remove_affect(null, Affects.affect_3e, player);
                }
            }
            else if (arg_0 == 1)
            {
                if (ovr025.find_affect(out affect_ptr, Affects.feeble, player) == true &&
                    var_11 > 7)
                {
                    var_11 = 3;
                }

                if (var_11 != 0xff)
                {
                    player._int = var_11;
                }
                else
                {
                    player._int = var_1;
                }
            }
            else if (arg_0 == 2)
            {
                if (ovr025.find_affect(out affect_ptr, Affects.feeble, player) == true &&
                    var_11 > 7)
                {
                    var_11 = 3;
                }

                if (var_11 != 0xff)
                {
                    player.wis = var_11;
                }
                else
                {
                    player.wis = var_1;
                }
            }
            else if (arg_0 == 3)
            {
                if (var_11 != 0xff)
                {
                    player.dex = var_11;
                }
                else
                {
                    player.dex = var_1;
                }
            }
            else if (arg_0 == 5)
            {
                if (ovr025.find_affect(out affect_ptr, Affects.friends, player) == true)
                {
                    var_1 = affect_ptr.field_3;
                }

                player.charisma = var_1;

            }
        }

        static Set unk_64F90 = new Set(0x0002, new byte[] { 0xC0, 0x01 });

        internal static void damage_person(bool arg_0, byte arg_2, sbyte arg_4, Player player)
        {
            string var_100;

            gbl.byte_1D2BE = (byte)arg_4;

            work_on_00(player, 6);

            if (arg_0 == true)
            {
                if (arg_2 == 1)
                {
                    gbl.byte_1D2BE = 0;
                }
                else if (arg_2 == 2)
                {
                    gbl.byte_1D2BE /= 2;
                }
            }
            else
            {
                work_on_00(player, 20);
            }

            if (gbl.byte_1D2BE > 0)
            {
                if (gbl.byte_1D2BE != 1)
                {
                    var_100 = "takes " + gbl.byte_1D2BE.ToString() + " points of damage ";
                }
                else
                {
                    var_100 = "takes 1 point of damage ";
                }

                int mask = gbl.byte_1D2BF & 0xf7;
                if (mask == 0x01)
                {
                    var_100 += "from Fire";
                }
                else if (mask == 0x02)
                {
                    var_100 += "from Cold";
                }
                else if (mask == 0x04)
                {
                    var_100 += "from Electricity";
                }
                else if (mask == 0x10)
                {
                    var_100 +=  "from Acid";
                }

                if ((gbl.byte_1D2BF & 8) == gbl.byte_1D2BF)
                {
                    var_100 += "from Magic";
                }

                ovr025.sub_6818A(var_100, 0, player);
                ovr025.damage_player(gbl.byte_1D2BE, player);

                if (gbl.game_state == 5)
                {
                    player.actions.can_cast = 0;

                    if (player.actions.spell_id > 0)
                    {
                        ovr025.DisplayPlayerStatusString(true, 12, "lost a spell", player);

                        ovr025.clear_spell(player.actions.spell_id, player);

                        player.actions.spell_id = 0;
                    }
                }

                if (player.in_combat == false)
                {
                    var_100 = "Goes Down";

                    if (player.health_status == Status.dying)
                    {
                        var_100 = var_100 + ", and is Dying";
                    }

                    if (unk_64F90.MemberOf((byte)player.health_status) == true)
                    {
                        var_100 = "is killed";
                    }

                    ovr025.DisplayPlayerStatusString(false, (byte)(gbl.textYCol + 1), var_100, player);

                    if (gbl.game_state != 5)
                    {
                        seg041.GameDelay();
                    }
                    else
                    {
                        sub_645AB(player);

                        work_on_00(player, 13);

                        if (player.in_combat == false)
                        {
                            ovr033.sub_74E6F(player);
                        }
                        else
                        {
                            seg041.GameDelay();
                        }
                    }
                }

                ovr025.ClearPlayerTextArea();
            }
        }


        internal static void is_unaffected(string arg_0, bool arg_4, byte arg_6, bool arg_8, byte arg_A, ushort arg_C, Affects arg_E, Player arg_10)
        {
            Affect var_2D;
            string var_29;

            var_29 = arg_0;

            gbl.byte_1D2BD = (byte)arg_E;

            work_on_00(arg_10, 9);

            if (gbl.byte_1D2BD == 0 ||
                (arg_4 == true && arg_6 == 1))
            {
                ovr025.DisplayPlayerStatusString(true, 10, "is Unaffected", arg_10);
            }
            else
            {
                if (ovr025.find_affect(out var_2D, arg_E, arg_10) == true &&
                    var_2D.field_1 > 0)
                {
                    remove_affect(var_2D, arg_E, arg_10);
                }

                add_affect(arg_8, arg_A, arg_C, arg_E, arg_10);

                if (var_29.Length != 0)
                {
                    ovr025.sub_6818A(var_29, 1, arg_10);
                    ovr025.ClearPlayerTextArea();
                }
            }
        }

        static Set unk_653B5 = new Set(0x0001, new byte[] { 0x33 });

        internal static bool heal_player(byte arg_0, byte amount_healed, Player player)
        {
            bool ret_val;

            ret_val = false;

            if (unk_653B5.MemberOf((byte)player.health_status) == true)
            {
                if (player.hit_point_current < player.hit_point_max ||
                    (player.hit_point_current >= player.hit_point_max &&
                    arg_0 == 0))
                {
                    player.hit_point_current = (byte)(amount_healed + player.hit_point_current);

                    if (player.hit_point_current > player.hit_point_max)
                    {
                        player.hit_point_current = player.hit_point_max;
                    }

                    if (player.in_combat == false)
                    {
                        if (player.health_status == Status.dying)
                        {
                            player.health_status = Status.unconscious;
                        }

                        if (player.health_status == Status.unconscious &&
                            gbl.game_state != 5)
                        {
                            sub_630C7(1, null, player, Affects.affect_4e);
                        }
                    }

                    ret_val = true;
                }
            }

            return ret_val;
        }


        internal static bool combat_heal(byte arg_0, Player player)
        {
            string var_2A;
            bool ret_val;

            if (ovr033.sub_7515A(1, ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player), player) != 0)
            {
                player.health_status = Status.okey;
                player.in_combat = true;
                player.hit_point_current = arg_0;

                if (gbl.game_state == 5)
                {
                    ovr033.sub_75356(false, 3, player);
                }

                if (player.combat_team == 1)
                {
                    var_2A = "stands up and grins";
                }
                else
                {
                    var_2A = "gets back up";
                }

                ovr025.DisplayPlayerStatusString(true, 10, var_2A, player);

                ovr025.count_teams();
                ret_val = true;
            }
            else
            {
                ret_val = false;
            }

            return ret_val;
        }
    }
}
