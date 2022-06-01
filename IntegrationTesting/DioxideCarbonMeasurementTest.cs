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

public class DioxideCarbonMeasurementTests : MeasurementControllerTests
{
    private Greenhouse _testGreenhouse;

    
    [Fact]
    public async Task GetLatestDioxideCarbonMeasurement_Null()
    {
        //Set
        _testGreenhouse = new Greenhouse();
        _testGreenhouse.GreenHouseId = "Qwerty1234567";
        _testGreenhouse.DioxideCarbonMeasurements = new List<Data.Models.Measurements.DioxideCarbonMeasurement>();
        await CreateDioxideCarbonMeasurementAsync(_testGreenhouse.GreenHouseId, new DioxideCarbonMeasurement(){Co2Measurement = 12, Time = 1234311});
        
        //Act
        List<DioxideCarbonMeasurement> model = null;
        TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response =
            await TestClient.GetAsync(
                $"DioxideCarbon/{_testGreenhouse.GreenHouseId}?latest=false&page=0&itemsPerPage=25");
        model = await response.Content.ReadAsAsync<List<DioxideCarbonMeasurement>>();
        float dioxideCarbonMeasurement = model[0].Co2Measurement;

        //Assert
        Assert.Equal(12, dioxideCarbonMeasurement);
        
    }

    [Fact]
    public async Task GetLatestDioxideCarbonMeasurement_OneMeasurement()
    {
        //Set
        await CreateDioxideCarbonMeasurementAsync(_testGreenhouse.GreenHouseId,
            new DioxideCarbonMeasurement {Co2Measurement = 14, Time = 1234311});

        //Act
        List<DioxideCarbonMeasurement> model = null;
        TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response =
            await TestClient.GetAsync(
                $"DioxideCarbon/{_testGreenhouse.GreenHouseId}?latest=false&page=0&itemsPerPage=25");
        model = await response.Content.ReadAsAsync<List<DioxideCarbonMeasurement>>();
        float DioxideCarbonMeasurement = model[model.Count - 1].Co2Measurement;

        //Assert
        Assert.Equal(14, DioxideCarbonMeasurement);
    }

    [Fact]
    public async Task GetLatestDioxideCarbonMeasurement_TwoMeasurements()
    {
        //Set
        await CreateDioxideCarbonMeasurementAsync(_testGreenhouse.GreenHouseId,
            new DioxideCarbonMeasurement {Co2Measurement = 14, Time = 1234311});

        //Act
        List<DioxideCarbonMeasurement> model = null;
        TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response =
            await TestClient.GetAsync(
                $"DioxideCarbon/{_testGreenhouse.GreenHouseId}?latest=false&page=0&itemsPerPage=25");
        model = await response.Content.ReadAsAsync<List<DioxideCarbonMeasurement>>();
        float DioxideCarbonMeasurement = model[model.Count - 1].Co2Measurement;

        //Assert
        Assert.Equal(14, DioxideCarbonMeasurement);
    }

    [Fact]
    public async Task GetLatestDioxideCarbonMeasurement_MultipleMeasurements()
    {
        //Set
        await CreateDioxideCarbonMeasurementAsync(_testGreenhouse.GreenHouseId,
            new DioxideCarbonMeasurement {Co2Measurement = 14, Time = 1234311});

        //Act
        List<DioxideCarbonMeasurement> model = null;
        TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response =
            await TestClient.GetAsync(
                $"DioxideCarbon/{_testGreenhouse.GreenHouseId}?latest=false&page=0&itemsPerPage=25");
        model = await response.Content.ReadAsAsync<List<DioxideCarbonMeasurement>>();
        float DioxideCarbonMeasurement = model[model.Count - 1].Co2Measurement;

        //Assert
        Assert.Equal(14, DioxideCarbonMeasurement);
    }

    [Fact]
    public async Task GetLatestDioxideCarbonMeasurement_NonExistingGreenhouse()
    {
        //Set
        await CreateDioxideCarbonMeasurementAsync(_testGreenhouse.GreenHouseId,
            new DioxideCarbonMeasurement {Co2Measurement = 14, Time = 1234311});

        //Act
        List<DioxideCarbonMeasurement> model = null;
        TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response =
            await TestClient.GetAsync(
                $"DioxideCarbon/{_testGreenhouse.GreenHouseId}?latest=false&page=0&itemsPerPage=25");
        model = await response.Content.ReadAsAsync<List<DioxideCarbonMeasurement>>();
        float DioxideCarbonMeasurement = model[model.Count - 1].Co2Measurement;

        //Assert
        Assert.Equal(14, DioxideCarbonMeasurement);
    }


