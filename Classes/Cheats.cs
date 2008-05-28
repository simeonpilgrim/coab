using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public class Cheats
    {
        public static bool allow_player_modify = true;
        public static void AllowPlayerModifyToggle()
        {
            allow_player_modify = !allow_player_modify;
        }

        public static bool always_show_areamap = false;
        public static void AlwayShowAreaMapToggle()
        {
            always_show_areamap = !always_show_areamap;
        }

        public static bool free_training = false;
        public static void FreeTrainingToggle()
        {
            free_training = !free_training;
        }

        public static bool view_item_stats = false;
        public static void ViewItemStatsTogle()
        {
            view_item_stats = !view_item_stats;
        }

        public static bool player_always_saves = false;
        public static void PlayerAlwaysSavesToggle()
        {
            player_always_saves = !player_always_saves;
        }

        public static bool allow_gods_intervene = false;
        public static void AllowGodsInterveneToggle()
        {
            allow_gods_intervene = !allow_gods_intervene;
        }

        public static bool skip_copy_protection = true;
        public static void SkipCopyProtectionToggle()
        {
            skip_copy_protection = !skip_copy_protection;
        }

        public static bool display_full_item_names = false;
        public static void DisplayFullItemNamesToggle()
        {
            display_full_item_names = !display_full_item_names;
        }

        public static bool allow_keyboard_exit = false;
        public static bool skip_title_screen = false;
    }
}
