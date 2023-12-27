using Core.Abstract;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RaportTestAPI
{
    public class RaportTest
    {
        [Fact]
        public void SendMessage_ShouldPublishMessageToQueue()
        {
            // Arrange
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "user",
                Password = "mypass",
                VirtualHost = "/"
            };

            var message = "Hazýrlanýyor";

            // Act
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("rapor", durable: false, exclusive: false);
                var jsonString = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(jsonString);
                channel.BasicPublish("", "rapor", body: body);
            }

           
        }

        [Fact]
        public void ListenToReports()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "user",
                Password = "mypass",
                VirtualHost = "/"
            };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("rapor", durable: false, exclusive: false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Rapor durumu : {message}");
            };

            channel.BasicConsume("rapor", true, consumer);
        }

    }


}