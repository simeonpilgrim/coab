using Classes;

namespace engine
{
    internal enum SpellId
    {
        knock = 0x1f
    }

    class ovr015
    {
        internal static void sub_4303C( byte arg_0, sbyte arg_2, sbyte arg_4 )
        {
            if (false)
            {
                /* TODO - this code was hard coded to do nothing.... 
                switch( arg_0 )
                {
                    case 6:
						
                        mov	al, [bp+arg_4]
                        cbw
                        mov	dx, ax
                        mov	al, [bp+arg_2]
                        cbw
                        mov	cl, 4
                        shl	ax, cl
                        les	di, int ptr stru_1D530.offset
                        add	di, ax
                        add	di, dx
                        mov	al, es:[di+300h]
                        and	al, 0x3F
                        mov	bl, al
                        mov	al, [bp+0Ah]
                        cbw
                        mov	dx, ax
                        mov	al, [bp+arg_2]
                        cbw
                        mov	cl, 4
                        shl	ax, cl
                        les	di, int ptr stru_1D530.offset
                        add	di, ax
                        add	di, dx
                        mov	es:[di+300h], bl
						
                        break;

                    case 4:
						
                        mov	al, [bp+0Ah]
                        cbw
                        mov	dx, ax
                        mov	al, [bp+arg_2]
                        cbw
                        mov	cl, 4
                        shl	ax, cl
                        les	di, int ptr stru_1D530.offset
                        add	di, ax
                        add	di, dx
                        mov	al, es:[di+300h]
                        and	al, 0x0CF
                        mov	bl, al
                        mov	al, [bp+0Ah]
                        cbw
                        mov	dx, ax
                        mov	al, [bp+arg_2]
                        cbw
                        mov	cl, 4
                        shl	ax, cl
                        les	di, int ptr stru_1D530.offset
                        add	di, ax
                        add	di, dx
                        mov	es:[di+300h], bl
						
                        break;

                    case 2:
						
                        mov	al, [bp+0Ah]
                        cbw
                        mov	dx, ax
                        mov	al, [bp+arg_2]
                        cbw
                        mov	cl, 4
                        shl	ax, cl
                        les	di, int ptr stru_1D530.offset
                        add	di, ax
                        add	di, dx
                        mov	al, es:[di+300h]
                        and	al, 0x0F3
                        mov	bl, al
                        mov	al, [bp+arg_4]
                        cbw
                        mov	dx, ax
                        mov	al, [bp+arg_2]
                        cbw
                        mov	cl, 4
                        shl	ax, cl
                        les	di, int ptr stru_1D530.offset
                        add	di, ax
                        add	di, dx
                        mov	es:[di+300h], bl
						
                        break;

                    case 0:
						
                        mov	al, [bp+0Ah]
                        cbw
                        mov	dx, ax
                        mov	al, [bp+arg_2]
                        cbw
                        mov	cl, 4
                        shl	ax, cl
                        les	di, int ptr stru_1D530.offset
                        add	di, ax
                        add	di, dx
                        mov	al, es:[di+300h]
                        and	al, 0x0FC
                        mov	bl, al
                        mov	al, [bp+0Ah]
                        cbw
                        mov	dx, ax
                        mov	al, [bp+arg_2]
                        cbw
                        mov	cl, 4
                        shl	ax, cl
                        les	di, int ptr stru_1D530.offset
                        add	di, ax
                        add	di, dx
                        mov	es:[di+300h], bl

                        break;
                }
                        */
            }
        }


        internal static void MapSetDoorUnlocked( int mapDir, int mapX, int mapY ) /*sub_43148*/
        {
			if( mapY < 0 || mapY > 15 ||
				mapX < 0 || mapX > 15 )
			{
				return;
			}

			switch( mapDir )
			{
				case 6:
                    gbl.stru_1D530[0x300 + mapY + (mapX << 4)] &= 0x3F;
                    gbl.stru_1D530[0x300 + mapY + (mapX << 4)] |= 0x40;

					break;

				case 4:
                    gbl.stru_1D530[0x300 + mapY + (mapX << 4)] &= 0xCF;
                    gbl.stru_1D530[0x300 + mapY + (mapX << 4)] |= 0x10;
					break;

				case 2:
                    gbl.stru_1D530[0x300 + mapY + (mapX << 4)] &= 0xF3;
                    gbl.stru_1D530[0x300 + mapY + (mapX << 4)] |= 0x04;
					break;

				case 0:
                    gbl.stru_1D530[0x300 + mapY + (mapX << 4)] &= 0xFC;
                    gbl.stru_1D530[0x300 + mapY + (mapX << 4)] |= 0x01;
					break;
			}
        }


