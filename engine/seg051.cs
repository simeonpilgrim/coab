using Classes;
using System.Collections.Generic;

namespace engine
{
    class seg051
    {
		static System.Random random_number;
        /*static int random_number;*/

        internal static void o_1_a_87_i_tpdos_idc_tpdos_l_tptv()
        {
            gbl.known01_02 = new Text();
            gbl.known01_02.Assign(string.Empty, Text.AssignType.Write);
        }


        internal static byte[] GetMem( int arg_0 )
        /*TODO REMOVE THIS*/
        {
            return new byte[arg_0];
        }


        internal static void FreeMem( int arg_0, object arg_2 )
        /*TODO REMOVE THIS*/
        {
            
        }

        internal static void FreeMem( byte[] arg_2)
        /*TODO REMOVE THIS*/
        {

        }

        internal static void Close( Text arg_2 )
        {
            if (arg_2 != null)
            {
                if (arg_2.reader != null)
                {
                    arg_2.reader.Close();
                }
                if (arg_2.writer != null)
                {
                    arg_2.writer.Close();
                }
            }
        }


        static void __PutEntry( Text arg_0 )
        {
            if (gbl.dos_call_result == 0 &&
                arg_0.field_2 != 0xD7B2)
            {
                gbl.dos_call_result = 0x69;
            }

            throw new System.NotSupportedException();//bx = arg_0.field_8;
            throw new System.NotSupportedException();//dx = arg_0.field_4;

            throw new System.NotSupportedException();//mov	bx, es:[di+8]
            throw new System.NotSupportedException();//mov	dx, es:[di+4]
            throw new System.NotSupportedException();//les	di, es:[di+0Ch]
        }


        static void __PutChar( )
        {
            throw new System.NotSupportedException();//mov	es:[bx+di], al
            throw new System.NotSupportedException();//inc	bx
            throw new System.NotSupportedException();//cmp	bx, dx
            throw new System.NotSupportedException();//jz	loc_14E83
            throw new System.NotSupportedException();//retn
            throw new System.NotSupportedException();//loc_14E83:
            throw new System.NotSupportedException();//mov	di, sp
            throw new System.NotSupportedException();//les	di, ss:[di+2]
        }


        internal static void WriteLn( Text arg_0 )
        {
            arg_0.writer.WriteLine();
        }


        internal static char Read(Text arg_0)
        {
            return (char)arg_0.reader.Read();
        }

		/// <summary>
		/// leaves arg_6 on the stack.
		/// </summary>
        internal static void Read( short arg_0, out string arg_2, Text arg_6 )
        {
            char[] buff = new char[arg_0];

            arg_6.reader.Read(buff,0,arg_0);

            arg_2 = Sys.ArrayToString(buff, 0, arg_0);
        }

        internal static void Write( short arg_0, string arg_2, Text arg_6 )
        {
            arg_6.writer.Write(string.Empty.PadLeft(arg_0));
            arg_6.writer.Write(arg_2);
        }

		/// <summary>
		/// leaves arg_6 on the stack.
		/// </summary>
		internal static void Write( short arg_0, int arg_2, Text arg_6 )
        //Write(Text far &,Longint,Word)
        {
            string var_20 = arg_2.ToString();

            throw new System.NotSupportedException();//les	di, [bp+arg_6]
            throw new System.NotSupportedException();//assume es:nothing
            throw new System.NotSupportedException();//push	es
            throw new System.NotSupportedException();//push	di
            throw new System.NotSupportedException();//call	__PutEntry
            throw new System.NotSupportedException();//jnz	loc_1508A
            throw new System.NotSupportedException();//mov	ax, [bp+arg_0]
            throw new System.NotSupportedException();//sub	ax, cx
            throw new System.NotSupportedException();//jle	loc_1507F
            throw new System.NotSupportedException();//mov	si, cx
            throw new System.NotSupportedException();//mov	cx, ax
            throw new System.NotSupportedException();//loc_15076:
            throw new System.NotSupportedException();//mov	al, 0x20
            throw new System.NotSupportedException();//call	__PutChar
            throw new System.NotSupportedException();//loop	loc_15076
            throw new System.NotSupportedException();//mov	cx, si
            throw new System.NotSupportedException();//loc_1507F:
            throw new System.NotSupportedException();//lea	si, [bp+var_20]
            throw new System.NotSupportedException();//loc_15082:
            throw new System.NotSupportedException();//cld
            throw new System.NotSupportedException();//lods	byte ptr ss:[si+0]
            throw new System.NotSupportedException();//call	__PutChar
            throw new System.NotSupportedException();//loop	loc_15082
            throw new System.NotSupportedException();//loc_1508A:
            throw new System.NotSupportedException();//pop	di
            throw new System.NotSupportedException();//pop	es
            throw new System.NotSupportedException();//mov	es:[di+8], bx
            throw new System.NotSupportedException();//mov	sp, bp
            throw new System.NotSupportedException();//pop	bp
            throw new System.NotSupportedException();//retf	6
        }

