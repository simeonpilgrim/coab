using Classes;

namespace engine
{
    class ovr007
    {
        internal static void sub_2F04E( ref short arg_0, out Item arg_4, ref Item arg_8, out char arg_C )
        {
            Item item_ptr;
            Item var_2;

            item_ptr = gbl.item_pointer;

            while( item_ptr != null )
            {
				if( item_ptr._value == 0 )
				{
					item_ptr._value = 1;
				}

				int val;
				switch( gbl.area2_ptr.field_6DA )
				{
					case 1:
						val = item_ptr._value >> 4;
						break;

					case 2:
						val = item_ptr._value >> 3;
						break;

					case 4:
						val = item_ptr._value >> 2;
						break;

					case 8:
						val = item_ptr._value >> 1;
						break;

					case 0x20:
						val = item_ptr._value << 1;
						break;

					case 0x40:
						val = item_ptr._value << 2;
						break;

					case 0x80:
						val = item_ptr._value << 3;
						break;

					default:
						val = item_ptr._value;
						break;
				}

                item_ptr.name = string.Format("{0,-21}{1,9}", item_ptr.name, val);
                item_ptr = item_ptr.next;
            }

            var_2 = gbl.item_pointer;
            gbl.byte_1D5BE = 0;

            arg_C = ovr027.sl_select_item(out arg_4, ref arg_0, ref gbl.byte_1AB16, true, var_2,
                0x16, 0x26, 1, 1, 15, 10, 13, "Buy", "Items: " );

			arg_8 = arg_4;

            item_ptr = gbl.item_pointer;

            while( item_ptr != null )
            {
                ovr025.ItemDisplayNameBuild( false, false, 0, 0, item_ptr, null );

                item_ptr = item_ptr.next;
            }
        }


        internal static void PlayerAddItem( out bool isOverloaded, Item item ) /*was overloaded */
        {

            if (ovr020.canCarry(item, gbl.player_ptr) == true)
            {
                ovr025.string_print01("Overloaded");
                isOverloaded = true;
            }
            else
            {
                isOverloaded = false;
                Item item_ptr = gbl.player_ptr.itemsPtr;

                if (item_ptr == null)
                {
                    item_ptr = item.ShallowClone();
                    item_ptr.next = null;
                    gbl.player_ptr.itemsPtr = item_ptr;
                }
                else
                {
                    while (item_ptr.next != null)
                    {
                        item_ptr = item_ptr.next;
                    }

                    Item var_8 = item.ShallowClone();
                    var_8.next = null;

                    item_ptr.next = var_8;
                }

                ovr025.sub_66C20(gbl.player_ptr);
            }
        }


        internal static void sub_2F474( )
        {
            short var_15;
            Item var_13;
            Item var_11;
            bool var_F;
            int var_E;
            int var_A;
            int var_8;
            bool var_6;
            char var_5;
            Item var_4;

            var_4 = null;
            var_11 = gbl.item_pointer;
            var_15 = 0;
            seg037.draw8x8_01();
            gbl.byte_1AB16 = true;

			do
			{
				sub_2F04E( ref var_15, out var_13, ref var_4, out var_5 );

				if( var_5 != 0x42 &&
					var_5 != 0x0d )
				{
					var_6 = true;
				}
				else
				{
					var_6 = false;
                    var_8 = var_4._value;

					switch( gbl.area2_ptr.field_6DA)
					{
						case 0x01:
							var_8 = var_4._value >> 4;

							break;

						case 0x02:
							var_8 = var_4._value >> 3;

							break;

						case 0x04:
							var_8 = var_4._value >> 2;
							break;

						case 0x08:
							var_8 = var_4._value >> 1;
							break;

						case 0x20:
							var_8 = var_4._value << 1;
							break;

						case 0x40:
							var_8 = var_4._value << 2;
							break;

						case 0x80:
							var_8 = var_4._value << 3;
							break;
					}

					var_A = ovr020.getPlayerGold( gbl.player_ptr );

					if( var_8 <= var_A )
					{
						PlayerAddItem( out var_F, var_4 );

						if( var_F == false )
						{
							var_A -= var_8;
							ovr022.setPlayerMoney( var_A );
						}
					}
					else
					{
						var_E = ovr022.getPooledGold( gbl.pooled_money );

						if( var_8 <= var_E )
						{
							PlayerAddItem( out var_F, var_4 );

							if( var_F == false )
							{
								var_E -= var_8;
								ovr022.setPooledGold( var_E );
							}
						}
						else
						{
							ovr025.string_print01( "Not enough Money." );
						}
					}
				}
			}while( var_6 == false );
        }


