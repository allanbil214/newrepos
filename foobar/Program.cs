namespace Foobar
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int maxLimit = 15;
            for (int i = 0; i < maxLimit; i++)
            {
                if (i % 3 == 0) Console.WriteLine("foo");
                else if (i % 5 == 0) Console.WriteLine("bar");
                else Console.WriteLine(i);
            }
        }
    }
}