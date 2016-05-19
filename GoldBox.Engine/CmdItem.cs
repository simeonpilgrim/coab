using GoldBox.Classes;

namespace GoldBox.Engine
{
    internal class CmdItem
    {
        public delegate void CmdDelegate();

        int size;
        string name;
        CmdDelegate cmd;

        public CmdItem(int Size, string Name, CmdDelegate Cmd)
        {
            size = Size;
            name = Name;
            cmd = Cmd;
        }

        public void Run()
        {
            cmd();
        }

        public string Name()
        {
            return name;
        }

        internal void Skip()
        {
            VmLog.WriteLine("SKIPPING: {0}", name);

            if (size == 0)
                gbl.ecl_offset += 1;
            else
                ovr008.vm_LoadCmdSets(size);
        }
    }
}
