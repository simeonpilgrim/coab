using Classes;

namespace engine
{
    class ovr013
    {
        /// <summary>
        /// If same as current affect damage set to zero, or if affect is zero
        /// </summary>
        internal static void protection_type_check(Affects affect) /* sub_3A019 */
        {
            if (affect == 0 ||
                gbl.current_affect == affect)
            {
                gbl.damage = 0;
                gbl.current_affect = 0;
            }
        }


        internal static bool addAffect(ushort arg_0, int arg_2, Affects affect_type, Player player)
        {
            bool var_1;

            if (gbl.byte_1D2C6 == true)
            {
                var_1 = false;
            }
            else
            {
                var_1 = true;
                ovr024.add_affect(true, arg_2, arg_0, affect_type, player);
            }
            return var_1;
        }


        internal static void sub_3A071(Effect arg_0, object param, Player player)
        {
            ovr025.clear_actions(player);
        }


        internal static void sub_3A087(Effect arg_0, object param, Player player)
        {
            gbl.saving_throw_roll += 1;
            gbl.attack_roll++;
        }


        internal static void sub_3A096(Effect add_remove, object param, Player player)
        {
            gbl.byte_1D2CC += 5;
            gbl.attack_roll++;
        }


        internal static void sub_3A0A6(Effect arg_0, object param, Player player)
        {
            if (gbl.byte_1D2CC < 5)
            {
                gbl.byte_1D2CC = 0;
            }
            else
            {
                gbl.byte_1D2CC -= 5;
            }
            gbl.attack_roll--;
        }


        internal static void sub_3A0DC(Effect arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            byte var_1 = (byte)(player.field_19D + player.field_19C);

            if (affect.field_3 > var_1)
            {
                affect.field_3 -= var_1;
            }
            else
            {
                ovr024.remove_affect(null, Affects.sticks_to_snakes, player);
            }

            ovr025.sub_6818A("is fighting with snakes", true, player);
            ovr025.ClearPlayerTextArea();

            ovr025.clear_actions(player);
        }


        internal static void sub_3A15F(Effect arg_0, object param, Player player)
        {
            if ((gbl.player_ptr.field_14B & 1) != 0)
            {
                gbl.attack_roll -= 7;
            }
        }


        internal static void sub_3A17A(Effect arg_0, object param, Player player)
        {
            int bonus = 0;

            if (player.actions != null &&
                player.actions.target != null)
            {
                gbl.spell_target = player.actions.target;

                if (gbl.spell_target.field_11A == 10)
                {
                    bonus = 1;
                }
                else if (gbl.spell_target.field_11A == 9 || gbl.spell_target.field_11A == 12)
                {
                    bonus = 2;
                }
                else if (gbl.spell_target.field_11A == 4)
                {
                    bonus = 3;
                }
                else
                {
                    bonus = 0;
                }
            }
            gbl.attack_roll += bonus;
            gbl.damage += bonus;
            gbl.damage_flags = 9;
        }


        internal static void sub_3A1DF(Effect arg_0, object param, Player player)
        {
            if (player.ac < 0x3A)
            {
                player.ac += 2;
            }
            else
            {
                player.ac = 0x3C;
            }

            if (player.field_19B < 0x3A)
            {
                player.field_19B += 2;
            }
            else
            {
                player.field_19B = 0x3C;
            }
        }


        internal static void affect_protect_evil(Effect arg_0, object param, Player player) /* sub_3A224 */
        {
            if (gbl.player_ptr.alignment == 2 ||
                gbl.player_ptr.alignment == 5 ||
                gbl.player_ptr.alignment == 8)
            {
                gbl.saving_throw_roll += 2;
                gbl.attack_roll -= 2;
            }
        }


        internal static void affect_protect_good(Effect arg_0, object param, Player player) /* sub_3A259 */
        {
            if (gbl.player_ptr.alignment == 0 ||
                gbl.player_ptr.alignment == 3 ||
                gbl.player_ptr.alignment == 6)
            {
                gbl.saving_throw_roll += 2;
                gbl.attack_roll -= 2;
            }
        }


        internal static void affect_resist_cold(Effect arg_0, object param, Player player) /* sub_3A28E */
        {
            if ((gbl.damage_flags & 2) != 0)
            {
                gbl.damage /= 2;
                gbl.saving_throw_roll += 3;
            }
        }


        internal static void affect_charm_person(Effect arg_0, object param, Player player) /* sub_3A2AD */
        {
            Affect affect = (Affect)param;

            if (arg_0 == Effect.Remove)
            {
                player.combat_team = (CombatTeam)((affect.field_3 & 0x40) >> 6);

                if (player.field_F7 == 0xB3)
                {
                    player.field_F7 = 0;
                }
            }
            else
            {
                if ((affect.field_3 & 0x20) == 0)
                {
                    affect.field_3 += (byte)(0x20 + (((int)player.combat_team) << 6));

                    player.combat_team = (CombatTeam)(affect.field_3 >> 7);
                    player.quick_fight = QuickFight.True;

                    if (player.field_F7 <= 0x7F)
                    {
                        player.field_F7 = 0x0B3;
                    }

                    player.actions.target = null;
                    ovr025.count_teams();
                }
                gbl.byte_1D2CC = 100;
            }
        }


        internal static void Suffocates(Effect arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            if (affect.field_3 == 0)
            {
                ovr024.sub_63014("Suffocates", Status.dead, player);
            }
            else
            {
                affect.field_3--;
            }
        }


        internal static void sub_3A3BC(Effect arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            if (addAffect(10, affect.field_3, Affects.affect_0f, player) == true &&
                player.hit_point_current > 1)
            {
                gbl.damage_flags = 0;

                ovr024.damage_person(false, 0, 1, player);

                if (gbl.game_state != 5)
                {
                    ovr025.Player_Summary(gbl.player_ptr);
                }
            }
        }


        internal static void affect_shield(Effect arg_0, object param, Player player) /* sub_3A41F */
        {
            if (player.ac < 0x39) // AC 3
            {
                player.ac = 0x39; // AC 3
            }

            gbl.saving_throw_roll += 1;

            if (gbl.spell_id == 15) /* Magic Missle */
            {
                gbl.damage = 0;
            }
        }


        internal static void sub_3A44A(Effect arg_0, object param, Player player)
        {
            if (player.actions != null &&
                player.actions.target != null &&
                (player.actions.target.field_14B & 2) != 0)
            {
                gbl.spell_target = player.actions.target;
                gbl.attack_roll++;
            }
        }


        internal static void affect_resist_fire(Effect add_remove, object param, Player player) /* sub_3A480 */
        {
            if (add_remove == Effect.Add &&
                (gbl.damage_flags & 1) != 0)
            {
                gbl.damage /= 2;
                gbl.saving_throw_roll += 3;
            }
        }


        internal static void is_silenced1(Effect arg_0, object param, Player player)
        {
            if (player.actions.field_2 != 0)
            {
                ovr025.DisplayPlayerStatusString(true, 10, "is silenced", player);
            }

            player.actions.field_2 = 0;
            player.actions.can_cast = false;
        }


        internal static void sub_3A517(Effect arg_0, object param, Player player)
        {
            if (ovr025.find_affect(Affects.poisoned, player) == true)
            {
                ovr024.sub_63014("dies from poison", Status.dead, player);
            }

            gbl.byte_1D2C6 = true;

            ovr024.remove_affect(null, Affects.affect_0f, player);

            gbl.byte_1D2C6 = false;
        }


