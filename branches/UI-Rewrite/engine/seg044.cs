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
            if (gbl.soundType == SoundType.PC)
            {
                if (arg_0 == Sound.sound_0)
                {
                    foreach (var sp in sounds)
                    {
                        if (sp != null)
                        {
                            sp.Stop();
                        }
                    }
                }
                else if (arg_0 == Sound.sound_1)
                {
                }
                else if (arg_0 == Sound.sound_FF) // off maybe.
                {
                    foreach (var sp in sounds)
                    {
                        if (sp != null)
                        {
                            sp.Stop();
                        }
                    }
                }
                else if (arg_0 >= Sound.sound_2 && arg_0 <= Sound.sound_e)
                {
                    int sampleId = (int)arg_0 - 1;
                    if (sounds[sampleId] != null)
                    {
                        sounds[sampleId].Play();
                    }
                    else
                    {
                    }
                }
                else if (arg_0 == Sound.sound_f)
                {
                }
            }
        }

        static System.Media.SoundPlayer[] sounds;

        internal static void SoundInit()
        {
            var resources = new System.Resources.ResourceManager("Main.Resource", System.Reflection.Assembly.GetEntryAssembly());

            sounds = new System.Media.SoundPlayer[13];

            sounds[1] = new System.Media.SoundPlayer(resources.GetStream("missle"));
            sounds[2] = new System.Media.SoundPlayer(resources.GetStream("magic_hit"));
            sounds[4] = new System.Media.SoundPlayer(resources.GetStream("death"));
            sounds[5] = new System.Media.SoundPlayer(resources.GetStream("sound_5"));
            sounds[6] = new System.Media.SoundPlayer(resources.GetStream("hit"));
            sounds[8] = new System.Media.SoundPlayer(resources.GetStream("miss"));
            sounds[9] = new System.Media.SoundPlayer(resources.GetStream("step"));
            sounds[10] = new System.Media.SoundPlayer(resources.GetStream("sound_10"));
            sounds[12] = new System.Media.SoundPlayer(resources.GetStream("start_sound"));
        }
    }
}
