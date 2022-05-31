
using Core.Interfaces;
using Core.Interfaces.DioxideCarbon;
using Core.Interfaces.Greenhouse;
using Core.Interfaces.Humidity;
using Core.Interfaces.Pot;
using Core.Interfaces.Sensors;
using Core.Interfaces.Temperature;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Data
{
    public static class RepositoryServiceExtensions
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<ITemperatureRepository, TemperatureRepository>();
            services.AddScoped<IThresholdRepository, ThresholdRepository>();
            services.AddScoped<IHumidityRepository, HumidityRepository>();
            services.AddScoped<IDioxideCarbonRepository, DioxideCarbonRepository>();
            services.AddScoped<IMoistureRepository, MoistureRepository>();
            services.AddScoped<IPotRepository, PotRepository>();
            services.AddScoped<ISensorRepository, SensorRepository>();
            services.AddScoped<IGreenhouseRepository, GreenhouseRepository>();

            return services;
        }
    }
}
