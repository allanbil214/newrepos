using System;

namespace TestingOverloadingOperator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hello");
            Anu xAnu = new Anu { value = 1 };
            Anu xAnu2 = new Anu { value = 2 };

            Anu yAnu = xAnu + xAnu2;

            Console.WriteLine(yAnu.value);
        }


    }

    public class Anu
    {
        public int value;

        public static Anu operator +(Anu a, Anu b)
        {
            return new Anu { value = a.value + b.value };
        }
    }
}