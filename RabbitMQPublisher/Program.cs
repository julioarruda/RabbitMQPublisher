using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace RabbitMQPublisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "40.78.128.18", UserName = "Teste", Password = "teste" };

            using (var connection = factory.CreateConnection())
            {
                int i = 0;

                while(true)
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(
                            queue: "SampleJA",
                            durable: true,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null
                            );

                        string message = string.Format("Mensagem ID {0}", i);

                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(
                            exchange: "",
                            routingKey: "SampleJA",
                            basicProperties: null,
                            body: body
                            );

                        Console.WriteLine("Mensagem Enviada com Sucesso");
                    }
                    i++;
                }

            }

            Console.ReadLine();
        }
    }
}
