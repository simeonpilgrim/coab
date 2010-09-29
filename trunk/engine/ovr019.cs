using Classes;

namespace engine
{
    class ovr019
    {
        /// <summary>
        /// set a single pixel on the display in graphics modes
        /// </summary>
        /// <param name="colour">if bit 7 set, value is XOR'ed onto screen</param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        static void SetPixel(byte colour, ushort row, ushort column)
        {
            Display.SetPixel3(column, row, colour);
        }


        internal static byte GetPixel(ushort row, ushort column)
        {
            return Display.GetPixel(column, row);
        }


        internal static void sub_52068()
        {
            seg040.SetPaletteColor(15, 9);
            seg049.SysDelay(1);
            seg040.SetPaletteColor(9, 9);
        }

        class Struct_1ADFB
        {
            internal void Reset()
            {
                field_0 = 1;
                field_1 = 1;
                field_2 = 1;
                field_3 = 1;
                field_4 = 1;
            }

            internal byte field_0;
            internal byte field_1;
            internal byte field_2;
            internal byte field_3;
            internal byte field_4;

            internal byte this[int index]
            {
                get
                {
                    switch (index)
                    {
                        case 0: return field_0;
                        case 1: return field_1;
                        case 2: return field_2;
                        case 3: return field_3;
                        case 4: return field_4;
                        default:
                            throw new System.NotSupportedException();
                    }
                }
            }
        }

        static Struct_1ADFB[] unk_1ADFB = new Struct_1ADFB[3];

        internal static void sub_520B8(byte[] arg_0, int arg_4, int arg_6, ushort arg_8, ushort arg_A)
        {
            double var_12;
            double var_C;
            byte[] var_3;

            double d;

            var_3 = arg_0; //byte[3];

            gbl.byte_1ADFA = 0;


            unk_1ADFB[1] = new Struct_1ADFB();
            unk_1ADFB[0] = new Struct_1ADFB();
            unk_1ADFB[2] = new Struct_1ADFB();
            unk_1ADFB[0].Reset();
            unk_1ADFB[1].Reset();
            unk_1ADFB[2].Reset();

            for (int var_5 = 0; var_5 < 3; var_5++)
            {
                if (var_3[var_5] > 1)
                {
                    unk_1ADFB[var_5].field_0 = 15;
                }

                unk_1ADFB[var_5].field_1 = 15;

                if (var_3[var_5] > 1)
                {
                    unk_1ADFB[var_5].field_2 = (byte)(var_3[var_5] + 8);
                }

                unk_1ADFB[var_5].field_3 = var_3[var_5];

                unk_1ADFB[var_5].field_4 = 1;

                byte var_15 = (byte)(seg051.Random(20) + 25);

                if (gbl.byte_1ADFA < var_15)
                {
                    gbl.byte_1ADFA = var_15;
                }

                int var_16 = seg051.Random(5) + 5;
                int var_18 = var_16 + 15;

                var_C = seg051.Random__Real() * (System.Math.PI * 2.0);
                var_12 = seg051.Random__Real() * (System.Math.PI * 2.0);

                d = (seg051.Random(10) + 24) * System.Math.Sin(var_C) * System.Math.Sin(var_12);
                int var_1A = (int)d + arg_6;

                d = (seg051.Random(10) + 24) * System.Math.Cos(var_C) * System.Math.Sin(var_12);
                int var_1C = (int)d + arg_4;

                for (int var_4 = 1; var_4 <= 0x28; var_4++)
                {
                    Struct_1ADF6 var_21 = gbl.dword_1ADF6[var_4 + ((var_5) * 40) - 1];

                    var_C = seg051.Random__Real() * (System.Math.PI * 2.0);
                    var_12 = seg051.Random__Real() * (System.Math.PI * 2.0);

                    var_21.field_00 = arg_A;
                    var_21.field_02 = arg_8;
                    var_21.field_08 = (short)(var_21.field_00 << 5);
                    var_21.field_0A = (short)(var_21.field_02 << 5);

                    d = System.Math.Sin(var_C) * 16.0 * System.Math.Sin(var_12);

                    var_21.field_0C = (short)(var_1A + ((ushort)d));

                    d = System.Math.Cos(var_C) * 16.0 * System.Math.Sin(var_12);
                    var_21.field_0E = (short)(var_1C + ((ushort)d));

                    var_21.field_10 = 1;
                    var_21.field_12 = 1;

                    var_21.field_13 = (byte)(var_16 + seg051.Random(7) - 4);
                    var_21.field_14 = (byte)(var_18 + seg051.Random(11) - 6);
                    var_21.field_15 = (byte)(var_15 + seg051.Random(7));

                    /*HACK commented out this code as it does not make sense
                    var_21.field_16 = var_21.field_16;
                     */

                    var_21.field_11 = GetPixel(var_21.field_02, var_21.field_00);
                }
            }
        }


