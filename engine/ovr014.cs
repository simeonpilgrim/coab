using Classes;
using Classes.Combat;

namespace engine
{
    class ovr014
    {
        internal static void CalculateInitiative(Player player) // sub_3E000
        {
            Action action = player.actions;

            action.spell_id = 0;
            action.can_cast = true;
            action.can_use = true;
            action.field_8 = false;
            action.attackIdx = 2;

            reclac_attacks(player);
            gbl.halfActionsLeft = player.baseHalfMoves;

            gbl.resetMovesLeft = false;

            ovr024.CheckAffectsEffect(player, CheckType.Movement);

            player.attack2_AttacksLeft = (byte)ThisRoundActionCount(gbl.halfActionsLeft);

            action.maxSweapTargets = player.attackLevel;

            if (player.in_combat == true)
            {
                action.delay = (sbyte)(ovr024.roll_dice(6, 1) + ovr025.DexReactionAdj(player));

                if (action.delay < 1)
                {
                    action.delay = 1;
                }

                if ((((int)player.combat_team + 1) & gbl.area2_ptr.field_596) != 0)
                {
                    action.delay -= 6;
                }

                if (action.delay < 0 ||
                    action.delay > 20)
                {
                    action.delay = 0;
                }
            }
            else
            {
                action.delay = 0;
            }

            player.actions.move = CalcMoves(player);
        }


        internal static int CalcMoves(Player player) // sub_3E124
        {
            int moves = player.movement;

            if (player.in_combat == false)
            {
                moves += gbl.area2_ptr.field_6E4;
            }

            if (moves < 1 || moves > 96)
            {
                moves = 1;
            }

            gbl.halfActionsLeft = moves * 2;

            gbl.resetMovesLeft = true;

            ovr024.CheckAffectsEffect(player, CheckType.Movement);

            gbl.resetMovesLeft = false;

            return gbl.halfActionsLeft;
        }


        static void sub_3E192(int index, Player target, Player attacker)
        {
            gbl.damage = ovr024.roll_dice_save(attacker.attackDiceSize(index), attacker.attackDiceCount(index));
            gbl.damage += attacker.attackDamageBonus(index);

            if (gbl.damage < 0)
            {
                gbl.damage = 0;
            }

            if (CanBackStabTarget(target, attacker) == true)
            {
                gbl.damage *= ((attacker.SkillLevel(SkillType.Thief) - 1) / 4) + 2;
            }

            gbl.damage_flags = 0;
            ovr024.CheckAffectsEffect(attacker, CheckType.SpecialAttacks);
            ovr024.CheckAffectsEffect(target, CheckType.Type_5);
        }


        enum AttackType
        {
            Normal = 0,
            Behind = 1,
            Backstab = 2,
            Slay = 3
        }

        static void DisplayAttackMessage(bool attackHits, int attackDamge, int actualDamage, AttackType attack, Player target, Player attacker) /* backstab */
        {
            string text;

            if (attack == AttackType.Backstab)
            {
                text = "-Backstabs-";
            }
            else if (attack == AttackType.Slay)
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

            if (attack == AttackType.Behind)
            {
                text = "(from behind) ";
            }
            else
            {
                text = string.Empty;
            }

            if (attackHits == true)
            {
                if (attack == AttackType.Slay)
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
                seg041.press_any_key(text, true, 10, line + 3, 0x26, line, 0x17);
            }

            line = gbl.textYCol + 1;

            seg041.GameDelay();

            if (actualDamage > 0)
            {
                ovr024.TryLooseSpell(target);
            }

            if (target.in_combat == false)
            {
                ovr025.DisplayPlayerStatusString(false, line, "goes down", target);
                line += 2;

                if (target.health_status == Status.dying)
                {
                    seg041.displayString("and is Dying", 0, 10, line, 0x17);
                }

                if (target.health_status == Status.dead ||
                    target.health_status == Status.stoned ||
                    target.health_status == Status.gone )
                {
                    ovr025.DisplayPlayerStatusString(false, line, "is killed", target);
                }

                line += 2;

                ovr024.RemoveCombatAffects(target);

                ovr024.CheckAffectsEffect(target, CheckType.Death);

                if (target.in_combat == false)
                {
                    ovr033.CombatantKilled(target);
                }
                else
                {
                    seg041.GameDelay();
                }
            }

            ovr025.ClearPlayerTextArea();
        }


        static void move_step_into_attack(Player target) /* sub_3E65D */
        {
            var nearTargets = ovr025.BuildNearTargets(1, target);

            if (target.in_combat)
            {
                foreach (var cpi in nearTargets)
                {
                    Player attacker = cpi.player;

                    if (attacker.actions.guarding == true &&
                        attacker.IsHeld() == false)
                    {
                        ovr033.redrawCombatArea(8, 2, ovr033.PlayerMapPos(target));

                        attacker.actions.guarding = false;

                        RecalcAttacksReceived(target, attacker);

                        AttackTarget(null, 0, target, attacker);
                    }
                }
            }
        }


        internal static void sub_3E748(int direction, Player player)
        {
            int player_index = ovr033.GetPlayerIndex(player);

            Point oldPos = gbl.CombatMap[player_index].pos;
            Point newPos = oldPos + gbl.MapDirectionDelta[direction];

            // TODO does this solve more problems than it causes? Regarding AI flee
            if (newPos.MapInBounds() == false)
            {
                return;
            }

            int costToMove = 0;
            if ((direction & 0x01) != 0)
            {
                // Diagonal walking...
                costToMove = gbl.BackGroundTiles[gbl.mapToBackGroundTile[newPos]].move_cost * 3;
            }
            else
            {
                costToMove = gbl.BackGroundTiles[gbl.mapToBackGroundTile[newPos]].move_cost * 2;
            }

            if (costToMove > player.actions.move)
            {
                player.actions.move = 0;
            }
            else
            {
                player.actions.move -= costToMove;
            }

            byte radius = 1;

            if (player.quick_fight == QuickFight.True)
            {
                radius = 3;

                if (ovr033.CoordOnScreen(newPos - gbl.mapToBackGroundTile.mapScreenTopLeft) == false &&
                    gbl.focusCombatAreaOnPlayer == true)
                {
                    ovr033.redrawCombatArea(8, 2, oldPos);
                }
            }

            if (gbl.focusCombatAreaOnPlayer == true)
            {
                ovr033.RedrawPlayerBackground(player_index);
            }

            gbl.CombatMap[player_index].pos = newPos;

            ovr033.setup_mapToPlayerIndex_and_playerScreen();

            if (gbl.focusCombatAreaOnPlayer == true)
            {
                ovr033.redrawCombatArea(8, radius, newPos);
            }

            player.actions.AttacksReceived = 0;
            player.actions.directionChanges = 0;
            seg044.PlaySound(Sound.sound_a);

            move_step_into_attack(player);

            if (player.in_combat == false ||
                player.IsHeld() == true)
            {
                player.actions.move = 0;
            }
        }


