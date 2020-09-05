using System;
using System.Threading;
using Slb.Drilling.ThinRabbit;
using Slb.Drilling.ThinRabbit.Implementation;

namespace Send
{
    class Send
    {
        public static void Main()
        {
            var conn = new RabbitMQConnection("amqp://localhost");
            var sendChannel = new SendChannel(conn);
            

            int i = 0;
            while (i++ < 30)
            {
                Thread.Sleep(1000);
                Console.WriteLine(" [x] Sent {0}", i);
                sendChannel.Send(i, null);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}