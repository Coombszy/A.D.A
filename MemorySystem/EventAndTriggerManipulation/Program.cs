using System;

namespace EventAndTriggerManipulation
{
    class Program
    {
        static void Main(string[] args)
        {
            MemoryHandler Test = new MemoryHandler();
            Test.BuildEventList();
            Test.BuildMemoryStructure();
            string text;
            while (true)
            {
                Console.WriteLine("ENTER TEXT");
                text = Console.ReadLine();
                if (Test.GetEventsofActiveNode().Contains(text))
                {
                    Test.Navigate(text);
                    Console.WriteLine(Test.GetTrigger());
                }
            }
            Console.WriteLine("END!");
            Console.ReadLine();
        }
    }
}
