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
    }
}
