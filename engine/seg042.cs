using Classes;
using System.Collections.Generic;

namespace engine
{
	class seg042
	{
		static void debug_display( string text )
		{
            System.Console.Write(text);
			seg043.GetInputKey();
		}


		internal static void delete_file( string fileString )
		{
            if (System.IO.File.Exists(fileString))
            {
                System.IO.File.Delete(fileString);
            }
		}



        internal static bool find_and_open_file(out File file_ptr, bool arg_4, string full_file_name)
        {
            string file_name = System.IO.Path.GetFileName(full_file_name);
            string dir_path = System.IO.Path.GetDirectoryName(full_file_name);

            if (dir_path.Length == 0)
            {
                dir_path = gbl.exe_path;
            }

            bool file_found;
            do
            {
                file_found = file_find(System.IO.Path.Combine(dir_path, file_name));

                if (file_found == false &&
                    dir_path == gbl.exe_path)
                {
                    string tmp_path = gbl.exe_path;

                    gbl.exe_path = gbl.data_path;
                    gbl.data_path = tmp_path;

                    dir_path = gbl.exe_path;

                    file_found = file_find(System.IO.Path.Combine(dir_path, file_name));
                }

                if (file_found == false &&
                    arg_4 == false)
                {
                    debug_display("Couldn't find " + file_name + ". Check install.");
                }
            } while (file_found == false && arg_4 == false);

            if (file_found == true)
            {
                file_ptr = new File();
                file_ptr.Assign(System.IO.Path.Combine(dir_path ,file_name));

                seg051.Reset(file_ptr);
            }
            else
            {
                file_ptr = null;
            }

            return file_found;
        }


		internal static bool file_find( string arg_0 )
		{
            seg046.FINDFIRST(arg_0);

            return (gbl.FIND_result == 0 && arg_0.Length != 0);
		}


		static char[] unk_16FA9 = { ' ', '.', '*', ',', '?', '/', '\\', ':', ';', '|' };

		internal static string clean_string( string s )
        {
            string var_1;

			var_1 = s.Trim( unk_16FA9 ).ToUpper();

			if( var_1.Length > 8 )
			{
				var_1 = var_1.Substring( 0, 8 );
			}

            return var_1;
        }


        static bool setupDaxFiles(out System.IO.BinaryReader fileA, out System.IO.BinaryReader fileB, out short arg_8, string file_name)
		{
			fileA = null;
			fileB = null;
            arg_8 = 0;

			if( System.IO.File.Exists( file_name ) == false )
			{
				/*TODO Add message about missing file here.*/
				return false;
			}

			try
			{
                System.IO.FileStream fsA = new System.IO.FileStream(file_name, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
                System.IO.FileStream fsB = new System.IO.FileStream(file_name, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);

				fileA = new System.IO.BinaryReader( fsA );
				fileB = new System.IO.BinaryReader( fsB );
			}
			catch( System.ApplicationException )
			{
				/*TODO Add message about error here.*/
				return false;
			}

			arg_8 = fileA.ReadInt16();
			arg_8 += 2;

			fileB.BaseStream.Seek( arg_8, System.IO.SeekOrigin.Begin );
			return true;
        }

        class DaxHeaderEntry
        {
            internal int id;
            internal int offset;
            internal int rawSize; // decodeSize
            internal int compSize; // dataLength
        }

        class DaxFileCache
        {
            Dictionary<int, byte[]> entries;

            internal DaxFileCache(string filename)
            {
                entries = new Dictionary<int, byte[]>();

                LoadFile(filename);
            }

            private void LoadFile(string filename)
            {
                int dataOffset = 0;

                if (System.IO.File.Exists(filename) == false)
                {
                    return ;
                }

                System.IO.BinaryReader fileA;

                try
                {
                    System.IO.FileStream fsA = new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);

                    fileA = new System.IO.BinaryReader(fsA);
                }
                catch (System.ApplicationException)
                {
                    return ;
                }

                dataOffset = fileA.ReadInt16() + 2;

                List<DaxHeaderEntry> headers = new List<DaxHeaderEntry>();

                const int headerEntrySize = 9;

                for (int i = 0; i < ((dataOffset - 2) / headerEntrySize); i++)
                {
                    DaxHeaderEntry dhe = new DaxHeaderEntry();
                    dhe.id = fileA.ReadByte();
                    dhe.offset = fileA.ReadInt32();
                    dhe.rawSize = fileA.ReadInt16();
                    dhe.compSize = fileA.ReadUInt16();

                    headers.Add(dhe);
                }

                foreach(DaxHeaderEntry dhe in headers)
                {
                    byte[] comp = new byte[dhe.compSize];
                    byte[] raw = new byte[dhe.rawSize];

                    fileA.BaseStream.Seek(dataOffset + dhe.offset, System.IO.SeekOrigin.Begin);

                    comp = fileA.ReadBytes(dhe.compSize);

                    Decode(dhe.rawSize, dhe.compSize, raw, comp);

                    entries.Add(dhe.id, raw);
                }

                fileA.Close();
            }

            void Decode(int decodeSize, int dataLength, byte[] output_ptr, byte[] input_ptr)
            {
                sbyte run_length;
                int output_index;
                int input_index;

                input_index = 0;
                output_index = 0;

                do
                {
                    run_length = (sbyte)input_ptr[input_index];

                    if (run_length >= 0)
                    {
                        for (int i = 0; i <= run_length; i++)
                        {
                            output_ptr[output_index + i] = input_ptr[input_index + i + 1];
                        }

                        input_index += run_length + 2;
                        output_index += run_length + 1;
                    }
                    else
                    {
                        run_length = (sbyte)(-run_length);

                        for (int i = 0; i < run_length; i++)
                        {
                            output_ptr[output_index + i] = input_ptr[input_index + 1];
                        }

                        input_index += 2;
                        output_index += run_length;
                    }
                } while (input_index < dataLength);
            }

            internal byte[] GetData(int block_id)
            {
                byte[] orig;
                if (entries.TryGetValue(block_id, out orig) == false)
                {
                    return null;
                }

                return (byte[])orig.Clone();
            }
        }

        static Dictionary<string, DaxFileCache> fileCache = new Dictionary<string, DaxFileCache>();

        internal static void load_decode_dax( out byte[] out_data, out short decodeSize, int block_id, string file_name )
        {
            seg044.sound_sub_120E0( Sound.sound_0 );

            DaxFileCache dfc;

            if( !fileCache.TryGetValue(file_name.ToLower(),out dfc) )
            {
                dfc = new DaxFileCache(file_name);
                fileCache.Add(file_name.ToLower(), dfc);
            }

            out_data = dfc.GetData(block_id);
            decodeSize = out_data == null ? (short)0:(short)out_data.Length;
        }
         

        internal static void set_game_area( byte arg_0 )
        {
            gbl.game_area_backup = gbl.game_area;
            gbl.game_area = arg_0;
        }


        internal static void restore_game_area( )
        {
            gbl.game_area = gbl.game_area_backup;
        }
    }
}
