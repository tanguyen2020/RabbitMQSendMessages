using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Message;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Sender
{
    public class SendProducer: RabbitMQMessages
    {
        public void SendMessages()
        {
            string startMessage = $"Sending {ConnectionConstants.HostName} messages to {ConnectionConstants.QueueName}";
            Console.WriteLine(startMessage);

            SendGuidMessage();
        }

        private void SendGuidMessage()
        {
            GuidMessage message = new GuidMessage
            {
                Identifier = Guid.NewGuid(),
                Content = String.Format("This is Guid message")
            };

            SendMessage(message);
            Console.WriteLine("Sent Guid message");
        }

        private void SendMessage<T>(T message)
        {
            chanel.BasicPublish(string.Empty, ConnectionConstants.QueueName, null, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)));
        }
    }

}
