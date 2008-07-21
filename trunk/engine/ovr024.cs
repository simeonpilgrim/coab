using Classes;

namespace engine
{
    public class ovr024
    {
        internal static void sub_63014(string text, Status new_health_status, Player player)
        {
            ovr025.DisplayPlayerStatusString(false, 10, text, player);

            if (player.health_status != Status.stoned &&
                player.health_status != Status.dead &&
                player.health_status != Status.gone)
            {
                player.health_status = new_health_status;
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


        internal static void CallSpellJumpTable(Effect add_remove, object parameter, Player player, Affects affect) /* sub_630C7 */
        {
            if (gbl.byte_1D8AC == true)
            {
                gbl.affect_jump_list[147](add_remove, parameter, player);
            }
            else
            {
                gbl.affect_jump_list[(int)affect](add_remove, parameter, player);
            }
        }

        internal static void remove_affect(Affect affect, Affects affect_id, Player player)
        {
            if (affect == null)
            {
                affect = player.affect_ptr;
                while (affect != null && affect.type != affect_id)
                {
                    affect = affect.next;
                }
            }

            if (affect != null)
            {
                if (affect.call_spell_jump_list == true)
                {
                    CallSpellJumpTable(Effect.Remove, affect, player, affect_id);
                }

                if (player.affect_ptr == affect)
                {
                    player.affect_ptr = affect.next;
                }
                else
                {
                    Affect tmp_affect = player.affect_ptr;

                    while (tmp_affect != null && tmp_affect.next != affect)
                    {
                        tmp_affect = tmp_affect.next;
                    }

                    tmp_affect.next = affect.next;
                }

                affect = null; //seg051.FreeMem(9, affect);

                if (affect_id == Affects.resist_fire)
                {
                    sub_648D9(5, player);
                }

                if (affect_id == Affects.enlarge || 
                    affect_id == Affects.strength || 
                    affect_id == Affects.affect_92)
                {
                    sub_648D9(0, player);
                }
            }
        }

        static Set unk_6325A = new Set(0x0205, new byte[] { 0x20, 0x00, 0x00, 0x60, 0x02 });

        internal static void calc_affect_effect(Affects affect_type, Player player)
        {
            bool found = false;

            Affect affect;
            if (ovr025.find_affect(out affect, affect_type, player) == true)
            {
                found = true;
            }
            else if (unk_6325A.MemberOf((int)affect_type) == true)
            {
                Player player_base = gbl.player_next_ptr;

                SortedCombatant[] bkup_list = new SortedCombatant[gbl.MaxSortedCombatantCount];
                System.Array.Copy(gbl.SortedCombatantList, bkup_list, gbl.MaxSortedCombatantCount);

                while (player_base != null && found == false)
                {
                    if (ovr025.find_affect(out affect, affect_type, player_base) == true)
                    {
                        if (gbl.game_state == 5)
                        {
                            int max_range = (affect_type == Affects.prayer) ? 6 : 1;

                            ovr032.Rebuild_SortedCombatantList(gbl.mapToBackGroundTile, ovr033.PlayerMapSize(player_base), 0xff,
                                max_range, ovr033.PlayerMapYPos(player_base), ovr033.PlayerMapXPos(player_base));

                            for (int i = 0; i < gbl.sortedCombatantCount; i++)
                            {
                                if (gbl.SortedCombatantList[i].player_index == ovr033.get_player_index(player))
                                {
                                    found = true;
                                }
                            }
                        }
                        else
                        {
                            found = true;
                        }
                    }

                    player_base = player_base.next_player;
                }

                System.Array.Copy(bkup_list, gbl.SortedCombatantList, gbl.MaxSortedCombatantCount);
            }

            if (found == true)
            {
                CallSpellJumpTable(Effect.Add, affect, player, affect_type);
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
                    calc_affect_effect(Affects.sp_dispel_evil, player);
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
                    calc_affect_effect(Affects.sticks_to_snakes, player);
                    calc_affect_effect(Affects.fumbling, player);
                    calc_affect_effect(Affects.entangle, player);
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
                    calc_affect_effect(Affects.stinking_cloud, player);
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
                    calc_affect_effect(Affects.spit_acid, player);
                    calc_affect_effect(Affects.affect_57, player);
                    calc_affect_effect(Affects.breath_acid, player);
                    calc_affect_effect(Affects.affect_7e, player);
                    calc_affect_effect(Affects.affect_80, player);
                    calc_affect_effect(Affects.cast_breath_fire, player);
                    calc_affect_effect(Affects.cast_throw_lightening, player);
                    calc_affect_effect(Affects.affect_8b, player);
                    break;

                case 15:
                    calc_affect_effect(Affects.silence_15_radius, player);
                    calc_affect_effect(Affects.stinking_cloud, player);
                    calc_affect_effect(Affects.charm_person, player);
                    calc_affect_effect(Affects.reduce, player);
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


        static Player sub_63D03(byte[] arg_0, byte arg_4, Struct_1D885 arg_6, int mapY, int mapX)
        {
            bool found = false;

            while (arg_6 != null && found == false)
            {
                for (int i = 1; i <= arg_4; i++)
                {
                    if (arg_6.field_10[i] != 0 &&
                        arg_6.target_x + gbl.MapDirectionXDelta[arg_0[i]] == mapX &&
                        arg_6.target_y + gbl.MapDirectionYDelta[arg_0[i]] == mapY)
                    {
                        found = true;
                    }
                }

                if (found == false)
                {
                    arg_6 = arg_6.next;
                }
            }

            Player player_base = (found) ? arg_6.player : gbl.player_next_ptr;

            return player_base;
        }


        internal static void in_poison_cloud(byte arg_0, Player player)
        {
            if (player.in_combat == true)
            {
                bool isPoisonousCloud;
                bool isNoxiouxCloud;
                byte dummyGroundTile;
                byte dummyPlayerIndex;
                ovr033.getGroundInformation(out isPoisonousCloud, out isNoxiouxCloud, out dummyGroundTile, out dummyPlayerIndex, 8, player);

                Affect affect;

                if (isNoxiouxCloud && arg_0 != 0 &&
                    ovr025.find_affect(out affect, Affects.helpless, player) == false &&
                    ovr025.find_affect(out affect, Affects.animate_dead, player) == false &&
                    ovr025.find_affect(out affect, Affects.affect_6f, player) == false &&
                    ovr025.find_affect(out affect, Affects.affect_7d, player) == false)
                {
                    bool save_passed = do_saving_throw(0, 0, player);

                    if (save_passed == true)
                    {
                        Player tmp_player_ptr = gbl.player_ptr;

                        gbl.player_ptr = sub_63D03(gbl.unk_18AEA, 4, gbl.stru_1D885,
                            ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player));

                        is_unaffected("starts to cough", save_passed, 0, false, 0xff, 1, Affects.stinking_cloud, player);

                        if (ovr025.find_affect(Affects.stinking_cloud, player) == true)
                        {
                            CallSpellJumpTable(Effect.Add, affect, player, Affects.stinking_cloud);
                        }

                        gbl.player_ptr = tmp_player_ptr;
                    }
                    else
                    {
                        Player tmp_player_ptr = gbl.player_ptr;

                        gbl.player_ptr = sub_63D03(gbl.unk_18AEA, 4, gbl.stru_1D885,
                            ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player));

                        is_unaffected("chokes and gags from nausea", save_passed, 0, false, 0xff, (ushort)(roll_dice(4, 1) + 1), Affects.helpless, player);

                        if (ovr025.find_affect(out affect, Affects.helpless, player) == true)
                        {
                            CallSpellJumpTable(Effect.Add, affect, player, Affects.helpless);
                        }

                        gbl.player_ptr = tmp_player_ptr;
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


        internal static bool sub_641DD(byte arg_0, Player target)
        {
            bool hit = false;
            gbl.attack_roll = roll_dice(20, 1);

            if (gbl.attack_roll > 1)
            {
                if (gbl.attack_roll == 20)
                {
                    gbl.attack_roll = 100;
                }

                work_on_00(target, 16);

                if (gbl.attack_roll >= 0)
                {
                    if ((gbl.attack_roll + arg_0) > target.ac)
                    {
                        hit = true;
                    }
                }
            }

            return hit;
        }


        internal static bool attacker_can_hit_target(int target_ac, Player target, Player attacker) /* sub_64245 */
        {
            bool hit = false;

            remove_invisibility(attacker);
            gbl.attack_roll = roll_dice(20, 1);

            if (gbl.attack_roll > 1)
            {
                if (gbl.attack_roll == 20)
                {
                    gbl.attack_roll = 100;
                }

                work_on_00(attacker, 10);
                work_on_00(target, 16);

                int team_bonus;
                if (attacker.combat_team == CombatTeam.Ours)
                {
                    team_bonus = gbl.area2_ptr.field_6E2;
                }
                else
                {
                    team_bonus = gbl.area2_ptr.field_6E0;
                }

                if (gbl.attack_roll >= 0)
                {
                    if ((gbl.attack_roll + attacker.hitBonus + team_bonus) >= target_ac)
                    {
                        hit = true;
                    }
                }
            }
            return hit;
        }


        internal static bool do_saving_throw(int save_bonus, byte arg_2, Player player)
        {
            gbl.save_made = true;

            gbl.saving_throw_roll = roll_dice(20, 1);

            if (Cheats.player_always_saves && player.combat_team == 0)
            {
                gbl.saving_throw_roll = 20;
            }

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
                gbl.saving_throw_roll += save_bonus + player.field_186;
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


        internal static void add_affect(bool call_spell_jump_list, int arg_2, ushort arg_4, Affects type, Player player)
        {
            Affect affect = new Affect(type, arg_4, (byte)arg_2, call_spell_jump_list);

            if (player.affect_ptr == null)
            {
                player.affect_ptr = affect;
            }
            else
            {
                Affect affect_tmp = player.affect_ptr;

                while (affect_tmp.next != null)
                {
                    affect_tmp = affect_tmp.next;
                }

                affect_tmp.next = affect;
            }
        }


        internal static void sub_644A7(string msg, Status health_status, Player player)
        {
            if (player.in_combat == true)
            {
                byte player_index = ovr033.get_player_index(player);

                ovr033.sub_75356(false, 3, player);

                ovr025.DisplayPlayerStatusString(true, 10, msg, player);

                player.in_combat = false;

                player.health_status = health_status;

                if (player.health_status != Status.running)
                {
                    player.hit_point_current = 0;
                }

                ovr033.draw_74572(player_index, 0, 0);
                seg040.DrawOverlay();

                gbl.CombatMap[player_index].size = 0;

                ovr033.setup_mapToPlayerIndex_and_playerScreen();

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
            Affects[] table = { 
								  Affects.faerie_fire,
								  Affects.charm_person,
								  Affects.reduce,
								  Affects.silence_15_radius,
								  Affects.spiritual_hammer,
								  Affects.stinking_cloud,
								  Affects.helpless,
								  Affects.animate_dead,
								  Affects.snake_charm,
								  Affects.paralyze,
								  Affects.sleep,
								  Affects.affect_3a,
								  Affects.regenerate,
								  Affects.affect_5F,
								  Affects.affect_62,
								  Affects.entangle,
								  Affects.affect_89,
								  Affects.affect_8b,
								  Affects.affect_90
							  };

            for (int i = 0; i < 19; i++)
            {
                remove_affect(null, table[i], player);
            }

            if (ovr025.find_affect(Affects.berserk, player) == true &&
                player.field_F7 == 0xB3)
            {
                player.combat_team = CombatTeam.Ours;
            }
        }


        internal static void sub_6460D(Player arg_0)
        {
            Affects[] table = {   Affects.reduce, 
								  Affects.affect_3a, 
								  Affects.affect_8b, 
								  Affects.affect_90 };

            for (int var_1 = 0; var_1 < 4; var_1++)
            {
                remove_affect(null, table[var_1], arg_0);
            }
        }


        internal static bool cure_affect(Affects affectId, Player player) /* is_cured */
        {
            Affect affect;
            if (ovr025.find_affect(out affect, affectId, player) == true)
            {
                ovr025.DisplayPlayerStatusString(true, 10, "is Cured", player);

                remove_affect(affect, affectId, player);

                return true;
            }

            return false;
        }


        internal static byte encode_strength(byte str_00, byte str) /* odd_math */
        {
            byte ret_val = (byte)(str + 100);

            if (str == 18)
            {
                ret_val = (byte)(str_00 + 1);
            }

            return ret_val;
        }


        internal static void decode_strength(out byte str_00, out byte str, Affect arg_8) /* sub_646D9 */
        {
            str_00 = 0;
            str = (byte)(arg_8.field_3 & 0x7F);

            if (str <= 101)
            {
                str_00 = (byte)(str - 1);
                str = 18;
            }
            else
            {
                str -= 100;
            }
        }


        internal static bool sub_64728(out byte encoded_str, byte str_100, byte str, Player player)
        {
            bool encoded;

            if (str > player.tmp_str ||
                (str == 18 && str_100 > player.max_str_00))
            {
                encoded = true;
                encoded_str = encode_strength(str_100, str);
            }
            else
            {
                encoded = false;
                encoded_str = 0;
            }

            return encoded;
        }


        internal static void max_strength(ref byte str_a, byte str_b, ref byte str_00_a, byte str_00_b) /* sub_64771 */
        {
            if (str_b > str_a ||
                (str_b == 18 && str_00_b > str_00_a))
            {
                str_a = str_b;
                str_00_a = str_00_b;
            }
        }


        internal static void sub_647BE(byte arg_2, int class_index, byte bp_var_1, ref byte bp_var_A, Player player)
        {
            if (gbl.max_class_levels[class_index] <= arg_2)
            {
                arg_2 = (byte)(gbl.max_class_levels[class_index] - 1);
            }

            if (class_index == 4 &&
                (player.field_E6 == 0 || player.field_115 == player.field_E6))
            {
                arg_2 += 1;
            }

            if (class_index == 2 || class_index == 3 || class_index == 4)
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


        internal static void sub_648D9(int stat_index, Player player)
        {
            Affect affect_ptr;

            byte var_A = 0;
            byte stat_b = 0;
            byte str_00_b = 0;
            byte var_11 = 0x0FF;

            Item item = player.itemsPtr;

            byte stat_a = player.stats[stat_index].tmp;
            byte str_00_a = player.max_str_00;

            while (item != null)
            {
                if ((int)item.affect_3 > 0x80 &&
                    item.readied == true)
                {
                    int var_12 = (int)item.affect_3 & 0x7F;

                    if (stat_index == 0)
                    {
                        if (var_12 == 5)
                        {
                            switch ((int)item.affect_2)
                            {
                                case 0:
                                    stat_b = 18;
                                    str_00_b = 100;
                                    break;

                                case 1:
                                    stat_b = 19;
                                    break;

                                case 2:
                                    stat_b = 20;
                                    break;

                                case 3:
                                    stat_b = 21;
                                    break;

                                case 4:
                                    stat_b = 22;
                                    break;

                                case 5:
                                    stat_b = 23;
                                    break;

                                case 6:
                                    stat_b = 24;
                                    break;
                            }
                        }
                        else if (var_12 == 8)
                        {
                            if (player.tmp_str < 18 &&
                                item.affect_2 == 0)
                            {
                                stat_b = (byte)(player.tmp_str + 1);
                                str_00_b = 0;
                            }
                        }
                        else if (var_12 == 13)
                        {
                            var_11 = 3;
                        }

                        max_strength(ref stat_a, stat_b, ref str_00_a, str_00_b);
                    }
                    else if (stat_index == 4)
                    {
                        if (var_12 == 6)
                        {
                            stat_a++;
                        }
                        else if (var_12 == 8 &&
                            player.stats[(int)Stat.CON].tmp < 18 &&
                            (int)item.affect_2 == 4)
                        {
                            stat_a++;
                        }
                    }
                    else if (stat_index == 1)
                    {
                        if (var_12 == 8)
                        {
                            if (player.stats[(int)Stat.INT].tmp < 0x18 &&
                                (int)item.affect_2 == 1)
                            {
                                stat_a++;
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
                    else if (stat_index == 2)
                    {
                        if (var_12 == 8 &&
                            (int)item.affect_2 == 2 &&
                            player.tmp_wis < 18)
                        {
                            stat_a++;
                        }
                    }
                    else if (stat_index == 3)
                    {
                        if (var_12 == 2)
                        {
                            if (player.tmp_dex >= 0 && player.tmp_dex <= 6)
                            {
                                stat_a += 4;
                            }
                            else if (player.tmp_dex >= 7 && player.tmp_dex <= 13)
                            {
                                stat_a += 2;
                            }
                            else
                            {
                                stat_a++;
                            }
                        }
                        else if (var_12 == 8)
                        {
                            if (player.tmp_dex < 18 &&
                                (int)item.affect_2 == 3)
                            {
                                stat_a++;
                            }
                        }
                        else if (var_12 == 10)
                        {
                            stat_a -= 2;
                        }
                    }
                    else if (stat_index == 5)
                    {
                        if (var_12 == 6)
                        {
                            stat_a -= 1;
                        }
                        else if (var_12 == 8 &&
                            player.tmp_cha < 18 &&
                            (int)item.affect_2 == 5)
                        {
                            stat_a += 1;
                        }
                    }
                }

                item = item.next;
            }

            if (stat_index == 0)
            {
                if (ovr025.find_affect(out affect_ptr, Affects.strength, player) == true)
                {
                    decode_strength(out str_00_b, out stat_b, affect_ptr);

                    if (stat_a <= 18 &&
                        str_00_a < 100)
                    {
                        stat_b += stat_a;

                        if (stat_b > 18)
                        {
                            if (player.fighter_lvl > 0 ||
                                player.field_113 > 0 ||
                                player.paladin_lvl > 0 ||
                                player.field_114 > 0 ||
                                player.ranger_lvl > 0 ||
                                player.field_115 > 0)
                            {
                                str_00_b = (byte)(player.tmp_str_00 + ((stat_b - 18) * 10));

                                if (str_00_b > 100)
                                {
                                    str_00_b = 100;
                                }

                                stat_b = 18;
                            }
                            else
                            {
                                stat_b = 18;
                            }
                        }
                    }


                    max_strength(ref stat_a, stat_b, ref str_00_a, str_00_b);
                }

                if (ovr025.find_affect(out affect_ptr, Affects.affect_92, player) == true)
                {
                    decode_strength(out str_00_b, out stat_b, affect_ptr);
                    max_strength(ref stat_a, stat_b, ref str_00_a, str_00_b);
                }

                if (ovr025.find_affect(out affect_ptr, Affects.enlarge, player) == true)
                {
                    decode_strength(out str_00_b, out stat_b, affect_ptr);
                    max_strength(ref stat_a, stat_b, ref str_00_a, str_00_b);
                }

                if (var_11 != 0xff)
                {
                    player.strength = var_11;
                    player.tmp_str_00 = 0;
                }
                else
                {
                    player.strength = stat_a;
                    player.tmp_str_00 = str_00_a;
                }
            }
            else if (stat_index == 4)
            {
                byte var_13 = 0;
                byte map_hp = player.hit_point_max;
                player.hit_point_max = player.field_12C;

                for (int var_C = 0; var_C <= 7; var_C++)
                {
                    byte var_B = player.Skill_B_lvl[var_C];

                    if (var_B > 0)
                    {
                        sub_647BE(var_B, var_C, stat_a, ref var_A, player);
                    }

                    var_B = player.class_lvls[var_C];

                    if (var_B > 0)
                    {
                        var_13++;
                    }

                    if (gbl.max_class_levels[var_C] < var_B)
                    {
                        var_B = gbl.max_class_levels[var_C];
                    }

                    if (var_B > player.field_E6)
                    {
                        var_B = player.field_E6;

                        sub_647BE(var_B, var_C, stat_a, ref var_A, player);
                    }
                }

                var_A /= var_13;
                player.hit_point_max += var_A;

                if (player.hit_point_max > map_hp)
                {
                    player.hit_point_current = (byte)(player.hit_point_max - map_hp);
                }

                if (player.hit_point_max < map_hp)
                {
                    if (player.hit_point_current > map_hp - player.hit_point_max)
                    {
                        player.hit_point_current = (byte)(map_hp - player.hit_point_max);
                    }
                    else
                    {
                        player.hit_point_current = 0;
                    }
                }

                player.con = stat_a;

                if (player.con > 20)
                {
                    if (ovr025.find_affect(Affects.affect_3e, player) == true)
                    {
                        add_affect(false, 0xff, 0x3c, Affects.affect_3e, player);
                    }
                }
                else
                {
                    remove_affect(null, Affects.affect_3e, player);
                }
            }
            else if (stat_index == 1)
            {
                if (ovr025.find_affect(Affects.feeblemind, player) == true &&
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
                    player._int = stat_a;
                }
            }
            else if (stat_index == 2)
            {
                if (ovr025.find_affect(Affects.feeblemind, player) == true &&
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
                    player.wis = stat_a;
                }
            }
            else if (stat_index == 3)
            {
                if (var_11 != 0xff)
                {
                    player.dex = var_11;
                }
                else
                {
                    player.dex = stat_a;
                }
            }
            else if (stat_index == 5)
            {
                if (ovr025.find_affect(out affect_ptr, Affects.friends, player) == true)
                {
                    stat_a = affect_ptr.field_3;
                }

                player.charisma = stat_a;
            }
        }

        static Set unk_64F90 = new Set(0x0002, new byte[] { 0xC0, 0x01 });

        internal static void damage_person(bool change_damage, byte arg_2, int damage, Player player)
        {
            string text;

            gbl.damage = damage;

            work_on_00(player, 6);

            if (change_damage == true)
            {
                if (arg_2 == 1)
                {
                    gbl.damage = 0;
                }
                else if (arg_2 == 2)
                {
                    gbl.damage /= 2;
                }
            }
            else
            {
                work_on_00(player, 20);
            }

            if (gbl.damage > 0)
            {
                if (gbl.damage != 1)
                {
                    text = "takes " + gbl.damage.ToString() + " points of damage ";
                }
                else
                {
                    text = "takes 1 point of damage ";
                }

                int mask = gbl.damage_flags & 0xf7;
                if (mask == 0x01)
                {
                    text += "from Fire";
                }
                else if (mask == 0x02)
                {
                    text += "from Cold";
                }
                else if (mask == 0x04)
                {
                    text += "from Electricity";
                }
                else if (mask == 0x10)
                {
                    text += "from Acid";
                }

                if ((gbl.damage_flags & 8) == gbl.damage_flags)
                {
                    text += "from Magic";
                }

                ovr025.sub_6818A(text, false, player);
                ovr025.damage_player(gbl.damage, player);

                if (gbl.game_state == 5)
                {
                    player.actions.can_cast = false;

                    if (player.actions.spell_id > 0)
                    {
                        ovr025.DisplayPlayerStatusString(true, 12, "lost a spell", player);

                        ovr025.clear_spell(player.actions.spell_id, player);

                        player.actions.spell_id = 0;
                    }
                }

                if (player.in_combat == false)
                {
                    text = "Goes Down";

                    if (player.health_status == Status.dying)
                    {
                        text = text + ", and is Dying";
                    }

                    if (unk_64F90.MemberOf((int)player.health_status) == true)
                    {
                        text = "is killed";
                    }

                    ovr025.DisplayPlayerStatusString(false, (byte)(gbl.textYCol + 1), text, player);

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


        internal static void is_unaffected(string text, bool saved, byte can_save, bool call_spell_jump_list, int arg_A, ushort arg_C, Affects affect_id, Player target)
        {
            gbl.current_affect = affect_id;

            work_on_00(target, 9);

            if (gbl.current_affect == 0 ||
                (saved == true && can_save == 1))
            {
                ovr025.DisplayPlayerStatusString(true, 10, "is Unaffected", target);
            }
            else
            {
                Affect found_affect;

                if (ovr025.find_affect(out found_affect, affect_id, target) == true &&
                    found_affect.field_1 > 0)
                {
                    remove_affect(found_affect, affect_id, target);
                }

                add_affect(call_spell_jump_list, arg_A, arg_C, affect_id, target);

                if (text.Length != 0)
                {
                    ovr025.sub_6818A(text, true, target);
                    ovr025.ClearPlayerTextArea();
                }
            }
        }

        static Set unk_653B5 = new Set(0x0001, new byte[] { 0x33 });

        internal static bool heal_player(byte arg_0, byte amount_healed, Player player)
        {
            if (unk_653B5.MemberOf((int)player.health_status) == true)
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
                            CallSpellJumpTable(Effect.Remove, null, player, Affects.affect_4e);
                        }
                    }

                    return true;
                }
            }

            return false;
        }


        internal static bool combat_heal(byte arg_0, Player player)
        {
            if (ovr033.sub_7515A(true, ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player), player) == true)
            {
                player.health_status = Status.okey;
                player.in_combat = true;
                player.hit_point_current = arg_0;

                if (gbl.game_state == 5)
                {
                    ovr033.sub_75356(false, 3, player);
                }

                string text;
                if (player.combat_team == CombatTeam.Enemy)
                {
                    text = "stands up and grins";
                }
                else
                {
                    text = "gets back up";
                }

                ovr025.DisplayPlayerStatusString(true, 10, text, player);

                ovr025.count_teams();

                return true;
            }

            return false;
        }
    }
}
