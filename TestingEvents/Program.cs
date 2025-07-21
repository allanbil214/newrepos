using System;

namespace TestingEvents
{

    class SomethingEventer
    {
        public delegate void SomethingEventHandler(string msg);

        public event SomethingEventHandler OnSomethingEvented;

        public void DoSomething()
        {
            Console.WriteLine("Something is happening?!");

            OnSomethingEvented?.Invoke("Event is triggered by DoSomething()!");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hello");

            var something = new SomethingEventer();

            something.OnSomethingEvented += EventHappen;

            something.DoSomething();
        }

        static void EventHappen(string msg)
        {
            Console.WriteLine($"Something event: {msg}");
        }
    }
}