using System;

namespace Main
{
    // sample loading moved here from engine.seg044 so the engine has no
    // dependency on the Windows-only System.Media API
    public class WinSoundPlayer : Classes.ISoundPlayer
    {
        System.Media.SoundPlayer[] sounds;

        public WinSoundPlayer()
        {
            var resources = new System.Resources.ResourceManager("Main.Resource", typeof(WinSoundPlayer).Assembly);

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

        public void Play(int sampleId)
        {
            if (sampleId >= 0 && sampleId < sounds.Length && sounds[sampleId] != null)
            {
                sounds[sampleId].Play();
            }
        }

        public void StopAll()
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
}
