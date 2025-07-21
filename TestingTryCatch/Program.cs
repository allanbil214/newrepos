using System;
using System.Data;

namespace TestingTryCatch
{
    class Program
    {
        static void Main(string[] args)
        {
            short a = 0;
            short b = 0;
            
            while (true)
            {
                Console.WriteLine("\nhello\n");
                try
                {
                    Console.WriteLine("Input 1: ");
                    a = short.Parse(Console.ReadLine());

                    Console.WriteLine("Input 2: ");
                    b = short.Parse(Console.ReadLine());

                    Division(a, b);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Not a valid Number: {ex.Message}\n");
                }
                catch (OverflowException ex)
                {
                    Console.WriteLine($"That's a largeee numberr... {ex.Message}\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static void Division(short a, short b)
        {
            try
            {
                Console.WriteLine($"Result: {a / b}\n");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Can't divide by ZERO, {ex.Message}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}