        internal static void sub_2F6E7( )
        {
            Item item_ptr;
            bool var_32 = false; /* Simeon */
            byte var_31;
            bool items_on_ground;
            bool money_on_ground;
            char var_2E;
            bool var_2C;

            gbl.game_state = 1;
            gbl.byte_1EE7E = (gbl.area_ptr.field_1CC == 0);

            ovr025.load_pic();
            gbl.byte_1EE7E = true;
            ovr025.Player_Summary( gbl.player_ptr );

			for( int var_1 = 0; var_1 < 7; var_1++ )
			{
				gbl.pooled_money[ var_1 ] = 0;
			}

            gbl.something01 = false;
            bool var_2D = false;
            item_ptr = gbl.item_pointer;

            while( item_ptr != null )
            {
                ovr025.ItemDisplayNameBuild(false, false, 0, 0, item_ptr, null);

                item_ptr = item_ptr.next;
            }

            do
            {
                ovr022.treasureOnGround( out items_on_ground, out money_on_ground );

                string text;
                if( money_on_ground == true )
                {
                    text = "Buy View Take Pool Share Appraise Exit";
                }
                else
                {
                    text = "Buy View Pool Appraise Exit";
                }

                var_2E = ovr027.displayInput(out var_2C, false, 1, 15, 10, 13, text, string.Empty);

                switch ( var_2E )
                {
                    case 'B':
                        sub_2F474();
                        break;

                    case 'V':
                        bool dummyBool;
                        ovr020.viewPlayer(out dummyBool);
                        break;

                    case 'T':
                        ovr022.takeItems();
                        break;

                    case 'P':
                        if( var_2C == false )
                        {
                            ovr022.poolMoney();
                        }
                        break;


                    case 'S':
                        ovr022.share_pooled();
                        break;


                    case 'A':
                        ovr022.appraiseGemsJewels( out var_32 );
                        break;

                    case 'E':
                        ovr022.treasureOnGround( out items_on_ground, out money_on_ground );

                        if( money_on_ground == true )
                        {
                            seg041.press_any_key( "As you Leave the Shopkeeper says, \"Excuse me but you have Left Some Money here.\"  ", true, 0, 0x0a, 0x16, 0x26, 0x11, 1 );
                            seg041.press_any_key( "Do you want to go back and get your Money?", false, 0, 0x0f, 0x16, 0x26, 0x11, 1 );

                            var_31 = ovr008.sub_317AA(false, 0, 0x0f, 0x0a, 0x0d, "~Yes ~No", string.Empty);
                
                            if( var_31 == 1 )
                            {
                                var_2D = true;
                            }
                            else
                            {
                                seg037.draw8x8_clear_area(0x16, 0x26, 17, 1);
                            }
                        }
                        else
                        {
                            var_2D = true;
                        }
                        break;

                    case 'G':
                        ovr020.sub_572CF( var_2E );
                        break;

                    case 'O':
                        ovr020.sub_572CF( var_2E );
                        break;
                }
                
                if( var_2E == 0x42 ||
                    var_2E == 0x54 )
                {
                    ovr025.load_pic();
                }
                else if( var_2E == 0x41 &&
                    var_32 == true )
                {
                    ovr025.load_pic();
                }

                ovr025.Player_Summary( gbl.player_ptr );

            }while( var_2D == false );
        }
    }
}
