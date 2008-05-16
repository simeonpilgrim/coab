using Classes;

namespace engine
{
    class ovr006
    {
        internal static void calc_battle_exp( ref int longint_ptr )
        {
            byte var_12;
            Item item;
            Item item_ptr;
            int total;
            byte loop2_var;
            Player player;

            if (gbl.combat_type == gbl.combatType.duel)
			{
				longint_ptr = gbl.player_ptr.field_E5 * 100;
			}
			else
			{
				/* Go through all players in battle
				 * Add the money from each monster
				 */
				total = 0;

				player = gbl.player_next_ptr;
				var_12 = 0;

				while( player != null )
				{
					item = player.itemsPtr;
					
					if( player.combat_team == CombatTeam.Enemy &&
						player.health_status != Status.okey &&
						player.health_status != Status.running )
					{
						gbl.byte_1AB14 = 1;

						for( loop2_var = 0; loop2_var <= 6; loop2_var++ )
						{
                            gbl.pooled_money[loop2_var] += player.Money[loop2_var];
						}

						total += player.field_13E * player.field_12C;
						total += player.field_13C;

						if( gbl.area2_ptr.field_5C6 != 1 )
						{
							while( item != null )
							{
								var_12++;
								
								item_ptr = gbl.item_pointer;

                                ovr025.ItemDisplayNameBuild(false, false, 0, 0, item, player);
									
								gbl.item_pointer = item.ShallowClone();

								gbl.item_pointer.readied = false;
								gbl.item_pointer.next = item_ptr;

								item = item.next;
							}
						}
					}
					player = player.next_player;
				}

				total += gbl.pooled_money[ money.copper ] / 200;
				total += gbl.pooled_money[ money.silver ] / 20;
				total += gbl.pooled_money[ money.electrum ] / 2;
				total += gbl.pooled_money[ money.gold ];
				total += gbl.pooled_money[ money.platum ] * 5;

				total += gbl.pooled_money[ money.gem ] * 250;
				total += gbl.pooled_money[ money.jewelry ] * 2200;

				item_ptr = gbl.item_pointer;

				while( gbl.item_pointer != null &&
					   item_ptr != gbl.item_ptr )
				{
					if( item_ptr.plus > 0 )
					{
						total += item_ptr.plus * 400;
					}

					item_ptr = item_ptr.next;
				}

				longint_ptr = total / (gbl.area2_ptr.field_67C - gbl.byte_1EE81);
			}
        }


        internal static void addExp( int exp_to_add )
        {
            int new_exp;
            Player player;

			player = gbl.player_next_ptr;
            
			while( player != null )
			{
				if( player.in_combat == true &&
					player.health_status != Status.animated )
				{
					new_exp = exp_to_add;

					switch( player._class )
					{
						case ClassId.cleric:
							if( player.wis > 15 )
							{
								new_exp = exp_to_add + (exp_to_add/10);
							}
							break;

						case ClassId.fighter:
							if( player.strength > 15 )
							{
								new_exp = exp_to_add + (exp_to_add/10);
							}
							break;

						case ClassId.paladin:
							if( player.strength > 15 &&
								player.wis > 15 )
							{
								new_exp = exp_to_add + (exp_to_add/10);
							}
							break;

						case ClassId.ranger:
							if( player.strength > 15 &&
								player._int > 15 &&
								player.wis > 15 )
							{
								new_exp = exp_to_add + (exp_to_add/10);
							}
							break;

						case ClassId.magic_user:
							if( player._int > 15 )
							{
								new_exp = exp_to_add + (exp_to_add/10);
							}
							break;

						case ClassId.thief:
							if( player.dex > 15 )
							{
								new_exp = exp_to_add + (exp_to_add/10);
							}
							break;


						default:

							if( player._class == ClassId.mc_c_f ||
								( player._class >= ClassId.mc_c_r && player._class <= ClassId.mc_f_t ) ||
								player._class == ClassId.mc_f_t )
							{
								new_exp = exp_to_add / 2;
							}
							else if( player._class == ClassId.mc_c_f_m ||
								player._class == ClassId.mc_f_mu_t )
							{
								new_exp = exp_to_add / 3;
							}
							break;

					}

					player.exp += new_exp;
				}

				player = player.next_player;
			}
        }

