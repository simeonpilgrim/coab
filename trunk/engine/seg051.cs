using Classes;
using System.Collections.Generic;

namespace engine
{
    class seg051
    {
		static System.Random random_number;
        /*static int random_number;*/


        internal static byte[] GetMem(int arg_0)
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
        }


        internal static void BlockRead(int read_len, byte[] data, File file)
        {
            file.stream.Read(data, 0, read_len);
        }

        internal static void BlockRead( out short arg_0, int arg_4, byte[] arg_6, File file )
        {
            arg_0 = (short)file.stream.Read(arg_6, 0, arg_4);
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


        internal static void FillChar( byte fill_byte, int buffer_size, byte[] buffer )
        {
			for(int i = 0; i < buffer_size; i++)
			{
				buffer[i] = fill_byte;
			}
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
