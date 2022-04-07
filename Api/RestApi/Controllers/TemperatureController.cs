

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.RestApi.Controllers
{
    [Route("[controller]/{greenhouseId:int}")]
    [ApiController]
    public class TemperatureController : ControllerBase
    {
        // GET: api/<TemperatureContoller>
        [HttpGet]
        public IEnumerable<TemperatureMeasurement> Get([FromQuery] bool latest)
        {
            if (latest)
            {

                return new TemperatureMeasurement[1] {
                    new TemperatureMeasurement()
                    {
                        Temperature =  Random.Shared.Next(-20, 55),
                        Time = DateTimeOffset.Now.ToUnixTimeSeconds()
                    }};
                //return new TemperatureMeasurementcs[] { "value1", "value2" };
            }
            return new TemperatureMeasurement[] {
                    new TemperatureMeasurement()
                    {
                        Temperature =  Random.Shared.Next(-20, 55),
                        Time = DateTimeOffset.Now.ToUnixTimeSeconds()
                    },
                    new TemperatureMeasurement()
                    {
                        Temperature =  Random.Shared.Next(-20, 55),
                        Time = DateTimeOffset.Now.ToUnixTimeSeconds()
                    }};

        }
    }
}