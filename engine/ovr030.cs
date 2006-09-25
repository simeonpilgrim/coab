using Classes;

namespace engine
{
    class ovr030
    {
        static byte[] unk_16DAA = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        static byte[] unk_16DBA = { 0xC, 0xC, 0xC, 0xC, 4, 5, 6, 7, 0xC, 0xC, 0xA, 0xC, 0xC, 0xC, 0xE, 0xC };
        static byte[] unk_16DCA = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        static byte[] unk_16DDA = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 0, 14, 15 };

        internal static void sub_7000A(DaxBlock dax_block, byte arg_4, short arg_6, short arg_8)
        {
            if (dax_block != null)
            {
                if (gbl.area_ptr.field_3FE > 0 ||
                    arg_4 != 0)
                {
                    if (gbl.area_ptr.field_3FE > 0)
                    {
                        seg040.DaxBlockRecolor(dax_block, 1, 0, unk_16DBA, unk_16DAA);
                    }

                    seg040.sub_E353(gbl.overlayLines, dax_block, 0, 0, (short)(arg_6 - 1), (short)(arg_8 - 1));
                    seg040.DrawOverlay();
                }
                else
                {
                    seg040.draw_picture(dax_block, arg_6, arg_8);
                }
            }
        }


        internal static void sub_7008D(DaxBlock dax_block, byte arg_4, byte arg_6)
        {
            sub_7000A(dax_block, 0, arg_4, arg_6);
        }


        internal static void sub_700AD(DaxBlock dax_block, byte arg_4, byte arg_6)
        {
            sub_7000A(dax_block, 0, (short)(arg_4 + 5), arg_6);
        }


        internal static void load_pic_final(ref DaxArray arg_0, byte masked, byte arg_6, string arg_8)
        {
            DaxBlock var_2C;
            byte var_28;
            DaxArray var_27;
            byte var_23;
            byte var_22;
            bool var_21;
            short var_20 = 0; /* Simeon */
            short var_1E;
            byte[] mem = null;
            short width;
            short height;
            byte var_12;
            short var_11;
            byte[] var_F;
            short var_B;
            string var_9;

            var_9 = arg_8;

            if (var_9 != gbl.byte_1D5A2 ||
                arg_6 != gbl.byte_1D5B4)
            {
                if (arg_6 != 0xff)
                {
                    if (gbl.AnimationsOn == true)
                    {
                        seg041.sub_1071A(0x28, 0, 0x18, 0);
                        seg041.displayString("Loading...Please Wait", 0, 10, 0x18, 0);
                    }

                    DaxArrayFreeDaxBlocks(arg_0);
                    gbl.byte_1D5A2 = var_9;

                    gbl.byte_1D5B4 = arg_6;

                    var_21 = (var_9 == "PIC" || var_9 == "FINAL");

                    seg042.load_decode_dax(out var_F, out var_B, arg_6, var_9 + gbl.game_area.ToString() + ".dax");

                    if (var_B == 0)
                    {
                        seg041.displayAndDebug("PIC not found", 0, 14);
                    }
                    else
                    {
                        var_11 = 0;
                        var_22 = 0;

                        var_27 = arg_0;

                        var_27.numFrames = var_F[var_11];
                        var_11++;
                        var_27.curFrame = 1;

                        var_23 = 0;

                        if (gbl.AnimationsOn == false && var_21 == true)
                        {
                            var_27.numFrames = 1;
                        }

                        var_28 = var_27.numFrames;

                        for (var_12 = 1; var_12 <= var_28; var_12++)
                        {
                            var_27.ptrs[var_12 - 1].field_0 = Sys.ArrayToInt(var_F, var_11);
                            var_11 += 4;

                            height = Sys.ArrayToShort(var_F, var_11);
                            var_11 += 2;

                            width = Sys.ArrayToShort(var_F, var_11);
                            var_11 += 2;

                            var_23++;

                            seg040.init_dax_block(out var_27.ptrs[var_12 - 1].field_4, masked, 1, width, height);

                            var_2C = var_27.ptrs[var_12 - 1].field_4;

                            var_2C.field_4 = Sys.ArrayToShort(var_F, var_11);
                            var_11 += 2;

                            var_2C.field_6 = Sys.ArrayToShort(var_F, var_11);
                            var_11 += 3;

                            System.Array.Copy(var_F, var_11, var_2C.field_9, 0, 8);
                            var_11 += 8;

                            var_20 = (short)((var_27.ptrs[var_12 - 1].field_4.bpp / 2) - 1);

                            if (var_21 == true)
                            {
                                if (var_12 == 1)
                                {
                                    mem = seg051.GetMem(var_20 + 1);

                                    System.Array.Copy(var_F, var_11, mem, 0, var_20 + 1);

                                    var_22 = 1;
                                }
                                else
                                {
                                    for( int i = 0; i < var_20; i++ )
                                    //for (var_1E = var_11; var_1E < (var_11 + var_20); var_1E++)
                                    //for (var_1E = var_11; var_1E <= (var_11 + var_20); var_1E++)
                                    {
                                        byte b = mem[i];
                                        var_F[var_11 + i] ^= b;
                                    }
                                }
                            }

                            seg040.turn_dax_to_videolayout(var_27.ptrs[var_12-1].field_4, 0, masked, var_11, var_F);

                            if ((masked & 1) > 0)
                            {
                                seg040.DaxBlockRecolor(var_27.ptrs[var_12-1].field_4, 0, 0, unk_16DDA, unk_16DCA);
                            }

                            var_11 += (short)(var_20 + 1);
                        }

                        var_27.numFrames = var_23;


                        if (var_21 == true && var_22 != 0)
                        {
                            seg051.FreeMem(var_20 + 1, mem);
                        }

                        seg051.FreeMem(var_B, var_F);
                        seg043.clear_keyboard();

                        if (gbl.AnimationsOn == true)
                        {
                            seg041.sub_1071A(0x28, 0, 0x18, 0);
                        }
                    }
                }
            }
        }