        static void sub_524F7(Struct_1ADF6[] arg_2, int arg_6)
        {
            int var_1 = arg_6 % 6;

            for (int var_3 = 1; var_3 <= 3; var_3++)
            {
                for (int var_2 = 1; var_2 <= 0x28; var_2++)
                {
                    Struct_1ADF6 var_7 = arg_2[(var_2 - 1) + ((var_3 - 1) * 40)];

                    var_7.field_08 = var_7.field_0C;
                    var_7.field_0A = var_7.field_0E;
                    var_7.field_04 = (short)(var_7.field_08 / 0x20);
                    var_7.field_06 = (short)(var_7.field_0A / 0x20);

                    if (var_1 == 0)
                    {
                        var_7.field_0E += 1;
                    }

                    if (var_7.field_0C > 0)
                    {
                        var_7.field_0C -= 1;
                    }
                    else if (var_7.field_0C < 0)
                    {
                        var_7.field_0C += 1;
                    }

                    if (var_7.byteArray_11(var_7.field_10) < arg_6 &&
                        var_7.field_10 < 5)
                    {
                        var_7.field_10 += 1;
                    }
                }
            }

            for (int var_2 = 0; var_2 < 40; var_2++)
            {
                for (int var_3 = 0; var_3 < 3; var_3++)
                {
                    Struct_1ADF6 var_7 = arg_2[var_2 + (var_3 * 40)];

                    if (var_7.field_02 > 8 &&
                        var_7.field_02 < 0x41)
                    {
                        SetPixel(var_7.field_11, var_7.field_02, var_7.field_00);
                    }
                }
            }

            for (int var_2 = 0; var_2 < 40; var_2++)
            {
                for (int var_3 = 0; var_3 < 3; var_3++)
                {
                    Struct_1ADF6 var_7 = arg_2[var_2 + (var_3 * 40)];

                    var_7.field_00 = (ushort)var_7.field_04;
                    var_7.field_02 = (ushort)var_7.field_06;

                    if (var_7.field_02 > 8 &&
                        var_7.field_02 < 0x41)
                    {
                        var_7.field_11 = GetPixel(var_7.field_02, var_7.field_00);
                    }
                }
            }

            for (int var_2 = 0; var_2 < 40; var_2++)
            {
                for (int var_3 = 0; var_3 < 3; var_3++)
                {
                    Struct_1ADF6 var_7 = arg_2[var_2 + (var_3 * 40)];

                    if (var_7.field_02 > 8 &&
                        var_7.field_02 < 0x41)
                    {
                        SetPixel(unk_1ADFB[var_3][var_7.field_10 - 1], var_7.field_02, var_7.field_00);
                    }
                }
            }

            if (arg_2[0].field_10 == 2)
            {
                sub_52068();
            }
        }


