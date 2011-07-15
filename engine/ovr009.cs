
using Classes;
using Classes.Combat;

namespace engine
{
    class ovr009
    {
        internal static void free_combat_stuff() /* sub_3304B */
        {
            gbl.NoxiousCloud.Clear();
            gbl.PoisonousCloud.Clear();

            gbl.mapToBackGroundTile = null;

            gbl.missile_dax = null;
            ovr033.Color_0_8_normal();
            gbl.dword_1D5CA = ovr023.NonCombatSpellCast;
        }


        internal static void MainCombatLoop() //sub_33100
        {
            gbl.game_state = GameState.Combat;
            gbl.dword_1D5CA = new spellDelegate(ovr014.target);
            ovr011.BattleSetup();
            bool end_combat = false;

            if (gbl.friends_count == 0 ||
                gbl.foe_count == 0)
            {
                end_combat = true;
            }

            while (end_combat == false)
            {
                ovr025.CountCombatTeamMembers();

                foreach (Player player in gbl.TeamList)
                {
                    ovr014.CalculateInitiative(player);
                }

                gbl.area2_ptr.field_596 = 0;

                foreach (Player player in FindNextCombatant())
                {
                    DoPlayerCombatTurn(player);
                }

                end_combat = BattleRoundChecks();
            }

            free_combat_stuff();
            gbl.DelayBetweenCharacters = true;
        }


        internal static System.Collections.Generic.IEnumerable<Player> FindNextCombatant() /* sub_331BC */
        {
            Player output_player;

            do
            {
                output_player = null;

                int max_delay = 0;
                int max_roll = 0;

                foreach (Player player in gbl.TeamList)
                {
                    int roll = ovr024.roll_dice(100, 1);

                    if (player.actions.delay > max_delay)
                    {
                        max_roll = roll;
                    }

                    if (player.actions.delay >= max_delay &&
                        roll >= max_roll)
                    {
                        max_roll = roll;
                        max_delay = player.actions.delay;

                        output_player = player;
                    }
                }

                if (max_delay == 0)
                {
                    output_player = null;
                }

                if (output_player != null)
                {
                    yield return output_player;
                }

            } while (output_player != null);
        }


        internal static void DoPlayerCombatTurn(Player player) // sub_33281
        {
            player.actions.AttacksReceived = 0;
            player.actions.directionChanges = 0;
            player.actions.guarding = false;
            ovr024.CheckAffectsEffect(player, CheckType.PlayerRestrained);

            if (player.actions.delay > 0)
            {
                if (player.actions.delay == 20)
                {
                    player.actions.delay = 19;
                }

                gbl.SelectedPlayer = player;

                gbl.focusCombatAreaOnPlayer = ((player.combat_team == CombatTeam.Ours) || (ovr033.PlayerOnScreen(false, player) == true));

                ovr033.RedrawCombatIfFocusOn(true, 2, player);
                ovr025.reclac_player_values(player);
                gbl.display_hitpoints_ac = true;
                ovr025.CombatDisplayPlayerSummary(player);
                ovr024.CheckAffectsEffect(player, CheckType.Type_15);

                if (player.actions.spell_id == 0)
                {
                    ovr024.CheckAffectsEffect(player, CheckType.Confusion);
                }

                if (player.actions.delay > 0)
                {
                    if (player.quick_fight == QuickFight.True)
                    {
                        ovr010.PlayerQuickFight(player);
                    }
                    else
                    {
                        combat_menu(player);
                    }
                }

                ovr033.RedrawPosition(ovr033.PlayerMapPos(player));
            }
        }


