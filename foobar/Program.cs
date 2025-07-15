namespace Foobar
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int maxLimit = 30;

            for (int i = 1; i <= maxLimit; i++)
            {
                if (i % 3 == 0 & i % 5 == 0) Console.Write("foobar");
                else if (i % 3 == 0) Console.Write("foo");
                else if (i % 5 == 0) Console.Write("bar");
                else Console.Write(i);
                Console.Write(", ");
            }

            Console.WriteLine();
        }
    }
}