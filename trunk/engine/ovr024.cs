using Classes;
using System.Collections.Generic;

namespace engine
{
    internal enum CheckType
    {
        None = 0,
        Visibility = 1,
        Type_2 = 2,
        Type_3 = 3,
        Type_4 = 4,
        Type_5 = 5,
        Type_6 = 6,
        Type_7 = 7,
        Type_8 = 8,
        Type_9 = 9,
        Type_10 = 10,
        Type_11 = 11,
        SavingThrow = 12,
        Death = 13,
        Type_14 = 14,
        Type_15 = 15,
        Type_16 = 16,
        Morale = 17,
        Movement = 18,
        Type_19 = 19,
        FireShield = 20,
        Confusion = 21,
        Type_22 = 22,
        Type_23 = 23
    }

    public class ovr024
    {
        internal static void KillPlayer(string text, Status new_health_status, Player player) // sub_63014
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
                CheckAffectsEffect(player, CheckType.Death);

                if (player.in_combat == false)
                {
                    ovr033.sub_74E6F(player);
                }

                seg041.GameDelay();
                ovr025.ClearPlayerTextArea();

                if (gbl.game_state == GameState.Combat)
                {
                    ovr025.PartySummary(gbl.player_ptr);
                }
            }
        }


        internal static void remove_affect(Affect affect, Affects affect_id, Player player)
        {
            if (affect == null)
            {
                affect = player.GetAffect(affect_id);
            }

            if (affect != null)
            {
                if (affect.callAffectTable == true)
                {
                    ovr013.CallAffectTable(Effect.Remove, affect, player, affect_id);
                }

                player.affects.Remove(affect);

                if (affect_id == Affects.resist_fire)
                {
                    CalcStatBonuses(Stat.CHA, player);
                }

                if (affect_id == Affects.enlarge || 
                    affect_id == Affects.strength || 
                    affect_id == Affects.strenght_spell)
                {
                    CalcStatBonuses(Stat.STR, player);
                }
            }
        }

        static Set unk_6325A = new Set(0x0205, new byte[] { 0x20, 0x00, 0x00, 0x60, 0x02 });

        internal static void calc_affect_effect(Affects affect_type, Player player)
        {
            bool found = false;

            Affect affect;
            if (ovr025.FindAffect(out affect, affect_type, player) == true)
            {
                found = true;
            }
            else if (unk_6325A.MemberOf((int)affect_type) == true)
            {
                foreach (Player player_base in gbl.player_next_ptr)
                {
                    if (found) break;

                    if (ovr025.FindAffect(out affect, affect_type, player_base) == true)
                    {
                        if (gbl.game_state == GameState.Combat)
                        {
                            int max_range = (affect_type == Affects.prayer) ? 6 : 1;

                            var scl = ovr032.Rebuild_SortedCombatantList(gbl.mapToBackGroundTile, ovr033.PlayerMapSize(player_base), 0xff,
                                max_range, ovr033.PlayerMapPos(player_base));

                            found = scl.Exists(sc => sc.player == player);
                        }
                        else
                        {
                            found = true;
                        }
                    }
                }
            }

            if (found == true)
            {
                ovr013.CallAffectTable(Effect.Add, affect, player, affect_type);
            }
        }

        static internal void CheckAffectsEffect(Player player, CheckType type) // work_on_00
        {
            switch (type)
            {
                case CheckType.None:
                    break;

                case CheckType.Visibility:
                    calc_affect_effect(Affects.blink, player);
                    calc_affect_effect(Affects.invisibility, player);
                    calc_affect_effect(Affects.invisible, player);
                    calc_affect_effect(Affects.invisible_to_animals, player);
                    break;

                case CheckType.Type_2:
                    calc_affect_effect(Affects.fireAttack_2d10, player);
                    calc_affect_effect(Affects.ankheg_acid_attack, player);
                    calc_affect_effect(Affects.sp_dispel_evil, player);
                    calc_affect_effect(Affects.affect_39, player);
                    calc_affect_effect(Affects.owlbear_hug_check, player);
                    calc_affect_effect(Affects.dracolich_paralysis, player);
                    calc_affect_effect(Affects.affect_7b, player);
                    break;

                case CheckType.Type_3:
                    calc_affect_effect(Affects.poison_plus_0, player);
                    calc_affect_effect(Affects.poison_plus_4, player);
                    calc_affect_effect(Affects.poison_plus_2, player);
                    calc_affect_effect(Affects.thri_kreen_paralyze, player);
                    calc_affect_effect(Affects.poison_neg_2, player);
                    calc_affect_effect(Affects.fireAttack_2d10, player);
                    calc_affect_effect(Affects.affect_57, player);
                    break;

                case CheckType.Type_4:
                    calc_affect_effect(Affects.ray_of_enfeeblement, player);
                    calc_affect_effect(Affects.affect_06, player);
                    calc_affect_effect(Affects.salamander_heat_damage, player);
                    calc_affect_effect(Affects.weap_dragon_slayer, player);
                    calc_affect_effect(Affects.weap_frost_brand, player);
                    calc_affect_effect(Affects.ranger_vs_giant, player);
                    break;

                case CheckType.Type_5:
                    calc_affect_effect(Affects.mirror_image, player);
                    calc_affect_effect(Affects.prot_from_normal_missiles, player);
                    calc_affect_effect(Affects.thri_kreen_dodge_missile, player);
                    calc_affect_effect(Affects.affect_78, player);
                    calc_affect_effect(Affects.troll_regen, player);
                    calc_affect_effect(Affects.affect_73, player);
                    calc_affect_effect(Affects.affect_74, player);
                    calc_affect_effect(Affects.affect_77, player);
                    calc_affect_effect(Affects.affect_5e, player);
                    calc_affect_effect(Affects.affect_75, player);
                    calc_affect_effect(Affects.resist_normal_weapons, player);
                    calc_affect_effect(Affects.half_damage, player);
                    calc_affect_effect(Affects.resist_fire_and_cold, player);
                    calc_affect_effect(Affects.affect_55, player);
                    calc_affect_effect(Affects.affect_82, player);
                    calc_affect_effect(Affects.affect_8f, player);
                    break;

                case CheckType.Type_6:
                    calc_affect_effect(Affects.affect_71, player);
                    calc_affect_effect(Affects.fire_resist, player);
                    calc_affect_effect(Affects.resist_cold, player);
                    calc_affect_effect(Affects.resist_fire, player);
                    calc_affect_effect(Affects.affect_69, player);
                    calc_affect_effect(Affects.affect_6a, player);
                    calc_affect_effect(Affects.immune_to_fire, player);
                    calc_affect_effect(Affects.affect_72, player);
                    calc_affect_effect(Affects.affect_76, player);
                    calc_affect_effect(Affects.shield, player);
                    calc_affect_effect(Affects.affect_5d, player);
                    calc_affect_effect(Affects.troll_regen, player);
                    calc_affect_effect(Affects.mirror_image, player);
                    calc_affect_effect(Affects.affect_6e, player);
                    calc_affect_effect(Affects.prot_drag_breath, player);
                    calc_affect_effect(Affects.resist_fire_and_cold, player);
                    calc_affect_effect(Affects.shambling_absorb_lightning, player);
                    calc_affect_effect(Affects.protect_magic, player);
                    calc_affect_effect(Affects.affect_85, player);
                    calc_affect_effect(Affects.protect_elec, player);
                    calc_affect_effect(Affects.minor_globe_of_invulnerability, player);
                    break;

                case CheckType.Type_7:
                    calc_affect_effect(Affects.snake_charm, player);
                    calc_affect_effect(Affects.paralyze, player);
                    calc_affect_effect(Affects.sleep, player);
                    calc_affect_effect(Affects.helpless, player);
                    calc_affect_effect(Affects.sticks_to_snakes, player);
                    calc_affect_effect(Affects.fumbling, player);
                    calc_affect_effect(Affects.entangle, player);
                    break;

                case CheckType.Type_8:
                    calc_affect_effect(Affects.affect_63, player);
                    calc_affect_effect(Affects.resist_fire_and_cold, player);
                    calc_affect_effect(Affects.displace, player);
                    calc_affect_effect(Affects.camouflage, player);
                    calc_affect_effect(Affects.item_invisibility, player);
                    break;

                case CheckType.Type_9:
                    calc_affect_effect(Affects.affect_69, player);
                    calc_affect_effect(Affects.affect_6a, player);
                    calc_affect_effect(Affects.elf_resist_sleep, player);
                    calc_affect_effect(Affects.protect_charm_sleep, player);
                    calc_affect_effect(Affects.affect_6d, player);
                    calc_affect_effect(Affects.affect_6e, player);
                    calc_affect_effect(Affects.affect_6f, player);
                    calc_affect_effect(Affects.immune_to_fire, player);
                    calc_affect_effect(Affects.halfelf_resistance, player);
                    calc_affect_effect(Affects.affect_7d, player);
                    calc_affect_effect(Affects.minor_globe_of_invulnerability, player);
                    calc_affect_effect(Affects.protect_magic, player);
                    break;

                case CheckType.Type_10:
                    calc_affect_effect(Affects.bless, player);
                    calc_affect_effect(Affects.cursed, player);
                    calc_affect_effect(Affects.blinded, player);
                    calc_affect_effect(Affects.bestow_curse, player);
                    calc_affect_effect(Affects.prayer, player);
                    calc_affect_effect(Affects.affect_06, player);
                    calc_affect_effect(Affects.gnome_vs_man_sized_giant, player);
                    calc_affect_effect(Affects.dwarf_vs_orc, player);
                    calc_affect_effect(Affects.weap_dragon_slayer, player);
                    calc_affect_effect(Affects.weap_frost_brand, player);
                    break;

                case CheckType.Type_11:
                    calc_affect_effect(Affects.blinded, player);
                    calc_affect_effect(Affects.shield, player);
                    calc_affect_effect(Affects.protection_from_evil, player);
                    calc_affect_effect(Affects.protection_from_good, player);
                    calc_affect_effect(Affects.prot_from_evil_10_radius, player);
                    calc_affect_effect(Affects.prot_from_good_10_radius, player);
                    calc_affect_effect(Affects.stinking_cloud, player);
                    calc_affect_effect(Affects.faerie_fire, player);
                    break;

                case CheckType.SavingThrow:
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
                    calc_affect_effect(Affects.con_saving_bonus, player);
                    calc_affect_effect(Affects.hot_fire_shield, player);
                    calc_affect_effect(Affects.cold_fire_shield, player);
                    break;

                case CheckType.Death:
                    calc_affect_effect(Affects.affect_63, player);
                    calc_affect_effect(Affects.troll_fire_or_acid, player);
                    calc_affect_effect(Affects.weap_dragon_slayer, player);
                    break;

                case CheckType.Type_14:
                    calc_affect_effect(Affects.paralizing_gaze, player);
                    calc_affect_effect(Affects.breath_elec, player);
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

                case CheckType.Type_15:
                    calc_affect_effect(Affects.silence_15_radius, player);
                    calc_affect_effect(Affects.stinking_cloud, player);
                    calc_affect_effect(Affects.charm_person, player);
                    calc_affect_effect(Affects.reduce, player);
                    calc_affect_effect(Affects.berserk, player);
                    break;

                case CheckType.Type_16:
                    calc_affect_effect(Affects.invisibility, player);
                    calc_affect_effect(Affects.invisible, player);
                    calc_affect_effect(Affects.blink, player);
                    calc_affect_effect(Affects.dwarf_and_gnome_vs_giants, player);
                    calc_affect_effect(Affects.affect_30, player);
                    calc_affect_effect(Affects.displace, player);
                    calc_affect_effect(Affects.dispel_evil, player);
                    break;

                case CheckType.Morale:
                    calc_affect_effect(Affects.bless, player);
                    calc_affect_effect(Affects.cursed, player);
                    calc_affect_effect(Affects.charm_person, player);
                    break;

                case CheckType.Movement:
                    calc_affect_effect(Affects.haste, player);
                    calc_affect_effect(Affects.slow, player);
                    calc_affect_effect(Affects.clear_movement, player);
                    break;

                case CheckType.Type_19:
                    calc_affect_effect(Affects.regen_3_hp, player);
                    calc_affect_effect(Affects.spiritual_hammer, player);
                    calc_affect_effect(Affects.camouflage, player);
                    calc_affect_effect(Affects.item_invisibility, player);
                    calc_affect_effect(Affects.charm_person, player);
                    break;

                case CheckType.FireShield:
                    calc_affect_effect(Affects.hot_fire_shield, player);
                    calc_affect_effect(Affects.cold_fire_shield, player);
                    break;

                case CheckType.Confusion:
                    calc_affect_effect(Affects.confuse, player);
                    break;

                case CheckType.Type_22:
                    calc_affect_effect(Affects.affect_8a, player);
                    break;

                case CheckType.Type_23:
                    calc_affect_effect(Affects.affect_4a, player);
                    break;
            }
        }


        static Player sub_63D03(byte[] arg_0, int arraySize, List<GasCloud> list, Point mapPos)
        {
            var arg_6 = list.Find(cell =>
            {
                for (int i = 1; i <= arraySize; i++)
                {
                    if (cell.present[i] == true &&
                        cell.targetPos + gbl.MapDirectionDelta[arg_0[i]] == mapPos)
                    {
                        return true;
                    }
                }

                return false;
            });

            Player player_base = (arg_6 != null) ? arg_6.player : gbl.player_next_ptr[0];

            return player_base;
        }


        internal static void in_poison_cloud(byte arg_0, Player player)
        {
            if (player.in_combat == true)
            {
                bool isPoisonousCloud;
                bool isNoxiouxCloud;
                int dummyGroundTile;
                int dummyPlayerIndex;
                ovr033.getGroundInformation(out isPoisonousCloud, out isNoxiouxCloud, out dummyGroundTile, out dummyPlayerIndex, 8, player);

                Affect affect;

                if (isNoxiouxCloud && arg_0 != 0 &&
                    ovr025.FindAffect(out affect, Affects.helpless, player) == false &&
                    ovr025.FindAffect(out affect, Affects.animate_dead, player) == false &&
                    ovr025.FindAffect(out affect, Affects.affect_6f, player) == false &&
                    ovr025.FindAffect(out affect, Affects.affect_7d, player) == false)
                {
                    bool save_passed = RollSavingThrow(0, 0, player);

                    if (save_passed == true)
                    {
                        Player tmp_player_ptr = gbl.player_ptr;

                        gbl.player_ptr = sub_63D03(gbl.unk_18AEA, 4, gbl.NoxiousCloud, ovr033.PlayerMapPos(player));

                        is_unaffected("starts to cough", save_passed, 0, false, 0xff, 1, Affects.stinking_cloud, player);

                        if (player.HasAffect(Affects.stinking_cloud) == true)
                        {
                            ovr013.CallAffectTable(Effect.Add, affect, player, Affects.stinking_cloud);
                        }

                        gbl.player_ptr = tmp_player_ptr;
                    }
                    else
                    {
                        Player tmp_player_ptr = gbl.player_ptr;

                        gbl.player_ptr = sub_63D03(gbl.unk_18AEA, 4, gbl.NoxiousCloud, ovr033.PlayerMapPos(player));

                        is_unaffected("chokes and gags from nausea", save_passed, 0, false, 0xff, (ushort)(roll_dice(4, 1) + 1), Affects.helpless, player);

                        if (ovr025.FindAffect(out affect, Affects.helpless, player) == true)
                        {
                            ovr013.CallAffectTable(Effect.Add, affect, player, Affects.helpless);
                        }

                        gbl.player_ptr = tmp_player_ptr;
                    }
                }

                if (isPoisonousCloud == true &&
                    player.in_combat == true)
                {
                    if (player.HitDice >= 0 && player.HitDice <= 4)
                    {
                        ovr025.DisplayPlayerStatusString(false, 10, "is Poisoned", player);
                        seg041.GameDelay();
                        add_affect(false, 0xff, 0, Affects.minor_globe_of_invulnerability, player);
                        KillPlayer("is killed", Status.dead, player);
                    }
                    else if (player.HitDice == 5)
                    {
                        if (RollSavingThrow(-4, 0, player) == false)
                        {
                            ovr025.DisplayPlayerStatusString(false, 10, "is Poisoned", player);
                            seg041.GameDelay();
                            add_affect(false, 0xff, 0, Affects.poisoned, player);
                            KillPlayer("is killed", Status.dead, player);
                        }
                    }
                    else if (player.HitDice == 6)
                    {
                        if (RollSavingThrow(0, 0, player) == false)
                        {
                            ovr025.DisplayPlayerStatusString(false, 10, "is Poisoned", player);
                            seg041.GameDelay();
                            add_affect(false, 0xff, 0, Affects.poisoned, player);
                            KillPlayer("is killed", Status.dead, player);
                        }
                    }
                }
            }
        }


        internal static bool CanHitTarget(int bonus, Player target) // sub_641DD
        {
            bool hit = false;
            gbl.attack_roll = roll_dice(20, 1);

            if (gbl.attack_roll > 1)
            {
                // natural 20, always hits, so make it huge, so it always beats the AC.
                if (gbl.attack_roll == 20)
                {
                    gbl.attack_roll = 100;
                }

                CheckAffectsEffect(target, CheckType.Type_16);

                if (gbl.attack_roll >= 0)
                {
                    if ((gbl.attack_roll + bonus) > target.ac)
                    {
                        hit = true;
                    }
                }
            }

            return hit;
        }


        internal static bool PC_CanHitTarget(int target_ac, Player target, Player attacker) /* sub_64245 */
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

                CheckAffectsEffect(attacker, CheckType.Type_10);
                CheckAffectsEffect(target, CheckType.Type_16);

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


        internal static bool RollSavingThrow(int saveBonus, SaveVerseType saveType, Player player) // do_saving_throw
        {
            gbl.savingThrowMade = true;
            gbl.savingThrowRoll = roll_dice(20, 1);

            if (Cheats.player_always_saves && player.combat_team == 0)
            {
                gbl.savingThrowRoll = 20;
            }

            if (gbl.savingThrowRoll == 1)
            {
                gbl.savingThrowMade = false;
            }
            else if (gbl.savingThrowRoll == 20)
            {
                gbl.savingThrowMade = true;
            }
            else
            {
                gbl.savingThrowRoll += saveBonus + player.field_186;
                gbl.saveVerseType = saveType;

                CheckAffectsEffect(player, CheckType.SavingThrow);

                gbl.savingThrowMade = gbl.savingThrowRoll >= player.saveVerse[(int)saveType];
            }

            return gbl.savingThrowMade;
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


        internal static int roll_dice_save(int dice_size, int dice_count)
        {
            gbl.dice_count = dice_count;

            return roll_dice(dice_size, dice_count);
        }


        internal static void add_affect(bool call_spell_jump_list, int data, ushort minutes, Affects type, Player player)
        {
            Affect affect = new Affect(type, minutes, (byte)data, call_spell_jump_list);

            player.affects.Add(affect);
            //TODO simplify this funcation.
        }


        internal static void sub_644A7(string msg, Status health_status, Player player)
        {
            if (player.in_combat == true)
            {
                int player_index = ovr033.GetPlayerIndex(player);

                ovr033.RedrawCombatIfFocusOn(false, 3, player);

                ovr025.DisplayPlayerStatusString(true, 10, msg, player);

                player.in_combat = false;

                player.health_status = health_status;

                if (player.health_status != Status.running)
                {
                    player.hit_point_current = 0;
                }

                ovr033.RedrawPlayerBackground(player_index);
                seg040.DrawOverlay();

                gbl.CombatMap[player_index].size = 0;

                ovr033.setup_mapToPlayerIndex_and_playerScreen();

                ovr025.clear_actions(player);
                sub_645AB(player);
            }
        }


        internal static void remove_invisibility(Player player)
        {
            Affect affect;

            while (ovr025.FindAffect(out affect, Affects.invisibility, player) == true)
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
								  Affects.clear_movement,
								  Affects.regenerate,
								  Affects.affect_5F,
								  Affects.regen_3_hp,
								  Affects.entangle,
								  Affects.affect_89,
								  Affects.affect_8b,
								  Affects.owlbear_hug_round_attack
							  };

            System.Array.ForEach(table, affect => remove_affect(null, affect, player));

            if (player.HasAffect(Affects.berserk) == true && player.field_F7 == 0xB3)
            {
                player.combat_team = CombatTeam.Ours;
            }
        }


        internal static void sub_6460D(Player player) // sub_6460D
        {
            Affects[] table = {   Affects.reduce, 
								  Affects.clear_movement, 
								  Affects.affect_8b, 
								  Affects.owlbear_hug_round_attack };

            System.Array.ForEach(table, affect => remove_affect(null, affect, player));
        }


        internal static bool cure_affect(Affects affectId, Player player) /* is_cured */
        {
            Affect affect = player.GetAffect(affectId);
            if (affect != null)
            {
                ovr025.DisplayPlayerStatusString(true, 10, "is Cured", player);

                remove_affect(affect, affectId, player);

                return true;
            }

            return false;
        }


        internal static int encode_strength(int str_00, int str) /* odd_math */
        {
            int ret_val = str + 100;

            if (str == 18)
            {
                ret_val = str_00 + 1;
            }

            return ret_val;
        }


        internal static void decode_strength(out byte str_00, out byte str, Affect arg_8) /* sub_646D9 */
        {
            str_00 = 0;
            str = (byte)(arg_8.affect_data & 0x7F);

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


        internal static bool TryEncodeStrength(out int encoded_str, int str_100, int str, Player player) // sub_64728
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


        internal static void CalcStatBonuses(Stat stat_index, Player player) // sub_648D9
        {
            byte var_A = 0;
            byte stat_b = 0;
            byte str_00_b = 0;
            byte var_11 = 0x0FF;

            byte stat_a = player.stats[(int)stat_index].tmp;
            byte str_00_a = player.max_str_00;

            foreach (Item item in player.items)
            {
                if ((int)item.affect_3 > 0x80 && item.readied == true)
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
                    else if (stat_index == Stat.CON)
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
                    else if (stat_index == Stat.INT)
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
                    else if (stat_index == Stat.WIS)
                    {
                        if (var_12 == 8 &&
                            (int)item.affect_2 == 2 &&
                            player.tmp_wis < 18)
                        {
                            stat_a++;
                        }
                    }
                    else if (stat_index == Stat.DEX)
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
                    else if (stat_index == Stat.CHA)
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
            }

            if (stat_index == 0) // Strength
            {
                Affect affect_ptr;

                if (ovr025.FindAffect(out affect_ptr, Affects.strength, player) == true)
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

                if (ovr025.FindAffect(out affect_ptr, Affects.strenght_spell, player) == true)
                {
                    decode_strength(out str_00_b, out stat_b, affect_ptr);
                    max_strength(ref stat_a, stat_b, ref str_00_a, str_00_b);
                }

                if (ovr025.FindAffect(out affect_ptr, Affects.enlarge, player) == true)
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
            else if (stat_index == Stat.CON)
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
                    if (player.HasAffect(Affects.highConRegen) == true)
                    {
                        add_affect(false, 0xff, 0x3c, Affects.highConRegen, player);
                    }
                }
                else
                {
                    remove_affect(null, Affects.highConRegen, player);
                }
            }
            else if (stat_index == Stat.INT)
            {
                if (player.HasAffect(Affects.feeblemind) == true && var_11 > 7)
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
            else if (stat_index == Stat.WIS)
            {
                if (player.HasAffect(Affects.feeblemind) == true &&
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
            else if (stat_index == Stat.DEX)
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
            else if (stat_index == Stat.CHA)
            {
                Affect affect;
                if (ovr025.FindAffect(out affect, Affects.friends, player) == true)
                {
                    stat_a = affect.affect_data;
                }

                player.charisma = stat_a;
            }
        }

        static Set unk_64F90 = new Set(0x0002, new byte[] { 0xC0, 0x01 });

        internal static void damage_person(bool change_damage, DamageOnSave arg_2, int damage, Player player)
        {
            string text;

            gbl.damage = damage;

            CheckAffectsEffect(player, CheckType.Type_6);

            if (change_damage == true)
            {
                if (arg_2 == DamageOnSave.Zero)
                {
                    gbl.damage = 0;
                }
                else if (arg_2 == DamageOnSave.Half)
                {
                    gbl.damage /= 2;
                }
            }
            else
            {
                CheckAffectsEffect(player, CheckType.FireShield);
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

                int mask = (int)gbl.damage_flags & 0xf7;
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

                if ((gbl.damage_flags & DamageType.Magic) == gbl.damage_flags)
                {
                    text += "from Magic";
                }

                ovr025.MagicAttackDisplay(text, false, player);
                ovr025.damage_player(gbl.damage, player);

                if (gbl.game_state == GameState.Combat)
                {
                    TryLooseSpell(player);
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

                    ovr025.DisplayPlayerStatusString(false, gbl.textYCol + 1, text, player);

                    if (gbl.game_state == GameState.Combat)
                    {
                        seg041.GameDelay();
                    }
                    else
                    {
                        sub_645AB(player);

                        CheckAffectsEffect(player, CheckType.Death);

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

        internal static void TryLooseSpell(Player player)
        {
            player.actions.can_cast = false;

            if (player.actions.spell_id > 0)
            {
                ovr025.DisplayPlayerStatusString(true, 12, "lost a spell", player);

                player.ClearSpell(player.actions.spell_id);
                player.actions.spell_id = 0;
            }
        }


        internal static void is_unaffected(string text, bool saved, DamageOnSave can_save, bool call_affect_table, int data, ushort time, Affects affect_id, Player target)
        {
            gbl.current_affect = affect_id;

            CheckAffectsEffect(target, CheckType.Type_9);

            if (gbl.current_affect == 0 ||
                (saved == true && can_save == DamageOnSave.Zero))
            {
                ovr025.DisplayPlayerStatusString(true, 10, "is Unaffected", target);
            }
            else
            {
                Affect found_affect;

                if (ovr025.FindAffect(out found_affect, affect_id, target) == true &&
                    found_affect.minutes > 0)
                {
                    remove_affect(found_affect, affect_id, target);
                }

                add_affect(call_affect_table, data, time, affect_id, target);

                if (text.Length != 0)
                {
                    ovr025.MagicAttackDisplay(text, true, target);
                    ovr025.ClearPlayerTextArea();
                }
            }
        }

        static Set unk_653B5 = new Set(0x0001, new byte[] { 0x33 });

        internal static bool heal_player(byte arg_0, int amount_healed, Player player)
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
                            gbl.game_state != GameState.Combat)
                        {
                            ovr013.CallAffectTable(Effect.Remove, null, player, Affects.affect_4e);
                        }
                    }

                    return true;
                }
            }

            return false;
        }


        internal static bool combat_heal(byte arg_0, Player player)
        {
            if (ovr033.sub_7515A(true, ovr033.PlayerMapPos(player), player) == true)
            {
                player.health_status = Status.okey;
                player.in_combat = true;
                player.hit_point_current = arg_0;

                if (gbl.game_state == GameState.Combat)
                {
                    ovr033.RedrawCombatIfFocusOn(false, 3, player);
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

                ovr025.CountCombatTeamMembers();

                return true;
            }

            return false;
        }
    }
}
