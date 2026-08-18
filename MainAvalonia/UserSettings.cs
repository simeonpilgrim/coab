using System;
using System.IO;
using System.Text.Json;
using Logging;

namespace MainAvalonia
{
    // JSON stand-in for the WinForms user-scoped Settings, stored next to the
    // saves under the "Curse of the Azure Bonds" folder
    public class UserSettings
    {
        public bool PlayerAlwaysSaves { get; set; }
        public bool AlwayShowAreaMap { get; set; }
        public bool FreeTraining { get; set; }
        public bool SkipCopyProtection { get; set; } = true;
        public bool AllowGodsIntervene { get; set; }
        public bool DisplayFullItemNames { get; set; }
        public bool ViewItemsStats { get; set; }
        public bool SkipTitleScreen { get; set; }
        public bool ImprovedAreaMap { get; set; }
        public bool NoRaceClassLimits { get; set; }
        public bool NoRaceLevelLimits { get; set; }
        public bool SortTreasure { get; set; }
        public bool SoundOn { get; set; } = true;
        public bool PictureOn { get; set; } = true;
        public bool AnimationOn { get; set; } = true;

        static string SettingsFile()
        {
            return Path.Combine(Config.GetBasePath(), "settings.json");
        }

        public static UserSettings Load()
        {
            try
            {
                string file = SettingsFile();
                if (System.IO.File.Exists(file))
                {
                    var loaded = JsonSerializer.Deserialize<UserSettings>(System.IO.File.ReadAllText(file));
                    if (loaded != null)
                    {
                        return loaded;
                    }
                }
            }
            catch (JsonException)
            {
            }

            return new UserSettings();
        }

        public void Save()
        {
            System.IO.File.WriteAllText(SettingsFile(),
                JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true }));
        }

        public void Apply()
        {
            Classes.Cheats.PlayerAlwaysSavesSet(PlayerAlwaysSaves);
            Classes.Cheats.AlwayShowAreaMapSet(AlwayShowAreaMap);
            Classes.Cheats.FreeTrainingSet(FreeTraining);
            Classes.Cheats.SkipCopyProtectionSet(SkipCopyProtection);
            Classes.Cheats.AllowGodsInterveneSet(AllowGodsIntervene);
            Classes.Cheats.DisplayFullItemNamesSet(DisplayFullItemNames);
            Classes.Cheats.ViewItemStatsSet(ViewItemsStats);
            Classes.Cheats.SkipTitleScreenSet(SkipTitleScreen);
            Classes.Cheats.ImprovedAreaMapSet(ImprovedAreaMap);
            Classes.Cheats.NoRaceLevelLimits(NoRaceLevelLimits);
            Classes.Cheats.NoRaceClassRestrictions(NoRaceClassLimits);
            Classes.Cheats.SortTreasureSet(SortTreasure);

            engine.seg044.SetSound(SoundOn);
            engine.seg044.SetPicture(PictureOn);
            engine.seg044.SetAnimation(AnimationOn);
        }
    }
}
