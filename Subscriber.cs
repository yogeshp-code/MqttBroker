using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Text;
using System.Threading.Tasks;

namespace test2
{
    class Subscriber
    {
        static async Task Main(string[] args)
        {
            var mqttfactory = new MqttFactory();
            var client = mqttfactory.CreateMqttClient();
            var options = new MqttClientOptionsBuilder().WithClientId("Yogesh Subscriber")
                .WithTcpServer("localhost", 707).Build();

            client.UseConnectedHandler(async e =>
            {
                Console.WriteLine("Connected To Mqtt");
                var topicFilter = new TopicFilterBuilder().WithTopic("topic1").Build();
                await client.SubscribeAsync(topicFilter);

                var topicFilter1 = new TopicFilterBuilder().WithTopic("Json3").Build();
                await client.SubscribeAsync(topicFilter1);

            });

            client.UseDisconnectedHandler(e =>
            {
                Console.WriteLine("DisConnected To Mqtt");
            });

            client.UseApplicationMessageReceivedHandler(e =>
            {
                Console.WriteLine($"Recieved Message-{Encoding.UTF8.GetString( e.ApplicationMessage.Payload)}");
            });



            await client.ConnectAsync(options);

            Console.WriteLine("Press Any Key To Continue");
            Console.ReadKey();

            

        }
    }
}
