namespace NotificationService.Configurations
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; }  // smtp.gmail.com
        public int Port { get; set; }  // 587
        public string Username { get; set; }  // Gmail kullanıcı adı
        public string SenderEmail { get; set; }  // Gönderen e-posta adresi
        public string SenderPassword { get; set; }  // Gmail şifresi veya uygulama şifresi
        public bool EnableSsl { get; set; }  // SSL kullanımı

    }
}
