using PersonelService.Models;

namespace PersonelService.Services
{
    public interface IRabbitMQService
    {
        void PublishMessage(object message, string queueName);
    }
}
