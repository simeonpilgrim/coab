using Classes;

namespace engine
{
    class seg039
    {
        internal static void config_game( )
        {
            string unk_1B1C4;
            string unk_1B1BB;

            seg046.FSplit(out unk_1B1C4, out unk_1B1BB, out gbl.unk_1B21A, seg046.getCurrentDirectory("*.exe"));
            
			gbl.unk_1B26A = gbl.unk_1B21A;
			


            gbl.byte_1B2BA = gbl.unk_1B26A[0];
   
            char option = 'N'; /* force None */

            if (option == 'P') // PC Speaker
			{
				gbl.soundType = SoundType.PC;
			}
			else // No Sounds
			{
				gbl.soundType = SoundType.None;
			}

            option = 'F'; /* force normal play (vs. demo) */

            if (option == 'F')
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
