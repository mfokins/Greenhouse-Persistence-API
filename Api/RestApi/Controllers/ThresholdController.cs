using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Core.Interfaces;
using Api.Mappers;

namespace Api.RestApi.Controllers
{
    [Route("[controller]/{greenhouseId}")]
    [ApiController]
    public class ThresholdController : ControllerBase
    {
        private readonly IThresholdService thresholdService;

        public ThresholdController(IThresholdService thresholdService)
        {
            this.thresholdService = thresholdService;
        }

        [Route("Temperature")]
        [HttpGet]
        public Threshold GetTemperature([FromRoute] string greenhouseId)
        {
            return DomToApi.Convert(thresholdService.GetTemperatureThresholds(greenhouseId));
        }
        [Route("Humidity")]
        [HttpGet]
        public Threshold GetHumidity()
        {
            return new Threshold();
        }
        [Route("DioxideCarbon")]
        [HttpGet]
        public Threshold GetDioxideCarbon()
        {
            return new Threshold();
        }
        [Route("Moisture")]
        [HttpGet]
        public MoistureThreshold GetMoisture()
        {
            return new MoistureThreshold();
        }
    }

}
