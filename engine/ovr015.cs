using Classes;

namespace engine
{
    class ovr015
    {
        internal static void MapSetDoorUnlocked(int mapDir, int mapY, int mapX) /*sub_43148*/
        {
            if (mapX < 0 || mapX > 15 ||
                mapY < 0 || mapY > 15)
            {
                return;
            }

            switch (mapDir)
            {
                case 6:
                    gbl.geo_ptr.maps[mapY, mapX].x3_dir_6 = 1;
                    break;

                case 4:
                    gbl.geo_ptr.maps[mapY, mapX].x3_dir_4 = 1;
                    break;

                case 2:
                    gbl.geo_ptr.maps[mapY, mapX].x3_dir_2 = 1;
                    break;

                case 0:
                    gbl.geo_ptr.maps[mapY, mapX].x3_dir_0 = 1;
                    break;
            }
        }


        static bool AnyPlayerHasSkill(SkillType skill) // any_player_has_skill
        {
            foreach (Player player in gbl.TeamList)
            {
                if (player.SkillLevel(skill) > 0)
                {
                    return true;
                }
            }
            return false;
        }


        static bool bash_door()
        {
            bool bash_worked = false;

            foreach (Player player in gbl.TeamList)
            {
                if (bash_worked == true)
                {
                    break;
                }

                if (ovr031.WallDoorFlagsGet(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX) == 3)
                {
                    if (player.stats2.Str.full == 18)
                    {
                        if (player.stats2.Str00.full >= 91 &&
                            player.stats2.Str00.full <= 99)
                        {
                            if (ovr024.roll_dice(6, 1) == 1)
                            {
                                bash_worked = true;
                            }
                        }
                        else if (player.stats2.Str00.full == 100)
                        {
                            if (ovr024.roll_dice(6, 1) <= 2)
                            {
                                bash_worked = true;
                            }
                        }
                        else
                        {
                            gbl.can_bash_door = false;
                        }
                    }
                    else if (player.stats2.Str.full == 19 ||
                             player.stats2.Str.full == 20)
                    {
                        if (ovr024.roll_dice(6, 1) <= 3)
                        {
                            bash_worked = true;
                        }
                    }
                    else if (player.stats2.Str.full == 21 ||
                             player.stats2.Str.full == 22)
                    {
                        if (ovr024.roll_dice(6, 1) <= 4)
                        {
                            bash_worked = true;
                        }
                    }
                    else if (player.stats2.Str.full == 23)
                    {
                        if (ovr024.roll_dice(6, 1) <= 5)
                        {
                            bash_worked = true;
                        }
                    }
                    else if (player.stats2.Str.full == 24)
                    {
                        if (ovr024.roll_dice(8, 1) <= 7)
                        {
                            bash_worked = true;
                        }
                    }
                    else if (player.stats2.Str.full == 25)
                    {
                        bash_worked = true;
                    }
                    else
                    {
                        gbl.can_bash_door = false;
                    }
                }
                else
                {
                    int str = player.stats2.Str.full;

                    if (str >= 3 && str <= 7)
                    {
                        if (ovr024.roll_dice(6, 1) == 1)
                        {
                            bash_worked = true;
                        }
                    }
                    else if (str >= 8 && str <= 15)
                    {
                        if (ovr024.roll_dice(6, 1) <= 2)
                        {
                            bash_worked = true;
                        }
                    }
                    else if (str >= 15 && str <= 17)
                    {
                        if (ovr024.roll_dice(6, 1) <= 3)
                        {
                            bash_worked = true;
                        }
                    }
                    else if (str == 18)
                    {
                        if (player.stats2.Str00.full >= 0 &&
                            player.stats2.Str00.full <= 50)
                        {
                            if (ovr024.roll_dice(6, 1) <= 3)
                            {
                                bash_worked = true;
                            }
                        }
                        else if (player.stats2.Str00.full >= 51 &&
                            player.stats2.Str00.full <= 99)
                        {
                            if (ovr024.roll_dice(6, 1) <= 4)
                            {
                                bash_worked = true;
                            }

                        }
                        else if (player.stats2.Str00.full == 100)
                        {
                            if (ovr024.roll_dice(6, 1) <= 5)
                            {
                                bash_worked = true;
                            }
                        }
                    }
                    else if (str == 19 || str == 20)
                    {
                        if (ovr024.roll_dice(8, 1) <= 7)
                        {
                            bash_worked = true;
                        }
                    }
                    else if (str == 21)
                    {
                        if (ovr024.roll_dice(10, 1) <= 9)
                        {
                            bash_worked = true;
                        }
                    }
                    else if (str == 22 || str == 23)
                    {
                        if (ovr024.roll_dice(12, 1) <= 11)
                        {
                            bash_worked = true;
                        }
                    }
                    else if (str == 24)
                    {
                        if (ovr024.roll_dice(20, 1) <= 19)
                        {
                            bash_worked = true;
                        }
                    }
                    else if (str == 25)
                    {
                        bash_worked = true;
                    }
                }
            }

            if (bash_worked == true)
            {
                MapSetDoorUnlocked(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);

                int map_x = gbl.MapDirectionXDelta[gbl.mapDirection] + gbl.mapPosX;
                int map_y = gbl.MapDirectionYDelta[gbl.mapDirection] + gbl.mapPosY;
                int map_dir = (gbl.mapDirection + 4) % 8;

                MapSetDoorUnlocked(map_dir, map_y, map_x);
            }

            return bash_worked;
        }