		static Affects[] affects_array = new Affects[] {
													Affects.affect_03,
													Affects.charm_person,
													Affects.affect_0d,
													Affects.silence_15_radius,
													Affects.spiritual_hammer,
													Affects.fumbling,
													Affects.confuse,
													Affects.affect_28,
													Affects.snake_charm,
													Affects.paralyze,
													Affects.sleep,
													Affects.affect_3a,
													Affects.affect_5b,
													Affects.affect_88,
													Affects.affect_89,
													Affects.affect_8b,
													Affects.affect_8e,
													Affects.affect_90,
													Affects.helpless
												};

        internal static void sub_2D556( )
        {
            byte no_exp;
            byte var_6;
            byte var_5;
            Player player;

            gbl.byte_1EE81 = 0;
            gbl.byte_1B2F0 = 1;
            gbl.party_fled = false;
            player = gbl.player_next_ptr;

            while( player != null && (player.actions == null || player.actions.field_12 != 1 ))
            {
                if( player.health_status == Status.running )
                {
                    gbl.party_fled = true;
                }

                player = player.next_player;
            }

            player = gbl.player_next_ptr;
            no_exp = 0;

            while( player != null && no_exp == 0 )
            {
                if( player.in_combat == true ||
                    player.health_status == Status.unconscious ||
                    player.health_status == Status.running ||
                    player.health_status == Status.dying )
                {
                    no_exp = 1;
                }

                player = player.next_player;
            }

            if( gbl.combat_type == gbl.combatType.duel ||
                ( gbl.area2_ptr.field_5CC != 0 && no_exp != 0 ) )
            {
                gbl.byte_1B2F0 = 0;
            }

            gbl.byte_1EE86 = 0;
            player = gbl.player_next_ptr;

            if (gbl.combat_type == gbl.combatType.normal ||
                gbl.inDemo == false )
            {
                while( player != null && (player.actions == null || player.actions.field_13 != 1 ))
                {
                    if( player.health_status == Status.running ||
                        player.health_status == Status.animated ||
                        player.health_status == Status.okey )
                    {
                        if (player.combat_team == CombatTeam.Ours &&
                            player.field_F7 < 0x80 )
                        {
                            gbl.byte_1B2F0 = 0;
                        }
                    }

                    if( player.health_status == Status.animated ||
                        player.health_status == Status.okey )
                    {
                        gbl.byte_1EE86 = 1;
                        gbl.party_fled = false;
                    }

                    if( player.in_combat == false ||
                        player.health_status == Status.animated )
                    {
                        gbl.byte_1EE81++;
                    }

                    for( var_6 = 0; var_6 < 0x13; var_6++)
                    {
                        ovr024.remove_affect( null , affects_array[var_6], player );
                    }
                    player = player.next_player;
                }

                if( gbl.byte_1EE86 != 0 )
                {
                    calc_battle_exp( ref gbl.exp_to_add );
                    addExp( gbl.exp_to_add );
                }

                player = gbl.player_next_ptr;

                if( gbl.byte_1B2F0 == 0 )
                {
                    while( player != null && (player.actions == null || player.actions.field_13 != 1 ))
                    {
                        if( gbl.party_fled == false )
                        {
                            switch( player.health_status )
                            {
                                case Status.running:
                                    player.health_status = Status.okey;
                                    player.in_combat = true;
                                    break;

                                case Status.dying:
                                    if( gbl.area2_ptr.field_5CC != 0 )
                                    {
                                        player.health_status = Status.okey;
                                        player.in_combat = true;
                                        player.hit_point_current = 1;
                                    }
                                    else
                                    {
                                        player.health_status = Status.unconscious;
                                    }
                                    break;

                                case Status.unconscious:
                                    if( player.hit_point_current > 0 )
                                    {
                                        player.health_status = Status.okey;
                                        player.in_combat = true;
                                    }
                                    else if( gbl.area2_ptr.field_5CC != 0 )
                                    {
                                        player.health_status = Status.okey;
                                        player.in_combat = true;
                                        player.hit_point_current = 1;
                                    }
                                    break;
                            }
                            player = player.next_player;
                        }
                        else
                        {
                            gbl.area2_ptr.field_58E = 0x81;

                            if( player.health_status == Status.running )
                            {
                                player.health_status = Status.okey;
                                player.in_combat = true;
                                player = player.next_player;
                            }
                            else
                            {
                                gbl.player_ptr = player;
                                player = player.next_player;
                                ovr018.free_players(1, false);
                            }
                        }
                    }
                }
                else
                {
                    player = gbl.player_next_ptr;
                    var_5 = 0;

                    while( player != null && var_5 == 0 )
                    {
                        if( player.actions.field_13 != 1 )
                        {
                            gbl.player_ptr = player;
                            ovr018.free_players(1, false);
                        }
                        else
                        {
                            var_5 = 1;
                        }

                        player = player.next_player;
                    }

                    gbl.area2_ptr.field_67C = 0;
                }
            }
            else
            {
                player = gbl.player_next_ptr;
                while( player != null )
                {
                    if( player.in_combat == true &&
                        player.health_status == Status.okey &&
                        player.combat_team == CombatTeam.Ours)
                    {
                        gbl.byte_1EE86 = 1;
                        calc_battle_exp( ref gbl.exp_to_add );
                        addExp( gbl.exp_to_add );
                    }

                    player = player.next_player;
                }

                player = gbl.player_next_ptr;

                while( player != null )
                {
                    if( player.health_status == Status.okey ||
                        player.health_status == Status.animated )
                    {
                        player.in_combat = true;
                    }

                    if( player.health_status == Status.dying )
                    {
                        player.health_status = Status.unconscious;
                    }

                    player = player.next_player;
                }
            }
        }


