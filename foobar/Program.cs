using System.Diagnostics;
using System.Text;

namespace Foobar
{
    public class Program
    {
        static int maxLimit = 0;
        static int ruleNumber = 0;
        static string ruleOutput = "";
        static FooBarBuilder fb = new FooBarBuilder();

        public static void Main()
        {
            InputMaxLimit();
            HowManyRules();

            // Console.WriteLine(ruleNumber.ToString());
            // Console.WriteLine(ruleOutput);

            fb.NewFoobar(maxLimit);

            Console.WriteLine("\n");
        }

        static void HowManyRules()
        {
            int howMany;
            while (true)
            {
                Console.WriteLine("\n[=] How many new rules do you want?");
                if (int.TryParse(Console.ReadLine(), out howMany) && howMany > 0) break;
                Console.WriteLine("[i] Womp womp, empty or less than 0 number.");
            }

            for (int i = 0; i < howMany; i++)
            {
                Console.WriteLine($"\n[i] Adding new rule number {i+1}.");
                InputNewRules();
            }
        }

        static void InputNewRules()
        {
            while (true)
            {
                Console.WriteLine("\n[=] Input The Number: ");

                if (int.TryParse(Console.ReadLine(), out ruleNumber) && ruleNumber > 0) break;
                Console.WriteLine("[i] Womp womp, empty or less than 0 number.");
            }

            while (true)
            {
                Console.WriteLine("\n[=] Input The Output: ");
                ruleOutput = Console.ReadLine();

                if (!string.IsNullOrEmpty(ruleOutput)) break;
                Console.WriteLine("\n[!] Womp womp, empty string.");
            }

            fb.AddRule(ruleNumber, ruleOutput);
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

    }
}