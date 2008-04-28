using Classes;

namespace engine
{
    class ovr005
    {
        static Affects[] disease_types = {  Affects.helpless,  Affects.cause_disease_1,
                                            Affects.affect_2b, Affects.cause_disease_2,
                                            Affects.funky__32, Affects.affect_39 };

		static string[] temple_sl = { "Cure Blindness", "Cure Disease", "Cure Light Wounds", "Cure Serious Wounds", "Cure Critical Wounds", "Heal", "Neutralize Poison", "Raise Dead", "Remove Curse", "Stone to Flesh", "Exit" };


        internal static char cast_cure_anyway( string arg_0 )
        {
            char ret_val;
 
			ovr025.DisplayPlayerStatusString( false, 0, arg_0, gbl.player_ptr );
			ret_val = ovr027.yes_no( 15, 10, 13, "cast cure anyway: " );

			ovr025.ClearPlayerTextArea();

            return ret_val;
        }


        internal static char buy_cure( short cost, string cure_name )
        {
            int var_106;
            short var_104;
            char var_102;
            string var_101;

			var_101 = cure_name + " will only cost	" + cost.ToString() + "	gold pieces.";

            seg041.press_any_key( var_101, true, 0, 10, 0x16, 0x26, 0x11, 1 );

            var_102 = ovr027.yes_no( 15, 10, 13, "pay for cure " );

			if( var_102 == 'Y' )
			{
				var_104 = (short)ovr020.getPlayerGold( gbl.player_ptr );
				
				if( cost <= var_104 )
				{
					var_104 = cost;

					ovr022.setPlayerMoney( var_104 );
				}
				else
				{
					var_106 = ovr022.getPooledGold( gbl.pooled_money );

					if( cost <= var_106 )
					{
						ovr022.setPooledGold( var_106 - cost );
					}
					else
					{
						ovr025.string_print01( "Not enough money." );
						var_102 = 'N';
					}
				}
			}

			if( var_102 == 'Y' )
			{
				ovr025.ClearPlayerTextArea();
				ovr025.DisplayPlayerStatusString( true, 0, "is cured." , gbl.player_ptr );
			}

			return var_102;
        }


        internal static void cure_blindness( )
        {
            char var_105;
            Affect var_4;

            var_105 = 'Y';

			if( ovr025.find_affect( out var_4, Affects.blinded, gbl.player_ptr ) == false )
			{
				var_105 = cast_cure_anyway ( "is not blind." );
			}

			if( var_105 == 'Y' )
			{
				var_105 = buy_cure( 1000, "Cure Blindness" );

				if( var_105 == 'Y' )
				{
					ovr024.remove_affect( null, Affects.blinded, gbl.player_ptr );
				}
			}
        }


        internal static void cure_disease( )
        {
            byte var_107;
            byte loop_var;
            char var_105;
            Affect var_4;

            var_107 = 0;
            var_105 = 'Y';

			for( loop_var = 0; loop_var < 6; loop_var++ )
			{
				if( ovr025.find_affect( out var_4, disease_types[loop_var], gbl.player_ptr ) == true )
				{
					var_107 = 1;
				}
			}

			if( var_107 == 0 )
			{
				var_105 = cast_cure_anyway( "is not Diseased." );
			}
 
			if( var_105 == 'Y' )
			{
				var_105 = buy_cure( 1000, "Cure Disease" );
				
				if( var_105 == 'Y' )
				{
					gbl.byte_1D2C6 = true;
					for( loop_var = 0; loop_var < 6; loop_var++ )
					{
						ovr024.remove_affect( null, disease_types[loop_var], gbl.player_ptr );
					}

					gbl.byte_1D2C6 = false;
				}
			}
        }


