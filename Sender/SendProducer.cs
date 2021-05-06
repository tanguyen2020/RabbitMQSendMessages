using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Message;
using RabbitMQ.Client;

namespace Sender
{
    public class SendProducer
    {
        private IConnection connection;
        private IModel channel;

        public void Connect()
        {
            ConnectionFactory factory = new ConnectionFactory()
            {
                HostName = ConnectionConstants.HostName
            };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.QueueDeclare(ConnectionConstants.QueueName, false, false, false, null);
        }

        public void DisConnect()
        {
            channel = null;
            if (connection.IsOpen)
                connection.Close();

            connection.Dispose();
            connection = null;
        }

        public void SendMessages()
        {
            WriteStartMessage();
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
        private static void WriteStartMessage()
        {
            string startMessage = $"Sending {ConnectionConstants.HostName} messages to {ConnectionConstants.QueueName}";
            Console.WriteLine(startMessage);
        }

        private void SendMessage<T>(T message)
        {
            byte[] messageBody = message.ToByteArray();
            channel.BasicPublish(string.Empty, ConnectionConstants.QueueName, null, messageBody);
        }
    }

}
