using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NativeStandardControls.TestLib
{
    static class AssertEx
    {
        public static void AreEqual<T>(T[] expected, T[] actual)
        {
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
        public static void AreEqual<T>(T[][] expected, T[][] actual)
        {
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                AreEqual(expected[i], actual[i]);
            }
        }
    }
}
