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

namespace IntegrationTesting;

public class MeasurementControllerTests
{
    protected readonly HttpClient TestClient;

    protected readonly Data.Models.Greenhouse _testGreenhouse;

    protected MeasurementControllerTests()
    {
        var appFactory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(GreenHouseDbContext));
                services.AddDbContext<GreenHouseDbContext>(options => { options.UseInMemoryDatabase("GreenHouse"); });
            });
        });
        TestClient = appFactory.CreateClient();
        _testGreenhouse = new Greenhouse();
        _testGreenhouse.GreenHouseId = "testtesttesttesttesttesttest";
        _testGreenhouse.TemperatureMesurments = new List<Data.Models.Measurements.TemperatureMeasurement>();
    }

    protected async Task<Api.Models.TemperatureMeasurement> CreateTemperatureMeasurementAsync(
        TemperatureMeasurement request)
    {
        var response = await TestClient.PostAsJsonAsync($"Temperature/{_testGreenhouse.GreenHouseId}", request);
        return await response.Content.ReadAsAsync<Api.Models.TemperatureMeasurement>();
    }

    protected async Task<Api.Models.DioxideCarbonMeasurement> CreateDioxideCarbonMeasurementAsync(
        TemperatureMeasurement request)
    {
        var response = await TestClient.PostAsJsonAsync($"Humidity/{_testGreenhouse.GreenHouseId}", request);
        return await response.Content.ReadFromJsonAsync<DioxideCarbonMeasurement>();
    }

    protected async Task<Api.Models.HumidityMeasurement> CreateHumidityMeasurementAsync(TemperatureMeasurement request)
    {
        var response = await TestClient.PostAsJsonAsync($"DioxideCarbon/{_testGreenhouse.GreenHouseId}", request);
        return await response.Content.ReadFromJsonAsync<Api.Models.HumidityMeasurement>();
    }
}