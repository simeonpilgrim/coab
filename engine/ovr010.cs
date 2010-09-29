using Classes;

namespace engine
{
    class ovr010
    {
        internal static void PlayerQuickFight(Player player) // sub_3504B
        {
            bool var_2 = process_input_in_monsters_turn(player);
            ovr027.ClearPromptArea();
            ovr025.ClearPlayerTextArea();

            if (player.in_combat == false)
            {
                var_2 = true;
                ovr025.clear_actions(player);
            }

            int var_1 = player.actions.field_15;

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
                var_2 = FleeCheck_001(player);
            }

            if (player.actions.moral_failure == true &&
                player.actions.fleeing == false)
            {
                ovr025.DisplayPlayerStatusString(true, 10, "flees in panic", player);
            }

            if (var_2 == true)
            {
                return;
            }

            if (sub_354AA(player))
            {
                ovr025.clear_actions(player);
                return;
            }

            if (player.actions.spell_id > 0)
            {
                ovr023.sub_5D2E1(1, QuickFight.True, player.actions.spell_id);

                ovr025.clear_actions(player);
                return;
            }

            if (turn_undead(player))
            {
                ovr025.clear_actions(player);
                return;
            }

            if (sub_3560B(player) == true)
            {
                return;
            }

            AI_items_selection(player);
            var_2 = process_input_in_monsters_turn(player);

            while (var_2 == false)
            {
                if (ovr014.find_target(false, 1, 0xff, player) == true &&
                    player.actions.delay > 0 &&
                    player.in_combat == true)
                {
                    var_2 = sub_35DB1(player);
                }
                else
                {
                    var_2 = true;
                    TryGuarding(player);
                }
            }
        }


        internal static bool turn_undead(Player player)
        {
            Player var_5;

            if (player.actions.hasTurnedUndead == false &&
                (player.cleric_lvl > 0 || player.cleric_old_lvl > player.multiclassLevel) &&
                ovr014.FindLowestE9Target(out var_5, player) == true)
            {
                ovr014.turns_undead(player);
                return true;
            }
            else
            {
                return false;
            }
        }