        internal static bool any_player_has_skill( Skills skill )
        {
            bool has_skill;
            Player player_ptr;
            int s = (int)skill;
 
			has_skill = false;			
			player_ptr = gbl.player_next_ptr;

			while ( player_ptr != null &&
					has_skill == false )
			{
                if (player_ptr.Skill_A_lvl[s] > 0 ||
                    (player_ptr.Skill_B_lvl[s] > 0 &&
					  ovr026.sub_6B3D1( player_ptr ) != 0 ) )
				{
					has_skill = true;
				}
				player_ptr = player_ptr.next_player;
			}
			return has_skill;
        }


        internal static bool bash_door( )
        {
            bool bash_worked = false;
            Player player_ptr = gbl.player_next_ptr;

			while ( player_ptr != null &&
					bash_worked == false )
			{
                Player player = player_ptr;

				if ( ovr031.WallDoorFlagsGet( gbl.mapDirection, gbl.mapPosY , gbl.mapPosX ) == 3 )
				{
					if ( player.strength == 18 )
					{
						if( player.strength_18_100 >= 0x5b &&
							player.strength_18_100 <= 99 )
						{
							if( ovr024.roll_dice( 6, 1 ) == 1 )
							{
								bash_worked = true;
							}
						}
						else if( player.strength_18_100 == 100 )
						{
							if( ovr024.roll_dice( 6, 1 ) <= 2 )
							{
								bash_worked = true;
							}
						}
						else
						{
							gbl.can_bash_door = false;
						}
					}
					else if( player.strength == 19 ||
							 player.strength == 20 )
					{
						if( ovr024.roll_dice( 6, 1 ) <= 3 )
						{
							bash_worked = true;
						}
					}
					else if( player.strength == 21 ||
							 player.strength == 22 )
					{
						if( ovr024.roll_dice( 6, 1 ) <= 4 )
						{
							bash_worked = true;
						}
					}
					else if ( player.strength == 23 )
					{
						if( ovr024.roll_dice( 6, 1 ) <= 5 )
						{
							bash_worked = true;
						}
					}
					else if ( player.strength == 24 )
					{
						if( ovr024.roll_dice( 8, 1 ) <= 7 )
						{
							bash_worked = true;
						}
					}
					else if ( player.strength == 25 )
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
					byte al = player.strength;

					if ( al >= 3 && al <= 7 )
					{
						if ( ovr024.roll_dice( 6, 1 ) == 1 )
						{
							bash_worked = true;
						}
					}
					else if ( al >= 8 && al <= 15 )
					{
						if ( ovr024.roll_dice( 6, 1 ) <= 2 )
						{
							bash_worked = true;
						}
					}
					else if ( al == 15 || al == 17 ) 
					{
						if ( ovr024.roll_dice( 6, 1 ) <= 3 )
						{
							bash_worked = true;
						}
					}
					else if ( al == 18 )
					{
						if( player.strength_18_100 >= 0 &&
							player.strength_18_100 <= 50 )
						{
							bash_worked = true;

							if ( ovr024.roll_dice( 6, 1 ) <= 3 )
							{
								bash_worked = true;
							}
						}
						else if ( player.strength_18_100 >= 51 &&
							player.strength_18_100 <= 99 )
						{
							if ( ovr024.roll_dice( 6, 1 ) <= 4 )
							{
								bash_worked = true;
							}

						}
						else if ( player.strength_18_100 == 100 )
						{
							if ( ovr024.roll_dice( 6, 1 ) <= 5 )
							{
								bash_worked = true;
							}
						}
					}
					else if ( al == 19 || al == 20 )
					{
						if ( ovr024.roll_dice( 8, 1 ) <= 7 )
						{
							bash_worked = true;
						}
					}
					else if ( al == 21 )
					{
						if ( ovr024.roll_dice( 10, 1 ) <= 9 )
						{
							bash_worked = true;
						}
					}
					else if ( al == 22 || al == 23 )
					{
						if ( ovr024.roll_dice( 12, 1 ) <= 11 )
						{
							bash_worked = true;
						}
					}
					else if ( al == 24 )
					{
						if ( ovr024.roll_dice( 20, 1 ) <= 19 )
						{
							bash_worked = true;
						}
					}
					else if ( al == 25 )
					{
						bash_worked = true;
					}
				}
				
				player_ptr = player_ptr.next_player;
			}

			if ( bash_worked == true )
			{
				MapSetDoorUnlocked( gbl.mapDirection , gbl.mapPosY , gbl.mapPosX );

                int v3 = gbl.MapDirectionXDelta[gbl.mapDirection] + gbl.mapPosX;
                int v2 = gbl.MapDirectionYDelta[gbl.mapDirection] + gbl.mapPosY;
                int v1 = (gbl.mapDirection + 4) % 8;

				MapSetDoorUnlocked( v1, v2, v3 );
			}

			return bash_worked;
        }


