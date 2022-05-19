using Microsoft.AspNetCore.Mvc;
using Api.Models;

namespace Api.RestApi.Controllers
{
    [Route("[controller]/{greenhouseId}")]
    [ApiController]
    public class ThresholdController : ControllerBase
    {
        [Route("Temperature")]
        [HttpGet]
        public Threshold GetTemperature()
        {
            return new Threshold();
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
