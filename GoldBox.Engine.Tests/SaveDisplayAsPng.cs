using System.IO;
using GoldBox.Classes;

namespace GoldBox.Engine
{
    internal class SaveDisplayAsPng
    {
        private int _counter;
        private readonly string _basePath;
        public SaveDisplayAsPng(string basePath)
        {
            _basePath = Path.Combine(basePath, "Display");
        }
        public void Reset()
        {
            _counter = 0;
            RecreateDisplayDirectory();
            Display.UpdateCallback = DisplayCallback;
        }

        private void RecreateDisplayDirectory()
        {
            if (Directory.Exists(_basePath))
                Directory.Delete(_basePath, true);
            Directory.CreateDirectory(_basePath);
        }

        private void DisplayCallback()
        {
            Display.bm.Save(Path.Combine(_basePath, $"Screen{_counter++}.png"));
        }

    }
}
