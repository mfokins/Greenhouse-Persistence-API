// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using System.Collections.Generic;
using System.Linq;
using Api.Mappers;
using Api.Models;
using Core.Interfaces.Temperature;
using Microsoft.AspNetCore.Mvc;

namespace Api.RestApi.Controllers
{
    [Route("[controller]/{greenhouseId}")]
    [ApiController]
    public class TemperatureController : ControllerBase
    {
        private ITemperatureService _service;

        public TemperatureController(ITemperatureService service)
        {
            _service = service;
        }

        // GET: api/<TemperatureContoller>
        [HttpGet]
        public IEnumerable<TemperatureMeasurement> Get([FromRoute] string greenhouseId, [FromQuery] bool latest, [FromQuery] int page = 0, [FromQuery] int itemsPerPage = 25)
        {
            if (latest)
            {
                return new[] {DomToApi.Convert(_service.GetLatest(greenhouseId))};
            }
            else
            {
                return _service.GetAll(greenhouseId,page, pageSize).Select(x => DomToApi.Convert(x));
            }
        }

        //Added so I can test the database
        [HttpPost]
        public void Post([FromRoute] string greenhouseId, [FromBody] TemperatureMeasurement value)
        {
            var convertedValue = ApiToDom.Convert(value);
            convertedValue.GreenHouseId = greenhouseId;
            _service.Add(convertedValue);
        }
    }
}