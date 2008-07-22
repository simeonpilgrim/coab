using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public class Cheats
    {
        public static bool allow_player_modify = true;
        public static void AllowPlayerModifySet(bool value)
        {
            allow_player_modify = value;
        }

        public static bool always_show_areamap = false;
        public static void AlwayShowAreaMapSet(bool value)
        {
            always_show_areamap = value;
        }

        public static bool free_training = false;
        public static void FreeTrainingSet(bool value)
        {
            free_training = value;
        }

        public static bool view_item_stats = false;
        public static void ViewItemStatsSet(bool value)
        {
            view_item_stats = value;
        }

        public static bool player_always_saves = false;
        public static void PlayerAlwaysSavesSet(bool value)
        {
            player_always_saves = value;
        }

        public static bool allow_gods_intervene = false;
        public static void AllowGodsInterveneSet(bool value)
        {
            allow_gods_intervene = value;
        }

        public static bool skip_copy_protection = true;
        public static void SkipCopyProtectionSet(bool value)
        {
            skip_copy_protection = value;
        }

        public static bool display_full_item_names = false;
        public static void DisplayFullItemNamesSet(bool value)
        {
            display_full_item_names = value;
        }

        public static bool allow_keyboard_exit = false;
        
        public static bool skip_title_screen = false;
        public static void SkipTitleScreenSet(bool value)
        {
            skip_title_screen = value;
        }

        public static bool improved_area_map = false;
        public static void ImprovedAreaMapSet(bool value)
        {
            improved_area_map = value;
        }
    }
}