        internal static void sub_5279B(byte[] arg_0)
        {
            byte[] var_3 = arg_0;
            int var_17 = gbl.byte_1ADFA + 1;

            for (int var_6 = 1; var_6 <= var_17; var_6++)
            {
                sub_524F7(gbl.dword_1ADF6, var_6);
            }

            for (int var_4 = 0; var_4 < 40; var_4++)
            {
                for (int var_5 = 0; var_5 < 3; var_5++)
                {
                    Struct_1ADF6 var_20 = gbl.dword_1ADF6[var_4 + (var_5 * 40)];

                    if (var_20.field_02 > 8 &&
                        var_20.field_02 < 0x41)
                    {
                        SetPixel(var_20.field_11, var_20.field_02, var_20.field_00);
                    }
                }
            }
        }


        internal static void endgame_5285E(byte arg_0, short arg_2, ref short arg_4, ref ushort arg_8, ref ushort arg_C, ref ushort arg_10) /* sub_5285E */
        {
            short var_D;
            byte var_B;
            short var_A;
            ushort var_8;
            ushort var_6;
            ushort var_4;
            ushort var_2;

            if (arg_0 != 0)
            {
                sub_52068();
            }

            var_6 = (ushort)(arg_10 << 5);
            var_8 = (ushort)(arg_C << 5);


            if (arg_0 != 0 &&
                arg_C > 8 &&
                arg_C < 0x41)
            {
                var_B = GetPixel(arg_C, arg_10);
            }
            else
            {
                /*HACK - var_B is only used when the above logic is the same, so this is to remove the error */
                var_B = 0;
            }

            var_D = arg_2;

            for (var_A = 1; var_A <= var_D; var_A++)
            {
                var_6 += arg_8;
                var_8 += (ushort)(arg_4 + 1);

                var_2 = (ushort)(var_6 / 0x20);
                var_4 = (ushort)(var_8 / 0x20);

                arg_4 += 1;

                if (arg_0 != 0 &&
                    arg_C > 8 &&
                    arg_C < 0x41)
                {
                    SetPixel(var_B, arg_C, arg_10);

                    var_B = GetPixel(var_4, var_2);

                    SetPixel((byte)(seg051.Random(7) + 8), var_4, var_2);
                }

                seg049.SysDelay(0x0F);

                arg_10 = var_2;
                arg_C = var_4;
            }

            if (arg_0 != 0 &&
                arg_C > 8 &&
                arg_C < 0x41)
            {
                SetPixel(var_B, arg_C, arg_10);
            }

            var_6 += arg_8;
            var_8 += (ushort)(arg_4 + 1);

            var_2 = (ushort)(var_6 / 0x20);
            var_4 = (ushort)(var_8 / 0x20);

            arg_4 += 1;
            arg_10 = var_2;
            arg_C = var_4;
        }


        internal static void endgame_529F4() /* sub_529F4 */
        {
            gbl.dword_1ADF6 = new Struct_1ADF6[120];
            for (int i = 0; i < 120; i++)
            {
                gbl.dword_1ADF6[i] = new Struct_1ADF6();
            }
            gbl.byte_1AE0A = 0;

            do
            {
                if (gbl.byte_1AE0A == 0 &&
                    seg051.Random(10000) < 1)
                {
                    seg051.FillChar(1, 3, gbl.unk_1AE0B);
                    gbl.byte_1AE1B = seg051.Random((byte)2);

                    for (byte i = 0; i < gbl.byte_1AE1B; i++)
                    {
                        gbl.unk_1AE0B[i] = (byte)(seg051.Random(5) + 2);
                    }

                    gbl.word_1AE0F = 65;
                    gbl.word_1AE11 = 65;
                    gbl.word_1AE13 = (ushort)(seg051.Random(20) + 35);

                    gbl.word_1AE15 = (short)(-(seg051.Random(5) + 50));

                    gbl.word_1AE19 = gbl.word_1AE15;
                    gbl.word_1AE17 = gbl.word_1AE13;

                    endgame_5285E(0, 0x3C, ref gbl.word_1AE15, ref gbl.word_1AE13, ref gbl.word_1AE11, ref gbl.word_1AE0F);
                    sub_520B8(gbl.unk_1AE0B, gbl.word_1AE15, gbl.word_1AE13, gbl.word_1AE11, gbl.word_1AE0F);

                    gbl.word_1AE13 = gbl.word_1AE17;
                    gbl.word_1AE15 = gbl.word_1AE19;

                    gbl.word_1AE0F = 0x41;
                    gbl.word_1AE11 = 0x41;

                    endgame_5285E(1, 0x3C, ref gbl.word_1AE15, ref gbl.word_1AE13, ref gbl.word_1AE11, ref gbl.word_1AE0F);

                    sub_5279B(gbl.unk_1AE0B);/*TODO - extra params - gbl.word_1AE15, gbl.word_1AE13, gbl.word_1AE11, gbl.word_1AE0F );*/

                    if (seg049.KEYPRESSED() == true)
                    {
                        gbl.byte_1AE0A = seg049.READKEY();
                    }
                }
            } while (gbl.byte_1AE0A == 0);

            gbl.dword_1ADF6 = null;
        }