        internal static void DaxArrayFreeDaxBlocks(DaxArray arg_0)
        {
            for (gbl.byte_1DA71 = 1; gbl.byte_1DA71 <= arg_0.numFrames; gbl.byte_1DA71++)
            {
                seg040.free_dax_block(ref arg_0.ptrs[gbl.byte_1DA71 - 1].field_4);
            }

            arg_0.numFrames = 0;
            arg_0.curFrame = 0;

            gbl.byte_1D5A2 = string.Empty;
            gbl.byte_1D5B4 = 0x0FF;
        }


        internal static void head_body(byte body_id, byte head_id)
        {
            string var_2;

            seg051.Str(1, out var_2, 0, gbl.game_area);

            if (head_id != 0xff &&
                (gbl.current_head_id == 0xff || gbl.current_head_id != head_id))
            {
                seg040.load_dax(ref gbl.headX_dax, 0, 0, head_id, "HEAD" + var_2);

                if (gbl.headX_dax == null)
                {
                    seg041.displayAndDebug("head not found", 0, 14);
                }

                gbl.current_head_id = head_id;
            }

            if (body_id != 0xff &&
                (gbl.current_body_id == 0xff || gbl.current_body_id != body_id))
            {
                seg040.load_dax(ref gbl.bodyX_dax, 0, 0, body_id, "BODY" + var_2);
                if (gbl.bodyX_dax == null)
                {
                    seg041.displayAndDebug("body not found", 0, 14);
                }

                gbl.current_body_id = body_id;
            }

            seg043.clear_keyboard();
        }


        internal static void sub_706DC(bool arg_0, byte arg_2, byte arg_4)
        {
            if (arg_0 == true)
            {
                sub_7008D(gbl.headX_dax, arg_2, arg_4);
                sub_700AD(gbl.bodyX_dax, arg_2, arg_4);
            }
            else
            {
                sub_7000A(gbl.headX_dax, 0, arg_2, arg_4);
            }
        }


        internal static void Show3DSprite(DaxArray arg_0, byte arg_4)
        {
            DaxBlock var_46;

            if (arg_4 < 1 || arg_4 > 3)
            {
                seg051.Write(0, "Illegal range in Show3DSprite.", gbl.known01_02);
                seg051.WriteLn(gbl.known01_02);
                seg043.print_and_exit();
            }

            if (arg_0.ptrs[arg_4 - 1].field_4 != null)
            {
                var_46 = arg_0.ptrs[arg_4 - 1].field_4;
                seg040.sub_E353(gbl.overlayLines, arg_0.ptrs[arg_4 - 1].field_4, 1, 0, (short)(var_46.field_6 + 3 - 1), (short)(var_46.field_4 + 3 - 1));
                seg040.DrawOverlay();
            }
        }


        internal static void bigpic(byte block_id)
        {
            DaxArrayFreeDaxBlocks(gbl.byte_1D556);

            if (gbl.byte_1D5BA != block_id)
            {
                seg040.load_dax(ref gbl.word_1D5B6, 0, 0, block_id, "bigpic" + gbl.game_area.ToString());
                gbl.byte_1D5BA = block_id;
            }
        }


        internal static void sub_7087A()
        {
            seg037.draw8x8_04();
            seg040.draw_picture(gbl.word_1D5B6, 1, 1);
        }
    }
}
