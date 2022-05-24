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
        public Threshold GetHumidity([FromRoute] string greenhouseId)
        {
            return DomToApi.Convert(thresholdService.GetHumidityThresholds(greenhouseId));
        }
        [Route("CO2")]
        [HttpGet]
        public Threshold GetDioxideCarbon([FromRoute] string greenhouseId)
        {
            return DomToApi.Convert(thresholdService.GetDioxideCarbonThresholds(greenhouseId));
        }
        [Route("Temperature")]
        [HttpPatch]
        public void UpdateTemperature([FromRoute] string greenhouseId, [FromBody] Threshold threshold)
        {
            var convertedThershold = ApiToDom.Convert(threshold);
            convertedThershold.Type = Core.Models.ThresholdType.Temperature;
            thresholdService.SetTemperatureThresholds(greenhouseId, convertedThershold);
        }
        [Route("Humidity")]
        [HttpPatch]
        public void UpdateHumidity([FromRoute] string greenhouseId, [FromBody] Threshold threshold)
        {
            var convertedThershold = ApiToDom.Convert(threshold);
            convertedThershold.Type = Core.Models.ThresholdType.Humidity;
            thresholdService.SetHumidityThresholds(greenhouseId, convertedThershold);
        }
        [Route("CO2")]
        [HttpPatch]
        public void UpdateDioxideCarbon([FromRoute] string greenhouseId, [FromBody] Threshold threshold)
        {
            var convertedThershold = ApiToDom.Convert(threshold);
            convertedThershold.Type = Core.Models.ThresholdType.DioxideCarbon;
            thresholdService.SetDioxideCarbonThresholds(greenhouseId, convertedThershold);
        }
    }

}
