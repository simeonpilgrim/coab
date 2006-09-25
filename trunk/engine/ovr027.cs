using Classes;

namespace engine
{
	class ovr027
	{
        static Set yesNoFlags = new Set( 0x0903, new byte[] { 0x40, 0x00, 0x02 } );

        internal ovr027()
        {
            yesNoFlags += (9*8) + 6;
            yesNoFlags += (11*8) + 1;
        }

        internal static T getStringListEntry<T>( T list, int index ) where T : class, Classes.IListBase
        {
            int loop_var;
            T current;
            T retList;

            loop_var = 0;

            current = list;

            while( current != null && loop_var != index )
            {
                current = (T)current.Next();
                loop_var++;
            }

            if( loop_var == index )
            {
                retList = current;
            }
            else
            {
                retList = null;
            }

            return retList;
        }


        internal static short StringListCount<T>(T list) where T : class, IListBase
        {
            short count;
            T current;

            count = 0;

            current = list;

            while (current != null)
            {
                count++;

                current = (T)current.Next();
            }

            return count;
        }

        static Set unk_6C0BA = new Set(0x0606, new byte[] { 0xFF, 3, 0xFE, 0xFF, 0xFF, 7 });

        /// <summary>
        /// sub_6C0DA
        /// </summary>
        internal static void buildInputKeys(string arg_2, byte[] bp_var_8D, out byte bp_var_8E)
        {
            byte var_2C;
            byte var_2B;
            byte var_2A;

            seg051.FillChar(0xff, 0x28, bp_var_8D);

            var_2A = 0;
            var_2C = (byte)arg_2.Length;

            for (var_2B = 0; var_2B < var_2C; var_2B++)
            {
                if (unk_6C0BA.MemberOf(arg_2[var_2B ]) == true)
                {
                    if (bp_var_8D[var_2A << 1] == 0xff)
                    {
                        bp_var_8D[var_2A << 1] = var_2B;
                    }
                    else
                    {
                        bp_var_8D[(var_2A << 1) + 1] = (byte)(var_2B - 2);
                        var_2A++;
                        bp_var_8D[var_2A  << 1] = var_2B;
                    }
                }
            }

            bp_var_8D[(var_2A << 1) + 1] = var_2C;
            bp_var_8E = var_2A;
        }


        internal static void sub_6C1E9(byte arg_2, byte bp_var_64, byte bp_arg_8, string bp_var_53, byte bp_var_65, byte bp_arg_A, byte[] bp_var_8D)
		{
			byte var_1;

			if( bp_var_64 > 0 )
			{
                for (var_1 = 0; var_1 < bp_var_64; var_1++)
				{
                    if (bp_var_8D[arg_2 << 1] <= var_1 &&
                        bp_var_8D[(arg_2 << 1) + 1] >= var_1 &&
                        bp_arg_8 != 0)
                    {
                        seg041.display_char01(true, bp_var_53[var_1], 1, bp_arg_8, 0, 0x18, bp_var_65 + var_1);
                    }
                    else
                    {
                        if (unk_6C0BA.MemberOf(bp_var_53[var_1]) == true)
                        {
                            seg041.display_char01(true, bp_var_53[var_1], 1, 0, bp_arg_8, 0x18, bp_var_65 + var_1);
                        }
                        else
                        {
                            seg041.display_char01(true, bp_var_53[var_1], 1, 0, bp_arg_A, 0x18, bp_var_65 + var_1);
                        }
                    }
				}

				if( bp_var_64 + bp_var_65 < 0x27 )
				{
					seg041.display_char01( true, ' ', (byte)(( 0x27 - bp_var_64 - bp_var_65) + 1),
						0, 0, 0x18, ( bp_var_65 + bp_var_64 ) );
				}

                Display.Update();
			}
		}

        static Set unk_6C398 = new Set(0x0408, new byte[] { 1, 0, 0xFF, 3, 0xFE, 0xFF, 0xFF, 7 });
        static Set unk_6C3B8 = new Set(0x0606, new byte[] { 0xFE, 3, 0, 0, 0, 0x10 });

