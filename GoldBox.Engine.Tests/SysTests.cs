using NUnit.Framework;
using GoldBox.Classes;

namespace GoldBox.Engine
{
    [TestFixture(TestOf =typeof(Sys))]
    public class SysTests
    {
        ObjectVerifier _verifier = new ObjectVerifier();
        [Test]
        public void TestWrapMinMax()
        {
            _verifier.CheckThat(()=> Sys.WrapMinMax(15, 0, 15) == 15 );
            //if value > max : value = min
            _verifier.CheckThat(() => Sys.WrapMinMax(16, 1, 15) == 1);
            //else if value < min : value = max
            _verifier.CheckThat(() => Sys.WrapMinMax(0, 1, 15) == 15);

            _verifier.Verify();
        }
    }
}
