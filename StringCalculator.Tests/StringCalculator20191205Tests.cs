using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.Tests
{
    [TestClass]
    public class StringCalculator20191205Tests
    {
        [TestMethod]
        public void Add_EmptyString_ReturnsZero()
        {
            var sut = new StringCalculator20191205();

            var result = sut.Add("");

            Assert.AreEqual(0, result);

        }

        [TestMethod]
        public void Add_SingleNumber_ReturnsNumber()
        {
            var sut = new StringCalculator20191205();

            var result = sut.Add("9");

            Assert.AreEqual(9, result);
        }

        [TestMethod]
        [DataRow("2,8", 10)]
        [DataRow("1,9,3", 13)]
        public void Add_TwoNumbersSeparatedByAComma_ReturnsSumOfNumbers(string numbers, int expectedSum)
        {
            var sut = new StringCalculator20191205();

            var result = sut.Add(numbers);

            Assert.AreEqual(expectedSum, result);
        }

        [TestMethod]
        public void Add_NumbersSeparatedByNewLines_IsAlsoValid()
        {
            var sut = new StringCalculator20191205();

            var result = sut.Add("1\n8\n2");

            Assert.AreEqual(11, result);
        }

        [TestMethod]
        [DataRow("//;\n9,2", 11)]
        [DataRow("//*\n103*54", 157)]
        [DataRow("//_\n13_4", 17)]
        public void Add_ACustomDelimiterIsSpecifiedOnFirstLine_DelimiterIsUsed(string numbers, int expectedSum)
        {
            var sut = new StringCalculator20191205();

            var result = sut.Add(numbers);

            Assert.AreEqual(expectedSum, result);
        }

        [TestMethod]
        [DataRow("//;\n-9,2", "-9")]
        [DataRow("//*\n103*-54,-3", "-54,-3")]
        [DataRow("//_\n13_4_-39,2,2,-1,-3", "-39,-1,-3")]
        public void Add_NegativeNumbers_ThrowsExceptionWithMessageContainingTheInvalidNumbers(string numbers, string expectedExceptionMessage)
        {
            var sut = new StringCalculator20191205();

            var exception = Assert.ThrowsException<Exception>(() => sut.Add(numbers));

            Assert.IsTrue(exception.Message.Contains(expectedExceptionMessage));
        }

        [TestMethod]
        [DataRow("2,1000", 1002)]
        [DataRow("1,1001", 1)]
        public void Add_NumbersGreaterThan1000_AreIgnored(string numbers, int expectedSum)
        {
            var sut = new StringCalculator20191205();

            var result = sut.Add(numbers);

            Assert.AreEqual(expectedSum, result);
        }

        [TestMethod]
        [DataRow("//[;;;]\n2;;;23;;;2", 27)]
        [DataRow("//[**]\n2**3**1", 6)]
        [DataRow("//[**]\n2**3,1", 6)]
        public void Add_CustomMultiCharacterDelimitersSurroundedBySquareBraces_AreValid(string numbers, int expectedSum)
        {
            var sut = new StringCalculator20191205();

            var result = sut.Add(numbers);

            Assert.AreEqual(expectedSum, result);
        }

        [TestMethod]
        [DataRow("//[;][_]\n2;23_2", 27)]
        [DataRow("//[*][%]\n2%3*1", 6)]
        public void Add_MultipleSingleCharacterDelimitersSurroundedBySquareBraces_AreValid(string numbers, int expectedSum)
        {
            var sut = new StringCalculator20191205();

            var result = sut.Add(numbers);

            Assert.AreEqual(expectedSum, result);
        }
    }
}
