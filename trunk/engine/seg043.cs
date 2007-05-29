using Classes;

namespace engine
{
    public class seg043
    {
        public static void print_and_exit()
        {
            gbl.soundType = gbl.soundTypeBackup;

            seg044.sub_120E0(gbl.word_188BC);

            if (gbl.printCommands == true)
            {
                seg051.Close(gbl.unk_1EE9A);
            }

            ovr012.keyboardStatus_0417 = gbl.byte_1EFBA;

            seg001.EngineThread.Abort();
        }


        static void display_players_affects(Player player)
        {
            Affect affect;

            affect = player.affect_ptr;

            while (affect != null)
            {
                seg051.Write(0, "who: ", gbl.unk_1EE9A);
                seg051.Write(0, player.name, gbl.unk_1EE9A);
                seg051.Write(0, "  sp#: ", gbl.unk_1EE9A);
                seg051.Write(0, (int)affect.type, gbl.unk_1EE9A);
                seg051.WriteLn(gbl.unk_1EE9A);

                affect = affect.next;
            }
        }

        static Set unk_11F12 = new Set(0x0001, new byte[] { 03 });

        internal static byte GetInputKey()
        {
            byte var_2;

            if (gbl.inDemo == true)
            {
                if (seg049.KEYPRESSED() == true)
                {
                    var_2 = seg049.READKEY();
                }
                else
                {
                    var_2 = 0;
                }
            }
            else
            {
                var_2 = seg049.READKEY();
            }

            if (var_2 == 0x13)
            {
                if (gbl.soundType != SoundType.None)
                {
                    gbl.soundTypeBackup = gbl.soundType;
                    seg044.sub_120E0(gbl.word_188BE);
                    gbl.soundType = SoundType.None;
                }
                else
                {
                    if (unk_11F12.MemberOf((byte)gbl.soundTypeBackup))
                    {
                        gbl.soundType = gbl.soundTypeBackup;
                        seg044.sub_120E0(gbl.word_188C0);
                    }
                }
            }

            if (seg051.ParamStr(1) == "STING")
            {
                if (var_2 == 3)
                {
                    print_and_exit();
                }
            }

            if (var_2 != 0)
            {
                while (seg049.KEYPRESSED() == true)
                {
                    var_2 = seg049.READKEY();
                }
            }

            return var_2;
        }

        public static void DumpPlayerAffects()
        {
            Player player;

            if (gbl.printCommands == true)
            {
                player = gbl.player_next_ptr;

                while (player != null)
                {
                    display_players_affects(player);
                    player = player.next_player;
                }
            }
        }

        public static void ToggleCommandDebugging()
        {
            
            gbl.printCommands = !gbl.printCommands;

            if (gbl.printCommands == true)
            {
                gbl.unk_1EE9A.Assign("debug.txt", Text.AssignType.Write);

                seg051.Write(0, System.DateTime.Now.ToString(), gbl.unk_1EE9A);
                seg051.WriteLn(gbl.unk_1EE9A);
            }
            else
            {
                seg051.Close(gbl.unk_1EE9A);
            }
        }

        internal static void clear_keyboard()
        {
            while (seg049.KEYPRESSED() == true)
            {
                GetInputKey();
            }
        }


        internal static void clear_one_keypress()
        {
            if (seg049.KEYPRESSED() == true)
            {
                GetInputKey();
            }
        }
    }
}