        internal static void displayCombatResults(int arg_0) /* sub_2DABC */
		{
			seg037.draw8x8_01();

			if( gbl.byte_1AB14 != 0 ||
                gbl.combat_type == gbl.combatType.duel)
			{
				if( gbl.party_fled == true )
				{
					seg041.displayString( "The party has fled.", 0, 10, 3, 1 );

					arg_0 = 0;

                    Item item_ptr = gbl.item_pointer;
					while( item_ptr != null )
					{
						Item var_115 = item_ptr.next;

						item_ptr = var_115;
					}

					for( int i=0; i<7; i++ )
					{
						gbl.pooled_money[i] = 0;
					}
				}
				else
				{
                    if ((gbl.combat_type == gbl.combatType.duel && gbl.byte_1EE86 == 0) ||
						( gbl.byte_1EE86 == 0 && gbl.area2_ptr.field_5CC != 0 ) )
					{
						gbl.area2_ptr.field_58E = 0x80;
						seg041.displayString( "You have lost the fight.", 0, 10, 3, 1 );

						arg_0 = 0;
					}
					else
					{
						if( gbl.combat_type == gbl.combatType.duel )
						{
							seg041.displayString( "You have won the duel.", 0, 10, 3, 1 );
						}
						else
						{
							seg041.displayString( "The party has won.", 0, 10, 3, 1 );
						}
					}
				}
			}
			else
			{
				seg041.displayString( "The party has found Treasure!", 0, 10, 3, 1 );
			}

            string text;
            if (gbl.combat_type == gbl.combatType.duel)
			{

                text = "The duelist receives " + arg_0.ToString();
			}
			else
			{
                text = "Each character receives " + arg_0.ToString();
			}

            seg041.displayString(text, 0, 10, 5, 1);
			seg041.displayString( "experience points.", 0, 10, 7, 1 );

            bool dummyBool;
            ovr027.displayInput(out dummyBool, false, 1, 15, 15, 15, "press <enter>/<return> to continue", string.Empty);
		}


        internal static void select_treasure(ref short index, out Item selectedItem, out char key) /* sub_2DD2B */
        {
            seg037.draw8x8_01();

            Item tmpItem = gbl.item_pointer;
            while( tmpItem != null )
            {
                ovr025.ItemDisplayNameBuild(false, false, 0, 0, tmpItem, null);
                tmpItem = tmpItem.next;
            }

            bool var_18 = true;
            key = ovr027.sl_select_item(out selectedItem, ref index, ref var_18, true, gbl.item_pointer, 
                 0x16, 0x26, 1, 1, 15, 10, 13, "Take", "Items: " );

        }


        internal static void take_items_treasure() /* sub_2DDFC */
        {
            bool stop;
            char key;
            Item var_4;
            short dummyShort = 0;

            do
            {
                select_treasure(ref dummyShort, out var_4, out key);

                if (key != 'T' &&
                    key != '\r')
                {
                    stop = true;
                }
                else
                {
                    stop = false;

                    bool willOverload;
                    ovr007.PlayerAddItem(out willOverload, var_4);

                    if (willOverload == false)
                    {
                        Item var_8 = gbl.item_pointer;

                        if (var_8 == var_4)
                        {
                            gbl.item_pointer = var_4.next;
                        }
                        else
                        {
                            while (var_8.next != var_4)
                            {
                                var_8 = var_8.next;
                            }

                            var_8.next = var_4.next;
                        }

                        var_4.next = null;
                        var_4 = null;

                        if (gbl.item_pointer == null)
                        {
                            stop = true;
                        }
                    }
                }
            } while (stop == false);

            ovr025.load_pic();
        }