        static byte[] unk_18AE0 = { 0x4F, 0x50, 0x51, 0x4B, 0x20, 0x4D, 0x47, 0x48, 0x49 };


		internal static char displayInput( out bool arg_0, byte arg_4, byte arg_6, byte arg_8, byte arg_A, byte arg_C, string accepted_keys, string displayString )
		{
			byte var_90;
			byte var_8F;
			byte var_8E;
            byte[] var_8D = new byte[0x28];
			byte var_65;
			byte var_64;
			byte var_63;
			byte var_62;
			byte var_61;
			char var_60;
			int var_5F;
			int var_5B;
			int var_57;
			string var_53;
			string var_2A;

			var_2A = displayString;
			var_53 = accepted_keys;

			gbl.byte_1D5BF = false;

			var_8F = ( (arg_A != 0) || (arg_8 != 0) )?(byte)1:(byte)0;

			buildInputKeys( var_53, var_8D, out var_8E );

			if( gbl.byte_1D5BE >= var_8E )
			{
				gbl.byte_1D5BE = 0;
			}

			var_64 = (byte)var_53.Length;
			var_60 = '\0';
            arg_0 = false;
			var_63 = 0;

			var_57 = seg041.time01();
			var_5B = seg041.time01() + 30;

			var_5F = var_5B + 50;

			if( var_2A.Length != 0 )
			{
				seg041.displayString( var_2A, 0, arg_C, 0x18, 0 );
			}

			var_65 = (byte)var_2A.Length;

            sub_6C1E9(gbl.byte_1D5BE, var_64, arg_8, var_53, var_65, arg_A, var_8D);

			if( gbl.game_state == 3 &&
				gbl.byte_1D5BA == 0x79 &&
				gbl.byte_1D5B4 != 0x50 )
			{
				ovr028.sub_6E005();
				ovr028.sub_6E02E();
				ovr028.sub_6E05D();
			}

			do
			{
				if( gbl.game_state == 3 &&
					gbl.byte_1D5BA == 0x79 &&
					gbl.byte_1D5B4 != 0x50 &&
					seg041.time01() >= var_5B )
				{
					ovr028.sub_6E02E();
					var_5B = var_5F + 30;
				}

				if( ( gbl.area_ptr.field_3FE != 0 || arg_4 != 0 ) &&
                    gbl.byte_1D556.curFrame > 0)
				{
                    ovr030.sub_7000A(gbl.byte_1D556.ptrs[gbl.byte_1D556.curFrame].field_4, arg_4, 3, 3);

                    if ((seg041.time01() - var_57) >= (gbl.byte_1D556.ptrs[gbl.byte_1D556.curFrame].field_0 * 10) ||
						gbl.area_ptr.field_3FE != 0 )
					{
                        gbl.byte_1D556.curFrame++;

                        if (gbl.byte_1D556.curFrame > gbl.byte_1D556.numFrames)
						{
                            gbl.byte_1D556.curFrame = 1;
						}

						var_57 = seg041.time01();
					}
				}

                if (gbl.word_1D5C0 > 0 &&
                    (seg041.time01() - var_57) >= gbl.word_1D5C0)
                {
                    var_60 = (char)gbl.byte_1D5C4;
                    var_63 = 1;
                }
                else if (seg049.KEYPRESSED() == true)
                {
                    var_60 = (char)seg043.debug_txt();

                    if (var_60 == 0)
                    {
                        var_60 = (char)seg043.debug_txt();

                        if (arg_6 != 0)
                        {
                            arg_0 = true;
                            var_63 = 1;
                        }
                    }
                    else if (var_60 == 0x1B)
                    {
                        var_63 = 1;
                        var_60 = '\0';
                    }
                    else if (var_60 == 13)
                    {
                        if (var_8F != 0)
                        {
                            if (var_8D[(gbl.byte_1D5BE) << 1] != 0xff)
                            {
                                var_60 = var_53[var_8D[(gbl.byte_1D5BE) << 1]];
                            }
                            else
                            {
                                var_60 = '\r';
                            }

                            var_63 = 1;
                        }
                    }
                    else if (var_60 == 0x2C)
                    {
                        if (gbl.byte_1D5BE == 0)
                        {
                            gbl.byte_1D5BE = (byte)(var_8E - 1);
                        }
                        else
                        {
                            gbl.byte_1D5BE--;
                        }

                        sub_6C1E9(gbl.byte_1D5BE, var_64, arg_8, var_53, var_65, arg_A, var_8D);
                    }
                    else if (var_60 == 0x2E)
                    {
                        gbl.byte_1D5BE++;

                        if (gbl.byte_1D5BE >= var_8E)
                        {
                            gbl.byte_1D5BE = 0;
                        }

                        sub_6C1E9(gbl.byte_1D5BE, var_64, arg_8, var_53, var_65, arg_A, var_8D);
                    }
                    else
                    {
                        var_60 = seg051.UpCase(var_60);
                        if (unk_6C398.MemberOf(var_60) == true)
                        {
                            if (var_60 == 0x20)
                            {
                                var_63 = 1;
                            }
                            else
                            {
                                var_90 = var_64;
                                for (var_62 = 0; var_62 < var_90; var_62++)
                                {
                                    if (var_53[var_62] == var_60)
                                    {
                                        var_63 = 1;
                                        var_61 = 0;

                                        while (var_8D[var_61 << 1] != var_62)
                                        {
                                            var_61++;
                                        }

                                        gbl.byte_1D5BE = var_61;

                                        sub_6C1E9(gbl.byte_1D5BE, var_64, arg_8, var_53, var_65, arg_A, var_8D);
                                    }
                                }
                            }
                        }

                        if (arg_6 != 0 &&
                            unk_6C3B8.MemberOf(var_60) == true)
                        {
                            if (var_60 == 0x5C)
                            {
                                var_60 = '7';
                            }
                            else
                            {
                                var_60 = (char)unk_18AE0[var_60 - 0x31];
                            }

                            arg_0 = true;
                            var_63 = 1;
                        }
                    }
                }

				if( gbl.game_state == 3 &&
					gbl.byte_1D5BA == 0x79 &&
					gbl.byte_1D5B4 != 0x50 &&
					seg041.time01() >= var_5F )
				{
					ovr028.sub_6E05D();

					var_5F = var_5B + 50;
				}

			}while( var_63 == 0 );

			gbl.area_ptr.field_3FE = 0;

			if( gbl.game_state == 3 &&
				gbl.byte_1D5BA == 0x79 &&
				gbl.byte_1D5B4 != 0x50 )
			{
				ovr028.sub_6E05D();
			}

            gbl.byte_1D5BF = arg_0;

            return var_60;
		}


