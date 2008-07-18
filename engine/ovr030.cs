using Classes;
using Logging;

namespace engine
{
    class ovr030
    {
        static byte[] unk_16DAA = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        static byte[] unk_16DBA = { 12, 12, 12, 12, 4, 5, 6, 7, 12, 12, 10, 12, 12, 12, 14, 12 };
        static byte[] unk_16DCA = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
        static byte[] unk_16DDA = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 0, 14, 15 };

        internal static void sub_7000A(DaxBlock dax_block, bool useOverlay, int rowY, int colX)
        {
            if (dax_block != null)
            {
                if (gbl.area_ptr.field_3FE > 0 ||
                    useOverlay == true)
                {
                    if (gbl.area_ptr.field_3FE > 0)
                    {
                        seg040.DaxBlockRecolor(dax_block, 1, 0, unk_16DBA, unk_16DAA);
                    }

                    seg040.OverlayBounded(dax_block, 0, 0, rowY - 1, colX - 1);
                    seg040.DrawOverlay();
                }
                else
                {
                    seg040.draw_picture(dax_block, rowY, colX, 0);
                }
            }
        }


        internal static void load_pic_final(ref DaxArray daxArray, byte masked, byte block_id, string file_name)
        {
            if (file_name != gbl.lastDaxFile ||
                block_id != gbl.lastDaxBlockId)
            {
                if (block_id != 0xff)
                {
                    if (gbl.AnimationsOn == true)
                    {
                        seg041.displaySpaceChar(0x28, 0, 0x18, 0);
                        seg041.displayString("Loading...Please Wait", 0, 10, 0x18, 0);
                    }

                    DaxArrayFreeDaxBlocks(daxArray);

                    gbl.lastDaxFile = file_name;
                    gbl.lastDaxBlockId = block_id;

                    bool is_pic_or_final = (file_name == "PIC" || file_name == "FINAL");

                    short uncompressed_size;
                    byte[] uncompressed_data;

                    seg042.load_decode_dax(out uncompressed_data, out uncompressed_size, block_id, file_name + gbl.game_area.ToString() + ".dax");

                    if (uncompressed_size == 0)
                    {
                        seg041.displayAndDebug("PIC not found", 0, 14);
                    }
                    else
                    {
                        int src_offset = 0;
                        bool allocated_first_frame = false;

                        daxArray.numFrames = uncompressed_data[src_offset];
                        src_offset++;
                        daxArray.curFrame = 1;

                        byte frames_count = 0; // kind of pointless...

                        if (gbl.AnimationsOn == false && is_pic_or_final == true)
                        {
                            daxArray.numFrames = 1;
                        }

                        byte[] first_frame_ega_layout = null;

                        for (int frame = 0; frame < daxArray.numFrames; frame++)
                        {
                            daxArray.frames[frame].delay = Sys.ArrayToInt(uncompressed_data, src_offset);
                            src_offset += 4;

                            short height = Sys.ArrayToShort(uncompressed_data, src_offset);
                            src_offset += 2;

                            short width = Sys.ArrayToShort(uncompressed_data, src_offset);
                            src_offset += 2;

                            frames_count++;

                            seg040.init_dax_block(out daxArray.frames[frame].picture, masked, 1, width, height);

                            DaxBlock dax_block = daxArray.frames[frame].picture;

                            dax_block.field_4 = Sys.ArrayToShort(uncompressed_data, src_offset);
                            src_offset += 2;

                            dax_block.field_6 = Sys.ArrayToShort(uncompressed_data, src_offset);
                            src_offset += 3;

                            System.Array.Copy(uncompressed_data, src_offset, dax_block.field_9, 0, 8);
                            src_offset += 8;

                            int ega_encoded_size = (daxArray.frames[frame].picture.bpp / 2) - 1;

                            if (is_pic_or_final == true)
                            {
                                if (frame == 0)
                                {
                                    first_frame_ega_layout = seg051.GetMem(ega_encoded_size + 1);

                                    System.Array.Copy(uncompressed_data, src_offset, first_frame_ega_layout, 0, ega_encoded_size + 1);

                                    allocated_first_frame = true;
                                }
                                else
                                {
                                    for (int i = 0; i < ega_encoded_size; i++)
                                    {
                                        byte b = first_frame_ega_layout[i];
                                        uncompressed_data[src_offset + i] ^= b;
                                    }
                                }
                            }

                            seg040.turn_dax_to_videolayout(daxArray.frames[frame].picture, 0, masked, src_offset, uncompressed_data);

                            if ((masked & 1) > 0)
                            {
                                seg040.DaxBlockRecolor(daxArray.frames[frame].picture, 0, 0, unk_16DDA, unk_16DCA);
                            }

                            src_offset += ega_encoded_size + 1;
                        }

                        daxArray.numFrames = frames_count; // also pointless


                        if (is_pic_or_final == true && allocated_first_frame == true)
                        {
                            //seg051.FreeMem(var_20 + 1, mem);
                        }

                        seg051.FreeMem(uncompressed_size, uncompressed_data);
                        seg043.clear_keyboard();

                        if (gbl.AnimationsOn == true)
                        {
                            seg041.displaySpaceChar(0x28, 0, 0x18, 0);
                        }
                    }
                }
            }
        }