        internal static bool pick_lock( ) /*sub_435B6*/
        {
            bool door_picked = false;
            Player player = gbl.player_next_ptr;

			while( player != null && door_picked == false )
			{
                if (ovr024.roll_dice(100, 1) <= player.field_EA[1] &&
					player.health_status == Status.okey )
				{
					door_picked = true;
				}

				player = player.next_player;
			}

            gbl.can_pick_door = false;

			if( door_picked == true )
			{
				MapSetDoorUnlocked( gbl.mapDirection, gbl.mapPosY, gbl.mapPosX );

				MapSetDoorUnlocked( ( gbl.mapDirection + 4 ) % 8, 
					gbl.MapDirectionYDelta[ gbl.mapDirection ] + gbl.mapPosY,
					gbl.MapDirectionXDelta[ gbl.mapDirection ] + gbl.mapPosX );
			}
 
			return door_picked;
        }

        internal static int find_spell(Player player, SpellId spell_id)
        {
            int loop_var;
            int ret_val;

            loop_var = 0;

			while( loop_var <= 83 && player.spell_list[ loop_var ] != (byte)spell_id )
			{
				loop_var++;
			}

			if( loop_var <= 83 )
			{
				ret_val = loop_var;
			}
			else
			{
				ret_val = -1;
			}

            return ret_val;
        }


        internal static Player find_player_with_spell( SpellId spell_id )
        {
            Player player;

            player = gbl.player_next_ptr;

			while( player != null && find_spell( player, spell_id ) != -1 )
			{
				player = player.next_player;

			}

			return player;
        }


        internal static bool find_knock_spell( )
        {
            Player player;
            bool var_1;

			player = find_player_with_spell( SpellId.knock );

			if( player != null )
			{
                player.spell_list[find_spell(player, SpellId.knock)] = 0;

				var_1 = true;
			}
			else
			{
				var_1 = false;
			}

			return var_1;
        }


        internal static void sub_43765( )
        {
			int mapX = gbl.mapPosX;
			int mapY = gbl.mapPosY;
			int mapDir = gbl.mapDirection;

			gbl.area2_ptr.field_5AA = 0;

			if( ovr031.WallDoorFlagsGet( mapDir, mapY, mapX ) != 0 )
			{
                mapX += gbl.MapDirectionXDelta[mapDir];
                mapY += gbl.MapDirectionYDelta[mapDir];

                if (mapX > 0x0F)
                {
                    gbl.mapPosX = 0x0F;
                    gbl.area2_ptr.field_5AA = 1;
                }

                if (mapX < 0)
                {
                    gbl.mapPosX = 0;
                    gbl.area2_ptr.field_5AA = 1;
                }

                if (mapY > 0x0F)
                {
                    gbl.mapPosY = 0x0F;
                    gbl.area2_ptr.field_5AA = 1;
                }

                if (mapY < 0)
                {
                    gbl.mapPosY = 0;
                    gbl.area2_ptr.field_5AA = 1;
                }
			}
        }


