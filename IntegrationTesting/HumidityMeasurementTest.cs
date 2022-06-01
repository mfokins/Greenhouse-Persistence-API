using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Api.Models;
using Data.Models;
using FluentAssertions;
using FluentAssertions.Common;
using FluentAssertions.Numeric;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Xunit;

namespace IntegrationTesting;

public class HumidityMeasurementTests : MeasurementControllerTests
{
    private Greenhouse _testGreenhouse;

    
    [Fact]
    public async Task GetLatestHumidityMeasurement_Null()
    {
        //Set
        _testGreenhouse = new Greenhouse();
        _testGreenhouse.GreenHouseId = "Qwerty1234567";
        _testGreenhouse.HumidityMeasurements = new List<Data.Models.Measurements.HumidityMeasurement>();
        await CreateHumidityMeasurementAsync(_testGreenhouse.GreenHouseId, new HumidityMeasurement{Humidity = 12, Time = 1234311});
        
        //Act
        List<HumidityMeasurement> model = null;
        TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response =
            await TestClient.GetAsync(
                $"Humidity/{_testGreenhouse.GreenHouseId}?latest=false&page=0&itemsPerPage=25");
        model = await response.Content.ReadAsAsync<List<HumidityMeasurement>>();
        double HumidityMeasurement = model[0].Humidity;

        //Assert
        Assert.Equal(12, HumidityMeasurement);
    }

    [Fact]
    public async Task GetLatestHumidityMeasurement_OneMeasurement()
    {
        //Set
        await CreateHumidityMeasurementAsync(_testGreenhouse.GreenHouseId,
            new HumidityMeasurement {Humidity = 14, Time = 1234311});

        //Act
        List<HumidityMeasurement> model = null;
        TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response =
            await TestClient.GetAsync(
                $"Humidity/{_testGreenhouse.GreenHouseId}?latest=false&page=0&itemsPerPage=25");
        model = await response.Content.ReadAsAsync<List<HumidityMeasurement>>();
        double HumidityMeasurement = model[model.Count - 1].Humidity;

        //Assert
        Assert.Equal(14, HumidityMeasurement);
    }

    [Fact]
    public async Task GetLatestHumidityMeasurement_TwoMeasurements()
    {
        //Set
        await CreateHumidityMeasurementAsync(_testGreenhouse.GreenHouseId,
            new HumidityMeasurement {Humidity = 14, Time = 1234311});

        //Act
        List<HumidityMeasurement> model = null;
        TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response =
            await TestClient.GetAsync(
                $"Humidity/{_testGreenhouse.GreenHouseId}?latest=false&page=0&itemsPerPage=25");
        model = await response.Content.ReadAsAsync<List<HumidityMeasurement>>();
        double HumidityMeasurement = model[model.Count - 1].Humidity;

        //Assert
        Assert.Equal(14, HumidityMeasurement);
    }

    [Fact]
    public async Task GetLatestHumidityMeasurement_MultipleMeasurements()
    {
        //Set
        await CreateHumidityMeasurementAsync(_testGreenhouse.GreenHouseId,
            new HumidityMeasurement {Humidity = 14, Time = 1234311});

        //Act
        List<HumidityMeasurement> model = null;
        TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response =
            await TestClient.GetAsync(
                $"Humidity/{_testGreenhouse.GreenHouseId}?latest=false&page=0&itemsPerPage=25");
        model = await response.Content.ReadAsAsync<List<HumidityMeasurement>>();
        double HumidityMeasurement = model[model.Count - 1].Humidity;

        //Assert
        Assert.Equal(14, HumidityMeasurement);
    }

