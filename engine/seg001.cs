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

            seg044.sound_sub_121BF();
            seg039.config_game();
        }

        public static void PROGRAM()
        {
            /* Memory Init - Start */
            gbl.CombatMap = new gbl.Struct_1C9CD[gbl.MaxCombatantCount+1]; /* God damm 1-n arrays */
            for (int i = 0; i <= gbl.MaxCombatantCount; i++)
            {
                gbl.CombatMap[i] = new gbl.Struct_1C9CD();
            }
            
            gbl.SortedCombatantList = new SortedCombatant[gbl.MaxSortedCombatantCount];
            for (int i = 0; i < gbl.MaxSortedCombatantCount; i++)
            {
                gbl.SortedCombatantList[i] = new SortedCombatant();
            }
            /* Memory Init - End */

            seg044.sound_sub_12194();

			if( gbl.gameFlag01 == true )
			{
				seg044.sound_sub_120E0( gbl.sound_FF_188BC );
			}

            ovr012.init_values_a();

			if( gbl.gameFlag01 == false )
			{
				seg044.sound_sub_12194();
			}

			seg044.sound_sub_120E0( gbl.sound_0_188BE );
            seg044.sound_sub_12194();

			if( Cheats.skip_title_screen == false )
			{
				ovr002.title_screen();
			}

			gbl.displayInputCentiSecondWait = 3000;
            gbl.displayInputTimeoutValue = 'D';
          
            gbl.byte_1AB06 = ovr027.displayInput( out gbl.unk_1AB07, false, 0, 15, 10, 13, "Play Demo", "Curse of the Azure Bonds v1.3 " );
 
            gbl.displayInputCentiSecondWait = 0;
            gbl.displayInputTimeoutValue = '\0';

			if( gbl.byte_1AB06 == 'D' )
			{
				gbl.inDemo = true;
			}

			if( Cheats.skip_copy_protection == false &&
				gbl.inDemo == false )
			{
				ovr004.copy_protection();
			}

			while( true )
			{
				if( gbl.inDemo == true )
				{
					gbl.game_area = 1;
					gbl.game_speed_var = 9;
				}
				else
				{
					gbl.game_area = 2;
				}

				if( gbl.gameFlag01 == true )
				{
					seg044.sound_sub_120E0( gbl.sound_FF_188BC );
				}

				if( gbl.gameFlag01 == false )
				{
					seg044.sound_sub_12194();
				}

				if( gbl.inDemo == false )
				{
					ovr018.startGameMenu();
				}

				if( gbl.gameFlag01 == true )
				{
					seg044.sound_sub_120E0( gbl.sound_FF_188BC );
				}

				if( gbl.gameFlag01 == false )
				{
					seg044.sound_sub_12194();
				}

				if( gbl.gameFlag01 == true )
				{
					seg044.sound_sub_120E0( gbl.sound_FF_188BC );
				}

				if( gbl.gameFlag01 == false )
				{
					seg044.sound_sub_12194();
				}

				ovr003.sub_29758();

				if( gbl.gameFlag01 == true )
				{
					seg044.sound_sub_120E0( gbl.sound_FF_188BC );
				}

				if( gbl.gameFlag01 == false )
				{
					seg044.sound_sub_12194();
				}

				ovr012.init_values_b();

				if( gbl.inDemo == true ) 
				{
					ovr002.title_screen();
					seg043.clear_keyboard();

					gbl.displayInputCentiSecondWait = 1000;
					gbl.displayInputTimeoutValue = 'D';
				
					gbl.byte_1AB06 = ovr027.displayInput( out gbl.unk_1AB07, false, 0, 15, 10, 13, "Play Demo", "Curse of the Azure Bonds v1.3 " );

					gbl.displayInputCentiSecondWait = 0;
                    gbl.displayInputTimeoutValue = '\0';

					gbl.inDemo = ( gbl.byte_1AB06 == 0x44 );

                    if (Cheats.skip_copy_protection == false &&
                        gbl.inDemo == false)
                    {
                        ovr004.copy_protection();
                    }
				
					seg044.sound_sub_120E0( gbl.sound_0_188BE );
				}
			}
        }
    }
}
