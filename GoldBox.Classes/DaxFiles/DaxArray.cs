namespace GoldBox.Classes.DaxFiles
{
    public class DaxArray
    {
        public int numFrames;
        public int curFrame;
        public AnimationFrame[] frames;

        public DaxArray()
        {
            numFrames = 0;
            curFrame = 0;
            frames = new AnimationFrame[8];
            frames[0] = new AnimationFrame();
            frames[1] = new AnimationFrame();
            frames[2] = new AnimationFrame();
            frames[3] = new AnimationFrame();
            frames[4] = new AnimationFrame();
            frames[5] = new AnimationFrame();
            frames[6] = new AnimationFrame();
            frames[7] = new AnimationFrame();
        }

        public void NextFrame()
        {
            curFrame++;

            if (curFrame > numFrames)
            {
                curFrame = 1;
            }
        }

        public DaxBlock CurrentPicture()
        {
            return frames[curFrame - 1].picture;
        }

        /// <summary>
        /// Tenth of a second
        /// </summary>
        public int CurrentDelay()
        {
            return frames[curFrame - 1].delay;
        }
    }
}
