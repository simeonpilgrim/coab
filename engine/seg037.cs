using Classes;

namespace engine
{
    class seg037
    {
        static byte[] unk_16EB0 = { 1, 8, 6, 1, 1, 1, 1, 1, 1, 1, 
                                    1, 4, 1, 1, 1, 1, 1, 6, 8, 1, 
                                    1, 1, 4, 1, 1, 1, 1, 1, 1, 6, 
                                    1, 1, 1, 1, 1, 1, 1, 1, 4, 3 };

        static byte[] unk_16ED6 = { 4, 3, 0, 6, 1, 1, 1, 1, 8, 1, 1, 4, 1, 1, 2, 1, 4 };

        static byte[] unk_16F0A = { 0, 7, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 2, 9, 4 };

        static byte[] byte_16E60 = { 0, 6, 1, 1, 1, 1, 1, 1, 6, 1, 1, 1, 1, 4, 1, 1,
                                       1, 6, 1, 1, 1, 1, 1, 1, 1, 8, 1, 1, 1, 1, 1, 1, 
                                       1, 4, 1, 1, 1, 6, 1, 2 };

        static byte[] x8x8_07 = { 0, 8, 1, 1, 1, 1, 1, 1, 1, 1, 1, 6, 1, 1, 1, 8, 4,
                                    1, 1, 1, 6, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 6, 1,
                                    1, 1, 1, 1, 8, 2 };



        static byte[] unk_16EE3 = { 1, 2, 1, 4, 1, 1, 1, 1, 1, 1, 8, 4, 1, 1, 3 };
        static byte[] unk_16EF2 = { 0, 2, 9, 5, 2, 2, 2, 2, 2, 2, 5, 7, 2, 2, 2, 2, 2,
                                      9, 7, 2, 2, 2, 7, 1 };
        static byte[] unk_16F1B = { 2, 2, 9, 7, 2, 2, 2, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2,
                                      7, 2, 2, 2, 2,5  };

        static byte[] unk_16F31 = { 5, 2, 0, 2, 7, 2, 2, 2, 2, 5, 2, 2, 2, 2, 1 };
        static byte[] unk_16F3E = { 2, 1, 2, 5, 9, 2, 2, 2, 7, 5, 2, 2, 2, 2, 3 };
        static byte[] unk_16F4D = { 0, 2, 9, 5, 2, 2, 2, 2, 2, 2, 5, 7, 2, 2, 2, 2, 2, 9, 7, 2, 2, 2, 1 };
        static byte[] unk_16F64 = { 0, 7, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 7, 5, 2, 2, 2, 2, 2, 2, 5, 2, 4 };
        static byte[] unk_16F7B = { 2, 2, 9, 7, 2, 2, 2, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 7, 2, 2, 2, 2, 2 };



        internal static void draw8x8_01()
        {
            byte var_2;
            byte var_1;

            Display.UpdateStop();

            draw8x8_clear_area(0x16, 0x26, 1, 1);

            for (var_1 = 0; var_1 <= 0x27; var_1++)
            {
                ovr038.Put8x8Symbol(0, 0, (short)(byte_16E60[var_1] + 0x11e), 0, var_1);
            }

            for (var_2 = 0; var_2 < 0x17; var_2++)
            {
                ovr038.Put8x8Symbol(0, 0, (short)(unk_16EF2[var_2] + 0x11e), var_2, 0);
                ovr038.Put8x8Symbol(0, 0, (short)(unk_16F1B[var_2] + 0x11e), var_2, 0x27);
            }

            for (var_1 = 0; var_1 <= 0x27; var_1++)
            {
                ovr038.Put8x8Symbol(0, 0, (short)(unk_16EB0[var_1] + 0x11e), 0x17, var_1);
            }

            Display.UpdateStart();
        }


        internal static void draw8x8_02()
        {
            byte var_1;

            Display.UpdateStop();

            draw8x8_01();

            for (var_1 = 0; var_1 <= 0x27; var_1++)
            {
                ovr038.Put8x8Symbol(0, 0, (short)(x8x8_07[var_1] + 0x11E), 3, var_1);
                ovr038.Put8x8Symbol(0, 0, (short)(x8x8_07[var_1] + 0x11E), 8, var_1);
            }

            Display.UpdateStart();
        }


