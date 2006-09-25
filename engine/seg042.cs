using Classes;

namespace engine
{
	class seg042
	{
		static byte[] dax_raw;

        static void allocate_dax_raw(ushort arg_0, out byte[] mem_ptr)
        {
            mem_ptr = new byte[((arg_0 + 7) & 0xfff8)];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="count">number of times to write character</param>
        /// <param name="colour">attribute (text mode) or color (graphics mode)
        /// if bit 7 set in graphics mode, character is XOR'ed onto screen</param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        static void sub_11340(char ch, byte count, byte colour, byte row, byte column)
        {
           throw new System.NotSupportedException();// SetCursor(column, row);
           throw new System.NotSupportedException();// WriteChar(ch, count, colour);
        }


        static void sub_113AC(string arg_0, byte colour, byte row, byte column)
        {
            foreach (char ch in arg_0)
            {
                sub_11340(ch, 1, colour, row, column);
                column++;
            }
        }


        static void sub_1141C(byte count, byte colour, byte row, byte column)
        {
            sub_11340((char)0xDB, count, colour, row, column);
        }


		static void sub_1143A( string arg_0, byte arg_4 )
		{
			byte var_2A;

			sub_1141C( 0x28, 0, 0x18, 0 );
			sub_113AC( arg_0, arg_4, 0x18, 0 );
			var_2A = seg043.debug_txt();
			sub_1141C( 0x28, 0, 0x18, 0 );
		}


		internal static void delete_file( string fileString )
		{
			if( file_find( fileString ) == true )
			{
                System.IO.File.Delete(fileString);
			}
		}


		internal static void check_overlay_file( )
		{
			bool var_2;
			byte var_1;

			do
			{
				var_2 = file_find( "game.ovr" );

				if( var_2 == false )
				{
					seg051.Write( 0, "Please insert overlay disk.", gbl.known01_02 );
					seg051.WriteLn( gbl.known01_02 );
					var_1 = seg043.debug_txt();
				}

			}while( var_2 == false );
		}



        internal static bool find_and_open_file(out File file_ptr, byte arg_4, string arg_6, string arg_A)
        {
            string var_246;
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
                        sub_1143A(arg_6 + gbl.byte_1B2BA.ToString() + ":", 14);
                    }
                    else
                    {
                        var_246 = "Couldn't find " + var_141 + var_146 + ". Check install.";
                        sub_1143A(var_246, 14);
                    }
                }
            } while (var_A4 == false && arg_4 == 0);

            if (var_A4 == true)
            {
                file_ptr = new File();
                file_ptr.Assign(System.IO.Path.Combine(var_138 ,var_141 + var_146));

                seg051.Reset(1, file_ptr);
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


        static void decode_dax_raw( ref short decodeSize, ref ushort dataLength, byte[] output_ptr, byte[] input_ptr )
        {
            sbyte run_length;
            ushort output_index;
            ushort input_index;

            input_index = 0;
            output_index = 0;

            do
            {
				run_length = (sbyte)input_ptr[ input_index ];

				if( run_length >= 0 )
				{
					for( int i = 0; i <= run_length; i++ )
					{
						output_ptr[ output_index + i ] = input_ptr[ input_index + i + 1 ];
					}

					input_index += (ushort)run_length;
					input_index += 2;
                    output_index += (ushort)run_length;
					output_index += 1;
				}
				else
				{
					run_length = (sbyte)(-run_length);

					for( int i = 0; i < run_length; i++ )
					{
						output_ptr[ output_index + i ] = input_ptr[ input_index + 1 ];
					}

					input_index += 2;
                    output_index += (ushort)run_length;
				}
            }while( input_index < dataLength );
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


        internal static void load_decode_dax( out byte[] out_data, out short decodeSize, byte block_id, string file_name )
        {
            System.IO.BinaryReader fileA;
            System.IO.BinaryReader fileB;
            short header_size;

            seg044.sub_120E0( gbl.word_188BE );

            if (setupDaxFiles(out fileA, out fileB, out header_size, file_name) == false)
            {
                decodeSize = 0;
                out_data = null;

                seg044.sub_120E0( gbl.word_188C0 );
            }
            else
            {
				byte blockId;
				ushort dataLength;
				int blockOffset;

                do
                {
					blockId = fileA.ReadByte();
					blockOffset = fileA.ReadInt32();
					decodeSize = fileA.ReadInt16();
					dataLength = fileA.ReadUInt16();

                    if( dataLength > 36000 )
                    {
                        sub_1143A( "tempsize " + dataLength.ToString(), 14 );
                        seg043.print_and_exit();
                    }

                }while( blockId != block_id && fileA.BaseStream.Position <= header_size );
    
                if( blockId != block_id )
                {
                    decodeSize = 0;
                    out_data = null;

					fileA.Close();
					fileB.Close();

					seg044.sub_120E0( gbl.word_188C0 );
                }
                else
                {
                    allocate_dax_raw( dataLength, out dax_raw );
                
					out_data = new byte[ decodeSize ];

					fileB.BaseStream.Seek( header_size + blockOffset, System.IO.SeekOrigin.Begin );

					dax_raw = fileB.ReadBytes( dataLength );

					fileA.Close();
					fileB.Close();

                    decode_dax_raw( ref decodeSize, ref dataLength, out_data, dax_raw );
					dax_raw = null;

                    seg044.sub_120E0( gbl.word_188C0 );
                }
            }
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
