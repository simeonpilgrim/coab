using Classes;

namespace engine
{
    class seg041
    {
        internal static void ega01(byte colour, int yEnd, int xEnd, int yStart, int xStart)
        {
            xStart *= 8;
            xEnd = (xEnd + 1) * 8;
            yStart *= 8;
            yEnd = (yEnd + 1) * 8;

            for (int x = xStart; x < xEnd; x += 2)
            {
                for (int y = yStart; y < yEnd; y++)
                {
                    Display.SetPixel2(x, y, colour);
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


        internal static void display_char01(bool arg_0, char ch, byte repeatCount, byte bgColor, byte fgColor,
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


        internal static void sub_1071A(byte arg_0, byte arg_2, byte yCol, int xCol)
        {
            if (xCol >= 0 && xCol <= 0x27 &&
                yCol >= 0 && yCol <= 0x18)
            {
                display_char01(true, ' ', arg_0, arg_2, arg_2, yCol, xCol);
            }

            Display.Update();
        }


        internal static void displayString(string s, byte bgColor, byte fgColor, int yCol, int xCol)
        {
            if (xCol <= 0x27 && yCol <= 0x27)
            {
                foreach (char ch in s)
                {
                    display_char01(true, ch, 1, bgColor, fgColor, yCol, xCol);
                    xCol++;
                }
                Display.Update();
            }
        }



        internal static void sub_107DE(string bp_var_100, ref byte bp_var_103, byte bp_var_104, byte bp_arg_6, byte bp_arg_8)
        {
            while (bp_var_103 <= bp_var_104)
            {
                display_char01(true, bp_var_100[bp_var_103-1], 1, bp_arg_6, bp_arg_8, gbl.textYCol, gbl.textXCol);

                if (gbl.byte_1B2F2 != 0)
                {
                    seg049.SysDelay(gbl.game_speed_var * 3);
                }

                bp_var_103 += 1;
                gbl.textXCol++;
            }
        }


        internal static void sub_10854(string bp_var_100, byte bp_var_101, ref byte bp_var_103)
        {
            while (bp_var_103 < bp_var_101 &&
                bp_var_100[bp_var_103] == ' ')
            {
                bp_var_103 += 1;
            }
        }

        static private byte[] unk_16FA6 = { 2, 70, 0, 0x8C };

        internal static void press_any_key(string arg_0, bool clearArea, byte arg_6, byte arg_8, byte yEnd, byte xEnd, byte yStart, byte xStart)
        {
            Set var_125;
            byte var_104;
            byte var_103;
            byte var_102;
            byte var_101;
            string var_100;

            var_100 = arg_0;

            if (xStart <= 0x27 && yStart <= 0x18 && xEnd <= 0x27 && yEnd <= 0x27)
            {
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

                var_103 = 1;
                var_101 = (byte)var_100.Length;

                if (var_101 != 0)
                {
                    var_102 = (byte)((xEnd - xStart) + 1);

                    do
                    {
                        var_104 = var_103;
                        var_125 = new Set(0x404, unk_16FA6);

                        while (var_104 < var_101 &&
                            var_125.MemberOf(var_100[var_104-1]) == true)
                        {
                            var_104++;
                        }

                        while (var_104 < var_101 &&
                            var_125.MemberOf(var_100[var_104-1]) == false &&
                            var_100[var_104-1] != ' ')
                        {
                            var_104++;
                        }

                        if (var_100[var_104-1] != ' ')
                        {
                            while (var_104 < var_101 &&
                                var_125.MemberOf(var_100[var_104 ]) == true)
                            {
                                var_104++;
                            }

                        }

                        if (((var_104 - var_103) + gbl.textXCol) > xEnd)
                        {
                            if (((var_104 - var_103) + gbl.textXCol) == xEnd &&
                                var_100[var_104-1] == ' ')
                            {
                                var_104 -= 1;
                                sub_107DE(var_100, ref var_103, var_104, arg_6, arg_8);
                            }

                            gbl.textXCol = xStart;
                            gbl.textYCol++;
                            sub_10854(var_100, var_101, ref var_103);

                            if (gbl.textYCol > yEnd &&
                                var_103 < var_101)
                            {
                                gbl.textXCol = xStart;
                                gbl.textYCol = yStart;

                                displayAndDebug("Press	any key	to continue", 0, 13);
                                seg043.clear_keyboard();

                                seg037.draw8x8_clear_area(yEnd, xEnd, yStart, xStart);

                                sub_107DE(var_100, ref var_103, var_104, arg_6, arg_8);
                            }
                        }
                        else
                        {
                            sub_107DE(var_100, ref var_103, var_104, arg_6, arg_8);
                        }

                    } while (var_103 <= var_101);

                    if (gbl.textXCol > xEnd)
                    {
                        gbl.textXCol = xStart;
                        gbl.textYCol++;
                    }
                }
            }
        }


        internal static string sub_10B26(byte arg_0, byte arg_2, byte arg_4, string arg_6)
        {
            string var_22B;
            char var_12B;
            string var_12A;
            int var_2A;
            string var_29;

            var_29 = arg_6;

            sub_1071A(0x28, 0, 0x18, 0);

            displayString(var_29, arg_2, arg_4, 0x18, 0);

            var_2A = var_29.Length;

            var_12A = string.Empty;

            do
            {
                var_12B = (char)seg043.debug_txt();

                if (var_12B >= 0x20 && var_12B <= 0x7A)
                {
                    if (var_12A.Length < arg_0)
                    {
                        var_12A += var_12B.ToString();

                        var_2A++;

                        displayString(var_12B.ToString(), 0, 15, 0x18, var_2A);
                    }
                }
                else if (var_12B == 8 && var_12A.Length > 0)
                {
                    var_12A = seg051.Copy(var_12A.Length - 1, 0, var_12A, out var_22B);

                    sub_1071A(1, 0, 0x18, var_2A);
                    var_2A -= 1;
                }

            } while (var_12B != 0x0d && var_12B != 0x1B && gbl.inDemo == false);

            sub_1071A(0x28, 0, 0x18, 0);

            return var_12A.ToUpper();
        }


        internal static ushort sub_10CB7(byte arg_0, byte arg_2, string arg_4)
        {
            bool var_5A;
            string var_58;
            int var_2F = 0; /* Simeon */
            ushort var_2;

            do
            {
                var_58 = sub_10B26(6, arg_0, arg_2, arg_4);

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
            sub_1071A(0x28, 0, 0x18, 0);

            displayString(arg_0, arg_4, arg_6, 0x18, 0);
            seg043.debug_txt();
        }


        internal static int time01()
        {
            System.DateTime dt = System.DateTime.Now;

            return (dt.Hour * 360000) + (dt.Minute * 6000) + (dt.Second * 100) + (dt.Millisecond / 10);
        }


        internal static void clear_screan()
        {
            ega01(0, 0x18, 0x27, 0, 0);
        }


        internal static void sub_10ECF(byte arg_0, byte arg_2, string arg_4)
        {
            sub_1071A(0x28, 0, 0x18, 0);

            displayString(arg_4, arg_0, arg_2, 0x18, 0);

            GameDelay();

            sub_1071A(0x28, 0, 0x18, 0);
        }


        internal static void GameDelay()
        {
            seg049.SysDelay(gbl.game_speed_var * 100);
        }


        static void ega_char_out(byte bgColor, byte fgColor, int Y, int X)
        {
            for (int i = 0; i < 8; i++)
            {
                Display.DisplayMono8x1(X, (Y * 8) + i, gbl.byte_1C8C2[i], bgColor, fgColor);
            }
        }
    }
}
