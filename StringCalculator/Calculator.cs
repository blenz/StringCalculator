using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class Calculator
    {
        private List<int> _numbers;
        private HashSet<string> _delimiters;

        public string AlternativeDelimiter
        {
            set
            {
                _delimiters.Add(value);
            }
        }

        private bool _denyNegativeNumbers;
        public bool DenyNegativeNumbers
        {
            set { _denyNegativeNumbers = value; }
        }

        private int _upperBound;
        public int UpperBound
        {
            set { _upperBound = value; }
        }

        public Calculator()
        {
            _delimiters = new HashSet<string>() { ",", "\n" };
            _denyNegativeNumbers = true;
            _upperBound = 1000;
        }

        public int Add(string input)
        {
            return Calculate(input, '+');
        }

        public int Subtract(string input)
        {
            return Calculate(input, '-');
        }

        public int Mulitply(string input)
        {
            return Calculate(input, '*');
        }

        public int Divide(string input)
        {
            return Calculate(input, '/');
        }

        private int Calculate(string input, char mathOperator)
        {
            // Parse and clean the data
            _numbers = ParseAndValidateInput(input);

            // Determine the operation to calculate
            Func<int, int, int> operation;

            switch (mathOperator)
            {
                case '+':
                    operation = (a, b) => a + b;
                    break;
                case '-':
                    operation = (a, b) => a - b;
                    break;
                case '*':
                    operation = (a, b) => a * b;
                    break;
                case '/':
                    operation = (a, b) => a / b;
                    break;
                default:
                    throw new ArgumentException("Invalid math operation");
            }

            var result = _numbers != null && _numbers.Count > 0
                ? _numbers.Aggregate(operation)
                : 0;

            PrintFormula(mathOperator, result);

            return result;
        }

        private List<int> ParseAndValidateInput(string input)
        {
            if (String.IsNullOrEmpty(input) || String.IsNullOrWhiteSpace(input))
            {
                return null;
            }

            var delimiters = GetCustomDelimiters(ref input);

            // Try to parse strings 
            // as ints else remove them
            var validatedNumbers = input
                .Split(delimiters, StringSplitOptions.None)
                // Filter out strings that are not ints
                .Where(i => { return Int32.TryParse(i, out int number); })
                // Convert string to ints
                .Select(num => Int32.Parse(num))
                // Filter out numbers over upperbound
                .Where(num => { return num <= _upperBound; })
                .ToList();

            if (_denyNegativeNumbers)
            {
                var negativeNumbers = validatedNumbers
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

            return validatedNumbers;
        }

        private string[] GetCustomDelimiters(ref string input)
        {
            // Check for custom delimiter
            var match = Regex.Match(input, @"^//((?:\[(.*?)\])+|.)\n(.*)", RegexOptions.Singleline);

            if (match.Success)
            {
                // Determine the delimiter type
                var firstMatch = match.Groups[1].Value;

                // Check first and last chars
                // to determine type
                var isSingleDelimiter = !(
                    firstMatch.StartsWith('[')
                    && firstMatch.EndsWith(']')
                );

                // Based on the delimiter type,
                // choose the correct group to
                // get delimiters
                var group = isSingleDelimiter ? 1 : 2;

                var captures = match.Groups[group].Captures;
                foreach (Capture capture in captures)
                {
                    _delimiters.Add(capture.Value);
                }

                // Set input to everything 
                // after the custom delimiter
                input = match.Groups.Last().Value;
            }

            return _delimiters.ToArray();
        }

        private void PrintFormula(char mathOperation, int result)
        {
            if (_numbers == null || _numbers.Count == 0)
            {
                return;
            }

            var formula = String.Join(mathOperation, _numbers.ToArray()).TrimEnd();

            Console.WriteLine(String.Format("{0} = {1}", formula, result));
        }
    }
}
