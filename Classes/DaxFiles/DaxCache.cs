using System;
using System.Collections.Generic;
using System.Text;

namespace Classes.DaxFiles
{
    public class DaxCache
    {
        static Dictionary<string, DaxFileCache> fileCache =
            new Dictionary<string, DaxFileCache>(StringComparer.OrdinalIgnoreCase);

        public static byte[] LoadDax(string file_name, int block_id)
        {
            DaxFileCache dfc;

            string basePath = string.IsNullOrEmpty(gbl.exe_path)
                ? System.IO.Directory.GetCurrentDirectory()
                : gbl.exe_path;
            file_name = FilePath.Resolve(System.IO.Path.Combine(basePath, file_name));

            if (!fileCache.TryGetValue(file_name, out dfc))
            {
                dfc = new DaxFileCache(file_name);
                fileCache.Add(file_name, dfc);
            }

            return dfc.GetData(block_id);
        }
    }
}
