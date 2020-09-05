using System;
using RabbitMQ.Client;
using System.Text;
using System.Threading;

public class Send
{
    public static void Main()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using(var connection = factory.CreateConnection())
        using(var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "System.Int32#BasicQueue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            int i = 0;
            while (i++ < 30) {
                string message = "Hello World! ";
                var body = Encoding.UTF8.GetBytes(i.ToString());
                channel.BasicPublish(exchange: "",
                                 routingKey: "System.Int32#BasicQueue",
                                 basicProperties: null,
                                 body: body);
                Thread.Sleep(1000);
                Console.WriteLine(" [x] Sent {0}", i);
            }

        }

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }
}