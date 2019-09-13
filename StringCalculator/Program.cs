using System;

namespace StringCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();

            // Read input until termination
            while (true)
            {
                var input = Console.ReadLine();

                calculator.Add(input);
            }
        }
    }
}
