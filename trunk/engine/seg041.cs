using Classes;

namespace engine
{
    class seg041
    {
        internal static void DrawRectangle(byte color, int yEnd, int xEnd, int yStart, int xStart)
        {
            xStart *= 8;
            xEnd = (xEnd + 1) * 8;
            yStart *= 8;
            yEnd = (yEnd + 1) * 8;

            for (int x = xStart; x < xEnd; x ++)
            {
                for (int y = yStart; y < yEnd; y++)
                {
                    Display.SetPixel3(x, y, color);
                }
            }
        }


        internal static void Load8x8Tiles() // load_8x8d1_201
        {
            byte[] block_ptr;
            short block_size;

            seg042.load_decode_dax(out block_ptr, out block_size, 201, "8X8d1.dax");

            if (block_size != 0)
            {
                for (int i = 0, j = 0; i < block_size && j < 177; i += 8, j++)
                {
                    for (int k = 0; k < 8 && (i + k) < block_size; k++)
                    {
                        gbl.dax_8x8d1_201[j, k] = block_ptr[i + k];
                    }
                }
            }
        }


        internal static void display_char01(char ch, int repeatCount, int bgColor, int fgColor, int YCol, int XCol) // display_char01
        {
            if (XCol < 40 &&
                YCol < 25 )
            {
                char index = (char)(char.ToUpper(ch) % 0x40);

                for (int i = 0; i < 8; i++)
                {
                    gbl.monoCharData[i] = gbl.dax_8x8d1_201[index, i];
                }

                for (int i = 0; i < repeatCount; i++)
                {
                    Display.DisplayMono8x8(XCol + i, YCol, gbl.monoCharData, bgColor, fgColor);
                }
            }
        }

        internal static void displaySpaceChar(int count, int color, int yCol, int xCol)
        {
            if (xCol >= 0 && xCol <= 0x27 &&
                yCol >= 0 && yCol <= 0x18)
            {
                display_char01(' ', count, color, color, yCol, xCol);

                Display.Update();
            }
        }


        internal static void displayString(string str, int bgColor, int fgColor, int yCol, int xCol)
        {
            if (xCol <= 0x27 && yCol <= 0x27)
            {
                foreach (char ch in str)
                {
                    display_char01(ch, 1, bgColor, fgColor, yCol, xCol);
                    xCol++;
                }
                Display.Update();
            }
        }



        internal static void displayStringSlow(string text
            , ref int text_index, int text_length, int bgColor, int fgColor) // sub_107DE
        {
            while (text_index <= text_length)
            {
                display_char01(text[text_index-1], 1, bgColor, fgColor, gbl.textYCol, gbl.textXCol);

                if (gbl.DelayBetweenCharacters)
                {
                    seg049.SysDelay(gbl.game_speed_var * 3);
                }

                text_index += 1;
                gbl.textXCol++;
            }
        }


        internal static void text_skip_space(string text, int text_max, ref int text_index) /* sub_10854 */
        {
            while (text_index < text_max &&
                text[text_index-1] == ' ')
            {
                text_index += 1;
            }
        }

        internal static int[,] bounds = new int[3, 4] { 
            { 0x16, 0x26, 0x11, 1 }, 
            { 0x16, 0x26, 0x15, 1 },
            { 0x15, 0x26, 1, 0x17 } // TextRegion.CombatSummary
        };

        internal static void press_any_key(string text, bool clearArea, int bgColor, int fgColor, TextRegion region)
        {
            int r = (int)region;
            press_any_key(text, clearArea, bgColor, fgColor, bounds[r, 0], bounds[r, 1], bounds[r, 2], bounds[r, 3]);
        }

