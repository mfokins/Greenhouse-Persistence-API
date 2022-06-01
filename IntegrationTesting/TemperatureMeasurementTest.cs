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

public class TemperatureMeasurementTest : MeasurementControllerTests
{
    private Greenhouse _testGreenhouse;
    [Fact]
    public async Task GetLatestTemperatureMeasurement_Null()
    {
        //Set
        _testGreenhouse = new Greenhouse();
        _testGreenhouse.GreenHouseId = "Qwerty1234567";
        _testGreenhouse.TemperatureMesurments = new List<Data.Models.Measurements.TemperatureMeasurement>();
        await CreateTemperatureMeasurementAsync(_testGreenhouse.GreenHouseId, new TemperatureMeasurement{Temperature = 12, Time = 1234311});
        
        //Act
        List<TemperatureMeasurement> model = null;
        TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response =
            await TestClient.GetAsync(
                $"Temperature/{_testGreenhouse.GreenHouseId}?latest=false&page=0&itemsPerPage=25");
        model = await response.Content.ReadAsAsync<List<TemperatureMeasurement>>();
        float temperatureMeasurement = model[0].Temperature;

        //Assert
        Assert.Equal(12, temperatureMeasurement);
    }

    [Fact]
    public async Task GetLatestTemperatureMeasurement_OneMeasurement()
    {
        //Set
        await CreateTemperatureMeasurementAsync(_testGreenhouse.GreenHouseId,
            new TemperatureMeasurement {Temperature = 14, Time = 1234311});

        //Act
        List<TemperatureMeasurement> model = null;
        TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response =
            await TestClient.GetAsync(
                $"Temperature/{_testGreenhouse.GreenHouseId}?latest=false&page=0&itemsPerPage=25");
        model = await response.Content.ReadAsAsync<List<TemperatureMeasurement>>();
        float temperatureMeasurement = model[model.Count - 1].Temperature;

        //Assert
        Assert.Equal(14, temperatureMeasurement);
    }

    [Fact]
    public async Task GetLatestTemperatureMeasurement_TwoMeasurements()
    {
        //Set
        await CreateTemperatureMeasurementAsync(_testGreenhouse.GreenHouseId,
            new TemperatureMeasurement {Temperature = 14, Time = 1234311});

        //Act
        List<TemperatureMeasurement> model = null;
        TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response =
            await TestClient.GetAsync(
                $"Temperature/{_testGreenhouse.GreenHouseId}?latest=false&page=0&itemsPerPage=25");
        model = await response.Content.ReadAsAsync<List<TemperatureMeasurement>>();
        float temperatureMeasurement = model[model.Count - 1].Temperature;

        //Assert
        Assert.Equal(14, temperatureMeasurement);
    }

    [Fact]
    public async Task GetLatestTemperatureMeasurement_MultipleMeasurements()
    {
        //Set
        await CreateTemperatureMeasurementAsync(_testGreenhouse.GreenHouseId,
            new TemperatureMeasurement {Temperature = 14, Time = 1234311});

        //Act
        List<TemperatureMeasurement> model = null;
        TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response =
            await TestClient.GetAsync(
                $"Temperature/{_testGreenhouse.GreenHouseId}?latest=false&page=0&itemsPerPage=25");
        model = await response.Content.ReadAsAsync<List<TemperatureMeasurement>>();
        float temperatureMeasurement = model[model.Count - 1].Temperature;

        //Assert
        Assert.Equal(14, temperatureMeasurement);
    }

    [Fact]
    public async Task GetLatestTemperatureMeasurement_NonExistingGreenhouse()
    {
        //Set
        await CreateTemperatureMeasurementAsync(_testGreenhouse.GreenHouseId,
            new TemperatureMeasurement {Temperature = 14, Time = 1234311});

        //Act
        List<TemperatureMeasurement> model = null;
        TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response =
            await TestClient.GetAsync(
                $"Temperature/{_testGreenhouse.GreenHouseId}?latest=false&page=0&itemsPerPage=25");
        model = await response.Content.ReadAsAsync<List<TemperatureMeasurement>>();
        float temperatureMeasurement = model[model.Count - 1].Temperature;

        //Assert
        Assert.Equal(14, temperatureMeasurement);
    }


    [Fact]
    public async Task GetAllTemperatureMeasurement()
    {
        //Set
        await CreateTemperatureMeasurementAsync(_testGreenhouse.GreenHouseId,
            new TemperatureMeasurement {Temperature = 14, Time = 1234311});

        //Act
        List<TemperatureMeasurement> model = null;
        TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response =
            await TestClient.GetAsync(
                $"Temperature/{_testGreenhouse.GreenHouseId}?latest=false&page=0&itemsPerPage=25");
        
        model = await response.Content.ReadAsAsync<List<TemperatureMeasurement>>();
        float temperatureMeasurement = model[model.Count - 1].Temperature;

        //Assert
        Assert.Equal(14, temperatureMeasurement);
    }

    [Fact]
    public async Task GetAllTemperatureMeasurement_NonExistingGreenhouse()
    {
        //Set
        await CreateTemperatureMeasurementAsync(_testGreenhouse.GreenHouseId,
            new TemperatureMeasurement {Temperature = 14, Time = 1234311});

        //Act
        List<TemperatureMeasurement> model = null;
        TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response =
            await TestClient.GetAsync(
                $"Temperature/{_testGreenhouse.GreenHouseId}?latest=false&page=0&itemsPerPage=25");
        model = await response.Content.ReadAsAsync<List<TemperatureMeasurement>>();
        float temperatureMeasurement = model[model.Count - 1].Temperature;

        //Assert
        Assert.Equal(14, temperatureMeasurement);
    }

    [Fact]
    public async Task GetAllTemperatureMeasurement_MoreElementsThanExpected()
    {
        //Set
        await CreateTemperatureMeasurementAsync(_testGreenhouse.GreenHouseId,
            new TemperatureMeasurement {Temperature = 14, Time = 1234311});

        //Act
        List<TemperatureMeasurement> model = null;
        TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response =
            await TestClient.GetAsync(
                $"Temperature/{_testGreenhouse.GreenHouseId}?latest=false&page=0&itemsPerPage=25");
        model = await response.Content.ReadAsAsync<List<TemperatureMeasurement>>();
        float temperatureMeasurement = model[model.Count - 1].Temperature;

        //Assert
        Assert.Equal(14, temperatureMeasurement);
    }

    [Fact]
    public async Task GetAllTemperatureMeasurement_LessElementsThanExpected()
    {
        //Set
        await CreateTemperatureMeasurementAsync(_testGreenhouse.GreenHouseId,
            new TemperatureMeasurement {Temperature = 14, Time = 1234311});

        //Act
        List<TemperatureMeasurement> model = null;
        TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response =
            await TestClient.GetAsync(
                $"Temperature/{_testGreenhouse.GreenHouseId}?latest=false&page=0&itemsPerPage=25");
        model = await response.Content.ReadAsAsync<List<TemperatureMeasurement>>();
        float temperatureMeasurement = model[model.Count - 1].Temperature;

        //Assert
        Assert.Equal(14, temperatureMeasurement);
    }

    [Fact]
    public async Task GetLatestTemperature_WithExistingTemperature()
    {
        await CreateTemperatureMeasurementAsync(_testGreenhouse.GreenHouseId,
            new TemperatureMeasurement {Temperature = 4, Time = 1234311});
        //Act
        var response = await TestClient.GetAsync($"Temperature/{_testGreenhouse.GreenHouseId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        (response.Content.ReadAsAsync<TemperatureMeasurement>().Result.Temperature).Equals(4);
    }
}