using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitSandbox
{
    class Program
    {
        private static IConnection _connection;

        static void Main(string[] args)
        {
            var cf = new ConnectionFactory();// { DispatchConsumersAsync = true};
            _connection = cf.CreateConnection();

            Console.WriteLine($"Current thread: {Thread.CurrentThread.ManagedThreadId}");

            var channel = _connection.CreateModel();
            const string routingKey = "rk";
            const string myexchange = "myExchange";
            var consumequeue = "consumeQueue";

            channel.QueueDeclare(consumequeue, true, false, false);
            channel.QueueBind(consumequeue, myexchange, routingKey, null);

            var consumer = new EventingBasicConsumer(channel); //AsyncEventingBasicConsumer(channel);
            consumer.Received += (obj, eventArgs) => 
                Task.Run(() => SendResponse(channel, eventArgs));
            
            channel.BasicConsume(consumequeue, false, consumer);

            while (true)
            {
                Console.ReadLine();
                channel.BasicPublish(myexchange, routingKey, null, Encoding.UTF8.GetBytes("msg1")); //-->consumeQueue
            }
        }

        private static async Task SendResponse(IModel channel, BasicDeliverEventArgs args)
        {
            Console.WriteLine($"On received - Current thread: {Thread.CurrentThread.ManagedThreadId}, processing for 2000ms");

            Thread.Sleep(2000);
            Console.WriteLine($"Sending response from - Current thread: {Thread.CurrentThread.ManagedThreadId}");
            var responseChannel = _connection.CreateModel();
            var body = Encoding.UTF8.GetBytes("msg2");
            const string responseroutingkey = "responseRoutingKey";
            responseChannel.BasicPublish("myExchange", responseroutingkey, null, body);

            channel.BasicAck(args.DeliveryTag, false);

            await Task.Delay(10);
        }
    }
}
