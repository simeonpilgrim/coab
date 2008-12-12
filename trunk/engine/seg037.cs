using Classes;

namespace engine
{
    class seg037
    {
        static int[] outer_frame_bottom /*unk_16EB0*/ = { 1, 8, 6, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, 1, 6, 8, 1, 1, 1, 4, 1, 1, 1, 1, 1, 1, 6, 1, 1, 1, 1, 1, 1, 1, 1, 4, 3 };

        static byte[] unk_16ED6 = { 4, 3, 0, 6, 1, 1, 1, 1, 8, 1, 1, 4, 1, 1, 2, 1, 4 };

        static byte[] unk_16F0A = { 0, 7, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 5, 2, 9, 4 };

        static int[] outer_frame_top /*byte_16E60*/ = { 0, 6, 1, 1, 1, 1, 1, 1, 6, 1, 1, 1, 1, 4, 1, 1, 1, 6, 1, 1, 1, 1, 1, 1, 1, 8, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 6, 1, 2 };

        static byte[] x8x8_07 = { 0, 8, 1, 1, 1, 1, 1, 1, 1, 1, 1, 6, 1, 1, 1, 8, 4,
                                    1, 1, 1, 6, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 6, 1,
                                    1, 1, 1, 1, 8, 2 };

        static byte[] unk_16EE3 = { 1, 2, 1, 4, 1, 1, 1, 1, 1, 1, 8, 4, 1, 1, 3 };
        static int[] outer_frame_left  /*unk_16EF2*/ = { 0, 2, 9, 5, 2, 2, 2, 2, 2, 2, 5, 7, 2, 2, 2, 2, 2, 9, 7, 2, 2, 2, 7, 1 };
        static int[] outer_frame_right /*unk_16F1B*/ = { 2, 2, 9, 7, 2, 2, 2, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 7, 2, 2, 2, 2, 5, 2 };

        static byte[] unk_16F31 = { 5, 2, 0, 2, 7, 2, 2, 2, 2, 5, 2, 2, 2, 2, 1 };
        static byte[] unk_16F3E = { 2, 1, 2, 5, 9, 2, 2, 2, 7, 5, 2, 2, 2, 2, 3 };
        static byte[] unk_16F4D = { 0, 2, 9, 5, 2, 2, 2, 2, 2, 2, 5, 7, 2, 2, 2, 2, 2, 9, 7, 2, 2, 2, 1 };
        static byte[] unk_16F64 = { 0, 7, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 7, 5, 2, 2, 2, 2, 2, 2, 5, 2, 4 };
        static byte[] unk_16F7B = { 2, 2, 9, 7, 2, 2, 2, 5, 2, 2, 2, 2, 2, 2, 2, 2, 2, 7, 2, 2, 2, 2, 2 };



        internal static void DrawFrame_Outer() /* draw8x8_01 */
        {
            Display.UpdateStop();

            draw8x8_clear_area(0x16, 0x26, 1, 1);

            for (int col_x = 0; col_x <= 0x27; col_x++)
            {
                ovr038.Put8x8Symbol(0, false, outer_frame_top[col_x] + 0x11e, 0, col_x);
            }

            for (int row_y = 0; row_y < 0x17; row_y++)
            {
                ovr038.Put8x8Symbol(0, false, outer_frame_left[row_y] + 0x11e, row_y, 0);
                ovr038.Put8x8Symbol(0, false, outer_frame_right[row_y] + 0x11e, row_y, 0x27);
            }

            for (int col_x = 0; col_x <= 0x27; col_x++)
            {
                ovr038.Put8x8Symbol(0, false, outer_frame_bottom[col_x] + 0x11e, 0x17, col_x);
            }

            Display.UpdateStart();
        }


        internal static void draw8x8_02()
        {
            Display.UpdateStop();

            DrawFrame_Outer();

            for (int col_x = 0; col_x <= 0x27; col_x++)
            {
                ovr038.Put8x8Symbol(0, false, x8x8_07[col_x] + 0x11E, 3, col_x);
                ovr038.Put8x8Symbol(0, false, x8x8_07[col_x] + 0x11E, 8, col_x);
            }

            Display.UpdateStart();
        }


