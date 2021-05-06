using System;

namespace Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Send Messages!");
            SendProducer send = new SendProducer();
            send.Connect();
            send.SendMessages();
            send.DisConnect();
            Console.ReadLine();
        }
    }
}
