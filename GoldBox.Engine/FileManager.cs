using GoldBox.Classes;
using GoldBox.Logging;
using System.IO;

namespace GoldBox.Engine
{
    class FileManager
    {
        internal bool find_and_open_file(out OpenOrCreateFile file_ptr, bool noError, string full_file_name)
        {
            string file_name = Path.GetFileName(full_file_name);
            string dir_path = Path.GetDirectoryName(full_file_name);

            if (dir_path.Length == 0)
            {
                dir_path = gbl.exe_path;
            }

            bool file_found = File.Exists(Path.Combine(dir_path, file_name));

            if (file_found == true)
            {
                file_ptr = new OpenOrCreateFile();
                file_ptr.Assign(Path.Combine(dir_path, file_name));
                file_ptr.Reset();
            }
            else
            {
                file_ptr = null;
                if(!noError)
                    debug_display("Couldn't find " + file_name + ". Check install.");

            }

            return file_found;
        }

        internal void DeleteIfExists(string fileString)
        {
            if (File.Exists(fileString))
            {
                File.Delete(fileString);
            }
        }

        internal bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }

        static void debug_display(string text)
        {
            Logger.Log(text);
            seg043.GetInputKey();
        }
    }
}
