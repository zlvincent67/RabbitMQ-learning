using System;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Slb.Drilling.ThinRabbit;
using Slb.Drilling.ThinRabbit.Implementation;

namespace Receive3
{
    public class Receive3
    {
        public static void Main()
        {
            System.DateTime timeStart = DateTime.UtcNow;
            var conn = new RabbitMQConnection("amqp://localhost");
            var receiveChannel = new ReceiveChannel<int>(conn);
            receiveChannel.StartAsync(receiveFunc: async message =>
            {
                await Task.Delay(1);
                Console.WriteLine(" Try to [x] Received {0}", message);
                //try
                //{
                System.DateTime timeNow = DateTime.UtcNow;
                TimeSpan timeCount = timeNow - timeStart;
                if (timeCount.TotalMilliseconds < 20000)
                {
                    //var x = 1 / (timeCount.Seconds - timeCount.Seconds);
                    //Console.WriteLine(x);
                    throw new Exception("Fail....");
                }

                Console.WriteLine(" [x] Received {0}", message);
                //}
                //catch (Exception e)
                //{
                //    System.Console.WriteLine(e.Message + message);
                //}


            });

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}