using System.Collections.Generic;
using System.Linq;
using Api.Mappers;
using Api.Models;
using Core.Interfaces.Luminosity;
using Microsoft.AspNetCore.Mvc;

namespace Api.RestApi.Controllers
{
    [Route("[controller]/{greenhouseId}")]
    [ApiController]
    public class LuminosityController : ControllerBase
    {
        private ILuminosityService _service;

        public LuminosityController(ILuminosityService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id:int}")] //not sure about Query, but followed the design in TemperatureController
        public IEnumerable<LuminosityMeasurement> Get([FromRoute] string greenhouseId, [FromQuery] bool latest)
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

        //Tried to make a look-a-like async example for future discussion below (just for later discussion)

        // [HttpGet]
        // [Route("{id:int}")] 
        // public async Task<ActionResult<IEnumerable<LuminosityMeasurement>>> Get([FromRoute] string greenhouseId, [FromQuery] bool latest)
        // {
        //     try
        //      {
        //          LuminosityMeasurement measurement = await _service.GetLatest(greenhouseId);
        //          return Ok(measurement);
        //      }
        //      catch (Exception e)
        //      {
        //          Console.WriteLine(e);
        //          return StatusCode(500, e.Message);
        //      }
        // }
    }
}