        internal static bool sub_352AF(int spell_id, Point pos) // sub_352AF
        {
            bool result = false;
            var spell_entry = gbl.spellCastingTable[spell_id];

            if (spell_entry.damageOnSave != DamageOnSave.Zero)
            {
                int save_bonus = (gbl.SelectedPlayer.combat_team == CombatTeam.Ours) ? -2 : 8;
                var opp = gbl.SelectedPlayer.OppositeTeam();

                var sortedCombatants = ovr032.Rebuild_SortedCombatantList(1, spell_entry.field_F, pos, p => p.combat_team != opp);

                foreach (var sc in sortedCombatants)
                {
                    Player tmpPlayer = sc.player;

                    if (ovr024.RollSavingThrow(save_bonus, spell_entry.saveVerse, sc.player) == false)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }


        internal static bool ShouldCastSpellX(int minPriority, int spellId, Player attacker) // sub_353B1
        {
            var spell_entry = gbl.spellCastingTable[spellId];
            if (spell_entry.priority >= minPriority)
            {
                Player dummy_target;
                if ((spellId != 3 && spell_entry.field_E == 0) ||
                    (spellId == 3 && ovr014.find_healing_target(out dummy_target, attacker)))
                {
                    return true;
                }
                else
                {
                    var nearTargets = ovr025.BuildNearTargets(ovr023.SpellRange(spellId), attacker);

                    if (nearTargets.Count > 0)
                    {
                        if (spell_entry.field_F == 0)
                        {
                            return true;
                        }
                        else
                        {
                            foreach (var cpi in nearTargets)
                            {
                                if (sub_352AF(spellId, cpi.pos) == true)
                                {
                                    return false;
                                }
                            }
                            return true;
                        }
                    }
                }
            }

            return false;
        }


        internal static bool sub_354AA(Player player)
        {
            Item bestWand = null;

            int teamCount = player.OppositeTeam() == CombatTeam.Ours ? gbl.friends_count : gbl.foe_count;
            if (player.actions.can_use == true &&
                teamCount > 0 &&
                gbl.area_ptr.can_cast_spells == false)
            {
                int prioritisToTry = ovr024.roll_dice(7, 1);
                for (int i = 0; i < prioritisToTry; i++)
                {
                    int priority = 7 - i;

                    foreach (var item_ptr in player.items)
                    {
                        byte spell_id = (byte)item_ptr.affect_2;

                        if (item_ptr.IsScroll() == false &&
                            (int)item_ptr.affect_3 < 0x80 &&
                            item_ptr.readied &&
                            spell_id > 0)
                        {
                            if (spell_id > 0x38)
                            {
                                spell_id -= 0x17;
                            }

                            if (ShouldCastSpellX(priority, spell_id, player))
                            {
                                bestWand = item_ptr;
                                break;
                            }
                        }
                    }
                }
            }

            if (bestWand != null)
            {
                bool var_15 = false; /* simeon */
                ovr020.sub_56478(ref var_15, bestWand);
                return true;
            }

            return false;
        }


        internal static bool sub_3560B(Player player)
        {
            byte[] spell_list = new byte[gbl.max_spells];

            int spells_count = 0;

            if (player.actions.can_cast == true)
            {
                foreach (int id in player.spellList.LearntList())
                {
                    spell_list[spells_count++] = (byte)id;
                }
            }

            byte spell_id = 0;
            byte priority = 7;
            int var_5B = ovr024.roll_dice(7, 1);
            int var_5D = 1;


            if (spells_count > 0 &&
                (player.control_morale >= Control.NPC_Base || gbl.AutoPCsCastMagic == true))
            {
                if ((player.OppositeTeam() == CombatTeam.Ours ? gbl.friends_count : gbl.foe_count) > 0)
                {
                    while (var_5D <= var_5B && spell_id == 0)
                    {
                        for (int var_5E = 1; var_5E < 4 && spell_id == 0; var_5E++)
                        {
                            int random_spell_index = ovr024.roll_dice(spells_count, 1) - 1;
                            byte random_spell_id = spell_list[random_spell_index];

                            if (ShouldCastSpellX(priority, random_spell_id, player))
                            {
                                spell_id = random_spell_id;
                            }
                        }

                        priority--;
                        var_5D++;
                    }
                }
            }

            bool casting_spell;

            if (spell_id > 0)
            {
                ovr014.spell_menu3(out casting_spell, QuickFight.True, spell_id);
            }
            else
            {
                casting_spell = false;
            }

            return casting_spell;
        }

        static int[] data_2B8 = new int[]{ 
0, 8, 7, 6, 1, 2, 8, 1, 2, 7,
6, 7, 1, 8, 6, 2, 1, 7, 8, 2,
6, 8, 7, 6, 5, 4, 8, 1, 2, 3,  
4, 8, 4, 6, 2, 8 ,6, 4, 0, 8,
0, 6, 2, 8, 2, 0, 4, 0, 0, 2,
6, 2, 2, 0, 4, 4, 4, 2, 6, 6 };/* actual from seg600:02BD - seg600:02F8 */

        internal static bool CanMove(out bool groundClear, int baseDirecction, int dirStep, Player player) // sub_3573B
        {
            groundClear = false;
            bool canMove = false;

            int var_6 = data_2B8[(player.actions.field_15 * 5) + dirStep];
            byte playerDirection = (byte)((baseDirecction + var_6) % 8);

            int groundTile;
            int playerIndex;
            bool isPoisonousCloud;
            bool isNoxiousCloud;
            ovr033.getGroundInformation(out isPoisonousCloud, out isNoxiousCloud, out groundTile, out playerIndex, playerDirection, player);

            if (groundTile == 0)
            {
                groundClear = true;
            }
            else
            {
                if (gbl.BackGroundTiles[groundTile].move_cost == 0xff)
                {
                    return false;
                }

                int move_cost = gbl.BackGroundTiles[groundTile].move_cost;
                if ((playerDirection & 1) != 0)
                {
                    move_cost *= 3;
                }
                else
                {
                    move_cost *= 2;
                }

                if (playerIndex == 0 && move_cost < player.actions.move)
                {
                    if (isNoxiousCloud == true &&
                        player.HasAffect(Affects.animate_dead) == false &&
                        player.HasAffect(Affects.stinking_cloud) == false &&
                        player.HasAffect(Affects.affect_6f) == false &&
                        player.HasAffect(Affects.affect_7d) == false &&
                        player.HasAffect(Affects.protect_magic) == false &&
                        player.HasAffect(Affects.minor_globe_of_invulnerability) == false &&
                        player.actions.fleeing == false)
                    {
                        if (ovr024.RollSavingThrow(0, 0, player) == false)
                        {
                            move_cost = player.actions.move + 1;
                        }
                    }

                    if (isPoisonousCloud == true &&
                        player.HitDice < 7 &&
                        player.HasAffect(Affects.protect_magic) == false &&
                        player.HasAffect(Affects.affect_6f) == false &&
                        player.HasAffect(Affects.affect_85) == false &&
                        player.HasAffect(Affects.affect_7d) == false &&
                        player.actions.fleeing == false)
                    {
                        move_cost = player.actions.move + 1;
                    }

                    if (player.actions.move >= move_cost)
                    {
                        canMove = true;
                    }
                }
            }

            return canMove;
        }


        internal static void moralFailureEscape(Player player) // sub_359D1
        {
            int var_2 = 0; /* Simeon */
            int dir;

            string prompt = string.Format("Move/Attack, Move Left = {0} ", player.actions.move / 2);

            seg041.displayString(prompt, 0, 10, 0x18, 0);

            if (process_input_in_monsters_turn(player))
            {
                return;
            }

            if ((player.actions.move / 2) > 0 &&
                player.actions.delay > 0)
            {
                if (player.control_morale < Control.NPC_Base ||
                   (player.control_morale >= Control.NPC_Base && gbl.enemyHealthPercentage <= (ovr024.roll_dice(100, 1) + gbl.monster_morale)) ||
                    player.combat_team == CombatTeam.Enemy)
                {
                    if (player.actions.moral_failure == true ||
                        player.armor != null ||
                        player._class != ClassId.magic_user)
                    {
                        if (player.actions.moral_failure == false)
                        {
                            dir = ovr014.getTargetDirection(player.actions.target, player);
                        }
                        else
                        {
                            player.actions.field_15 = ovr024.roll_dice(2, 1);
                            dir = gbl.mapDirection - (((gbl.mapDirection + 2) % 4) / 2) + 8;

                            if (player.combat_team == CombatTeam.Ours)
                            {
                                dir += 4;
                            }

                            dir %= 8;
                        }

                        bool zeroTitle = false;
                        bool var_5 = false;
                        int dirStep = 1;

                        while (dirStep < 6 && var_5 == false &&
                            CanMove(out zeroTitle, dir, dirStep, player) == false)
                        {
                            if (player.actions.moral_failure == true &&
                                zeroTitle == true)
                            {
                                var_5 = true;
                                ovr014.flee_battle(player);
                            }
                            else
                            {
                                dirStep++;
                            }
                        }

                        if (var_5 == true)
                        {
                            player.actions.move = 0;
                            player.actions.moral_failure = false;
                            ovr025.clear_actions(player);
                        }
                        else
                        {
                            var_2 = (data_2B8[(player.actions.field_15 * 5) + dirStep] + dir) % 8;

                            if (dirStep == 6 || ((var_2 + 4) % 8) == byte_1AB18)
                            {
                                byte_1AB19++;
                                player.actions.field_15 = (player.actions.field_15 % 6) + 1;

                                if (byte_1AB19 > 1)
                                {
                                    player.actions.target = null;

                                    if (byte_1AB19 > 2)
                                    {
                                        player.actions.move = 0;
                                        var_5 = true;
                                    }
                                    else if (ovr014.find_target(false, 1, 0xFF, player) == false)
                                    {
                                        var_5 = true;
                                        TryGuarding(player);
                                    }
                                }
                            }

                            if (dirStep < 6)
                            {
                                byte_1AB18 = var_2;
                            }
                            else
                            {
                                var_5 = true;
                            }
                        }

                        if (var_5 == false)
                        {
                            gbl.focusCombatAreaOnPlayer = (gbl.byte_1D90E || ovr033.PlayerOnScreen(false, player) || player.combat_team == CombatTeam.Ours);

                            ovr033.draw_74B3F(0, 0, var_2, player);
                            ovr014.move_step_away_attack(player.actions.direction, player);

                            if (player.in_combat == false)
                            {
                                var_5 = true;
                                ovr025.clear_actions(player);
                            }
                            else
                            {
                                if (player.actions.move > 0)
                                {
                                    ovr014.sub_3E748(player.actions.direction, player);
                                }

                                if (player.in_combat == false)
                                {
                                    var_5 = true;
                                    ovr025.clear_actions(player);
                                }

                                ovr024.in_poison_cloud(1, player);
                            }
                        }
                        return;
                    }
                }
            }

            TryGuarding(player);
        }

        static int byte_1AB18; // byte_1AB18
        static int byte_1AB19; // byte_1AB19

        internal static bool sub_35DB1(Player player)
        {
            byte_1AB18 = 8;
            byte_1AB19 = 0;

            ovr024.CheckAffectsEffect(player, CheckType.Type_14);

            if (player.combat_team == CombatTeam.Ours &&
                ovr025.bandage(true) == true)
            {
                player.actions.delay = 0;
            }

            int counter = 0;
            bool stop = false;
            bool delayed = player.actions.delay != 0;

            while (stop == false && delayed == true)
            {
                if (player.actions.moral_failure == true)
                {
                    while (player.actions.move > 0 &&
                        player.actions.delay > 0 &&
                        player.actions.delay < 20)
                    {
                        moralFailureEscape(player);
                    }
                }

                if (player.actions.delay == 0 ||
                    player.actions.delay == 20)
                {
                    delayed = false;
                }
                else
                {
                    stop = false;
                }

                if (stop == false && delayed == true)
                {
                    counter++;

                    if (counter > 20)
                    {
                        stop = true;
                        delayed = false;
                        TryGuarding(player);
                    }

                    gbl.byte_1D90E = false;
                    int range = 1;

                    if (player.field_151 != null)
                    {
                        range = gbl.ItemDataTable[player.field_151.type].range - 1;
                    }

                    if (range == 0 || range == 0xff || range == -1)
                    {
                        range = 1;
                    }

                    Player target = player.actions.target;

                    if (target != null &&
                        (target.in_combat == false ||
                        target.combat_team == CombatTeam.Ours))
                    {
                        target = null;
                    }

                    if (target != null &&
                        ovr014.CanSeeTargetA(target, player) == true)
                    {
                        var targetPos = ovr033.PlayerMapPos(target);
                        var attackPos = ovr033.PlayerMapPos(player);

                        int steps = range;

                        gbl.mapToBackGroundTile.field_6 = false;

                        if (ovr032.canReachTarget(ref steps, targetPos, attackPos) == true &&
                            (steps / 2) <= range)
                        {
                            gbl.byte_1D90E = true;
                        }
                    }

                    if (gbl.byte_1D90E == false)
                    {
                        var nearTargets = ovr025.BuildNearTargets(range, player);

                        if (nearTargets.Count == 0)
                        {
                            if (ovr014.find_target(false, 0, 0xff, player) == true)
                            {
                                moralFailureEscape(player);
                            }
                            else
                            {
                                stop = true;
                                TryGuarding(player);
                            }
                        }
                        else
                        {
                            int roll = ovr024.roll_dice(nearTargets.Count, 1);

                            target = nearTargets[roll - 1].player;

                            if (ovr025.is_weapon_ranged(player) == true &&
                                ovr025.is_weapon_ranged_melee(player) == false &&
                                ovr025.BuildNearTargets(1, player).Count > 0)
                            {
                                AI_items_selection(player);
                                stop = true;
                            }
                            else if (ovr025.getTargetRange(target, player) == 1 ||
                                ovr014.CanSeeTargetA(target, player) == true)
                            {
                                gbl.byte_1D90E = true;
                            }
                        }
                    }

                    if (gbl.byte_1D90E == true)
                    {
                        ovr033.redrawCombatArea(ovr014.getTargetDirection(target, player), 2, ovr033.PlayerMapPos(player));
                    }

                    if (gbl.byte_1D90E == true)
                    {
                        if (ovr014.TrySweepAttack(target, player) == true)
                        {
                            stop = true;
                            ovr025.clear_actions(player);
                        }
                        else
                        {
                            ovr014.recalc_action_12(target, player);

                            Item item = null;

                            if (ovr025.is_weapon_ranged(player) == true)
                            {
                                gbl.byte_1D90E = ovr025.GetCurrentAttackItem(out item, player);

                                if (ovr025.is_weapon_ranged_melee(player) == true &&
                                    ovr025.getTargetRange(target, player) == 1)
                                {
                                    item = null;
                                }
                            }

                            ovr014.AttackTarget(out stop, item, 0, target, player);

                            if (stop == true)
                            {
                                delayed = false;
                            }
                            else if (target.in_combat == false)
                            {
                                stop = true;
                            }
                        }
                    }
                }
            }

            return (delayed == false);
        }


        static void TryGuarding(Player player) // sub_361F7
        {
            ovr025.ClearPlayerTextArea();

            if (player.IsHeld() == true ||
                ovr025.is_weapon_ranged(player) == true ||
                player.actions.delay == 0)
            {
                player.actions.Clear();
            }
            else
            {
                ovr025.guarding(player);
            }
        }


        /// <summary>
        /// processes keyboard input during combat. Returns if current player is user controlled
        /// </summary>
        static bool process_input_in_monsters_turn(Player player) /* sub_36269 */
        {
            bool player_turn = false;

            if (seg049.KEYPRESSED() == true)
            {
                byte var_6 = seg043.GetInputKey();

                if (var_6 == 0)
                {
                    var_6 = seg043.GetInputKey();
                }

                if (var_6 == '2')
                {
                    gbl.AutoPCsCastMagic = !gbl.AutoPCsCastMagic;

                    if (gbl.AutoPCsCastMagic == true)
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
                    foreach (Player player_ptr in gbl.TeamList)
                    {
                        if (player_ptr.control_morale < Control.NPC_Base &&
                            player_ptr.health_status != Status.animated)
                        {
                            player_ptr.quick_fight = QuickFight.False;
                        }
                    }

                    if (player.quick_fight == QuickFight.False)
                    {
                        player.actions.delay = 20;
                        player_turn = true;
                    }
                }
                else if (var_6 == '-')
                {
                    ovr014.god_intervene();
                }
            }

            seg043.clear_keyboard();

            return player_turn;
        }


        static bool FleeCheck_001(Player player) // sub_3637F
        {
            bool var_1 = false;
            player.actions.moral_failure = false;

            ovr024.RemoveAttackersAffects(player);

            if (player.actions.fleeing == true)
            {
                player.actions.moral_failure = true;
                ovr025.DisplayPlayerStatusString(true, 10, "is forced to flee", player);
            }
            else if (player.control_morale >= Control.NPC_Base)
            {
                gbl.monster_morale = (player.control_morale & Control.PC_Mask) << 1;

                if (gbl.monster_morale > 102)
                {
                    gbl.monster_morale = 0;
                }
                ovr024.CheckAffectsEffect(player, CheckType.Morale);

                if (gbl.monster_morale < (100 - ((player.hit_point_current * 100) / player.hit_point_max)) ||
                    gbl.monster_morale == 0)
                {
                    //byte var_3 = gbl.byte_1D2CC;
                    gbl.monster_morale = gbl.enemyHealthPercentage;

                    ovr024.CheckAffectsEffect(player, CheckType.Morale);

                    if (gbl.monster_morale < (100 - gbl.area2_ptr.field_58C) ||
                        gbl.monster_morale == 0 ||
                        player.combat_team == CombatTeam.Ours)
                    {
                        int var_2 = ovr014.MaxOppositionMoves(player);

                        if (var_2 <= (ovr014.CalcMoves(player) / 2))
                        {
                            player.actions.moral_failure = true;
                            ovr024.remove_affect(null, Affects.affect_4a, player);
                            ovr024.remove_affect(null, Affects.weap_dragon_slayer, player);
                        }
                        else if (player._int > 5)
                        {
                            ovr024.RemoveFromCombat("Surrenders", Status.unconscious, player);

                            var_1 = true;
                            ovr025.clear_actions(player);
                        }
                    }
                }
            }

            return var_1;
        }


        static int CalcItemPowerRating(Item item, Player player) // sub_36535
        {
            ItemData itemData = gbl.ItemDataTable[item.type];

            int rating = itemData.diceSizeNormal * itemData.diceCountNormal;

            if (item.plus > 0)
            {
                rating += item.plus * 8;
            }

            if (itemData.bonusNormal > 0)
            {
                rating += itemData.bonusNormal * 2;
            }

            if (item.type == ItemType.Type_85 &&
                player.actions.target != null &&
                player.actions.target.field_E9 > 0)
            {
                rating = 8;
            }

            if ((itemData.field_E & ItemDataFlags.flag_08) > 0)
            {
                rating += (itemData.numberAttacks - 1) * 2;
            }

            if (itemData.handsCount <= 1)
            {
                rating += 3;
            }

            if ((itemData.handsCount + player.field_185) > 3)
            {
                rating = 0;
            }

            if (item.affect_3 == Affects.cast_throw_lightening &&
                ((int)item.affect_2 & 0x0f) != player.alignment)
            {
                rating = 0;
            }

            if (item.affect_2 == Affects.paralizing_gaze)
            {
                rating = 0;
            }

            if (item.cursed == true)
            {
                rating = 0;
            }

            return rating;
        }


        static void AI_items_selection(Player player)  // sub_36673 
        {
            if (player.field_151 != null)
            {
                player.field_185 -= gbl.ItemDataTable[player.field_151.type].handsCount;
            }

            if (player.field_155 != null)
            {
                player.field_185 -= gbl.ItemDataTable[player.field_155.type].handsCount;
            }

            Item var_4 = null;
            Item var_8 = null;
            int var_15 = 1;

            int var_16 = player.attack1_DiceSizeBase * player.attack1_DiceCountBase;

            if (player.attack1_DamageBonusBase > 0)
            {
                var_16 += player.attack1_DamageBonusBase * 2;
            }

            int max_bonus = 0;
            Item best_weapon = null;

            foreach (Item item in player.items)
            {
                ItemType item_type = item.type;

                if (gbl.ItemDataTable[item_type].item_slot == ItemSlot.slot_0 &&
                    (gbl.ItemDataTable[item_type].classFlags & player.classFlags) != 0)
                {
                    int power_rating = CalcItemPowerRating(item, player);

                    if ((gbl.ItemDataTable[item_type].field_E & ItemDataFlags.flag_08) != 0 ||
                        (gbl.ItemDataTable[item_type].field_E & ItemDataFlags.flag_10) != 0)
                    {
                        if (power_rating > var_15)
                        {
                            var_4 = item;
                            var_15 = power_rating;
                        }
                    }

                    if ((gbl.ItemDataTable[item_type].field_E & ItemDataFlags.flag_08) == 0 &&
                        power_rating > var_16)
                    {
                        var_8 = item;
                        var_16 = power_rating;
                    }
                }


                if (gbl.ItemDataTable[item_type].item_slot == ItemSlot.slot_1)
                {
                    if ((gbl.ItemDataTable[item_type].classFlags & player.classFlags) != 0)
                    {
                        int bonus = item.plus >= 0 ? item.plus + 1 : 0;

                        if (bonus > max_bonus)
                        {
                            best_weapon = item;
                            max_bonus = bonus;
                        }
                    }
                }
            }

            bool ranged_melee = ovr025.item_is_ranged_melee(var_4);
            bool var_1F = false;
            Item tmpItem = null;
            var itemFlags = ItemDataFlags.None;

            if (var_4 != null)
            {
                itemFlags = gbl.ItemDataTable[var_4.type].field_E;

                if ((itemFlags & ItemDataFlags.flag_10) != 0)
                {
                    tmpItem = var_4;
                }

                if ((itemFlags & ItemDataFlags.flag_08) != 0)
                {
                    if ((itemFlags & ItemDataFlags.arrows) != 0)
                    {
                        tmpItem = player.arrows;
                    }

                    if ((itemFlags & ItemDataFlags.quarrels) != 0)
                    {
                        tmpItem = player.quarrels;
                    }
                }
            }

            if (tmpItem != null ||
                itemFlags == (ItemDataFlags.flag_02 | ItemDataFlags.flag_08))
            {
                var_1F = true;
            }

            Item weapon;

            if (var_4 != null &&
                var_15 > (var_16 >> 1) &&
                var_1F == true &&
                (ranged_melee == true || ovr025.BuildNearTargets(1, player).Count == 0))
            {
                weapon = var_4;
            }
            else
            {
                weapon = var_8;
            }

            bool itemsChanged = false;
            bool replace_weapon = true;

            if (player.field_151 != null &&
                (player.field_151 == weapon ||
                 player.field_151.cursed == true))
            {
                replace_weapon = false;
            }

            if (replace_weapon)
            {
                if (player.field_151 != null)
                {
                    ovr020.ready_Item(player.field_151);
                }

                ovr025.reclac_player_values(player);

                if (player.field_155 != null &&
                    player.field_155.cursed == false)
                {
                    player.field_185 -= gbl.ItemDataTable[player.field_155.type].handsCount;
                }

                if (weapon != null)
                {
                    ovr020.ready_Item(weapon);
                }

                itemsChanged = true;
            }

            ovr025.reclac_player_values(player);
            ovr014.reclac_attacks(player);
            replace_weapon = true;

            if (player.field_155 != null &&
                (player.field_155 == best_weapon || player.field_155.cursed == true))
            {
                replace_weapon = false;
            }

            if (player.field_185 > 2)
            {
                if (player.field_155 == null ||
                    player.field_155.cursed == true)
                {
                    ovr020.ready_Item(weapon);
                    itemsChanged = true;
                }
                else
                {
                    ovr020.ready_Item(player.field_155);
                    itemsChanged = true;
                }
            }
            else if (player.field_185 < 2 && replace_weapon)
            {
                if (player.field_155 != null)
                {
                    ovr020.ready_Item(player.field_155);
                }
                ovr025.reclac_player_values(player);

                if (best_weapon != null)
                {
                    ovr020.ready_Item(best_weapon);
                }

                itemsChanged = true;
            }


            ovr025.reclac_player_values(player);

            if (itemsChanged == true)
            {
                ovr025.CombatDisplayPlayerSummary(player);
            }
        }
    }
}
