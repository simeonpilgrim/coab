using GoldBox.Classes;
using GoldBox.Classes.DaxFiles;
using GoldBox.Logging;

namespace GoldBox.Engine
{
    class seg042
    {
        internal static void load_decode_dax(out byte[] out_data, out short decodeSize, int block_id, string file_name)
        {
            seg044.PlaySound(Sound.sound_0);

            out_data = DaxCache.LoadDax(file_name.ToLower(), block_id);
            decodeSize = out_data == null ? (short)0 : (short)out_data.Length;
        }

        internal static void set_game_area(byte arg_0)
        {
            gbl.game_area_backup = gbl.game_area;
            gbl.game_area = arg_0;
        }

        internal static void restore_game_area()
        {
            gbl.game_area = gbl.game_area_backup;
        }
    }
}