        internal static void MovePartyForward( ) /* sub_43813 */
        {
            seg044.sub_120E0( gbl.word_188D2 );
			seg049.SysDelay( 50 );

            gbl.mapPosX += gbl.MapDirectionXDelta[gbl.mapDirection];
            gbl.mapPosY += gbl.MapDirectionYDelta[gbl.mapDirection];

            if (gbl.mapPosX < 0)
            {
                gbl.mapPosX = 0x0F;
            }

            if (gbl.mapPosX > 0x0f)
            {
                gbl.mapPosX = 0;
            }

            if (gbl.mapPosY < 0)
            {
                gbl.mapPosY = 0x0F;
            }

            if (gbl.mapPosY > 0x0f)
            {
                gbl.mapPosY = 0;
            }


            gbl.byte_1D53C = ovr031.sub_716A2(gbl.mapDirection, gbl.mapPosY, gbl.mapPosX);

			gbl.can_bash_door = true;
			gbl.can_pick_door = true;
			gbl.can_knock_door = true;

            gbl.byte_1D53D = ovr031.sub_717A5( gbl.mapPosY, gbl.mapPosX );

			if( (gbl.area2_ptr.field_594 & 1) > 0 )
			{
				ovr021.sub_583FA( 2, 1 );
			}
			else
			{
				ovr021.sub_583FA( 1, 1 );
			}
        }


        internal static char sub_438DF( )
        {
            bool var_2E;
            bool var_4;
            char var_3 = '\0'; /* simeon */ 
            bool var_2;
		
			gbl.area2_ptr.field_592 = 0;

			if( gbl.game_state == 4 )
			{
				var_2 = false;

				do
				{
					var_3 = ovr027.displayInput( out var_4, 0, 1, 15, 10, 13, "Area Cast View Encamp Search Look", string.Empty );

					if( var_4 == false )
					{
						switch( var_3 )
						{
							case 'A':
								if( gbl.area_ptr.field_1F6 == 0 ||
									seg051.ParamStr( 2 ) == gbl.byte_1EFA4 )
								{
									gbl.mapAreaDisplay = ( gbl.mapAreaDisplay == false );

									ovr031.sub_71820( gbl.mapDirection, gbl.mapPosY, gbl.mapPosX );
								}
								else
								{
									seg041.DisplayStatusText( 0, 14, "Not Here" );
								}
								break;

							case 'C':
								if( gbl.player_ptr.health_status == Status.okey )
								{
									gbl.byte_1D5BE = 1;
									ovr016.cast_spell();
								}
								break;

							case 'V':
								gbl.byte_1D5BE = 1;
								ovr020.viewPlayer( out var_2E );
								break;

							case 'E':
								var_2 = true;
								gbl.byte_1D5BE = 1;
								break;

							case 'S':
								gbl.area2_ptr.field_594 ^= 1;
								break;

							case 'L':
								gbl.area2_ptr.field_594 |= 2;
								ovr021.sub_583FA( 2, 1 );
								gbl.ecl_offset = gbl.word_1B2D5;
								var_2 = true;
								break;
						}
					}
					else
					{
						switch( var_3 )
						{
							case 'H':
								sub_43765();
								var_2 = true;
								break;

							case 'P':
								gbl.mapDirection = (byte)((gbl.mapDirection + 4) % 8);

								gbl.byte_1D53C = ovr031.sub_716A2( gbl.mapDirection, gbl.mapPosY, gbl.mapPosX );
								ovr031.sub_71820( gbl.mapDirection, gbl.mapPosY, gbl.mapPosX );
								break;

							case 'K':
								gbl.mapDirection = (byte)(( gbl.mapDirection + 6 ) % 8);

								seg044.sub_120E0( gbl.word_188D2 );
								gbl.byte_1D53C = ovr031.sub_716A2( gbl.mapDirection, gbl.mapPosY, gbl.mapPosX );
								ovr031.sub_71820( gbl.mapDirection, gbl.mapPosY, gbl.mapPosX );
								break;

							case 'M':
								gbl.mapDirection = (byte)(( gbl.mapDirection + 2 ) % 8);

								seg044.sub_120E0( gbl.word_188D2 );

								gbl.byte_1D53C = ovr031.sub_716A2( gbl.mapDirection, gbl.mapPosY, gbl.mapPosX );
								ovr031.sub_71820( gbl.mapDirection, gbl.mapPosY, gbl.mapPosX );
								break;

							default:
								ovr020.sub_572CF( var_3 );
								ovr025.Player_Summary( gbl.player_ptr );
								break;
						}
					}

					ovr025.camping_search();

				}while( var_2 == false );
			}

			if( gbl.byte_1EE90 == 0 )
			{
				seg037.draw8x8_clear_area( 0x16, 0x26, 0x11, 1 );

				gbl.byte_1EE90 = 1;
			}

			return var_3;
        }


