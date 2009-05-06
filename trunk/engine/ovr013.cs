using Classes;

namespace engine
{
    class ovr013
    {
        /// <summary>
        /// If same as current affect damage set to zero, or if affect is zero
        /// </summary>
        static void ProtectedIf(Affects affect) /* sub_3A019 */
        {
            if (gbl.current_affect == affect)
            {
                Protected();
            }
        }

        internal static void Protected()
        {
            gbl.damage = 0;
            gbl.current_affect = 0;
        }


        internal static bool addAffect(ushort time, int data, Affects affect_type, Player player)
        {
            if (gbl.cureSpell == true)
            {
                return false;
            }
            else
            {
                ovr024.add_affect(true, data, time, affect_type, player);
                return true;
            }
        }


        internal static void sub_3A071(Effect arg_0, object param, Player player)
        {
            ovr025.clear_actions(player);
        }


        internal static void sub_3A087(Effect arg_0, object param, Player player)
        {
            gbl.savingThrowRoll += 1;
            gbl.attack_roll++;
        }


        internal static void Bless(Effect add_remove, object param, Player player)
        {
            gbl.monster_morale += 5;
            gbl.attack_roll++;
        }


        internal static void Curse(Effect arg_0, object param, Player player)
        {
            if (gbl.monster_morale < 5)
            {
                gbl.monster_morale = 0;
            }
            else
            {
                gbl.monster_morale -= 5;
            }
            gbl.attack_roll--;
        }


        internal static void SticksToSnakes(Effect arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            byte var_1 = (byte)(player.field_19D + player.field_19C);

            if (affect.affect_data > var_1)
            {
                affect.affect_data -= var_1;
            }
            else
            {
                ovr024.remove_affect(null, Affects.sticks_to_snakes, player);
            }

            ovr025.MagicAttackDisplay("is fighting with snakes", true, player);
            ovr025.ClearPlayerTextArea();

            ovr025.clear_actions(player);
        }


        internal static void DispelEvil(Effect arg_0, object param, Player player)
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

                if (gbl.spell_target.monsterType == MonsterType.type_10)
                {
                    bonus = 1;
                }
                else if (gbl.spell_target.monsterType == MonsterType.type_9 || gbl.spell_target.monsterType == MonsterType.type_12)
                {
                    bonus = 2;
                }
                else if (gbl.spell_target.monsterType == MonsterType.animated_dead)
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
            gbl.damage_flags = DamageType.Magic | DamageType.Fire;
        }


