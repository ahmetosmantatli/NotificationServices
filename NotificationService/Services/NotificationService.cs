using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using NotificationService.Configurations;
using NotificationService.DTOs;
using System.Net;
using System.Net.Mail;

namespace NotificationService.Services
{
    /*Bu yapıyı kullanarak, EmailSettings'i appsettings.json'dan alıp, NotificationService içinde kullanabilirsiniz.
     Özet:
     EmailSettings sınıfını oluşturduk.
     Program.cs içinde EmailSettings'i yapılandırarak uygulamaya dahil ettik.
     NotificationService içinde EmailSettings'i kullanarak e-posta gönderme işlemini gerçekleştirebiliriz.
     *  
     */
    public class NotificationService : INotificationService
    {
        private readonly EmailSettings _emailSettings;
        private readonly SmtpClient _smtpClient;

        public NotificationService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;

            if (_emailSettings == null)
            {
                throw new ArgumentNullException("Email settings cannot be null");
            }

            // Port numarasının geçerli olduğundan emin olun
            int port = _emailSettings.Port == 0 ? 587 : _emailSettings.Port;

            _smtpClient = new SmtpClient(_emailSettings.SmtpServer)
            {
                Port = port,
                Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.SenderPassword),
                EnableSsl = _emailSettings.EnableSsl
            };
        }

        public async Task SendEmail(NotificationDto notificationDto)
        {
            //burdan patlıyo
           
            //email gondercekken acaba notificationdto dan mı almalı from = newmailadres senderemail kısmı emailsettingsten taşınma ??
            if (string.IsNullOrEmpty(notificationDto.RecipientEmail))
            {
                throw new ArgumentNullException("RecipientEmail cannot be null or empty");
            }

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.SenderEmail),
                Subject = notificationDto.Subject,
                Body = notificationDto.Body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(new MailAddress(notificationDto.RecipientEmail));

            try
            {
                await _smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to send email", ex);
            }   
        }
    }
}
