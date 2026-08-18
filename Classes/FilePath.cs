using System;
using System.IO;

namespace Classes
{
    public static class FilePath
    {
        // the original DOS data files are typically upper-case on disk while the
        // engine asks for mixed-case names; on case-sensitive file systems
        // (Linux, macOS) fall back to a case-insensitive directory scan
        public static string Resolve(string path)
        {
            if (System.IO.File.Exists(path))
            {
                return path;
            }

            string dir = Path.GetDirectoryName(path);
            if (string.IsNullOrEmpty(dir))
            {
                dir = Directory.GetCurrentDirectory();
            }

            if (Directory.Exists(dir) == false)
            {
                return path;
            }

            string name = Path.GetFileName(path);
            foreach (string candidate in Directory.EnumerateFiles(dir))
            {
                if (string.Equals(Path.GetFileName(candidate), name, StringComparison.OrdinalIgnoreCase))
                {
                    return candidate;
                }
            }

            return path;
        }
    }
}