		internal static void redraw_screen( )
		{
			seg041.sub_1071A( 0x28, 0, 0x18, 0 );

            Display.Update();
		}


		internal static T sub_6C804<T>( T arg_2, short arg_6 )where T: class,IListBase
		{
			short var_E;
            T var_C;
            T var_8;
            T var_4;

			var_E = 0;

			var_8 = arg_2;
			var_C = null;

			while( var_8 != null && var_E < arg_6 )
			{
				if( var_8.Field29() != 0 )
				{
					var_C = var_8;
				}

				var_8 = (T)var_8.Next();
				var_E++;
			}

			if( var_C == null &&
                arg_2.Field29() != 0)
			{
				var_4 = arg_2;
			}
			else
			{
				var_4 = var_C;
			}

			return var_4;
		}


        internal static void sub_6C897<T>(short arg_2,
            sbyte bp_arg_12, sbyte bp_arg_14, sbyte bp_arg_16, sbyte bp_arg_18, T bp_arg_E,
            byte bp_arg_1C, byte bp_arg_1E, byte bp_var_54) where T : class, IListBase
        {
            sbyte var_9;
            T var_8;
            T var_4;

            seg037.draw8x8_clear_area(bp_arg_12, bp_arg_14, bp_arg_16, bp_arg_18);
            var_4 = getStringListEntry(bp_arg_E, arg_2);
            var_8 = sub_6C804(bp_arg_E, arg_2);

            var_9 = bp_arg_16;

            while (var_4 != null && var_9 <= bp_arg_12)
            {
                if (var_4.Field29() != 0)
                {
                    seg041.displayString(var_4.String(), 0, bp_arg_1E, var_9, bp_arg_18);
                }
                else
                {
                    seg041.displayString(var_4.String(), 0, bp_arg_1C, var_9, bp_arg_18);
                }

                if (var_4.String().Length < bp_var_54)
                {
                    seg041.display_char01(true, ' ', (byte)(bp_var_54 - var_4.String().Length), 0, 0, var_9, (byte)(var_4.String().Length + bp_arg_18));
                }

                var_4 = (T)var_4.Next();
                var_9++;
            }
        }


