using Microsoft.Extensions.Options;
using NotificationService.Configurations;
using NotificationService.Services;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using NotificationService.Models;
using NotificationService.DTOs;


namespace NotificationService.Queue
{
    public class RabbitMQService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly INotificationService _notificationService;

        public RabbitMQService(IConfiguration configuration, INotificationService notificationService)
        {
            _notificationService = notificationService;

            var factory = new ConnectionFactory()
            {
                HostName = configuration["RabbitMQ:Hostname"],
                UserName = configuration["RabbitMQ:Username"],
                Password = configuration["RabbitMQ:Password"],
                Port = int.Parse(configuration["RabbitMQ:Port"])
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            try
            {
                _channel.QueueDeclare(
                    queue: configuration["RabbitMQ:QueueName"], // Kuyruk adı
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
                Console.WriteLine($"Queue '{configuration["RabbitMQ:QueueName"]}' declared successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error declaring queue: {ex.Message}");
            }
        }

        public void StartListening()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Message received: {message}");

                var notification = JsonConvert.DeserializeObject<NotificationDto>(message);

                await _notificationService.SendEmail(notification);
            };

            _channel.BasicConsume(queue: "personnel_update_queue", autoAck: true, consumer: consumer);
            Console.WriteLine("Started listening to queue...");
        }
    }
}
