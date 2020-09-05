using System;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Slb.Drilling.ThinRabbit;
using Slb.Drilling.ThinRabbit.Implementation;

namespace Receive4
{
    public class Receive4
    {
        public static void Main()
        {
            System.DateTime timeStart = DateTime.UtcNow;
            var conn = new RabbitMQConnection("amqp://localhost");
            var receiveChannel = new ReceiveChannel<int>(conn);
            Console.WriteLine(" Ready to go! ");
            receiveChannel.StartAsync(receiveFunc: async message =>
            {
                Console.WriteLine(" Try to [x] Received {0}", message);
                await Task.Delay(1);
                //try
                //{
                    throw new Exception("Fail....");
                    
                    Console.WriteLine(" [x] Received {0}", message);
                //}
                //catch (Exception e)
                //{
                //    System.Console.WriteLine(e.Message + message);
                //    Console.WriteLine(" Fail to handle [x] Received {0}", message);
                //}
            });

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}