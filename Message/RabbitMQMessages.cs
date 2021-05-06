using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace Message
{
    public abstract class RabbitMQMessages
    {
        public IConnection connection;
        public IModel chanel;
        public void Connect()
        {
            ConnectionFactory factory = new ConnectionFactory() { HostName = ConnectionConstants.HostName };
            connection = factory.CreateConnection();
            chanel = connection.CreateModel();
            chanel.QueueDeclare(ConnectionConstants.QueueName, false, false, false, null);
        }

        public void DisConnect()
        {
            chanel = null;
            if (connection.IsOpen)
                connection.Close();

            connection.Dispose();
            connection = null;
        }
    }
}
