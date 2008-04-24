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


        internal static void sub_6C1E9(byte arg_2, int displayInputStringLen, byte highlightFgColor, string displayInputString, int xOffset, byte fgColor, byte[] bp_var_8D)
        {
            if (displayInputStringLen > 0)
            {
                for (int i = 0; i < displayInputStringLen; i++)
                {
                    if (bp_var_8D[arg_2 << 1] <= i &&
                        bp_var_8D[(arg_2 << 1) + 1] >= i &&
                        highlightFgColor != 0)
                    {
                        seg041.display_char01(true, displayInputString[i], 1, highlightFgColor, 0, 0x18, xOffset + i);
                    }
                    else
                    {
                        if (unk_6C0BA.MemberOf(displayInputString[i]) == true)
                        {
                            seg041.display_char01(true, displayInputString[i], 1, 0, highlightFgColor, 0x18, xOffset + i);
                        }
                        else
                        {
                            seg041.display_char01(true, displayInputString[i], 1, 0, fgColor, 0x18, xOffset + i);
                        }
                    }
                }

                if (displayInputStringLen + xOffset < 0x27)
                {
                    seg041.display_char01(true, ' ', (0x27 - displayInputStringLen - xOffset) + 1,
                        0, 0, 0x18, xOffset + displayInputStringLen);
                }

                Display.Update();
            }
        }

        static Set unk_6C398 = new Set(0x0408, new byte[] { 1, 0, 0xFF, 3, 0xFE, 0xFF, 0xFF, 7 });
        static Set unk_6C3B8 = new Set(0x0606, new byte[] { 0xFE, 3, 0, 0, 0, 0x10 });

        static byte[] unk_18AE0 = { 0x4F, 0x50, 0x51, 0x4B, 0x20, 0x4D, 0x47, 0x48, 0x49 };


        internal static char displayInput(out bool arg_0, bool useOverlay, byte arg_6, byte highlightFgColor, byte fgColor, byte extraStringFgColor, string displayInputString, string displayExtraString)
        {
            byte var_8E;
            byte[] var_8D = new byte[0x28];
            byte var_63;
            byte var_62;
            byte var_61;
            char var_60;
            int var_5F;
            int var_5B;

            gbl.displayInput_specialKeyPressed = false;

            bool var_8F = (fgColor != 0) || (highlightFgColor != 0);

            buildInputKeys(displayInputString, var_8D, out var_8E);

            if (gbl.byte_1D5BE >= var_8E)
            {
                gbl.byte_1D5BE = 0;
            }

            int displayInputStringLen = displayInputString.Length;
            var_60 = '\0';
            arg_0 = false;
            var_63 = 0;

            int timeStart = seg041.time01();
            var_5B = seg041.time01() + 30;

            var_5F = var_5B + 50;

            if (displayExtraString.Length != 0)
            {
                seg041.displayString(displayExtraString, 0, extraStringFgColor, 0x18, 0);
            }

            int displayInputXOffset = displayExtraString.Length;

            sub_6C1E9(gbl.byte_1D5BE, displayInputStringLen, highlightFgColor, displayInputString, displayInputXOffset, fgColor, var_8D);

            if (gbl.game_state == 3 &&
                gbl.byte_1D5BA == 0x79 &&
                gbl.byte_1D5B4 != 0x50)
            {
                ovr028.sub_6E005();
                ovr028.sub_6E02E();
                ovr028.sub_6E05D();
            }

            do
            {
                if (gbl.game_state == 3 &&
                    gbl.byte_1D5BA == 0x79 &&
                    gbl.byte_1D5B4 != 0x50 &&
                    seg041.time01() >= var_5B)
                {
                    ovr028.sub_6E02E();
                    var_5B = var_5F + 30;
                }

                if ((gbl.area_ptr.field_3FE != 0 || useOverlay == true) &&
                    gbl.byte_1D556.curFrame > 0)
                {
                    ovr030.sub_7000A(gbl.byte_1D556.ptrs[gbl.byte_1D556.curFrame].field_4, useOverlay, 3, 3);

                    if ((seg041.time01() - timeStart) >= (gbl.byte_1D556.ptrs[gbl.byte_1D556.curFrame].field_0 * 10) ||
                        gbl.area_ptr.field_3FE != 0)
                    {
                        gbl.byte_1D556.curFrame++;

                        if (gbl.byte_1D556.curFrame > gbl.byte_1D556.numFrames)
                        {
                            gbl.byte_1D556.curFrame = 1;
                        }

                        timeStart = seg041.time01();
                    }
                }

                if (gbl.displayInputCentiSecondWait > 0 &&
                    (seg041.time01() - timeStart) >= gbl.displayInputCentiSecondWait)
                {
                    var_60 = gbl.displayInputTimeoutValue;
                    var_63 = 1;
                }
                else if (seg049.KEYPRESSED() == true)
                {
                    var_60 = (char)seg043.GetInputKey();

                    if (var_60 == 0)
                    {
                        var_60 = (char)seg043.GetInputKey();

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
                        if (var_8F)
                        {
                            if (var_8D[(gbl.byte_1D5BE) << 1] != 0xff)
                            {
                                var_60 = displayInputString[var_8D[(gbl.byte_1D5BE) << 1]];
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

                        sub_6C1E9(gbl.byte_1D5BE, displayInputStringLen, highlightFgColor, displayInputString, displayInputXOffset, fgColor, var_8D);
                    }
                    else if (var_60 == 0x2E)
                    {
                        gbl.byte_1D5BE++;

                        if (gbl.byte_1D5BE >= var_8E)
                        {
                            gbl.byte_1D5BE = 0;
                        }

                        sub_6C1E9(gbl.byte_1D5BE, displayInputStringLen, highlightFgColor, displayInputString, displayInputXOffset, fgColor, var_8D);
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
                                for (var_62 = 0; var_62 < displayInputStringLen; var_62++)
                                {
                                    if (displayInputString[var_62] == var_60)
                                    {
                                        var_63 = 1;
                                        var_61 = 0;

                                        while (var_8D[var_61 << 1] != var_62)
                                        {
                                            var_61++;
                                        }

                                        gbl.byte_1D5BE = var_61;

                                        sub_6C1E9(gbl.byte_1D5BE, displayInputStringLen, highlightFgColor, displayInputString, displayInputXOffset, fgColor, var_8D);
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

                if (gbl.game_state == 3 &&
                    gbl.byte_1D5BA == 0x79 &&
                    gbl.byte_1D5B4 != 0x50 &&
                    seg041.time01() >= var_5F)
                {
                    ovr028.sub_6E05D();

                    var_5F = var_5B + 50;
                }

                System.Threading.Thread.Sleep(20);

            } while (var_63 == 0);

            gbl.area_ptr.field_3FE = 0;

            if (gbl.game_state == 3 &&
                gbl.byte_1D5BA == 0x79 &&
                gbl.byte_1D5B4 != 0x50)
            {
                ovr028.sub_6E05D();
            }

            gbl.displayInput_specialKeyPressed = arg_0;

            return var_60;
        }


		internal static void redraw_screen( )
		{
			seg041.displaySpaceChar( 0x28, 0, 0x18, 0 );

            Display.Update();
		}


		internal static T sub_6C804<T>( T arg_2, int index )where T: class,IListBase
		{
            T var_C;
            T var_8;
            T var_4;

			int count = 0;

			var_8 = arg_2;
			var_C = null;

			while( var_8 != null && count < index )
			{
				if( var_8.Field29() != 0 )
				{
					var_C = var_8;
				}

				var_8 = (T)var_8.Next();
				count++;
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
            int yEnd, int xEnd, int yStart, int xStart, T bp_arg_E,
            byte bp_arg_1C, byte bp_arg_1E, int displayFillWidth) where T : class, IListBase
        {
            seg037.draw8x8_clear_area(yEnd, xEnd, yStart, xStart);

            T var_4 = getStringListEntry(bp_arg_E, arg_2);
            T var_8 = sub_6C804(bp_arg_E, arg_2);

            int yCol = yStart;

            while (var_4 != null && yCol <= yEnd)
            {
                if (var_4.Field29() != 0)
                {
                    seg041.displayString(var_4.String(), 0, bp_arg_1E, yCol, xStart);
                }
                else
                {
                    seg041.displayString(var_4.String(), 0, bp_arg_1C, yCol, xStart);
                }

                if (var_4.String().Length < displayFillWidth)
                {
                    seg041.display_char01(true, ' ', displayFillWidth - var_4.String().Length, 0, 0, 
                        yCol, var_4.String().Length + xStart);
                }

                var_4 = (T)var_4.Next();
                yCol++;
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


        static void ListItemHighlighted<T>(short index, T stringList, int listDisplayStartY, int listDisplayStartX,
            byte bgColor) where T : class, IListBase
		{
			T var_4 = getStringListEntry( stringList, index );

			int stringStart = getBegingOfString( var_4.String() );
			int stringEnd = getEndOfString( var_4.String() ) - stringStart ;

			seg041.displayString( 
				seg051.Copy( stringEnd, stringStart, var_4.String() ),
				bgColor, 
				0, 
				listDisplayStartY + ( index - gbl.word_1D5BC ),
				listDisplayStartX + stringStart );
		}


        static void ListItemNormal<T>(short arg_2, T bp_arg_10, int startY, int startX, byte bp_arg_1C, byte bp_arg_1E) where T : class, IListBase
        {
            string var_106;
            byte var_5;
            T var_4;

            var_4 = getStringListEntry(bp_arg_10, arg_2);

            var_5 = getBegingOfString(var_4.String());

            int copyLen = (byte)((getEndOfString(var_4.String()) - var_5));

            seg051.Copy(copyLen, var_5, var_4.String(), out var_106);
            if (var_4.Field29() != 0)
            {
                seg041.displayString(var_106, 0, bp_arg_1E, startY + (arg_2 - gbl.word_1D5BC), startX + var_5);
            }
            else
            {
                seg041.displayString(var_106, 0, bp_arg_1C, startY + (arg_2 - gbl.word_1D5BC), startX + var_5);
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
            int yEnd, int xEnd, int yStart, int xStart,
            byte bp_arg_1C, byte bp_arg_1E, int displayFillWidth) where T : class,IListBase
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

            sub_6C897(gbl.word_1D5BC, yEnd, xEnd, yStart, xStart,
                bp_arg_E, bp_arg_1C, bp_arg_1E, displayFillWidth);
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
            ref bool arg_8, bool showExit, T stringList, 
            int endY, int endX, int startY, int startX, 
            byte highlightBgColor, byte arg_1C, byte arg_1E,
            string inputString, string extraTextString) where T : class, IListBase
        {
            short var_8D;
            short stringList_size;
            T tmpStringList;
            bool showPrevious;
            bool showNext;
            char var_5A;
            short var_59;
            bool var_57;
            short var_56;

            char ret_val = '\0'; /* Simeon */
            result_ptr = null; /*Simeon*/

            if (stringList == null)
            {
                index_ptr = 0;
                result_ptr = null;

                return '\0';
            }
            
            gbl.byte_1D5BE = 1;

            int listDisplayWidth = (byte)((endX - startX) + 1);
            var_56 = StringListCount(stringList);
            var_59 = (short)((endY - startY) + 1);

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
                sub_6C897(gbl.word_1D5BC, endY, endX, startY, startX,
                    stringList, arg_1C, arg_1E, listDisplayWidth);
            }

            arg_8 = false;

            bool loop_end = false;

            while (loop_end == false)
            {
                ListItemHighlighted(index_ptr, stringList, startY, startX, highlightBgColor);
                string displayString = inputString;

                showNext = false;
                showPrevious = false;

                if ((var_56 - var_59) > gbl.word_1D5BC)
                {
                    displayString += " Next";
                    showNext = true;
                }

                if (gbl.word_1D5BC > stringList_size)
                {
                    displayString += " Prev";
                    showPrevious = true;
                }

                if (showExit == true)
                {
                    displayString += " Exit";
                }

                var_5A = displayInput(out var_57, false, 1, highlightBgColor, arg_1C, arg_1E, displayString, extraTextString);

                ListItemNormal(index_ptr, stringList, startY, startX, arg_1C, arg_1E);

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
                            if (showPrevious == true)
                            {
                                sub_6CD38(false, ref index_ptr, stringList, var_56, var_59, endY, endX, startY, startX, arg_1C, arg_1E, listDisplayWidth);
                            }
                            break;

                        case 'Q':
                            if (showNext == true)
                            {
                                sub_6CD38(true, ref index_ptr, stringList, var_56, var_59, endY, endX, startY, startX, arg_1C, arg_1E, listDisplayWidth);
                            }
                            break;
                    }
                }
                else
                {
                    switch (var_5A)
                    {
                        case 'P':
                            sub_6CD38(false, ref index_ptr, stringList, var_56, var_59, endY, endX, startY, startX, arg_1C, arg_1E, listDisplayWidth);
                            break;

                        case 'N':

                            sub_6CD38(true, ref index_ptr, stringList, var_56, var_59, endY, endX, startY, startX, arg_1C, arg_1E, listDisplayWidth);
                            break;

                        case (char)0x1B:
                        case '\0':
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

            return ret_val;
        }


        internal static char yes_no( byte arg_0, byte arg_2, byte arg_4, string inputString )
        {
            char inputKey;
            bool var_2B;

            gbl.byte_1D5BE = 2;

			do
			{
				inputKey = displayInput( out var_2B, false, 0, arg_0, arg_2, arg_4, "Yes No", inputString );

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
