using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class Calculator
    {
        private List<int> _numbers;

        public Calculator()
        {
            _numbers = new List<int>();
        }

        public int Add(string input)
        {
            ParseInput(input);
            ValidateNumbers();

            return _numbers.Sum();
        }

        private void ParseInput(string input)
        {
            // Try to parse strings 
            // as ints else use 0
            var numbers = input.Split(',', '\n')
                .Select(i =>
                {
                    int number = 0;
                    Int32.TryParse(i, out number);
                    return number;
                });

            _numbers.Clear();
            _numbers.AddRange(numbers);
        }

        private void ValidateNumbers()
        {
            var negativeNumbers = _numbers
                .Where(num => num < 0)
                .ToList();

            // If there are negative numbers
            // throw an exception
            if (negativeNumbers.Count > 0)
            {
                var negativeNumbersStr = string.Join(
                    ", ", negativeNumbers.ToArray()
                ).TrimEnd();

                throw new ArgumentException("Negative numbers were found: " + negativeNumbersStr);
            }
        }
    }
}