        internal static void affect_spiritual_hammer(Effect add_remove, object param, Player player) /* sub_3A583 */
        {
            Item item = player.itemsPtr;
            bool item_found = false;

            while (item != null && item_found == false)
            {
                if (item.type == 0x14 &&
                    item.field_31 == 0xf3)
                {
                    item_found = true;
                }
                else
                {
                    item = item.next;
                }
            }

            if (add_remove == Effect.Remove && item != null)
            {
                ovr025.lose_item(item, player);
            }

            if (add_remove == Effect.Add &&
                item_found == false &&
                player.field_14C < 0x10)
            {
                item = new Item();
                item.type = 20;
                item.field_30 = 20;
                item.field_31 = 243;
                item.plus = 1;
                item.affect_2 = Affects.spiritual_hammer;
                item.affect_3 = (Affects)160;

                ovr025.addItem(item, player);
                ovr020.ready_Item(item);

                seg051.FreeMem(Item.StructSize, item);

                ovr025.DisplayPlayerStatusString(true, 10, "Gains an item", player);
            }

            ovr025.reclac_player_values(player);
        }


        internal static void sub_3A6C6(Effect arg_0, object param, Player player)
        {
            if (player.name.Length == 0 &&
                ovr025.find_affect(Affects.detect_invisibility, gbl.player_ptr) == false)
            {
                gbl.byte_1D2C5 = 1;
                gbl.attack_roll -= 4;
            }
        }


        internal static void sub_3A6FB(Effect arg_0, object param, Player player)
        {
            gbl.spell_target = player.actions.target;

            if ((gbl.spell_target.field_14B & 4) != 0)
            {
                gbl.attack_roll++;
            }
        }


        internal static void sub_3A73F(Effect arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            if (ovr024.roll_dice((affect.field_3 >> 4) + 1, 1) > 1 &&
                gbl.spell_id > 0 &&
                gbl.byte_1D2C7 == false)
            {
                protection_type_check(0);

                ovr025.DisplayPlayerStatusString(true, 10, "lost an image", player);

                affect.field_3 -= 1;

                if (affect.field_3 == 0)
                {
                    ovr024.remove_affect(null, Affects.mirror_image, player);
                }
            }
        }


        internal static void three_quarters_damage(Effect arg_0, object param, Player player)
        {
            gbl.damage -= gbl.damage / 4;
        }


        internal static void sub_3A7E8(Effect arg_0, object param, Player player)
        {
            if (player.actions.field_2 != 0)
            {
                ovr025.DisplayPlayerStatusString(true, 10, "is coughing", player);
            }

            player.actions.field_2 = 0;
            player.actions.can_cast = false;

            ovr025.reclac_player_values(player);
            
            if( player.field_19B > 0x34 )
            {
                player.field_19B -= 2;
            }
            else
            {
                player.field_19B = 0x32;
            }

            player.ac = player.field_19B;

            if (player == gbl.player_ptr)
            {
                ovr025.display_hitpoint_ac(player);
            }
        }


        internal static void sub_3A89E(Effect arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            affect.call_spell_jump_list = false;

            if (gbl.byte_1D2C6 == false)
            {
                ovr024.sub_63014("collapses", Status.dead, player);
            }

            player.combat_team = (CombatTeam)(affect.field_3 >> 4);
            player.quick_fight = QuickFight.True;
            player.field_E9 = 0;

            player.field_DD = (byte)(player.fighter_lvl + (player.field_113 * ovr026.sub_6B3D1(player)));
            player.field_E4 = 0x0C;

            if (player.field_F7 == 0xB3)
            {
                player.field_F7 = 0;
            }

            player.field_11A = 0;
        }


        internal static void sub_3A951(Effect arg_0, object param, Player player)
        {
            gbl.attack_roll -= 4;

            player.ac -= 4;
            player.field_19B -= 4;

            gbl.saving_throw_roll -= 4;
        }


        internal static void sub_3A974(Effect add_remove, object param, Player player)
        {
            ovr024.CallSpellJumpTable(add_remove, param, player, Affects.affect_2b);
            ovr024.CallSpellJumpTable(add_remove, param, player, Affects.cause_disease_2);
        }


        internal static void sub_3A9D9(Effect arg_0, object arg_2, Player player)
        {
            byte var_1 = ovr024.roll_dice(100, 1);

            if (var_1 >= 1 && var_1 <= 10)
            {
                ovr024.remove_affect(null, Affects.confuse, player);
                player.actions.field_10 = 1;
                player.quick_fight = QuickFight.True;

                if (player.field_F7 <= 0x7f)
                {
                    player.field_F7 = 0xb3;
                }

                player.actions.target = null;

                ovr024.is_unaffected("runs away", false, 1, true, 0, 10, Affects.fear, player);
            }
            else if (var_1 >= 11 && var_1 <= 60)
            {
                ovr025.sub_6818A("is confused", true, player);
                ovr025.ClearPlayerTextArea();
                sub_3A071(0, arg_2, player);
            }
            else if (var_1 >= 61 && var_1 <= 80)
            {
                ovr024.is_unaffected("goes berserk", false, 1, true, (byte)player.combat_team, 1, Affects.affect_89, player);
                ovr024.CallSpellJumpTable(Effect.Add, null, player, Affects.affect_89);
            }
            else if (var_1 >= 81 && var_1 <= 100)
            {
                ovr025.sub_6818A("is enraged", true, player);
                ovr025.ClearPlayerTextArea();
            }

            if (ovr024.do_saving_throw(-2, 4, player) == true)
            {
                ovr024.remove_affect(null, Affects.confuse, player);
            }
        }


        internal static void affect_curse(Effect arg_0, object param, Player player) /* sub_3AB6F */
        {
            gbl.attack_roll -= 4;
            gbl.saving_throw_roll -= 4;
        }


        internal static void has_action_timedout(Effect arg_0, object param, Player player)
        {
            if (player.actions.delay == 0)
            {
                gbl.byte_1D2C5 = 1;
                gbl.attack_roll = -1;
            }
        }


        internal static void spl_age(Effect arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            if ((affect.field_3 & 0x10) == 0)
            {
                affect.field_3 += 0x10;

                ovr025.DisplayPlayerStatusString(true, 10, "ages", player);
                player.age++;
            }

            gbl.byte_1D2C0 *= 2;
        }