		static byte getBegingOfString( string arg_2 )
		{
            return (byte)(arg_2.Length - arg_2.TrimStart(' ').Length);
		}

        static byte getEndOfString(string arg_2)
		{
            return (byte)arg_2.TrimEnd(' ').Length;	
		}


        static void ListItemHighlighted<T>(short arg_2, T bp_arg_10, sbyte bp_arg_16, sbyte bp_arg_18, byte bp_arg_1A) where T : class, IListBase
		{
			byte var_6;
			byte var_5;
			T var_4;

			var_4 = getStringListEntry( bp_arg_10, arg_2 );

			var_5 = getBegingOfString( var_4.String() );

			var_6 = (byte)(( getEndOfString( var_4.String() ) - var_5 ) /*+ 1*/);

			seg041.displayString( 
				seg051.Copy( var_6, var_5, var_4.String() ),
				bp_arg_1A, 
				0, 
				bp_arg_16 + ( arg_2 - gbl.word_1D5BC ),
				bp_arg_18 + var_5 );
		}


        static void ListItemNormal<T>(short arg_2, T bp_arg_10, sbyte bp_arg_16, sbyte bp_arg_18, byte bp_arg_1C, byte bp_arg_1E) where T : class, IListBase
        {
            string var_106;
            byte var_6;
            byte var_5;
            T var_4;

            var_4 = getStringListEntry(bp_arg_10, arg_2);

            var_5 = getBegingOfString(var_4.String());

            var_6 = (byte)((getEndOfString(var_4.String()) - var_5) /*+ 1*/);

            seg051.Copy(var_6, var_5, var_4.String(), out var_106);
            if (var_4.Field29() != 0)
            {
                seg041.displayString(var_106, 0, bp_arg_1E, bp_arg_16 + (arg_2 - gbl.word_1D5BC), bp_arg_18 + var_5);
            }
            else
            {
                seg041.displayString(var_106, 0, bp_arg_1C, bp_arg_16 + (arg_2 - gbl.word_1D5BC), bp_arg_18 + var_5);
            }
        }


        internal static void sub_6CC08<T>( bool arg_2, ref short bp_arg_4, T bp_arg_E, short bp_var_56, short bp_var_59) where T: class,IListBase
        {
            short var_2;

			var_2 = 0;

            if (arg_2 == true)
            {
                while (var_2 < bp_var_59 &&
                    getStringListEntry(bp_arg_E, bp_arg_4).Field29() != 0 )
                {
                    var_2++;
                    bp_arg_4 += 1;

                    if ((gbl.word_1D5BC + bp_var_59 - 1) < bp_arg_4)
                    {
                        bp_arg_4 = gbl.word_1D5BC;
                    }

                    if ((bp_var_56 - 1) < bp_arg_4)
                    {
                        bp_arg_4 = gbl.word_1D5BC;
                    }
                }
            }
            else
            {
                while (var_2 < bp_var_59 &&
                    getStringListEntry(bp_arg_E, bp_arg_4).Field29() != 0)
                {
                    var_2++;
                    bp_arg_4 -= 1;

                    if (bp_arg_4 < gbl.word_1D5BC)
                    {
                        bp_arg_4 = (short)(gbl.word_1D5BC + bp_var_59 - 1);
                    }

                    if ((bp_var_56 - 1) < bp_arg_4)
                    {
                        bp_arg_4 = (short)(bp_var_56 - 1);
                    }
                }
            }
        }


