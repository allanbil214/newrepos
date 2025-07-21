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
            // Foobar(); // + AddJazz
            // Console.WriteLine("\n");
            
            // versus?
            FooBarJazz(); // me personally prefer this :thinking_emoji

            Console.WriteLine("\n");
        }

        static void InputMaxLimit()
        {
            Console.WriteLine("\n[=] Input The Maximum Limit: ");
            if (!int.TryParse(Console.ReadLine(), out maxLimit) || maxLimit <= 0)
            {
                maxLimit = 105;
                Console.WriteLine("[i] Defaulted to 105. \n");
            }
        }

        static void Foobar()
        {
            output = "";
            for (int i = 1; i <= maxLimit; i++)
            {
                if (i % 3 == 0 & i % 5 == 0) Console.Write(output = "foobar" + AddJazz(i));
                else if (i % 3 == 0) Console.Write(output = "foo" + AddJazz(i));
                else if (i % 5 == 0) Console.Write(output = "bar" + AddJazz(i));
                else if (i % 7 == 0) Console.Write(AddJazz(i));
                else Console.Write(i);

                if (i != maxLimit) Console.Write(", ");
            }
        }

        static void FooBarJazz()
        {
            output = "";
            for (int i = 1; i <= maxLimit; i++)
            {
                if (i % 3 != 0 & i % 5 != 0 & i % 7 != 0) Console.Write(i);
                else Console.Write(output + AddFoo(i) + AddBar(i) + AddJazz(i));

                if (i != maxLimit) Console.Write(", ");
            }
        }

        static string AddFoo(int input)
        {
            if (input % 3 == 0) return "foo";
            else return "";
        }

        static string AddBar(int input)
        {
            if (input % 5 == 0) return "bar";
            else return "";
        }

        static string AddJazz(int input)
        {
            if (input % 7 == 0) return "jazz";
            else return "";
        }
    }
}