using System;

namespace StringCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();

            if (args != null && args.Length == 3)
            {
                try
                {
                    calculator.AlternativeDelimiter = args[0];
                    calculator.DenyNegativeNumbers = Boolean.Parse(args[1]);
                    calculator.UpperBound = Int32.Parse(args[2]);
                }
                catch (Exception)
                {
                    Console.WriteLine(
                        "Invalid arguments, must be: alternative-delimiter(string) deny-negative-numbers(boolean) upperbound(integer)"
                    );

                    Environment.Exit(1);
                }
            }

            // Read input until termination
            while (true)
            {
                var input = Console.ReadLine();

                calculator.Add(input);
            }
        }
    }
}
