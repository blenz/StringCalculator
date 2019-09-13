using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
            var delimiters = GetCustomDelimiters(ref input);

            // Try to parse strings 
            // as ints else use 0
            var numbers = input.Split(delimiters, StringSplitOptions.None)
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

            // Ignore all numbers greater than 1000
            // by setting them to 0
            _numbers = _numbers.Select(num =>
                {
                    return num <= 1000 ? num : 0;
                })
                .ToList();
        }

        private string[] GetCustomDelimiters(ref string input)
        {
            var delimiters = new HashSet<string>() { ",", "\n" };

            // Check for custom delimiter
            var match = Regex.Match(input, @"^//(\[(.*)\]|(.))\n(.*)");

            if (match.Success)
            {
                // Determine the delimiter type
                var firstMatch = match.Groups[1].Value;

                var isMultiLengthDelimiter =
                    firstMatch.StartsWith('[')
                    && firstMatch.EndsWith(']');

                // Based on the delimiter type,
                // choose the correct group to
                // get the delimiter
                var group = isMultiLengthDelimiter ? 2 : 3;

                delimiters.Add(match.Groups[group].Value);

                // Set input to everything 
                // after the custom delimiter
                input = match.Groups.Last().Value;
            }

            return delimiters.ToArray();
        }
    }
}