        internal static void take_treasure(ref bool items_present, ref bool money_present) /* sub_2DF2E */
        {
            bool ctrl_key;
            byte var_2;

            var_2 = 0;
            if( money_present == true )
            {
                if( items_present == true )
                {
                    do
                    {
                        char key = ovr027.displayInput( out ctrl_key, true, 1, 15, 10, 13, "Money Items Exit", "Take: " );
                        
                        switch( key )
                        {
                            case 'M':
                            ovr022.takeItems();
                                ovr025.load_pic();
                                break;

                            case 'I':
                                take_items_treasure();
                                break;

                            case 'E':
                                goto case '\0';
  
                            case '\0':
                                var_2 = 1;
                                break;

                            case 'G':
                                ovr020.sub_572CF( key );
                                break;

                            case 'O':
                                ovr020.sub_572CF( key );
                                break;
                        }

                        ovr025.Player_Summary( gbl.player_ptr );
                        ovr022.treasureOnGround( out items_present, out money_present );
                        
                        if( money_present == false ||
                            items_present == false )
                        {
                            var_2 = 1;
                        }
                    }while( var_2 == 0 );
                }
                else
                {
                    ovr022.takeItems();
                    ovr025.load_pic();
                }
            }
            else
            {
                take_items_treasure();
            }
        }


        internal static void distributeCombatTreasure() /* sub_2E0C3 */
        {
            string suffix;
            byte var_10B = 0; /* Simeon */
            byte var_109;
            string var_108;
            bool ctrl_key;
            bool var_105;
            char var_104;
            string text;
            bool items_present;
            bool money_present;

            var_108 = string.Empty;
            var_105 = false;
            ovr025.load_pic();

            do
            {
                ovr022.treasureOnGround(out items_present, out money_present);

                text = "View Pool Exit";
                suffix = " Exit";
                bool can_detect_magic = false;
                int index = 0;

                if (items_present == true)
                {
                    while (index < gbl.max_spells && can_detect_magic == false)
                    {
                        if (gbl.player_ptr.spell_list[index] == 5 ||
                            gbl.player_ptr.spell_list[index] == 11 ||
                            gbl.player_ptr.spell_list[index] == 0x4D)
                        {
                            if (gbl.player_ptr.in_combat == true)
                            {
                                can_detect_magic = true;
                                var_10B = gbl.player_ptr.spell_list[index];
                            }
                        }

                        index++;
                    }
                }

                if (can_detect_magic == true)
                {
                    suffix = " Detect Exit";
                }

                if (money_present == true)
                {
                    text = "View Take Pool Share" + suffix;
                }
                else if (items_present == true)
                {
                    text = "View Take Pool" + suffix;
                }

                var_104 = ovr027.displayInput(out ctrl_key, true, 1, 15, 10, 13, text, var_108);

                switch (var_104)
                {
                    case 'V':
                        bool dummyBool;
                        ovr020.viewPlayer(out dummyBool);
                        break;

                    case 'T':
                        take_treasure(ref items_present, ref money_present);
                        break;

                    case 'P':
                        if (ctrl_key == false)
                        {
                            ovr022.poolMoney();
                        }
                        break;

                    case 'S':
                        ovr022.share_pooled();
                        break;

                    case 'D':
                        ovr023.sub_5D2E1(ref ctrl_key, 0, 0, var_10B);
                        break;

                    case 'E':
                    case '\0':
                        ovr022.treasureOnGround(out items_present, out money_present);

                        if (money_present == true || items_present == true)
                        {
                            seg041.press_any_key("There is still treasure left.  ", true, 0, 10, 0x16, 0x26, 0x11, 1);
                            seg041.press_any_key("Do you want to go back and claim your treasure?", false, 0, 15, 0x16, 0x26, 0x11, 1);
                            var_109 = ovr008.sub_317AA(false, 0, 15, 10, 13, "~Yes ~No", var_108);

                            if (var_109 == 1)
                            {
                                var_105 = true;
                            }
                            else
                            {
                                seg037.draw8x8_clear_area(0x16, 0x26, 17, 1);
                            }
                        }
                        else
                        {
                            var_105 = true;
                        }
                        break;

                    case 'G':
                        ovr020.sub_572CF(var_104);
                        ovr025.Player_Summary(gbl.player_ptr);
                        break;

                    case 'O':
                        ovr020.sub_572CF(var_104);
                        ovr025.Player_Summary(gbl.player_ptr);
                        break;
                }
            } while (var_105 == false);
        }


