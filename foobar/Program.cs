namespace Foobar
{
    public class Program
    {
        static int maxLimit = 0;
        static string output = "";
        public static void Main()
        {
            // string test = "asd";
            // Console.WriteLine(test + "anu");

            InputMaxLimit();
            Foobar();

            Console.WriteLine("\n");
        }

        static void InputMaxLimit()
        {
            Console.WriteLine("\n[=] Input The Maximum Limit: ");
            if (!int.TryParse(Console.ReadLine(), out maxLimit))
            {
                maxLimit = 105;
                Console.WriteLine("[i] Defaulted to 105. \n");
            }
        }

        static void Foobar()
        {
            for (int i = 1; i <= maxLimit; i++)
            {
                if (i % 3 == 0 & i % 5 == 0) Console.Write(output = "foobar" + AddJazz(i));
                else if (i % 3 == 0) Console.Write(output = "foo" + AddJazz(i));
                else if (i % 5 == 0) Console.Write(output = "bar" + AddJazz(i));
                else Console.Write(i);

                if (i != maxLimit) Console.Write(", ");
            }
        }

        static string AddJazz(int input)
        {
            if (input % 7 == 0)
            {
                return "jazz";
            }
            else
            {
                return "";
            }
        }
    }
}