        internal static void draw8x8_03()
        {
            Display.UpdateStop();

            DrawFrame_Outer();

            for (int colX = 0; colX <= 0x27; colX++)
            {
                ovr038.Put8x8Symbol(0, false, x8x8_07[colX] + 0x11E, 0x10, colX);
            }

            for (int rowY = 0; rowY <= 0x10; rowY++)
            {
                ovr038.Put8x8Symbol(0, false, unk_16F0A[rowY] + 0x11E, rowY, 0x10);
            }

            for (int col_x = 2; col_x <= 14; col_x++)
            {
                ovr038.Put8x8Symbol(0, false, unk_16ED6[col_x] + 0x114, 2, col_x);
                ovr038.Put8x8Symbol(0, false, unk_16EE3[col_x] + 0x114, 14, col_x);
            }

            for (int row_y = 2; row_y <= 14; row_y++)
            {
                ovr038.Put8x8Symbol(0, false, unk_16F31[row_y] + 0x114, row_y, 2);
                ovr038.Put8x8Symbol(0, false, unk_16F3E[row_y] + 0x114, row_y, 14);
            }

            Display.UpdateStart();
        }


        internal static void DrawFrame_WildernessMap() // draw8x8_04
        {
            Display.UpdateStop();

            DrawFrame_Outer();

            for (int col_x = 0; col_x <= 0x27; col_x++)
            {
                ovr038.Put8x8Symbol(0, false, x8x8_07[col_x] + 0x11E, 0x10, col_x);
            }

            Display.UpdateStart();
        }


        internal static void draw8x8_05()
        {
            Display.UpdateStop();

            draw8x8_clear_area(0x10, 0x26, 1, 1);

            for (int col_x = 0; col_x <= 0x27; col_x++)
            {
                ovr038.Put8x8Symbol(0, false, outer_frame_top[col_x] + 0x11E, 0, col_x);
            }

            for (int row_y = 0; row_y <= 0x17; row_y++)
            {
                ovr038.Put8x8Symbol(0, false, outer_frame_left[row_y] + 0x11E, row_y, 0);
                ovr038.Put8x8Symbol(0, false, outer_frame_right[row_y] + 0x11E, row_y, 0x27);
            }

            for (int col_x = 0; col_x <= 0x27; col_x++)
            {
                ovr038.Put8x8Symbol(0, false, outer_frame_bottom[col_x] + 0x11E, 0x17, col_x);
            }

            for (int col_x = 0; col_x <= 0x27; col_x++)
            {
                ovr038.Put8x8Symbol(0, false, x8x8_07[col_x] + 0x11E, 0x10, col_x);
            }

            Display.UpdateStart();
        }


        internal static void DrawFrame_Combat() // draw8x8_06
        {
            Display.UpdateStop();

            draw8x8_clear_area(0x17, 0x27, 0, 0);

            // Top Bar
            for (int col_x = 0; col_x <= 0x27; col_x++)
            {
                ovr038.Put8x8Symbol(0, false, outer_frame_top[col_x] + 0x11E, 0, col_x);
            }

            // Three Vert Bars
            for (int row_y = 0; row_y <= 0x16; row_y++)
            {
                ovr038.Put8x8Symbol(0, false, unk_16F4D[row_y] + 0x11e, row_y, 0);
                ovr038.Put8x8Symbol(0, false, unk_16F64[row_y] + 0x11e, row_y, 0x16);
                ovr038.Put8x8Symbol(0, false, unk_16F7B[row_y] + 0x11e, row_y, 0x27);
            }

            // Bottom Bar
            for (int col_x = 0; col_x <= 0x27; col_x++)
            {
                ovr038.Put8x8Symbol(0, false, outer_frame_bottom[col_x] + 0x11e, 0x16, col_x);
            }

            Display.UpdateStart();
        }


        internal static void draw8x8_07()
        {
            Display.UpdateStop();

            DrawFrame_Outer();

            for (int col_x = 0; col_x <= 0x27; col_x++)
            {
                ovr038.Put8x8Symbol(0, false, x8x8_07[col_x] + 0x11e, 2, col_x);
            }

            Display.UpdateStart();
        }


        internal static void draw8x8_clear_area(int yEnd, int xEnd, int yStart, int xStart)
        {
            seg041.DrawRectangle(0, yEnd, xEnd, yStart, xStart);
        }
    }
}