        internal static void sub_3AC1D(Effect arg_0, object param, Player player)
        {
            Struct_1D885 var_8;
            Struct_1D885 var_4;

            Affect affect = (Affect)param;

            byte var_A = (byte)(affect.field_3 >> 4);

            bool var_9 = false;
            var_8 = gbl.stru_1D885;

            while (var_8 != null && var_9 == false)
            {
                if (var_8.player == player &&
                    var_8.field_1C == var_A)
                {
                    var_9 = true;
                }
                else
                {
                    var_8 = var_8.next;
                }
            }

            if (var_9 == true)
            {
                ovr025.string_print01("The air clears a little...");

                for (int var_B = 1; var_B <= 4; var_B++)
                {
                    if (var_8.field_10[var_B] != 0)
                    {
                        int tmp_x = var_8.target_x + gbl.MapDirectionXDelta[gbl.unk_18AE9[var_B]];
                        int tmp_y = var_8.target_y + gbl.MapDirectionYDelta[gbl.unk_18AE9[var_B]];

                        var_9 = false;

                        for (int i = 1; i <= gbl.byte_1D1BB; i++)
                        {
                            if (gbl.unk_1D183[i].target != null &&
                                gbl.unk_1D183[i].mapX == tmp_x &&
                                gbl.unk_1D183[i].mapY == tmp_y)
                            {
                                var_9 = true;
                            }
                        }

                        if (var_9 == true)
                        {
                            gbl.mapToBackGroundTile[tmp_x, tmp_y] = 0x1F;
                        }
                        else
                        {
                            gbl.mapToBackGroundTile[tmp_x, tmp_y] = var_8.field_7[var_B];
                        }
                    }
                }

                var_4 = gbl.stru_1D885;

                if (var_4 == var_8)
                {
                    gbl.stru_1D885 = var_4.next;
                }
                else
                {
                    while (var_4.next != var_8)
                    {
                        var_4 = var_4.next;
                    }
                }

                var_4.next = var_8.next;

                seg051.FreeMem(0x1E, var_8);
                var_4 = gbl.stru_1D885;

                while (var_4 != null)
                {
                    for (int var_B = 1; var_B <= 4; var_B++)
                    {
                        if (var_4.field_10[var_B] != 0)
                        {
                            int tmp_x = gbl.MapDirectionXDelta[gbl.unk_18AE9[var_B]] + var_4.target_x;
                            int tmp_y = gbl.MapDirectionYDelta[gbl.unk_18AE9[var_B]] + var_4.target_y;

                            gbl.mapToBackGroundTile[tmp_x, tmp_y] = 0x1E;
                        }
                    }

                    var_4 = var_4.next;
                }
            }
        }


        internal static void sub_3AF06(byte arg_0, Player player)
        {
            if (gbl.player_ptr.field_151 != null &&
                ovr025.getTargetRange(player, gbl.player_ptr) == 0 &&
                ovr024.roll_dice(100, 1) <= arg_0)
            {
                ovr025.DisplayPlayerStatusString(true, 10, "Avoids it", player);
                gbl.damage = 0;
                gbl.attack_roll = -1;
                gbl.byte_1D2CA--;
            }
        }


        internal static Item get_primary_weapon(Player player) /* sub_3AF77 */
        {
            Item item = null;

            if (player.field_151 != null)
            {
                bool item_found = ovr025.sub_6906C(out item, player);

                if (item_found == false ||
                    item == null)
                {
                    item = player.field_151;
                }
            }

            return item;
        }


        internal static void sub_3AFE0(Effect arg_0, object param, Player player)
        {
            Item item = get_primary_weapon(gbl.player_ptr);

            if (item != null && item.plus == 0)
            {
                sub_3AF06(100, player);
            }
        }


        internal static void sub_3B01B(Effect arg_0, object param, Player player)
        {
            gbl.byte_1D2C0 /= 2;
        }


        internal static void weaken(Effect arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            if (addAffect(0x3c, affect.field_3, Affects.affect_2b, player) == true)
            {
                if (player.strength > 3)
                {
                    ovr025.DisplayPlayerStatusString(true, 10, "is weakened", player);
                    player.strength--;
                }
                else if (ovr025.find_affect(Affects.helpless, player) == true)
                {
                    ovr024.add_affect(false, 0xff, 0, Affects.helpless, player);
                }
            }
        }


        internal static void sub_3B0C2(Effect arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            if (addAffect(10, affect.field_3, Affects.cause_disease_2, player) == true)
            {
                if (player.hit_point_current > 1)
                {
                    gbl.damage_flags = 0;

                    ovr024.damage_person(false, 0, 1, player);

                    if (gbl.game_state != 5)
                    {
                        ovr025.Player_Summary(gbl.player_ptr);
                    }
                }
                else if (ovr025.find_affect(Affects.helpless, player) == false)
                {
                    ovr024.add_affect(false, 0xff, 0, Affects.helpless, player);
                }
            }
        }


        internal static void sub_3B153(Effect arg_0, object param, Player player)
        {
            gbl.spell_target = player.actions.target;

            if (gbl.player_ptr.field_11A == 2 ||
                gbl.player_ptr.field_11A == 10)
            {
                if ((gbl.player_ptr.field_DE & 0x7F) == 2)
                {
                    gbl.attack_roll -= 4;
                }
            }
        }


        internal static void sub_3B1A2(Effect arg_0, object param, Player player)
        {
            if (gbl.player_ptr.field_11A == 1 &&
                (gbl.player_ptr.field_DE & 0x7F) == 2)
            {
                gbl.attack_roll -= 4;
            }
        }


        internal static void sub_3B1C9(Effect arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            CombatTeam team = (CombatTeam)((affect.field_3 & 0x10) >> 4);

            if (player.combat_team == team)
            {
                sub_3A087(arg_0, affect, player);
            }
            else
            {
                gbl.attack_roll -= 1;
                gbl.saving_throw_roll -= 1;
            }
        }


        internal static void sub_3B212(Effect arg_0, object param, Player player)
        {
            if ((gbl.damage_flags & 2) != 0)
            {
                gbl.saving_throw_roll += 2;
            }
            else if ((gbl.damage_flags & 1) != 0 && gbl.save_made == false)
            {
                gbl.damage *= 2;
            }
        }


        internal static void sub_3B243(Effect arg_0, object param, Player player)
        {
            if ((gbl.damage_flags & 1) != 0)
            {
                gbl.saving_throw_roll += 2;
            }
            else if ((gbl.damage_flags & 2) != 0 && gbl.byte_1C01B == 0)
            {
                gbl.damage *= 2;
            }
        }


        internal static void sub_3B27B(Effect arg_0, object param, Player player)
        {
            ovr024.add_affect(false, 12, 1, Affects.invisibility, player);
        }


        internal static void sub_3B29A(Effect arg_0, object param, Player player)
        {
            player.actions.move = 0;

            if (gbl.reset_byte_1D2C0 == true)
            {
                gbl.byte_1D2C0 = 0;
            }
        }


        internal static void sub_3B2BA(Effect arg_0, object param, Player player)
        {
            ovr024.add_affect(false, 0xff, 0, Affects.affect_62, player);
        }


        internal static void sub_3B2D8(Effect arg_0, object param, Player player)
        {
            Item weapon = get_primary_weapon(gbl.player_ptr);

            if (weapon == null ||
                weapon.plus == 0)
            {
                gbl.damage = 0;
            }
            else if (weapon != null &&
                weapon.plus < 3)
            {
                gbl.damage /= 2;
            }
        }


        internal static void sub_3B32B(Effect arg_0, object param, Player player)
        {
            if ((gbl.damage_flags & 1) != 0)
            {
                for (int i = 1; i <= gbl.dice_count; i++)
                {
                    gbl.damage -= 2;

                    if (gbl.damage < gbl.dice_count)
                    {
                        gbl.damage = gbl.dice_count;
                    }
                }

                gbl.saving_throw_roll += 4;

                if ((gbl.damage_flags & 8) == 0)
                {
                    protection_type_check(0);
                }
            }
        }


        internal static void heal_1hp_per_turn(Effect arg_0, object param, Player player) /* sub_3B386 */
        {
            Affect affect = (Affect)param;

            if (addAffect(0x3C, affect.field_3, Affects.affect_3e, player) == true &&
                ovr024.heal_player(1, 1, player) == true)
            {
                ovr025.describeHealing(player);
            }
        }


        internal static void blocks_spells_below_lvl4(Effect arg_0, object param, Player player) /* sub_3B3CA */
        {
            if (gbl.spell_id > 0 &&
                gbl.spell_table[gbl.spell_id].spellLevel < 4)
            {
                protection_type_check(0);
            }
        }


