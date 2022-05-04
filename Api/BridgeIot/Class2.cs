using Core.Interfaces.Temperature;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Api.BridgeIot
{

    public class Class2 : BackgroundService
    {
        private IServiceScopeFactory _scopeFactory;
        
        public Class2(IServiceScopeFactory factory) =>
        _scopeFactory = factory;
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var x = _scopeFactory.CreateScope().ServiceProvider.GetService<ITemperatureService>().GetAll("test");
                await Task.Delay(1000, stoppingToken);

            }
        }
    }
}
