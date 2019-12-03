using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace StringCalculator.Tests
{
    [TestClass]
    public class StringCalculator20191203Tests
    {
        [TestMethod]
        public void Add_EmptyString_ReturnsZero()
        {
            var sut = new StringCalculator20191203();

            var result = sut.Add("");

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Add_SingleNumber_ReturnsNumber()
        {
            var sut = new StringCalculator20191203();

            var result = sut.Add("5");

            Assert.AreEqual(5, result);
        }

        [TestMethod]
        [DataRow("1,5", 6)]
        [DataRow("1,2,3,4,5,6,7", 28)]
        [DataRow("20,5", 25)]
        public void Add_MultipleNumbersSeparatedByComma_ReturnsSumOfNumbers(string numbers, int expectedSum)
        {
            var sut = new StringCalculator20191203();

            var result = sut.Add(numbers);

            Assert.AreEqual(expectedSum, result);
        }

        [TestMethod]
        public void Add_NumbersSeparatedByLNewLines_IsAlsoValid()
        {
            var sut = new StringCalculator20191203();

            var result = sut.Add("1\n5");

            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void Add_DelimiterSpecifiedOnFirstLine_UsesSpecifiedDelimiterOnTopOfDefaultDelimiters()
        {
            var sut = new StringCalculator20191203();

            var result = sut.Add("//;\n1;7");

            Assert.AreEqual(8, result);
        }

        [TestMethod]
        [DataRow("1,-7", "-7")]
        [DataRow("1,-7, 2, -3", "-7,-3")]
        public void Add_NegativeValues_ThrowsExceptionWithMessageContainingTheInvalidValues(string numbers, string expectedErrorMessage)
        {
            var sut = new StringCalculator20191203();

            var exception = Assert.ThrowsException<Exception>(() => sut.Add(numbers));

            Assert.IsTrue(exception.Message.Contains(expectedErrorMessage));
        }

        [TestMethod]
        [DataRow("2,1001", 2)]
        [DataRow("1,1000", 1001)]
        [DataRow("102,1234510", 102)]
        public void Add_NumbersGreaterThan1000_AreIgnored(string numbers, int expectedSum)
        {
            var sut = new StringCalculator20191203();

            var result = sut.Add(numbers);

            Assert.AreEqual(expectedSum, result);
        }

        [TestMethod]
        public void Add_MultiCharacterDelimiters_AreValid()
        {
            var sut = new StringCalculator20191203();

            var result = sut.Add("//[***]\n1***5***8");

            Assert.AreEqual(14, result);
        }
    }
}