        internal static void draw8x8_03()
        {
            byte var_2;
            byte var_1;

            Display.UpdateStop();
            
            draw8x8_01();

            for (var_1 = 0; var_1 <= 0x27; var_1++)
            {
                ovr038.Put8x8Symbol(0, 0, (short)(x8x8_07[var_1] + 0x11E), 0x10, var_1);
            }

            for (var_2 = 0; var_2 <= 0x10; var_2++)
            {
                ovr038.Put8x8Symbol(0, 0, (short)(unk_16F0A[var_2] + 0x11E), var_2, 0x10);
            }

            for (var_1 = 2; var_1 <= 14; var_1++)
            {
                ovr038.Put8x8Symbol(0, 0, (short)(unk_16ED6[var_1] + 0x114), 2, var_1);
                ovr038.Put8x8Symbol(0, 0, (short)(unk_16EE3[var_1] + 0x114), 14, var_1);
            }

            for (var_2 = 2; var_2 <= 14; var_2++)
            {
                ovr038.Put8x8Symbol(0, 0, (short)(unk_16F31[var_2] + 0x114), var_2, 2);
                ovr038.Put8x8Symbol(0, 0, (short)(unk_16F3E[var_2] + 0x114), var_2, 14);
            }

            Display.UpdateStart();

        }


        internal static void draw8x8_04()
        {
            byte var_1;

            Display.UpdateStop();

            draw8x8_01();

            for (var_1 = 0; var_1 <= 0x27; var_1++)
            {
                ovr038.Put8x8Symbol(0, 0, (short)(x8x8_07[var_1] + 0x11E), 0x10, var_1);
            }

            Display.UpdateStart();
        }


        internal static void draw8x8_05()
        {
            byte var_2;
            byte var_1;

            Display.UpdateStop();

            draw8x8_clear_area(0x10, 0x26, 1, 1);

            for (var_1 = 0; var_1 <= 0x27; var_1++)
            {
                ovr038.Put8x8Symbol(0, 0, (short)(byte_16E60[var_1] + 0x11E), 0, var_1);
            }

            for (var_2 = 0; var_2 <= 0x17; var_2++)
            {
                ovr038.Put8x8Symbol(0, 0, (short)(unk_16EF2[var_2] + 0x11E), var_2, 0);
                ovr038.Put8x8Symbol(0, 0, (short)(unk_16F1B[var_2] + 0x11E), var_2, 0x27);
            }

            for (var_1 = 0; var_1 <= 0x27; var_1++)
            {
                ovr038.Put8x8Symbol(0, 0, (short)(unk_16EB0[var_1] + 0x11E), 0x17, var_1);
            }

            for (var_1 = 0; var_1 <= 0x27; var_1++)
            {
                ovr038.Put8x8Symbol(0, 0, (short)(x8x8_07[var_1] + 0x11E), 0x10, var_1);
            }

            Display.UpdateStart();

        }


        internal static void draw8x8_06()
        {
            byte var_2;
            byte var_1;

            Display.UpdateStop();

            draw8x8_clear_area(0x17, 0x27, 0, 0);

            for (var_1 = 0; var_1 <= 0x27; var_1++)
            {
                ovr038.Put8x8Symbol(0, 0, (short)(byte_16E60[var_1] + 0x11E), 0, var_1);
            }


            for (var_2 = 0; var_2 <= 0x16; var_2++)
            {
                ovr038.Put8x8Symbol(0, 0, (short)(unk_16F4D[var_2] + 0x11e), var_2, 0);
                ovr038.Put8x8Symbol(0, 0, (short)(unk_16F64[var_2] + 0x11e), var_2, 0x16);
                ovr038.Put8x8Symbol(0, 0, (short)(unk_16F7B[var_2] + 0x11e), var_2, 0x27);
            }

            for (var_1 = 0; var_1 <= 0x27; var_1++)
            {
                ovr038.Put8x8Symbol(0, 0, (short)(unk_16EB0[var_1] + 0x11e), 0x16, var_1);
            }

            Display.UpdateStart();

        }


        internal static void draw8x8_07()
        {
            byte loop_var;

            Display.UpdateStop();

            draw8x8_01();

            for (loop_var = 0; loop_var <= 0x27; loop_var++)
            {
                ovr038.Put8x8Symbol(0, 0, (short)(x8x8_07[loop_var] + 0x11e), 2, loop_var);
            }

            Display.UpdateStart();

        }


        internal static void draw8x8_clear_area(int yEnd, int xEnd, int yStart, int xStart)
        {
            seg041.ega01(0, yEnd, xEnd, yStart, xStart);
        }
    }
}