        internal static string Copy(int CopyLen, int StartAt, string InString)
        {
            string OutString; 

            if (CopyLen >= InString.Length - StartAt)
            {
                CopyLen = InString.Length - StartAt;
            }

            if (CopyLen > 0)
            {
                OutString = InString.Substring(StartAt, CopyLen);
            }
            else
            {
                OutString = string.Empty;
            }

            return OutString;
        }

		internal static string Copy( int CopyLen, int StartAt, string InString, out string OutString )
        {
            OutString = Copy(CopyLen, StartAt, InString);

            return OutString;
        }


        internal static int Pos( string arg_0, string arg_4 )
        {
            int p = arg_0.IndexOf(arg_4);

            if (p == -1)
            {
                p = arg_0.Length;
            }

            return p;
        }


        internal static void Delete( int deleteLen, int beginAt, ref string arg_4 )
        {
            if( deleteLen > 0 )
            {
                arg_4 = arg_4.Remove( beginAt, deleteLen );
            }
        }

        internal static short Random( short arg_0 )
        {
            if( arg_0 == 0 )
            {
                return 0;
            }

			return (short)(random_number.Next() % arg_0);
        }


        internal static byte Random( byte arg_0 )
        {
            if( arg_0 == 0 )
            {
                return 0;
            }

            return (byte)(random_number.Next() % arg_0);
        }

        internal static int Random(int arg_0)
        {
            if (arg_0 == 0)
            {
                return 0;
            }

            return random_number.Next() % arg_0;
        }

        internal static double Random__Real( )
        {
			return random_number.NextDouble();
        }


        internal static void Randomize( )
        {
			random_number = new System.Random(unchecked((int)System.DateTime.Now.Ticks)); 
        }


        internal static void Str( short arg_0, out string arg_2, short arg_6, int arg_8 )
        {
            string var_20 = arg_8.ToString();

            int len = var_20.Length;

            if (len > arg_0)
            {
                var_20 = var_20.Substring(len - arg_0, arg_0);
            }

            arg_2 = var_20;
        }


        internal static void Reset(File arg_4 )
        {
            arg_4.stream.Seek(0, System.IO.SeekOrigin.Begin);
        }

        internal static void Rewrite(File arg_2 )
        {
            arg_2.stream.SetLength(0);
        }


        internal static void Close( File arg_0 )
        {
            arg_0.stream.Close();

            if (checkFILEvalid(arg_0))
			{
				arg_0.status = 0xD7B0;
			}
        }


        static bool checkFILEvalid( File f )
        {
			if( f.status != 0xD7B3 )
			{
				gbl.dos_call_result = 0x67;
				return false;
			}

			return true;
        }


        internal static void BlockRead(int arg_4, byte[] arg_6, File arg_A)
        {
            arg_A.stream.Read(arg_6, 0, arg_4);
        }

        internal static void BlockRead( out short arg_0, int arg_4, byte[] arg_6, File arg_A )
        {
            arg_0 = (short)arg_A.stream.Read(arg_6, 0, arg_4);
        }


        internal static void BlockWrite(int arg_4, byte[] arg_6, File arg_A)
        {
            arg_A.stream.Write(arg_6, 0, arg_4);
        }

        internal static void Seek( int arg_0, File arg_4 )
        {
            arg_4.stream.Seek(arg_0, System.IO.SeekOrigin.Begin);
        }


        internal static long FileSize( File f )
        {
            System.IO.FileInfo fi = new System.IO.FileInfo( f.name );

			return fi.Length;
        }


        internal static void Move( short count, byte[] dest, byte[] source )
        {
            System.Array.Copy( source, dest, count );
        }


        internal static void FillChar( byte fill_byte, ushort buffer_size, byte[] buffer )
        {
			for(int i = 0; i < buffer_size; i++)
			{
				buffer[i] = fill_byte;
			}
        }

        static List<string> Param = new List<string>();

        internal static string ParamStr(short arg_0)
        {
            string param;
            if (Param.Count < arg_0)
            {
                param = string.Empty;
            }
            else
            {
                param = Param[arg_0];
            }

            return param;
        }


        internal static void MkDir( string path )
        {
			System.IO.Directory.CreateDirectory( path );
        }


        internal static char UpCase( char arg_0 )
        {
			return char.ToUpper( arg_0 );
        }
    }
}