		internal static void cure_wounds( int arg_0 )
		{
			bool var_4;
			char var_3;
			byte var_2;
			byte var_1;

			switch ( arg_0 )
			{
				case 1:
					var_3 = buy_cure( 100, "Cure Light Wounds" );
					if( var_3 == 'Y' )
					{
						var_1 = ovr024.roll_dice( 8, 1 );
						var_4 = ovr024.heal_player( 0, var_1, gbl.player_ptr );
					}
					break;

				case 2:
					var_3 = buy_cure( 350, "Cure Serious Wounds" );
					if( var_3 == 'Y' )
					{
						var_1 = (byte)(ovr024.roll_dice( 8, 2 ) + 1);
						var_4 = ovr024.heal_player( 0, var_1, gbl.player_ptr );
					}
					break;

				case 3:
					var_3 = buy_cure( 600, "Cure Critical Wounds" );
					if( var_3 == 'Y' )
					{
						var_1 = (byte)(ovr024.roll_dice( 8, 3 ) + 3);
						var_4 = ovr024.heal_player( 0, var_1, gbl.player_ptr );
					}
					break;

				case 4:
					var_3 = buy_cure( 5000, "Heal" );
					if( var_3 == 'Y' )
					{
						var_1 = gbl.player_ptr.hit_point_max;
						var_1 -= gbl.player_ptr.hit_point_current;
						var_1 -= ovr024.roll_dice( 4, 1 );

						var_4 =	ovr024.heal_player( 0, var_1, gbl.player_ptr );
						ovr024.remove_affect( null , Affects.blinded, gbl.player_ptr );

						for( var_2 = 0; var_2 < 6; var_2++ )
						{
							ovr024.remove_affect( null , disease_types[var_2], gbl.player_ptr );
						}

						ovr024.remove_affect( null , Affects.feeble, gbl.player_ptr );

						ovr024.sub_648D9( 1, gbl.player_ptr );
						ovr024.sub_648D9( 2, gbl.player_ptr );
					}
					break;
			}
		}


        internal static void raise_dead( )
        {
            Player player01;
            byte var_109;
            byte var_108;
            byte var_107;
            byte var_106;
            char var_105;

            player01 = gbl.player_ptr;
            var_106 = 0;
            var_105 = 'Y';

            if( player01.health_status == Status.dead ||
                player01.health_status == Status.animated )
            {
                var_106 = 1;
            }

            if( var_106 == 0 )
            {
                var_105 = cast_cure_anyway( "is not dead." );
            }


            if( var_105 == 'Y' )
            {
                var_105 = buy_cure( 5500, "Raise Dead" );

                if( var_105 == 'Y' &&
                    var_106 != 0 )
                {
                    gbl.byte_1D2C6 = true;

                    ovr024.remove_affect( null , Affects.funky__32, player01 );
                    ovr024.remove_affect( null , Affects.poisoned, player01 );

                    gbl.byte_1D2C6 = false;

                    player01.hit_point_current = 1;
                    player01.health_status = Status.okey;
                    player01.in_combat = true;

                    if( player01.con <= 0 )
                    {
                        player01.con--;
                    }
                    
                    if( player01.hit_point_max > player01.field_12C )
                    {
                        var_107 = (byte)( player01.hit_point_max - player01.field_12C );
                    }
                    else
                    {
                        var_107 = 0;
                    }

                    var_108 = 0;

                    if( player01.con >= 14 )
                    {
                        for( var_109 = 0; var_109 <= 7; var_109++ )
                        {
                            if (player01.Skill_A_lvl[var_109] > 0)
                            {
                                if( var_109 == 2 )
                                {
                                    var_108 += (byte)((player01.con - 14) * player01.fighter_lvl);
                                }
                                else if( player01.con > 15 )
                                {
                                    var_108 += (byte)(player01.Skill_A_lvl[var_109] * 2);
                                }
                                else
                                {
                                    var_108 += player01.Skill_A_lvl[var_109];
                                }
                            }
                        }

                        if( var_108 > 0 )
                        {
                            var_107 /= var_108;
                        }

                        if( player01.con < 17 ||
                            player01.fighter_lvl > 0 ||
                            player01.fighter_lvl > player01.field_E6 )
                        {
                            player01.hit_point_max = var_107;
                        }
                    }
                }
            }
        }


