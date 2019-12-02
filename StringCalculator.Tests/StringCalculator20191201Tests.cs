using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace StringCalculator.Tests
{
    [TestClass]
    public class StringCalculator20191201Tests
    {
        [TestMethod]
        public void Add_EmptyString_Returns0()
        {
            var sut = new StringCalculator20191201();

            var result = sut.Add("");

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Add_SingleNumber_ReturnsSingleNumber()
        {
            var sut = new StringCalculator20191201();

            var result = sut.Add("2");

            Assert.AreEqual(2, result);
        }



        [TestMethod]
        [DataRow("1,2", 3)]
        [DataRow("1,2,3,4,5,6,7", 28)]
        public void Add_MultipleNumbersSeparatedByCommas_ReturnsSumOfAllNumbers(string numbers, int expectedSum)
        {
            var sut = new StringCalculator20191201();

            var result = sut.Add(numbers);

            Assert.AreEqual(expectedSum, result);
        }

        [TestMethod]
        public void Add_NumbersSeparatedByNewLine_ReturnsSumOfNumbers()
        {
            var sut = new StringCalculator20191201();

            var result = sut.Add("1\n3");

            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void Add_DelimterSpecifiedBeforeNumbers_UsesSpecifiedDelimiter()
        {
            var sut = new StringCalculator20191201();

            var result = sut.Add("//;\n1;2;5");

            Assert.AreEqual(8, result);
        }

        [TestMethod]
        public void Add_NegativeNumbers_ThrowsExceptionWithMessageContainingTheInvalidValues()
        {
            var sut = new StringCalculator20191201();

            var exception = Assert.ThrowsException<Exception>(() => sut.Add("1,-2,-5"));

            Assert.IsTrue(exception.Message.Contains("-5"));
            Assert.IsTrue(exception.Message.Contains("-2"));
        }
    }
}
