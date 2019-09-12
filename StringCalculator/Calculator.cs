using System;

namespace StringCalculator
{
    public class Calculator
    {
        private int _firstNumber, _secondNumber;

        public Calculator()
        {
            _firstNumber = 0;
            _secondNumber = 0;
        }

        public int Add(string input)
        {
            ParseInput(input);

            return _firstNumber + _secondNumber;
        }

        private void ParseInput(string input)
        {
            var numbers = input.Split(',');

            // Try to parse strings as numbers else use 0
            var firstNumber = numbers.Length >= 1 ? numbers[0] : "0";
            Int32.TryParse(firstNumber, out _firstNumber);

            var secondNumber = numbers.Length >= 2 ? numbers[1] : "0";
            Int32.TryParse(secondNumber, out _secondNumber);
        }
    }
}
