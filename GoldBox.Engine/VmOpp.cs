using GoldBox.Classes;
using GoldBox.Logging;

namespace GoldBox.Engine
{
    static class VmLog
    {
        internal static void Write(string fmt, params object[] args)
        {
            if (gbl.printCommands)
            {
                Logger.DebugWrite(fmt, args);
            }
        }

        internal static void WriteLine(string fmt, params object[] args)
        {
            if (gbl.printCommands)
            {
                Logger.Debug(fmt, args);
            }
        }
    }
}