        internal static void sub_3B407(int save_bonus, Player player)
        {
            gbl.spell_target = player.actions.target;

            if (ovr024.do_saving_throw(save_bonus, 0, gbl.spell_target) == false)
            {
                ovr025.DisplayPlayerStatusString(false, 10, "is Poisoned", gbl.spell_target);
                seg041.GameDelay();
                ovr024.add_affect(false, 0xff, 0, Affects.poisoned, gbl.spell_target);

                ovr024.sub_63014("is killed", Status.dead, gbl.spell_target);
            }
        }


        internal static void sub_3B4AE(byte arg_0, ushort affect, Player player)
        {
            gbl.spell_target = player.actions.target;

            if (ovr024.do_saving_throw(0, 0, gbl.spell_target) == false)
            {
                ovr025.sub_6818A("is Paralyzed", true, gbl.spell_target);
                ovr024.add_affect(false, 12, affect, Affects.paralyze, gbl.spell_target);
            }
        }


        internal static void sub_3B520(Effect arg_0, object param, Player player)
        {
            sub_3B407(0, player);
        }


        internal static void sub_3B534(Effect arg_0, object param, Player player)
        {
            sub_3B407(4, player);
        }


        internal static void sub_3B548(Effect arg_0, object param, Player player)
        {
            sub_3B407(2, player);
        }


        internal static void sub_3B55C(Effect arg_0, object param, Player player)
        {
            sub_3B4AE(0, ovr024.roll_dice(8, 2), player);
        }


        internal static void spell_stupid(Effect arg_0, object param, Player player)
        {
            player._int = 7;
            player.wis = 7;

            ovr025.DisplayPlayerStatusString(true, 10, "is stupid", player);

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
        }


        internal static void sub_3B636(Effect arg_0, object param, Player player)
        {
            if (gbl.player_ptr.field_11A == 0x13)
            {
                if (ovr025.find_affect(Affects.detect_invisibility, gbl.player_ptr) == false)
                {
                    gbl.byte_1D2C5 = 1;
                }

                gbl.attack_roll -= 4;
            }
        }


        internal static void sub_3B671(Effect arg_0, object param, Player player)
        {
            sub_3B407(-2, player);
        }


        internal static void sub_3B685(Effect arg_0, object param, Player player)
        {
            gbl.byte_1D2C5 = 1;
            gbl.attack_roll -= 4;
        }


        internal static void sub_3B696(Effect arg_0, object param, Player player)
        {
            if (ovr024.roll_dice(100, 1) <= 95)
            {
                ovr024.add_affect(false, 12, 1, Affects.invisibility, player);
            }
        }


        internal static void sub_3B6D2(Effect arg_0, object param, Player player)
        {
            if ((gbl.damage_flags & 0x20) > 0)
            {
                protection_type_check(0);
                ovr025.DisplayPlayerStatusString(true, 10, "is unaffected", player);
            }
        }


        internal static void sub_3B71A(Effect arg_0, object param, Player player)
        {
            if (player.actions != null &&
                player.actions.target != null)
            {
                gbl.spell_target = player.actions.target;

                if (gbl.spell_target.field_11A == 3)
                {
                    gbl.damage = (ovr024.roll_dice(12, 1) * 3) + 4 + ovr025.strengthDamBonus(player);
                    gbl.attack_roll += 2;
                }
            }
        }


        internal static void sub_3B772(Effect arg_0, object param, Player player)
        {
            if (player.actions != null)
            {
                gbl.spell_target = player.actions.target;

                if (gbl.spell_target != null &&
                    gbl.spell_target.field_11A == 8)
                {
                    gbl.attack_roll += 3;
                    gbl.damage += 3;
                }
            }
        }


        internal static void spl_berzerk(Effect arg_0, object param, Player player)
        {
            if (arg_0 == Effect.Add)
            {
                player.quick_fight = QuickFight.True;

                if (player.field_F7 <= 0x7F ||
                    player.field_F7 == 0xb3)
                {
                    player.field_F7 = 0x0B3;
                }
                else
                {
                    player.field_F7 = 0x0B2;
                }

                if (gbl.game_state == 5)
                {
                    player.actions.target = null;

                    ovr032.Rebuild_SortedCombatantList(gbl.mapToBackGroundTile, ovr033.PlayerMapSize(player), 0xff, 0xff,
                        ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player));

                    player.actions.target = gbl.player_array[gbl.SortedCombatantList[1].player_index];

                    player.actions.can_cast = false;
                    player.combat_team = ovr025.opposite_team(player.actions.target);

                    ovr025.DisplayPlayerStatusString(true, 10, "goes berzerk", player);
                }
            }
            else
            {
                if (player.field_F7 == 0xb3)
                {
                    player.field_F7 = 0;
                }

                player.combat_team = CombatTeam.Ours;
            }
        }


        internal static void sub_3B8D9(Effect arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            if (ovr024.combat_heal(player.hit_point_current, player) == false)
            {
                addAffect(1, affect.field_3, Affects.affect_4e, player);
            }
        }


        internal static void sub_3B919(Effect arg_0, object param, Player player)
        {
            gbl.damage_flags = 9;

            ovr024.damage_person(false, 0, ovr024.roll_dice_save(10, 2), player.actions.target);
        }


        internal static void sub_3B94C(Effect arg_0, object param, Player player)
        {
            gbl.damage_flags = 0x10;

            ovr024.damage_person(false, 0, ovr024.roll_dice_save(4, 1), player.actions.target);
        }


        internal static void half_damage(Effect arg_0, object param, Player player) /* sub_3B97F */
        {
            gbl.damage /= 2;
        }


        internal static void sub_3B990(Effect arg_0, object param, Player player)
        {
            if ((gbl.damage_flags & 0x01) != 0 ||
                (gbl.damage_flags & 0x02) != 0)
            {
                if (ovr024.do_saving_throw(0, 4, player) == true &&
                    gbl.spell_table[gbl.spell_id].can_save_flag != 0)
                {
                    gbl.damage = 0;
                }
                else
                {
                    gbl.damage /= 2;
                }
            }
        }


        internal static void sub_3B9E1(Effect arg_0, object param, Player player)
        {
            byte var_1;

            if ((gbl.damage_flags & 0x04) != 0)
            {
                protection_type_check(0);
                var_1 = ovr024.roll_dice(8, 1);

                player.ac += 8;
            }
        }


        internal static void sub_3BA14(Effect arg_0, object param, Player player)
        {
            Item var_4;

            var_4 = get_primary_weapon(gbl.player_ptr);

            if (var_4 != null &&
                gbl.unk_1C020[var_4.type].field_7 == 1)
            {
                gbl.damage = 1;
            }
        }


        internal static void spell_displace(Effect arg_0, object param, Player player) /*sub_3BA55*/
        {
            Affect affect = (Affect)param;

            if (affect != null)
            {
                if (gbl.byte_1D8B7 == 0 && gbl.attack_roll == 0)
                {
                    affect.field_3 &= 0x0f;
                }
                else if ((affect.field_3 & 0x10) == 0)
                {
                    gbl.attack_roll = -1;
                    affect.field_3 |= 0x10;
                }
            }
            //else
            //{
            //    int i = 0;
            //}
        }


