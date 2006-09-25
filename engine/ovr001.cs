using Classes;

namespace engine
{
    class ovr001
    {
        internal static short mem_check( short arg_0 )
        {
            byte var_3;
            short ret_val = 0;

			if ( arg_0 <= 0 ) 
			{
				var_3 = 0;
				do 
				{
					seg041.displayAndDebug ( "WARNING: Insufficient	Memory", 0, 0x0e );

					if( gbl.byte_1B2C0 != 0 )
					{
						ovr017.SaveGame();
						var_3 = gbl.byte_1C01B;
					}
					else
					{
						var_3 = 1;
						seg041.displayAndDebug( "Insufficient Memory. Exiting Program", 0, 0x0e );
					}
				
				} while ( var_3 == 0 );

				seg043.print_and_exit();
			}

			return ret_val;
        }
    }
}
