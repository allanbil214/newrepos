using System;

namespace TestingDelegates
{
    class Program
    {
        delegate int Transformer(int x);
        static int Square(int x) => x * x;
        static int Cube(int x) => x * x * x;

        delegate void SomeDelegate(int x);
        static void Write1(int x) => Console.WriteLine($"{x} = 1");
        static void Write2(int x) => Console.WriteLine($"{x} = 2");

        delegate TResult Transformer<TArgs, TResult>(TArgs args);
        delegate TResult Anu<T1, T2, TResult>(T1 t1, T2 t2);

        static void Main(string[] args)
        {
            tryingGenericDel();
        }

        static void tryingDel1()
        {
            Transformer t = Square;

            int answer = t(2);

            Console.WriteLine("{0}", answer);
        }

        static void tryingPluginDel()
        {
            static void Transform(int[] values, Transformer t) // 't' is a delegate parameter
            {
                for (int i = 0; i < values.Length; i++)
                    values[i] = t(values[i]); // Invoke the plug-in method
            }
            int[] values = { 1, 2, 3 };

            Transform(values, Square); // Use Square method as the plug-in
            foreach (int i in values)
                Console.Write(i + "  "); // Output: 1   4   9
        }

        static void tryingMultiCastDelOnNonVoid()
        {
            Transformer t = Square;
            t += Cube;

            int answer = t(2);

            Console.WriteLine("{0}", answer);
        }

        static void tryingMultiCastDelOnVoid(int x)
        {
            SomeDelegate d = Write1;
            d += Write2;

            d(x);
        }

        static void tryingGenericDel()
        {
            static void Transform<T>(T[] values, Transformer<T, T> t)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    Console.WriteLine(values[i] = t(values[i]));
                }
            }
            int[] values = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            Transform(values, Square);

            Console.WriteLine("\n");

            static void LocalAnu<T>(T value1, T value2, Anu<T, T, T> t)
            {
                Console.WriteLine(t(value1, value2));
                Console.WriteLine(t(value2, value1));
            }
            Anu<int, int, int> sum = (a, b) => a + b;

            LocalAnu(1, 2, sum);
        }
        

    }
}