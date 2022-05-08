using Api.BridgeIot;
using Core.Interfaces.Greenhouse;
using Core.Interfaces.Humidity;
using Core.Interfaces.Luminosity;
using Core.Interfaces.Temperature;
using Core.Services;
using Data;
using Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<GreenHouseDbContext>();

builder.Services.AddScoped<ITemperatureRepository, TemperatureRepository>();
builder.Services.AddScoped<ITemperatureService, TemperatureService>();

builder.Services.AddScoped<ILuminosityRepository, LuminosityRepository>();
builder.Services.AddScoped<ILuminosityService, LuminosityService>();

builder.Services.AddScoped<IHumidityRepository, HumidityRepository>();
builder.Services.AddScoped<IHumidityService, HumidityService>();

builder.Services.AddScoped<IGreenhouseService, GreenhouseService>();
builder.Services.AddScoped<IGreenhouseRepository,GreenhouseRepository>();

//builder.Services.AddHostedService<Class2>();
builder.Services.AddHostedService<BridgeMain>();
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


