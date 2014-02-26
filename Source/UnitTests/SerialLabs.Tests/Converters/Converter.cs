using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerialLabs.Converters;

namespace SerialLabs.Tests.Converters
{
    /// <summary>
    /// Unit tests for <see cref="Converter"/> class
    /// </summary>
    [TestClass]
    public class ConverterUnitTests
    {
        #region Positive tests
        /// <summary>
        /// Positive unit test.
        /// Perform a number of conversions and check results
        /// </summary>
        [TestMethod, TestCategory("Positive")]
        public void ConverterTests()
        {
            // try a few simple conversions
            Assert.AreEqual("65535", Converter.HexToDec.Convert("FFFF"));
            Assert.AreEqual("109", Converter.BinToDec.Convert("1101101"));

            // this time specifying a base to convert from
            Assert.AreEqual("109", Converter.Convert(NumberBases.Binary, "1101101"));
            Assert.AreEqual("109", Converter.Convert(NumberBases.Decimal, "109"));

            // check to/from base values (radixes)
            Assert.AreEqual(2, Converter.BinToOct.From);
            Assert.AreEqual(8, Converter.BinToOct.To);
            Assert.AreEqual(10, Converter.DecToHex.From);
            Assert.AreEqual(16, Converter.DecToHex.To);

            // create a base 3 --> dec converter (the long way)
            Assert.AreEqual("242", Converter.Convert(3, NumberingSchemes.ZeroToZ, NumberBases.Decimal, NumberingSchemes.ZeroToZ, "22222"));

            // create and use a bin --> dec converter instance
            Converter o1 = Converter.Create(NumberBases.Binary);
            Assert.AreEqual("109", o1.Convert("1101101"));

            // create and use a bin --> hex converter instance
            Converter o2 = Converter.Create(NumberBases.Binary, NumberBases.Hexadecimal);
            Assert.AreEqual("6D", o2.Convert("1101101"));

            // convert to same base
            Assert.AreEqual("567", Converter.Convert(NumberBases.Decimal, NumberBases.Decimal, "567"));
            Assert.AreEqual("999", Converter.Convert(NumberBases.Decimal, NumberBases.Decimal, "999"));
            Assert.AreEqual("FF", Converter.Convert(NumberBases.Hexadecimal, NumberBases.Hexadecimal, "FF"));

            // convert a large number to base 10
            Assert.AreEqual("9223372036854775807", Converter.Convert(NumberBases.Decimal, Int64.MaxValue.ToString()));

            // base 26 (alphabet based) such as Excel column names --> decimal
            Converter o3 = Converter.Create(26, NumberingSchemes.AToZ, NumberBases.Decimal, NumberingSchemes.ZeroToZ);
            Assert.AreEqual("1", o3.Convert("A"));
            Assert.AreEqual("28", o3.Convert("AB"));
            Assert.AreEqual("26", o3.Convert("Z"));
            Assert.AreEqual("702", o3.Convert("ZZ"));

            // check minimum allowed characters
            Assert.AreEqual("0", Converter.Convert(NumberBases.Hexadecimal, NumberBases.Decimal, "00"));
            Assert.AreEqual("1", Converter.Convert(26, NumberingSchemes.AToZ, NumberBases.Decimal, NumberingSchemes.ZeroToZ, "A"));

            // check maximum allowed characters
            Assert.AreEqual("255", Converter.Convert(NumberBases.Hexadecimal, NumberBases.Decimal, "FF"));
            Assert.AreEqual("702", Converter.Convert(26, NumberingSchemes.AToZ, NumberBases.Decimal, NumberingSchemes.ZeroToZ, "ZZ"));

            // check leading padding characters are ignored
            Assert.AreEqual("109", Converter.Convert(NumberBases.Binary, "00000000001101101"));
            Assert.AreEqual("109", Converter.Convert(NumberBases.Decimal, "00000000000000109"));
        }

        #endregion

        #region Negative tests
        /// <summary>
        /// Negative unit test. Test argument validation.
        /// </summary>
        [TestMethod, TestCategory("Negative"), ExpectedException(typeof(ArgumentNullException))]
        public void ConverterArgumentValueNullTest()
        {
            Converter.Convert(NumberBases.Binary, null);
        }

        /// <summary>
        /// Negative unit test. Test argument validation.
        /// </summary>
        [TestMethod, TestCategory("Negative"), ExpectedException(typeof(ArgumentNullException))]
        public void ConverterArgumentValueEmptyTest()
        {
            Converter.Convert(NumberBases.Binary, string.Empty);
        }

        /// <summary>
        /// Negative unit test. Test argument validation.
        /// Check for specific exception message
        /// </summary>
        [TestMethod, TestCategory("Negative")]
        public void ConverterArgumentValueContainsCharactersInvalidForBaseTest()
        {
            try
            {
                Converter.Convert(NumberBases.Binary, "110121");
                Assert.Fail("Expected error did not occur");
            }
            catch (FormatException ex)
            {
                Assert.AreEqual("Value contains character not valid for number base", ex.Message);
            }
        }

        /// <summary>
        /// Negative unit test. Test argument validation.
        /// Check for specific exception message
        /// </summary>
        [TestMethod, TestCategory("Negative")]
        public void ConverterArgumentValueNegativeTest()
        {
            try
            {
                Converter.Convert(NumberBases.Decimal, "-10");
                Assert.Fail("Expected error did not occur");
            }
            catch (FormatException ex)
            {
                Assert.AreEqual("Unsupported character in value string", ex.Message);
            }
        }

        /// <summary>
        /// Negative unit test. Test argument validation.
        /// </summary>
        [TestMethod, TestCategory("Negative"), ExpectedException(typeof(FormatException))]
        public void ConverterArgumentValueFloatTest()
        {
            Converter.Convert(NumberBases.Decimal, "123.45");
        }

        /// <summary>
        /// Negative unit test. Test argument validation.
        /// </summary>
        [TestMethod, TestCategory("Negative"), ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConverterArgumentFromBaseTooLowTest()
        {
            Converter.Convert(1, "1011");
        }

        /// <summary>
        /// Negative unit test. Test argument validation.
        /// </summary>
        [TestMethod, TestCategory("Negative"), ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConverterArgumentFromBaseTooHighTest()
        {
            Converter.Convert(10000, "1101");
        }

        /// <summary>
        /// Negative unit test. Test argument validation.
        /// </summary>
        [TestMethod, TestCategory("Negative"), ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConverterArgumentToBaseTooLowTest()
        {
            Converter.Convert(NumberBases.Binary, 1, "1011");
        }

        /// <summary>
        /// Negative unit test. Test argument validation.
        /// </summary>
        [TestMethod, TestCategory("Negative"), ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConverterArgumentToBaseTooHighTest()
        {
            Converter.Convert(NumberBases.Binary, 10000, "1011");
        }

        /// <summary>
        /// Negative unit test. Test argument validation.
        /// </summary>
        [TestMethod, TestCategory("Negative"), ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ConverterArgumentBaseTooHighForNumberingSchemeTest()
        {
            Converter.Convert(36, NumberingSchemes.AToZ, 35, NumberingSchemes.AToZ, "1011");
        }

        /// <summary>
        /// Negative unit test.
        /// Int64 is the largest supported conversion result. Try to break this
        /// </summary>
        [TestMethod, TestCategory("Negative"), ExpectedException(typeof(OverflowException))]
        public void ConverterOverflowTest()
        {
            Converter.Convert(NumberBases.Decimal, string.Concat("999", Int64.MaxValue));
        }

        #endregion

    }
}