        internal static void cure_poison2( )
        {
            byte var_106;
            char var_105;
            Affect var_4;

            var_106 = 0;

            var_105 = 'Y';

			if( ovr025.find_affect( out var_4, Affects.poisoned, gbl.player_ptr ) == true )
			{
				var_106 = 1;
			}

			if( var_106 == 0 )
			{
				var_105 = cast_cure_anyway( "is not poisoned." );
			}

			if( var_105 == 'Y' )
			{
				var_105 = buy_cure( 1000, "Neutralize Poison" );

				if( var_105 == 'Y' )
				{
					gbl.byte_1D2C6 = true;

					ovr024.remove_affect( null , Affects.poisoned, gbl.player_ptr );
					ovr024.remove_affect( null , Affects.slow_poison, gbl.player_ptr );
					ovr024.remove_affect( null , Affects.affect_0f, gbl.player_ptr );

					gbl.byte_1D2C6 = false;
				}
			}
        }


        internal static void remove_curse( )
        {
            char var_10A;
            Affect var_9;
            Item var_5;
            byte var_1;

            var_10A = 'Y';
            var_1 = 0;

			var_5 = gbl.player_ptr.itemsPtr;

			while( var_5 != null && var_1 == 0 )
			{
				if( var_5.field_36 != 0 )
				{
					var_1 = 1;
				}

				var_5 = var_5.next;
			}

			if( var_1 == 0 &&
				ovr025.find_affect( out var_9, Affects.bestow_curse, gbl.player_ptr ) == false )
			{
				var_10A = cast_cure_anyway( "is not cursed." );
			}

			if( var_10A == 'Y' )
			{
				var_10A = buy_cure( 3500, "Remove Curse" );

				if( var_10A == 'Y' )
				{
					gbl.sp_target[1] = gbl.player_ptr;
					ovr023.uncurse();
				}
			}
        }


        internal static void stone_to_flesh( )
        {
            char var_105;
 
			var_105 = 'Y';

			if( gbl.player_ptr.health_status != Status.stoned )
			{
				var_105 = cast_cure_anyway( "is not stoned." );
			}
			
			if( var_105 == 'Y' )
			{
				var_105 = buy_cure( 2000, "Stone to Flesh" );

				if( var_105 == 'Y' &&
					gbl.player_ptr.health_status == Status.stoned )
				{
					gbl.player_ptr.health_status = Status.okey;
					gbl.player_ptr.in_combat = true;
					gbl.player_ptr.hit_point_current = 1;
				}
			}
        }


        internal static void temple_heal( )
        {
            char sl_output;
            string var_36;
            bool end_shop;
            short sl_index = 0;
            int var_A;
            bool var_9;
            StringList stringListPtr;
            StringList stringList;

            end_shop = false;
            stringList = null;
            stringListPtr = null;
			ovr027.alloc_stringList( out stringList, 10 );

			stringListPtr = stringList;
            var_A = 0;
 
			while( stringList != null )
			{
				stringList.s = temple_sl[var_A]; 

				stringList.field_29 = 0;
				
				stringList = stringList.next;

				var_A++;
			}

			stringList = stringListPtr;

            seg037.draw8x8_clear_area(0x18, 0x27, 0x18, 0);
			var_9 = true;
            seg037.draw8x8_04();

			do
			{
				var_36 = gbl.player_ptr.name + ", how can we help you?";
				seg041.displayString( var_36, 0, 15, 1, 1 );

                sl_output = ovr027.sl_select_item(out stringListPtr, ref sl_index, ref var_9, false,
                    stringList, 15, 0x26, 4, 2, 15, 10, 13, "Heal Exit", string.Empty);

				if( sl_output == 'H' || sl_output == 0x0d )
				{
					switch( sl_index )
					{
						case 0:
							cure_blindness();
							break;

						case 1:
							cure_disease();
							break;

						case 2:
							cure_wounds( 1 );
							break;

						case 3:
							cure_wounds( 2 );
							break;

						case 4:
							cure_wounds( 3 );
							break;

						case 5:
							cure_wounds( 4 );
							break;

						case 6:
							cure_poison2();
							break;

						case 7:
							raise_dead();
							break;

						case 8:
							remove_curse();
							break;

						case 9:
							stone_to_flesh();
							break;

						case 10:
							end_shop = true;
							break;
					}
				}
				else if( sl_output == 0 )
				{
					end_shop = true;
				}
			
			}while( end_shop == false );
   
            ovr027.free_stringList( ref stringList );

            ovr025.load_pic();
            ovr025.Player_Summary( gbl.player_ptr );
        }


