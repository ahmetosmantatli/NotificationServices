using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using PersonelService.Models;
using Newtonsoft.Json;

namespace PersonelService.Services
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly IConfiguration _configuration;

        public RabbitMQService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void PublishMessage(object message, string queueName)
        {
            var hostname = _configuration["RabbitMQ:HostName"];
            var queueNameFromConfig = _configuration["RabbitMQ:QueueName"];  // appsettings'tan kuyruk adı alıyoruz

            var factory = new ConnectionFactory() { HostName = hostname };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queueNameFromConfig,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "",
                                 routingKey: queueNameFromConfig,
                                 basicProperties: null,
                                 body: body);
        }
    }
}
