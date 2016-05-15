using System;

namespace GoldBox.Classes
{
    public struct StatValue : IDataIO
    {
        int[,,] raceSexMinMax;
        int[] classMin;
        int[] ageEffects;

        public StatValue(int[,,] _raceSexMinMax, int[] _classMin, int[] _ageEffects)
        {
            raceSexMinMax = _raceSexMinMax;
            classMin = _classMin;
            ageEffects = _ageEffects;
            cur = full = 0;
        }

        public int cur;
        public int full;

        public void Load(int val)
        {
            full = cur = val;
        }

        public void Assign(StatValue sv)
        {
            full = sv.full;
            cur = sv.cur;
        }

        public void Inc()
        {
            full += 1;
            cur += 1;
        }

        public void Dec()
        {
            full -= 1;
            cur -= 1;
        }

        public void EnforceRaceSexLimits(int race, int sex)
        {
            if (raceSexMinMax != null)
            {
                full = Math.Min(raceSexMinMax[race, 1, sex], full);
                full = Math.Max(raceSexMinMax[race, 0, sex], full);
            }
            cur = full;
        }

        public void EnforceClassLimits(int _class)
        {
            if (classMin != null)
            {
                full = Math.Max(classMin[_class], full);
            }
            cur = full;
        }

        public void AgeEffects(int race, int age)
        {
            for (int i = 0; i < 5; i++)
            {
                if (Limits.RaceAgeBrackets[race, i] < age)
                {
                    full += ageEffects[i];
                }
            }
        }

        public void Write(byte[] data, int offset)
        {
            data[offset + 0] = (byte)cur;
            data[offset + 1] = (byte)full;
        }

        public void Read(byte[] data, int offset)
        {
            // enforce values in valid range
            cur = Math.Min((int)data[offset + 0], 25);
            full = Math.Min((int)data[offset + 1], 25);
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}", cur, full);
        }
    }

}
