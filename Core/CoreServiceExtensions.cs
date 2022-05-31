
using Core.Interfaces;
using Core.Interfaces.DioxideCarbon;
using Core.Interfaces.Greenhouse;
using Core.Interfaces.Humidity;
using Core.Interfaces.Pot;
using Core.Interfaces.Sensors;
using Core.Interfaces.Temperature;
using Core.Services;
using Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class CoreServiceExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<ITemperatureService, TemperatureService>();
            services.AddScoped<IThresholdService, ThresholdService>();
            services.AddScoped<IHumidityService, HumidityService>();
            services.AddScoped<IDioxideCarbonService, DioxideCarbonService>();
            services.AddScoped<IMoistureService, MoistureService>();
            services.AddScoped<IPotService, PotService>();
            services.AddScoped<ISensorService, SensorService>();
            services.AddScoped<IGreenhouseService, GreenhouseService>();
            services.AddSingleton<INotificationService, NotificationService>();

            return services;
        }
    }
}
