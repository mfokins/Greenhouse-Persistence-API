using Core.Interfaces.DioxideCarbon;
using Microsoft.AspNetCore.Mvc;
using Api.Mappers;
using Api.Models;
using Core.Interfaces.Temperature;
using Microsoft.AspNetCore.Mvc;

namespace Api.RestApi.Controllers;

[Route("[controller]/{greenhouseId}")]
[ApiController]
public class DioxideCarbonController : ControllerBase
{
    private IDioxideCarbonService _service;

    public DioxideCarbonController(IDioxideCarbonService service)
    {
        _service = service;
    }


    [HttpGet]
    public IEnumerable<DioxideCarbonMeasurement> Get([FromRoute] string greenhouseId, [FromQuery] bool latest,
        [FromQuery] int page = 0, [FromQuery] int itemsPerPage = 25)
    {
        if (latest)
        {
            return new[] {DomToApi.Convert(_service.GetLatest(greenhouseId))};
        }
        else
        
           {
            return _service.GetAll(greenhouseId, page, itemsPerPage).Select(x => DomToApi.Convert(x));
        }
    }

    [HttpPost]
    public void Post([FromRoute] string greenhouseId, [FromBody] DioxideCarbonMeasurement value)
    {
        var convertedValue = ApiToDom.Convert(value);
        convertedValue.GreenHouseId = greenhouseId;
        _service.Add(convertedValue);
    }
}