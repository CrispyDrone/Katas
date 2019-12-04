using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace StringCalculator.Tests
{
    [TestClass]
    public class StringCalculator20191204Tests
    {
        [TestMethod]
        public void Add_EmptyString_ReturnsZero()
        {
            var sut = new StringCalculator20191204();

            var result = sut.Add("");

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Add_SingleNumber_ReturnsNumber()
        {
            var sut = new StringCalculator20191204();

            var result = sut.Add("9");

            Assert.AreEqual(9, result);
        }

        [TestMethod]
        [DataRow("1,9", 10)]
        [DataRow("1,9,2,10,5", 27)]
        public void Add_MultipleNumbersSeparatedByCommas_ReturnsSumOfNumbers(string numbers, int expectedSum)
        {
            var sut = new StringCalculator20191204();

            var result = sut.Add(numbers);

            Assert.AreEqual(expectedSum, result);
        }

        [TestMethod]
        public void Add_NewLineDelimiter_IsAlsoValid()
        {
            var sut = new StringCalculator20191204();

            var result = sut.Add("1\n3,5");

            Assert.AreEqual(9, result);
        }

        [TestMethod]
        public void Add_DelimiterSpecifiedOnFirstLine_UsesSpecifiedDelimiterOnTopOfDefaultDelimiters()
        {
            var sut = new StringCalculator20191204();

            var result = sut.Add("//;\n1;5;10;3,0\n2");

            Assert.AreEqual(21, result);
        }

        [TestMethod]
        public void Add_NegativeValues_ThrowsExceptionWithErrorMessageContainingTheInvalidValues()
        {
            var sut = new StringCalculator20191204();

            var exception = Assert.ThrowsException<Exception>(() => sut.Add("2,-4,10,-2"));

            Assert.IsTrue(exception.Message.Contains("-4,-2"));
        }

        [TestMethod]
        [DataRow("1000,2", 1002)]
        [DataRow("1001,2", 2)]
        public void Add_NumbersGreaterThan1000_AreIgnored(string numbers, int expectedSum)
        {
            var sut = new StringCalculator20191204();

            var result = sut.Add(numbers);

            Assert.AreEqual(expectedSum, result);
        }

        [TestMethod]
        [DataRow("//[;;;]\n1;;;3;;;5,7,0\n12", 28)]
        [DataRow("//[***]\n1***3***5,7,0\n12", 28)]
        public void Add_MultiCharacterDelimiters_AreValid(string numbers, int expectedSum)
        {
            var sut = new StringCalculator20191204();

            var result = sut.Add(numbers);

            Assert.AreEqual(expectedSum, result);
        }

        [TestMethod]
        [DataRow("//[*][%]\n1*2%3", 6)]
        [DataRow("//[**][%][H]\n1**2%3H5", 11)]
        public void Add_MultipleDelimitersSpecified_IsValid(string numbers, int expectedSum)
        {
            var sut = new StringCalculator20191204();

            var result = sut.Add(numbers);

            Assert.AreEqual(expectedSum, result);
        }
    }
}
