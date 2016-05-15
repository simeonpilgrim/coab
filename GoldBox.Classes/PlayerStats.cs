using System;

namespace GoldBox.Classes
{
    public class PlayerStats : IDataIO
    {
        public StatValue Str = new StatValue(Limits.StrRaceSexMinMax, Limits.StrClassMin, Limits.StrAgeEffect);
        public StatValue Str00 = new StatValue(Limits.Str00RaceSexMinMax, Limits.Str00ClassMin, Limits.Str00AgeEffect);
        public StatValue Con = new StatValue(Limits.ConRaceSexMinMax, Limits.ConClassMin, Limits.ConAgeEffect);
        public StatValue Dex = new StatValue(Limits.DexRaceSexMinMax, Limits.DexClassMin, Limits.DexAgeEffect);
        public StatValue Int = new StatValue(Limits.IntRaceSexMinMax, Limits.IntClassMin, Limits.IntAgeEffect);
        public StatValue Wis = new StatValue(Limits.WisRaceSexMinMax, Limits.WisClassMin, Limits.WisAgeEffect);
        public StatValue Cha = new StatValue(Limits.ChaRaceSexMinMax, Limits.ChaClassMin, Limits.ChaAgeEffect);

        public void Write(byte[] data, int offset)
        {
            Str.Write(data, offset + 0x00);
            Int.Write(data, offset + 0x02);
            Wis.Write(data, offset + 0x04);
            Dex.Write(data, offset + 0x06);
            Con.Write(data, offset + 0x08);
            Cha.Write(data, offset + 0x0a);
            Str00.Write(data, offset + 0x0c);
        }

        public void Read(byte[] data, int offset)
        {
            Str.Read(data, offset + 0x00);
            Int.Read(data, offset + 0x02);
            Wis.Read(data, offset + 0x04);
            Dex.Read(data, offset + 0x06);
            Con.Read(data, offset + 0x08);
            Cha.Read(data, offset + 0x0a);
            Str00.Read(data, offset + 0x0c);
        }

        public void Assign(PlayerStats ps)
        {
            Str.Assign(ps.Str);
            Int.Assign(ps.Int);
            Wis.Assign(ps.Wis);
            Dex.Assign(ps.Dex);
            Con.Assign(ps.Con);
            Cha.Assign(ps.Cha);
            Str00.Assign(ps.Str00);
        }

        public void Dec(int idx)
        {
            switch (idx)
            {
                case 0: Str.Dec(); break;
                case 1: Int.Dec(); break;
                case 2: Wis.Dec(); break;
                case 3: Dex.Dec(); break;
                case 4: Con.Dec(); break;
                case 5: Cha.Dec(); break;
                default: throw new IndexOutOfRangeException(string.Format("idx {0} not in [0-5]", idx));
            }
        }

        public void Inc(int idx)
        {
            switch (idx)
            {
                case 0: Str.Inc(); break;
                case 1: Int.Inc(); break;
                case 2: Wis.Inc(); break;
                case 3: Dex.Inc(); break;
                case 4: Con.Inc(); break;
                case 5: Cha.Inc(); break;
                default: throw new IndexOutOfRangeException(string.Format("idx {0} not in [0-5]", idx));
            }
        }

        public StatValue this[int idx]
        {
            get
            {
                switch (idx)
                {
                    case 0: return Str;
                    case 1: return Int;
                    case 2: return Wis;
                    case 3: return Dex;
                    case 4: return Con;
                    case 5: return Cha;
                    default: throw new IndexOutOfRangeException(string.Format("idx {0} not in [0-5]", idx));
                }
            }
        }

        public override string ToString()
        {
            return string.Format("S:{0} ({6}),I:{1},W:{2},C:{3},D:{4},Ch:{5}", Str, Int, Wis, Con, Dex, Cha, Str00);
        }
    }
}