        internal static void temple_shop( )
        {
            bool var_30 = false; /* Simeon */
            byte var_2F;
            bool var_2E;
            bool var_2D;
            string var_2C;
            bool var_3;
            byte var_2;
            char var_1;

            gbl.game_state = 1;
			gbl.byte_1EE7E = false;

			if( gbl.area_ptr.field_1CC == 0 )
			{
				gbl.byte_1EE7E = true;
			}
 
			ovr025.load_pic();
            gbl.byte_1EE7E = true;
            ovr025.Player_Summary( gbl.player_ptr );

			for( int i=0; i<7; i++ )
			{
				gbl.pooled_money[i] = 0;
			}

			gbl.something01 = false;
            var_2 = 0;

			do
			{
				ovr022.treasureOnGround( out var_2E, out var_2D );

				if( var_2D == true )
				{
					var_2C = "Heal View Take Pool Share Appraise Exit";
				}
				else
				{
					var_2C = "Heal View Pool Appraise Exit";
				}

				var_1 = ovr027.displayInput( out var_3, false, 1, 15, 10, 13, var_2C, string.Empty );

				switch( var_1 )
				{
					case 'H':
						if( var_3 == false )
						{
							temple_heal();
						}
						break;

					case 'V':
						ovr020.viewPlayer( out var_3 );
						break;

					case 'T':
						ovr022.takeItems();
						break;

					case 'P':
						if( var_3 == false )
						{
							ovr022.poolMoney();
						}
						break;

					case 'S':
						ovr022.share_pooled();
						break;

					case 'A':
						ovr022.appraiseGemsJewels( out var_30 );
						break;

					case 'E':		
						ovr022.treasureOnGround( out var_2E, out var_2D );

						if( var_2D == true )
						{
							var_2C = "~Yes ~No";

							seg041.press_any_key( "As you leave a priest says, \"Excuse me but you have left some money here\" ", true, 0, 10, 0x16, 0x26, 0x11, 1 );
							seg041.press_any_key( "Do you want to go back and retrieve your money?", true, 0, 10, 0x16, 0x26, 0x11, 1 );
							var_2F = ovr008.sub_317AA( false, 0, 15, 10, 13, var_2C, string.Empty );

							if( var_2F == 1 )
							{
								var_2 = 1;
							}
							else
							{
                                seg037.draw8x8_clear_area(0x16, 0x26, 17, 1);
							}
						}
						else
						{
							var_2 = 1;
						}

						break;

					case 'G':
						ovr020.sub_572CF( var_1 );
						break;

					case 'O':
						ovr020.sub_572CF( var_1 );
						break;
				}

				if( var_1 == 0x42 ||
					var_1 == 0x54 )
				{
					ovr025.load_pic();
				}
				else if( var_1 == 0x41 && var_30 == true )
				{
					ovr025.load_pic();
				}

				ovr025.Player_Summary( gbl.player_ptr );
			} while( var_2 == 0 );
        }
    }
}
