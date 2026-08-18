using Classes;

namespace engine
{
    public class seg044
    {
        public static void SetSound(bool On)
        {
            gbl.soundType = On ? SoundType.PC : SoundType.None;
        }

        public static void SetPicture(bool On)
        {
            gbl.PicsOn = On;
        }

        public static void SetAnimation(bool On)
        {
            gbl.AnimationsOn = On;
        }

        internal static void PlaySound(Sound arg_0) /*sub_120E0*/
        {
            if (gbl.soundType == SoundType.PC && soundPlayer != null)
            {
                if (arg_0 == Sound.sound_0)
                {
                    soundPlayer.StopAll();
                }
                else if (arg_0 == Sound.sound_1)
                {
                }
                else if (arg_0 == Sound.sound_FF) // off maybe.
                {
                    soundPlayer.StopAll();
                }
                else if (arg_0 >= Sound.sound_2 && arg_0 <= Sound.sound_e)
                {
                    int sampleId = (int)arg_0 - 1;

                    soundPlayer.Play(sampleId);
                }
                else if (arg_0 == Sound.sound_f)
                {
                }
            }
        }

        static ISoundPlayer soundPlayer;

        public static void SetSoundPlayer(ISoundPlayer player)
        {
            soundPlayer = player;
        }
    }
}
