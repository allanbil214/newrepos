using System;

namespace Calculator
{
    class Program
    {
        enum orIntDouble : byte {Integer = 1, Double = 2}
        int typeSelect = 0;

        static void Main()
        {
            Program program = new Program();
            program.Intro();
        }

        void Intro()
        {
            Console.WriteLine("\n==== Calculator ====");
            typeSelect = 0;
            while (typeSelect == 0)
            {
                Console.WriteLine("[i] Choose your option:");
                Console.WriteLine("[1] Integer");
                Console.WriteLine("[2] Decimal");
                typeSelect = TypeSelector(Console.ReadLine());
            }

            while (true)
            {
                Console.WriteLine("\n[i] Choose your option:");
                Console.WriteLine("[1] Addition");
                Console.WriteLine("[2] Subtraction");
                Console.WriteLine("[3] Multiplication");
                Console.WriteLine("[4] Division");
                Console.WriteLine("[=] Select: ");
                AritmaticSelector(Console.ReadLine());
            }

        }

        int TypeSelector(string input)
        {
            switch (input)
            {
                case "1":
                    return (int)orIntDouble.Integer;
                case "2":
                    return (int)orIntDouble.Double;
                default:
                    Console.WriteLine("[!] Invalid input!");
                    return 0;
            }
        }

        void AritmaticSelector(string input)
        {
            switch (input)
            {
                case "1":
                    Addition();
                    break;
                case "2":
                    Subtract();
                    break;
                case "3":
                    Multiply();
                    break;
                case "4":
                    Division();
                    break;
                default:
                    Console.WriteLine("[!] Invalid input!");
                    break;
            }
        }

        void Addition()
        {
            string val1;
            string val2;

            Console.WriteLine("Value 1: ");
            val1 = Console.ReadLine();

            Console.WriteLine("Value 2: ");
            val2 = Console.ReadLine();

            var intCalc = new Calc<int>();
            var doubleCalc = new Calc<double>();

            if (typeSelect == (int)orIntDouble.Integer)
            {
                intCalc.OnCalculationComplete += result => Console.WriteLine($"Result: {result}");
                intCalc.OnError += error => Console.WriteLine($"Error: {error}");
                intCalc.Add(IntTryParsing(val1), IntTryParsing(val2));
            }
            else if (typeSelect == (int)orIntDouble.Double)
            {
                doubleCalc.OnCalculationComplete += result => Console.WriteLine($"Result: {result}");
                doubleCalc.OnError += error => Console.WriteLine($"Error: {error}");
                doubleCalc.Add(DoubleTryParsing(val1), DoubleTryParsing(val2));
            }
        }

       void Subtract()
        {
            string val1;
            string val2;

            Console.WriteLine("Value 1: ");
            val1 = Console.ReadLine();

            Console.WriteLine("Value 2: ");
            val2 = Console.ReadLine();

            var intCalc = new Calc<int>();
            var doubleCalc = new Calc<double>();

            if (typeSelect == (int)orIntDouble.Integer)
            {
                intCalc.OnCalculationComplete += result => Console.WriteLine($"Result: {result}");
                intCalc.OnError += error => Console.WriteLine($"Error: {error}");
                intCalc.Subtract(IntTryParsing(val1), IntTryParsing(val2));
            }
            else if (typeSelect == (int)orIntDouble.Double)
            {
                doubleCalc.OnCalculationComplete += result => Console.WriteLine($"Result: {result}");
                doubleCalc.OnError += error => Console.WriteLine($"Error: {error}");
                doubleCalc.Subtract(DoubleTryParsing(val1), DoubleTryParsing(val2));
            }
        }

       void Multiply()
        {
            string val1;
            string val2;

            Console.WriteLine("Value 1: ");
            val1 = Console.ReadLine();

            Console.WriteLine("Value 2: ");
            val2 = Console.ReadLine();

            var intCalc = new Calc<int>();
            var doubleCalc = new Calc<double>();

            if (typeSelect == (int)orIntDouble.Integer)
            {
                intCalc.OnCalculationComplete += result => Console.WriteLine($"Result: {result}");
                intCalc.OnError += error => Console.WriteLine($"Error: {error}");
                intCalc.Multiply(IntTryParsing(val1), IntTryParsing(val2));
            }
            else if (typeSelect == (int)orIntDouble.Double)
            {
                doubleCalc.OnCalculationComplete += result => Console.WriteLine($"Result: {result}");
                doubleCalc.OnError += error => Console.WriteLine($"Error: {error}");
                doubleCalc.Multiply(DoubleTryParsing(val1), DoubleTryParsing(val2));
            }
        }

       void Division()
        {
            string val1;
            string val2;

            Console.WriteLine("Value 1: ");
            val1 = Console.ReadLine();

            Console.WriteLine("Value 2: ");
            val2 = Console.ReadLine();

            var intCalc = new Calc<int>();
            var doubleCalc = new Calc<double>();

            if (typeSelect == (int)orIntDouble.Integer)
            {
                intCalc.OnCalculationComplete += result => Console.WriteLine($"Result: {result}");
                intCalc.OnError += error => Console.WriteLine($"Error: {error}");
                intCalc.Divide(IntTryParsing(val1), IntTryParsing(val2));
            }
            else if (typeSelect == (int)orIntDouble.Double)
            {
                doubleCalc.OnCalculationComplete += result => Console.WriteLine($"Result: {result}");
                doubleCalc.OnError += error => Console.WriteLine($"Error: {error}");
                doubleCalc.Divide(DoubleTryParsing(val1), DoubleTryParsing(val2));
            }
        }

        public int IntTryParsing(string msg)
        {
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int value))
                {

                    return value;
                }
                else
                {
                    Console.WriteLine("[!] Womp Womp, unrecognized input :()");
                }
            }
        }

        public double DoubleTryParsing(string msg)
        {
            while (true)
            {
                if (double.TryParse(Console.ReadLine(), out double value))
                {
                    
                    return value;
                }
                else
                {
                    Console.WriteLine("[!] Womp Womp, unrecognized input :()");
                }
            }
        }

        void Testing()
        {
            var intCalc = new Calc<int>();
            var doubleCalc = new Calc<double>();

            intCalc.OnCalculationComplete += result => Console.WriteLine($"Result: {result}");
            intCalc.OnError += error => Console.WriteLine($"Error: {error}");

            intCalc.Add(10, 5);
            intCalc.Multiply(4, 3);

            doubleCalc.OnCalculationComplete += result => Console.WriteLine($"Result: {result:F2}");

            doubleCalc.Divide(10.0, 3.0);
            doubleCalc.Subtract(5.5, 1.5);

            Calc<int>.OperationDelegate power = (a, b) => (int)Math.Pow(a, b);
            var powerResult = intCalc.CustomOps(2, 3, power);

            var modResult = intCalc.CustomOps(10, 3, (a, b) => a % b);
        }
    }
}