        internal static void sub_6CD38<T>(bool arg_2, ref short bp_arg_4, T bp_arg_E, short bp_var_56,
            short bp_var_59,
            sbyte bp_arg_12, sbyte bp_arg_14, sbyte bp_arg_16, sbyte bp_arg_18,
            byte bp_arg_1C, byte bp_arg_1E, byte bp_var_54) where T : class,IListBase
        {
            short var_2;

            var_2 = (short)(bp_arg_4 - gbl.word_1D5BC);

            if (arg_2 == true)
            {
                gbl.word_1D5BC += bp_var_59;
                if ((bp_var_56 - bp_var_59) < gbl.word_1D5BC)
                {
                    gbl.word_1D5BC = (short)(bp_var_56 - bp_var_59);
                }
            }
            else
            {
                gbl.word_1D5BC -= bp_var_59;

                if (gbl.word_1D5BC < 0)
                {
                    gbl.word_1D5BC = 0;
                }
            }

            bp_arg_4 = (short)(gbl.word_1D5BC + var_2);

            sub_6CC08(arg_2, ref bp_arg_4, bp_arg_E, bp_var_56, bp_var_59);

            sub_6C897(gbl.word_1D5BC, bp_arg_12, bp_arg_14, bp_arg_16, bp_arg_18,
                bp_arg_E, bp_arg_1C, bp_arg_1E, bp_var_54);
        }


        internal static void sub_6CDCA<T>(bool arg_2, ref short index, T list, short bp_var_56, short bp_var_59 ) where T : class, IListBase
        {
			if( arg_2 == true )
			{
                index += 1;

                if ((gbl.word_1D5BC + bp_var_59 - 1) < index)
                {
                    index = gbl.word_1D5BC;
                }

                if ((bp_var_56 - 1) < index)
                {
                    index = gbl.word_1D5BC;
                }
			}
			else
			{
                index -= 1;

                if (index < gbl.word_1D5BC)
                {
                    index = (short)(gbl.word_1D5BC + bp_var_59 - 1);
                }

                if ((bp_var_56 - 1) < index)
                {
                    index = (short)(bp_var_56 - 1);
                }
			}

            sub_6CC08( arg_2, ref index, list, bp_var_56, bp_var_59 );
        }


