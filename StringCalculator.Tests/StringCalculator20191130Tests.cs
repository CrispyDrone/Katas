using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.Tests
{
    [TestClass]
    public class StringCalculator20191130Tests
    {
        [TestMethod]
        public void Add_EmptyString_ReturnsZero()
        {
            var sut = new StringCalculator20191130();

            var sum = sut.Add("");

            Assert.AreEqual(0, sum);
        }

        [TestMethod]
        public void Add_SingleNumber_ReturnsNumber()
        {
            var sut = new StringCalculator20191130();

            var sum = sut.Add("1");

            Assert.AreEqual(1, sum);
        }

        [TestMethod]
        public void Add_TwoNumbers_ReturnsSumOfNumbers()
        {
            var sut = new StringCalculator20191130();

            var sum = sut.Add("1,2");

            Assert.AreEqual(3, sum);
        }

        [TestMethod]
        public void Add_MultipleNumbers_ReturnsSumOfAllNumbers()
        {
            var sut = new StringCalculator20191130();

            var sum = sut.Add("1,2,6,10,11,99");

            Assert.AreEqual(129, sum);
        }

        [TestMethod]
        public void Add_NewLineAsSeparator_IsAlsoValid()
        {
            var sut = new StringCalculator20191130();

            var sum = sut.Add("1\n2,3");

            Assert.AreEqual(6, sum);
        }

        [TestMethod]
        public void Add_DelimiterSpecifiedBeforeNumbers_UsesTheSpecifiedDelimitersOnTopOfDefaultDelimiters()
        {
            var sut = new StringCalculator20191130();

            var sum = sut.Add("//;\n1;2");

            Assert.AreEqual(3, sum);
        }

        [TestMethod]
        public void Add_NegativeNumber_ThrowsExceptionWithNegativeValue()
        {
            var sut = new StringCalculator20191130();

            var exception = Assert.ThrowsException<Exception>(() => sut.Add("1,-1"));

            exception.Message.Contains("-1");
        }
    }
}