        internal static void locked_door( )
        {
            char input;
            string var_2B;
            bool var_2;
            bool var_1;

            var_1 = false;

            if( gbl.game_state == 4 )
            {
                if( gbl.area2_ptr.field_592 < 0xff )
                {
                    gbl.byte_1D8AA = 1;

                    byte al = ovr031.WallDoorFlagsGet( gbl.mapDirection, gbl.mapPosY, gbl.mapPosX );

                    if( al == 1 )
                    {
                        var_1 = true;
                    }
                    else if( al == 2 )
                    {
                        var_2B = string.Empty;

                        if( gbl.can_bash_door == true )
                        {
                            var_2B = "Bash";
                        }

                        if( gbl.can_pick_door == true &&
                            any_player_has_skill( Skills.thief ) == true )
                        {
                            var_2B += " Pick";
                        }

                        if( gbl.can_knock_door == true &&
                            find_player_with_spell(SpellId.knock) != null)
                        {
                            var_2B += " Knock";
                        }

                        var_2B += " Exit";

                        if( var_2B == " Exit" )
                        {
                            sub_4303C( gbl.mapDirection, gbl.mapPosY, gbl.mapPosX );
                        }
                        else
                        {
                            input = ovr027.displayInput( out var_2, 0, 0, 15, 10, 13, var_2B, "Locked. " );

                            switch( input )
                            {
                                case 'B':
                                    var_1 = bash_door();
                                    break;

                                case 'P':
                                    var_1 = pick_lock();
                                    break;

                                case 'K':
                                    var_1 = find_knock_spell();
                                    break;
                            }
                        }
                    }
                    else if( al == 3 )
                    {
                        var_2B = string.Empty;

                        if( gbl.can_bash_door == true )
                        {
                            var_2B = "Bash";
                        }

                        if( gbl.can_pick_door == true &&
                            any_player_has_skill(Skills.thief) == true)
                        {
                            var_2B += " Pick";
                        }

                        if (gbl.can_knock_door == true &&
                            find_player_with_spell(SpellId.knock) != null)
                        {
                            var_2B += " Knock";
                        }

                        var_2B += " Exit";

                        if( var_2B == " Exit" )
                        {
                            sub_4303C( gbl.mapDirection, gbl.mapPosY, gbl.mapPosX );
                        }
                        else
                        {
                            input = ovr027.displayInput( out var_2, 0, 0, 15, 10, 13, var_2B, "Locked. " );

                            switch ( input )
                            {
                                case 'B':
                                    var_1 = bash_door();
                                    break;

                                case 'P':
                                    gbl.can_pick_door = false;
                                    break;

                                case 'K':
                                    var_1 = find_knock_spell();
                                    break;
                            }
                        }
                    }

                    if( var_1 == true )
                    {
                        MovePartyForward();
                    }

                    ovr025.camping_search();
                }
                else
                {
                    gbl.area2_ptr.field_592 = 0;
                }
            }

            ovr030.DaxArrayFreeDaxBlocks( gbl.byte_1D556 );


			seg040.free_dax_block( ref gbl.headX_dax );
			seg040.free_dax_block( ref gbl.bodyX_dax );
			gbl.current_head_id = 0xFF;
            gbl.current_body_id = 0xFF;
        }
    }
}
