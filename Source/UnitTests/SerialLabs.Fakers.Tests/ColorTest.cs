using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SerialLabs.Fakers.Tests
{
    [TestClass]
    public class ColorTest
    {
        public void ColorAsIntegerTest()
        {
            for (int i = 0; i < 500; i++)
            {
                int result = Fakers.Color.ColorAsInteger();
                System.Drawing.Color color = System.Drawing.Color.FromArgb(result);
                Assert.IsFalse(color.IsEmpty);
            }
        }
    }
}
