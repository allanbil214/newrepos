using System.Text;

namespace Foobar
{
    public class FooBarBuilder
    {
        private Dictionary<int, string> rules = new Dictionary<int, string>
        {
            { 3, "Foo" },
            { 4, "Baz" }, 
            { 5, "Bar" },
            { 7, "Jazz" },
            { 9, "Huzz" }
        };

        public void AddRule(int number, string output)
        {
            rules.Add(number, output);
        }

        public void NewFoobar(int maxLimit)
        {

            StringBuilder sb = new();

            for (int i = 1; i <= maxLimit; i++)
            {
                sb.Clear();
                foreach (var d in rules)
                {
                    if (i % d.Key == 0)
                    {
                        sb.Append(d.Value);
                    }
                }

                string result = sb.Length > 0 ? sb.ToString() : i.ToString();
                Console.Write($"{result}{AddPunctuation(i, maxLimit)} ");
            }
        }

        string AddPunctuation(int current, int max)
        {
            if (current != max) return ",";
            else if (current == max) return ".";
            else return "";
        }

    }
}