        internal static bool pick_lock() /*sub_435B6*/
        {
            bool door_picked = false;

            foreach (Player player in gbl.TeamList)
            {
                if (door_picked) break;

                if (ovr024.roll_dice(100, 1) <= player.thief_skills[(byte)ThiefSkills.OpenLocks] &&
                    player.health_status == Status.okey)
                {
                    door_picked = true;
                }
            }

            gbl.can_pick_door = false;

            if (door_picked == true)
            {
                MapSetDoorUnlocked(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);

                MapSetDoorUnlocked((gbl.mapDirection + 4) % 8,
                    gbl.MapDirectionYDelta[gbl.mapDirection] + gbl.mapPosY,
                    gbl.MapDirectionXDelta[gbl.mapDirection] + gbl.mapPosX);
            }

            return door_picked;
        }


        static bool TeamMemberHasSpell(Spells spellId)
        {
            return gbl.TeamList.Exists(p => p.spellList.HasSpell((int)spellId));
        }


        static bool RemoveKnockSpell()
        {
            foreach (Player player in gbl.TeamList)
            {
                if (player.spellList.HasSpell((int)Spells.knock))
                {
                    player.spellList.ClearSpell((int)Spells.knock);
                    return true;
                }
            }

            return false;
        }


        static void TryStepForward() // sub_43765
        {
            int mapX = gbl.mapPosX;
            int mapY = gbl.mapPosY;
            int mapDir = gbl.mapDirection;

            gbl.area2_ptr.tried_to_exit_map = false;

            if (ovr031.WallDoorFlagsGet(mapDir, mapY, mapX) != 0)
            {
                mapX += gbl.MapDirectionXDelta[mapDir];
                mapY += gbl.MapDirectionYDelta[mapDir];

                if (mapX > 0x0F)
                {
                    gbl.mapPosX = 0x0F;
                    gbl.area2_ptr.tried_to_exit_map = true;
                }

                if (mapX < 0)
                {
                    gbl.mapPosX = 0;
                    gbl.area2_ptr.tried_to_exit_map = true;
                }

                if (mapY > 0x0F)
                {
                    gbl.mapPosY = 0x0F;
                    gbl.area2_ptr.tried_to_exit_map = true;
                }

                if (mapY < 0)
                {
                    gbl.mapPosY = 0;
                    gbl.area2_ptr.tried_to_exit_map = true;
                }
            }
        }


        internal static void MovePartyForward() /* sub_43813 */
        {
            seg044.PlaySound(Sound.sound_a);
            seg049.SysDelay(50);

            gbl.mapPosX += gbl.MapDirectionXDelta[gbl.mapDirection];
            gbl.mapPosY += gbl.MapDirectionYDelta[gbl.mapDirection];

            gbl.mapPosX &= 0x0f; // wrap via masking
            gbl.mapPosY &= 0x0f; // wrap via masking

            gbl.mapWallType = ovr031.getMap_wall_type(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);

            gbl.can_bash_door = true;
            gbl.can_pick_door = true;
            gbl.can_knock_door = true;

            gbl.mapWallRoof = ovr031.get_wall_x2(gbl.mapPosY, gbl.mapPosX);

            if ((gbl.area2_ptr.search_flags & 1) > 0)
            {
                ovr021.step_game_time(2, 1);
            }
            else
            {
                ovr021.step_game_time(1, 1);
            }
        }


