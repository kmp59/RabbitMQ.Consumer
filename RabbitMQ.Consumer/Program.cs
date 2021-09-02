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
                var arguments = new Dictionary<string, object> {};
                arguments.Add("x-queue-type", "quorum");
                channel.QueueDeclare(queue: "demo-queue", durable: true, exclusive: false, autoDelete: false, arguments: arguments);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (sender, e) =>
                {
                    var body = e.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(message);
                };
                channel.BasicConsume("demo-queue", true, consumer);
                Console.ReadLine();
            }
        }
    }
}
