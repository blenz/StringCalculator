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

        [TestCase("5,5", 10)]
        [TestCase("0,0", 0)]
        [TestCase("5 , 5", 10)]
        public void Add_TwoValidNumbers_ReturnSum(string input, int expectedSum)
        {
            var sum = _calc.Add(input);

            Assert.AreEqual(sum, expectedSum);
        }

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

        [TestCase("1,2,3,4,5,6,7,8,9,10,11,12", 78)]
        [TestCase("1,1,1,1,1,1,1,1,1,1", 10)]
        [TestCase("1,2,3", 6)]
        public void Add_ValidNumberList_ReturnSum(string input, int expectedSum)
        {
            int sum = _calc.Add(input);

            Assert.AreEqual(sum, expectedSum);
        }

        [TestCase(",,,,,,,,,", 0)]
        [TestCase("5,,,,,,,,,", 5)]
        [TestCase(",,,,,5,,,,", 5)]
        [TestCase(",,,,,,,,,5", 5)]
        [TestCase("5,,,,,,,,,5", 10)]
        [TestCase("1,,a,bad,a, ,a, ,a,", 1)]
        public void Add_InvalidNumberList_InvalidNumberAsZeroReturnSum(string input, int expectedSum)
        {
            int sum = _calc.Add(input);

            Assert.AreEqual(sum, expectedSum);
        }

        [TestCase("1\n2,3", 6)]
        [TestCase("1,2\n3", 6)]
        [TestCase("1\n2\n3", 6)]
        public void Add_ValidNewLineAsDelimiter_ReturnSum(string input, int expectedSum)
        {
            var sum = _calc.Add(input);

            Assert.AreEqual(sum, expectedSum);
        }

        [TestCase("\n", 0)]
        [TestCase("\n\n", 0)]
        [TestCase("\n,\n", 0)]
        [TestCase("\n,1\n", 1)]
        public void Add_InvalidNewLineAsDelimiter_ReturnSum(string input, int expectedSum)
        {
            var sum = _calc.Add(input);

            Assert.AreEqual(sum, expectedSum);
        }

        [TestCase("-2,0,2", "Negative numbers were found: -2")]
        [TestCase("-2,-2\n-2", "Negative numbers were found: -2, -2, -2")]
        [TestCase("-1", "Negative numbers were found: -1")]
        public void Add_NegativeNumbers_ThrowException(string input, string errorMessage)
        {
            var exception = Assert.Throws<ArgumentException>(() =>
                _calc.Add(input)
            );

            Assert.That(exception.Message, Is.EqualTo(errorMessage));
        }

        [TestCase("1,999,1", 1001)]
        [TestCase("1,1000,1", 1002)]
        [TestCase("1,1001,1", 2)]
        public void Add_NumbersAboveUpperBoundLimit_IgnoreNumbersOverUpperBoundReturnSum(string input, int expectedSum)
        {
            var sum = _calc.Add(input);

            Assert.AreEqual(sum, expectedSum);
        }

        [TestCase("//;\n2;5", 7)]
        [TestCase("//,\n2,5", 7)]
        [TestCase("//.\n2.5", 7)]
        [TestCase("//l\n2l5", 7)]
        [TestCase("//2\n125", 6)]
        [TestCase("///\n1/5", 6)]
        [TestCase("//\n\n1\n5", 6)]
        [TestCase("// \n1 5", 6)]
        [TestCase("//;\n1\n1,1", 3)]
        public void Add_ValidCustomDelimiter_ReturnSum(string input, int expectedSum)
        {
            var sum = _calc.Add(input);

            Assert.AreEqual(sum, expectedSum);
        }

        [TestCase("///;\n2;5", 0)]
        [TestCase("/;\n2;5", 0)]
        [TestCase(",\n2,5", 7)]
        [TestCase("//\n2,5", 7)]
        [TestCase("//;2,5", 5)]
        [TestCase("/,/;\n2;5", 0)]
        [TestCase(",//;\n2;5", 0)]
        public void Add_InvalidCustomDelimiter_ReturnSum(string input, int expectedSum)
        {
            var sum = _calc.Add(input);

            Assert.AreEqual(sum, expectedSum);
        }

        [TestCase("//[***]\n11***22***33", 66)]
        [TestCase("//[[]\n11[22[33", 66)]
        [TestCase("//[']\n11'22'33", 66)]
        [TestCase("//[ ]\n11 22 33", 66)]
        public void Add_ValidCustomLengthDelimiter_ReturnSum(string input, int expectedSum)
        {
            var sum = _calc.Add(input);

            Assert.AreEqual(sum, expectedSum);
        }

        [TestCase("/[***]\n11***22***33", 0)]
        [TestCase("//***]\n11***22***33", 0)]
        [TestCase("//[***\n11***22***33", 0)]
        [TestCase("//[..]11...22...33", 0)]
        [TestCase("[***]\n11***22***33", 0)]
        [TestCase("//***\n11***22***33", 0)]
        [TestCase("//[[***]]\n11***22***33", 0)]
        [TestCase("//[]\n11***22***33", 0)]
        [TestCase("//[...]\n11***,2,3", 5)]
        [TestCase("//[...]\n", 0)]
        public void Add_InvalidCustomLengthDelimiter_ReturnSum(string input, int expectedSum)
        {
            var sum = _calc.Add(input);

            Assert.AreEqual(sum, expectedSum);
        }

        [TestCase("//[*][!!][r9r]\n11r9r22*33!!44", 110)]
        [TestCase("//[,][]][[]\n11,22]33[44", 110)]
        [TestCase("//[][][]\n11,22,33,44", 110)]
        [TestCase("//[][][]\n10\n10,10", 30)]
        [TestCase("//[][][]\n11", 11)]
        public void Add_ValidMultipleCustomLengthDelimiters_ReturnSum(string input, int expectedSum)
        {
            var sum = _calc.Add(input);

            Assert.AreEqual(sum, expectedSum);
        }

        [TestCase("[*][!!][r9r]\n11r9r22*33!!44", 0)]
        [TestCase("/[*][!!][r9r]\n11r9r22*33!!44", 0)]
        [TestCase("//[*][!!][r9r]11r9r22*33!!44", 0)]
        [TestCase("//[*][!!][r9r]11r9r22*33,44", 44)]
        [TestCase("//\n11r9r22*33!!44", 0)]
        [TestCase("//[,][,][,]\n11r9r22*33!!44", 0)]
        public void Add_InvalidMultipleCustomLengthDelimiters_ReturnSum(string input, int expectedSum)
        {
            var sum = _calc.Add(input);

            Assert.AreEqual(sum, expectedSum);
        }

        [TestCase("1,2,3", -4)]
        [TestCase("//;\n1;2;3", -4)]
        [TestCase("//[;;]\n1;;2;;3", -4)]
        [TestCase("//[;;][.]\n1;;2.3", -4)]
        [TestCase("//[;;][.]\n1;;2.3", -4)]
        public void Subtract_ValidInput_ReturnDifference(string input, int expectedDifference)
        {
            var difference = _calc.Subtract(input);

            Assert.AreEqual(difference, expectedDifference);
        }

        [TestCase("1,2,3", 6)]
        [TestCase("1,2,0", 0)]
        [TestCase("//;\n1;2;3", 6)]
        [TestCase("//[;;]\n1;;2;;3", 6)]
        [TestCase("//[;;][.]\n1;;2.3", 6)]
        [TestCase("1,1001,3", 3)]
        [TestCase("1,a,3", 3)]
        public void Multiply_ValidInput_ReturnProduct(string input, int expectedProduct)
        {
            var product = _calc.Mulitply(input);

            Assert.AreEqual(product, expectedProduct);
        }

        [TestCase("20,2,2", 5)]
        [TestCase("//;\n20;2;2", 5)]
        [TestCase("//[;;]\n20;;2;;2", 5)]
        [TestCase("//[;;][.]\n20;;2.2", 5)]
        [TestCase("0,1001,1", 0)]
        [TestCase("0,a,1", 0)]
        public void Divide_ValidInput_ReturnQuotient(string input, int expectedQuotient)
        {
            var quotient = _calc.Divide(input);

            Assert.AreEqual(quotient, expectedQuotient);
        }

        [TestCase("20,2,0")]
        public void Divide_ByZero_ThrowError(string input)
        {
            Assert.Throws<DivideByZeroException>(() =>
                _calc.Divide(input)
            );
        }

        [TestCase("1/2/3", 6)]
        public void AlternativeDelimiter_SetNewDelimiter_ReturnSum(string input, int expectedSum)
        {
            _calc.AlternativeDelimiter = "/";

            var sum = _calc.Add(input);

            Assert.AreEqual(sum, expectedSum);
        }

        [TestCase("1,3,-3", 1)]
        public void DenyNegativeNumbers_SetToFalse_ReturnSum(string input, int expectedSum)
        {
            _calc.DenyNegativeNumbers = false;

            var sum = _calc.Add(input);

            Assert.AreEqual(sum, expectedSum);
        }

        [TestCase("1,9,1", 11)]
        [TestCase("1,10,1", 12)]
        [TestCase("1,11,1", 2)]
        public void UpperBound_SetUpperBound_ReturnSum(string input, int expectedSum)
        {
            _calc.UpperBound = 10;

            var sum = _calc.Add(input);

            Assert.AreEqual(sum, expectedSum);
        }
    }
}