        internal static void sub_3BAB9(Effect arg_0, object param, Player player)
        {
            byte var_B;
            Struct_1D885 var_8;
            Struct_1D885 var_4;

            Affect affect = (Affect)param;

            byte var_A = (byte)(affect.field_3 >> 4);

            bool var_9 = false;
            var_8 = gbl.stru_1D889;

            while (var_8 != null && var_9 == false)
            {
                if (var_8.player == player &&
                    var_8.field_1C == var_A)
                {
                    var_9 = true;
                }
                else
                {
                    var_8 = var_8.next;
                }
            }

            if (var_9 == true)
            {
                ovr025.string_print01("The air clears a little...");

                for (var_B = 1; var_B <= 9; var_B++)
                {
                    if (var_8.field_10[var_B] != 0)
                    {
                        int tmp_x = var_8.target_x + gbl.MapDirectionXDelta[gbl.unk_18AED[var_B]];
                        int tmp_y = var_8.target_y + gbl.MapDirectionYDelta[gbl.unk_18AED[var_B]];

                        bool var_E = false;

                        for (int i = 1; i <= gbl.byte_1D1BB; i++)
                        {
                            if (gbl.unk_1D183[i].target != null &&
                                gbl.unk_1D183[i].mapX == tmp_x &&
                                gbl.unk_1D183[i].mapY == tmp_y)
                            {
                                var_E = true;
                            }
                        }

                        if (var_E == true)
                        {
                            gbl.mapToBackGroundTile[tmp_x, tmp_y] = 0x1F;
                        }
                        else
                        {
                            gbl.mapToBackGroundTile[tmp_x, tmp_y] = var_8.field_7[var_B];
                        }
                    }
                }

                var_4 = gbl.stru_1D889;

                if (var_4 == var_8)
                {
                    gbl.stru_1D889 = var_4.next;
                }
                else
                {
                    while (var_4.next != var_8)
                    {
                        var_4 = var_4.next;
                    }
                }

                var_4.next = var_8.next;
                seg051.FreeMem(0x1E, var_8);
                var_4 = gbl.stru_1D889;

                while (var_4 != null)
                {
                    for (var_B = 1; var_B <= 9; var_B++)
                    {
                        if (var_4.field_10[var_B] != 0)
                        {
                            int tmp_x = gbl.MapDirectionXDelta[gbl.unk_18AED[var_B]] + var_4.target_x;
                            int tmp_y = gbl.MapDirectionYDelta[gbl.unk_18AED[var_B]] + var_4.target_y;

                            gbl.mapToBackGroundTile[tmp_x, tmp_y] = 0x1C;
                        }
                    }

                    var_4 = var_4.next;
                }
            }
        }


        internal static void sub_3BD98(Effect arg_0, object param, Player arg_6)
        {
            if ((gbl.damage_flags & 0x01) != 0)
            {
                gbl.damage /= 2;
            }
        }


        internal static void sub_3BDB2(Effect arg_0, object param, Player arg_6)
        {
            Item item = get_primary_weapon(gbl.player_ptr);

            if (item != null &&
                (gbl.unk_1C020[item.type].field_7 & 0x81) != 0)
            {
                gbl.damage /= 2;
            }
        }


        internal static void sub_3BE06(Effect arg_0, object param, Player arg_6)
        {
            Affect affect = (Affect)param;
            affect.call_spell_jump_list = false;

            if (arg_6.in_combat == true)
            {

                ovr024.sub_63014("Falls dead", Status.dead, arg_6);
            }
        }


        internal static void con_saving_bonus(Effect arg_0, object param, Player player) /* sub_3BE42 */
        {
            if (gbl.byte_1D2D1 == 4 ||
                gbl.byte_1D2D1 == 2)
            {
                int save_bonus = 0;

                if (player.con >= 4 && player.con <= 6)
                {
                    save_bonus = 1;
                }
                else if (player.con >= 7 && player.con <= 10)
                {
                    save_bonus = 2;
                }
                else if (player.con >= 11 && player.con <= 13)
                {
                    save_bonus = 3;
                }
                else if (player.con >= 14 && player.con <= 17)
                {
                    save_bonus = 4;
                }
                else if (player.con >= 18 && player.con <= 20)
                {
                    save_bonus = 5;
                }

                gbl.saving_throw_roll += save_bonus;
            }
        }


        internal static void sub_3BEB8(Effect arg_0, object param, Player arg_6)
        {
            arg_6.hit_point_current += 3;

            if (arg_6.hit_point_current > arg_6.hit_point_max)
            {
                arg_6.hit_point_current = arg_6.hit_point_max;
            }
        }


        internal static void sub_3BEE8(Effect arg_0, object param, Player player_ptr)
        {
            Affect arg_2 = (Affect)param;

            byte heal_amount;

            heal_amount = 0;

            if (player_ptr.health_status == Status.dying &&
                player_ptr.actions.bleeding < 6)
            {
                heal_amount = (byte)(6 - player_ptr.actions.bleeding);
            }

            if (player_ptr.health_status == Status.unconscious)
            {
                heal_amount = 6;
            }

            if (heal_amount > 0 &&
                ovr024.combat_heal(heal_amount, player_ptr) == true)
            {
                ovr024.add_affect(true, 0xff, (ushort)(ovr024.roll_dice(4, 1) + 1), Affects.affect_5F, player_ptr);
                arg_2.call_spell_jump_list = false;
                ovr024.remove_affect(arg_2, Affects.affect_63, player_ptr);
            }
        }


        internal static void sub_3BF91(Effect arg_0, object param, Player player)
        {
            if ((gbl.damage_flags & 1) == 0 &&
                (gbl.damage_flags & 0x10) == 0)
            {
                ovr024.add_affect(true, 0xff, ovr024.roll_dice(6, 3), Affects.affect_66, player);
            }
        }


        internal static void sp_regenerate(Effect arg_0, object param, Player player)
        {
            if (ovr025.find_affect(Affects.affect_62, player) == false &&
                ovr025.find_affect(Affects.regenerate, player) == false)
            {
                ovr024.add_affect(true, 0xff, 3, Affects.regenerate, player);
            }
        }


        internal static void sub_3C01E(Effect arg_0, object param, Player player)
        {
            Affect arg_2 = (Affect)param;

            if (ovr024.combat_heal(player.hit_point_max, player) == false)
            {
                addAffect(1, arg_2.field_3, Affects.affect_66, player);
            }
        }


        internal static void sub_3C05D(Effect arg_0, object param, Player player)
        {
            gbl.spell_target = player.actions.target;

            if (ovr025.find_affect(Affects.resist_fire, gbl.spell_target) == false &&
                ovr025.find_affect(Affects.cold_fire_shield, gbl.spell_target) == false &&
                ovr025.find_affect(Affects.fire_resist, gbl.spell_target) == false)
            {
                gbl.damage += ovr024.roll_dice(6, 1);
            }
        }


        internal static void sub_3C0DA(Effect arg_0, object param, Player player)
        {
            sub_3AF06(0x3c, player);
        }


        internal static void sub_3C0EE(byte arg_0)
        {
            int target_count = ovr025.spell_target_count(gbl.spell_id);
            int var_2 = (byte)(arg_0 + ((0x0b - target_count) * 5));

            if (gbl.current_affect != 0 ||
                (gbl.damage_flags & 8) != 0)
            {
                if (ovr024.roll_dice(100, 1) <= var_2)
                {
                    protection_type_check(0);
                }
            }
        }


        internal static void sub_3C14F(Effect arg_0, object param, Player arg_6)
        {
            sub_3C0EE(50);
        }


        internal static void sub_3C15D(Effect arg_0, object param, Player arg_6)
        {
            sub_3C0EE(15);
        }