    [Fact]
    public async Task GetAllDioxideCarbonMeasurement()
    {
        //Set
        await CreateDioxideCarbonMeasurementAsync(_testGreenhouse.GreenHouseId,
            new DioxideCarbonMeasurement {Co2Measurement = 14, Time = 1234311});

        //Act
        List<DioxideCarbonMeasurement> model = null;
        TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response =
            await TestClient.GetAsync(
                $"DioxideCarbon/{_testGreenhouse.GreenHouseId}?latest=false&page=0&itemsPerPage=25");
        
        model = await response.Content.ReadAsAsync<List<DioxideCarbonMeasurement>>();
        float DioxideCarbonMeasurement = model[model.Count - 1].Co2Measurement;

        //Assert
        Assert.Equal(14, DioxideCarbonMeasurement);
    }

    [Fact]
    public async Task GetAllDioxideCarbonMeasurement_NonExistingGreenhouse()
    {
        //Set
        await CreateDioxideCarbonMeasurementAsync(_testGreenhouse.GreenHouseId,
            new DioxideCarbonMeasurement {Co2Measurement = 14, Time = 1234311});

        //Act
        List<DioxideCarbonMeasurement> model = null;
        TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response =
            await TestClient.GetAsync(
                $"DioxideCarbon/{_testGreenhouse.GreenHouseId}?latest=false&page=0&itemsPerPage=25");
        model = await response.Content.ReadAsAsync<List<DioxideCarbonMeasurement>>();
        float DioxideCarbonMeasurement = model[model.Count - 1].Co2Measurement;

        //Assert
        Assert.Equal(14, DioxideCarbonMeasurement);
    }

    [Fact]
    public async Task GetAllDioxideCarbonMeasurement_MoreElementsThanExpected()
    {
        //Set
        await CreateDioxideCarbonMeasurementAsync(_testGreenhouse.GreenHouseId,
            new DioxideCarbonMeasurement {Co2Measurement = 14, Time = 1234311});

        //Act
        List<DioxideCarbonMeasurement> model = null;
        TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response =
            await TestClient.GetAsync(
                $"DioxideCarbon/{_testGreenhouse.GreenHouseId}?latest=false&page=0&itemsPerPage=25");
        model = await response.Content.ReadAsAsync<List<DioxideCarbonMeasurement>>();
        float DioxideCarbonMeasurement = model[model.Count - 1].Co2Measurement;

        //Assert
        Assert.Equal(14, DioxideCarbonMeasurement);
    }

    [Fact]
    public async Task GetAllDioxideCarbonMeasurement_LessElementsThanExpected()
    {
        //Set
        await CreateDioxideCarbonMeasurementAsync(_testGreenhouse.GreenHouseId,
            new DioxideCarbonMeasurement {Co2Measurement = 14, Time = 1234311});

        //Act
        List<DioxideCarbonMeasurement> model = null;
        TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var response =
            await TestClient.GetAsync(
                $"DioxideCarbon/{_testGreenhouse.GreenHouseId}?latest=false&page=0&itemsPerPage=25");
        model = await response.Content.ReadAsAsync<List<DioxideCarbonMeasurement>>();
        float DioxideCarbonMeasurement = model[model.Count - 1].Co2Measurement;

        //Assert
        Assert.Equal(14, DioxideCarbonMeasurement);
    }

    [Fact]
    public async Task GetLatestDioxideCarbon_WithExistingDioxideCarbon()
    {
        await CreateDioxideCarbonMeasurementAsync(_testGreenhouse.GreenHouseId,
            new DioxideCarbonMeasurement {Co2Measurement = 4, Time = 1234311});
        //Act
        var response = await TestClient.GetAsync($"DioxideCarbon/{_testGreenhouse.GreenHouseId}");

        //Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        (response.Content.ReadAsAsync<DioxideCarbonMeasurement>().Result.Co2Measurement).Equals(4);
    }
}