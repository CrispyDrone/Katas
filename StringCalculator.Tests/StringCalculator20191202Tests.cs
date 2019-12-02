using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace StringCalculator.Tests
{
    [TestClass]
    public class StringCalculator20191202Tests
    {
        [TestMethod]
        public void Add_EmptyString_ReturnsZero()
        {
            var sut = new StringCalculator20191202();

            var result = sut.Add("");

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Add_SingleNumber_ReturnsSingleNumber()
        {
            var sut = new StringCalculator20191202();

            var result = sut.Add("5");

            Assert.AreEqual(5, result);
        }

        [TestMethod]
        [DataRow("1,2", 3)]
        [DataRow("1,5,10,3", 19)]
        [DataRow("1,2,3,4,5,6", 21)]
        public void Add_TwoNumbersSeparatedByComma_ReturnsSumOfNumbers(string numbers, int expectedSum)
        {
            var sut = new StringCalculator20191202();

            var result = sut.Add(numbers);

            Assert.AreEqual(expectedSum, result);
        }

        [TestMethod]
        public void Add_NewLineDelimiter_IsAlsoValid()
        {
            var sut = new StringCalculator20191202();

            var result = sut.Add("1\n3");

            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void Add_NewLineDelimiterAndCommasAtTheSameTime_ReturnsSumOfNumbers()
        {
            var sut = new StringCalculator20191202();

            var result = sut.Add("1\n3,5");

            Assert.AreEqual(9, result);
        }

        [TestMethod]
        public void Add_DelimiterSpecifiedOnFirstLine_UsesSpecifiedDelimiterOnTopOfDefaults()
        {
            var sut = new StringCalculator20191202();

            var result = sut.Add("//;\n13,5");

            Assert.AreEqual(18, result);
        }

        [TestMethod]
        public void Add_MultiCharacterDelimiters_AreValid()
        {
            var sut = new StringCalculator20191202();

            var result = sut.Add("//;;;\n1;;;3;;;5");

            Assert.AreEqual(9, result);
        }

        [TestMethod]
        [DataRow("1,-5", "-5")]
        [DataRow("1,-5,-6,-10", "-5,-6,-10")]
        public void Add_NegativeNumbers_ThrowsExceptionWithMessageContainingTheInvalidNumbers(string numbers, string expectedErrorMessage)
        {
            var sut = new StringCalculator20191202();

            var exception = Assert.ThrowsException<Exception>(() => sut.Add(numbers));

            Assert.IsTrue(exception.Message.Contains(expectedErrorMessage));
        }

        [TestMethod]
        [DataRow("5,50,10001", 55)]
        [DataRow("1001,2", 2)]
        public void Add_NumbersGreaterThan1000_AreIgnored(string numbers, int expectedSum)
        {
            var sut = new StringCalculator20191202();

            var result = sut.Add(numbers);

            Assert.AreEqual(expectedSum, result);
        }
    }
}
