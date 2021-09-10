using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ.Consumer
{
    public static class DirectExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            //Declare Direct Exchage
            channel.ExchangeDeclare("demo-direct-exchange",
                ExchangeType.Direct);

            //Set queue Properties
            var arguments = new Dictionary<string, object> { };
            arguments.Add("x-queue-type", "quorum");

            //Declare Queue with properties
            channel.QueueDeclare(queue: "demo-direct-queue", durable: true, exclusive: false, autoDelete: false, arguments: arguments);

            //Bind Queue with its Exchange with a routing-key
            channel.QueueBind(queue:"demo-direct-queue", exchange:"demo-direct-exchange", routingKey:"account.init");

            //Set Prefetch Count for the consumer to receive specified number of messages.
            channel.BasicQos(prefetchSize: 0, prefetchCount: 2, global: false); //Quorum Queue does not support Global QoS Prefetch

            //Create a consumer for the queue
            var consumer1 = new EventingBasicConsumer(channel);
            var consumer2 = new EventingBasicConsumer(channel);

            //Get Messages from Queue
            consumer1.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message + "consumer1");
            };

            channel.BasicConsume("demo-direct-queue", true, consumer1);

            consumer2.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message + "consumer2");
            };

            channel.BasicConsume("demo-direct-queue", true, consumer2);

            Console.WriteLine("Consumer Started!");
            Console.ReadLine();
        }
    }
}
