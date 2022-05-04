using System.Collections.Generic;
using System.Linq;
using Api.Mappers;
using Api.Models;
using Core.Interfaces.Humidity;
using Microsoft.AspNetCore.Mvc;

namespace Api.RestApi.Controllers
{
    [Route("[controller]/{greenhouseId}")]
    [ApiController]
    public class HumidityController:ControllerBase
    {
        private IHumidityService _service;

        public HumidityController(IHumidityService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<HumidityMeasurement> Get([FromRoute] string greenhouseId, [FromQuery] bool latest)
        {
            if (latest)
            {
                return new[] {DomToApi.Convert(_service.GetLatest(greenhouseId))};
            }
            else
            {
                return _service.GetAll(greenhouseId).Select(x => DomToApi.Convert(x));
            }
        }
        [HttpPost]
        public void Post([FromRoute] string greenhouseId, [FromBody] HumidityMeasurement value)
        {
            value.GreenHouseId = greenhouseId;
            _service.Add(ApiToDom.Convert(value));
        }
    }
}