        internal static void move_step_away_attack(int direction, Player player) /* sub_3E954 */
        {
            var originAttackers = ovr025.BuildNearTargets(1, player);

            if (originAttackers.Count == 0)
            {
                return;
            }

            var combatmap = gbl.CombatMap[ovr033.GetPlayerIndex(player)];

            // move to destination position
            combatmap.pos += gbl.MapDirectionDelta[direction];

            var destAttackers = ovr025.BuildNearTargets(1, player);

            // move back to original position
            combatmap.pos -= gbl.MapDirectionDelta[direction];

            // remove attackers from both locations
            foreach (var cpiB in destAttackers)
            {
                originAttackers.RemoveAll(cpiA => cpiA.player == cpiB.player);
            }

            if (player.in_combat == false)
            {
                //what the heck are we doing here then?
                // and why is this test not earlier in the function.
                //throw new System.NotSupportedException();
                return;
            }

            foreach (var cpiA in originAttackers)
            {
                gbl.display_hitpoints_ac = true;
                gbl.focusCombatAreaOnPlayer = true;
                bool found = false;

                Player attacker = cpiA.player;

                if (attacker.IsHeld() == false &&
                    CanSeeTargetA(player, attacker) == true &&
                    attacker.HasAffect(Affects.weap_dragon_slayer) == false &&
                    attacker.HasAffect(Affects.affect_4a) == false)
                {
                    int end_dir = attacker.actions.direction + 10;

                    for (int tmpDir = attacker.actions.direction + 6; tmpDir <= end_dir; tmpDir++)
                    {
                        if (found == false)
                        {
                            if (attacker.actions.delay > 0 ||
                                attacker.actions.AttacksReceived == 0 ||
                                ovr032.CanSeeCombatant(tmpDir % 8, ovr033.PlayerMapPos(player), ovr033.PlayerMapPos(attacker)) == true)
                            {
                                byte attackIndex = 1;
                                if (attacker.attacksCount == 0)
                                {
                                    attackIndex = 2;
                                }

                                if (attacker.attack1_AttacksLeft > 0)
                                {
                                    attackIndex = 1;
                                }

                                if (attacker.attack2_AttacksLeft > 0)
                                {
                                    attackIndex = 2;
                                }

                                if (attacker.AttacksLeft(attackIndex) == 0)
                                {
                                    attacker.AttacksLeftSet(attackIndex, 1);
                                }

                                attacker.actions.attackIdx = attackIndex;

                                Player backupTarget = attacker.actions.target;

                                AttackTarget(null, 1, player, attacker);
                                found = true;

                                attacker.actions.target = backupTarget;

                                if (player.in_combat == true)
                                {
                                    gbl.display_hitpoints_ac = true;
                                    ovr025.CombatDisplayPlayerSummary(player);
                                }
                            }
                        }
                    }
                }
            }
        }



        internal static void flee_battle(Player player)
        {
            bool gets_away = false;

            if (ovr025.BuildNearTargets(0xff, player).Count == 0)
            {
                gets_away = true;
            }
            else
            {
                int var_4 = CalcMoves(player) / 2;
                int var_3 = MaxOppositionMoves(player);

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
                ovr024.RemoveFromCombat("Got Away", Status.running, player);
            }
            else
            {
                ovr025.string_print01("Escape is blocked");
            }

            ovr025.clear_actions(player);
        }


        internal static void reclac_attacks(Player player) // sub_3EDD4
        {
            bool foundRanged = false;
            Item rangedItem = null;
            int origAttacks = player.attack1_AttacksLeft;
            player.attack1_AttacksLeft = player.attacksCount;

            if (ovr025.is_weapon_ranged(player) == true &&
                ovr025.GetCurrentAttackItem(out rangedItem, player) == true)
            {
                foundRanged = true;
                int numAttacks = gbl.ItemDataTable[player.activeItems.primaryWeapon.type].numberAttacks;

                if (numAttacks < 2)
                {
                    numAttacks = 2;
                }

                gbl.halfActionsLeft = numAttacks;
            }
            else
            {
                gbl.halfActionsLeft = player.attack1_AttacksLeft;
            }

            gbl.resetMovesLeft = false;
            ovr024.CheckAffectsEffect(player, CheckType.Movement);

            int attacks = ThisRoundActionCount(gbl.halfActionsLeft);

            if (foundRanged == true &&
                rangedItem != null)
            {
                int rangedAmmo = 1;
                if (rangedItem.count > rangedAmmo)
                {
                    rangedAmmo = rangedItem.count;
                }

                if (rangedAmmo < attacks &&
                    rangedItem.count > 0)
                {
                    attacks = rangedAmmo;
                }
            }

            if (player.actions.field_8 == false ||
                attacks < origAttacks ||
                (player.actions.field_8 == true &&
                  attacks < (origAttacks * 2) &&
                  foundRanged == false))
            {
                player.attack1_AttacksLeft = (byte)attacks;
            }
        }


        internal static int ThisRoundActionCount(int halfActionsLeft) // sub_3EF0D
        {
            if ((gbl.combat_round & 1) == 1)
            {
                halfActionsLeft++;
            }

            return halfActionsLeft / 2;
        }


        internal static bool TrySweepAttack(Player target, Player attacker) // sub_3EF3D
        {
            if (attacker.attack1_AttacksLeft < attacker.actions.maxSweapTargets &&
                target.HitDice == 0 &&
                ovr025.getTargetRange(target, attacker) == 1)
            {
                var nearTargets = ovr025.BuildNearTargets(1, attacker);

                var targetepi = nearTargets.Find(epi => epi.player == target);
                int sweepableCount = nearTargets.FindAll(epi => epi.player.HitDice == 0).Count;

                if (sweepableCount > attacker.attack1_AttacksLeft)
                {
                    if (sweepableCount > attacker.actions.maxSweapTargets)
                    {
                        sweepableCount = attacker.actions.maxSweapTargets;
                    }

                    ovr025.DisplayPlayerStatusString(true, 10, "sweeps", attacker);

                    nearTargets.Remove(targetepi);
                    nearTargets.Insert(0, targetepi);

                    foreach (var sweepepi in nearTargets.FindAll(e => e.player.hitBonus == 0).GetRange(0, sweepableCount))
                    {
                        var sweeptarget = sweepepi.player;
                        RecalcAttacksReceived(sweeptarget, attacker);

                        attacker.attack1_AttacksLeft = 1;

                        AttackTarget(null, 0, sweeptarget, attacker);
                    }

                    return true;
                }
            }

            return false;
        }


        internal static bool CanSeeTargetA(Player targetA, Player targetB) //sub_3F143 
        {
            if (targetA != null)
            {
                if (targetB == targetA)
                {
                    return true;
                }
                else
                {
                    gbl.targetInvisible = false;

                    ovr024.CheckAffectsEffect(targetA, CheckType.Visibility);

                    if (gbl.targetInvisible == false)
                    {
                        var old_target = targetB.actions.target;

                        targetB.actions.target = targetA;

                        ovr024.CheckAffectsEffect(targetB, CheckType.None);

                        targetB.actions.target = old_target;
                    }

                    return (gbl.targetInvisible == false);
                }
            }

            return false;
        }

        static sbyte[] /*seg600:0369*/ unk_16679 = { 0, 
        17, 17,  0,  0,  1, 17,  0,  0, 32, 32, 
        10,  7,  4,  1,  1,  0,  0, -1, -1, -1 };

