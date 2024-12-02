namespace NotificationService.DTOs
{
    public class NotificationDto
    {
        public string RecipientEmail { get; set; }  // Alıcının e-posta adresi
        public string Subject { get; set; }         // E-posta başlığı
        public string Body { get; set; }            // E-posta içeriği
        public string ActionType { get; set; }      // Yapılan işlem türü (ekleme, güncelleme, silme)
        public string PersonnelName { get; set; }   // Personel adı
        public string PersonnelPosition { get; set; } // Personelin pozisyonu
        //public string PersonnelEmail { get; set; }  // Personel e-posta adresi (gerekiyorsa)

    }
}
