using Classes;

namespace engine
{
    class SearchRec
    {
        internal const int FileNotFound = 0x02;
        internal const int PathNotFound = 0x03;
        internal const int NoMoreFiles = 0x12;

        internal int index;
        internal System.IO.FileInfo[] files;

        internal string fileName;
    }

    class seg046
    {
        internal static int getDiskSpace( byte arg_0 )
        {
			return 0x7FFFFFFF; /*HACK this need to be completed.*/
        }


        internal static void FINDFIRST(out SearchRec record, short search_attrib, string arg_file_string)
        {
            record = new SearchRec();
            
            System.IO.DirectoryInfo di;

            string root = string.Empty;
            string filter = System.IO.Path.GetFileName(arg_file_string);

            if (System.IO.Path.IsPathRooted(arg_file_string))
            {
                root = System.IO.Path.GetDirectoryName(arg_file_string);
            }
            else
            {
                string cwd = System.IO.Directory.GetCurrentDirectory();
                root = System.IO.Path.Combine(cwd, System.IO.Path.GetDirectoryName(arg_file_string));
            }

            if (filter == string.Empty)
            {
                filter = "*.*";
            }

            if (System.IO.Directory.Exists(root) == false)
            {
                gbl.FIND_result = SearchRec.PathNotFound;
                return;
            } 
            
            di = new System.IO.DirectoryInfo(root);



            record.files = di.GetFiles(filter);

            if (record.files.Length == 0)
            {
                gbl.FIND_result = SearchRec.NoMoreFiles;
                return;
            }

            gbl.FIND_result = 0;
            record.fileName = record.files[0].Name;
            record.index = 0;
        }


        internal static void FINDNEXT(SearchRec record)
        {
            record.index++;
            if (record.index >= record.files.Length)
            {
                gbl.FIND_result = SearchRec.NoMoreFiles;
                return;
            }

            record.fileName = record.files[record.index].Name;
        }
    }
}
