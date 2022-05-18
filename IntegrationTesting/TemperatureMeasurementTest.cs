using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Api.Models;
using FluentAssertions;
using FluentAssertions.Numeric;
using Xunit;

namespace IntegrationTesting;

public class TemperatureMeasurementTest : MeasurementControllerTests
{
    [Fact]
    public async Task GetLatestTemperatureMeasurement_WithoutAnything()
    {   //Set
        await CreateTemperatureMeasurementAsync(new TemperatureMeasurement {Temperature = 4, Time = 1234311});
        //Act
        var response = await TestClient.GetAsync($"Temperature/{_testGreenhouse.GreenHouseId}?page=0&itemsPerPage=25");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        (await response.Content.ReadAsAsync<Data.Models.Measurements.TemperatureMeasurement>()).Should();
    }

    [Fact]
    public async Task GetLatestTemperature_WithExistingTemperature()
    {
        await CreateTemperatureMeasurementAsync(new TemperatureMeasurement {Temperature = 4, Time = 1234311});
        //Act
        var response = await TestClient.GetAsync($"Temperature/{_testGreenhouse.GreenHouseId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        (response.Content.ReadAsAsync<TemperatureMeasurement>().Result.Temperature).Equals(4);
    }
}