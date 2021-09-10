using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            using var conn = factory.CreateConnection();
            using(var channel = conn.CreateModel())
            {
                DirectExchangeConsumer.Consume(channel);
            }
        }
    }
}
