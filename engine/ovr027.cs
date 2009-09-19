using Classes;
using System.Collections.Generic;

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

        internal static MenuItem getStringListEntry(List<MenuItem> list, int index)
        {
            return (list.Count > index) ? list[index] : null;
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
        internal static HighlightSet BuildInputKeys(string menuText, out int highlighCount)
        {
            HighlightSet highlighSet = new HighlightSet();

            int index = 0;

            for (int var_2B = 0; var_2B < menuText.Length; var_2B++)
            {
                if (highlightable_text.MemberOf(menuText[var_2B ]) == true)
                {
                    if (highlighSet[index].start == -1)
                    {
                        highlighSet[index].start = var_2B;
                    }
                    else
                    {
                        highlighSet[index].end = var_2B - 2;
                        index++;
                        highlighSet[index].start = var_2B;
                    }
                }
            }

            highlighSet[index].end = menuText.Length;
            highlighCount = index + 1;

            return highlighSet;
        }


        internal static void display_highlighed_text(int highlighed_word, int highlightFgColor, 
            string text, int xOffset, int fgColor, HighlightSet highlights) /* sub_6C1E9 */
        {
            if (text.Length > 0)
            {
                for (int i = 0; i < text.Length; i++)
                {
                    if (highlights[highlighed_word].start <= i &&
                        highlights[highlighed_word].end >= i &&
                        highlightFgColor != 0)
                    {
                        seg041.display_char01(text[i], 1, highlightFgColor, 0, 0x18, xOffset + i);
                    }
                    else if (highlightable_text.MemberOf(text[i]) == true)
                    {
                        seg041.display_char01(text[i], 1, 0, highlightFgColor, 0x18, xOffset + i);
                    }
                    else
                    {
                        seg041.display_char01(text[i], 1, 0, fgColor, 0x18, xOffset + i);
                    }
                }

                if (text.Length + xOffset < 0x27)
                {
                    seg041.display_char01(' ', (0x27 - text.Length - xOffset) + 1,
                        0, 0, 0x18, xOffset + text.Length);
                }

                Display.Update();
            }
        }

        static Set unk_6C398 = new Set(0x0408, new byte[] { 1, 0, 0xFF, 3, 0xFE, 0xFF, 0xFF, 7 });
        static Set unk_6C3B8 = new Set(0x0606, new byte[] { 0xFE, 3, 0, 0, 0, 0x10 });

        static byte[] unk_18AE0 = { 0x4F, 0x50, 0x51, 0x4B, 0x20, 0x4D, 0x47, 0x48, 0x49 };

        internal static char displayInput(bool useOverlay, byte arg_6, int highlightFgColor, int fgColor, int extraStringFgColor, string displayInputString, string displayExtraString)
        {
            bool dummyBool;
            return displayInput(out dummyBool, useOverlay, arg_6, highlightFgColor, fgColor, extraStringFgColor, displayInputString, displayExtraString);
        }

        internal static char displayInput(out bool specialKeyPressed, bool useOverlay, byte arg_6, int highlightFgColor, int fgColor, int extraStringFgColor, string displayInputString, string displayExtraString)
        {
            int highlistCount;
            byte var_61;

            gbl.displayInput_specialKeyPressed = false;

            bool var_8F = (fgColor != 0) || (highlightFgColor != 0);

            HighlightSet highlights = BuildInputKeys(displayInputString, out highlistCount);

            if (gbl.menuSelectedWord >= highlistCount)
            {
                gbl.menuSelectedWord = 0;
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

            display_highlighed_text(gbl.menuSelectedWord, highlightFgColor,
                displayInputString, displayInputXOffset, fgColor, highlights);

            if (gbl.game_state == GameState.WildernessMap &&
                gbl.bigpic_block_id == 0x79 &&
                gbl.lastDaxBlockId != 0x50)
            {
                ovr028.SetMapCursorPosition();
                ovr028.map_cursor_draw();
                ovr028.map_cursor_restore();
            }

            do
            {
                if (gbl.game_state == GameState.WildernessMap &&
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
                    ovr030.DrawMaybeOverlayed(gbl.byte_1D556.CurrentPicture(), useOverlay, 3, 3);

                    int delay = gbl.byte_1D556.CurrentDelay() * 10;

                    if ((seg041.time01() - timeStart) >= delay ||
                        gbl.area_ptr.picture_fade != 0)
                    {
                        gbl.byte_1D556.NextFrame();

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
                            if (highlights[gbl.menuSelectedWord].start != -1)
                            {
                                input_key = displayInputString[highlights[gbl.menuSelectedWord].start];
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
                        if (gbl.menuSelectedWord == 0)
                        {
                            gbl.menuSelectedWord = highlistCount - 1;
                        }
                        else
                        {
                            gbl.menuSelectedWord--;
                        }

                        display_highlighed_text(gbl.menuSelectedWord, highlightFgColor, displayInputString, displayInputXOffset, fgColor, highlights);
                    }
                    else if (input_key == 0x2E)
                    {
                        gbl.menuSelectedWord++;

                        if (gbl.menuSelectedWord >= highlistCount)
                        {
                            gbl.menuSelectedWord = 0;
                        }

                        display_highlighed_text(gbl.menuSelectedWord, highlightFgColor, displayInputString, displayInputXOffset, fgColor, highlights);
                    }
                    else
                    {
                        input_key = char.ToUpper(input_key);
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

                                        gbl.menuSelectedWord = var_61;

                                        display_highlighed_text(gbl.menuSelectedWord, highlightFgColor, displayInputString, displayInputXOffset, fgColor, highlights);
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

                if (gbl.game_state == GameState.WildernessMap &&
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

            if (gbl.game_state == GameState.WildernessMap &&
                gbl.bigpic_block_id == 0x79 &&
                gbl.lastDaxBlockId != 0x50)
            {
                ovr028.map_cursor_restore();
            }

            gbl.displayInput_specialKeyPressed = specialKeyPressed;

            return input_key;
        }


        internal static void ClearPromptArea() // redraw_screen
        {
            ClearPromptAreaNoUpdate();

            Display.Update();
        }

        internal static void ClearPromptAreaNoUpdate()
        {
            seg041.DrawRectangle(0, 0x18, 0x27, 0x18, 0);
        }

        internal static void sub_6C897(int index,
            int yEnd, int xEnd, int yStart, int xStart, List<MenuItem> list,
            int normalColor, int headingColor, int displayFillWidth) // sub_6C897
        {
            seg037.draw8x8_clear_area(yEnd, xEnd, yStart, xStart);

            MenuItem var_4 = getStringListEntry(list, index);

            int yCol = yStart;
            int count = System.Math.Min(yEnd - yStart + 1, list.Count - index);

            foreach (var menu in list.GetRange(index, count))
            {
                seg041.displayString(menu.Text, 0, menu.Heading ? headingColor : normalColor, yCol, xStart);

                if (menu.Text.Length < displayFillWidth)
                {
                    seg041.display_char01(' ', displayFillWidth - menu.Text.Length, 0, 0, yCol, menu.Text.Length + xStart);
                }
                yCol++;
            }
        }


		static int getBegingOfString( string text )
		{
            return text.Length - text.TrimStart(' ').Length;
		}

        static void ListItemHighlighted(int index, List<MenuItem> stringList, int yCol, int xCol, int bgColor) 
		{
			MenuItem menu_item = getStringListEntry( stringList, index );

            int stringStart = getBegingOfString(menu_item.Text);

			seg041.displayString(
                menu_item.Text.Trim(),
				bgColor, 
				0, 
				yCol + ( index - gbl.menuScreenIndex ),
				xCol + stringStart );
		}


        static void ListItemNormal(int index, List<MenuItem> list, int yCol, int xCol, int normalColor, int headingColor)
        {
            MenuItem menu_item = getStringListEntry(list, index);

            int var_5 = getBegingOfString(menu_item.Text);

            string text = menu_item.Text.Trim();

            if (menu_item.Heading)
            {
                seg041.displayString(text, 0, headingColor, yCol + (index - gbl.menuScreenIndex), xCol + var_5);
            }
            else
            {
                seg041.displayString(text, 0, normalColor, yCol + (index - gbl.menuScreenIndex), xCol + var_5);
            }
        }


        internal static int skipHeadings(bool backwardsStep, int index, List<MenuItem> list, int listDisplayHeight) // sub_6CC08
        {
            short var_2 = 0;

            if (backwardsStep == true)
            {
                while (var_2 < listDisplayHeight && list[index].Heading)
                {
                    var_2++;
                    index += 1;

                    if ((gbl.menuScreenIndex + listDisplayHeight - 1) < index)
                    {
                        index = gbl.menuScreenIndex;
                    }

                    if ((list.Count - 1) < index)
                    {
                        index = gbl.menuScreenIndex;
                    }
                }
            }
            else
            {
                while (var_2 < listDisplayHeight && list[index].Heading)
                {
                    var_2++;
                    index -= 1;

                    if (index < gbl.menuScreenIndex)
                    {
                        index = (short)(gbl.menuScreenIndex + listDisplayHeight - 1);
                    }

                    if ((list.Count - 1) < index)
                    {
                        index = (short)(list.Count - 1);
                    }
                }
            }

            return index;
        }


        internal static void menu_sub_6CD38(bool backwardsStep, ref int index, List<MenuItem> list, int listDisplayHeight,
            int yEnd, int xEnd, int yStart, int xStart,
            int normalColor, int headingColor, int displayFillWidth) // sub_6CD38
        {
            int screenOffset = index - gbl.menuScreenIndex;

            if (backwardsStep == true)
            {
                gbl.menuScreenIndex += listDisplayHeight;
                if ((list.Count - listDisplayHeight) < gbl.menuScreenIndex)
                {
                    gbl.menuScreenIndex = (short)(list.Count - listDisplayHeight);
                }
            }
            else
            {
                gbl.menuScreenIndex -= listDisplayHeight;

                if (gbl.menuScreenIndex < 0)
                {
                    gbl.menuScreenIndex = 0;
                }
            }

            index = gbl.menuScreenIndex + screenOffset;

            index = skipHeadings(backwardsStep, index, list, listDisplayHeight);

            sub_6C897(gbl.menuScreenIndex, yEnd, xEnd, yStart, xStart,
                list, normalColor, headingColor, displayFillWidth);
        }


        internal static int menu_sub_6CDCA(bool backwardsStep, int index, List<MenuItem> list, int listDisplayHeight) // sub_6CDCA
        {
			if( backwardsStep == true )
			{
                index += 1;

                if ((gbl.menuScreenIndex + listDisplayHeight - 1) < index)
                {
                    index = gbl.menuScreenIndex;
                }

                if ((list.Count - 1) < index)
                {
                    index = gbl.menuScreenIndex;
                }
			}
			else
			{
                index -= 1;

                if (index < gbl.menuScreenIndex)
                {
                    index = (short)(gbl.menuScreenIndex + listDisplayHeight - 1);
                }

                if ((list.Count - 1) < index)
                {
                    index = (short)(list.Count - 1);
                }
			}

            return skipHeadings( backwardsStep, index, list, listDisplayHeight );
        }


        internal static char sl_select_item(out MenuItem result_ptr, ref int index_ptr,
            ref bool redrawMenuItems, bool showExit, List<MenuItem> stringList, 
            int endY, int endX, int startY, int startX, 
            int highlightBgColor, int normalColor, int headingColor,
            string inputString, string extraTextString)
        {
            bool showPrevious;
            bool showNext;

            char ret_val = '\0'; /* Simeon */
            result_ptr = null; /*Simeon*/

            if (stringList == null)
            {
                index_ptr = 0;
                result_ptr = null;

                return '\0';
            }
            
            gbl.menuSelectedWord = 1;

            int listDisplayWidth = (endX - startX) + 1;
            int listDisplayHeight = (short)((endY - startY) + 1);

            int listCount = stringList.Count;

            if (listCount <= listDisplayHeight)
            {
                gbl.menuScreenIndex = 0;
            }

            if (gbl.menuScreenIndex > index_ptr)
            {
                gbl.menuScreenIndex = index_ptr;
                redrawMenuItems = true;
            }

            if (gbl.menuScreenIndex > listCount)
            {
                gbl.menuScreenIndex = 0;
                redrawMenuItems = true;
            }

            index_ptr++;
            index_ptr = menu_sub_6CDCA(false, index_ptr, stringList, listDisplayHeight);

            if (redrawMenuItems == true)
            {
                sub_6C897(gbl.menuScreenIndex, endY, endX, startY, startX,
                    stringList, normalColor, headingColor, listDisplayWidth);
            }

            redrawMenuItems = false;

            bool loop_end = false;

            while (loop_end == false)
            {
                ListItemHighlighted(index_ptr, stringList, startY, startX, highlightBgColor);
                string displayString = inputString;

                showNext = false;
                showPrevious = false;

                if ((listCount - listDisplayHeight) > gbl.menuScreenIndex)
                {
                    displayString += " Next";
                    showNext = true;
                }            
                
                if (gbl.menuScreenIndex > 0)
                {
                    displayString += " Prev";
                    showPrevious = true;
                }

                if (showExit == true)
                {
                    displayString += " Exit";
                }

                bool speical_key;
                char input_key = displayInput(out speical_key, false, 1, highlightBgColor, normalColor, headingColor, displayString, extraTextString);

                ListItemNormal(index_ptr, stringList, startY, startX, normalColor, headingColor);

                if (speical_key == true)
                {
                    switch (input_key)
                    {
                        case 'G':
                            index_ptr = menu_sub_6CDCA(false, index_ptr, stringList, listDisplayHeight);
                            break;

                        case 'O':
                            index_ptr = menu_sub_6CDCA(true, index_ptr, stringList, listDisplayHeight);
                            break;

                        case 'I':
                            if (showPrevious == true)
                            {
                                menu_sub_6CD38(false, ref index_ptr, stringList, listDisplayHeight, endY, endX, startY, startX, normalColor, headingColor, listDisplayWidth);
                            }
                            break;

                        case 'Q':
                            if (showNext == true)
                            {
                                menu_sub_6CD38(true, ref index_ptr, stringList, listDisplayHeight, endY, endX, startY, startX, normalColor, headingColor, listDisplayWidth);
                            }
                            break;
                    }
                }
                else
                {
                    switch (input_key)
                    {
                        case 'P':
                            menu_sub_6CD38(false, ref index_ptr, stringList, listDisplayHeight, endY, endX, startY, startX, normalColor, headingColor, listDisplayWidth);
                            break;

                        case 'N':

                            menu_sub_6CD38(true, ref index_ptr, stringList, listDisplayHeight, endY, endX, startY, startX, normalColor, headingColor, listDisplayWidth);
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

            gbl.menuSelectedWord = 2;

            do
            {
                inputKey = displayInput(false, 0, highlightFgColor, fgColor, extraStringFgColor, "Yes No", inputString);
            
            } while (yesNoFlags.MemberOf(inputKey) == false);

            return inputKey;
        }
	}
}
