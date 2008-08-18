using Classes;

namespace engine
{
    class seg039
    {
        internal static void config_game( )
        {
            gbl.exe_path = System.IO.Directory.GetCurrentDirectory();
			gbl.data_path = gbl.exe_path;
		   
            if (seg044.load_dump_bin()) // PC Speaker
			{
				gbl.soundType = SoundType.PC;
			}
			else // No Sounds
			{
				gbl.soundType = SoundType.None;
			}
        }
    }
}
