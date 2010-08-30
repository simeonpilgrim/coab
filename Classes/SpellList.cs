using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public class SpellItem
    {
        public int Id;
        public bool Learning;

        public SpellItem(int id) { Id = id; Learning = false; }
        public SpellItem(int id, bool learning) { Id = id; Learning = learning; }
    }




    public class SpellList
    {
        public const int SpellListSize = 84;

        List<SpellItem> spells = new List<SpellItem>();

        public void Clear()
        {
            spells.Clear();
        }

        public void ClearSpell(int spellId)
        {
            SpellItem found = null;

            foreach (var sp in spells)
            {
                if (sp.Id == spellId)
                {
                    found = sp;
                    break;
                }
            }

            spells.Remove(found);
        }

        public IEnumerable<int> IdList()
        {
            foreach (var sp in spells)
            {
                yield return sp.Id;
            }
        }

        public IEnumerable<int> LearntList()
        {
            foreach (var sp in spells)
            {
                if (sp.Learning == false)
                {
                    yield return sp.Id;
                }
            }
        }

        public IEnumerable<int> LearningList()
        {
            foreach (var sp in spells)
            {
                if (sp.Learning)
                {
                    yield return sp.Id;
                }
            }
        }

        public void AddLearn(int id)
        {
            spells.Add(new SpellItem(id, true));
        }

        public void AddLearnt(int id)
        {
            spells.Add(new SpellItem(id & 0x7F, id > 0x7f));
        }

        public void MarkLearnt(int id)
        {
            var spell = spells.Find(sp => sp.Id == id && sp.Learning == true);

            if (spell != null)
            {
                spell.Learning = false;
            }
        }

        public bool HasSpells()
        {
            return spells.Count > 0;
        }

        public bool HasSpell(int id)
        {
            return spells.Exists(sp => sp.Id == id);
        }

        public void CancelLearning()
        {
            spells.RemoveAll(sp => sp.Learning == true);
        }

        public void Load(byte[] data, int offset)
        {
            for (int i = 0; i < SpellListSize; i++)
            {
                if (data[offset + i] > 0)
                {
                    AddLearnt(data[offset + i]);
                }
            }
        }

        public void Save(byte[] data, int offset)
        {
            for (int i = 0; i < SpellListSize; i++)
            {
                data[offset + i] = 0;
            }

            int idx = SpellListSize - 1;
            foreach (var sp in spells)
            {
                if (sp.Learning == false)
                {
                    data[offset + idx] = (byte)sp.Id;
                    idx -= 1;
                }
            }
        }
    }
}
