using NotificationService.Services;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text.Json;
using NotificationService.Models;
using System.Text;
using NotificationService.Configurations;
using NotificationService.Queue;
var builder = WebApplication.CreateBuilder(args);

// RabbitMQ ayarlar�n� yap�land�rma
builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddLogging();  // Logger'� ekle
// Email ayarlar�n� yap�land�rma
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

// Dependency Injection ile NotificationService'i ekleme
builder.Services.AddSingleton<INotificationService, NotificationService.Services.NotificationService>();

// RabbitMQ service'inizi ekliyoruz
builder.Services.AddSingleton<RabbitMQService>();

// Uygulama i�in gerekli di�er servisler
builder.Services.AddControllers();

var app = builder.Build();

// HTTPS y�nlendirmesi, e�er uygulaman�z HTTPS kullanacaksa
app.UseHttpsRedirection();

// Yetkilendirme middleware'i, e�er uygulaman�zda yetkilendirme kullan�lacaksa
app.UseAuthorization();

// Uygulaman�n y�nlendirilmesi ve denetleyicilerin �al��mas� i�in gerekli middleware
app.MapControllers();

// Uygulaman�n �al��maya ba�lamas�
app.Run();