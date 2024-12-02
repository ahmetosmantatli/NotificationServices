using NotificationService.Services;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text.Json;
using NotificationService.Models;
using System.Text;
using NotificationService.Configurations;
using NotificationService.Queue;
var builder = WebApplication.CreateBuilder(args);

// RabbitMQ ayarlarýný yapýlandýrma
builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddLogging();  // Logger'ý ekle
// Email ayarlarýný yapýlandýrma
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

// Dependency Injection ile NotificationService'i ekleme
builder.Services.AddSingleton<INotificationService, NotificationService.Services.NotificationService>();

// RabbitMQ service'inizi ekliyoruz
builder.Services.AddSingleton<RabbitMQService>();

// Uygulama için gerekli diðer servisler
builder.Services.AddControllers();

var app = builder.Build();

// HTTPS yönlendirmesi, eðer uygulamanýz HTTPS kullanacaksa
app.UseHttpsRedirection();

// Yetkilendirme middleware'i, eðer uygulamanýzda yetkilendirme kullanýlacaksa
app.UseAuthorization();

// Uygulamanýn yönlendirilmesi ve denetleyicilerin çalýþmasý için gerekli middleware
app.MapControllers();

// Uygulamanýn çalýþmaya baþlamasý
app.Run();