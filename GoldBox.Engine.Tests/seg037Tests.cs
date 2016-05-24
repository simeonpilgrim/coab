using GoldBox.Classes;
using GoldBox.Classes.DaxFiles;
using GoldBox.Logging;
using NUnit.Framework;

namespace GoldBox.Engine
{
    [TestFixture(TestOf = typeof(seg037))]
    public class seg037Tests
    {
        private readonly SaveDisplayAsPng _display = new SaveDisplayAsPng(TestContext.CurrentContext.TestDirectory);

        [SetUp]
        public void Setup()
        {
            Config.Setup(TestContext.CurrentContext.TestDirectory);
            _display.Reset();
        }

        [Test]
        public void TestSeg()
        {
            Logger.Setup("seg037Tests");

            gbl.symbol_8x8_set = new DaxBlock[5];
            gbl.dax_8x8d1_201 = new byte[177, 8];
            seg041.Load8x8Tiles();

            seg037.DrawFrame_Outer();
            seg037.draw8x8_02();
            seg037.draw8x8_03();
        }
    }
}