        internal static void turns_undead(Player player)
        {
            ovr025.DisplayPlayerStatusString(false, 10, "turns undead...", player);
            ovr027.ClearPromptArea();
            seg041.GameDelay();

            bool any_turned = false;
            byte var_6 = 0;

            player.actions.hasTurnedUndead = true;

            byte var_3 = 6;
            int var_2 = ovr024.roll_dice(12, 1);
            int var_1 = ovr024.roll_dice(20, 1);

            int clericLvl = player.SkillLevel(SkillType.Cleric);

            byte var_B;

            if (clericLvl >= 1 && clericLvl <= 8)
            {
                var_B = player.cleric_lvl;
            }
            else if (clericLvl >= 9 && clericLvl <= 13)
            {
                var_B = 9;
            }
            else
            {
                var_B = 10;
            }

            Player target;
            while (FindLowestE9Target(out target, player) == true && var_2 > 0 && var_6 == 0)
            {
                int var_4 = unk_16679[(target.field_E9 * 10) + var_B];

                if (var_1 >= System.Math.Abs(var_4))
                {
                    any_turned = true;

                    ovr033.RedrawCombatIfFocusOn(false, 3, target);
                    gbl.display_hitpoints_ac = true;
                    ovr025.CombatDisplayPlayerSummary(target);

                    if (var_4 > 0)
                    {
                        target.actions.fleeing = true;
                        ovr025.MagicAttackDisplay("is turned", true, target);
                    }
                    else
                    {
                        ovr025.DisplayPlayerStatusString(false, 10, "Is destroyed", target);
                        ovr033.CombatantKilled(target);
                        target.health_status = Status.gone;
                        target.in_combat = false;
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

            if (any_turned == false)
            {
                ovr025.string_print01("Nothing Happens...");
            }

            ovr025.CountCombatTeamMembers();
            ovr025.clear_actions(player);

            ovr025.ClearPlayerTextArea();
        }


        internal static bool FindLowestE9Target(out Player output, Player player) /* sub_3F433 */
        {
            output = null;

            byte minE9 = 13;

            var nearTargets = ovr025.BuildNearTargets(0xff, player);
            bool result = false;

            foreach (var epi in nearTargets)
            {
                Player target = epi.player;

                if (target.actions.fleeing == false &&
                    target.field_E9 > 0 &&
                    target.field_E9 < minE9)
                {
                    minE9 = target.field_E9;
                    output = target;
                    result = true;
                }
            }

            return result;
        }


        internal static bool AttackTarget01(Item item, int arg_8, Player target, Player attacker) // sub_3F4EB
        {
            int target_ac;
            bool turnComplete = true;
            bool BehindAttack = arg_8 != 0;
            turnComplete = false;
            gbl.bytes_1D2C9[1] = 0;
            gbl.bytes_1D2C9[2] = 0;
            gbl.bytes_1D900[1] = 0;
            gbl.bytes_1D900[2] = 0;
            bool var_11 = false;
            bool targetNotInCombat = false;
            gbl.damage = 0;

            attacker.actions.field_8 = true;

            if (target.IsHeld() == true)
            {
                seg044.PlaySound(Sound.sound_attackHeld);

                while (attacker.AttacksLeft(attacker.actions.attackIdx) == 0)
                {
                    attacker.actions.attackIdx--;
                }

                gbl.bytes_1D900[attacker.actions.attackIdx] += 1;

                DisplayAttackMessage(true, 1, target.hit_point_current + 5, AttackType.Slay, target, attacker);
                ovr024.remove_invisibility(attacker);

                attacker.attack1_AttacksLeft = 0;
                attacker.attack2_AttacksLeft = 0;

                var_11 = true;
                turnComplete = true;
            }
            else
            {
                if (attacker.activeItems.primaryWeapon != null &&
                    (target.field_DE > 0x80 || (target.field_DE & 7) > 1))
                {
                    ItemData itemData = gbl.ItemDataTable[attacker.activeItems.primaryWeapon.type];

                    attacker.attack1_DiceCount = itemData.diceCountLarge;
                    attacker.attack1_DiceSize = itemData.diceSizeLarge;
                    attacker.attack1_DamageBonus -= itemData.bonusNormal;
                    attacker.attack1_DamageBonus += itemData.bonusLarge;
                }

                ovr025.reclac_player_values(target);
                ovr024.CheckAffectsEffect(target, CheckType.Type_11);

                if (CanBackStabTarget(target, attacker) == true)
                {
                    target_ac = target.ac_behind - 4;
                }
                else
                {
                    if (target.actions.AttacksReceived > 1 &&
                        getTargetDirection(target, attacker) == target.actions.direction &&
                        target.actions.directionChanges > 4)
                    {
                        BehindAttack = true;
                    }

                    if (BehindAttack == true)
                    {
                        target_ac = target.ac_behind;
                    }
                    else
                    {
                        target_ac = target.ac;
                    }
                }

                target_ac += RangedDefenseBonus(target, attacker);
                AttackType attack_type = AttackType.Normal;
                if (BehindAttack == true)
                {
                    attack_type = AttackType.Behind;
                }

                if (CanBackStabTarget(target, attacker) == true)
                {
                    attack_type = AttackType.Backstab;
                }

                for (int attackIdx = attacker.actions.attackIdx; attackIdx >= 1; attackIdx--)
                {
                    while (attacker.AttacksLeft(attackIdx) > 0 &&
                        targetNotInCombat == false)
                    {
                        attacker.AttacksLeftDec(attackIdx);
                        attacker.actions.attackIdx = attackIdx;

                        gbl.bytes_1D900[attackIdx] += 1;

                        if (ovr024.PC_CanHitTarget(target_ac, target, attacker) ||
                            target.IsHeld() == true)
                        {
                            gbl.bytes_1D2C9[attackIdx] += 1;

                            seg044.PlaySound(Sound.sound_attackHeld);
                            var_11 = true;
                            sub_3E192(attackIdx, target, attacker);
                            DisplayAttackMessage(true, gbl.damage, gbl.damage, attack_type, target, attacker);

                            if (target.in_combat == true)
                            {
                                ovr024.CheckAffectsEffect(attacker, (CheckType)attackIdx + 1);
                            }

                            if (target.in_combat == false)
                            {
                                targetNotInCombat = true;
                            }

                            if (attacker.in_combat == false)
                            {
                                attacker.AttacksLeftSet(attackIdx, 0);
                            }
                        }
                    }
                }

                if (item != null &&
                    item.count == 0 &&
                    item.type == ItemType.DartOfHornetsNest)
                {
                    attacker.attack1_AttacksLeft = 0;
                    attacker.attack2_AttacksLeft = 0;
                }

                if (var_11 == false)
                {
                    seg044.PlaySound(Sound.sound_9);
                    DisplayAttackMessage(false, 0, 0, attack_type, target, attacker);
                }

                turnComplete = true;
                if (attacker.attack1_AttacksLeft > 0 ||
                    attacker.attack2_AttacksLeft > 0)
                {
                    turnComplete = false;
                }

                attacker.actions.maxSweapTargets = 0;
            }

            if (attacker.in_combat == false)
            {
                turnComplete = true;
            }

            if (turnComplete == true)
            {
                ovr025.clear_actions(attacker);
            }

            return turnComplete;
        }


        internal static void RecalcAttacksReceived(Player target, Player attacker) // sub_3F94D
        {
            target.actions.AttacksReceived++;

            int targetDir = getTargetDirection(attacker, target);

            int dirDiff = ((targetDir - target.actions.direction) + 8) % 8;

            if (dirDiff > 4)
            {
                dirDiff = 8 - dirDiff;
            }

            target.actions.directionChanges = (target.actions.directionChanges + dirDiff) % 8;
        }


        internal static bool AttackTarget(Item rangedWeapon, int attackType, Player target, Player attacker) // sub_3F9DB
        {
            int dir = 0;

            gbl.focusCombatAreaOnPlayer = true;
            gbl.display_hitpoints_ac = true;

            gbl.combat_round_no_action_limit = gbl.combat_round + gbl.combat_round_no_action_value;

            if (target.actions.AttacksReceived < 2 && attackType == 0)
            {
                dir = getTargetDirection(attacker, target);

                target.actions.direction = (dir + 4) % 8;
            }
            else if (ovr033.PlayerOnScreen(false, target) == true)
            {
                dir = target.actions.direction;

                if (attackType == 0)
                {
                    target.actions.direction = (dir + 4) % 8;
                }
            }

            if (ovr033.PlayerOnScreen(false, target) == true)
            {
                ovr033.draw_74B3F(false, Icon.Normal, dir, target);
            }

            dir = getTargetDirection(target, attacker);
            ovr025.CombatDisplayPlayerSummary(attacker);

            ovr033.draw_74B3F(false, Icon.Attack, dir, attacker);

            attacker.actions.target = target;

            seg049.SysDelay(100);

            if (rangedWeapon != null)
            {
                DrawRangedAttack(rangedWeapon, target, attacker);
            }

            if (attacker.activeItems.primaryWeapon != null && 
                (attacker.activeItems.primaryWeapon.type == ItemType.Sling ||
                attacker.activeItems.primaryWeapon.type == ItemType.StaffSling))
            {
                DrawRangedAttack(attacker.activeItems.primaryWeapon, target, attacker);
            }

            bool turnComplete = true;

            if (attacker.attack1_AttacksLeft > 0 ||
                attacker.attack2_AttacksLeft > 0)
            {
                Player player_bkup = gbl.SelectedPlayer;

                gbl.SelectedPlayer = attacker;

                turnComplete = AttackTarget01(rangedWeapon, attackType, target, attacker);

                if (rangedWeapon != null)
                {
                    if (rangedWeapon.count > 0)
                    {
                        rangedWeapon.count = gbl.bytes_1D900[1];
                    }

                    if (rangedWeapon.count == 0)
                    {
                        if (ovr025.is_weapon_ranged_melee(attacker) == true &&
                            rangedWeapon.affect_3 != Affects.affect_89)
                        {
                            Item new_item = rangedWeapon.ShallowClone();
                            new_item.readied = false;

                            gbl.items_pointer.Add(new_item);

                            ovr025.lose_item(rangedWeapon, attacker);
                            gbl.item_ptr = new_item;
                        }
                        else
                        {
                            ovr025.lose_item(rangedWeapon, attacker);
                        }
                    }
                }

                ovr025.reclac_player_values(attacker);
                gbl.SelectedPlayer = player_bkup;
            }

            if (turnComplete == true)
            {
                ovr025.clear_actions(attacker);
            }

            if (ovr033.PlayerOnScreen(false, attacker) == true)
            {
                ovr033.draw_74B3F(true, Icon.Attack, attacker.actions.direction, attacker);
                ovr033.draw_74B3F(false, Icon.Normal, attacker.actions.direction, attacker);
            }

            return turnComplete;
        }


        internal static int RangedDefenseBonus(Player target, Player attacker) /* sub_3FCED */
        {
            if (ovr025.is_weapon_ranged(attacker) == true)
            {
                int range = ovr025.getTargetRange(target, attacker);

                int oneThirdRange = (gbl.ItemDataTable[attacker.activeItems.primaryWeapon.type].range - 1) / 3;
                int acAdjustment = 0;

                if (range > oneThirdRange)
                {
                    range -= oneThirdRange;
                    acAdjustment += 2;
                }

                if (range > oneThirdRange)
                {
                    range -= oneThirdRange;
                    acAdjustment += 3;
                }

                return acAdjustment;
            }
            else
            {
                return 0;
            }
        }

        internal static bool find_healing_target(out Player target, Player healer) /* sub_3FDFE */
        {
            Player lowest_target = null;
            int lowest_hp = 0x0FF;
            Struct_1D183 var_8 = null;

            for (int dir = 0; dir <= 8; dir++)
            {
                var map = gbl.MapDirectionDelta[dir] + ovr033.PlayerMapPos(healer);

                int ground_tile;
                int player_index;
                ovr033.AtMapXY(out ground_tile, out player_index, map);

                if (player_index > 0)
                {
                    target = gbl.player_array[player_index];

                    if (target.combat_team == healer.combat_team &&
                        target.hit_point_current < target.hit_point_max)
                    {
                        if (target.hit_point_current < lowest_hp ||
                            (target == healer && target.hit_point_current < (target.hit_point_max / 2)))
                        {
                            lowest_target = target;
                            lowest_hp = target.hit_point_current;
                        }
                    }
                }
                else if (ground_tile == 0x1F)
                {
                    var_8 = gbl.downedPlayers.FindLast(cell => cell.target != null && cell.map == map &&
                        cell.target.health_status != Status.tempgone && cell.target.health_status != Status.running &&
                        cell.target.health_status != Status.unconscious);
                }
            }

            if (lowest_hp < 8 ||
                var_8 == null)
            {
                target = lowest_target;
            }
            else
            {
                target = var_8.target;
            }

            bool target_found = (target != null);

            return target_found;
        }

        static Affects[] unk_18ADB = { Affects.bless, Affects.snake_charm, Affects.paralyze, Affects.sleep, Affects.helpless }; // seg600:27CB first is filler (off by 1)

        internal static bool sub_4001C(Struct_1D183 arg_0, bool canTargetEmptyGround, QuickFight quick_fight, int spellId)
        {
            bool var_2 = false;
            if (quick_fight == QuickFight.False)
            {
                bool allowTarget = spellId != 0x53;

                var_2 = aim_menu(arg_0, allowTarget, canTargetEmptyGround, false, ovr023.SpellRange(spellId), gbl.SelectedPlayer);
                gbl.SelectedPlayer.actions.target = arg_0.target;
            }
            else if (gbl.spellCastingTable[spellId].field_E == 0)
            {
                arg_0.target = gbl.SelectedPlayer;

                if (spellId != 3 || find_healing_target(out arg_0.target, gbl.SelectedPlayer))
                {
                    arg_0.map = ovr033.PlayerMapPos(arg_0.target);
                    var_2 = true;
                }
            }
            else
            {
                int var_9 = 1;

                while (var_9 > 0 &&
                        var_2 == false)
                {
                    bool var_3 = true;

                    if (find_target(true, 0, ovr023.SpellRange(spellId), gbl.SelectedPlayer) == true)
                    {
                        Player target = gbl.SelectedPlayer.actions.target;

                        if (target.IsHeld() == true)
                        {
                            for (int i = 1; i <= 4; i++)
                            {
                                if (gbl.spellCastingTable[spellId].affect_id == unk_18ADB[i])
                                {
                                    var_3 = false;
                                }
                            }
                        }

                        if (var_3 == true)
                        {
                            arg_0.target = gbl.SelectedPlayer.actions.target;
                            arg_0.map = ovr033.PlayerMapPos(arg_0.target);
                            var_2 = true;
                        }
                    }

                    var_9 -= 1;
                }
            }


            if (var_2 == true)
            {
                gbl.targetPos = arg_0.map;
            }
            else
            {
                arg_0.Clear();
            }

            return var_2;
        }

        internal static bool target(QuickFight quick_fight, int spellId)
        {
            Struct_1D183 var_C = new Struct_1D183();

            bool castSpell = true;
            gbl.spellTargets.Clear();
            gbl.byte_1D2C7 = false;

            gbl.targetPos = ovr033.PlayerMapPos(gbl.SelectedPlayer);

            int tmp1 = gbl.spellCastingTable[spellId].field_6 & 0x0F;

            if (tmp1 == 0)
            {
                gbl.spellTargets.Clear();
                gbl.spellTargets.Add(gbl.SelectedPlayer);
            }
            else if (tmp1 == 5)
            {
                int var_5 = 0;
                gbl.spellTargets.Clear();

                int var_4;

                if (spellId == 0x4F)
                {
                    var_4 = ovr025.spellMaxTargetCount(0x4F);
                }
                else
                {
                    var_4 = ovr024.roll_dice(4, 2);
                }

                bool stop_loop = false;

                do
                {
                    if (sub_4001C(var_C, false, quick_fight, spellId) == true)
                    {
                        bool found = gbl.spellTargets.Exists(st => st == var_C.target);

                        if (found == false)
                        {
                            Player target = var_C.target;
                            gbl.spellTargets.Add(target);

                            gbl.targetPos = ovr033.PlayerMapPos(var_C.target);

                            if (spellId != 0x4f)
                            {
                                byte hitDice = target.HitDice;

                                if (hitDice == 0 || hitDice == 1)
                                {
                                    var_5 += 1;
                                }
                                else if (hitDice == 2)
                                {
                                    var_5 += 2;
                                }
                                else if (hitDice == 3)
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
                                byte al = target.field_DE;

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

                            if (gbl.spellTargets.Count > 0 && var_5 > var_4)
                            {
                                stop_loop = true;
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

                        ovr033.RedrawPosition(ovr033.PlayerMapPos(var_C.target));
                    }
                    else
                    {
                        stop_loop = true;
                    }
                } while (stop_loop == false && var_4 != 0);
            }
            else if (tmp1 == 0x0F)
            {
                if (sub_4001C(var_C, false, quick_fight, spellId) == true)
                {
                    if (gbl.SelectedPlayer.actions.target != null)
                    {
                        gbl.spellTargets.Clear();
                        gbl.spellTargets.Add(gbl.SelectedPlayer.actions.target);
                    }
                    else
                    {
                        /* TODO it doesn't make sense to mask the low nibble then shift it out */
                        var scl = ovr032.Rebuild_SortedCombatantList(1, (gbl.spellCastingTable[spellId].field_6 & 0x0f) >> 4, gbl.targetPos, sc => true);

                        gbl.spellTargets.Clear();
                        foreach (var sc in scl)
                        {
                            gbl.spellTargets.Add(sc.player);
                        }
                        gbl.byte_1D2C7 = true;
                    }
                }
                else
                {
                    castSpell = false;
                }
            }
            else if (tmp1 >= 8 && tmp1 <= 0x0E)
            {
                if (sub_4001C(var_C, true, quick_fight, spellId) == true)
                {
                    var scl = ovr032.Rebuild_SortedCombatantList(1, gbl.spellCastingTable[spellId].field_6 & 7, gbl.targetPos, sc => true);

                    gbl.spellTargets.Clear();
                    foreach (var sc in scl)
                    {
                        gbl.spellTargets.Add(sc.player);
                    }

                    gbl.byte_1D2C7 = true;
                }
                else
                {
                    castSpell = false;
                }
            }
            else
            {
                int max_targets = (gbl.spellCastingTable[spellId].field_6 & 3) + 1;
                gbl.spellTargets.Clear();

                while (max_targets > 0)
                {
                    if (sub_4001C(var_C, false, quick_fight, spellId) == true)
                    {
                        bool found = gbl.spellTargets.Exists(st => st == var_C.target);

                        if (found == false)
                        {
                            gbl.spellTargets.Add(var_C.target);
                            max_targets -= 1;

                            gbl.targetPos = ovr033.PlayerMapPos(var_C.target);
                        }
                        else
                        {
                            if (quick_fight == 0)
                            {
                                ovr025.string_print01("Already been targeted");
                            }
                            else
                            {
                                max_targets -= 1;
                            }
                        }

                        ovr033.RedrawPosition(ovr033.PlayerMapPos(var_C.target));
                    }
                    else
                    {
                        max_targets = 0;
                    }
                }

                if (gbl.spellTargets.Count == 0)
                {
                    castSpell = false;
                    gbl.targetPos = new Point();
                }

                //gbl.targetPos = ovr033.PlayerMapPos(gbl.spellTargets[gbl.spellTargets.Count-1]);
            }

            return castSpell;
        }


        internal static void spell_menu3(out bool casting_spell, QuickFight quick_fight, int spell_id)
        {
            Player player = gbl.SelectedPlayer;
            bool var_6 = true;
            int var_5 = -1;
            casting_spell = false;

            if (spell_id == 0)
            {
                spell_id = ovr020.spell_menu2(out var_6, ref var_5, SpellSource.Cast, SpellLoc.memory);
            }

            if (spell_id > 0 &&
                gbl.spellCastingTable[spell_id].whenCast == SpellWhen.Camp)
            {
                ovr025.string_print01("Camp Only Spell");
                spell_id = 0;
            }

            if (quick_fight == QuickFight.False)
            {
                ovr025.RedrawCombatScreen();
                gbl.focusCombatAreaOnPlayer = true;
                gbl.display_hitpoints_ac = true;

                ovr033.RedrawCombatIfFocusOn(true, 3, player);
                ovr025.CombatDisplayPlayerSummary(player);
            }

            if (spell_id > 0)
            {
                sbyte delay = (sbyte)(gbl.spellCastingTable[spell_id].castingDelay / 3);

                if (delay == 0)
                {
                    ovr023.sub_5D2E1(true, quick_fight, spell_id);

                    casting_spell = true;
                    ovr025.clear_actions(player);
                }
                else
                {
                    casting_spell = true;
                    ovr025.DisplayPlayerStatusString(true, 10, "Begins Casting", player);

                    player.actions.spell_id = spell_id;

                    if (player.actions.delay > delay)
                    {
                        player.actions.delay = delay;
                    }
                    else
                    {
                        player.actions.delay = 1;
                    }
                }
            }
        }


        internal static bool CanBackStabTarget(Player target, Player attacker) /* sub_408D7 */
        {
            if (attacker.SkillLevel(SkillType.Thief) > 0)
            {
                Item weapon = attacker.activeItems.primaryWeapon;

                if (weapon == null ||
                    weapon.type == ItemType.DrowLongSword ||
                    weapon.type == ItemType.Club ||
                    weapon.type == ItemType.Dagger ||
                    weapon.type == ItemType.BroadSword ||
                    weapon.type == ItemType.LongSword ||
                    weapon.type == ItemType.ShortSword)
                {
                    if (target.actions.AttacksReceived > 1 &&
                        (target.field_DE & 0x7F) <= 1 &&
                        getTargetDirection(target, attacker) == target.actions.direction)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        internal static byte getTargetDirection(Player playerB, Player playerA) /* sub_409BC */
        {
            Point plyr_a = ovr033.PlayerMapPos(playerA);
            Point plyr_b = ovr033.PlayerMapPos(playerB);

            int diff_x = System.Math.Abs(plyr_b.x - plyr_a.x);
            int diff_y = System.Math.Abs(plyr_b.y - plyr_a.y);

            byte direction = 0;
            bool solved = false;

            while (solved == false)
            {
                switch (direction)
                {
                    case 0:
                        if (plyr_b.y > plyr_a.y ||
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
                        if (plyr_b.x < plyr_a.x ||
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
                        if (plyr_b.y < plyr_a.y ||
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
                        if (plyr_b.x > plyr_a.x ||
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
                        if (plyr_b.y > plyr_a.y ||
                            plyr_b.x < plyr_a.x ||
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
                        if (plyr_b.y < plyr_a.y ||
                            plyr_b.x < plyr_a.x ||
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
                        if (plyr_b.y < plyr_a.y ||
                            plyr_b.x > plyr_a.x ||
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
                        if (plyr_b.y > plyr_a.y ||
                            plyr_b.x > plyr_a.x ||
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


        internal static void DrawRangedAttack(Item item, Player target, Player attacker) /* sub_40BF1 */
        {
            seg044.PlaySound(Sound.sound_c);

            int dir = getTargetDirection(target, attacker);

            int frame_count = 1;
            int delay = 10;
            int iconId = 13;

            switch (item.type)
            {
                case ItemType.Dart:
                case ItemType.Javelin:
                case ItemType.DartOfHornetsNest:
                case ItemType.Quarrel:
                case ItemType.Spear:
                case ItemType.Arrow:
                    if ((dir & 1) == 1)
                    {
                        if (dir == 3 || dir == 5)
                        {
                            ovr025.load_missile_dax((dir == 5), 0, Icon.Attack, iconId + 1);
                        }
                        else
                        {
                            ovr025.load_missile_dax((dir == 7), 0, Icon.Normal, iconId + 1);
                        }
                    }
                    else
                    {
                        if (dir >= 4)
                        {
                            ovr025.load_missile_dax(false, 0, Icon.Attack, iconId + (dir % 4));
                        }
                        else
                        {
                            ovr025.load_missile_dax(false, 0, Icon.Normal, iconId + (dir % 4));
                        }
                    }
                    seg044.PlaySound(Sound.sound_c);
                    break;

                case ItemType.HandAxe:
                case ItemType.Club:
                case ItemType.Glaive:
                    ovr025.load_missile_icons(iconId + 3);
                    frame_count = 4;
                    delay = 50;
                    seg044.PlaySound(Sound.sound_9);
                    break;

                case ItemType.Type_85:
                case ItemType.FlaskOfOil:
                    ovr025.load_missile_icons(iconId + 4);
                    frame_count = 4;
                    delay = 50;
                    seg044.PlaySound(Sound.sound_6);
                    break;

                case ItemType.StaffSling:
                case ItemType.Sling:
                case ItemType.Spine:
                    iconId++;
                    ovr025.load_missile_dax(false, 0, Icon.Normal, iconId + 7);
                    ovr025.load_missile_dax(false, 1, Icon.Attack, iconId + 7);
                    frame_count = 2;
                    delay = 10;
                    seg044.PlaySound(Sound.sound_6);
                    break;

                default:
                    ovr025.load_missile_dax(false, 0, Icon.Normal, iconId + 7);
                    ovr025.load_missile_dax(false, 1, Icon.Attack, iconId + 7);
                    frame_count = 2;
                    delay = 20;
                    seg044.PlaySound(Sound.sound_9);
                    break;
            }

            ovr025.draw_missile_attack(delay, frame_count, ovr033.PlayerMapPos(target), ovr033.PlayerMapPos(attacker));
        }


        internal static void calc_enemy_health_percentage() /* sub_40E00 */
        {
            int maxTotal = 0;
            int currentTotal = 0;

            foreach (Player player in gbl.TeamList)
            {
                if (player.combat_team == CombatTeam.Enemy)
                {
                    if (player.in_combat == true)
                    {
                        currentTotal += player.hit_point_current;
                    }

                    maxTotal += player.hit_point_max;
                }
            }

            if (maxTotal > 0)
            {
                gbl.enemyHealthPercentage = ((20 * currentTotal) / maxTotal) * 5;
            }
        }


        internal static int MaxOppositionMoves(Player player) // sub_40E8F
        {
            int maxMoves = 0;

            foreach (Player mob in gbl.TeamList)
            {
                if (player.OppositeTeam() == mob.combat_team && mob.in_combat == true)
                {
                    int moves = CalcMoves(mob) / 2;

                    maxMoves = System.Math.Max(moves, maxMoves);
                }
            }

            return maxMoves;
        }


        internal static bool can_attack_target(Player target, Player attacker) /* sub_40F1F */
        {
            bool result;

            if (target.OppositeTeam() == attacker.combat_team ||
                attacker.quick_fight == QuickFight.True)
            {
                result = true;
            }
            else if (ovr027.yes_no(gbl.defaultMenuColors, "Attack Ally: ") != 'Y')
            {
                result = false;
            }
            else
            {
                result = true;
                gbl.area2_ptr.field_666 = 1;

                foreach (Player player in gbl.TeamList)
                {
                    if (player.health_status == Status.okey &&
                        player.control_morale >= Control.NPC_Base)
                    {
                        player.combat_team = CombatTeam.Enemy;
                        player.actions.target = null;
                    }
                }

                ovr025.CountCombatTeamMembers();
            }

            return result;
        }


        internal static char aim_sub_menu(bool showTarget, bool showRange, int maxRange, Player target, Player attacker) /* Aim_menu */
        {
            string text = string.Empty;
            int range = ovr025.getTargetRange(target, attacker);
            int direction = getTargetDirection(target, attacker);

            if (showRange == true)
            {
                string range_txt = "Range = " + range.ToString() + "  ";
                seg041.displayString(range_txt, 0, 10, 0x17, 0);
            }

            if (range <= maxRange)
            {
                if (showRange == false)
                {
                    if (showTarget == true)
                    {
                        text = "Target ";
                    }
                    else
                    {
                        text = string.Empty;
                    }
                }
                else if (target != attacker)
                {
                    if (ovr025.is_weapon_ranged(attacker) == false)
                    {
                        text = "Target ";
                    }
                    else
                    {
                        Item dummyItem;
                        if (ovr025.GetCurrentAttackItem(out dummyItem, attacker) == true &&
                            (ovr025.BuildNearTargets(1, attacker).Count == 0 || ovr025.is_weapon_ranged_melee(attacker) == true))
                        {
                            text = "Target ";
                        }
                    }
                }
            }

            text = "Next Prev Manual " + text + "Center Exit";
            ovr033.RedrawCombatIfFocusOn(true, 3, target);
            gbl.display_hitpoints_ac = true;
            ovr025.CombatDisplayPlayerSummary(target);

            char input_key = ovr027.displayInput(false, 1, gbl.defaultMenuColors, text, "Aim:");

            return input_key;
        }


        internal static bool sub_411D8(Struct_1D183 arg_0, bool showRange, Player target, Player attacker)
        {
            bool arg_4 = true;

            if (showRange &&
                can_attack_target(target, attacker) == false)
            {
                arg_4 = false;
            }

            if (arg_4 == true)
            {
                arg_0.target = target;
                arg_0.map = ovr033.PlayerMapPos(target);
                gbl.mapToBackGroundTile.drawTargetCursor = false;

                ovr033.redrawCombatArea(8, 3, gbl.mapToBackGroundTile.mapScreenTopLeft + Point.ScreenCenter);

                if (showRange)
                {
                    if (TrySweepAttack(target, attacker) == true)
                    {
                        arg_4 = true;
                        ovr025.clear_actions(attacker);
                    }
                    else
                    {
                        RecalcAttacksReceived(target, attacker);

                        Item rangedWeapon = null;

                        if (ovr025.is_weapon_ranged(attacker) == true &&
                            ovr025.GetCurrentAttackItem(out rangedWeapon, attacker) == true &&
                            ovr025.is_weapon_ranged_melee(attacker) == true &&
                            ovr025.getTargetRange(target, attacker) == 0)
                        {
                            rangedWeapon = null;
                        }

                        arg_4 = AttackTarget(rangedWeapon, 0, target, attacker);
                    }
                }
            }
            else
            {
                arg_0.Clear();
            }

            return arg_4;
        }

        static Set asc_41342 = new Set(0, 69, 84);

        internal static bool Target(Struct_1D183 arg_0, bool allowTarget, bool canTargetEmptyGround, bool showRange, int maxRange, Player target, Player player01)
        {
            Item dummyItem;

            arg_0.Clear();

            var pos = ovr033.PlayerMapPos(target);

            char input_key = ' ';
            byte dir = 8;

            bool arg_4 = false;

            gbl.mapToBackGroundTile.drawTargetCursor = true;
            gbl.mapToBackGroundTile.size = 1;

            while (asc_41342.MemberOf(input_key) == false)
            {
                ovr033.redrawCombatArea(dir, 3, pos);
                pos += gbl.MapDirectionDelta[dir];
                pos.MapBoundaryTrunc();

                int groundTile;
                int playerAtXY;

                ovr033.AtMapXY(out groundTile, out playerAtXY, pos);
                seg043.clear_keyboard();
                bool can_target = false;
                int range = 255;

                if (ovr032.canReachTarget(ref range, pos, ovr033.PlayerMapPos(player01)) == true)
                {
                    can_target = true;

                    if (showRange)
                    {
                        string range_text = "Range = " + (range / 2).ToString() + "  ";

                        seg041.displayString(range_text, 0, 10, 0x17, 0);
                    }
                }
                else
                {
                    if (showRange)
                    {
                        seg037.draw8x8_clear_area(0x17, 0x27, 0x17, 0);
                    }
                }

                range /= 2;
                target = null;

                if (can_target)
                {
                    if (playerAtXY > 0)
                    {
                        target = gbl.player_array[playerAtXY];
                    }
                    else if (groundTile == 0x1f)
                    {
                        var c = gbl.downedPlayers.Find(cell => cell.map == pos);
                        if (c != null && c.target != null)
                        {
                            target = c.target;
                        }
                    }
                }

                if (target != null)
                {
                    gbl.display_hitpoints_ac = true;
                    ovr025.CombatDisplayPlayerSummary(target);
                }
                else
                {
                    seg037.draw8x8_clear_area(TextRegion.CombatSummary);
                }

                if (range > maxRange ||
                    gbl.BackGroundTiles[groundTile].move_cost == 0xff)
                {
                    can_target = false;
                }

                if (target != null)
                {
                    if (CanSeeTargetA(target, player01) == false ||
                        allowTarget == false)
                    {
                        can_target = false;
                    }

                    if (showRange)
                    {
                        if (player01 == target ||
                            (playerAtXY == 0 && groundTile == 0x1f))
                        {
                            can_target = false;
                        }
                        else if (ovr025.is_weapon_ranged(player01) == true &&
                             (ovr025.GetCurrentAttackItem(out dummyItem, player01) == false ||
                             (ovr025.BuildNearTargets(1, player01).Count > 0 &&
                                ovr025.is_weapon_ranged_melee(player01) == false)))
                        {
                            can_target = false;
                        }
                    }
                }
                else if (canTargetEmptyGround == false)
                {
                    can_target = false;
                }

                string text = "Center Exit";

                if (can_target)
                {
                    text = "Target " + text;
                }

                input_key = ovr027.displayInput(false, 1, gbl.defaultMenuColors, text, "(Use Cursor keys) ");

                switch (input_key)
                {
                    case '\r':
                    case 'T':
                        gbl.mapToBackGroundTile.drawTargetCursor = false;

                        if (can_target)
                        {
                            arg_0.map = pos;

                            if (target != null)
                            {
                                arg_0.target = target;
                            }
                            else
                            {
                                arg_0.target = null;
                            }

                            if (showRange)
                            {
                                arg_4 = sub_411D8(arg_0, showRange, arg_0.target, player01);
                            }
                            else
                            {
                                arg_4 = true;
                            }
                        }

                        if (can_target == false ||
                            arg_4 == false)
                        {
                            ovr033.RedrawPosition(pos);
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
                        ovr033.RedrawPosition(pos);
                        arg_0.Clear();
                        arg_4 = false;
                        break;

                    case 'C':
                        ovr033.redrawCombatArea(8, 0, pos);
                        dir = 8;
                        break;

                    default:
                        dir = 8;
                        break;
                }
            }

            return arg_4;
        }


        internal static SortedCombatant[] copy_sorted_players(Player player) /* sub_4188F */
        {
            var scl = ovr032.Rebuild_SortedCombatantList(player, 0x7F, p => true);

            return scl.ToArray();
        }


        internal static Player step_combat_list(bool arg_2, int step, ref int list_index, ref Point attackerPos, SortedCombatant[] sorted_list) /* sub_41932 */
        {
            if (arg_2 == true)
            {
                attackerPos = sorted_list[list_index - 1].pos;
            }
            else
            {
                ovr033.RedrawPosition(attackerPos);
            }

            list_index += step;

            if (list_index == 0)
            {
                list_index = sorted_list.GetLength(0);
            }

            if (list_index > sorted_list.GetLength(0))
            {
                list_index = 1;
            }

            Player newTarget = sorted_list[list_index - 1].player;

            var targetPos = sorted_list[list_index - 1].pos;

            if (arg_2 == true)
            {
                ovr025.draw_missile_attack(0, 1, targetPos, attackerPos);
                attackerPos = targetPos;
            }

            return newTarget;
        }

        static Set unk_41AE5 = new Set(0, 69);
        static Set unk_41B05 = new Set(71, 72, 73, 75, 77, 79, 80, 81);


        internal static bool aim_menu(Struct_1D183 arg_0, bool allowTarget, bool canTargetEmptyGround, bool showRange, int maxRange, Player attacker) /* sub_41B25 */
        {
            Player target; /* var_E5 */

            ovr025.load_missile_dax(false, 0, 0, 0x19);

            arg_0.Clear();

            bool arg_4 = false;

            if (maxRange == -1 || maxRange == 0xff)
            {
                if (attacker.activeItems.primaryWeapon != null)
                {
                    maxRange = gbl.ItemDataTable[attacker.activeItems.primaryWeapon.type].range - 1;
                }
                else
                {
                    maxRange = 1;
                }
            }

            if (maxRange == 0 ||
                maxRange == -1 || maxRange == 0xff)
            {
                maxRange = 1;
            }

            SortedCombatant[] sorted_list = copy_sorted_players(attacker);

            int list_index = 1;
            int next_prev_step = 0;
            int target_step = 0;

            Point attackerPos = new Point();

            target = step_combat_list(true, next_prev_step, ref list_index, ref attackerPos, sorted_list);

            next_prev_step = 1;
            char input = ' ';

            while (arg_4 == false && unk_41AE5.MemberOf(input) == false)
            {
                if (CanSeeTargetA(target, attacker) == false)
                {
                    target = step_combat_list(false, next_prev_step, ref list_index, ref attackerPos, sorted_list);
                }
                else
                {
                    input = aim_sub_menu(allowTarget, showRange, maxRange, target, attacker);

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
                                arg_4 = Target(arg_0, allowTarget, canTargetEmptyGround, showRange, maxRange, target, attacker);
                                ovr025.load_missile_dax(false, 0, 0, 0x19);

                                sorted_list = copy_sorted_players(attacker);
                                target_step = 0;
                                break;

                            case 'T':
                                arg_4 = sub_411D8(arg_0, showRange, target, attacker);
                                ovr025.load_missile_dax(false, 0, 0, 0x19);

                                sorted_list = copy_sorted_players(attacker);
                                target_step = 0;
                                break;

                            case 'C':
                                ovr033.redrawCombatArea(8, 0, ovr033.PlayerMapPos(target));
                                target_step = 0;
                                break;
                        }
                    }
                    else if (unk_41B05.MemberOf(input) == true)
                    {
                        arg_4 = Target(arg_0, allowTarget, canTargetEmptyGround, showRange, maxRange, target, attacker);
                        ovr025.load_missile_dax(false, 0, 0, 0x19);
                        sorted_list = copy_sorted_players(attacker);
                        target_step = 0;
                    }

                    ovr033.RedrawPosition(ovr033.PlayerMapPos(target));

                    target = step_combat_list((arg_4 == false && unk_41AE5.MemberOf(input) == false), target_step,
                       ref list_index, ref attackerPos, sorted_list);
                }
            }

            if (showRange)
            {
                seg037.draw8x8_clear_area(0x17, 0x27, 0x17, 0);
            }

            return arg_4;
        }


        internal static bool find_target(bool clear_target, byte arg_2, int max_range, Player player) /* sub_41E44 */
        {
            bool target_found = false;

            Player target = player.actions.target;

            if (clear_target == true ||
                 (target != null &&
                   (target.combat_team == player.combat_team ||
                    target.in_combat == false ||
                    CanSeeTargetA(target, player) == false)))
            {
                player.actions.target = null;
            }

            if (player.actions.target != null)
            {
                target_found = true;
            }

            bool secondPass = false;
            bool var_5 = false;
            while (target_found == false && var_5 == false)
            {
                var_5 = secondPass;

                if (secondPass == true && clear_target == false)
                {
                    gbl.mapToBackGroundTile.ignoreWalls = true;
                }

                int tryCount = 20;
                var nearTargets = ovr025.BuildNearTargets(max_range, player);

                while (tryCount > 0 && target_found == false && nearTargets.Count > 0)
                {
                    tryCount--;
                    int roll = ovr024.roll_dice(nearTargets.Count, 1);

                    var epi = nearTargets[roll - 1];
                    target = epi.player;

                    if ((arg_2 != 0 && gbl.mapToBackGroundTile.ignoreWalls == true) ||
                        CanSeeTargetA(target, player) == true)
                    {
                        target_found = true;
                        player.actions.target = target;
                    }
                    else
                    {
                        nearTargets.Remove(epi);
                    }
                }

                if (secondPass == false)
                {
                    secondPass = true;
                }
            }

            gbl.mapToBackGroundTile.ignoreWalls = false;

            return target_found;
        }


        internal static void engulfs(Effect arg_0, object param, Player attacker)
        {
            Player target = attacker.actions.target;

            if (gbl.bytes_1D2C9[1] == 2 &&
                target.in_combat == true &&
                target.HasAffect(Affects.clear_movement) == false &&
                target.HasAffect(Affects.reduce) == false)
            {
                target = attacker.actions.target;
                ovr025.DisplayPlayerStatusString(true, 12, "engulfs " + target.name, attacker);
                ovr024.add_affect(false, ovr033.GetPlayerIndex(target), 0, Affects.clear_movement, target);

                ovr013.CallAffectTable(Effect.Add, null, target, Affects.clear_movement);
                ovr024.add_affect(false, ovr024.roll_dice(4, 2), 0, Affects.reduce, target);
                ovr024.add_affect(true, ovr033.GetPlayerIndex(target), 0, Affects.affect_8b, attacker);
            }
        }


        internal static void LoadMissleIconAndDraw(int icon_id, Player target, Player attacker) //sub_42159
        {
            ovr025.load_missile_icons(icon_id + 13);

            ovr025.draw_missile_attack(0x1E, 1, ovr033.PlayerMapPos(target), ovr033.PlayerMapPos(attacker));
        }


        internal static bool sub_421C1(bool clear_target, ref int range, Player player) // sub_421C1
        {
            bool var_5 = true;

            if (find_target(clear_target, 0, 0xff, player) == true)
            {
                var target = ovr033.PlayerMapPos(player.actions.target);

                if (ovr032.canReachTarget(ref range, target, ovr033.PlayerMapPos(player)) == true)
                {
                    var_5 = false;
                }
            }

            return var_5;
        }


        internal static void attack_or_kill(Effect arg_0, object param, Player attacker)
        {
            int range = 0xFF; /* simeon */

            byte attacksTired = 0;
            int attackTiresLeft = 4;

            attacker.actions.target = null;
            sub_421C1(true, ref range, attacker);

            do
            {
                Player target = attacker.actions.target;

                range = ovr025.getTargetRange(target, attacker);
                attackTiresLeft--;

                if (target != null)
                {
                    if (range == 2 && (attacksTired & 1) == 0)
                    {
                        attacksTired |= 1;

                        ovr025.DisplayPlayerStatusString(true, 10, "fires a disintegrate ray", attacker);
                        LoadMissleIconAndDraw(5, target, attacker);

                        if (ovr024.RollSavingThrow(0, SaveVerseType.type3, target) == false)
                        {
                            ovr024.KillPlayer("is disintergrated", Status.gone, target);
                        }

                        sub_421C1(false, ref range, attacker);
                    }
                    else if (range == 3 && (attacksTired & 2) == 0)
                    {
                        attacksTired |= 2;

                        ovr025.DisplayPlayerStatusString(true, 10, "fires a stone to flesh ray", attacker);
                        LoadMissleIconAndDraw(10, target, attacker);

                        if (ovr024.RollSavingThrow(0, SaveVerseType.type1, target) == false)
                        {
                            ovr024.KillPlayer("is Stoned", Status.stoned, target);
                        }

                        sub_421C1(false, ref range, attacker);
                    }
                    else if (range == 4 && (attacksTired & 4) == 0)
                    {
                        attacksTired |= 4;

                        ovr025.DisplayPlayerStatusString(true, 10, "fires a death ray", attacker);
                        LoadMissleIconAndDraw(5, target, attacker);

                        if (ovr024.RollSavingThrow(0, 0, target) == false)
                        {
                            ovr024.KillPlayer("is killed", Status.dead, target);
                        }

                        sub_421C1(false, ref range, attacker);
                    }
                    else if (range == 5 && (attacksTired & 8) == 0)
                    {
                        attacksTired |= 8;

                        ovr025.DisplayPlayerStatusString(true, 10, "wounds you", attacker);
                        LoadMissleIconAndDraw(5, target, attacker);

                        ovr024.damage_person(false, 0, ovr024.roll_dice_save(8, 2) + 1, target);
                        sub_421C1(false, ref range, attacker);
                    }
                    else if ((attacksTired & 0x10) == 0)
                    {
                        ovr023.sub_5D2E1(true, QuickFight.True, 0x54);
                        attacksTired |= 0x10;
                    }
                    else if ((attacksTired & 0x20) == 0)
                    {
                        ovr023.sub_5D2E1(true, QuickFight.True, 0x37);
                        attacksTired |= 0x20;
                    }
                    else if ((attacksTired & 0x40) == 0)
                    {
                        ovr023.sub_5D2E1(true, QuickFight.True, 0x15);
                        attacksTired |= 0x40;
                    }
                }
            } while (attackTiresLeft > 0 && attacker.actions.target != null);
        }


        internal static void sub_425C6(Effect add_remove, object param, Player player)
        {
            Affect affect = (Affect)param;

            gbl.spell_target = gbl.player_array[affect.affect_data];

            if (add_remove == Effect.Remove ||
                player.in_combat == false ||
                gbl.spell_target.in_combat == false)
            {
                ovr024.remove_affect(null, Affects.clear_movement, gbl.spell_target);
                ovr024.remove_affect(null, Affects.reduce, gbl.spell_target);

                if (add_remove == Effect.Add)
                {
                    affect.callAffectTable = false;

                    ovr024.remove_affect(affect, Affects.affect_8b, player);
                }
            }
            else
            {
                player.attack1_AttacksLeft = 2;
                player.attack2_AttacksLeft = 0;
                player.attack1_DiceCount = 2;
                player.attack1_DiceSize = 8;

                AttackTarget(null, 1, gbl.spell_target, player);

                ovr025.clear_actions(player);

                if (gbl.spell_target.in_combat == false)
                {
                    ovr024.remove_affect(null, Affects.affect_8b, player);
                    ovr024.remove_affect(null, Affects.clear_movement, gbl.spell_target);
                    ovr024.remove_affect(null, Affects.reduce, gbl.spell_target);
                }
            }
        }


        internal static void AffectOwlbearHugRoundAttack(Effect arg_0, object param, Player player) // sub_426FC
        {
            Affect affect = (Affect)param;

            gbl.spell_target = gbl.player_array[affect.affect_data];

            if (arg_0 == Effect.Remove ||
                player.in_combat == false ||
                gbl.spell_target.in_combat == false)
            {
                ovr024.remove_affect(null, Affects.clear_movement, gbl.spell_target);
                if (arg_0 == Effect.Add)
                {
                    affect.callAffectTable = false;
                    ovr024.remove_affect(affect, Affects.owlbear_hug_round_attack, player);
                }
            }
            else
            {
                player.attack1_AttacksLeft = 1;
                player.attack2_AttacksLeft = 0;
                player.attack1_DiceCount = 2;
                player.attack1_DiceSize = 8;


                AttackTarget(null, 2, gbl.spell_target, player);

                ovr025.clear_actions(player);

                if (gbl.spell_target.in_combat == false)
                {
                    ovr024.remove_affect(null, Affects.owlbear_hug_round_attack, player);
                    ovr024.remove_affect(null, Affects.clear_movement, gbl.spell_target);
                }
            }
        }


        internal static void AffectOwlbearHugAttackCheck(Effect arg_0, object param, Player player) // hugs
        {
            if (gbl.attack_roll >= 18)
            {
                gbl.spell_target = player.actions.target;
                ovr025.DisplayPlayerStatusString(true, 12, "hugs " + gbl.spell_target.name, player);

                ovr024.add_affect(false, ovr033.GetPlayerIndex(gbl.spell_target), 0, Affects.clear_movement, gbl.spell_target);
                ovr013.CallAffectTable(Effect.Add, null, gbl.spell_target, Affects.clear_movement);

                ovr024.add_affect(true, ovr033.GetPlayerIndex(gbl.spell_target), 0, Affects.owlbear_hug_round_attack, player);
            }
        }


        internal static bool god_intervene()
        {
            bool intervened = false;

            if (Cheats.allow_gods_intervene)
            {
                intervened = true;
                ovr025.string_print01("The Gods intervene!");

                foreach (Player player in gbl.TeamList)
                {
                    if (player.combat_team == CombatTeam.Enemy)
                    {
                        player.in_combat = false;
                        player.health_status = Status.dead;

                        gbl.CombatMap[ovr033.GetPlayerIndex(player)].size = 0;
                    }

                    ovr025.clear_actions(player);
                }

                ovr033.redrawCombatArea(8, 0xff, gbl.mapToBackGroundTile.mapScreenTopLeft + Point.ScreenCenter);
            }

            return intervened;
        }
    }
}
