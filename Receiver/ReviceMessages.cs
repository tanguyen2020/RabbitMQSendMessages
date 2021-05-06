using Message;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Receiver
{
    public class ReviceMessages
    {
        private IConnection connection;
        private IModel channel;

        public void Connect()
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                HostName = ConnectionConstants.HostName
            };

            connection = factory.CreateConnection();
            channel = connection.CreateModel();

            channel.QueueDeclare(ConnectionConstants.QueueName, false, false, false, null);
        }

        public void ConsumeMessages()
        {
            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

            WriteStartMessage();

            consumer.Received += (model, ea) =>
            {
                var body = Encoding.UTF8.GetString(ea.Body.ToArray());
                Console.WriteLine(" [x] Received {0}", body);

                Console.WriteLine(" [x] Done");

                //channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                try
                {
                    object message = SerializationHelper.FromByteArray(body.ToByteArray());
                    Console.WriteLine("Received {0} : {1}", message.GetType().Name, message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed message: {0}", ex);
                }
            };
            channel.BasicConsume(ConnectionConstants.QueueName, true, consumer);
            connection.Close();
            connection.Dispose();
            connection = null;
        }

        private static void WriteStartMessage()
        {
            string startMessage = string.Format("Waiting for messages on {0}/{1}. Press 'q' to quit",
                ConnectionConstants.HostName, ConnectionConstants.QueueName);
            Console.WriteLine(startMessage);
        }
    }
}