        internal static char sl_select_item<T>(out T result_ptr, ref short index_ptr,
            ref bool arg_8, bool show_exit, T stringList, sbyte arg_12, sbyte arg_14,
            sbyte arg_16, sbyte arg_18, byte arg_1A, byte arg_1C, byte arg_1E,
            string string01, string string02) where T : class, IListBase
        {
            short var_8D;
            short stringList_size;
            T tmpStringList;
            bool var_85;
            bool var_84;
            string var_83;
            char var_5A;
            short var_59;
            bool var_57;
            short var_56;
            byte var_54;
            string var_53;
            string var_2A;
            char ret_val = '\0'; /* Simeon */

            result_ptr = null; /*Simeon*/

            var_2A = string02;
            var_53 = string01;

            if (stringList == null)
            {
                index_ptr = 0;
                result_ptr = null;

                ret_val = '\0';
            }
            else
            {
                gbl.byte_1D5BE = 1;

                var_54 = (byte)((arg_14 - arg_18) + 1);
                var_56 = StringListCount(stringList);
                var_59 = (short)((arg_12 - arg_16) + 1);

                tmpStringList = stringList;
                stringList_size = 0;

                while (tmpStringList != null && tmpStringList.Field29() != 0)
                {
                    tmpStringList = (T)tmpStringList.Next();
                    stringList_size++;
                }

                tmpStringList = stringList;
                var_8D = 0;

                while (tmpStringList != null)
                {
                    tmpStringList = (T)tmpStringList.Next();
                    var_8D++;
                }

                if (var_8D <= var_59)
                {
                    gbl.word_1D5BC = 0;
                }

                if (gbl.word_1D5BC > index_ptr)
                {
                    gbl.word_1D5BC = index_ptr;
                    arg_8 = true;
                }

                if (gbl.word_1D5BC > var_8D)
                {
                    gbl.word_1D5BC = 0;
                    arg_8 = true;
                }

                index_ptr++;
                sub_6CDCA(false, ref index_ptr, stringList, var_56, var_59);

                if (arg_8 == true)
                {
                    sub_6C897(gbl.word_1D5BC, arg_12, arg_14, arg_16, arg_18,
                        stringList, arg_1C, arg_1E, var_54);
                }

                arg_8 = false;

                bool loop_end = false;

                while (loop_end == false)
                {
                    ListItemHighlighted(index_ptr, stringList, arg_16, arg_18, arg_1A);
                    var_83 = var_53;

                    var_84 = false;
                    var_85 = false;

                    if ((var_56 - var_59) > gbl.word_1D5BC)
                    {
                        var_83 += " Next";
                        var_84 = true;
                    }

                    if (gbl.word_1D5BC > stringList_size)
                    {
                        var_83 += " Prev";
                        var_85 = true;
                    }

                    if (show_exit == true)
                    {
                        var_83 += " Exit";
                    }

                    var_5A = displayInput(out var_57, 0, 1, arg_1A, arg_1C, arg_1E, var_83, var_2A);

                    ListItemNormal(index_ptr, stringList, arg_16, arg_18, arg_1C, arg_1E);

                    if (var_57 == true)
                    {
                        switch (var_5A)
                        {
                            case 'G':
                                sub_6CDCA(false, ref index_ptr, stringList, var_56, var_59);
                                break;

                            case 'O':
                                sub_6CDCA(true, ref index_ptr, stringList, var_56, var_59);
                                break;

                            case 'I':
                                if (var_85 == true)
                                {
                                    sub_6CD38(false, ref index_ptr, stringList, var_56, var_59, arg_12, arg_14, arg_16, arg_18, arg_1C, arg_1E, var_54);
                                }
                                break;

                            case 'Q':
                                if (var_84 == true)
                                {
                                    sub_6CD38(true, ref index_ptr, stringList, var_56, var_59, arg_12, arg_14, arg_16, arg_18, arg_1C, arg_1E, var_54);
                                }
                                break;
                        }
                    }
                    else
                    {
                        switch (var_5A)
                        {
                            case 'P':
                                sub_6CD38(false, ref index_ptr, stringList, var_56, var_59, arg_12, arg_14, arg_16, arg_18, arg_1C, arg_1E, var_54);
                                break;

                            case 'N':

                                sub_6CD38(true, ref index_ptr, stringList, var_56, var_59, arg_12, arg_14, arg_16, arg_18, arg_1C, arg_1E, var_54);
                                break;

                            case (char)0x1B:
                                goto case 'E';

                            case '\0':
                                goto case 'E';

                            case 'E':
                                result_ptr = null;
                                ret_val = '\0';
                                loop_end = true;
                                break;

                            default:
                                result_ptr = getStringListEntry(stringList, index_ptr);
                                ret_val = var_5A;
                                loop_end = true;
                                break;
                        }
                    }
                }
            }

            return ret_val;
        }


        internal static char yes_no( byte arg_0, byte arg_2, byte arg_4, string inputString )
        {
            char inputKey;
            bool var_2B;

            gbl.byte_1D5BE = 2;

			do
			{
				inputKey = displayInput( out var_2B, 0, 0, arg_0, arg_2, arg_4, "Yes No", inputString );

			} while( yesNoFlags.MemberOf( inputKey ) == false );

            return inputKey;
        }


        internal static void alloc_stringList( out StringList sl, int numEntries )
        {
            StringList sl_ptr;

            sl = new StringList();
			
			sl_ptr = sl;

			for( int i = 1; i < numEntries; i++ )
			{
				sl_ptr.next = new StringList();

                sl_ptr = sl_ptr.next;
			}
        }


        internal static void free_stringList( ref StringList arg_0 )
        {
            object tmp_thing;
            StringList thing;

			thing = arg_0;

			while( thing != null )
			{
				tmp_thing = thing;

				thing = thing.next;

				seg051.FreeMem( 0x2E, tmp_thing );
			}
        }
	}
}