        internal static void sub_3C16B(Effect arg_0, object param, Player arg_6)
        {
            if (ovr024.roll_dice(100, 1) <= 0x5a)
            {
                protection_type_check(Affects.sleep);
                protection_type_check(Affects.charm_person);
            }
        }


        internal static void sub_3C18F(Effect arg_0, object param, Player arg_6)
        {
            protection_type_check(Affects.charm_person);
            protection_type_check(Affects.sleep);
        }


        internal static void sub_3C1A4(Effect arg_0, object param, Player arg_6)
        {
            protection_type_check(Affects.paralyze);
        }


        internal static void sub_3C1B2(Effect arg_0, object param, Player arg_6)
        {
            if ((gbl.damage_flags & 2) != 0)
            {
                protection_type_check(0);
            }
        }


        internal static void sub_3C1C9(Effect arg_0, object param, Player arg_6)
        {
            protection_type_check(Affects.poisoned);
            protection_type_check(Affects.paralyze);

            if (gbl.byte_1D2D1 == 0)
            {
                gbl.saving_throw_roll = 100;
            }
        }


        internal static void sub_3C1EA(Effect arg_0, object param, Player arg_6)
        {
            if ((gbl.damage_flags & 1) != 0)
            {
                protection_type_check(0);
            }
        }


        internal static void sub_3C201(Effect arg_0, object param, Player arg_6)
        {
            if ((gbl.damage_flags & 1) != 0)
            {
                for (int i = 1; i <= gbl.dice_count; i++)
                {
                    gbl.damage--;

                    if (gbl.damage < gbl.dice_count)
                    {
                        gbl.damage = gbl.dice_count;
                    }
                }
            }
        }


        internal static void sub_3C246(Effect arg_0, object param, Player player)
        {
            if ((gbl.damage_flags & 4) != 0)
            {
                gbl.damage_flags >>= 1;
            }
        }


        internal static void sub_3C260(Effect arg_0, object param, Player player)
        {
            Item weapon = get_primary_weapon(gbl.player_ptr);

            if (weapon != null)
            {
                if (gbl.unk_1C020[weapon.type].field_7 == 0 ||
                    (gbl.unk_1C020[weapon.type].field_7 & 1) != 0)
                {
                    gbl.damage /= 2;
                }
            }
        }


        internal static void half_damage_if_weap_magic(Effect arg_0, object param, Player player) /* sub_3C2BF */
        {
            Item weapon = get_primary_weapon(gbl.player_ptr);

            if (weapon != null &&
                weapon.plus > 0)
            {
                gbl.damage /= 2;
            }
        }


        internal static void sub_3C2F9(Effect arg_0, object param, Player player)
        {
            Item item = gbl.player_ptr.field_151;

            if (item != null &&
                item.type == 0x55)
            {
                gbl.damage = ovr024.roll_dice_save(6, 1) + 1;
            }
        }


        internal static void sub_3C33C(Effect arg_0, object param, Player player)
        {
            if ((gbl.damage_flags & 2) != 0)
            {
                gbl.damage /= 2;
            }
        }


        internal static void sub_3C356(Effect arg_0, object param, Player player)
        {
            Item var_4;

            var_4 = get_primary_weapon(gbl.player_ptr);

            if (var_4 == null ||
                var_4.plus == 0)
            {
                if (gbl.player_ptr.race > 0 ||
                    gbl.player_ptr.field_E5 < 4)
                {
                    gbl.damage = 0;
                }
            }
        }


        internal static void sub_3C3A2(Effect arg_0, object param, Player player)
        {
            Item field_151;

            field_151 = player.field_151;

            if (field_151 != null)
            {
                if (field_151.type == 0x57 ||
                    field_151.type == 0x58)
                {
                    sub_3AF06(0x32, player);
                }
            }
        }


