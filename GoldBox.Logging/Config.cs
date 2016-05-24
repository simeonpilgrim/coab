using System;
using System.IO;

namespace GoldBox.Logging
{
    public static class Config
    {
        public static FileInfo BaseFile(string fileName) => new FileInfo(Path.Combine(BasePath, fileName));
        public static FileInfo LogFile(string fileName) => new FileInfo(Path.Combine(LogPath, fileName));
        public static FileInfo SaveFile(string fileName) => new FileInfo(Path.Combine(SavePath, fileName));
        public static FileInfo DataFile(string fileName) => new FileInfo(Path.Combine(DataPath, fileName));

        private static string BasePath { get; set; }
        public static string LogPath { get; set; }
        public static string SavePath { get; set; }
        public static string DataPath { get; set; }

        public static void Setup()
        {
            Setup(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Curse of the Azure Bonds"));
        }

        public static void Setup(string initialFolder)
        {
            BasePath = initialFolder;
            LogPath = Path.Combine(BasePath, "Logs");
            SavePath = Path.Combine(BasePath, "Save");
            DataPath = Path.Combine(BasePath, "Data");

            CreateIfNeeded(BasePath);
            CreateIfNeeded(LogPath);
            CreateIfNeeded(SavePath);
            CreateIfNeeded(DataPath);

            Logger.Setup(LogPath);
        }

        private static void CreateIfNeeded(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}
