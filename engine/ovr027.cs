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
            int loop_var = 0;

            T current = list;

            while( current != null && loop_var != index )
            {
                current = (T)current.Next();
                loop_var++;
            }

            if( loop_var == index )
            {
                return current;
            }
            else
            {
                return null;
            }
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

        static Set highlightable_text = new Set(0x0606, new byte[] { 0xFF, 3, 0xFE, 0xFF, 0xFF, 7 });

        internal class highlight
        {
            internal int start = -1;
            internal int end = -1;

            public override string ToString()
            {
                return string.Format("{0} - {1}", start, end);
            }
        }

        internal class HighlightSet
        {
            highlight[] highlights;
            const int length = 20;

            internal HighlightSet()
            {
                highlights = new highlight[length];

                Clear();
            }


            internal highlight this[int index]
            {
                get { return highlights[index]; }
                set { highlights[index] = value; }
            }

            internal void Clear()
            {
                for(int i = 0; i < length; i++)
                {
                    highlights[i] = new highlight();
                }
            }
        }

        /// <summary>
        /// sub_6C0DA
        /// </summary>
        internal static HighlightSet buildInputKeys(string arg_2, out int bp_var_8E)
        {
            HighlightSet bp_var_8D = new HighlightSet();

            int var_2A = 0;

            for (int var_2B = 0; var_2B < arg_2.Length; var_2B++)
            {
                if (highlightable_text.MemberOf(arg_2[var_2B ]) == true)
                {
                    if (bp_var_8D[var_2A].start == -1)
                    {
                        bp_var_8D[var_2A].start = var_2B;
                    }
                    else
                    {
                        bp_var_8D[var_2A].end = var_2B - 2;
                        var_2A++;
                        bp_var_8D[var_2A].start = var_2B;
                    }
                }
            }

            bp_var_8D[var_2A].end = arg_2.Length;
            bp_var_8E = var_2A;

            return bp_var_8D;
        }


        internal static void display_highlighed_text(int highlighed_word, byte highlightFgColor, 
            string text, int xOffset, byte fgColor, HighlightSet highlights) /* sub_6C1E9 */
        {
            if (text.Length > 0)
            {
                for (int i = 0; i < text.Length; i++)
                {
                    if (highlights[highlighed_word].start <= i &&
                        highlights[highlighed_word].end >= i &&
                        highlightFgColor != 0)
                    {
                        seg041.display_char01(true, text[i], 1, highlightFgColor, 0, 0x18, xOffset + i);
                    }
                    else
                    {
                        if (highlightable_text.MemberOf(text[i]) == true)
                        {
                            seg041.display_char01(true, text[i], 1, 0, highlightFgColor, 0x18, xOffset + i);
                        }
                        else
                        {
                            seg041.display_char01(true, text[i], 1, 0, fgColor, 0x18, xOffset + i);
                        }
                    }
                }

                if (text.Length + xOffset < 0x27)
                {
                    seg041.display_char01(true, ' ', (0x27 - text.Length - xOffset) + 1,
                        0, 0, 0x18, xOffset + text.Length);
                }

                Display.Update();
            }
        }

        static Set unk_6C398 = new Set(0x0408, new byte[] { 1, 0, 0xFF, 3, 0xFE, 0xFF, 0xFF, 7 });
        static Set unk_6C3B8 = new Set(0x0606, new byte[] { 0xFE, 3, 0, 0, 0, 0x10 });

        static byte[] unk_18AE0 = { 0x4F, 0x50, 0x51, 0x4B, 0x20, 0x4D, 0x47, 0x48, 0x49 };


        internal static char displayInput(out bool specialKeyPressed, bool useOverlay, byte arg_6, byte highlightFgColor, byte fgColor, byte extraStringFgColor, string displayInputString, string displayExtraString)
        {
            int var_8E;
            byte var_61;

            gbl.displayInput_specialKeyPressed = false;

            bool var_8F = (fgColor != 0) || (highlightFgColor != 0);

            HighlightSet highlights = buildInputKeys(displayInputString, out var_8E);

            if (gbl.byte_1D5BE >= var_8E)
            {
                gbl.byte_1D5BE = 0;
            }

            char input_key = '\0';
            specialKeyPressed = false;
            bool var_63 = false;

            int timeStart = seg041.time01();
            int var_5B = seg041.time01() + 30;

            int var_5F = var_5B + 50;

            if (displayExtraString.Length != 0)
            {
                seg041.displayString(displayExtraString, 0, extraStringFgColor, 0x18, 0);
            }

            int displayInputXOffset = displayExtraString.Length;

            display_highlighed_text(gbl.byte_1D5BE, highlightFgColor, 
                displayInputString, displayInputXOffset, fgColor, highlights);

            if (gbl.game_state == 3 &&
                gbl.bigpic_block_id == 0x79 &&
                gbl.lastDaxBlockId != 0x50)
            {
                ovr028.sub_6E005();
                ovr028.map_cursor_draw();
                ovr028.map_cursor_restore();
            }

            do
            {
                if (gbl.game_state == 3 &&
                    gbl.bigpic_block_id == 0x79 &&
                    gbl.lastDaxBlockId != 0x50 &&
                    seg041.time01() >= var_5B)
                {
                    ovr028.map_cursor_draw();
                    var_5B = var_5F + 30;
                }

                if ((gbl.area_ptr.picture_fade != 0 || useOverlay == true) &&
                    gbl.byte_1D556.curFrame > 0)
                {
                    ovr030.sub_7000A(gbl.byte_1D556.frames[gbl.byte_1D556.curFrame-1].picture, useOverlay, 3, 3);

                    int delay = gbl.byte_1D556.frames[gbl.byte_1D556.curFrame - 1].delay * 10;

					if ((seg041.time01() - timeStart) >= delay ||
                        gbl.area_ptr.picture_fade != 0)
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
                    input_key = gbl.displayInputTimeoutValue;
                    var_63 = true;
                }
                else if (seg049.KEYPRESSED() == true)
                {
                    input_key = (char)seg043.GetInputKey();

                    if (input_key == 0)
                    {
                        input_key = (char)seg043.GetInputKey();

                        if (arg_6 != 0)
                        {
                            specialKeyPressed = true;
                            var_63 = true;
                        }
                    }
                    else if (input_key == 0x1B)
                    {
                        var_63 = true;
                        input_key = '\0';
                    }
                    else if (input_key == 13)
                    {
                        if (var_8F)
                        {
                            if (highlights[gbl.byte_1D5BE].start != -1)
                            {
                                input_key = displayInputString[highlights[gbl.byte_1D5BE].start];
                            }
                            else
                            {
                                input_key = '\r';
                            }

                            var_63 = true;
                        }
                    }
                    else if (input_key == 0x2C)
                    {
                        if (gbl.byte_1D5BE == 0)
                        {
                            gbl.byte_1D5BE = (byte)(var_8E - 1);
                        }
                        else
                        {
                            gbl.byte_1D5BE--;
                        }

                        display_highlighed_text(gbl.byte_1D5BE, highlightFgColor, displayInputString, displayInputXOffset, fgColor, highlights);
                    }
                    else if (input_key == 0x2E)
                    {
                        gbl.byte_1D5BE++;

                        if (gbl.byte_1D5BE >= var_8E)
                        {
                            gbl.byte_1D5BE = 0;
                        }

                        display_highlighed_text(gbl.byte_1D5BE, highlightFgColor, displayInputString, displayInputXOffset, fgColor, highlights);
                    }
                    else
                    {
                        input_key = seg051.UpCase(input_key);
                        if (unk_6C398.MemberOf(input_key) == true)
                        {
                            if (input_key == 0x20)
                            {
                                var_63 = true;
                            }
                            else
                            {
                                for (int var_62 = 0; var_62 < displayInputString.Length; var_62++)
                                {
                                    if (displayInputString[var_62] == input_key)
                                    {
                                        var_63 = true;
                                        var_61 = 0;

                                        while (highlights[var_61].start != var_62)
                                        {
                                            var_61++;
                                        }

                                        gbl.byte_1D5BE = var_61;

                                        display_highlighed_text(gbl.byte_1D5BE, highlightFgColor, displayInputString, displayInputXOffset, fgColor, highlights);
                                    }
                                }
                            }
                        }

                        if (arg_6 != 0 &&
                            unk_6C3B8.MemberOf(input_key) == true)
                        {
                            if (input_key == 0x5C)
                            {
                                input_key = '7';
                            }
                            else
                            {
                                input_key = (char)unk_18AE0[input_key - 0x31];
                            }

                            specialKeyPressed = true;
                            var_63 = true;
                        }
                    }
                }

                if (gbl.game_state == 3 &&
                    gbl.bigpic_block_id == 0x79 &&
                    gbl.lastDaxBlockId != 0x50 &&
                    seg041.time01() >= var_5F)
                {
                    ovr028.map_cursor_restore();

                    var_5F = var_5B + 50;
                }

                System.Threading.Thread.Sleep(20);

            } while (var_63 == false);

            gbl.area_ptr.picture_fade = 0;

            if (gbl.game_state == 3 &&
                gbl.bigpic_block_id == 0x79 &&
                gbl.lastDaxBlockId != 0x50)
            {
                ovr028.map_cursor_restore();
            }

            gbl.displayInput_specialKeyPressed = specialKeyPressed;

            return input_key;
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
            byte var_5;
            T var_4;

            var_4 = getStringListEntry(bp_arg_10, arg_2);

            var_5 = getBegingOfString(var_4.String());

            int copyLen = (byte)((getEndOfString(var_4.String()) - var_5));

            string text = seg051.Copy(copyLen, var_5, var_4.String());
            if (var_4.Field29() != 0)
            {
                seg041.displayString(text, 0, bp_arg_1E, startY + (arg_2 - gbl.word_1D5BC), startX + var_5);
            }
            else
            {
                seg041.displayString(text, 0, bp_arg_1C, startY + (arg_2 - gbl.word_1D5BC), startX + var_5);
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
            short var_59;
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

                bool speical_key;
                char input_key = displayInput(out speical_key, false, 1, highlightBgColor, arg_1C, arg_1E, displayString, extraTextString);

                ListItemNormal(index_ptr, stringList, startY, startX, arg_1C, arg_1E);

                if (speical_key == true)
                {
                    switch (input_key)
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
                    switch (input_key)
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
                            ret_val = input_key;
                            loop_end = true;
                            break;
                    }
                }
            }

            return ret_val;
        }


        internal static char yes_no(byte highlightFgColor, byte fgColor, byte extraStringFgColor, string inputString)
        {
            char inputKey;
            bool dummyBool;

            gbl.byte_1D5BE = 2;

            do
            {
                inputKey = displayInput(out dummyBool, false, 0, highlightFgColor, fgColor, extraStringFgColor, "Yes No", inputString);
            
            } while (yesNoFlags.MemberOf(inputKey) == false);

            return inputKey;
        }


        internal static StringList alloc_stringList(int numEntries)
        {
            StringList sl = new StringList();
            StringList sl_ptr = sl;

			for( int i = 1; i < numEntries; i++ )
			{
				sl_ptr.next = new StringList();

                sl_ptr = sl_ptr.next;
			}

            return sl;
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
