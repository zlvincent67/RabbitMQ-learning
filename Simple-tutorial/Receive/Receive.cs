using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

class Receive
{
    public static void Main()
    {
        System.DateTime TimeStart = DateTime.UtcNow;
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using(var connection = factory.CreateConnection())
        using(var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "hello",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                System.Console.WriteLine(ea.Redelivered);
                try {
                    System.DateTime TimeNow = DateTime.UtcNow;
                    TimeSpan TimeCount = TimeNow - TimeStart;
                    if(TimeCount.TotalMilliseconds < 1000) {
                        var x = 1/(TimeCount.Seconds - TimeCount.Seconds);
                    }

                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    Console.WriteLine(" [x] Received {0}", message);
                } catch(Exception e)
                {
                    System.Console.WriteLine(e.Message + message);
                    channel.BasicNack(ea.DeliveryTag, true, false);
                }
            };

            channel.BasicConsume(queue: "hello",
                                 autoAck: false,
                                 consumer: consumer);

            
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}