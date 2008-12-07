using Classes;

namespace engine
{
    class seg044
    {
        internal static void sound_sub_120E0(Sound arg_0) /*sub_120E0*/
        {
            if (arg_0 == Sound.sound_0)
            {
                if (gbl.soundType != SoundType.None)
                {
                    foreach (var sp in sounds)
                    {
                        if (sp != null)
                        {
                            sp.Stop();
                        }
                    }
                }
            }
            else if (arg_0 == Sound.sound_1)
            {
            }
            else if (arg_0 == Sound.sound_FF) // off maybe.
            {
                if (gbl.soundType != SoundType.None)
                {
                    foreach (var sp in sounds)
                    {
                        if (sp != null)
                        {
                            sp.Stop();
                        }
                    }
                    gbl.soundFlag01 = false;
                }
            }
            else if (arg_0 >= Sound.sound_2 && arg_0 <= Sound.sound_e)
            {
                if (gbl.soundType == SoundType.PC)
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
            }
            else if (arg_0 == Sound.sound_f)
            {
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