        internal static void sub_2E3C7( )
        {
            Player var_6;
            Player player;

            gbl.area2_ptr.field_590 = 0;
			player = gbl.player_next_ptr;

			while( player != null )
			{
                if( (player.actions != null && player.actions.field_13 == 1 ) ||
                    player.combat_team == CombatTeam.Enemy)
                {
                    gbl.byte_1AB14 = 1;
                    if( player.in_combat == false )
                    {
                        gbl.area2_ptr.field_590++;
                    }

                    var_6 = player.next_player;

                    gbl.player_ptr = player;

                    ovr018.free_players(1, (player.actions != null && player.actions.field_13 == 1));

                    player = var_6;
                }
                else
                {
                    if( player.actions != null )
                    {
                        player.actions = null;
                    }

                    player = player.next_player;
                }
			}

			gbl.player_ptr = gbl.player_next_ptr;
        }


        internal static void distributeNpcTreasure() /*sub_2E50E*/
        {
            bool treasureTaken = false;

            Player player = gbl.player_next_ptr;
            int npcParts = 0;
            int totalParts = 0;
 
			while( player != null )
			{
				if( player.field_F7 > 0x7f &&
					player.health_status == Status.okey )
				{
					npcParts += (byte)(player.field_F8 & 7);
					totalParts += (byte)(player.field_F8 & 7);
				}
				else
				{
					totalParts++;
				}

				player = player.next_player;
			}

            if( npcParts > 0 )
            {
                for( int i = 0; i <= 6; i++ )
                {
                    if( gbl.pooled_money[i] > 0 )
                    {
                        gbl.pooled_money[i] -= (gbl.pooled_money[i] / totalParts) * npcParts;

                        treasureTaken = true;
                    }
                }
            }

            if( treasureTaken )
            {
                seg037.draw8x8_01();
                player = gbl.player_next_ptr;
                int yCol = 0;

                while( player != null )
                {
                    if( player.field_F7 > 0x7f &&
                        player.health_status == Status.okey &&
                        player.field_F8 > 0 )
                    {
                        string output = player.name + " takes and hides " + ((player.sex == 0) ? "his" : "her") + " share.";

                        seg041.press_any_key(output, true, 0, 10, 0x16, 0x22, (byte)(yCol + 5), 5);

                        yCol += 2;
                    }

                    player = player.next_player;
                }

                bool tmpBool;
                ovr027.displayInput( out tmpBool, false, 1, 15, 15, 15, "press <enter>/<return> to continue", string.Empty );
            }
        }


        internal static void sub_2E7A2( )
        {
            Player player;
            Item item_ptr;
            Item item;

            gbl.area2_ptr.field_58E = 0;
            gbl.byte_1AB14 = 0;

            if( gbl.inDemo == false )
            {
                sub_2D556();
            }

            gbl.game_state = 6;
            
            sub_2E3C7();

            if( gbl.inDemo == false )
            {
                player = gbl.player_next_ptr;

                while( player != null )
                {
                    ovr025.sub_66C20( player );
                    player = player.next_player;
                }

                if( gbl.byte_1B2F0 == 0 ||
                    gbl.combat_type == gbl.combatType.duel )
                {
                    if( gbl.party_fled == true )
                    {
                        item = gbl.item_pointer;

                        while( item != null )
                        {
                            item_ptr = item;
                            item = item.next;

                            item_ptr = null;
                        }

                        gbl.item_pointer = null;

                    }

                    if(gbl.inDemo == false )
                    {
                        distributeNpcTreasure();
                        displayCombatResults( gbl.exp_to_add ); 
                        distributeCombatTreasure();
                    }
 
                    item = gbl.item_pointer;

                    while( item != null )
                    {
                        item_ptr = item;
                        item = item.next;
                        seg051.FreeMem( 0x3f, item_ptr );
                    }

                    gbl.item_pointer = null;
                }
                else
                {
                    gbl.area2_ptr.field_58E = 0x80;
                    seg037.draw8x8_01();
                    gbl.textXCol = 2;
                    gbl.textYCol = 6;
                    seg041.press_any_key( "The monsters rejoice for the party has been destroyed", true, 0, 10, 0x16, 0x25, 5, 2 );
                    seg041.displayAndDebug( "Press any key to continue", 0, 0x0d );

                }

                gbl.DelayBetweenCharacters = true;
                gbl.area2_ptr.field_6E0 = 0;
                gbl.area2_ptr.field_6E2 = 0;
                gbl.area2_ptr.field_6E4 = 0;
                gbl.area2_ptr.field_5C6 = 0;
                gbl.area2_ptr.field_5CC = 0;

            }
        }

    }
}
