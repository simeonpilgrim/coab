using GoldBox.Classes;

namespace GoldBox.Engine
{
    class seg051
    {
        static System.Random random_number;

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

        internal static void FillChar(byte fill_byte, int buffer_size, byte[] buffer)
        {
            for (int i = 0; i < buffer_size; i++)
            {
                buffer[i] = fill_byte;
            }
        }
    }
}
