using Message;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Receiver
{
    public class ReviceMessages: RabbitMQMessages
    {
        public void ConsumeMessages()
        {
            EventingBasicConsumer consumer = new EventingBasicConsumer(chanel);

            WriteStartMessage();

            consumer.Received += (model, ea) =>
            {
                var body = Encoding.UTF8.GetString(ea.Body.ToArray());
                Console.WriteLine(" [x] Received {0}", body);
                Console.WriteLine(" [x] Done");

                chanel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                try
                {
                    object message = SerializationHelper.ConvertToObject<GuidMessage>(body);
                    Console.WriteLine("Received {0} : {1}", message.GetType().Name, message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed message: {0}", ex);
                }
            };
            chanel.BasicConsume(ConnectionConstants.QueueName, false, consumer);
        }

        private static void WriteStartMessage()
        {
            string startMessage = $"Waiting for messages on {ConnectionConstants.HostName} - queue: {ConnectionConstants.QueueName}. Press 'q' to quit";
            Console.WriteLine(startMessage);
        }
    }
}
