using Classes;

namespace engine
{
    class ovr014
    {
        internal static void sub_3E000(Player player)
        {
            Action action = player.actions;

            action.spell_id = 0;
            action.can_cast = true;
            action.can_use = true;
            action.field_8 = 0;
            action.field_4 = 2;

            sub_3EDD4(player);
            gbl.movesLeft = player.field_11D;

            gbl.resetMovesLeft = false;

            ovr024.CheckAffectsEffect(player, CheckType.Movement);

            player.field_19D = (byte)sub_3EF0D(gbl.movesLeft);

            action.field_5 = player.field_DD;

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
                    action.delay > 0x14)
                {
                    action.delay = 0;
                }
            }
            else
            {
                action.delay = 0;
            }

            player.actions.move = (byte)sub_3E124(player);
        }


        internal static int sub_3E124(Player player)
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

            gbl.movesLeft = moves * 2;

            gbl.resetMovesLeft = true;
            
            ovr024.CheckAffectsEffect(player, CheckType.Movement);

            gbl.resetMovesLeft = false;

            return gbl.movesLeft;
        }


        static void sub_3E192(int index, Player target, Player attacker)
        {
            gbl.damage = ovr024.roll_dice_save(attacker.field_19FArray(index), attacker.field_19DArray(index));
            gbl.damage += attacker.field_1A1Array(index);

            if (gbl.damage < 0)
            {
                gbl.damage = 0;
            }

            if (CanBackStabTarget(target, attacker) == true)
            {
                gbl.damage *= (((attacker.thief_lvl + (attacker.field_117 * ovr026.sub_6B3D1(attacker))) - 1) / 4) + 2;
            }

            gbl.damage_flags = 0;
            ovr024.CheckAffectsEffect(attacker, CheckType.Type_4);
            ovr024.CheckAffectsEffect(target, CheckType.Type_5);
        }

        static Set unk_3E2EE = new Set(0x0002, new byte[] { 0xC0, 0x01 });


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
                seg041.press_any_key(text, true, 0, 10, line + 3, 0x26, line, 0x17);
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

                if (unk_3E2EE.MemberOf((int)target.health_status) == true)
                {
                    ovr025.DisplayPlayerStatusString(false, line, "is killed", target);
                }

                line += 2;

                ovr024.sub_645AB(target);

                ovr024.CheckAffectsEffect(target, CheckType.Death);

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


        static void move_step_into_attack(Player target) /* sub_3E65D */
        {
            var nearTargets = ovr025.BuildNearTargets(1, target);

            if (target.in_combat)
            {
                foreach(var cpi in nearTargets) 
                {
                    Player attacker = cpi.player;

                    if (attacker.actions.guarding == true &&
                        attacker.IsHeld() == false)
                    {
                        ovr033.redrawCombatArea(8, 2, ovr033.PlayerMapPos(target));

                        attacker.actions.guarding = false;

                        sub_3F94D(target, attacker);

                        sub_3F9DB(0, target, attacker);
                    }
                }
            }
        }


        internal static void sub_3E748(int direction, Player player)
        {
            int player_index = ovr033.GetPlayerIndex(player);

            Point oldPos = gbl.CombatMap[player_index].pos;
            Point newPos = oldPos + gbl.MapDirectionDelta[direction];

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
                player.actions.move -= (byte)costToMove;
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

            player.actions.field_F = 0;
            player.actions.field_12 = 0;
            seg044.sound_sub_120E0(Sound.sound_a);

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

            if (originAttackers.Count > 0)
            {
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

                if (player.in_combat)
                {
                    foreach (var cpiA in originAttackers)
                    {
                        gbl.display_hitpoints_ac = true;
                        gbl.focusCombatAreaOnPlayer = true;
                        bool var_12 = false;

                        Player attacker = cpiA.player;

                        if (attacker.IsHeld() == false &&
                            CanSeeTargetA(player, attacker) == true &&
                            attacker.HasAffect(Affects.affect_4b) == false &&
                            attacker.HasAffect(Affects.affect_4a) == false)
                        {
                            int end_dir = attacker.actions.direction + 10;

                            for (int var_1B = attacker.actions.direction + 6; var_1B <= end_dir; var_1B++)
                            {
                                if (var_12 == false)
                                {
                                    if (attacker.actions.delay > 0 ||
                                        attacker.actions.field_F == 0 ||
                                        ovr032.CanSeeCombatant(var_1B % 8, ovr033.PlayerMapPos(player), ovr033.PlayerMapPos(attacker)) == true)
                                    {
                                        byte var_1A = 1;
                                        if (attacker.field_11C == 0)
                                        {
                                            var_1A = 2;
                                        }

                                        if (attacker.field_19C > 0)
                                        {
                                            var_1A = 1;
                                        }

                                        if (attacker.field_19D > 0)
                                        {
                                            var_1A = 2;
                                        }

                                        if (attacker.field_19BArray(var_1A) == 0)
                                        {
                                            attacker.field_19BArraySet(var_1A, 1);
                                        }

                                        attacker.actions.field_4 = var_1A;

                                        Player backupTarget = attacker.actions.target;

                                        sub_3F9DB(1, player, attacker);
                                        var_12 = true;

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
                int var_4 = sub_3E124(player) / 2;
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
                ovr024.sub_644A7("Got Away", Status.running, player);
            }
            else
            {
                ovr025.string_print01("Escape is blocked");
            }

            ovr025.clear_actions(player);
        }


        internal static void sub_3EDD4(Player player)
        {
            bool var_5 = false;
            Item var_9 = null;
            byte var_2 = player.field_19C;
            player.field_19C = player.field_11C;

            if (ovr025.is_weapon_ranged(player) == true &&
                ovr025.sub_6906C(out var_9, player) == true)
            {
                var_5 = true;
                byte var_4 = gbl.ItemDataTable[player.field_151.type].field_5;

                if (var_4 < 2)
                {
                    var_4 = 2;
                }

                gbl.movesLeft = var_4;
            }
            else
            {
                gbl.movesLeft = player.field_19C;
            }

            gbl.resetMovesLeft = false;
            ovr024.CheckAffectsEffect(player, CheckType.Movement);

            int var_1 = sub_3EF0D(gbl.movesLeft);

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
                    var_1 = var_3;
                }
            }

            if (player.actions.field_8 == 0 ||
                var_1 < var_2 ||
                (player.actions.field_8 != 0 &&
                  var_1 < (var_2 * 2) &&
                  var_5 == false))
            {
                player.field_19C = (byte)var_1;
            }
        }


        internal static int sub_3EF0D(int movesleft)
        {
            if ((gbl.combat_round & 1) == 1)
            {
                movesleft++;
            }

            return movesleft / 2;
        }


        internal static bool TrySweepAttack(Player target, Player attacker) // sub_3EF3D
        {
            if (attacker.field_19C < attacker.actions.field_5 &&
                target.HitDice == 0 &&
                ovr025.getTargetRange(target, attacker) == 1)
            {
                var nearTargets = ovr025.BuildNearTargets(1, attacker);

                var targetepi = nearTargets.Find(epi => epi.player == target);
                int sweapableCount = nearTargets.FindAll(epi => epi.player.HitDice == 0).Count;

                if (sweapableCount > attacker.field_19C)
                {
                    if (sweapableCount > attacker.actions.field_5)
                    {
                        sweapableCount = attacker.actions.field_5;
                    }

                    ovr025.DisplayPlayerStatusString(true, 10, "sweeps", attacker);

                    nearTargets.Remove(targetepi);
                    nearTargets.Insert(0, targetepi);

                    foreach (var sweapepi in nearTargets.FindAll(e => e.player.hitBonus == 0).GetRange(0, sweapableCount))
                    {
                        var sweaptarget = sweapepi.player;
                        sub_3F94D(sweaptarget, attacker);

                        attacker.field_19C = 1;

                        sub_3F9DB(0, sweaptarget, attacker);
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
                    gbl.byte_1D2C5 = false;

                    ovr024.CheckAffectsEffect(targetA, CheckType.Visibility);

                    if (gbl.byte_1D2C5 == false)
                    {
                        var old_target = targetB.actions.target;

                        targetB.actions.target = targetA;

                        ovr024.CheckAffectsEffect(targetB, CheckType.None);

                        targetB.actions.target = old_target;
                    }

                    return (gbl.byte_1D2C5 == false);
                }
            }

            return false;
        }

        static sbyte[] /*seg600:0369*/ unk_16679 = { 0, 
            17, 17,  0,  0,  1, 17,  0,  0, 32, 32, 
            10,  7,  4,  1,  1,  0,  0, -1, -1, -1 };

        internal static void turns_undead(Player player)
        {
            byte var_B;

            ovr025.DisplayPlayerStatusString(false, 10, "turns undead...", player);
            ovr027.ClearPromptArea();
            seg041.GameDelay();

            bool any_turned = false;
            byte var_6 = 0;

            player.actions.hasTurnedUndead = true;

            byte var_3 = 6;
            int var_2 = ovr024.roll_dice(12, 1);
            int var_1 = ovr024.roll_dice(20, 1);

            int al = (ovr026.sub_6B3D1(player) * player.turn_undead) + player.cleric_lvl;

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
                var_B = 10;
            }

            Player target;
            while (sub_3F433(out target, player) == true && var_2 > 0 && var_6 == 0)
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
                        ovr033.sub_74E6F(target);
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


        internal static bool sub_3F433(out Player output, Player player)
        {
            output = null;

            byte var_4 = 13;

            var nearTargets = ovr025.BuildNearTargets(0xff, player);
            bool result = false;

            foreach(var epi in nearTargets)
            {
                Player target = epi.player;

                if (target.actions.fleeing == false &&
                    target.field_E9 > 0 &&
                    target.field_E9 < var_4)
                {
                    var_4 = target.field_E9;
                    output = target;
                    result = true;
                }
            }

            return result;
        }


        internal static void sub_3F4EB(Item item, ref bool arg_4, byte arg_8, Player target, Player attacker)
        {
            int target_ac;

            byte var_13 = arg_8;
            arg_4 = false;
            gbl.byte_1D2CA = 0;
            gbl.byte_1D2CB = 0;
            gbl.byte_1D901 = 0;
            gbl.byte_1D902 = 0;
            bool var_11 = false;
            bool var_12 = false;
            gbl.damage = 0;

            attacker.actions.field_8 = 1;

            if (target.IsHeld() == true)
            {
                seg044.sound_sub_120E0(Sound.sound_7);

                while (attacker.field_19BArray(attacker.actions.field_4) == 0)
                {
                    attacker.actions.field_4--;
                }

                gbl.inc_byte_byte_1D90x(attacker.actions.field_4);

                DisplayAttackMessage(true, 1, target.hit_point_current + 5, AttackType.Slay, target, attacker);
                ovr024.remove_invisibility(attacker);

                attacker.field_19C = 0;
                attacker.field_19D = 0;

                var_11 = true;
                arg_4 = true;
            }
            else
            {
                if (attacker.field_151 != null && 
                    (target.field_DE > 0x80 || (target.field_DE & 7) > 1))
                {
                    ItemData itemData = gbl.ItemDataTable[attacker.field_151.type];

                    attacker.attack_dice_count = itemData.diceCount;
                    attacker.attack_dice_size = itemData.diceSize;
                    attacker.damageBonus -= itemData.field_B;
                    attacker.damageBonus += itemData.field_4;
                }

                ovr025.reclac_player_values(target);
                ovr024.CheckAffectsEffect(target, CheckType.Type_11);

                if (CanBackStabTarget(target, attacker) == true)
                {
                    target_ac = target.field_19B - 4;
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
                        target_ac = target.field_19B;
                    }
                    else
                    {
                        target_ac = target.ac;
                    }
                }

                target_ac += RangedDefenseBonus(target, attacker);
                AttackType attack_type = AttackType.Normal;
                if (var_13 != 0)
                {
                    attack_type = AttackType.Behind;
                }

                if (CanBackStabTarget(target, attacker) == true)
                {
                    attack_type = AttackType.Backstab;
                }

                byte var_16 = attacker.actions.field_4;
                for (byte var_15 = var_16; var_15 >= 1; var_15--)
                {
                    while (attacker.field_19BArray(var_15) > 0 &&
                        var_12 == false)
                    {
                        attacker.field_19BArrayDec(var_15);
                        attacker.actions.field_4 = var_15;

                        gbl.inc_byte_byte_1D90x(var_15);

                        if (ovr024.PC_CanHitTarget(target_ac, target, attacker) ||
                            target.IsHeld() == true)
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

                            seg044.sound_sub_120E0(Sound.sound_7);
                            var_11 = true;
                            sub_3E192(var_15, target, attacker);
                            DisplayAttackMessage(true, gbl.damage, gbl.damage, attack_type, target, attacker);

                            if (target.in_combat == true)
                            {
                                ovr024.CheckAffectsEffect(attacker, (CheckType)var_15 + 1);
                            }

                            if (target.in_combat == false)
                            {
                                var_12 = true;
                            }

                            if (attacker.in_combat == false)
                            {
                                var_16 = 0;
                                attacker.field_19BArraySet(var_15, 0);
                            }
                        }
                    }
                }

                if (item != null && 
                    item.count == 0 && 
                    item.type == 100)
                {
                    attacker.field_19C = 0;
                    attacker.field_19D = 0;
                }

                if (var_11 == false)
                {
                    seg044.sound_sub_120E0(Sound.sound_9);
                    DisplayAttackMessage(false, 0, 0, attack_type, target, attacker);
                }

                arg_4 = true;
                if (attacker.field_19C > 0 ||
                    attacker.field_19D > 0)
                {
                    arg_4 = false;
                } 
                
                attacker.actions.field_5 = 0;
            }

            if (attacker.in_combat == false)
            {
                arg_4 = true;
            }

            if (arg_4 == true)
            {
                ovr025.clear_actions(attacker);
            }
        }


        internal static void sub_3F94D(Player target, Player attacker)
        {
            target.actions.field_F++;

            byte var_2 = getTargetDirection(attacker, target);

            int var_1 = ((var_2 - target.actions.direction) + 8) % 8;

            if (var_1 > 4)
            {
                var_1 = 8 - var_1;
            }

            target.actions.field_12 = (target.actions.field_12 + var_1) % 8;
        }


        internal static void sub_3F9DB(byte arg_8, Player target, Player attacker)
        {
            bool dummyBool;
            sub_3F9DB(out dummyBool, null, arg_8, target, attacker);
        }


        internal static void sub_3F9DB(out bool arg_0, Item item, byte arg_8, Player target, Player attacker)
        {
            int dir = 0;

            gbl.focusCombatAreaOnPlayer = true;
            gbl.display_hitpoints_ac = true;

            gbl.combat_round_no_action_limit = gbl.combat_round + 15;

            if (target.actions.field_F < 2 && arg_8 == 0)
            {
                dir = getTargetDirection(attacker, target);

                target.actions.direction = (dir + 4) % 8;
            }
            else if (ovr033.PlayerOnScreen(false, target) == true)
            {
                dir = target.actions.direction;

                if (arg_8 == 0)
                {
                    target.actions.direction = (dir + 4) % 8;
                }
            }

            if (ovr033.PlayerOnScreen(false, target) == true)
            {
                ovr033.draw_74B3F(0, 0, dir, target);
            }

            dir = getTargetDirection(target, attacker);
            ovr025.CombatDisplayPlayerSummary(attacker);

            ovr033.draw_74B3F(0, 1, dir, attacker);

            attacker.actions.target = target;

            seg049.SysDelay(100);

            if (item != null)
            {
                sub_40BF1(item, target, attacker);
            }

            if (attacker.field_151 != null && /* added to get passed this point when null, why null, should 151 ever be null? */
                (attacker.field_151.type == 47 || // ItemType.Sling
                attacker.field_151.type == 101)) 
            {
                sub_40BF1(attacker.field_151, target, attacker);
            }

            arg_0 = true;

            if (attacker.field_19C > 0 ||
                attacker.field_19D > 0)
            {
                Player player_bkup = gbl.player_ptr;

                gbl.player_ptr = attacker;

                sub_3F4EB(item, ref arg_0, arg_8, target, attacker);

                if (item != null)
                {
                    if (item.count > 0)
                    {
                        item.count = gbl.byte_1D901;
                    }

                    if (item.count == 0)
                    {
                        if (ovr025.is_weapon_ranged_melee(attacker) == true &&
                            item.affect_3 != Affects.affect_89)
                        {
                            Item new_item = item.ShallowClone();
                            new_item.readied = false;

                            gbl.items_pointer.Add(new_item);

                            ovr025.lose_item(item, attacker);
                            gbl.item_ptr = new_item;
                        }
                        else
                        {
                            ovr025.lose_item(item, attacker);
                        }
                    }
                }

                ovr025.reclac_player_values(attacker);
                gbl.player_ptr = player_bkup;
            }

            if (arg_0 == true)
            {
                ovr025.clear_actions(attacker);
            }

            if (ovr033.PlayerOnScreen(false, attacker) == true)
            {
                ovr033.draw_74B3F(1, 1, attacker.actions.direction, attacker);
                ovr033.draw_74B3F(0, 0, attacker.actions.direction, attacker);
            }
        }


        internal static int RangedDefenseBonus(Player target, Player attacker) /* sub_3FCED */
        {
            int oneThirdRange;
            int range = ovr025.getTargetRange(target, attacker);

            if (ovr025.is_weapon_ranged(attacker) == true)
            {
                oneThirdRange = (gbl.ItemDataTable[attacker.field_151.type].range - 1) / 3;
            }
            else
            {
                oneThirdRange = range;
            }

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
                    var_8 = gbl.downedPlayers.FindLast( cell => cell.target != null && cell.map == map && 
                        cell.target.health_status != Status.tempgone && cell.target.health_status != Status.running &&
                        cell.target.health_status != Status.unconscious );
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

        internal static bool sub_4001C(Struct_1D183 arg_0, byte arg_4, QuickFight quick_fight, byte spellId)
        {
            bool var_2 = false;
            if (quick_fight == QuickFight.False)
            {
                byte var_A = spellId != 0x53 ? (byte)1 : (byte)0;

                aim_menu(arg_0, out var_2, var_A, arg_4, 0, ovr023.SpellRange(spellId), gbl.player_ptr);
                gbl.player_ptr.actions.target = arg_0.target;
            }
            else if (gbl.spell_table[spellId].field_E == 0)
            {
                arg_0.target = gbl.player_ptr;

                if (spellId != 3 || find_healing_target(out arg_0.target, gbl.player_ptr))
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

                    if (find_target(true, 0, ovr023.SpellRange(spellId), gbl.player_ptr) == true)
                    {
                        Player target = gbl.player_ptr.actions.target;

                        if (target.IsHeld() == true)
                        {
                            for (int i = 1; i <= 4; i++)
                            {
                                if (gbl.spell_table[spellId].affect_id == unk_18ADB[i])
                                {
                                    var_3 = false;
                                }
                            }
                        }

                        if (var_3 == true)
                        {
                            arg_0.target = gbl.player_ptr.actions.target;
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

        internal static void target(out bool arg_0, QuickFight quick_fight, byte spellId)
        {
            Struct_1D183 var_C = new Struct_1D183();

            arg_0 = true;
            gbl.spellTargets.Clear();
            gbl.byte_1D2C7 = false;

            gbl.targetPos = ovr033.PlayerMapPos(gbl.player_ptr);

            byte tmp1 = (byte)(gbl.spell_table[spellId].field_6 & 0x0F);

            if (tmp1 == 0)
            {
                gbl.spellTargets.Clear();
                gbl.spellTargets.Add(gbl.player_ptr);
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
                    if (sub_4001C(var_C, 0, quick_fight, spellId) == true)
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

                        ovr033.sub_7431C(ovr033.PlayerMapPos(var_C.target));
                    }
                    else
                    {
                        stop_loop = true;
                    }
                } while (stop_loop == false && var_4 != 0);
            }
            else if (tmp1 == 0x0F)
            {

                if (sub_4001C(var_C, 0, quick_fight, spellId) == true)
                {
                    if (gbl.player_ptr.actions.target != null)
                    {
                        gbl.spellTargets.Clear();
                        gbl.spellTargets.Add(gbl.player_ptr.actions.target);
                    }
                    else
                    {
                        /* TODO it doesn't make sense to mask the low nibble then shift it out */
                        var scl = ovr032.Rebuild_SortedCombatantList(gbl.mapToBackGroundTile, 1, 0xff, (gbl.spell_table[spellId].field_6 & 0x0f) >> 4, gbl.targetPos);
                        

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
                    arg_0 = false;
                }
            }
            else if (tmp1 >= 8 && tmp1 <= 0x0E)
            {
                if (sub_4001C(var_C, 1, quick_fight, spellId) == true)
                {
                    var scl = ovr032.Rebuild_SortedCombatantList(gbl.mapToBackGroundTile, 1, 0xff, gbl.spell_table[spellId].field_6 & 7, gbl.targetPos);

                    gbl.spellTargets.Clear();
                    foreach (var sc in scl)
                    {
                        gbl.spellTargets.Add(sc.player);
                    }

                    gbl.byte_1D2C7 = true;
                }
                else
                {
                    arg_0 = false;
                }
            }
            else
            {
                int var_1 = (gbl.spell_table[spellId].field_6 & 3) + 1;
                gbl.spellTargets.Clear();

                while (var_1 > 0)
                {
                    if (sub_4001C(var_C, 0, quick_fight, spellId) == true)
                    {
                        bool found = gbl.spellTargets.Exists(st => st == var_C.target);

                        if (found == false)
                        {
                            gbl.spellTargets.Add(var_C.target);
                            var_1 -= 1;

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
                                var_1 -= 1;
                            }
                        }

                        ovr033.sub_7431C(ovr033.PlayerMapPos(var_C.target));
                    }
                    else
                    {
                        var_1 = 0;
                    }
                }

                if (gbl.spellTargets.Count == 0)
                {
                    arg_0 = false;
                    gbl.targetPos = new Point();
                }
 
                //gbl.targetPos = ovr033.PlayerMapPos(gbl.spellTargets[gbl.spellTargets.Count-1]);
            }
        }


        internal static void spell_menu3(out bool casting_spell, QuickFight quick_fight, byte spell_id)
        {
            Player player = gbl.player_ptr;
            bool var_6 = true;
            int var_5 = -1;
            casting_spell = false;

            if (spell_id == 0)
            {
                spell_id = ovr020.spell_menu2(out var_6, ref var_5, SpellSource.Cast, SpellLoc.memory);
            }

            if (spell_id > 0 &&
                gbl.spell_table[spell_id].whenCast == SpellWhen.Camp)
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
                sbyte delay = (sbyte)(gbl.spell_table[spell_id].castingDelay / 3);

                if (delay == 0)
                {
                    ovr023.sub_5D2E1(1, quick_fight, spell_id);

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
            Item weapon = attacker.field_151;

            bool correctWeapon = false;

            if (attacker.thief_lvl > 0 ||
                (attacker.field_117 > 0 && ovr026.sub_6B3D1(attacker) != 0))
            {
                if (weapon == null ||
                    weapon.type == 97 ||
                    weapon.type == 7 || // ItemType.Club
                    weapon.type == 8 || // ItemType.Dagger
                    weapon.type == 35 || // ItemType.BroadSword
                    weapon.type == 36 || // ItemType.LongSword
                    weapon.type == 37) // ItemType.ShortSword
                {
                    correctWeapon = true;
                }
            }

            if (correctWeapon == true &&
                target.actions.field_F > 1 &&
                (target.field_DE & 0x7F) <= 1 &&
                getTargetDirection(target, attacker) == target.actions.direction)
            {
                return true;
            }
            else
            {
                return false;
            }
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


        internal static void sub_40BF1(Item item, Player target, Player attacker) /* sub_40BF1 */
        {
            seg044.sound_sub_120E0(Sound.sound_c);

            byte dir = getTargetDirection(target, attacker);

            int frame_count = 1;
            int delay = 10;
            byte var_1 = 0x0D;

            switch (item.type)
            {
                case 9: // ItemType.Dart
                case 21: // ItemType.Javelin
                case 100:
                case 28: // ItemType.Quarrel
                case 31: // ItemType.Spear
                case 73: // ItemType.Arrow
                    if ((dir & 1) == 1)
                    {
                        if (dir == 3 || dir == 5)
                        {
                            ovr025.load_missile_dax((dir == 5), 0, 1, var_1 + 1);
                        }
                        else
                        {
                            ovr025.load_missile_dax((dir == 7), 0, 0, var_1 + 1);
                        }
                    }
                    else
                    {
                        ovr025.load_missile_dax(false, 0, dir >> 2, var_1 + (dir % 4));
                    }
                    seg044.sound_sub_120E0(Sound.sound_c);
                    break;

                case 2: // ItemType.HandAxe
                case 7: // ItemType.Club
                case 14: // ItemType.Glaive
                    ovr025.load_missile_icons(var_1 + 3);
                    frame_count = 4;
                    delay = 50;
                    seg044.sound_sub_120E0(Sound.sound_9);
                    break;

                case 85:
                case 86:
                    ovr025.load_missile_icons(var_1 + 4);
                    frame_count = 4;
                    delay = 50;
                    seg044.sound_sub_120E0(Sound.sound_6);
                    break;

                case 101:
                case 47: // ItemType.Sling
                case 98:
                    var_1++;
                    ovr025.load_missile_dax(false, 0, 0, var_1 + 7);
                    ovr025.load_missile_dax(false, 1, 1, var_1 + 7);
                    frame_count = 2;
                    delay = 10;
                    seg044.sound_sub_120E0(Sound.sound_6);
                    break;

                default:
                    ovr025.load_missile_dax(false, 0, 0, var_1 + 7);
                    ovr025.load_missile_dax(false, 1, 1, var_1 + 7);
                    frame_count = 2;
                    delay = 20;
                    seg044.sound_sub_120E0(Sound.sound_9);
                    break;
            }

            ovr025.draw_missile_attack(delay, frame_count, ovr033.PlayerMapPos(target), ovr033.PlayerMapPos(attacker));
        }


        internal static void calc_enemy_health_percentage() /* sub_40E00 */
        {
            int maxTotal = 0;
            int currentTotal = 0;

            foreach (Player player in gbl.player_next_ptr)
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


        internal static int MaxOppositionMoves(Player arg_0) // sub_40E8F
        {
            int maxMoves = 0;

            foreach (Player player in gbl.player_next_ptr)
            {
                if (arg_0.OppositeTeam() == player.combat_team && player.in_combat == true)
                {
                    int moves = sub_3E124(player) / 2;

                    if (moves > maxMoves)
                    {
                        maxMoves = moves;
                    }
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
            else if (ovr027.yes_no(15, 10, 13, "Attack Ally: ") != 'Y')
            {
                result = false;
            }
            else
            {
                result = true;
                gbl.area2_ptr.field_666 = 1;

                foreach (Player player in gbl.player_next_ptr)
                {
                    if (player.health_status == Status.okey &&
                        player.field_F7 > 0x7F)
                    {
                        player.combat_team = CombatTeam.Enemy;
                        player.actions.target = null;
                    }
                }

                ovr025.CountCombatTeamMembers();
            }

            return result;
        }


        internal static char aim_sub_menu(byte arg_0, byte arg_2, byte arg_4, int maxRange, Player target, Player attacker) /* Aim_menu */
        {
            string text = string.Empty;
            int range = ovr025.getTargetRange(target, attacker);
            int direction = getTargetDirection(target, attacker);

            if (arg_4 != 0)
            {
                string range_txt = "Range = " + range.ToString() + "  ";
                seg041.displayString(range_txt, 0, 10, 0x17, 0);
            }

            if (range <= maxRange)
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
                else if (target != attacker)
                {
                    if (ovr025.is_weapon_ranged(attacker) == false)
                    {
                        text = "Target ";
                    }
                    else
                    {
                        Item dummyItem;
                        if (ovr025.sub_6906C(out dummyItem, attacker) == true &&
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

            char input_key = ovr027.displayInput(false, 1, 15, 10, 13, text, "Aim:");

            return input_key;
        }


        internal static void sub_411D8(Struct_1D183 arg_0, out bool arg_4, byte arg_8, Player target, Player attacker)
        {
            arg_4 = true;

            if (arg_8 == 1 &&
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

                if (arg_8 == 1)
                {
                    if (TrySweepAttack(target, attacker) == true)
                    {
                        arg_4 = true;
                        ovr025.clear_actions(attacker);
                    }
                    else
                    {
                        sub_3F94D(target, attacker);

                        Item var_5 = null;

                        if (ovr025.is_weapon_ranged(attacker) == true &&
                            ovr025.sub_6906C(out var_5, attacker) == true &&
                            ovr025.is_weapon_ranged_melee(attacker) == true &&
                            ovr025.getTargetRange(target, attacker) == 0)
                        {
                            var_5 = null;
                        }

                        sub_3F9DB(out arg_4, var_5, 0, target, attacker);
                    }
                }
            }
            else
            {
                arg_0.Clear();
            }
        }

        static Set asc_41342 = new Set(0x000B, new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20, 0x00, 0x10 });

        internal static void Target(Struct_1D183 arg_0, out bool arg_4, byte arg_8, byte arg_A, byte arg_C, int maxRange, Player target, Player player01)
        {
            Item dummyItem;

            arg_0.Clear();

            var pos = ovr033.PlayerMapPos(target);

            char input_key = ' ';
            byte dir = 8;

            arg_4 = false;

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

                if (ovr032.canReachTarget(gbl.mapToBackGroundTile, ref range, pos, ovr033.PlayerMapPos(player01)) == true)
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
                        arg_8 == 0)
                    {
                        can_target = false;
                    }

                    if (arg_C == 1)
                    {
                        if (player01 == target ||
                            (playerAtXY == 0 && groundTile == 0x1f))
                        {
                            can_target = false;
                        }
                        else if (ovr025.is_weapon_ranged(player01) == true &&
                             (ovr025.sub_6906C(out dummyItem, player01) == true ||
                             (ovr025.BuildNearTargets(1, player01).Count >= 0 &&
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

                input_key = ovr027.displayInput( false, 1, 15, 10, 13, var_29, "(Use Cursor keys) ");

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

                            if (arg_C == 1)
                            {
                                sub_411D8(arg_0, out arg_4, arg_C, arg_0.target, player01);
                            }
                            else
                            {
                                arg_4 = true;
                            }
                        }

                        if (can_target == false ||
                            arg_4 == false)
                        {
                            ovr033.sub_7431C(pos);
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
                        ovr033.sub_7431C(pos);
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
        }


        internal static SortedCombatant[] copy_sorted_players(Player player) /* sub_4188F */
        {
            var scl = ovr032.Rebuild_SortedCombatantList(gbl.mapToBackGroundTile, ovr033.PlayerMapSize(player), 0xff, 0x7F, ovr033.PlayerMapPos(player));

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
                ovr033.sub_7431C(attackerPos);
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

        static Set unk_41AE5 = new Set(0x0009, new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x20 });
        static Set unk_41B05 = new Set(0x0803, new byte[] { 0x80, 0xAB, 3 });

        internal static void aim_menu(Struct_1D183 arg_0, out bool arg_4, byte arg_8, byte arg_A, byte arg_C, int maxRange, Player attacker) /* sub_41B25 */
        {
            Player target; /* var_E5 */ 

            ovr025.load_missile_dax(false, 0, 0, 0x19);

            arg_0.Clear();

            arg_4 = false;

            if (maxRange == 0xff)
            {
                if (attacker.field_151 != null)
                {
                    maxRange = gbl.ItemDataTable[attacker.field_151.type].range - 1;
                }
                else
                {
                    maxRange = 1;
                }
            }

            if (maxRange == 0 ||
                maxRange == 0xff)
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
                    input = aim_sub_menu(arg_8, arg_A, arg_C, maxRange, target, attacker);

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
                                Target(arg_0, out arg_4, arg_8, arg_A, arg_C, maxRange, target, attacker);
                                ovr025.load_missile_dax(false, 0, 0, 0x19);

                                sorted_list = copy_sorted_players(attacker);
                                target_step = 0;
                                break;

                            case 'T':
                                sub_411D8(arg_0, out arg_4, arg_C, target, attacker);
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
                        Target(arg_0, out arg_4, arg_8, arg_A, arg_C, maxRange, target, attacker);
                        ovr025.load_missile_dax(false, 0, 0, 0x19);
                        sorted_list = copy_sorted_players(attacker);
                        target_step = 0;
                    }

                    ovr033.sub_7431C(ovr033.PlayerMapPos(target));

                    target = step_combat_list((arg_4 == false && unk_41AE5.MemberOf(input) == false), target_step,
                       ref list_index, ref attackerPos, sorted_list);
                }
            }

            if (arg_C != 0)
            {
                seg037.draw8x8_clear_area(0x17, 0x27, 0x17, 0);
            }
        }


        internal static bool find_target(bool clear_target, byte arg_2, int max_range, Player player) /* sub_41E44 */
        {
            byte var_7 = 0;
            bool target_found = false;
            byte var_5 = 0;

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

            while (target_found == false && var_5 == 0)
            {
                var_5 = var_7;

                if (var_7 != 0 && clear_target == false)
                {
                    gbl.mapToBackGroundTile.field_6 = true;
                }

                int tryCount = 20;
                var nearTargets = ovr025.BuildNearTargets(max_range, player);

                while (tryCount > 0 && target_found == false && nearTargets.Count > 0)
                {
                    tryCount--;
                    int roll = ovr024.roll_dice(nearTargets.Count, 1);

                    var epi = nearTargets[roll - 1];
                    target = epi.player;

                    if ((arg_2 != 0 && gbl.mapToBackGroundTile.field_6 == true) ||
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

                if (var_7 == 0)
                {
                    var_7 = 1;
                }
            }

            gbl.mapToBackGroundTile.field_6 = false;

            return target_found;
        }


        internal static void engulfs(Effect arg_0, object param, Player attacker)
        {
            Player target = attacker.actions.target;

            if (gbl.byte_1D2CA == 2 &&
                target.in_combat == true &&
                target.HasAffect(Affects.affect_3a) == false &&
                target.HasAffect(Affects.reduce) == false)
            {
                target = attacker.actions.target;
                ovr025.DisplayPlayerStatusString(true, 12, "engulfs " + target.name, attacker);
                ovr024.add_affect(false, ovr033.GetPlayerIndex(target), 0, Affects.affect_3a, target);

                ovr013.CallAffectTable(Effect.Add, null, target, Affects.affect_3a);
                ovr024.add_affect(false, ovr024.roll_dice(4, 2), 0, Affects.reduce, target);
                ovr024.add_affect(true, ovr033.GetPlayerIndex(target), 0, Affects.affect_8b, attacker);
            }
        }


        internal static void LoadMissleIconAndDraw(int icon_id, Player target, Player attacker) //sub_42159
        {
            ovr025.load_missile_icons(icon_id + 13);

            ovr025.draw_missile_attack(0x1E, 1, ovr033.PlayerMapPos(target), ovr033.PlayerMapPos(attacker));
        }


        internal static bool sub_421C1(bool clear_target, int range, Player player) // sub_421C1
        {
            bool var_5 = true;

            if (find_target(clear_target, 0, 0xff, player) == true)
            {
                var target = ovr033.PlayerMapPos(player.actions.target);

                if (ovr032.canReachTarget(gbl.mapToBackGroundTile, ref range, target, ovr033.PlayerMapPos(player)) == true)
                {
                    var_5 = false;
                }
            }

            return var_5;
        }


        internal static void attack_or_kill(Effect arg_0, object param, Player attacker)
        {
            int range = 0xFF; /* simeon */

            byte var_4 = 0;
            byte var_1 = 4;

            attacker.actions.target = null;
            sub_421C1(true, range, attacker);

            do
            {
                Player target = attacker.actions.target;

                range = ovr025.getTargetRange(target, attacker);
                var_1--;

                if (target != null)
                {
                    if (range == 2 && (var_4 & 1) == 0)
                    {
                        var_4 |= 1;

                        ovr025.DisplayPlayerStatusString(true, 10, "fires a disintegrate ray", attacker);
                        LoadMissleIconAndDraw(5, target, attacker);

                        if (ovr024.RollSavingThrow(0, SaveVerseType.type3, target) == false)
                        {
                            ovr024.KillPlayer("is disintergrated", Status.gone, target);
                        }

                        sub_421C1(false, range, attacker);
                    }
                    else if (range == 3 && (var_4 & 2) == 0)
                    {
                        var_4 |= 2;

                        ovr025.DisplayPlayerStatusString(true, 10, "fires a stone to flesh ray", attacker);
                        LoadMissleIconAndDraw(10, target, attacker);

                        if (ovr024.RollSavingThrow(0, SaveVerseType.type1, target) == false)
                        {
                            ovr024.KillPlayer("is Stoned", Status.stoned, target);
                        }

                        sub_421C1(false, range, attacker);
                    }
                    else if (range == 4 && (var_4 & 4) == 0)
                    {
                        var_4 |= 4;

                        ovr025.DisplayPlayerStatusString(true, 10, "fires a death ray", attacker);
                        LoadMissleIconAndDraw(5, target, attacker);

                        if (ovr024.RollSavingThrow(0, 0, target) == false)
                        {
                            ovr024.KillPlayer("is killed", Status.dead, target);
                        }

                        sub_421C1(false, range, attacker);
                    }
                    else if (range == 5 && (var_4 & 8) == 0)
                    {
                        var_4 |= 8;

                        ovr025.DisplayPlayerStatusString(true, 10, "wounds you", attacker);
                        LoadMissleIconAndDraw(5, target, attacker);

                        ovr024.damage_person(false, 0, ovr024.roll_dice_save(8, 2) + 1, target);
                        sub_421C1(false, range, attacker);
                    }
                    else if ((var_4 & 0x10) == 0)
                    {
                        ovr023.sub_5D2E1(1, QuickFight.True, 0x54);
                        var_4 |= 0x10;
                    }
                    else if ((var_4 & 0x20) == 0)
                    {
                        ovr023.sub_5D2E1(1, QuickFight.True, 0x37);
                        var_4 |= 0x20;
                    }
                    else if ((var_4 & 0x40) == 0)
                    {
                        ovr023.sub_5D2E1(1, QuickFight.True, 0x15);
                        var_4 |= 0x40;
                    }
                }
            } while (var_1 > 0 && attacker.actions.target != null);
        }


        internal static void sub_425C6(Effect add_remove, object param, Player player)
        {
            Affect affect = (Affect)param;

            gbl.spell_target = gbl.player_array[affect.affect_data];

            if (add_remove == Effect.Remove ||
                player.in_combat == false ||
                gbl.spell_target.in_combat == false)
            {
                ovr024.remove_affect(null, Affects.affect_3a, gbl.spell_target);
                ovr024.remove_affect(null, Affects.reduce, gbl.spell_target);

                if (add_remove == Effect.Add)
                {
                    affect.callAffectTable = false;

                    ovr024.remove_affect(affect, Affects.affect_8b, player);
                }
            }
            else
            {
                player.field_19C = 2;
                player.field_19D = 0;
                player.attack_dice_count = 2;
                player.attack_dice_size = 8;

                sub_3F9DB(1, gbl.spell_target, player);

                ovr025.clear_actions(player);

                if (gbl.spell_target.in_combat == false)
                {
                    ovr024.remove_affect(null, Affects.affect_8b, player);
                    ovr024.remove_affect(null, Affects.affect_3a, gbl.spell_target);
                    ovr024.remove_affect(null, Affects.reduce, gbl.spell_target);
                }
            }
        }


        internal static void sub_426FC(Effect arg_0, object param, Player player)
        {
            Affect affect = (Affect)param;

            gbl.spell_target = gbl.player_array[affect.affect_data];

            if (arg_0 == Effect.Remove ||
                player.in_combat == false ||
                gbl.spell_target.in_combat == false)
            {
                ovr024.remove_affect(null, Affects.affect_3a, gbl.spell_target);
                if (arg_0 == Effect.Add)
                {
                    affect.callAffectTable = false;
                    ovr024.remove_affect(affect, Affects.affect_90, player);
                }
            }
            else
            {
                player.field_19C = 1;
                player.field_19D = 0;
                player.attack_dice_count = 2;
                player.attack_dice_size = 8;
                

                sub_3F9DB(2, gbl.spell_target, player);

                ovr025.clear_actions(player);

                if (gbl.spell_target.in_combat == false)
                {
                    ovr024.remove_affect(null, Affects.affect_90, player);
                    ovr024.remove_affect(null, Affects.affect_3a, gbl.spell_target);
                }
            }
        }


        internal static void hugs(Effect arg_0, object param, Player player)
        {
            if (gbl.attack_roll >= 18)
            {
                gbl.spell_target = player.actions.target;
                ovr025.DisplayPlayerStatusString(true, 12, "hugs " + gbl.spell_target.name, player);

                ovr024.add_affect(false, ovr033.GetPlayerIndex(gbl.spell_target), 0, Affects.affect_3a, gbl.spell_target);
                ovr013.CallAffectTable(Effect.Add, null, gbl.spell_target, Affects.affect_3a);

                ovr024.add_affect(true, ovr033.GetPlayerIndex(gbl.spell_target), 0, Affects.affect_90, player);
            }
        }


        internal static bool god_intervene()
        {
            bool intervened = false;

            if (Cheats.allow_gods_intervene)
            {
                intervened = true;
                ovr025.string_print01("The Gods intervene!");

                foreach (Player player in gbl.player_next_ptr)
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
