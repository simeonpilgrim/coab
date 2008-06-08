using System;
using System.Collections.Generic;
using System.Text;
using Classes;
using Logging;

namespace engine
{
    static class VmLog
    {
        static internal void Write(string fmt, params object[] args)
        {
            if (gbl.printCommands == true)
            {
                Logger.DebugWrite(fmt, args);
            }
        }

        static internal void WriteLine(string fmt, params object[] args)
        {
            if (gbl.printCommands == true)
            {
                Logger.Debug(fmt, args);
            }
        }
    }

    internal class MemLoc
    {
        ushort loc;
        internal MemLoc(ushort _loc)
        {
            loc = _loc;
        }

        public override string ToString()
        {
            return String.Format("0x{0:X}", loc);
        }
    }
}
