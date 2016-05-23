using System;
using System.Linq;
using NUnit.Framework;
using System.Windows.Forms;

namespace Main
{
    [TestFixture]
    public class KeyboardTests
    {
        private Keys[] _mappedKeys;
        private ObjectVerifier _verifier;

        [SetUp]
        public void Setup()
        {
            _verifier = new ObjectVerifier();
            _mappedKeys = new[]
            {
                Keys.A,Keys.B,Keys.C,Keys.D,Keys.E,Keys.F,Keys.G,Keys.H,Keys.I,Keys.J,Keys.K,Keys.L,Keys.M,Keys.N,Keys.O,Keys.P,Keys.Q,Keys.R,Keys.S,Keys.T,Keys.U,Keys.V,Keys.W,Keys.X,Keys.Y,Keys.Z,
                Keys.NumPad1,Keys.NumPad2,Keys.NumPad3,Keys.NumPad4,Keys.NumPad5,Keys.NumPad6,Keys.NumPad7,Keys.NumPad8,Keys.NumPad9,
                Keys.D0,Keys.D1,Keys.D2,Keys.D3,Keys.D4,Keys.D5,Keys.D6,Keys.D7,Keys.D8,Keys.D9,
                Keys.OemOpenBrackets,Keys.OemCloseBrackets,Keys.Oemcomma,Keys.OemPeriod,Keys.OemMinus,
                Keys.Enter,Keys.Space,Keys.Delete,
                Keys.Up,Keys.Down,Keys.Left,Keys.Right,
                Keys.PageUp,Keys.PageDown,
                Keys.Back,Keys.Home,Keys.End,Keys.Escape,
            };
        }

        [Test]
        public void TestAllKeysNotMappedReturns0x20()
        {
            var allKeys = (Keys[])Enum.GetValues(typeof(Keys));
            var unmappedKeys = allKeys.Except(_mappedKeys);
            foreach (var unmappedKey in unmappedKeys)
            {
                VerifyMapping(unmappedKey, 0x20);
            }
            Assert.That(_verifier.VerificationCount, Is.EqualTo(114));
            _verifier.Verify();
        }

        [Test]
        public void TestMappedKeys()
        {
            AssertAllDigitNumbers();
            AssertAllAlphabetKeys();
            AssertAllOtherKeys();
            Assert.That(_verifier.VerificationCount, Is.EqualTo(63));
            _verifier.Verify();
        }

        private void AssertAllOtherKeys()
        {
            VerifyMapping(Keys.NumPad1, 0x4F00);
            VerifyMapping(Keys.NumPad2, 0x5000);
            VerifyMapping(Keys.NumPad3, 0x5100);
            VerifyMapping(Keys.NumPad4, 0x4B00);
            VerifyMapping(Keys.NumPad5, 0x4C00);
            VerifyMapping(Keys.NumPad6, 0x4D00);
            VerifyMapping(Keys.NumPad7, 0x4700);
            VerifyMapping(Keys.NumPad8, 0x4800);
            VerifyMapping(Keys.NumPad9, 0x4900);
            VerifyMapping(Keys.OemOpenBrackets, 0x4700);
            VerifyMapping(Keys.OemCloseBrackets, 0x4F00);
            VerifyMapping(Keys.Oemcomma, 0x2c);
            VerifyMapping(Keys.OemPeriod, 0x2e);
            VerifyMapping(Keys.OemMinus, 0x2d00);
            VerifyMapping(Keys.Enter, 0x1C0D);
            VerifyMapping(Keys.Space, 0x20);
            VerifyMapping(Keys.Delete, 0x5300);
            VerifyMapping(Keys.Up, 0x4800);
            VerifyMapping(Keys.Down, 0x5000);
            VerifyMapping(Keys.Left, 0x4B00);
            VerifyMapping(Keys.Right, 0x4D00);
            VerifyMapping(Keys.PageUp, 0x4900);
            VerifyMapping(Keys.PageDown, 0x5100);
            VerifyMapping(Keys.Back, 0x08);
            VerifyMapping(Keys.Home, 0x4700);
            VerifyMapping(Keys.End, 0x4F00);
            VerifyMapping(Keys.Escape, 0x1b);
        }

        private void AssertAllAlphabetKeys()
        {
            VerifyMapping(Keys.A,0x41);
            VerifyMapping(Keys.B,0x42);
            VerifyMapping(Keys.C,0x43);
            VerifyMapping(Keys.D,0x44);
            VerifyMapping(Keys.E,0x45);
            VerifyMapping(Keys.F,0x46);
            VerifyMapping(Keys.G,0x47);
            VerifyMapping(Keys.H,0x48);
            VerifyMapping(Keys.I,0x49);
            VerifyMapping(Keys.J,0x4A);
            VerifyMapping(Keys.K,0x4B);
            VerifyMapping(Keys.L,0x4C);
            VerifyMapping(Keys.M,0x4D);
            VerifyMapping(Keys.N,0x4E);
            VerifyMapping(Keys.O,0x4F);
            VerifyMapping(Keys.P,0x50);
            VerifyMapping(Keys.Q,0x51);
            VerifyMapping(Keys.R,0x52);
            VerifyMapping(Keys.S,0x53);
            VerifyMapping(Keys.T,0x54);
            VerifyMapping(Keys.U,0x55);
            VerifyMapping(Keys.V,0x56);
            VerifyMapping(Keys.W,0x57);
            VerifyMapping(Keys.X,0x58);
            VerifyMapping(Keys.Y,0x59);
            VerifyMapping(Keys.Z,0x5A);
        }

        private void AssertAllDigitNumbers()
        {
            VerifyMapping(Keys.D0, 0x30);
            VerifyMapping(Keys.D1, 0x31);
            VerifyMapping(Keys.D2, 0x32);
            VerifyMapping(Keys.D3, 0x33);
            VerifyMapping(Keys.D4, 0x34);
            VerifyMapping(Keys.D5, 0x35);
            VerifyMapping(Keys.D6, 0x36);
            VerifyMapping(Keys.D7, 0x37);
            VerifyMapping(Keys.D8, 0x38);
            VerifyMapping(Keys.D9, 0x39);
        }

        private void VerifyMapping(Keys key, short expected)
        {
            _verifier.CheckThat(() => Keyboard.KeyToIBMKey(key) == expected);
        }
    }
}