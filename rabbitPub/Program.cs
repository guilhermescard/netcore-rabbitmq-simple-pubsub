using System;
using RabbitMQ.Client;
using System.Text;

namespace rabbitPub
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() 
            { 
                HostName = "localhost" ,
                VirtualHost = "/",
                UserName = "guest",
                Password = "guest"
            };
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange:"exchange_demo",
                                        type:"fanout",
                                        durable:true);

                string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);
                
                channel.BasicPublish(exchange: "exchange_demo",
                                    routingKey: "demo",
                                    basicProperties: null,
                                    body: body);

                Console.WriteLine("Sent {0}", message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
