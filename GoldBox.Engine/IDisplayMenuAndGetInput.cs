using GoldBox.Classes;

namespace GoldBox.Engine
{
    public interface IDisplayMenuAndGetInput
    {
        char displayInput(out bool specialKeyPressed, bool useOverlay, byte arg_6, MenuColorSet colors, string displayInputString, string displayExtraString);
    }
    internal class DisplayMenuAndGetInput : IDisplayMenuAndGetInput
    {
        public char displayInput(out bool specialKeyPressed, bool useOverlay, byte arg_6, MenuColorSet colors, string displayInputString, string displayExtraString)
        {
            return ovr027.displayInput(out specialKeyPressed, useOverlay, arg_6, colors, displayInputString, displayExtraString);
        }
    }
}