        internal static void DaxArrayFreeDaxBlocks(DaxArray arg_0)
        {
            for (gbl.byte_1DA71 = 1; gbl.byte_1DA71 <= arg_0.numFrames; gbl.byte_1DA71++)
            {
                seg040.free_dax_block(ref arg_0.frames[gbl.byte_1DA71 - 1].picture);
            }

            arg_0.numFrames = 0;
            arg_0.curFrame = 0;

            gbl.lastDaxFile = string.Empty;
            gbl.lastDaxBlockId = 0x0FF;
        }


        internal static void head_body(byte body_id, byte head_id)
        {
            string text = gbl.game_area.ToString();

            if (head_id != 0xff &&
                (gbl.current_head_id == 0xff || gbl.current_head_id != head_id))
            {
                seg040.load_dax(ref gbl.headX_dax, 0, 0, head_id, "HEAD" + text);

                if (gbl.headX_dax == null)
                {
                    seg041.displayAndDebug("head not found", 0, 14);
                }

                gbl.current_head_id = head_id;
            }

            if (body_id != 0xff &&
                (gbl.current_body_id == 0xff || gbl.current_body_id != body_id))
            {
                seg040.load_dax(ref gbl.bodyX_dax, 0, 0, body_id, "BODY" + text);
                if (gbl.bodyX_dax == null)
                {
                    seg041.displayAndDebug("body not found", 0, 14);
                }

                gbl.current_body_id = body_id;
            }

            seg043.clear_keyboard();
        }


        internal static void draw_head_and_body(bool draw_body, byte rowY, byte colX) /* sub_706DC */
        {
            if (draw_body == true)
            {
                sub_7000A(gbl.headX_dax, false, rowY, colX);
                sub_7000A(gbl.bodyX_dax, false, rowY + 5, colX);
            }
            else
            {
                sub_7000A(gbl.headX_dax, false, rowY, colX);
            }
        }


        internal static void Show3DSprite(DaxArray arg_0, int sprite_index)
        {
            if (sprite_index < 1 || sprite_index > 3)
            {
                Logger.LogAndExit("Illegal range in Show3DSprite. {0}", sprite_index);
            }

            if (arg_0.frames[sprite_index - 1].picture != null)
            {
                DaxBlock var_46 = arg_0.frames[sprite_index - 1].picture;
                seg040.OverlayBounded(arg_0.frames[sprite_index - 1].picture, 1, 0, var_46.field_6 + 3 - 1, var_46.field_4 + 3 - 1);
                seg040.DrawOverlay();
            }
        }


        internal static void load_bigpic(byte block_id) /* bigpic */
        {
            DaxArrayFreeDaxBlocks(gbl.byte_1D556);

            if (gbl.bigpic_block_id != block_id)
            {
                seg040.load_dax(ref gbl.bigpic_dax, 0, 0, block_id, "bigpic" + gbl.game_area.ToString());
                gbl.bigpic_block_id = block_id;
            }
        }


        internal static void draw_bigpic() /* sub_7087A */
        {
            seg037.draw8x8_04();
            seg040.draw_picture(gbl.bigpic_dax, 1, 1, 0);
        }
    }
}
