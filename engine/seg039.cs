using Classes;

namespace engine
{
    class seg039
    {
		static Set unk_D130 = new Set( 0x0602, new byte[] { 0xff, 0x03 } );

        internal static byte sub_D150( byte arg_0, byte arg_2 )
        {
            Set var_21 = new Set();

            var_21.Clear();
            var_21.SetRange( arg_0, arg_2 );

            byte var_1;

            do
            {
                do
                {
                    gbl.byte_1AFE6 = (char)seg049.READKEY();
                }while( unk_D130.MemberOf( gbl.byte_1AFE6 ) == false );
            }while( var_21.MemberOf( (byte)(gbl.byte_1AFE6 - 50) ) == false );

            seg051.Write( 0, gbl.byte_1AFE6, gbl.known01_02 );
            seg051.Write( gbl.known01_02 );
            var_1 = (byte)(gbl.byte_1AFE6 - 0x30);
            return var_1;
         }

        static Set asc_D2D4 = new Set(0x0801, new byte[] { 0x06 });
        static Set unk_D2F4 = new Set(0x0803, new byte[] { 0xFE, 0xFF, 0x01 });

        internal static void config_game( )
        {
            gbl.byte_1EFBE = 0;

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
				gbl.byte_1BF14 = 1;
			}
			else if( gbl.byte_1AFE6 == 'P' ) // PC Speaker
			{
				gbl.byte_1BF14 = 0;
			}
			else // No Sounds
			{
				gbl.byte_1BF14 = 2;
			}

            gbl.byte_1AFE6 = 'F'; /* force normal play (vs. demo) */

			if( gbl.byte_1AFE6 == 'F' )
			{
				gbl.byte_1B2C1 = 1;
			}
			else
			{
				gbl.byte_1B2C1 = 0;
			}
        }
    }
}