        internal static void sub_3C3F6(Effect arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            gbl.spell_target = player.actions.target;

            if (ovr024.roll_dice(100, 1) <= 25)
            {
                if (ovr025.getTargetRange(gbl.spell_target, player) < 4)
                {
                    ovr025.clear_actions(player);

                    ovr025.DisplayPlayerStatusString(true, 10, "Spits Acid", player);

                    ovr025.load_missile_icons(0x17);

                    ovr025.draw_missile_attack(0x1e, 1,
                        ovr033.PlayerMapYPos(gbl.spell_target), ovr033.PlayerMapXPos(gbl.spell_target),
                        ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player));

                    int damage = ovr024.roll_dice_save(4, 8);
                    bool saved = ovr024.do_saving_throw(0, 3, gbl.spell_target);

                    ovr024.damage_person(saved, 2, damage, gbl.spell_target);

                    ovr024.remove_affect(affect, Affects.affect_79, player);
                    ovr024.remove_affect(null, Affects.affect_50, player);
                }
            }
        }


        internal static void spl_paralyze(Effect arg_0, object param, Player player)
        {
            gbl.spell_target = player.actions.target;

            if (ovr024.do_saving_throw(0, 0, gbl.spell_target) == false)
            {
                ovr024.add_affect(false, 0xff, 0, Affects.paralyze, gbl.spell_target);

                ovr025.DisplayPlayerStatusString(true, 10, "is paralyzed", gbl.spell_target);
            }
        }


        internal static void sub_3C59D(Effect arg_0, object param, Player player)
        {
            gbl.damage_flags = 0x0A;

            ovr024.damage_person(false, 0, ovr024.roll_dice_save(8, 2), player.actions.target);
        }


        internal static void sub_3C5D0(Effect arg_0, object param, Player player)
        {
            if (ovr024.roll_dice(100, 1) <= 30)
            {
                protection_type_check(Affects.charm_person);
                protection_type_check(Affects.sleep);
            }
        }


        internal static void sub_3C5F4(Effect arg_0, object param, Player player)
        {
            protection_type_check(Affects.charm_person);
            protection_type_check(Affects.sleep);
            protection_type_check(Affects.paralyze);
            protection_type_check(Affects.poisoned);

            if (gbl.byte_1D2D1 != 0)
            {
                gbl.saving_throw_roll = 100;
            }
        }


        internal static void sub_3C623(Effect arg_0, object param, Player player)
        {
            if (gbl.current_affect != 0 ||
                (gbl.damage_flags & 8) != 0)
            {
                protection_type_check(0);
            }
        }


        internal static void sub_3C643(Effect arg_0, object arg_2, Player player)
        {
            Item item;

            if (ovr025.sub_6906C(out item, gbl.player_ptr) == true &&
                item != null &&
                item.type == 0x1c &&
                item.field_31 == 0x87)
            {
                player.health_status = Status.gone;
                player.in_combat = false;
                player.hit_point_current = 0;
                ovr024.sub_645AB(player);
                ovr024.work_on_00(player, 13);

                if (player.in_combat == true)
                {
                    ovr033.sub_74E6F(player);
                }
            }
        }


        internal static void do_items_affect(Effect remove_affect, object param, Player player) /* sub_3C6D3 */
        {
            Item item = (Item)param;

            gbl.byte_1D8AC = false;

            if (remove_affect == Effect.Remove)
            {
                ovr024.remove_affect(null, item.affect_2, player);
            }
            else
            {
                ovr024.add_affect(true, 0xff, 0, item.affect_2, player);

                if (gbl.game_state != 5)
                {
                    ovr024.CallSpellJumpTable(Effect.Add, null, player, item.affect_2);
                }
            }
        }


        internal static void sub_3C750(Effect arg_0, object param, Player player)
        {
            protection_type_check(Affects.fear);
            protection_type_check(Affects.ray_of_enfeeblement);
            protection_type_check(Affects.feeblemind);

            if ((gbl.damage_flags & 0x40) != 0)
            {
                protection_type_check(0);
            }
        }


        internal static void sub_3C77C(Effect arg_0, object param, Player player)
        {
            gbl.spell_target = player.actions.target;

            if ((gbl.spell_target.field_14B & 8) != 0)
            {
                gbl.damage += player.ranger_lvl;
            }
        }


        internal static void sub_3C7B5(Effect arg_0, object param, Player player)
        {
            if ((gbl.damage_flags & 0x04) != 0)
            {
                protection_type_check(0);
            }
        }


        internal static void sub_3C7CC(Effect arg_0, object param, Player player)
        {
            player.actions.move = 0;
        }


        internal static void sub_3C7E0(Effect arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            if (arg_0 == Effect.Add)
            {
                player.quick_fight = QuickFight.True;

                if (player.field_F7 <= 0x7f ||
                    player.field_F7 == 0xb3)
                {
                    player.field_F7 = 0x0B3;
                }
                else
                {
                    player.field_F7 = 0x0B2;
                }

                player.actions.target = null;

                ovr032.Rebuild_SortedCombatantList(gbl.mapToBackGroundTile, ovr033.PlayerMapSize(player), 0xff, 0xff,
                    ovr033.PlayerMapYPos(player), ovr033.PlayerMapXPos(player));

                player.actions.target = gbl.player_array[gbl.SortedCombatantList[1].player_index];

                player.combat_team = ovr025.opposite_team(player.actions.target);
            }
            else
            {
                if (player.field_F7 == 0xb3)
                {
                    player.field_F7 = 0;
                }

                player.combat_team = (CombatTeam)affect.field_3;
            }
        }


        internal static void add_affect_19(Effect arg_0, object param, Player player)
        {
            ovr024.add_affect(false, 0xff, 0xff, Affects.invisibility, player);
        }


        internal static void sub_3C8EF(Effect add_remove, object param, Player player)
        {
            if (add_remove == Effect.Remove)
            {
                player.field_191 = (byte)((((player.field_114 * ovr026.sub_6B3D1(player)) + player.paladin_lvl - 1) / 5) + 1);
            }
        }


        internal static void sp_fear(Effect add_remove, object param, Player player) /* sub_3C932 */
        {
            if (add_remove == Effect.Remove)
            {
                if (player.field_F7 == 0xB3)
                {
                    player.field_F7 = 0;
                    player.quick_fight = QuickFight.False;
                }

                player.actions.field_10 = 0;
            }
        }


        internal static void sub_3C975(Effect arg_0, object arg_2, Player target)
        {
            if (ovr025.getTargetRange(target, gbl.player_ptr) < 2)
            {
                int bkup_damage = gbl.damage;
                byte bkup_damage_flags = gbl.damage_flags;

                gbl.damage *= 2;
                gbl.damage_flags = 8;

                ovr025.DisplayPlayerStatusString(true, 10, "resists dispel evil", gbl.player_ptr);

                ovr024.damage_person(false, 0, gbl.damage, gbl.player_ptr);
                gbl.damage = bkup_damage;
                gbl.damage_flags = bkup_damage_flags;
            }
        }


        internal static void sp_dispel_evil(Effect arg_0, object param, Player player)
        {
            gbl.spell_target = player.actions.target;

            if ((gbl.spell_target.field_14B & 1) != 0 &&
                ovr024.do_saving_throw(0, 4, gbl.spell_target) == false)
            {
                ovr024.sub_63014("is dispelled", Status.gone, gbl.spell_target);

                ovr024.remove_affect(null, Affects.dispel_evil, gbl.player_ptr);
                ovr024.remove_affect(null, Affects.sp_dispel_evil, gbl.player_ptr);
            }
            else
            {
                ovr025.DisplayPlayerStatusString(true, 10, "resists dispel evil", gbl.spell_target);
            }
        }

        internal static void empty(Effect arg_0, object param, Player player)
        {
        }

        internal static void setup_spells2()
        {
            gbl.affect_jump_list = new affectDelegate[148];

            /* gbl.spell_jump_list[ 0 ] == stru_1D2D0 */
            gbl.affect_jump_list[1] = ovr013.sub_3A096;
            gbl.affect_jump_list[2] = ovr013.sub_3A0A6;
            gbl.affect_jump_list[3] = ovr013.sub_3A0DC;
            gbl.affect_jump_list[4] = ovr013.sub_3A15F;
            gbl.affect_jump_list[5] = ovr013.empty;
            gbl.affect_jump_list[6] = ovr013.sub_3A17A;
            gbl.affect_jump_list[7] = ovr013.sub_3A1DF;
            gbl.affect_jump_list[8] = ovr013.affect_protect_evil;
            gbl.affect_jump_list[9] = ovr013.affect_protect_good;
            gbl.affect_jump_list[10] = ovr013.affect_resist_cold;
            gbl.affect_jump_list[11] = ovr013.affect_charm_person;
            gbl.affect_jump_list[12] = ovr013.empty;
            gbl.affect_jump_list[13] = ovr013.Suffocates;
            gbl.affect_jump_list[14] = ovr013.empty;
            gbl.affect_jump_list[15] = ovr013.sub_3A3BC;
            gbl.affect_jump_list[16] = ovr013.empty;
            gbl.affect_jump_list[17] = ovr013.affect_shield;
            gbl.affect_jump_list[18] = ovr013.sub_3A44A;
            gbl.affect_jump_list[19] = ovr013.empty;
            gbl.affect_jump_list[20] = ovr013.affect_resist_fire;
            gbl.affect_jump_list[21] = ovr013.is_silenced1;
            gbl.affect_jump_list[22] = ovr013.sub_3A517;
            gbl.affect_jump_list[23] = ovr013.affect_spiritual_hammer;
            gbl.affect_jump_list[24] = ovr013.empty;
            gbl.affect_jump_list[25] = ovr013.sub_3A6C6;
            gbl.affect_jump_list[26] = ovr013.sub_3A6FB;
            gbl.affect_jump_list[27] = ovr013.sub_3A071;
            gbl.affect_jump_list[28] = ovr013.sub_3A73F;
            gbl.affect_jump_list[29] = ovr013.three_quarters_damage;
            gbl.affect_jump_list[30] = ovr013.sub_3A7E8;
            gbl.affect_jump_list[31] = ovr013.sub_3A071;
            gbl.affect_jump_list[32] = ovr013.sub_3A89E;
            gbl.affect_jump_list[33] = ovr013.sub_3A951;
            gbl.affect_jump_list[34] = ovr013.sub_3A974;
            gbl.affect_jump_list[35] = ovr013.sub_3A9D9;
            gbl.affect_jump_list[36] = ovr013.affect_curse;
            gbl.affect_jump_list[37] = ovr013.has_action_timedout;
            gbl.affect_jump_list[38] = ovr013.empty;
            gbl.affect_jump_list[39] = ovr013.spl_age;
            gbl.affect_jump_list[40] = ovr013.sub_3AC1D;
            gbl.affect_jump_list[41] = ovr013.sub_3AFE0;
            gbl.affect_jump_list[42] = ovr013.sub_3B01B;
            gbl.affect_jump_list[43] = ovr013.weaken;
            gbl.affect_jump_list[44] = ovr013.sub_3B0C2;
            gbl.affect_jump_list[45] = ovr013.affect_protect_evil;
            gbl.affect_jump_list[46] = ovr013.affect_protect_good;
            gbl.affect_jump_list[47] = ovr013.sub_3B153;
            gbl.affect_jump_list[48] = ovr013.sub_3B1A2;
            gbl.affect_jump_list[49] = ovr013.sub_3B1C9;
            gbl.affect_jump_list[50] = ovr013.sub_3B212;
            gbl.affect_jump_list[51] = ovr013.sub_3A071;
            gbl.affect_jump_list[52] = ovr013.sub_3A071;
            gbl.affect_jump_list[53] = ovr013.sub_3A071;
            gbl.affect_jump_list[54] = ovr013.sub_3B243;
            gbl.affect_jump_list[55] = ovr013.empty;
            gbl.affect_jump_list[56] = ovr013.sub_3B27B;
            gbl.affect_jump_list[57] = ovr014.engulfs;
            gbl.affect_jump_list[58] = ovr013.sub_3B29A;
            gbl.affect_jump_list[59] = ovr013.sub_3B2BA;
            gbl.affect_jump_list[60] = ovr013.sub_3B2D8;
            gbl.affect_jump_list[61] = ovr013.sub_3B32B;
            gbl.affect_jump_list[62] = ovr013.heal_1hp_per_turn;
            gbl.affect_jump_list[63] = ovr013.blocks_spells_below_lvl4;
            gbl.affect_jump_list[64] = ovr013.sub_3B520;
            gbl.affect_jump_list[65] = ovr013.sub_3B534;
            gbl.affect_jump_list[66] = ovr013.sub_3B548;
            gbl.affect_jump_list[67] = ovr013.sub_3B55C;
            gbl.affect_jump_list[68] = ovr013.spell_stupid;
            gbl.affect_jump_list[69] = ovr013.sub_3B636;
            gbl.affect_jump_list[70] = ovr013.sub_3B671;
            gbl.affect_jump_list[71] = ovr013.sub_3B685;
            gbl.affect_jump_list[72] = ovr013.sub_3B696;
            gbl.affect_jump_list[73] = ovr013.sub_3B6D2;
            gbl.affect_jump_list[74] = ovr013.empty;
            gbl.affect_jump_list[75] = ovr013.sub_3B71A;
            gbl.affect_jump_list[76] = ovr013.sub_3B772;
            gbl.affect_jump_list[77] = ovr013.spl_berzerk;
            gbl.affect_jump_list[78] = ovr013.sub_3B8D9;
            gbl.affect_jump_list[79] = ovr013.sub_3B919;
            gbl.affect_jump_list[80] = ovr013.sub_3B94C;
            gbl.affect_jump_list[81] = ovr013.half_damage;
            gbl.affect_jump_list[82] = ovr013.sub_3B990;
            gbl.affect_jump_list[83] = ovr023.spell_paralizing_gaze;
            gbl.affect_jump_list[84] = ovr013.sub_3B9E1;
            gbl.affect_jump_list[85] = ovr013.sub_3BA14;
            gbl.affect_jump_list[86] = ovr023.spell_spit_acid;
            gbl.affect_jump_list[87] = ovr014.attack_or_kill;
            gbl.affect_jump_list[88] = ovr023.cast_breath;
            gbl.affect_jump_list[89] = ovr013.spell_displace;
            gbl.affect_jump_list[90] = ovr023.spell_breathes_acid;
            gbl.affect_jump_list[91] = ovr013.sub_3BAB9;
            gbl.affect_jump_list[92] = ovr013.empty;
            gbl.affect_jump_list[93] = ovr013.sub_3BD98;
            gbl.affect_jump_list[94] = ovr013.sub_3BDB2;
            gbl.affect_jump_list[95] = ovr013.sub_3BE06;
            gbl.affect_jump_list[96] = ovr014.hugs;
            gbl.affect_jump_list[97] = ovr013.con_saving_bonus;
            gbl.affect_jump_list[98] = ovr013.sub_3BEB8;
            gbl.affect_jump_list[99] = ovr013.sub_3BEE8;
            gbl.affect_jump_list[100] = ovr013.sub_3BF91;
            gbl.affect_jump_list[101] = ovr013.sp_regenerate;
            gbl.affect_jump_list[102] = ovr013.sub_3C01E;
            gbl.affect_jump_list[103] = ovr013.sub_3C05D;
            gbl.affect_jump_list[104] = ovr013.sub_3C0DA;
            gbl.affect_jump_list[105] = ovr013.sub_3C14F;
            gbl.affect_jump_list[106] = ovr013.sub_3C15D;
            gbl.affect_jump_list[107] = ovr013.sub_3C16B;
            gbl.affect_jump_list[108] = ovr013.sub_3C18F;
            gbl.affect_jump_list[109] = ovr013.sub_3C1A4;
            gbl.affect_jump_list[110] = ovr013.sub_3C1B2;
            gbl.affect_jump_list[111] = ovr013.sub_3C1C9;
            gbl.affect_jump_list[112] = ovr013.sub_3C1EA;
            gbl.affect_jump_list[113] = ovr013.sub_3C201;
            gbl.affect_jump_list[114] = ovr013.sub_3C246;
            gbl.affect_jump_list[115] = ovr013.sub_3C260;
            gbl.affect_jump_list[116] = ovr013.half_damage_if_weap_magic;
            gbl.affect_jump_list[117] = ovr013.sub_3C2F9;
            gbl.affect_jump_list[118] = ovr013.sub_3C33C;
            gbl.affect_jump_list[119] = ovr013.sub_3C356;
            gbl.affect_jump_list[120] = ovr013.sub_3C3A2;
            gbl.affect_jump_list[121] = ovr013.sub_3C3F6;
            gbl.affect_jump_list[122] = ovr013.spl_paralyze;
            gbl.affect_jump_list[123] = ovr013.sub_3C59D;
            gbl.affect_jump_list[124] = ovr013.sub_3C5D0;
            gbl.affect_jump_list[125] = ovr013.sub_3C5F4;
            gbl.affect_jump_list[126] = ovr023.cast_gaze_paralyze;
            gbl.affect_jump_list[127] = ovr013.empty;
            gbl.affect_jump_list[128] = ovr023.spell_breathes_fire;
            gbl.affect_jump_list[129] = ovr013.sub_3C623;
            gbl.affect_jump_list[130] = ovr013.sub_3C643;
            gbl.affect_jump_list[131] = ovr023.cast_breath_fire;
            gbl.affect_jump_list[132] = ovr023.cast_throw_lightening;
            gbl.affect_jump_list[133] = ovr013.sub_3C750;
            gbl.affect_jump_list[134] = ovr013.sub_3C77C;
            gbl.affect_jump_list[135] = ovr013.sub_3C7B5;
            gbl.affect_jump_list[136] = ovr013.sub_3C7CC;
            gbl.affect_jump_list[137] = ovr013.sub_3C7E0;
            gbl.affect_jump_list[138] = ovr013.add_affect_19;
            gbl.affect_jump_list[139] = ovr014.sub_425C6;
            gbl.affect_jump_list[140] = ovr013.empty;
            gbl.affect_jump_list[141] = ovr013.sub_3C8EF;
            gbl.affect_jump_list[142] = ovr013.sp_fear;
            gbl.affect_jump_list[143] = ovr013.sub_3C975;
            gbl.affect_jump_list[144] = ovr014.sub_426FC;
            gbl.affect_jump_list[145] = ovr013.sp_dispel_evil;
            gbl.affect_jump_list[146] = ovr013.empty;
            gbl.affect_jump_list[147] = ovr013.do_items_affect;

        }
    }
}
