using Classes;
using System.Collections.Generic;

namespace engine
{
	class seg042
	{
		//static byte[] dax_raw;

		static void debug_display( string arg_0 )
		{
            System.Console.Write(arg_0);
			seg043.GetInputKey();
		}


		internal static void delete_file( string fileString )
		{
			if( file_find( fileString ) == true )
			{
                System.IO.File.Delete(fileString);
			}
		}



        internal static bool find_and_open_file(out File file_ptr, byte arg_4, string arg_6, string arg_A)
        {
            string var_146;
            string var_141;
            string var_138;
            string var_F4;
            bool var_A4;

            seg046.FSplit(out var_146, out var_141, out var_138, arg_A);

            if (var_138.Length == 0)
            {
                var_138 = gbl.unk_1B21A;
            }

            do
            {

                var_A4 = file_find(System.IO.Path.Combine(var_138, var_141 + var_146));

                if (var_A4 == false &&
                    var_138 == gbl.unk_1B21A)
                {
                    var_F4 = gbl.unk_1B21A;

                    gbl.unk_1B21A = gbl.unk_1B26A;
                    gbl.unk_1B26A = var_F4;
                    var_138 = gbl.unk_1B21A;

                    var_A4 = file_find(System.IO.Path.Combine(var_138, var_141 + var_146));
                }

                if (var_A4 == false &&
                    arg_4 == 0)
                {
                    if (var_138[0] < 0x43)
                    {
                        debug_display(arg_6 + gbl.byte_1B2BA.ToString() + ":");
                    }
                    else
                    {
                        debug_display("Couldn't find " + var_141 + var_146 + ". Check install.");
                    }
                }
            } while (var_A4 == false && arg_4 == 0);

            if (var_A4 == true)
            {
                file_ptr = new File();
                file_ptr.Assign(System.IO.Path.Combine(var_138 ,var_141 + var_146));

                seg051.Reset(file_ptr);
            }
            else
            {
                file_ptr = null;
            }

            return var_A4;
        }


		internal static bool file_find( string arg_0 )
		{
			SearchRec var_7D;
			string local_string;
			bool ret_val;
            
			local_string = arg_0;

            seg046.FINDFIRST(out var_7D, 0, local_string);

			if( gbl.word_1EFBC != 0 ||
				local_string.Length == 0 )
			{
				ret_val = false;
			}
			else
			{
				ret_val = true;
			}

			return ret_val;
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

            internal byte[] GetData(byte block_id)
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

        internal static void load_decode_dax( out byte[] out_data, out short decodeSize, byte block_id, string file_name )
        {
            seg044.sound_sub_120E0( gbl.sound_0_188BE );

            DaxFileCache dfc;

            if( !fileCache.TryGetValue(file_name.ToLower(),out dfc) )
            {
                dfc = new DaxFileCache(file_name);
                fileCache.Add(file_name.ToLower(), dfc);
            }

            out_data = dfc.GetData(block_id);
            decodeSize = out_data == null ? (short)0:(short)out_data.Length;

            seg044.sound_sub_120E0(gbl.sound_1_188C0);
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