        internal static void ShowAnimation(int num_loops, byte block_id, short row_y, short col_x) // sub_52B79
        {
            int loop_count = 0;
            int start_time = seg041.time01();

            DaxArray animation = new DaxArray();

            ovr030.load_pic_final(ref animation, 2, block_id, "PIC");
            seg040.OverlayBounded(animation.frames[0].picture, 0, 0, row_y - 1, col_x - 1);
            seg040.DrawOverlay();

            do
            {
                ovr030.DrawMaybeOverlayed(animation.CurrentPicture(), true, row_y, col_x);
                int current_time = seg041.time01();

                int delay = animation.CurrentDelay() * (gbl.game_speed_var + 3);

                if ((current_time - start_time) > delay)
                {
                    animation.curFrame += 1;

                    if (animation.curFrame > animation.numFrames)
                    {
                        animation.curFrame = 1;
                        loop_count++;
                    }

                    start_time = current_time;
                }
            } while (loop_count != num_loops);

            ovr030.DaxArrayFreeDaxBlocks(animation);
        }

        static string aTyranthraxusSp = "Tyranthraxus' spirit coalesces over the slain ";
        static string aStormGiant_You = "storm giant. 'You have defeated me. Were it not for ";
        static string aTheAmuletOfLyt = "the Amulet of Lythander, I could possess you and rob ";
        static string aYouOfYourVicto = "you of your victory. Still I can escape through the pool.";

        static string aAsYouReachForT = "As you reach for the Pool of Radiance, he cries ";
        static string aOutKeepTheGaun = "out, 'Keep the Gauntlet of Moander away from there, you ";
        static string aWillUnleashDan = "will unleash dangerous energies. Stay back!' As the ";
        static string aGauntletContac = "gauntlet contacts the pool, it contracts and shatters it.";
        static string aIAmTrappedWith = "'I am trapped without escape, you have succeeded ";
        static string aWhereArmiesHav = "where armies have not. Gloat while you may, Tyranthraxus ";
        static string aIsSlainThisDay = "is slain this day.' Before your eyes he crumbles into ";
        static string aNothingness_ = "nothingness.";
        static string aYouAreCertainH = "You are certain he is destroyed because your ";
        static string aFinalBondFades = "final bond fades away. The Curse of the Azure Bonds ";
        static string aHasFinallyBeen = "has finally been lifted from you! You are free at ";
        static string aLast = "last!";
        static string aTheKnightsOfMy = "The Knights of Myth Drannor rush in, '";
        static string aCongratulati_0 = "Congratulations, you have destroyed the Flamed One. ";
        static string aWithThePowerOf = "With the power of Elminster, let us take you from ";
        static string aThisFoulPlaceT = "this  foul place, to a fine feast.'";
        static string aYouAreTeleport = "You are teleported to Shadowdale, where festivities ";
        static string aHaveAlreadyBeg = "have already begun. A huge cheer goes up at your arrival. ";
        static string aGharriAndNacac = "Gharri and Nacacia, arm in arm, yell congratulations ";
        static string aFromTheNearbyS = "from the nearby stands. 'You have won!'";



