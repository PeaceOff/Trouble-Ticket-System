using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace RestAPI.Services
{
    public static class MessageQueue
    {
        private static ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
        private static string DEPT_NAME = "memes";

        public static void SendMessageToDepartment(string id, string title, string description) {

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: DEPT_NAME,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = JsonConvert.SerializeObject(new DeptMessage(id,title,description));
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: DEPT_NAME,
                                     basicProperties: null,
                                     body: body);
            }
        }

        private class DeptMessage {
            string id;
            string title;
            string description;

            public DeptMessage(string i, string t, string d) {
                id = i;
                title = t;
                description = d;
            }
        }
    }
}
