
using Api.Mappers;
using Api.Models;
using Core.Interfaces.Sensors;
using Microsoft.AspNetCore.Mvc;

namespace Api.RestApi.Controllers
{
    [Route("Greenhouse/{greenhouseId}/SensorStatus")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly ISensorService _sensorService;

        public SensorController(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        [HttpGet]
        public IEnumerable<SensorStatus> Get([FromRoute] string greenhouseId) 
        {
            return _sensorService.GetSensorStatuses(greenhouseId)
                .Select(status => DomToApi.Convert(status));
        }
        ////This is only for the testing purposes
        //[HttpPost]
        //public void Post([FromRoute] string greenhouseId, [FromBody] SensorStatus sensorStatus)
        //{
        //    _sensorService.SetSensorStatus(null, greenhouseId,null);
        //}


    }
}