    [Fact]
    public async Task GetLatestHumidityMeasurement_NonExistingGreenhouse()
    {
        //Set
        await CreateHumidityMeasurementAsync(_testGreenhouse.GreenHouseId,
            new HumidityMeasurement {Humidity = 14, Time = 1234311});

        //Act
        List<HumidityMeasurement> model = null;
        TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response =
            await TestClient.GetAsync(
                $"Humidity/{_testGreenhouse.GreenHouseId}?latest=false&page=0&itemsPerPage=25");
        model = await response.Content.ReadAsAsync<List<HumidityMeasurement>>();
        double HumidityMeasurement = model[model.Count - 1].Humidity;

        //Assert
        Assert.Equal(14, HumidityMeasurement);
    }


    [Fact]
    public async Task GetAllHumidityMeasurement()
    {
        //Set
        await CreateHumidityMeasurementAsync(_testGreenhouse.GreenHouseId,
            new HumidityMeasurement {Humidity = 14, Time = 1234311});

        //Act
        List<HumidityMeasurement> model = null;
        TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response =
            await TestClient.GetAsync(
                $"Humidity/{_testGreenhouse.GreenHouseId}?latest=false&page=0&itemsPerPage=25");
        
        model = await response.Content.ReadAsAsync<List<HumidityMeasurement>>();
        double HumidityMeasurement = model[model.Count - 1].Humidity;

        //Assert
        Assert.Equal(14, HumidityMeasurement);
    }

    [Fact]
    public async Task GetAllHumidityMeasurement_NonExistingGreenhouse()
    {
        //Set
        await CreateHumidityMeasurementAsync(_testGreenhouse.GreenHouseId,
            new HumidityMeasurement {Humidity = 14, Time = 1234311});

        //Act
        List<HumidityMeasurement> model = null;
        TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response =
            await TestClient.GetAsync(
                $"Humidity/{_testGreenhouse.GreenHouseId}?latest=false&page=0&itemsPerPage=25");
        model = await response.Content.ReadAsAsync<List<HumidityMeasurement>>();
        double HumidityMeasurement = model[model.Count - 1].Humidity;

        //Assert
        Assert.Equal(14, HumidityMeasurement);
    }

    [Fact]
    public async Task GetAllHumidityMeasurement_MoreElementsThanExpected()
    {
        //Set
        await CreateHumidityMeasurementAsync(_testGreenhouse.GreenHouseId,
            new HumidityMeasurement {Humidity = 14, Time = 1234311});

        //Act
        List<HumidityMeasurement> model = null;
        TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response =
            await TestClient.GetAsync(
                $"Humidity/{_testGreenhouse.GreenHouseId}?latest=false&page=0&itemsPerPage=25");
        model = await response.Content.ReadAsAsync<List<HumidityMeasurement>>();
        double HumidityMeasurement = model[model.Count - 1].Humidity;

        //Assert
        Assert.Equal(14, HumidityMeasurement);
    }

    [Fact]
    public async Task GetAllHumidityMeasurement_LessElementsThanExpected()
    {
        //Set
        await CreateHumidityMeasurementAsync(_testGreenhouse.GreenHouseId,
            new HumidityMeasurement {Humidity = 14, Time = 1234311});

        //Act
        List<HumidityMeasurement> model = null;
        TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response =
            await TestClient.GetAsync(
                $"Humidity/{_testGreenhouse.GreenHouseId}?latest=false&page=0&itemsPerPage=25");
        model = await response.Content.ReadAsAsync<List<HumidityMeasurement>>();
        double HumidityMeasurement = model[model.Count - 1].Humidity;

        //Assert
        Assert.Equal(14, HumidityMeasurement);
    }

    [Fact]
    public async Task GetLatestHumidity_WithExistingHumidity()
    {
        await CreateHumidityMeasurementAsync(_testGreenhouse.GreenHouseId,
            new HumidityMeasurement {Humidity = 4, Time = 1234311});
        //Act
        var response = await TestClient.GetAsync($"Humidity/{_testGreenhouse.GreenHouseId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        (response.Content.ReadAsAsync<HumidityMeasurement>().Result.Humidity).Equals(4);
    }
}