namespace GoldBox.Classes
{
    public class SetBlock
    {
        public SetBlock() { Reset(); }
        public SetBlock(int _setId, int _blockId) { setId = _setId; blockId = _blockId; }
        public void Reset() { setId = -1; blockId = -1; }
        public int blockId; // byte_1D53A[di] 1*4
        public int setId; // byte_1D53C[di] 1*4
    }

}
