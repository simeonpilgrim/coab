using Classes;

namespace engine
{
    public class seg001
    {
        internal static System.Threading.Thread EngineThread;

        public delegate void VoidDelegate();
        static VoidDelegate EngineStoppedCallback;

        internal static void EngineStop()
        {
            EngineStoppedCallback();
            EngineThread.Abort();
        }

        public static void __SystemInit(VoidDelegate stoppedCallback)
        {
            EngineThread = System.Threading.Thread.CurrentThread;
            EngineStoppedCallback = stoppedCallback;

            ConfigGame();
        }

        internal static void ConfigGame()
        {
            gbl.exe_path = System.IO.Directory.GetCurrentDirectory();
            gbl.data_path = gbl.exe_path;

            seg044.SoundInit();

            if (true) // PC Speaker //TODO make this configured
            {
                gbl.soundType = SoundType.PC;
            }
            else // No Sounds
            {
                gbl.soundType = SoundType.None;
            }
        }

        public static void PROGRAM()
        {
            /* Memory Init - Start */
            gbl.CombatMap = new Struct_1C9CD[gbl.MaxCombatantCount + 1]; /* God damm 1-n arrays */
            for (int i = 0; i <= gbl.MaxCombatantCount; i++)
            {
                gbl.CombatMap[i] = new Struct_1C9CD();
            }

            gbl.SortedCombatantList = new SortedCombatant[gbl.MaxSortedCombatantCount];
            for (int i = 0; i < gbl.MaxSortedCombatantCount; i++)
            {
                gbl.SortedCombatantList[i] = new SortedCombatant();
            }
            /* Memory Init - End */

            if (gbl.soundFlag01 == true)
            {
                seg044.sound_sub_120E0(Sound.sound_FF);
            }

            ovr012.init_values_a();

            seg044.sound_sub_120E0(Sound.sound_0);

            if (Cheats.skip_title_screen == false)
            {
                ovr002.title_screen();
            }

            gbl.displayInputCentiSecondWait = 3000;
            gbl.displayInputTimeoutValue = 'D';

            bool dummyBool;
            char inputKey = ovr027.displayInput(out dummyBool, false, 0, 15, 10, 13, "Play Demo", "Curse of the Azure Bonds v1.3 ");

            gbl.displayInputCentiSecondWait = 0;
            gbl.displayInputTimeoutValue = '\0';

            if (inputKey == 'D')
            {
                gbl.inDemo = true;
            }

            if (Cheats.skip_copy_protection == false &&
                gbl.inDemo == false)
            {
                ovr004.copy_protection();
            }

            while (true)
            {
                if (gbl.inDemo == true)
                {
                    gbl.game_area = 1;
                    gbl.game_speed_var = 9;
                }
                else
                {
                    gbl.game_area = 2;
                }

                if (gbl.soundFlag01 == true)
                {
                    seg044.sound_sub_120E0(Sound.sound_FF);
                }

                if (gbl.inDemo == false)
                {
                    ovr018.startGameMenu();
                }

                if (gbl.soundFlag01 == true)
                {
                    seg044.sound_sub_120E0(Sound.sound_FF);
                }

                if (gbl.soundFlag01 == true)
                {
                    seg044.sound_sub_120E0(Sound.sound_FF);
                }

                ovr003.sub_29758();

                if (gbl.soundFlag01 == true)
                {
                    seg044.sound_sub_120E0(Sound.sound_FF);
                }

                ovr012.init_values_b();

                if (gbl.inDemo == true)
                {
                    ovr002.title_screen();
                    seg043.clear_keyboard();

                    gbl.displayInputCentiSecondWait = 1000;
                    gbl.displayInputTimeoutValue = 'D';

                    inputKey = ovr027.displayInput(out dummyBool, false, 0, 15, 10, 13, "Play Demo", "Curse of the Azure Bonds v1.3 ");

                    gbl.displayInputCentiSecondWait = 0;
                    gbl.displayInputTimeoutValue = '\0';

                    gbl.inDemo = (inputKey == 'D');

                    if (Cheats.skip_copy_protection == false &&
                        gbl.inDemo == false)
                    {
                        ovr004.copy_protection();
                    }

                    seg044.sound_sub_120E0(Sound.sound_0);
                }
            }
        }
    }
}
