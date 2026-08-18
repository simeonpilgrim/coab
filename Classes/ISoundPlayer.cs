namespace Classes
{
    // implemented by each frontend so the engine has no dependency on a
    // platform-specific audio API; sample ids match the original sound table
    // (Sound.sound_2 .. Sound.sound_e map to ids 1 .. 13)
    public interface ISoundPlayer
    {
        void Play(int sampleId);
        void StopAll();
    }
}
