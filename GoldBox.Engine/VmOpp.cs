using GoldBox.Classes;
using GoldBox.Logging;

namespace GoldBox.Engine
{
    static class VmLog
    {
        static internal void Write(string fmt, params object[] args)
        {
            if (gbl.printCommands)
            {
                Logger.DebugWrite(fmt, args);
            }
        }

        static internal void WriteLine(string fmt, params object[] args)
        {
            if (gbl.printCommands)
            {
                Logger.Debug(fmt, args);
            }
        }
    }
}
