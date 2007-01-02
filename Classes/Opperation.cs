using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public delegate ushort VmGetMemoryValue(ushort loc);

    public class Opperation
    {
        bool codeSet;
        bool lowSet;
        bool highSet;

        byte code;
        byte low;
        byte high;

        ushort word;

        public VmGetMemoryValue getMemoryValue;

        public void Clear()
        {
            codeSet = false;
            lowSet = false;
            highSet = false;
        }

        public byte Code
        {
            set {
                if (codeSet == false)
                {
                    code = value;
                    codeSet = true;
                }
                else throw new InvalidOperationException();
            }
            get
            {
                if (codeSet)
                    return code;
                else throw new InvalidOperationException();
            }
        }

        public ushort Word
        {
            get
            {
                if (highSet)
                    return word;
                else throw new InvalidOperationException();
            }
        }

        public byte Low
        {
            set
            {
                if (lowSet == false)
                {
                    low = value;
                    lowSet = true;
                }
                else throw new InvalidOperationException();
            }
            get
            {
                if (lowSet)
                    return low;
                else throw new InvalidOperationException();
            }
        }

        public byte High
        {
            set
            {
                if (highSet == false)
                {
                    high = value;
                    highSet = true;
                    word = (ushort)(low + (high << 8));
                }
                else throw new InvalidOperationException();
            }
            get
            {
                if (highSet)
                    return high;
                else throw new InvalidOperationException();
            }
        }

        public ushort GetCmdValue()
        {
            if (codeSet)
            {
                System.Console.WriteLine("  GetCmdValue: code: {0:X}", code);

                switch (code)
                {
                    case 0x00:
                        if (lowSet)
                            return low;
                        else throw new InvalidOperationException();

                    case 0x01:
                    case 0x03:
                    case 0x80:
                        if (highSet)
                            return getMemoryValue(word);
                        else throw new InvalidOperationException();

                    case 0x02:
                    case 0x81:
                        if (highSet)
                            return word;
                        else throw new InvalidOperationException();

                    default:
                        throw new InvalidOperationException();
                        //return 0;
                }
            }
            else throw new InvalidOperationException();
        }
    }
}
