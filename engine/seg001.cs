using Classes;

namespace engine
{
    public class seg001
    {
        internal static System.Threading.Thread EngineThread;

        public static void __SystemInit( )
        {
            EngineThread = System.Threading.Thread.CurrentThread;

            seg051.o_1_a_87_i_tpdos_idc_tpdos_l_tptv();
            seg049.__CRTInit();
            seg045.sub_13840();
            seg044.sub_121BF();
            seg039.config_game();
        }

        public static void PROGRAM()
        {
            seg044.sub_12194();

			if( gbl.gameFlag01 == true )
			{
				seg044.sub_120E0( gbl.word_188BC );
			}

            ovr012.sub_39054();

			if( gbl.gameFlag01 == false )
			{
				seg044.sub_12194();
			}

			seg044.sub_120E0( gbl.word_188BE );
            seg044.sub_12194();

			if( seg051.ParamStr( 1 ) != "STING" )
			{
				ovr002.title_screen();
			}

			gbl.word_1D5C0 = 0x0BB8;
            gbl.word_1D5C2 = 0;
            gbl.byte_1D5C4 = 0x44;
          
            gbl.byte_1AB06 = ovr027.displayInput( out gbl.unk_1AB07, 0, 0, 15, 10, 13, "Play Demo", "Curse of the Azure Bonds v1.3 " );
 
            gbl.word_1D5C0 = 0;
            gbl.word_1D5C2 = 0;
            gbl.byte_1D5C4 = 0;

			if( gbl.byte_1AB06 == 'D' )
			{
				gbl.inDemo = true;
			}

			if( seg051.ParamStr( 2 ) != gbl.byte_1EFA4 &&
				gbl.inDemo == false )
			{
				//ovr004.copy_protection();
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
					seg044.sub_120E0( gbl.word_188BC );
				}

				if( gbl.gameFlag01 == false )
				{
					seg044.sub_12194();
				}

				if( gbl.inDemo == false )
				{
					ovr018.startGameMenu();
				}

				if( gbl.gameFlag01 == true )
				{
					seg044.sub_120E0( gbl.word_188BC );
				}

				if( gbl.gameFlag01 == false )
				{
					seg044.sub_12194();
				}

				ovr031.sub_71165( gbl.byte_1D537, gbl.byte_1D536, gbl.byte_1D535, gbl.byte_1D534 );

				if( gbl.gameFlag01 == true )
				{
					seg044.sub_120E0( gbl.word_188BC );
				}

				if( gbl.gameFlag01 == false )
				{
					seg044.sub_12194();
				}

				ovr003.sub_29758();

				if( gbl.gameFlag01 == true )
				{
					seg044.sub_120E0( gbl.word_188BC );
				}

				if( gbl.gameFlag01 == false )
				{
					seg044.sub_12194();
				}

				ovr012.sub_396E5();

				if( gbl.inDemo == true ) 
				{
					ovr002.title_screen();
					seg043.clear_keyboard();

					gbl.word_1D5C0 = 0x3E8;
					gbl.word_1D5C2 = 0;
					gbl.byte_1D5C4 = 0x44;
				
					gbl.byte_1AB06 = ovr027.displayInput( out gbl.unk_1AB07, 0, 0, 15, 10, 13, "Play Demo", "Curse of the Azure Bonds v1.3 " );

					gbl.word_1D5C0 = 0;
					gbl.word_1D5C2 = 0;
					gbl.byte_1D5C4 = 0;

					gbl.inDemo = ( gbl.byte_1AB06 == 0x44 );

					if( seg051.ParamStr( 2 ) != gbl.byte_1EFA4 &&
						gbl.inDemo == false )
					{
						ovr004.copy_protection();
					}
				
					seg044.sub_120E0( gbl.word_188BE );
				}
			}
        }
    }
}