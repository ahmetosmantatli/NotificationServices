namespace NotificationService.Models
{
    public class Notification
    {
        //notification bıldırım atıcak yanı bıldırım atıcagı kısının maılını aldı daha soyut tuttuk eskı ozellıkler yok 
        public string RecipientEmail { get; set; }  // Alıcının e-posta adresi
        public string Subject { get; set; }        // E-posta başlığı
        public string Body { get; set; }           // E-posta içeriği
    }
}
