using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Api.Models;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http;
using Greenhouse = Core.Models.Greenhouse;

namespace IntegrationTesting;

public class MeasurementControllerTests
{
    protected readonly HttpClient TestClient;
   
    protected MeasurementControllerTests()
    {
        var appFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(GreenHouseDbContext));
                services.AddDbContext<GreenHouseDbContext>(options =>
                {
                    options.UseInMemoryDatabase("GreenHouse"); 
                    
                });
            });
        });
        TestClient = appFactory.CreateClient();
        
    }

    protected async Task<Api.Models.TemperatureMeasurement> CreateTemperatureMeasurementAsync(string GreenhouseId,
        TemperatureMeasurement request)
    {
        var response = await TestClient.PostAsJsonAsync($"Temperature/{GreenhouseId}", request);
        return await response.Content.ReadAsAsync<Api.Models.TemperatureMeasurement>();
    }

    protected async Task<Api.Models.DioxideCarbonMeasurement> CreateDioxideCarbonMeasurementAsync(string GreenhouseId,
        DioxideCarbonMeasurement request)
    {
        var response = await TestClient.PostAsJsonAsync($"Humidity/{GreenhouseId}", request);
        return await response.Content.ReadAsAsync<DioxideCarbonMeasurement>();
    }

    protected async Task<Api.Models.HumidityMeasurement> CreateHumidityMeasurementAsync(string GreenhouseId,HumidityMeasurement request)
    {
        var response = await TestClient.PostAsJsonAsync($"DioxideCarbon/{GreenhouseId}", request);
        return await response.Content.ReadAsAsync<Api.Models.HumidityMeasurement>();
    }

    protected async Task<Api.Models.Pot> CreatePotAsync(string GreenhouseId,Api.Models.Pot request)
    {
        var response = await TestClient.PostAsJsonAsync($"Pot/{GreenhouseId}", request);
        return await response.Content.ReadAsAsync<Api.Models.Pot>();
    }
}