using System;

namespace Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            ReviceMessages producer = new ReviceMessages();
            producer.Connect();
            producer.ConsumeMessages();

            Console.WriteLine("Quitting...");
        }
    }
}