        internal static void end_game_text()
        {
            gbl.last_game_state = gbl.game_state;
            gbl.game_state = GameState.EndGame;

            seg041.press_any_key(aTyranthraxusSp, true, 0, 10, TextRegion.NormalBottom);
            seg041.press_any_key(aStormGiant_You, false, 0, 10, TextRegion.NormalBottom);
            seg041.press_any_key(aTheAmuletOfLyt, false, 0, 10, TextRegion.NormalBottom);
            seg041.press_any_key(aYouOfYourVicto, false, 0, 10, TextRegion.NormalBottom);
            seg041.displayAndDebug("Press any key to continue.", 0, 13);

            seg041.press_any_key(aAsYouReachForT, true, 0, 10, TextRegion.NormalBottom);
            seg041.press_any_key(aOutKeepTheGaun, false, 0, 10, TextRegion.NormalBottom);
            seg041.press_any_key(aWillUnleashDan, false, 0, 10, TextRegion.NormalBottom);
            seg041.press_any_key(aGauntletContac, false, 0, 10, TextRegion.NormalBottom);

            ShowAnimation(1, 0x4a, 3, 3);

            seg041.displayAndDebug("Press any key to continue.", 0, 13);
            ovr027.ClearPromptArea();

            seg041.press_any_key(aIAmTrappedWith, true, 0, 10, TextRegion.NormalBottom);
            seg041.press_any_key(aWhereArmiesHav, false, 0, 10, TextRegion.NormalBottom);
            seg041.press_any_key(aIsSlainThisDay, false, 0, 10, TextRegion.NormalBottom);
            seg041.press_any_key(aNothingness_, false, 0, 10, TextRegion.NormalBottom);

            ShowAnimation(1, 0x4B, 3, 3);

            seg041.displayAndDebug("Press any key to continue.", 0, 13);
            ovr027.ClearPromptArea();

            seg041.press_any_key(aYouAreCertainH, true, 0, 10, TextRegion.NormalBottom);
            seg041.press_any_key(aFinalBondFades, false, 0, 10, TextRegion.NormalBottom);
            seg041.press_any_key(aHasFinallyBeen, false, 0, 10, TextRegion.NormalBottom);
            seg041.press_any_key(aLast, false, 0, 10, TextRegion.NormalBottom);

            gbl.area_ptr.picture_fade = 1;

            ShowAnimation((10 - gbl.game_speed_var) * 2, 0x4d, 3, 3);

            gbl.area_ptr.picture_fade = 0;

            ovr030.head_body(0x41, 0x41);
            ovr030.draw_head_and_body(true, 3, 3);

            seg041.press_any_key(aTheKnightsOfMy, true, 0, 10, TextRegion.NormalBottom);
            seg041.press_any_key(aCongratulati_0, false, 0, 10, TextRegion.NormalBottom);
            seg041.press_any_key(aWithThePowerOf, false, 0, 10, TextRegion.NormalBottom);
            seg041.press_any_key(aThisFoulPlaceT, false, 0, 10, TextRegion.NormalBottom);

            seg041.displayAndDebug("Press any key to continue.", 0, 13);
            ovr027.ClearPromptArea();
            ovr030.load_bigpic(0x7A);

            ovr030.draw_bigpic();

            seg041.press_any_key(aYouAreTeleport, true, 0, 10, TextRegion.NormalBottom);
            seg041.press_any_key(aHaveAlreadyBeg, false, 0, 10, TextRegion.NormalBottom);
            seg041.press_any_key(aGharriAndNacac, false, 0, 10, TextRegion.NormalBottom);
            seg041.press_any_key(aFromTheNearbyS, false, 0, 10, TextRegion.NormalBottom);
            endgame_529F4();

            gbl.game_state = gbl.last_game_state;
            ovr025.load_pic();
        }
    }
}
