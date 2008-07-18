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


        internal static void load_8x8d1_201()
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

                seg051.FreeMem(block_size, block_ptr);
            }
        }


        internal static void display_char01(bool arg_0, char ch, int repeatCount, int bgColor, int fgColor,
            int YCol, int XCol)
        {
            char var_2;

            if (XCol <= 39 &&
                YCol <= 24 &&
                gbl.dax_8x8d1_201 != null)
            {
                if (arg_0 == true)
                {
                    var_2 = (char)(seg051.UpCase(ch) % 0x40);
                }
                else
                {
                    var_2 = ch;
                }

                for (int i = 0; i < 8; i++)
                {
                    gbl.byte_1C8C2[i] = gbl.dax_8x8d1_201[var_2, i];
                }

                for (int i = 0; i < repeatCount; i++)
                {
                    ega_char_out(bgColor, fgColor, YCol, XCol + i);
                }
            }
        }


        internal static void displaySpaceChar(int count, int color, int yCol, int xCol)
        {
            if (xCol >= 0 && xCol <= 0x27 &&
                yCol >= 0 && yCol <= 0x18)
            {
                display_char01(true, ' ', count, color, color, yCol, xCol);

                Display.Update();
            }
        }


        internal static void displayString(string str, int bgColor, int fgColor, int yCol, int xCol)
        {
            if (xCol <= 0x27 && yCol <= 0x27)
            {
                foreach (char ch in str)
                {
                    display_char01(true, ch, 1, bgColor, fgColor, yCol, xCol);
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
                display_char01(true, text[text_index-1], 1, bgColor, fgColor, gbl.textYCol, gbl.textXCol);

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

        static private byte[] unk_16FA6 = { 2, 0x70, 0, 0x8C };

        internal static void press_any_key(string text, bool clearArea,
            int bgColor, int fgColor,
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
                //int var_102 = (xEnd - xStart) + 1;

                Set var_125 = new Set(0x404, unk_16FA6);
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
            string var_22B;
            char var_12B;
            string var_12A;
            int var_2A;

            displaySpaceChar(0x28, 0, 0x18, 0);

            displayString(prompt, bgColor, fgColor, 0x18, 0);

            var_2A = prompt.Length;

            var_12A = string.Empty;

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
                    var_12A = seg051.Copy(var_12A.Length - 1, 0, var_12A, out var_22B);

                    displaySpaceChar(1, 0, 0x18, var_2A);
                    var_2A -= 1;
                }

            } while (var_12B != 0x0d && var_12B != 0x1B && gbl.inDemo == false);

            displaySpaceChar(0x28, 0, 0x18, 0);

            return var_12A.ToUpper();
        }


        internal static ushort getUserInputShort(byte bgColor, byte fgColor, string prompt)
        {
            bool var_5A = true;
            int var_2F = 0; /* Simeon */
            ushort var_2;

            do
            {
                string var_58 = getUserInputString(6, bgColor, fgColor, prompt);

                try
                {
                    var_2F = int.Parse(var_58);
                    var_5A = false;
                }
                catch (System.Exception)
                {
                    var_5A = true;
                }

                if (var_5A == false)
                {
                    if (var_2F > 0x00010000 ||
                        var_2F < 0)
                    {
                        var_5A = true;
                    }
                }
            } while (var_5A == true);

            var_2 = (ushort)var_2F;

            return var_2;
        }


        internal static void displayAndDebug(string arg_0, byte arg_4, byte arg_6)
        {
            displaySpaceChar(0x28, 0, 0x18, 0);

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


        internal static void clear_screan()
        {
            DrawRectangle(0, 0x18, 0x27, 0, 0);
        }


        internal static void DisplayStatusText(byte bgColor, byte fgColor, string text) /* sub_10ECF */
        {
            displaySpaceChar(0x28, 0, 0x18, 0);

            displayString(text, bgColor, fgColor, 0x18, 0);

            GameDelay();

            displaySpaceChar(0x28, 0, 0x18, 0);
        }


        internal static void GameDelay()
        {
            seg049.SysDelay(gbl.game_speed_var * 100);
        }


        static void ega_char_out(int bgColor, int fgColor, int Y, int X)
        {
            for (int i = 0; i < 8; i++)
            {
                Display.DisplayMono8x1(X, (Y * 8) + i, gbl.byte_1C8C2[i], bgColor, fgColor);
            }
        }
    }
}
