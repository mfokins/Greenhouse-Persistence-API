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
using FluentAssertions.Numeric;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Xunit;

namespace IntegrationTesting;

public class TemperatureMeasurementTest : MeasurementControllerTests
{
    public TemperatureMeasurementTest()
    {
       // _testGreenhouse.HumidityMeasurements.
    }

    [Fact]
    public async Task GetLatestTemperatureMeasurement_Null()
    {
        //Set

        //Act
        List<TemperatureMeasurement> model = null;
        TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response =
            await TestClient.GetAsync(
                $"Temperature/{_testGreenhouse.GreenHouseId}?latest=false&page=0&itemsPerPage=25");
        model = await response.Content.ReadAsAsync<List<TemperatureMeasurement>>();
        float temperatureMeasurement = model[0].Temperature;

        //Assert
        Assert.Equal(4, model.Count);
    }

    [Fact]
    public async Task GetLatestTemperatureMeasurement_OneMeasurement()
    {
        //Set
        await CreateTemperatureMeasurementAsync(new TemperatureMeasurement {Temperature = 14, Time = 1234311});

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
        await CreateTemperatureMeasurementAsync(new TemperatureMeasurement {Temperature = 14, Time = 1234311});

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
        await CreateTemperatureMeasurementAsync(new TemperatureMeasurement {Temperature = 14, Time = 1234311});

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
        await CreateTemperatureMeasurementAsync(new TemperatureMeasurement {Temperature = 14, Time = 1234311});

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
        await CreateTemperatureMeasurementAsync(new TemperatureMeasurement {Temperature = 14, Time = 1234311});

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
        await CreateTemperatureMeasurementAsync(new TemperatureMeasurement {Temperature = 14, Time = 1234311});

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
        await CreateTemperatureMeasurementAsync(new TemperatureMeasurement {Temperature = 14, Time = 1234311});

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
        await CreateTemperatureMeasurementAsync(new TemperatureMeasurement {Temperature = 14, Time = 1234311});

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
        await CreateTemperatureMeasurementAsync(new TemperatureMeasurement {Temperature = 4, Time = 1234311});
        //Act
        var response = await TestClient.GetAsync($"Temperature/{_testGreenhouse.GreenHouseId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        (response.Content.ReadAsAsync<TemperatureMeasurement>().Result.Temperature).Equals(4);
    }
}