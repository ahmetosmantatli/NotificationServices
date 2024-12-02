using Microsoft.EntityFrameworkCore;
using PersonelService.DbContexts;
using PersonelService.Models;
using PersonelService.Repository;
using PersonelService.Services;
using RabbitMQService = PersonelService.Services.RabbitMQService;
using NotificationService.Services;
using Microsoft.OpenApi.Models;
using AutoMapper;
using PersonelService.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDbContext<PersonnelContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// AutoMapper yapýlandýrmasý
var mapperConfig = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new PersonnelProfile()); // PersonnelProfile'ý ekledik
});
var mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper); // Mapper'ý DI container'a ekledik

// DI servisleri ekleyelim
builder.Services.AddScoped<IPersonnelService, PersonnelService>();
builder.Services.AddScoped<IPersonnelRepository, PersonnelRepository>();
builder.Services.AddScoped<IRabbitMQService, RabbitMQService>();
builder.Services.AddScoped<INotificationService, NotificationService.Services.NotificationService>();

// Swagger yapýlandýrmasý
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Personnel Service API", Version = "v1" });
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Personnel Service API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();