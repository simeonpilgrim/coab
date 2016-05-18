using System;

namespace GoldBox.Engine
{
    public interface IKeyboardInput
    {
        void PressAnyKey(int secondsTimeout);
    }

    public class KeyboardInput : IKeyboardInput
    {
        public void PressAnyKey(int secondsTimeout)
        {
            seg043.clear_keyboard();

            var timeEnd = DateTime.Now.AddSeconds(secondsTimeout);

            while (seg049.KEYPRESSED() == false &&
                DateTime.Now < timeEnd)
            {
                System.Threading.Thread.Sleep(100);
            }

            seg043.clear_keyboard();
        }

    }
}
