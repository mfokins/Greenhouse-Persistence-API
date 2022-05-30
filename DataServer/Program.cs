using Api.BridgeIot;
using Core.Interfaces;
using Core.Interfaces.DioxideCarbon;
using Core.Interfaces.Greenhouse;
using Core.Interfaces.Humidity;
using Core.Interfaces.Pot;
using Core.Interfaces.Sensors;
using Core.Interfaces.Temperature;
using Core.Services;
using Core.Services.Interfaces;
using Data;
using Data.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<GreenHouseDbContext>();

builder.Services.AddScoped<ITemperatureRepository, TemperatureRepository>();
builder.Services.AddScoped<ITemperatureService, TemperatureService>();

builder.Services.AddScoped<IThresholdRepository, ThresholdRepository>();
builder.Services.AddScoped<IThresholdService, ThresholdService>();

builder.Services.AddScoped<IHumidityRepository, HumidityRepository>();
builder.Services.AddScoped<IHumidityService, HumidityService>();

builder.Services.AddScoped<IDioxideCarbonRepository, DioxideCarbonRepository>();
builder.Services.AddScoped<IDioxideCarbonService, DioxideCarbonService>();

builder.Services.AddScoped<IMoistureRepository, MoistureRepository>();
builder.Services.AddScoped<IMoistureService, MoistureService>();

builder.Services.AddScoped<IPotRepository, PotRepository>();
builder.Services.AddScoped<IPotService, PotService>();

builder.Services.AddScoped<ISensorRepository, SensorRepository>();
builder.Services.AddScoped<ISensorService, SensorService>();

builder.Services.AddScoped<IGreenhouseService, GreenhouseService>();
builder.Services.AddScoped<IGreenhouseRepository, GreenhouseRepository>();

builder.Services.AddSingleton<INotificationService, NotificationService>();


//builder.Services.AddHostedService<Class2>();
builder.Services.AddScoped<IMessageHandler, MessageHandler>();
builder.Services.AddHostedService<BridgeMain>();
builder.Services.AddScoped<DownlinkHandler>();
//Class1.testMethod();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
//}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Adding static services 

app.Run();