        internal static void FaerieFire(Effect arg_0, object param, Player player)
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
                gbl.savingThrowRoll += 2;
                gbl.attack_roll -= 2;
            }
        }


        internal static void affect_protect_good(Effect arg_0, object param, Player player) /* sub_3A259 */
        {
            if (gbl.player_ptr.alignment == 0 ||
                gbl.player_ptr.alignment == 3 ||
                gbl.player_ptr.alignment == 6)
            {
                gbl.savingThrowRoll += 2;
                gbl.attack_roll -= 2;
            }
        }


        internal static void affect_resist_cold(Effect arg_0, object param, Player player) /* sub_3A28E */
        {
            if ((gbl.damage_flags & DamageType.Cold) != 0)
            {
                gbl.damage /= 2;
                gbl.savingThrowRoll += 3;
            }
        }


        internal static void affect_charm_person(Effect arg_0, object param, Player player) /* sub_3A2AD */
        {
            Affect affect = (Affect)param;

            if (arg_0 == Effect.Remove)
            {
                player.combat_team = (CombatTeam)((affect.affect_data & 0x40) >> 6);

                if (player.control_morale == Control.PC_Berzerk)
                {
                    player.control_morale = Control.PC_Base;
                }
            }
            else
            {
                if ((affect.affect_data & 0x20) == 0)
                {
                    affect.affect_data += (byte)(0x20 + (((int)player.combat_team) << 6));

                    player.combat_team = (CombatTeam)(affect.affect_data >> 7);
                    player.quick_fight = QuickFight.True;

                    if (player.control_morale < Control.NPC_Base)
                    {
                        player.control_morale = Control.PC_Berzerk;
                    }

                    player.actions.target = null;
                    ovr025.CountCombatTeamMembers();
                }
                gbl.monster_morale = 100;
            }
        }


        internal static void Suffocates(Effect arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            if (affect.affect_data == 0)
            {
                ovr024.KillPlayer("Suffocates", Status.dead, player);
            }
            else
            {
                affect.affect_data--;
            }
        }


        internal static void AffectPoisonDamage(Effect arg_0, object param, Player player) // sub_3A3BC
        {
            Affect affect = (Affect)param;

            if (addAffect(10, affect.affect_data, Affects.poison_damage, player) == true &&
                player.hit_point_current > 1)
            {
                gbl.damage_flags = 0;

                ovr024.damage_person(false, 0, 1, player);

                if (gbl.game_state != GameState.Combat)
                {
                    ovr025.PartySummary(gbl.player_ptr);
                }
            }
        }


        internal static void AffectShield(Effect arg_0, object param, Player player) /* sub_3A41F */
        {
            if (player.ac < 0x39) // AC 3
            {
                player.ac = 0x39; // AC 3
            }

            gbl.savingThrowRoll += 1;

            if (gbl.spell_id == 15) /* Magic Missle */
            {
                gbl.damage = 0;
            }
        }


        internal static void AffectGnomeVsManSizedGiant(Effect arg_0, object param, Player player) // sub_3A44A
        {
            if (player.actions != null &&
                player.actions.target != null &&
                (player.actions.target.field_14B & 2) != 0)
            {
                gbl.spell_target = player.actions.target;
                gbl.attack_roll++;
            }
        }


        internal static void AffectResistFire(Effect add_remove, object param, Player player) /* sub_3A480 */
        {
            if (add_remove == Effect.Add &&
                (gbl.damage_flags & DamageType.Fire) != 0)
            {
                gbl.damage /= 2;
                gbl.savingThrowRoll += 3;
            }
        }


        internal static void is_silenced1(Effect arg_0, object param, Player player)
        {
            if (player.actions.can_use == true)
            {
                ovr025.DisplayPlayerStatusString(true, 10, "is silenced", player);
            }

            player.actions.can_use = false;
            player.actions.can_cast = false;
        }


        internal static void AffectSlowPoison(Effect arg_0, object param, Player player) // sub_3A517
        {
            if (player.HasAffect(Affects.poisoned) == true)
            {
                ovr024.KillPlayer("dies from poison", Status.dead, player);
            }

            gbl.cureSpell = true;

            ovr024.remove_affect(null, Affects.poison_damage, player);

            gbl.cureSpell = false;
        }


        internal static void affect_spiritual_hammer(Effect add_remove, object param, Player player) /* sub_3A583 */
        {
            Item item = player.items.Find(i => i.type == 20 && i.namenum3 == 0xf3); // ItemType.Hammer
            bool item_found = item != null;

            if (add_remove == Effect.Remove && item != null)
            {
                ovr025.lose_item(item, player);
            }

            if (add_remove == Effect.Add &&
                item_found == false &&
                player.items.Count < Player.MaxItems)
            {
                item = new Item();
                item.type = 20; // ItemType.Hammer
                item.namenum2 = 20;
                item.namenum3 = 243;
                item.plus = 1;
                item.affect_2 = Affects.spiritual_hammer;
                item.affect_3 = (Affects)160;

                player.items.Add(item);
                ovr020.ready_Item(item);

                ovr025.DisplayPlayerStatusString(true, 10, "Gains an item", player);
            }

            ovr025.reclac_player_values(player);
        }


        internal static void sub_3A6C6(Effect arg_0, object param, Player player)
        {
            if (player.name.Length == 0 &&
                gbl.player_ptr.HasAffect(Affects.detect_invisibility) == false)
            {
                gbl.targetInvisible = true;
                gbl.attack_roll -= 4;
            }
        }


        internal static void AffectDwarfVsOrc(Effect arg_0, object param, Player player) // sub_3A7E8
        {
            gbl.spell_target = player.actions.target;

            if ((gbl.spell_target.field_14B & 4) != 0)
            {
                gbl.attack_roll++;
            }
        }


        internal static void MirrorImage(Effect arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            if (ovr024.roll_dice((affect.affect_data >> 4) + 1, 1) > 1 &&
                gbl.spell_id > 0 &&
                gbl.byte_1D2C7 == false)
            {
                Protected();

                ovr025.DisplayPlayerStatusString(true, 10, "lost an image", player);

                affect.affect_data -= 1;

                if (affect.affect_data == 0)
                {
                    ovr024.remove_affect(null, Affects.mirror_image, player);
                }
            }
        }


        internal static void three_quarters_damage(Effect arg_0, object param, Player player)
        {
            gbl.damage -= gbl.damage / 4;
        }


        internal static void StinkingCloud(Effect arg_0, object param, Player player)
        {
            if (player.actions.can_use == true)
            {
                ovr025.DisplayPlayerStatusString(true, 10, "is coughing", player);
            }

            player.actions.can_use = false;
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
                ovr025.CombatDisplayPlayerSummary(player);
            }
        }


        internal static void sub_3A89E(Effect arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            affect.callAffectTable = false;

            if (gbl.cureSpell == false)
            {
                ovr024.KillPlayer("collapses", Status.dead, player);
            }

            player.combat_team = (CombatTeam)(affect.affect_data >> 4);
            player.quick_fight = QuickFight.True;
            player.field_E9 = 0;

            player.field_DD = (byte)(player.fighter_lvl + (player.fighter_old_lvl * ovr026.MulticlassExceedLastLevel(player)));
            player.base_movement = 0x0C;

            if (player.control_morale == Control.PC_Berzerk)
            {
                player.control_morale = Control.PC_Base;
            }

            player.monsterType = 0;
        }


        internal static void AffectBlinded(Effect arg_0, object param, Player player) // sub_3A951
        {
            gbl.attack_roll -= 4;

            player.ac -= 4;
            player.field_19B -= 4;

            gbl.savingThrowRoll -= 4;
        }


        internal static void AffectCauseDisease(Effect add_remove, object param, Player player) // sub_3A974
        {
            ovr013.CallAffectTable(add_remove, param, player, Affects.affect_2b);
            ovr013.CallAffectTable(add_remove, param, player, Affects.cause_disease_2);
        }


        internal static void AffectConfuse(Effect arg_0, object arg_2, Player player) // sub_3A9D9
        {
            byte var_1 = ovr024.roll_dice(100, 1);

            if (var_1 >= 1 && var_1 <= 10)
            {
                ovr024.remove_affect(null, Affects.confuse, player);
                player.actions.fleeing = true;
                player.quick_fight = QuickFight.True;

                if (player.control_morale < Control.NPC_Base)
                {
                    player.control_morale = Control.PC_Berzerk;
                }

                player.actions.target = null;

                ovr024.is_unaffected("runs away", false, DamageOnSave.Zero, true, 0, 10, Affects.fear, player);
            }
            else if (var_1 >= 11 && var_1 <= 60)
            {
                ovr025.MagicAttackDisplay("is confused", true, player);
                ovr025.ClearPlayerTextArea();
                sub_3A071(0, arg_2, player);
            }
            else if (var_1 >= 61 && var_1 <= 80)
            {
                ovr024.is_unaffected("goes berserk", false, DamageOnSave.Zero, true, (byte)player.combat_team, 1, Affects.affect_89, player);
                ovr013.CallAffectTable(Effect.Add, null, player, Affects.affect_89);
            }
            else if (var_1 >= 81 && var_1 <= 100)
            {
                ovr025.MagicAttackDisplay("is enraged", true, player);
                ovr025.ClearPlayerTextArea();
            }

            if (ovr024.RollSavingThrow(-2, SaveVerseType.type4, player) == true)
            {
                ovr024.remove_affect(null, Affects.confuse, player);
            }
        }


        internal static void affect_curse(Effect arg_0, object param, Player player) /* sub_3AB6F */
        {
            gbl.attack_roll -= 4;
            gbl.savingThrowRoll -= 4;
        }


        internal static void AffectBlink(Effect arg_0, object param, Player player) // has_action_timedout
        {
            if (player.actions.delay == 0)
            {
                gbl.targetInvisible = true;
                gbl.attack_roll = -1;
            }
        }


        internal static void AffectHaste(Effect arg_0, object param, Player player) // spl_age
        {
            Affect affect = (Affect)param;

            if ((affect.affect_data & 0x10) == 0)
            {
                affect.affect_data += 0x10;

                ovr025.DisplayPlayerStatusString(true, 10, "ages", player);
                player.age++;
            }

            gbl.attacksLeft *= 2;
        }


        internal static void sub_3AC1D(Effect arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            var var_8 = gbl.NoxiousCloud.Find(cell => cell.player == player && cell.field_1C == (affect.affect_data >> 4));

            if (var_8 != null)
            {
                ovr025.string_print01("The air clears a little...");

                for (int var_B = 1; var_B <= 4; var_B++)
                {
                    if (var_8.present[var_B] == true)
                    {
                        var tmp = var_8.targetPos + gbl.MapDirectionDelta[gbl.unk_18AE9[var_B]];

                        bool var_9 = gbl.downedPlayers.Exists(cell => cell.target != null && cell.map == tmp);

                        if (var_9 == true)
                        {
                            gbl.mapToBackGroundTile[tmp] = 0x1F;
                        }
                        else
                        {
                            gbl.mapToBackGroundTile[tmp] = var_8.groundTile[var_B];
                        }
                    }
                }

                gbl.NoxiousCloud.Remove(var_8);

                foreach(var var_4 in gbl.NoxiousCloud)
                {
                    for (int var_B = 1; var_B <= 4; var_B++)
                    {
                        if (var_4.present[var_B] == true)
                        {
                            var tmp = gbl.MapDirectionDelta[gbl.unk_18AE9[var_B]] + var_4.targetPos;

                            gbl.mapToBackGroundTile[tmp] = 0x1E;
                        }
                    }
                }
            }
        }


        static void AvoidMissleAttack(int percentage, Player player) // sub_3AF06
        {
            if (gbl.player_ptr.field_151 != null &&
                ovr025.getTargetRange(player, gbl.player_ptr) == 0 &&
                ovr024.roll_dice(100, 1) <= percentage)
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

                if (item_found == false || item == null)
                {
                    item = player.field_151;
                }
            }

            return item;
        }


        internal static void AffectProtNormalMissles(Effect arg_0, object param, Player player) // sub_3AFE0
        {
            Item item = get_primary_weapon(gbl.player_ptr);

            if (item != null && item.plus == 0)
            {
                AvoidMissleAttack(100, player);
            }
        }


        internal static void AffectSlow(Effect arg_0, object param, Player player) //sub_3B01B
        {
            gbl.attacksLeft /= 2;
        }


        internal static void weaken(Effect arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            if (addAffect(0x3c, affect.affect_data, Affects.affect_2b, player) == true)
            {
                if (player.strength > 3)
                {
                    ovr025.DisplayPlayerStatusString(true, 10, "is weakened", player);
                    player.strength--;
                }
                else if (player.HasAffect(Affects.helpless) == true)
                {
                    ovr024.add_affect(false, 0xff, 0, Affects.helpless, player);
                }
            }
        }


        internal static void sub_3B0C2(Effect arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            if (addAffect(10, affect.affect_data, Affects.cause_disease_2, player) == true)
            {
                if (player.hit_point_current > 1)
                {
                    gbl.damage_flags = 0;

                    ovr024.damage_person(false, 0, 1, player);

                    if (gbl.game_state != GameState.Combat)
                    {
                        ovr025.PartySummary(gbl.player_ptr);
                    }
                }
                else if (player.HasAffect(Affects.helpless) == false)
                {
                    ovr024.add_affect(false, 0xff, 0, Affects.helpless, player);
                }
            }
        }


        internal static void AffectDwarfGnomeVsGiants(Effect arg_0, object param, Player player)
        {
            gbl.spell_target = player.actions.target;

            if (gbl.player_ptr.monsterType == MonsterType.giant ||
                gbl.player_ptr.monsterType == MonsterType.type_10)
            {
                if ((gbl.player_ptr.field_DE & 0x7F) == 2)
                {
                    gbl.attack_roll -= 4;
                }
            }
        }


        internal static void sub_3B1A2(Effect arg_0, object param, Player player)
        {
            if (gbl.player_ptr.monsterType == MonsterType.type_1 &&
                (gbl.player_ptr.field_DE & 0x7F) == 2)
            {
                gbl.attack_roll -= 4;
            }
        }


        internal static void AffectPrayer(Effect arg_0, object param, Player player) // sub_3B1C9
        {
            Affect affect = (Affect)param;

            CombatTeam team = (CombatTeam)((affect.affect_data & 0x10) >> 4);

            if (player.combat_team == team)
            {
                sub_3A087(arg_0, affect, player);
            }
            else
            {
                gbl.attack_roll -= 1;
                gbl.savingThrowRoll -= 1;
            }
        }


        internal static void HotFireShield(Effect arg_0, object param, Player player) // sub_3B212
        {
            if ((gbl.damage_flags & DamageType.Cold) != 0)
            {
                gbl.savingThrowRoll += 2;
            }
            else if ((gbl.damage_flags & DamageType.Fire) != 0 && gbl.savingThrowMade == false)
            {
                gbl.damage *= 2;
            }
        }


        internal static void ColdFireShield(Effect arg_0, object param, Player player) // sub_3B243
        {
            if ((gbl.damage_flags & DamageType.Fire) != 0)
            {
                gbl.savingThrowRoll += 2;
            }
            else if ((gbl.damage_flags & DamageType.Cold) != 0 && gbl.savingThrowMade == false)
            {
                gbl.damage *= 2;
            }
        }


        internal static void sub_3B27B(Effect arg_0, object param, Player player) // sub_3B27B
        {
            ovr024.add_affect(false, 12, 1, Affects.invisibility, player);
        }


        internal static void AffectClearMovement(Effect arg_0, object param, Player player) //sub_3B29A
        {
            player.actions.move = 0;

            if (gbl.resetMovesLeft == true)
            {
                gbl.attacksLeft = 0;
            }
        }


        internal static void AffectRegenration(Effect arg_0, object param, Player player)
        {
            ovr024.add_affect(false, 0xff, 0, Affects.regen_3_hp, player);
        }


        internal static void AffectResistWeapons(Effect arg_0, object param, Player player) // sub_3B2D8
        {
            Item weapon = get_primary_weapon(gbl.player_ptr);

            if (weapon == null ||
                weapon.plus == 0)
            {
                gbl.damage = 0;
            }
            else if (weapon.plus < 3)
            {
                gbl.damage /= 2;
            }
        }


        internal static void AffectFireResist(Effect arg_0, object param, Player player)
        {
            if ((gbl.damage_flags & DamageType.Fire) != 0)
            {
                for (int i = 1; i <= gbl.dice_count; i++)
                {
                    gbl.damage -= 2;

                    if (gbl.damage < gbl.dice_count)
                    {
                        gbl.damage = gbl.dice_count;
                    }
                }

                gbl.savingThrowRoll += 4;

                if ((gbl.damage_flags & DamageType.Magic) == 0)
                {
                    Protected();
                }
            }
        }


        internal static void AffectHighConRegen(Effect arg_0, object param, Player player) /* sub_3B386 */
        {
            Affect affect = (Affect)param;

            if (addAffect(0x3C, affect.affect_data, Affects.highConRegen, player) == true &&
                ovr024.heal_player(1, 1, player) == true)
            {
                ovr025.DescribeHealing(player);
            }
        }


        internal static void AffectMinorGlobeOfInvulnerability(Effect arg_0, object param, Player player) /* sub_3B3CA */
        {
            if (gbl.spell_id > 0 &&
                gbl.spell_table[gbl.spell_id].spellLevel < 4)
            {
                Protected();
            }
        }


        internal static void PoisonAttack(int save_bonus, Player player)
        {
            gbl.spell_target = player.actions.target;

            if (ovr024.RollSavingThrow(save_bonus, SaveVerseType.Poison, gbl.spell_target) == false)
            {
                ovr025.DisplayPlayerStatusString(false, 10, "is Poisoned", gbl.spell_target);
                seg041.GameDelay();
                ovr024.add_affect(false, 0xff, 0, Affects.poisoned, gbl.spell_target);

                ovr024.KillPlayer("is killed", Status.dead, gbl.spell_target);
            }
        }


        internal static void AffectPoisonPlus0(Effect arg_0, object param, Player player) // sub_3B520
        {
            PoisonAttack(0, player);
        }


        internal static void AffectPoisonPlus4(Effect arg_0, object param, Player player) // sub_3B534
        {
            PoisonAttack(4, player);
        }


        internal static void AffectPoisonPlus2(Effect arg_0, object param, Player player) // sub_3B548
        {
            PoisonAttack(2, player);
        }


        internal static void ThriKreenParalyze(Effect arg_0, object param, Player player) // sub_3B55C
        {
            ushort time = ovr024.roll_dice(8, 2);

            gbl.spell_target = player.actions.target;

            if (ovr024.RollSavingThrow(0, SaveVerseType.Poison, gbl.spell_target) == false)
            {
                ovr025.MagicAttackDisplay("is Paralyzed", true, gbl.spell_target);
                ovr024.add_affect(false, 12, time, Affects.paralyze, gbl.spell_target);
            }
        }


        internal static void AffectFeebleMind(Effect arg_0, object param, Player player) // spell_stupid
        {
            player._int = 7;
            player.wis = 7;

            ovr025.DisplayPlayerStatusString(true, 10, "is stupid", player);

            if (gbl.game_state == GameState.Combat)
            {
                ovr024.TryLooseSpell(player);
            }
        }


        internal static void AffectInvisToAnimals(Effect arg_0, object param, Player player) // sub_3B636
        {
            if (gbl.player_ptr.monsterType == MonsterType.animal)
            {
                if (gbl.player_ptr.HasAffect(Affects.detect_invisibility) == false)
                {
                    gbl.targetInvisible = true;
                }

                gbl.attack_roll -= 4;
            }
        }


        internal static void AffectPoisonNeg2(Effect arg_0, object param, Player player) // sub_3B671
        {
            PoisonAttack(-2, player);
        }


        internal static void AffectInvisible(Effect arg_0, object param, Player player) // sub_3B685
        {
            gbl.targetInvisible = true;
            gbl.attack_roll -= 4;
        }


        internal static void AffectCamouflage(Effect arg_0, object param, Player player) // sub_3B696
        {
            if (ovr024.roll_dice(100, 1) <= 95)
            {
                ovr024.add_affect(false, 12, 1, Affects.invisibility, player);
            }
        }


        internal static void ProtDragonsBreath(Effect arg_0, object param, Player player)
        {
            if ((gbl.damage_flags & DamageType.DragonBreath) > 0)
            {
                Protected();
                ovr025.DisplayPlayerStatusString(true, 10, "is unaffected", player);
            }
        }


        internal static void AffectDragonSlayer(Effect arg_0, object param, Player player) // sub_3B71A
        {
            if (player.actions != null &&
                player.actions.target != null)
            {
                gbl.spell_target = player.actions.target;

                if (gbl.spell_target.monsterType == MonsterType.dragon)
                {
                    gbl.damage = (ovr024.roll_dice(12, 1) * 3) + 4 + ovr025.strengthDamBonus(player);
                    gbl.attack_roll += 2;
                }
            }
        }


        internal static void AffectFrostBrand(Effect arg_0, object param, Player player) // sub_3B772
        {
            if (player.actions != null)
            {
                gbl.spell_target = player.actions.target;

                if (gbl.spell_target != null &&
                    gbl.spell_target.monsterType == MonsterType.fire)
                {
                    gbl.attack_roll += 3;
                    gbl.damage += 3;
                }
            }
        }


        internal static void AffectBerzerk(Effect arg_0, object param, Player player)
        {
            if (arg_0 == Effect.Add)
            {
                player.quick_fight = QuickFight.True;

                if (player.control_morale < Control.NPC_Base ||
                    player.control_morale == Control.PC_Berzerk)
                {
                    player.control_morale = Control.PC_Berzerk;
                }
                else
                {
                    player.control_morale = Control.NPC_Berzerk;
                }

                if (gbl.game_state == GameState.Combat)
                {
                    player.actions.target = null;

                    var scl = ovr032.Rebuild_SortedCombatantList(gbl.mapToBackGroundTile, ovr033.PlayerMapSize(player), 0xff, 0xff,
                        ovr033.PlayerMapPos(player));

                    player.actions.target = scl[0].player;

                    player.actions.can_cast = false;
                    player.combat_team = player.actions.target.OppositeTeam();

                    ovr025.DisplayPlayerStatusString(true, 10, "goes berzerk", player);
                }
            }
            else
            {
                if (player.control_morale == Control.PC_Berzerk)
                {
                    player.control_morale = Control.PC_Base;
                }

                player.combat_team = CombatTeam.Ours;
            }
        }


        internal static void sub_3B8D9(Effect arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            if (ovr024.combat_heal(player.hit_point_current, player) == false)
            {
                addAffect(1, affect.affect_data, Affects.affect_4e, player);
            }
        }


        internal static void MagicFireAttack_2d10(Effect arg_0, object param, Player player) // sub_3B919
        {
            gbl.damage_flags = DamageType.Magic | DamageType.Fire;

            ovr024.damage_person(false, 0, ovr024.roll_dice_save(10, 2), player.actions.target);
        }


        internal static void AnkhegAcidAttack(Effect arg_0, object param, Player player) // sub_3B94C
        {
            gbl.damage_flags = DamageType.Acid;

            ovr024.damage_person(false, 0, ovr024.roll_dice_save(4, 1), player.actions.target);
        }


        internal static void half_damage(Effect arg_0, object param, Player player) /* sub_3B97F */
        {
            gbl.damage /= 2;
        }


        internal static void AffectResistFireAndCold(Effect arg_0, object param, Player player) // sub_3B990
        {
            if ((gbl.damage_flags & DamageType.Fire) != 0 ||
                (gbl.damage_flags & DamageType.Cold) != 0)
            {
                if (ovr024.RollSavingThrow(0, SaveVerseType.type4, player) == true &&
                    gbl.spell_table[gbl.spell_id].damageOnSave != 0)
                {
                    gbl.damage = 0;
                }
                else
                {
                    gbl.damage /= 2;
                }
            }
        }


        internal static void AffectShamblerAbsorbLightning(Effect arg_0, object param, Player player) // sub_3B9E1
        {
            // Shambling Mounds absorb lighting and get more powerful.

            if ((gbl.damage_flags & DamageType.Electricity) != 0)
            {
                Protected();
                //byte var_1 = ovr024.roll_dice(8, 1);

                player.ac += 8;
            }
        }


        internal static void sub_3BA14(Effect arg_0, object param, Player player)
        {
            Item item = get_primary_weapon(gbl.player_ptr);

            if (item != null &&
                gbl.ItemDataTable[item.type].field_7 == 1)
            {
                gbl.damage = 1;
            }
        }


        internal static void AffectDisplace(Effect arg_0, object param, Player player) /*sub_3BA55*/
        {
            Affect affect = (Affect)param;

            if (affect != null)
            {
                if (gbl.combat_round == 0 && gbl.attack_roll == 0)
                {
                    affect.affect_data &= 0x0f;
                }
                else if ((affect.affect_data & 0x10) == 0)
                {
                    gbl.attack_roll = -1;
                    affect.affect_data |= 0x10;
                }
            }
        }


        internal static void sub_3BAB9(Effect arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            GasCloud var_8 = gbl.PoisonousCloud.Find(cell => cell.player == player && cell.field_1C == (affect.affect_data >> 4));

            if (var_8 != null)
            {
                ovr025.string_print01("The air clears a little...");

                for (int var_B = 1; var_B <= 9; var_B++)
                {
                    if (var_8.present[var_B] == true)
                    {
                        var tmp = var_8.targetPos + gbl.MapDirectionDelta[gbl.unk_18AED[var_B]];

                        bool var_E = gbl.downedPlayers.Exists(cell => cell.target != null && cell.map == tmp);

                        if (var_E == true)
                        {
                            gbl.mapToBackGroundTile[tmp] = 0x1F;
                        }
                        else
                        {
                            gbl.mapToBackGroundTile[tmp] = var_8.groundTile[var_B];
                        }
                    }
                }


                gbl.PoisonousCloud.Remove(var_8);

                foreach(var var_4 in gbl.PoisonousCloud)
                {
                    for (int var_B = 1; var_B <= 9; var_B++)
                    {
                        if (var_4.present[var_B] == true)
                        {
                            var tmp = var_4.targetPos + gbl.MapDirectionDelta[gbl.unk_18AED[var_B]];

                            gbl.mapToBackGroundTile[tmp] = 0x1C;
                        }
                    }
                }
            }
        }


        internal static void half_fire_damage(Effect arg_0, object param, Player arg_6) // sub_3BD98
        {
            if ((gbl.damage_flags & DamageType.Fire) != 0)
            {
                gbl.damage /= 2;
            }
        }


        internal static void sub_3BDB2(Effect arg_0, object param, Player arg_6)
        {
            Item item = get_primary_weapon(gbl.player_ptr);

            if (item != null &&
                (gbl.ItemDataTable[item.type].field_7 & 0x81) != 0)
            {
                gbl.damage /= 2;
            }
        }


        internal static void sub_3BE06(Effect arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;
            affect.callAffectTable = false;

            if (player.in_combat == true)
            {
                ovr024.KillPlayer("Falls dead", Status.dead, player);
            }
        }


        internal static void con_saving_bonus(Effect arg_0, object param, Player player) /* sub_3BE42 */
        {
            if (gbl.saveVerseType == SaveVerseType.type4 ||
                gbl.saveVerseType == SaveVerseType.type2)
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

                gbl.savingThrowRoll += save_bonus;
            }
        }


        internal static void AffectRegen3Hp(Effect arg_0, object param, Player player) // sub_3BEB8
        {
            player.hit_point_current += 3;

            if (player.hit_point_current > player.hit_point_max)
            {
                player.hit_point_current = player.hit_point_max;
            }
        }


        internal static void sub_3BEE8(Effect arg_0, object param, Player player_ptr)
        {
            Affect arg_2 = (Affect)param;

            byte heal_amount = 0;

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
                arg_2.callAffectTable = false;
                ovr024.remove_affect(arg_2, Affects.affect_63, player_ptr);
            }
        }


        internal static void AffectTrollFireOrAcid(Effect arg_0, object param, Player player)
        {
            if ((gbl.damage_flags & DamageType.Fire) == 0 &&
                (gbl.damage_flags & DamageType.Acid) == 0)
            {
                ovr024.add_affect(true, 0xff, ovr024.roll_dice(6, 3), Affects.TrollRegen, player);
            }
        }


        internal static void AffectTrollRegenerate(Effect arg_0, object param, Player player) // sp_regenerate
        {
            if (player.HasAffect(Affects.regen_3_hp) == false &&
                player.HasAffect(Affects.regenerate) == false)
            {
                ovr024.add_affect(true, 0xff, 3, Affects.regenerate, player);
            }
        }


        internal static void AffectTrollRegen(Effect arg_0, object param, Player player) // sub_3C01E
        {
            Affect affect = (Affect)param;

            if (ovr024.combat_heal(player.hit_point_max, player) == false)
            {
                addAffect(1, affect.affect_data, Affects.TrollRegen, player);
            }
        }


        internal static void AffectSalamanderHeatDamage(Effect arg_0, object param, Player player) // sub_3C05D
        {
            gbl.spell_target = player.actions.target;

            if (gbl.spell_target.HasAffect(Affects.resist_fire) == false &&
                gbl.spell_target.HasAffect(Affects.cold_fire_shield) == false &&
                gbl.spell_target.HasAffect(Affects.fire_resist) == false)
            {
                gbl.damage += ovr024.roll_dice(6, 1);
            }
        }


        internal static void sub_3C0DA(Effect arg_0, object param, Player player)
        {
            AvoidMissleAttack(60, player);
        }


        internal static void sub_3C0EE(int rollBase)
        {
            int target_count = ovr025.spellMaxTargetCount(gbl.spell_id);
            int rollNeeded = rollBase + ((11 - target_count) * 5);

            if (gbl.current_affect != 0 || (gbl.damage_flags & DamageType.Magic) != 0)
            {
                if (ovr024.roll_dice(100, 1) <= rollNeeded)
                {
                    Protected();
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


        internal static void AffectElfRisistSleep(Effect arg_0, object param, Player arg_6) // sub_3C16B
        {
            if (ovr024.roll_dice(100, 1) <= 90)
            {
                ProtectedIf(Affects.sleep);
                ProtectedIf(Affects.charm_person);
            }
        }


        internal static void AffectProtCharmSleep(Effect arg_0, object param, Player arg_6) // sub_3C18F
        {
            ProtectedIf(Affects.charm_person);
            ProtectedIf(Affects.sleep);
        }


        internal static void sub_3C1A4(Effect arg_0, object param, Player arg_6)
        {
            ProtectedIf(Affects.paralyze);
        }


        internal static void sub_3C1B2(Effect arg_0, object param, Player arg_6)
        {
            if ((gbl.damage_flags & DamageType.Cold) != 0)
            {
                Protected();
            }
        }


        internal static void sub_3C1C9(Effect arg_0, object param, Player arg_6)
        {
            ProtectedIf(Affects.poisoned);
            ProtectedIf(Affects.paralyze);

            if (gbl.saveVerseType == SaveVerseType.Poison)
            {
                gbl.savingThrowRoll = 100;
            }
        }


        internal static void AffectImmuneToFire(Effect arg_0, object param, Player arg_6) // sub_3C1EA
        {
            if ((gbl.damage_flags & DamageType.Fire) != 0)
            {
                Protected();
            }
        }


        internal static void sub_3C201(Effect arg_0, object param, Player arg_6)
        {
            if ((gbl.damage_flags & DamageType.Fire) != 0)
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
            if ((gbl.damage_flags & DamageType.Electricity) != 0)
            {
                gbl.damage /= 2;
            }
        }


        internal static void sub_3C260(Effect arg_0, object param, Player player)
        {
            Item weapon = get_primary_weapon(gbl.player_ptr);

            if (weapon != null)
            {
                if (gbl.ItemDataTable[weapon.type].field_7 == 0 ||
                    (gbl.ItemDataTable[weapon.type].field_7 & 1) != 0)
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
                item.type == 85)
            {
                gbl.damage = ovr024.roll_dice_save(6, 1) + 1;
            }
        }


        internal static void AffectProtCold(Effect arg_0, object param, Player player) // sub_3C33C
        {
            if ((gbl.damage_flags & DamageType.Cold) != 0)
            {
                gbl.damage /= 2;
            }
        }


        internal static void AffectProtNonMagicWeapons(Effect arg_0, object param, Player player) // sub_3C356
        {
            Item weapon = get_primary_weapon(gbl.player_ptr);

            if ((weapon == null || weapon.plus == 0) &&
                (gbl.player_ptr.race > 0 || gbl.player_ptr.HitDice < 4))
            {
                gbl.damage = 0;
            }
        }


        internal static void sub_3C3A2(Effect arg_0, object param, Player player)
        {
            Item field_151 = player.field_151;

            if (field_151 != null)
            {
                if (field_151.type == 87 || field_151.type == 88)
                {
                    AvoidMissleAttack(50, player);
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

                    ovr025.draw_missile_attack(0x1e, 1, ovr033.PlayerMapPos(gbl.spell_target), ovr033.PlayerMapPos(player));

                    int damage = ovr024.roll_dice_save(4, 8);
                    bool saved = ovr024.RollSavingThrow(0, SaveVerseType.type3, gbl.spell_target);

                    ovr024.damage_person(saved, DamageOnSave.Half, damage, gbl.spell_target);

                    ovr024.remove_affect(affect, Affects.affect_79, player);
                    ovr024.remove_affect(null, Affects.ankheg_acid_attack, player);
                }
            }
        }


        internal static void AffectDracolichParalysis(Effect arg_0, object param, Player player) // spl_paralyze
        {
            gbl.spell_target = player.actions.target;

            if (ovr024.RollSavingThrow(0, 0, gbl.spell_target) == false)
            {
                ovr024.add_affect(false, 0xff, 0, Affects.paralyze, gbl.spell_target);

                ovr025.DisplayPlayerStatusString(true, 10, "is paralyzed", gbl.spell_target);
            }
        }


        internal static void sub_3C59D(Effect arg_0, object param, Player player)
        {
            gbl.damage_flags = DamageType.Acid;

            ovr024.damage_person(false, 0, ovr024.roll_dice_save(8, 2), player.actions.target);
        }


        internal static void AffectHalfElfResistance(Effect arg_0, object param, Player player) // sub_3C5D0
        {
            if (ovr024.roll_dice(100, 1) <= 30)
            {
                ProtectedIf(Affects.charm_person);
                ProtectedIf(Affects.sleep);
            }
        }


        internal static void sub_3C5F4(Effect arg_0, object param, Player player)
        {
            ProtectedIf(Affects.charm_person);
            ProtectedIf(Affects.sleep);
            ProtectedIf(Affects.paralyze);
            ProtectedIf(Affects.poisoned);

            if (gbl.saveVerseType != SaveVerseType.Poison)
            {
                gbl.savingThrowRoll = 100;
            }
        }


        internal static void AffectProtMagic(Effect arg_0, object param, Player player) // sub_3C623
        {
            if (gbl.current_affect != 0 ||
                (gbl.damage_flags & DamageType.Magic) != 0)
            {
                Protected();
            }
        }


        internal static void sub_3C643(Effect arg_0, object arg_2, Player player)
        {
            Item item;

            if (ovr025.sub_6906C(out item, gbl.player_ptr) == true &&
                item != null &&
                item.type == 28 && //ItemType.Quarrel
                item.namenum3 == 0x87)
            {
                player.health_status = Status.gone;
                player.in_combat = false;
                player.hit_point_current = 0;
                ovr024.sub_645AB(player);
                ovr024.CheckAffectsEffect(player, CheckType.Death);

                if (player.in_combat == true)
                {
                    ovr033.sub_74E6F(player);
                }
            }
        }


        internal static void do_items_affect(Effect remove_affect, object param, Player player) /* sub_3C6D3 */
        {
            Item item = (Item)param;

            gbl.applyItemAffect = false;

            if (remove_affect == Effect.Remove)
            {
                ovr024.remove_affect(null, item.affect_2, player);
            }
            else
            {
                ovr024.add_affect(true, 0xff, 0, item.affect_2, player);

                if (gbl.game_state != GameState.Combat)
                {
                    ovr013.CallAffectTable(Effect.Add, null, player, item.affect_2);
                }
            }
        }


        internal static void AffectDracolichA(Effect arg_0, object param, Player player) //sub_3C750
        {
            ProtectedIf(Affects.fear);
            ProtectedIf(Affects.ray_of_enfeeblement);
            ProtectedIf(Affects.feeblemind);

            if ((gbl.damage_flags & DamageType.Electricity) != 0)
            {
                Protected();
            }
        }


        internal static void AffectRangerVsGiant(Effect arg_0, object param, Player player) // sub_3C77C
        {
            gbl.spell_target = player.actions.target;

            if ((gbl.spell_target.field_14B & 8) != 0) // giant
            {
                gbl.damage += player.ranger_lvl;
            }
        }


        internal static void AffectProtElec(Effect arg_0, object param, Player player)//sub_3C7B5
        {
            if ((gbl.damage_flags & DamageType.Electricity) != 0)
            {
                Protected();
            }
        }


        internal static void AffectEntangle(Effect arg_0, object param, Player player) // sub_3C7CC
        {
            player.actions.move = 0;
        }


        internal static void sub_3C7E0(Effect arg_0, object param, Player player) // sub_3C7E0
        {
            Affect affect = (Affect)param;

            if (arg_0 == Effect.Add)
            {
                player.quick_fight = QuickFight.True;

                if (player.control_morale < Control.NPC_Base ||
                    player.control_morale == Control.PC_Berzerk)
                {
                    player.control_morale = Control.PC_Berzerk;
                }
                else
                {
                    player.control_morale = Control.NPC_Berzerk;
                }

                player.actions.target = null;

                var scl = ovr032.Rebuild_SortedCombatantList(gbl.mapToBackGroundTile, ovr033.PlayerMapSize(player), 0xff, 0xff, ovr033.PlayerMapPos(player));

                player.actions.target = scl[0].player;
                player.combat_team = player.actions.target.OppositeTeam();
            }
            else
            {
                if (player.control_morale == Control.PC_Berzerk)
                {
                    player.control_morale = 0;
                }

                player.combat_team = (CombatTeam)affect.affect_data;
            }
        }


        internal static void add_affect_19(Effect arg_0, object param, Player player)
        {
            ovr024.add_affect(false, 0xff, 0xff, Affects.invisibility, player);
        }


        internal static void PaladinCastCureRefresh(Effect add_remove, object param, Player player) // sub_3C8EF
        {
            if (add_remove == Effect.Remove)
            {
                player.paladinCuresLeft = (byte)((((player.paladin_old_lvl * ovr026.MulticlassExceedLastLevel(player)) + player.paladin_lvl - 1) / 5) + 1);
            }
        }


        internal static void AffectFear(Effect add_remove, object param, Player player) /* sub_3C932 */
        {
            if (add_remove == Effect.Remove)
            {
                if (player.control_morale == Control.PC_Berzerk)
                {
                    player.control_morale = Control.PC_Base;
                    player.quick_fight = QuickFight.False;
                }

                player.actions.fleeing = false;
            }
        }


        internal static void sub_3C975(Effect arg_0, object arg_2, Player target)
        {
            if (ovr025.getTargetRange(target, gbl.player_ptr) < 2)
            {
                int bkup_damage = gbl.damage;
                DamageType bkup_damage_flags = gbl.damage_flags;

                gbl.damage *= 2;
                gbl.damage_flags = DamageType.Magic;

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
                ovr024.RollSavingThrow(0, SaveVerseType.type4, gbl.spell_target) == false)
            {
                ovr024.KillPlayer("is dispelled", Status.gone, gbl.spell_target);

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

        static System.Collections.Generic.Dictionary<Affects, affectDelegate> affect_table;

        internal static void SetupAffectTables() // setup_spells2
        {
            affect_table = new System.Collections.Generic.Dictionary<Affects, affectDelegate>();

            affect_table.Add(Affects.bless, ovr013.Bless);
            affect_table.Add(Affects.cursed, ovr013.Curse);
            affect_table.Add(Affects.sticks_to_snakes, ovr013.SticksToSnakes);
            affect_table.Add(Affects.dispel_evil, ovr013.DispelEvil);
            affect_table.Add(Affects.detect_magic, ovr013.empty);
            affect_table.Add(Affects.affect_06, ovr013.sub_3A17A);
            affect_table.Add(Affects.faerie_fire, ovr013.FaerieFire);
            affect_table.Add(Affects.protection_from_evil, ovr013.affect_protect_evil);
            affect_table.Add(Affects.protection_from_good, ovr013.affect_protect_good);
            affect_table.Add(Affects.resist_cold, ovr013.affect_resist_cold);
            affect_table.Add(Affects.charm_person, ovr013.affect_charm_person);
            affect_table.Add(Affects.enlarge, ovr013.empty);
            affect_table.Add(Affects.reduce, ovr013.Suffocates);
            affect_table.Add(Affects.friends, ovr013.empty);
            affect_table.Add(Affects.poison_damage, ovr013.AffectPoisonDamage);
            affect_table.Add(Affects.read_magic, ovr013.empty);
            affect_table.Add(Affects.shield, ovr013.AffectShield);
            affect_table.Add(Affects.gnome_vs_man_sized_giant, ovr013.AffectGnomeVsManSizedGiant);
            affect_table.Add(Affects.find_traps, ovr013.empty);
            affect_table.Add(Affects.resist_fire, ovr013.AffectResistFire);
            affect_table.Add(Affects.silence_15_radius, ovr013.is_silenced1);
            affect_table.Add(Affects.slow_poison, ovr013.AffectSlowPoison);
            affect_table.Add(Affects.spiritual_hammer, ovr013.affect_spiritual_hammer);
            affect_table.Add(Affects.detect_invisibility, ovr013.empty);
            affect_table.Add(Affects.invisibility, ovr013.sub_3A6C6);
            affect_table.Add(Affects.dwarf_vs_orc, ovr013.AffectDwarfVsOrc);
            affect_table.Add(Affects.fumbling, ovr013.sub_3A071);
            affect_table.Add(Affects.mirror_image, ovr013.MirrorImage);
            affect_table.Add(Affects.ray_of_enfeeblement, ovr013.three_quarters_damage);
            affect_table.Add(Affects.stinking_cloud, ovr013.StinkingCloud);
            affect_table.Add(Affects.helpless, ovr013.sub_3A071);
            affect_table.Add(Affects.animate_dead, ovr013.sub_3A89E);
            affect_table.Add(Affects.blinded, ovr013.AffectBlinded);
            affect_table.Add(Affects.cause_disease_1, ovr013.AffectCauseDisease);
            affect_table.Add(Affects.confuse, ovr013.AffectConfuse);
            affect_table.Add(Affects.bestow_curse, ovr013.affect_curse);
            affect_table.Add(Affects.blink, ovr013.AffectBlink);
            affect_table.Add(Affects.strength, ovr013.empty);
            affect_table.Add(Affects.haste, ovr013.AffectHaste);
            affect_table.Add(Affects.affect_28, ovr013.sub_3AC1D);
            affect_table.Add(Affects.prot_from_normal_missiles, ovr013.AffectProtNormalMissles);
            affect_table.Add(Affects.slow, ovr013.AffectSlow);
            affect_table.Add(Affects.affect_2b, ovr013.weaken);
            affect_table.Add(Affects.cause_disease_2, ovr013.sub_3B0C2);
            affect_table.Add(Affects.prot_from_evil_10_radius, ovr013.affect_protect_evil);
            affect_table.Add(Affects.prot_from_good_10_radius, ovr013.affect_protect_good);
            affect_table.Add(Affects.dwarf_and_gnome_vs_giants, ovr013.AffectDwarfGnomeVsGiants);
            affect_table.Add(Affects.affect_30, ovr013.sub_3B1A2);
            affect_table.Add(Affects.prayer, ovr013.AffectPrayer);
            affect_table.Add(Affects.hot_fire_shield, ovr013.HotFireShield);
            affect_table.Add(Affects.snake_charm, ovr013.sub_3A071);
            affect_table.Add(Affects.paralyze, ovr013.sub_3A071);
            affect_table.Add(Affects.sleep, ovr013.sub_3A071);
            affect_table.Add(Affects.cold_fire_shield, ovr013.ColdFireShield);
            affect_table.Add(Affects.poisoned, ovr013.empty);
            affect_table.Add(Affects.item_invisibility, ovr013.sub_3B27B);
            affect_table.Add(Affects.affect_39, ovr014.engulfs);
            affect_table.Add(Affects.clear_movement, ovr013.AffectClearMovement);
            affect_table.Add(Affects.regenerate, ovr013.AffectRegenration);
            affect_table.Add(Affects.resist_normal_weapons, ovr013.AffectResistWeapons);
            affect_table.Add(Affects.fire_resist, ovr013.AffectFireResist);
            affect_table.Add(Affects.highConRegen, ovr013.AffectHighConRegen);
            affect_table.Add(Affects.minor_globe_of_invulnerability, ovr013.AffectMinorGlobeOfInvulnerability);
            affect_table.Add(Affects.poison_plus_0, ovr013.AffectPoisonPlus0);
            affect_table.Add(Affects.poison_plus_4, ovr013.AffectPoisonPlus4);
            affect_table.Add(Affects.poison_plus_2, ovr013.AffectPoisonPlus2);
            affect_table.Add(Affects.thri_kreen_paralyze, ovr013.ThriKreenParalyze);
            affect_table.Add(Affects.feeblemind, ovr013.AffectFeebleMind);
            affect_table.Add(Affects.invisible_to_animals, ovr013.AffectInvisToAnimals);
            affect_table.Add(Affects.poison_neg_2, ovr013.AffectPoisonNeg2);
            affect_table.Add(Affects.invisible, ovr013.AffectInvisible);
            affect_table.Add(Affects.camouflage, ovr013.AffectCamouflage);
            affect_table.Add(Affects.prot_drag_breath, ovr013.ProtDragonsBreath);
            affect_table.Add(Affects.affect_4a, ovr013.empty);
            affect_table.Add(Affects.weap_dragon_slayer, ovr013.AffectDragonSlayer);
            affect_table.Add(Affects.weap_frost_brand, ovr013.AffectFrostBrand);
            affect_table.Add(Affects.berserk, ovr013.AffectBerzerk);
            affect_table.Add(Affects.affect_4e, ovr013.sub_3B8D9);
            affect_table.Add(Affects.fireAttack_2d10, ovr013.MagicFireAttack_2d10);
            affect_table.Add(Affects.ankheg_acid_attack, ovr013.AnkhegAcidAttack);
            affect_table.Add(Affects.half_damage, ovr013.half_damage);
            affect_table.Add(Affects.resist_fire_and_cold, ovr013.AffectResistFireAndCold);
            affect_table.Add(Affects.paralizing_gaze, ovr023.AffectParalizingGaze);
            affect_table.Add(Affects.shambling_absorb_lightning, ovr013.AffectShamblerAbsorbLightning);
            affect_table.Add(Affects.affect_55, ovr013.sub_3BA14);
            affect_table.Add(Affects.spit_acid, ovr023.AffectSpitAcid);
            affect_table.Add(Affects.affect_57, ovr014.attack_or_kill);
            affect_table.Add(Affects.breath_elec, ovr023.DragonBreathElec);
            affect_table.Add(Affects.displace, ovr013.AffectDisplace);
            affect_table.Add(Affects.breath_acid, ovr023.DragonBreathAcid);
            affect_table.Add(Affects.affect_5b, ovr013.sub_3BAB9);
            affect_table.Add(Affects.affect_5c, ovr013.empty);
            affect_table.Add(Affects.affect_5d, ovr013.half_fire_damage);
            affect_table.Add(Affects.affect_5e, ovr013.sub_3BDB2);
            affect_table.Add(Affects.affect_5F, ovr013.sub_3BE06);
            affect_table.Add(Affects.owlbear_hug_check, ovr014.AffectOwlbearHugAttackCheck);
            affect_table.Add(Affects.con_saving_bonus, ovr013.con_saving_bonus);
            affect_table.Add(Affects.regen_3_hp, ovr013.AffectRegen3Hp);
            affect_table.Add(Affects.affect_63, ovr013.sub_3BEE8);
            affect_table.Add(Affects.troll_fire_or_acid, ovr013.AffectTrollFireOrAcid);
            affect_table.Add(Affects.troll_regen, ovr013.AffectTrollRegenerate);
            affect_table.Add(Affects.TrollRegen, ovr013.AffectTrollRegen);
            affect_table.Add(Affects.salamander_heat_damage, ovr013.AffectSalamanderHeatDamage);
            affect_table.Add(Affects.thri_kreen_dodge_missile, ovr013.sub_3C0DA);
            affect_table.Add(Affects.affect_69, ovr013.sub_3C14F);
            affect_table.Add(Affects.affect_6a, ovr013.sub_3C15D);
            affect_table.Add(Affects.elf_resist_sleep, ovr013.AffectElfRisistSleep);
            affect_table.Add(Affects.protect_charm_sleep, ovr013.AffectProtCharmSleep);
            affect_table.Add(Affects.affect_6d, ovr013.sub_3C1A4);
            affect_table.Add(Affects.affect_6e, ovr013.sub_3C1B2);
            affect_table.Add(Affects.affect_6f, ovr013.sub_3C1C9);
            affect_table.Add(Affects.immune_to_fire, ovr013.AffectImmuneToFire);
            affect_table.Add(Affects.affect_71, ovr013.sub_3C201);
            affect_table.Add(Affects.affect_72, ovr013.sub_3C246);
            affect_table.Add(Affects.affect_73, ovr013.sub_3C260);
            affect_table.Add(Affects.affect_74, ovr013.half_damage_if_weap_magic);
            affect_table.Add(Affects.affect_75, ovr013.sub_3C2F9);
            affect_table.Add(Affects.affect_76, ovr013.AffectProtCold);
            affect_table.Add(Affects.affect_77, ovr013.AffectProtNonMagicWeapons);
            affect_table.Add(Affects.affect_78, ovr013.sub_3C3A2);
            affect_table.Add(Affects.affect_79, ovr013.sub_3C3F6);
            affect_table.Add(Affects.dracolich_paralysis, ovr013.AffectDracolichParalysis);
            affect_table.Add(Affects.affect_7b, ovr013.sub_3C59D);
            affect_table.Add(Affects.halfelf_resistance, ovr013.AffectHalfElfResistance);
            affect_table.Add(Affects.affect_7d, ovr013.sub_3C5F4);
            affect_table.Add(Affects.affect_7e, ovr023.cast_gaze_paralyze);
            affect_table.Add(Affects.affect_7f, ovr013.empty);
            affect_table.Add(Affects.affect_80, ovr023.DragonBreathFire);
            affect_table.Add(Affects.protect_magic, ovr013.AffectProtMagic);
            affect_table.Add(Affects.affect_82, ovr013.sub_3C643);
            affect_table.Add(Affects.cast_breath_fire, ovr023.cast_breath_fire);
            affect_table.Add(Affects.cast_throw_lightening, ovr023.cast_throw_lightening);
            affect_table.Add(Affects.affect_85, ovr013.AffectDracolichA);
            affect_table.Add(Affects.ranger_vs_giant, ovr013.AffectRangerVsGiant);
            affect_table.Add(Affects.protect_elec, ovr013.AffectProtElec);
            affect_table.Add(Affects.entangle, ovr013.AffectEntangle);
            affect_table.Add(Affects.affect_89, ovr013.sub_3C7E0);
            affect_table.Add(Affects.affect_8a, ovr013.add_affect_19);
            affect_table.Add(Affects.affect_8b, ovr014.sub_425C6);
            affect_table.Add(Affects.paladinDailyHealCast, ovr013.empty);
            affect_table.Add(Affects.paladinDailyCureRefresh, ovr013.PaladinCastCureRefresh);
            affect_table.Add(Affects.fear, ovr013.AffectFear);
            affect_table.Add(Affects.affect_8f, ovr013.sub_3C975);
            affect_table.Add(Affects.owlbear_hug_round_attack, ovr014.AffectOwlbearHugRoundAttack);
            affect_table.Add(Affects.sp_dispel_evil, ovr013.sp_dispel_evil);
            affect_table.Add(Affects.strenght_spell, ovr013.empty);
            affect_table.Add(Affects.do_items_affect, ovr013.do_items_affect);
        }

        internal static void CallAffectTable(Effect add_remove, object parameter, Player player, Affects affect) /* sub_630C7 */
        {
            if (gbl.applyItemAffect == true)
            {
                affect = Affects.do_items_affect;
            }
            
            affectDelegate func;
            if (affect_table.TryGetValue(affect, out func))
            {
                func(add_remove, parameter, player);
            }
        }
    }
}
