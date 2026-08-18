using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Logging;

namespace MainAvalonia
{
    // plays the short effect samples by spawning the platform's command-line
    // wav player (afplay on macOS, paplay/aplay/play on Linux); this avoids any
    // native audio library dependency at the cost of a few ms launch latency
    public class ProcessSoundPlayer : Classes.ISoundPlayer
    {
        static readonly string[] sampleNames = {
            null, "missle", "magic_hit", null, "death", "sound_5", "hit",
            null, "miss", "step", "sound_10", null, "start_sound",
        };

        readonly string soundDir;
        readonly string playerCommand;
        readonly List<Process> running = new List<Process>();

        public ProcessSoundPlayer()
        {
            soundDir = Path.Combine(Config.GetBasePath(), "Sounds");
            ExtractSamples();
            playerCommand = FindPlayerCommand();
        }

        void ExtractSamples()
        {
            Directory.CreateDirectory(soundDir);

            var assembly = typeof(ProcessSoundPlayer).Assembly;
            foreach (string resource in assembly.GetManifestResourceNames())
            {
                if (resource.EndsWith(".wav", StringComparison.OrdinalIgnoreCase) == false)
                {
                    continue;
                }

                // resource names look like "MainAvalonia.sounds.step.wav"
                string[] parts = resource.Split('.');
                string fileName = parts[parts.Length - 2] + ".wav";
                string target = Path.Combine(soundDir, fileName);

                using (var stream = assembly.GetManifestResourceStream(resource))
                using (var file = System.IO.File.Create(target))
                {
                    stream.CopyTo(file);
                }
            }
        }

        static string FindPlayerCommand()
        {
            if (OperatingSystem.IsMacOS())
            {
                return "afplay";
            }

            foreach (string candidate in new[] { "paplay", "pw-play", "aplay", "play" })
            {
                string pathVar = Environment.GetEnvironmentVariable("PATH") ?? "";
                foreach (string dir in pathVar.Split(Path.PathSeparator))
                {
                    if (dir.Length > 0 && System.IO.File.Exists(Path.Combine(dir, candidate)))
                    {
                        return candidate;
                    }
                }
            }

            return null;
        }

        public void Play(int sampleId)
        {
            if (playerCommand == null ||
                sampleId < 0 || sampleId >= sampleNames.Length || sampleNames[sampleId] == null)
            {
                return;
            }

            string wavFile = Path.Combine(soundDir, sampleNames[sampleId] + ".wav");
            if (System.IO.File.Exists(wavFile) == false)
            {
                return;
            }

            try
            {
                var process = Process.Start(new ProcessStartInfo
                {
                    FileName = playerCommand,
                    ArgumentList = { wavFile },
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                });

                lock (running)
                {
                    running.RemoveAll(p => p.HasExited);
                    running.Add(process);
                }
            }
            catch (System.ComponentModel.Win32Exception)
            {
            }
        }

        public void StopAll()
        {
            lock (running)
            {
                foreach (var process in running)
                {
                    try
                    {
                        if (process.HasExited == false)
                        {
                            process.Kill();
                        }
                    }
                    catch (InvalidOperationException)
                    {
                    }
                }

                running.Clear();
            }
        }
    }
}
