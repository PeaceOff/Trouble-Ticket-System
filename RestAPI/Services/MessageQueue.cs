using RabbitMQ.Client;
using System;
using System.Text;

namespace RestAPI.Services
{
    public class MessageQueue
    {
        private MessageQueue() {
            int i = 1;
            do
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string message = i + ": Hello World!";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent {0}", message);
                }

                Console.WriteLine(" Press 'q' to exit.");
                i++;
            } while (Console.ReadLine() != "q");
        }
    }
}