        internal static void combat_menu(Player player) /* camp_menu */
        {
            int spell_id;
            Struct_1D183 var_D = new Struct_1D183();
            char var_1;

            if (player.in_combat == true)
            {
                if (player.actions.spell_id > 0)
                {
                    spell_id = player.actions.spell_id;
                    player.actions.spell_id = 0;

                    ovr023.sub_5D2E1(1, QuickFight.False, spell_id);
                    ovr025.clear_actions(player);
                }
                else
                {
                    bool var_2 = false;

                    while (var_2 == false)
                    {
                        combat_menu(out var_1, player);

                        if (gbl.displayInput_specialKeyPressed == false)
                        {
                            switch (var_1)
                            {
                                case 'Q':
                                    SetPlayerQuickFight(player);
                                    ovr027.ClearPromptArea();
                                    seg043.clear_keyboard();
                                    seg049.SysDelay(0x0C8);
                                    var_2 = true;
                                    ovr010.PlayerQuickFight(player);
                                    break;

                                case 'M':
                                    sub_33B26(ref var_2, ' ', player);
                                    break;

                                case 'V':
                                    var_2 = ovr020.viewPlayer();
                                    ovr014.reclac_attacks(player);
                                    if (var_2 == false)
                                    {
                                        ovr025.RedrawCombatScreen();
                                    }
                                    break;

                                case 'A':
                                    var_2 = ovr014.aim_menu(var_D, true, false, true, -1, player);
                                    break;

                                case 'U':
                                    gbl.menuSelectedWord = 2;
                                    ovr020.PlayerItemsMenu(ref var_2);
                                    ovr014.reclac_attacks(player);
                                    if (var_2 == false)
                                    {
                                        ovr025.RedrawCombatScreen();
                                    }
                                    break;

                                case 'C':
                                    ovr014.spell_menu3(out var_2, 0, 0);
                                    break;

                                case 'T':
                                    ovr014.turns_undead(player);
                                    var_2 = true;
                                    ovr025.clear_actions(player);
                                    break;

                                case 'D':
                                    delay_menu(ref var_2, player);
                                    break;

                                case ' ':
                                    /* Turn off auto-fight. */
                                    foreach (Player p in gbl.TeamList)
                                    {
                                        if (p.control_morale < Control.NPC_Base)
                                        {
                                            p.quick_fight = QuickFight.False;
                                        }
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            switch (var_1)
                            {
                                case 'G':
                                case 'H':
                                case 'K':
                                case 'M':
                                case 'O':
                                case 'P':
                                case 'Q':
                                case 'I':
                                    sub_33B26(ref var_2, var_1, player);
                                    break;

                                case '2':
                                    gbl.AutoPCsCastMagic = !gbl.AutoPCsCastMagic;

                                    if (gbl.AutoPCsCastMagic == true)
                                    {
                                        ovr025.string_print01("Magic On");
                                    }
                                    else
                                    {
                                        ovr025.string_print01("Magic Off");
                                    }
                                    break;

                                case (char)0x10:
                                    player.actions.delay = 20;
                                    foreach (Player p in gbl.TeamList)
                                    {
                                        SetPlayerQuickFight(p);
                                    }
                                    ovr027.ClearPromptArea();
                                    seg049.SysDelay(0x0C8);

                                    var_2 = true;
                                    break;

                                case '-':
                                    if (ovr014.god_intervene() == true)
                                    {
                                        ovr033.RedrawCombatIfFocusOn(false, 3, player);
                                        var_2 = true;
                                    }
                                    else
                                    {
                                        ovr025.string_print01("That doesn't work");
                                    }
                                    break;

                            }
                        }

                        if (var_2 == false)
                        {
                            ovr033.RedrawCombatIfFocusOn(true, 2, player);
                            gbl.display_hitpoints_ac = true;
                            ovr025.CombatDisplayPlayerSummary(player);
                        }
                    }
                }
            }
            else
            {
                ovr025.clear_actions(player);
            }
        }

        static Set unk_33748 = new Set(0x0209, new byte[] { 0x09, 0x00, 0x00, 0x20, 0x04, 0x00, 0x80, 0xAB, 0x03 });
        static Set unk_33768 = new Set(0x0209, new byte[] { 0x09, 0x00, 0x01, 0x20, 0x04, 0x00, 0x9A, 0xAB, 0x73 });


        internal static void combat_menu(out char arg_0, Player player)
        {
            string menuText = string.Empty;

            if (player.actions.move > 0)
            {
                menuText += "Move ";
            }

            menuText += "View Aim ";

            if (player.items.Count > 0)
            {
                menuText += "Use ";
            }

            bool hasSpells = player.spellList.HasSpells();

            if (hasSpells == true &&
                player.actions.can_cast == true &&
                gbl.area_ptr.can_cast_spells == false)
            {
                menuText += "Cast ";
            }

            if (player.SkillLevel(SkillType.Cleric) > 0 &&
                player.actions.hasTurnedUndead == false)
            {
                menuText += "Turn ";
            }

            menuText += "Quick Done";

            do
            {
                bool ctrlKey;
                arg_0 = ovr027.displayInput(out ctrlKey, false, 1, gbl.defaultMenuColors, menuText, string.Empty);

                if (ctrlKey == true &&
                    unk_33748.MemberOf(arg_0) == false)
                {
                    arg_0 = '\0';
                }

            } while (unk_33768.MemberOf(arg_0) == false);

            ovr027.ClearPromptArea();
        }


        internal static bool BattleRoundChecks() // battle01
        {
            ovr021.step_game_time(1, 1);
            gbl.combat_round++;
            ovr014.calc_enemy_health_percentage();

            foreach (Player player in gbl.TeamList)
            {
                ovr024.CheckAffectsEffect(player, CheckType.Type_19);
                ovr024.in_poison_cloud(0, player);

                if (player.health_status == Status.dying)
                {
                    player.actions.bleeding += 1;

                    if (player.actions.bleeding > 9)
                    {
                        player.health_status = Status.dead;
                    }

                }
            }

            if (ovr025.bandage(false))
            {
                ovr025.string_print01("Your Teammate is Dying");
            }

            ovr025.CountCombatTeamMembers();

            ovr033.redrawCombatArea(8, 0xff, gbl.mapToBackGroundTile.mapScreenTopLeft + Point.ScreenCenter);

            bool battleOver = false;

            if (gbl.friends_count == 0 ||
                gbl.foe_count == 0 ||
                gbl.combat_round >= gbl.combat_round_no_action_limit)
            {
                battleOver = true;
            }

            if (gbl.friends_count > 1 &&
                gbl.foe_count == 0 &&
                gbl.inDemo == false &&
                ovr027.yes_no(gbl.defaultMenuColors, "Continue Battle:") == 'Y')
            {
                battleOver = false;
            }

            return battleOver;
        }


        internal static void sub_33B26(ref bool arg_0, char arg_4, Player player)
        {
            int movesBackup = player.actions.move;
            int dirBackup = player.actions.direction;
            Point pos = ovr033.PlayerMapPos(player);

            arg_0 = false;
            byte dir = 8;

            while (player.actions.move > 1 &&
                arg_4 != 0 &&
                arg_4 != 13)
            {
                seg037.draw8x8_clear_area(0x18, 0x27, 0x18, 0x18);

                if (arg_4 == ' ')
                {
                    string text = string.Format("Move/Attack, Move Left = {0} ", player.actions.move / 2);

                    arg_4 = ovr027.displayInput(false, 1, new MenuColorSet(15, 10, 10), string.Empty, text);
                }

                switch (arg_4)
                {
                    case '\0':
                        player.actions.move = movesBackup;

                        ovr033.RedrawPlayerBackground(ovr033.GetPlayerIndex(player));

                        if (ovr033.sub_7515A(false, pos, player) == false)
                        {
                            arg_0 = true;
                        }
                        else
                        {
                            arg_0 = false;
                        }

                        ovr033.redrawCombatArea(8, 0, ovr033.PlayerMapPos(player));
                        player.actions.direction = dirBackup;
                        dir = 8;
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

                    default:
                        dir = 8;
                        break;
                }

                if (dir < 8)
                {
                    ovr033.draw_74B3F(false, Icon.Normal, dir, player);
                    int ground_tile;
                    int target_index;

                    ovr033.getGroundInformation(out ground_tile, out target_index, dir, player);

                    if (target_index > 0)
                    {
                        sub_33F03(ref arg_0, gbl.player_array[target_index], player);
                    }
                    else if (ground_tile == 0)
                    {
                        char b = ovr027.yes_no(gbl.defaultMenuColors, "Flee:");
                        if (b == 'Y')
                        {
                            arg_0 = true;
                            ovr014.flee_battle(player);
                        }
                        else if (b == 'N')
                        {
                            arg_0 = false;
                        }
                    }
                    else
                    {
                        int cost;

                        if ((dir / 2) < 1)
                        {
                            cost = gbl.BackGroundTiles[ground_tile].move_cost * 3;
                        }
                        else
                        {
                            cost = gbl.BackGroundTiles[ground_tile].move_cost * 2;
                        }

                        if (gbl.BackGroundTiles[ground_tile].move_cost == 0xFF)
                        {
                            cost = 0xFFFF;
                        }

                        if (cost > player.actions.move)
                        {
                            ovr025.string_print01("can't go there");
                        }
                        else
                        {
                            ovr014.move_step_away_attack(dir, player);

                            if (player.in_combat == false)
                            {
                                arg_0 = true;
                                ovr025.clear_actions(player);
                            }
                            else
                            {
                                if (player.actions.move > 0)
                                {
                                    ovr014.sub_3E748(dir, player);
                                }

                                if (player.in_combat == false)
                                {
                                    arg_0 = true;
                                    ovr025.clear_actions(player);
                                }

                                ovr024.in_poison_cloud(1, player);

                                if (player.IsHeld())
                                {
                                    arg_0 = true;
                                    ovr025.clear_actions(player);
                                }
                            }
                        }
                    }
                }

                if (arg_4 != 0 &&
                    arg_4 != 13)
                {
                    arg_4 = ' ';
                }
            }

            if (player.actions.move < 2)
            {
                player.actions.move = 0;
            }
        }


        internal static void sub_33F03(ref bool arg_0, Player target, Player player)
        {
            if (ovr025.is_weapon_ranged(player) == true &&
                ovr025.is_weapon_ranged_melee(player) == false)
            {
                ovr025.string_print01("Not with that weapon");
            }
            else if (ovr014.can_attack_target(target, player) == true)
            {
                player.actions.target = target;

                if (ovr014.TrySweepAttack(target, player) == true)
                {
                    arg_0 = true;
                }
                else
                {
                    ovr014.RecalcAttacksReceived(target, player);

                    arg_0 = ovr014.AttackTarget(null, 0, target, player);
                }
            }
        }


        internal static void delay_menu(ref bool turnEnded, Player player)
        {
            turnEnded = false;
            string menuText = string.Empty;

            if (ovr025.is_weapon_ranged(player) == false ||
                ovr025.is_weapon_ranged_melee(player) == true)
            {
                menuText += "Guard ";
            }

            menuText += "Delay Quit ";

            if (ovr025.bandage(false) == true)
            {
                menuText += "Bandage ";
            }

            menuText += "Speed Exit";
            char input = ' ';

            while (input != '\0' && input != 'E' && turnEnded == false)
            {
                input = ovr027.displayInput(false, 0, gbl.defaultMenuColors, menuText, string.Empty);

                switch (input)
                {
                    case 'G':
                        ovr025.guarding(player);
                        turnEnded = true;
                        break;

                    case 'D':
                        player.actions.delay = 1;
                        turnEnded = true;
                        break;

                    case 'Q':
                        ovr025.clear_actions(player);
                        turnEnded = true;
                        break;

                    case 'B':
                        ovr025.bandage(true);
                        ovr025.clear_actions(player);
                        turnEnded = true;
                        break;

                    case 'S':
                        set_gamespeed();
                        break;
                }
            }
        }


        internal static void set_gamespeed()
        {
            char input = ' ';

            while (input != '\0' && input != 'E')
            {
                string text = "GameSpeed (" + gbl.game_speed_var.ToString() + ") :";
                string menu = " ";

                if (gbl.game_speed_var < 9)
                {
                    menu += "Slower ";
                }

                if (gbl.game_speed_var > 0)
                {
                    menu += "Faster ";
                }

                menu += "Exit";

                input = ovr027.displayInput(false, 0, gbl.defaultMenuColors, menu, text);

                if (input == 0x53)
                {
                    gbl.game_speed_var++;
                }
                else if (input == 0x46)
                {
                    gbl.game_speed_var--;
                }
            }
        }


        internal static void SetPlayerQuickFight(Player player) // sub_3432F
        {
            player.quick_fight = QuickFight.True;
            if (player.actions.target != null &&
                player.actions.target.combat_team == player.combat_team)
            {
                player.actions.target = null;
            }
        }
    }
}
