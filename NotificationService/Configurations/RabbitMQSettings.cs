namespace NotificationService.Configurations
{
    public class RabbitMQSettings
    {
        public string Hostname { get; set; }  // RabbitMQ sunucu adresi
        public string Username { get; set; }  // RabbitMQ kullanıcı adı
        public string Password { get; set; }  // RabbitMQ şifresi
        public int Port { get; set; }         // RabbitMQ portu
        public string QueueName { get; set; } // Kuyruğun adı
    }
}
