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
            frames[0] = new AnimationFrame(); // 1D558 - seg600:7248
            frames[1] = new AnimationFrame(); // 1D560 - seg600:7250
            frames[2] = new AnimationFrame(); // 1D568 - seg600:7258
            frames[3] = new AnimationFrame(); // 1D570 - seg600:7260
            frames[4] = new AnimationFrame(); // 1D578 - seg600:7268
            frames[5] = new AnimationFrame(); // 1D580 - seg600:7270
            frames[6] = new AnimationFrame(); // 1D588 - seg600:7278
            frames[7] = new AnimationFrame(); // 1D590 - seg600:7280
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