        internal static char main_3d_world_menu() /* sub_438DF */
        {
            char input_key = '\0'; /* simeon */

            gbl.area2_ptr.field_592 = 0;

            if (gbl.game_state == GameState.DungeonMap)
            {
                bool stop_loop = false;

                do
                {
                    bool special_key;

                    input_key = ovr027.displayInput(out special_key, false, 1, gbl.defaultMenuColors, "Area Cast View Encamp Search Look", string.Empty);

                    if (special_key == false)
                    {
                        switch (input_key)
                        {
                            case 'A':
                                if (gbl.area_ptr.block_area_view == 0 ||
                                    Cheats.always_show_areamap)
                                {
                                    gbl.mapAreaDisplay = !gbl.mapAreaDisplay;

                                    ovr031.Draw3dWorld(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);
                                }
                                else
                                {
                                    seg041.DisplayStatusText(0, 14, "Not Here");
                                }
                                break;

                            case 'C':
                                if (gbl.SelectedPlayer.health_status == Status.okey)
                                {
                                    gbl.menuSelectedWord = 1;
                                    ovr016.cast_spell();
                                }
                                break;

                            case 'V':
                                gbl.menuSelectedWord = 1;
                                ovr020.viewPlayer();
                                break;

                            case 'E':
                                stop_loop = true;
                                gbl.menuSelectedWord = 1;
                                break;

                            case 'S':
                                gbl.area2_ptr.search_flags ^= 1;
                                break;

                            case 'L':
                                gbl.area2_ptr.search_flags |= 2;
                                ovr021.step_game_time(2, 1);
                                gbl.ecl_offset = gbl.SearchLocationAddr;
                                stop_loop = true;
                                break;
                        }
                    }
                    else
                    {
                        switch (input_key)
                        {
                            case 'H': // forward
                                TryStepForward();
                                stop_loop = true;
                                break;

                            case 'P': // turn 180
                                gbl.mapDirection = (byte)((gbl.mapDirection + 4) % 8);

                                gbl.mapWallType = ovr031.getMap_wall_type(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);
                                ovr031.Draw3dWorld(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);
                                break;

                            case 'K': // turn left
                                gbl.mapDirection = (byte)((gbl.mapDirection + 6) % 8);

                                seg044.PlaySound(Sound.sound_a);
                                gbl.mapWallType = ovr031.getMap_wall_type(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);
                                ovr031.Draw3dWorld(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);
                                break;

                            case 'M': // turn right
                                gbl.mapDirection = (byte)((gbl.mapDirection + 2) % 8);

                                seg044.PlaySound(Sound.sound_a);

                                gbl.mapWallType = ovr031.getMap_wall_type(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);
                                ovr031.Draw3dWorld(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);
                                break;

                            default:
                                ovr020.scroll_team_list(input_key);
                                ovr025.PartySummary(gbl.SelectedPlayer);
                                break;
                        }
                    }

                    ovr025.display_map_position_time();

                } while (stop_loop == false);
            }

            if (gbl.bottomTextHasBeenCleared == false)
            {
                seg037.draw8x8_clear_area(TextRegion.NormalBottom);

                gbl.bottomTextHasBeenCleared = true;
            }

            return input_key;
        }


        internal static void locked_door()
        {
            char input;
            bool var_2;

            bool var_1 = false;

            if (gbl.game_state == GameState.DungeonMap)
            {
                if (gbl.area2_ptr.field_592 < 0xff)
                {
                    gbl.can_draw_bigpic = true;

                    byte al = ovr031.WallDoorFlagsGet(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);

                    if (al == 1)
                    {
                        var_1 = true;
                    }
                    else if (al == 2)
                    {
                        string prompt = string.Empty;

                        if (gbl.can_bash_door == true)
                        {
                            prompt = "Bash";
                        }

                        if (gbl.can_pick_door == true &&
                            AnyPlayerHasSkill(SkillType.Thief))
                        {
                            prompt += " Pick";
                        }

                        if (gbl.can_knock_door == true &&
                            TeamMemberHasSpell(Spells.knock))
                        {
                            prompt += " Knock";
                        }

                        if (prompt != "")
                        {
                            prompt += " Exit";

                            input = ovr027.displayInput(out var_2, false, 0, gbl.defaultMenuColors, prompt, "Locked. ");

                            switch (input)
                            {
                                case 'B':
                                    var_1 = bash_door();
                                    break;

                                case 'P':
                                    var_1 = pick_lock();
                                    break;

                                case 'K':
                                    var_1 = RemoveKnockSpell();
                                    break;
                            }
                        }
                    }
                    else if (al == 3) // unpickable
                    {
                        string prompt = string.Empty;

                        if (gbl.can_bash_door == true)
                        {
                            prompt = "Bash";
                        }

                        if (gbl.can_pick_door == true &&
                            AnyPlayerHasSkill(SkillType.Thief))
                        {
                            prompt += " Pick";
                        }

                        if (gbl.can_knock_door == true &&
                            TeamMemberHasSpell(Spells.knock))
                        {
                            prompt += " Knock";
                        }

                        if (prompt != "")
                        {
                            prompt += " Exit";

                            input = ovr027.displayInput(out var_2, false, 0, gbl.defaultMenuColors, prompt, "Locked. ");

                            switch (input)
                            {
                                case 'B':
                                    var_1 = bash_door();
                                    break;

                                case 'P':
                                    gbl.can_pick_door = false;
                                    break;

                                case 'K':
                                    var_1 = RemoveKnockSpell();
                                    break;
                            }
                        }
                    }

                    if (var_1 == true)
                    {
                        MovePartyForward();
                    }

                    ovr025.display_map_position_time();
                }
                else
                {
                    gbl.area2_ptr.field_592 = 0;
                }
            }

            ovr030.DaxArrayFreeDaxBlocks(gbl.byte_1D556);

            gbl.headX_dax = null;
            gbl.bodyX_dax = null;
            gbl.current_head_id = 0xFF;
            gbl.current_body_id = 0xFF;
        }
    }
}
