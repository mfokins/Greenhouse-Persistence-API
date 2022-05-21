using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Core.Interfaces;
using Api.Mappers;

namespace Api.RestApi.Controllers;

[ApiController]
[Route("[controller]/{greenhouseId}")]
public class MoistureController : ControllerBase
{
    private IMoistureService _service;

    public MoistureController(IMoistureService service)
    {
        _service = service;
    }

    [HttpGet]
    public IEnumerable<MoistureMeasurement> Get([FromRoute] string greenhouseId, [FromRoute] int potId,
        [FromQuery] bool latest, [FromQuery] int timeinterval, [FromQuery] int page = 0,
        [FromQuery] int itemsPerPage = 25)
    {
        if (latest)
        {
            return new[] {DomToApi.Convert(_service.GetLatest(greenhouseId, potId))};
        }
        else
        {
            return _service.GetAll(greenhouseId, potId, page, itemsPerPage).Select(x => DomToApi.Convert(x));
        }
    }

    //Added so I can test the database
    [HttpPost]
    public void Post([FromRoute] string greenhouseId, [FromBody] MoistureMeasurement value)
    {
        var convertedValue = ApiToDom.Convert(value);
        convertedValue.GreenHouseId = greenhouseId;
        _service.Add(convertedValue);
    }
}