        internal static void press_any_key(string text, bool clearArea, int bgColor, int fgColor,
            int yEnd, int xEnd, int yStart, int xStart)
        {
            if (xStart > 0x27 || yStart > 0x18 ||
                xEnd > 0x27 && yEnd > 0x27)
            {
                return;
            }

            if (gbl.textXCol < xStart ||
                gbl.textXCol > xEnd ||
                gbl.textYCol < yStart ||
                gbl.textYCol > yEnd)
            {
                gbl.textXCol = xStart;
                gbl.textYCol = yStart;
            }

            if (clearArea == true)
            {
                seg037.draw8x8_clear_area(yEnd, xEnd, yStart, xStart);
                gbl.textXCol = xStart;
                gbl.textYCol = yStart;
            }

            int text_start = 1;
            int input_lenght = text.Length;

            if (input_lenght != 0)
            {
                Set var_125 = new Set(0x404, new byte[] { 2, 0x70, 0, 0x8C });
                do
                {
                    int text_end = text_start;

                    while (text_end < input_lenght &&
                        var_125.MemberOf(text[text_end - 1]) == true)
                    {
                        text_end++;
                    }

                    while (text_end < input_lenght &&
                        var_125.MemberOf(text[text_end - 1]) == false &&
                        text[text_end - 1] != ' ')
                    {
                        text_end++;
                    }

                    if (text[text_end - 1] != ' ')
                    {
                        while (text_end + 1 < input_lenght &&
                            var_125.MemberOf(text[text_end]) == true)
                        {
                            text_end++;
                        }
                    }

                    if (((text_end - text_start) + gbl.textXCol) > xEnd)
                    {
                        if (((text_end - text_start) + gbl.textXCol) == xEnd &&
                            text[text_end - 1] == ' ')
                        {
                            text_end -= 1;
                            displayStringSlow(text, ref text_start, text_end, bgColor, fgColor);
                        }

                        gbl.textXCol = xStart;
                        gbl.textYCol++;
                        text_skip_space(text, input_lenght, ref text_start);

                        if (gbl.textYCol > yEnd &&
                            text_start < input_lenght)
                        {
                            gbl.textXCol = xStart;
                            gbl.textYCol = yStart;

                            displayAndDebug("Press any key to continue", 0, 13);
                            seg043.clear_keyboard();

                            seg037.draw8x8_clear_area(yEnd, xEnd, yStart, xStart);

                            displayStringSlow(text, ref text_start, text_end, bgColor, fgColor);
                        }
                    }
                    else
                    {
                        displayStringSlow(text, ref text_start, text_end, bgColor, fgColor);
                        Display.Update();
                    }
                } while (text_start <= input_lenght);

                if (gbl.textXCol > xEnd)
                {
                    gbl.textXCol = xStart;
                    gbl.textYCol++;
                }
            }
        }


        internal static string getUserInputString(byte inputLen, byte bgColor, byte fgColor, string prompt)
        {
            ovr027.ClearPromptAreaNoUpdate();

            displayString(prompt, bgColor, fgColor, 0x18, 0);

            int var_2A = prompt.Length;

            char var_12B;
            string var_12A = string.Empty;

            do
            {
                var_12B = (char)seg043.GetInputKey();

                if (var_12B >= 0x20 && var_12B <= 0x7A)
                {
                    if (var_12A.Length < inputLen)
                    {
                        var_12A += var_12B.ToString();

                        var_2A++;

                        displayString(var_12B.ToString(), 0, 15, 0x18, var_2A);
                    }
                }
                else if (var_12B == 8 && var_12A.Length > 0)
                {
                    var_12A = seg051.Copy(var_12A.Length - 1, 0, var_12A);

                    displaySpaceChar(1, 0, 0x18, var_2A);
                    var_2A -= 1;
                }

            } while (var_12B != 0x0d && var_12B != 0x1B && gbl.inDemo == false);

            ovr027.ClearPromptAreaNoUpdate();

            return var_12A.ToUpper();
        }


        internal static ushort getUserInputShort(byte bgColor, byte fgColor, string prompt)
        {
            bool bad_input = true;
            int value = 0;

            do
            {
                string input = getUserInputString(6, bgColor, fgColor, prompt);

                try
                {
                    value = int.Parse(input);
                    bad_input = false;
                }
                catch (System.Exception)
                {
                    bad_input = true;
                }

                if (bad_input == false)
                {
                    bad_input = (value > 0x00010000 || value < 0);
                }
            } while (bad_input == true);

            return (ushort)value;
        }


        internal static void displayAndDebug(string arg_0, byte arg_4, byte arg_6)
        {
            ovr027.ClearPromptAreaNoUpdate();

            displayString(arg_0, arg_4, arg_6, 0x18, 0);
            seg043.GetInputKey();
        }


        /// <summary>
        /// Gets the centi seconds since midnight
        /// </summary>
        internal static int time01() // time01
        {
            System.DateTime dt = System.DateTime.Now;

            return (dt.Hour * 360000) + (dt.Minute * 6000) + (dt.Second * 100) + (dt.Millisecond / 10);
        }


        internal static void ClearScreen()
        {
            DrawRectangle(0, 0x18, 0x27, 0, 0);
        }


        internal static void DisplayStatusText(byte bgColor, byte fgColor, string text) /* sub_10ECF */
        {
            ovr027.ClearPromptAreaNoUpdate();

            displayString(text, bgColor, fgColor, 0x18, 0);

            GameDelay();

            ovr027.ClearPromptAreaNoUpdate();
        }


        internal static void GameDelay()
        {
            seg049.SysDelay(gbl.game_speed_var * 100);
        }
    }
}
