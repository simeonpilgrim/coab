using System;
using System.IO;

namespace GoldBox.Logging
{
    public static class Config
    {
        public static string BasePath { get; set; }
        public static string LogPath { get; set; }
        public static string SavePath { get; set; }

        public static void Setup()
        {
            BasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Curse of the Azure Bonds");
            LogPath = Path.Combine(BasePath, "Logs");
            SavePath = Path.Combine(BasePath, "Save");

            CreateIfNeeded(BasePath);
            CreateIfNeeded(LogPath);
            CreateIfNeeded(SavePath);

            Logger.Setup(LogPath);
        }
        private static void CreateIfNeeded(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}
