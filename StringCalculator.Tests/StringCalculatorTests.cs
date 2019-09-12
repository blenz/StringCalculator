using System;
using NUnit.Framework;

namespace StringCalculator.Tests
{
    public class StringCalculatorTests
    {
        private Calculator _calc;

        [SetUp]
        public void Setup()
        {
            _calc = new Calculator();
        }

        [TestCase("1,5000", 5001)]
        [TestCase("0,0", 0)]
        [TestCase("5,5", 10)]
        [TestCase("5 , 5", 10)]
        public void Add_TwoValidNumbers_ReturnSum(string input, int expectedSum)
        {
            var sum = _calc.Add(input);

            Assert.AreEqual(sum, expectedSum);
        }

        [TestCase("1,2,3", 3)]
        [TestCase("5,x", 5)]
        [TestCase("x,5", 5)]
        [TestCase("x,x", 0)]
        [TestCase("5,", 5)]
        [TestCase(",5", 5)]
        [TestCase(" , ", 0)]
        [TestCase(",", 0)]
        public void Add_InvalidNumbers_InvalidNumberAsZeroReturnSum(string input, int expectedSum)
        {
            var sum = _calc.Add(input);

            Assert.AreEqual(sum, expectedSum);
        }

        [TestCase("20")]
        [TestCase("0")]
        public void Add_SingleValidNumber_ReturnNumber(string input)
        {
            var sum = _calc.Add(input);

            Assert.AreEqual(sum, Int32.Parse(input));
        }

        [TestCase("bad")]
        [TestCase(",,")]
        [TestCase("?")]
        [TestCase(" ")]
        [TestCase("")]
        public void Add_SingleInvalidNumber_ReturnZero(string input)
        {
            var sum = _calc.Add(input);

            Assert.AreEqual(sum, 0);
        }
    }
}
