using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace RestAPI.Services
{
    public static class MessageQueue
    {
        private static ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
        private static string DEPT_NAME = "genius";

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

        public static void SendAnswerToSolver(string solver, string id, string answer)
        {

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: solver,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = JsonConvert.SerializeObject(new SolverMessage(id, answer));
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: solver,
                                     basicProperties: null,
                                     body: body);
            }
        }

        private class SolverMessage {
            public string id { get; set; }
            public string answer { get; set; }

            public SolverMessage(string i, string a) {
                id = i;
                answer = a;
            }
        }

        private class DeptMessage {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }

            public DeptMessage(string i, string t, string d) {
                id = i;
                title = t;
                description = d;
            }
        }
    }
}
