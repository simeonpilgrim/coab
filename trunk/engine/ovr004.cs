using Classes;

namespace engine
{
    class ovr004
    {
        static string[] codeWheel = { 
								 "CWLNRTESSCEDCSHSISERRRNSHSSTSSNNHSHN",
								 "LAASDRAIILIDSUGADAEEOEGRLSELIITESOIO",
								 "LRUNIMMORIIGRRIUPTIIUELIMLHMIXACGRIL",
								 "Z0LIOHEUVNODSGEOGXYWISIOCRARLRARRHOI",
								 "AMTELRLUIYNAEOOITOUELRREREUIMADPPFAB",
								 "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"
							 };

        internal static void copy_protection()
        {
            string var_117;

            string var_17;
            byte var_15;
            byte var_14;
            string var_13;
            string var_11;
            byte var_7;
            byte var_6;
            int var_5;
            char var_3;
            char var_2;
            int var_1;


            ovr034.Load24x24Set(0x1a, 0, 1, "tiles");
            ovr034.Load24x24Set(0x16, 0x1A, 2, "tiles");

            seg037.draw8x8_01();

            seg041.displayString("Align the espruar and dethek runes", 0, 10, 2, 3);
            seg041.displayString("shown below, on translation wheel", 0, 10, 3, 3);
            seg041.displayString("like this:", 0, 10, 4, 3);
            var_1 = 0;

            do
            {

                var_6 = seg051.Random(26);
                var_7 = seg051.Random(22);

                ovr034.sub_760F7(0, var_6, 3, 0x11);
                ovr034.sub_760F7(0, (byte)(var_7 + 0x1a), 7, 0x11);

                seg040.DrawOverlay();
                var_14 = seg051.Random(3);

                switch (var_14)
                {
                    case 0:
                        var_11 = "-..-..-..";
                        break;

                    case 1:
                        var_11 = "- - - - -";
                        break;

                    case 2:
                        var_11 = ".........";
                        break;

                    default:
                        var_11 = string.Empty;
                        break;
                }

                var_15 = seg051.Random(6);

                seg051.Str(1, out var_13, 0, 6 - var_15);
                var_117 = "Type the character in box number " + var_13;

                seg041.displayString(var_117, 0, 10, 12, 3);

                seg041.displayString("under the ", 0, 10, 13, 3);
                seg041.displayString(var_11, 0, 15, 13, 14);
                seg041.displayString("path.", 0, 10, 13, 0x19);

                var_5 = var_6 + 0x22 - var_7 + (var_14 * 12) + ((5 - var_15) << 1);

                while (var_5 < 0)
                {
                    var_5 += 36;
                }

                while (var_5 > 35)
                {
                    var_5 -= 36;
                }

                var_3 = codeWheel[var_15][var_5];

                var_17 = " ";
                var_17 = seg041.sub_10B26(1, 0, 13, "type character and press return: ");

                var_2 = (var_17 == null ||var_17.Length == 0 ) ? ' ' : var_17[0];
                var_1++;

                if (var_2 != var_3)
                {
                    seg041.sub_10ECF(0, 14, "Sorry, that's incorrect.");
                }
                else
                {
                    return;
                }
            } while (var_2 != var_3 && var_1 < 3);

            if (var_1 >= 3)
            {
                seg044.sub_120E0(gbl.word_188C0);
                seg044.sub_120E0(gbl.word_188C8);
                gbl.game_speed_var = 9;
                seg041.sub_10ECF(0, 14, "An unseen force hurls you into the abyss!");
                seg049.SysDelay(0x3E8);
                seg043.print_and_exit();
            }
        }
    }
}
