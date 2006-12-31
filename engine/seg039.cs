using Classes;

namespace engine
{
    class seg039
    {
        internal static void config_game( )
        {
            seg042.check_overlay_file();

            string unk_1B1C4;
            string unk_1B1BB;

            seg046.FSplit(out unk_1B1C4, out unk_1B1BB, out gbl.unk_1B21A, seg046.getCurrentDirectory("*.exe"));
            
			if( gbl.unk_1B21A[0] < 'C' )
			{
				if( gbl.unk_1B21A[0] == 'A' )
				{
					gbl.unk_1B26A = "B:\\";
				}
				else
				{
					gbl.unk_1B26A = "A:\\";
				}
			}
			else
			{
				gbl.unk_1B26A = gbl.unk_1B21A;
			}


            gbl.byte_1B2BA = gbl.unk_1B26A[0];
   
            gbl.byte_1AFE6 = 'P'; /* force PC Speaker */

			if( gbl.byte_1AFE6 == 'T' ) // Tandy
			{
				gbl.soundType = SoundType.Tandy;
			}
			else if( gbl.byte_1AFE6 == 'P' ) // PC Speaker
			{
				gbl.soundType = SoundType.PC;
			}
			else // No Sounds
			{
				gbl.soundType = SoundType.None;
			}

            gbl.byte_1AFE6 = 'F'; /* force normal play (vs. demo) */

			if( gbl.byte_1AFE6 == 'F' )
			{
				gbl.DisplayFullTitleScreen = true;
			}
			else
			{
				gbl.DisplayFullTitleScreen = false;
			}
        }
    }
}
