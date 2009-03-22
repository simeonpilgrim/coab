using Classes;
using System.Collections.Generic;

namespace engine
{
    class ovr007
    {
        internal static char ShopChooseItem(ref int index, out Item arg_4) // sub_2F04E
        {
            List<MenuItem> list = new List<MenuItem>();
            foreach(var item in gbl.items_pointer)
            {
				if( item._value == 0 )
				{
					item._value = 1;
				}

                int val = ItemsValue(item);

                list.Add(new MenuItem(string.Format("{0,-21}{1,9}", item.name, val), item));
            }

            gbl.menuSelectedWord = 0;

            MenuItem mi;

            char input_key = ovr027.sl_select_item(out mi, ref index, ref gbl.shopRedrawMenuItems, true, list,
                0x16, 0x26, 1, 1, 15, 10, 13, "Buy", "Items: " );

            arg_4 = mi.Item;

            foreach (var item in gbl.items_pointer)
            {
                ovr025.ItemDisplayNameBuild(false, false, 0, 0, item);
            }

            return input_key;
        }

        private static int ItemsValue(Item item_ptr)
        {
            int val;
            switch (gbl.area2_ptr.field_6DA)
            {
                case 0x01:
                    val = item_ptr._value >> 4;
                    break;

                case 0x02:
                    val = item_ptr._value >> 3;
                    break;

                case 0x04:
                    val = item_ptr._value >> 2;
                    break;

                case 0x08:
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
            return val;
        }


        internal static bool PlayerAddItem( Item item ) /*was overloaded */
        {
            bool wouldOverload;
            if (ovr020.canCarry(item, gbl.player_ptr) == true)
            {
                ovr025.string_print01("Overloaded");
                wouldOverload = true;
            }
            else
            {
                wouldOverload = false;

                gbl.player_ptr.items.Add(item.ShallowClone());

                ovr025.reclac_player_values(gbl.player_ptr);
            }

            return wouldOverload;
        }


        internal static void shop_buy() /* sub_2F474 */
        {
            seg037.DrawFrame_Outer();
            gbl.shopRedrawMenuItems = true;

            int index = 0;
            while(true)
            {
                Item item;
                char input_key = ShopChooseItem(ref index, out item);

                if (input_key != 'B' && input_key != 0x0d)
                {
                    return;
                }
                else
                {
                    int item_cost = ItemsValue(item);
                    int player_gold = ovr020.getPlayerGold(gbl.player_ptr);

                    if (item_cost <= player_gold)
                    {
                        bool overloaded = PlayerAddItem(item);

                        if (overloaded == false)
                        {
                            player_gold -= item_cost;
                            ovr022.setPlayerMoney(player_gold);
                        }
                    }
                    else
                    {
                        int pooled_gold = ovr022.getPooledGold();

                        if (item_cost <= pooled_gold)
                        {
                            bool overloaded = PlayerAddItem(item);

                            if (overloaded == false)
                            {
                                pooled_gold -= item_cost;
                                ovr022.setPooledGold(pooled_gold);
                            }
                        }
                        else
                        {
                            ovr025.string_print01("Not enough Money.");
                        }
                    }
                }
            }
        }


        internal static void sub_2F6E7( )
        {
            bool reloadPics = false; /* Simeon */
            bool items_on_ground;
            bool money_on_ground;
            char var_2E;

            gbl.game_state = GameState.Shop;
            gbl.redrawBoarder = (gbl.area_ptr.inDungeon == 0);

            ovr025.load_pic();
            gbl.redrawBoarder = true;
            ovr025.PartySummary( gbl.player_ptr );

			for( int var_1 = 0; var_1 < 7; var_1++ )
			{
				gbl.pooled_money[ var_1 ] = 0;
			}

            bool exitShop = false;

            gbl.items_pointer.ForEach(item => ovr025.ItemDisplayNameBuild(false, false, 0, 0, item));

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

                bool controlKey;

                var_2E = ovr027.displayInput(out controlKey, false, 1, 15, 10, 13, text, string.Empty);

                switch ( var_2E )
                {
                    case 'B':
                        shop_buy();
                        break;

                    case 'V':
                        ovr020.viewPlayer();
                        break;

                    case 'T':
                        ovr022.TakePoolMoney();
                        break;

                    case 'P':
                        if( controlKey == false )
                        {
                            ovr022.poolMoney();
                        }
                        break;


                    case 'S':
                        ovr022.share_pooled();
                        break;


                    case 'A':
                        reloadPics = ovr022.appraiseGemsJewels();
                        break;

                    case 'E':
                        ovr022.treasureOnGround( out items_on_ground, out money_on_ground );

                        if( money_on_ground == true )
                        {
                            seg041.press_any_key( "As you Leave the Shopkeeper says, \"Excuse me but you have Left Some Money here.\"  ", true, 0, 0x0a, 0x16, 0x26, 0x11, 1 );
                            seg041.press_any_key( "Do you want to go back and get your Money?", false, 0, 0x0f, 0x16, 0x26, 0x11, 1 );

                            int menu_selected = ovr008.sub_317AA(false, 0, 0x0f, 0x0a, 0x0d, "~Yes ~No", string.Empty);
                
                            if( menu_selected == 1 )
                            {
                                exitShop = true;
                            }
                            else
                            {
                                seg037.draw8x8_clear_area(0x16, 0x26, 17, 1);
                            }
                        }
                        else
                        {
                            exitShop = true;
                        }
                        break;

                    case 'G':
                        ovr020.scroll_team_list( var_2E );
                        break;

                    case 'O':
                        ovr020.scroll_team_list( var_2E );
                        break;
                }
                
                if( var_2E == 'B' ||
                    var_2E == 'T' )
                {
                    ovr025.load_pic();
                }
                else if( reloadPics == true )
                {
                    ovr025.load_pic();
                    reloadPics = false;
                }

                ovr025.PartySummary( gbl.player_ptr );

            }while( exitShop == false );
        }
    }
}
