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


        internal static void FreeMem(int arg_0, object arg_2)
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

        internal static byte Random(byte arg_0)
        {
            if (arg_0 == 0)
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

        internal static double Random__Real()
        {
            return random_number.NextDouble();
        }


        internal static void Randomize()
        {
            random_number = new System.Random(unchecked((int)System.DateTime.Now.Ticks));
        }


        internal static void Reset(File arg_4)
        {
            arg_4.stream.Seek(0, System.IO.SeekOrigin.Begin);
        }

        internal static void Rewrite(File arg_2)
        {
            arg_2.stream.SetLength(0);
        }


        internal static void Close(File arg_0)
        {
            arg_0.stream.Close();
        }

        internal static int BlockRead(int count, byte[] data, File file)
        {
            return file.stream.Read(data, 0, count);
        }


        internal static void BlockWrite(int arg_4, byte[] arg_6, File arg_A)
        {
            arg_A.stream.Write(arg_6, 0, arg_4);
        }

        internal static void Seek(int arg_0, File arg_4)
        {
            arg_4.stream.Seek(arg_0, System.IO.SeekOrigin.Begin);
        }


        internal static long FileSize(File f)
        {
            var fi = new System.IO.FileInfo(f.name);

            return fi.Length;
        }


        internal static void FillChar(byte fill_byte, int buffer_size, byte[] buffer)
        {
            for (int i = 0; i < buffer_size; i++)
            {
                buffer[i] = fill_byte;
            }
        }
    }
}
