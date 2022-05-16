using Api.Mappers;
using Api.Models;
using Core.Interfaces.Pot;
using Microsoft.AspNetCore.Mvc;

namespace Api.RestApi.Controllers
{
    [Route("[controller]/{greenhouseId}")]
    
    public class PotController :  ControllerBase
    {
        private IPotService _service;

        public PotController(IPotService service)
        {
            _service = service;
        }
        [HttpGet]
        public IEnumerable<Pot> Get([FromRoute] string greenhouseId, [FromQuery] int page = 0, [FromQuery] int itemsPerPage = 25)
        {
                return _service.GetAll(greenhouseId, page, itemsPerPage).Select(x => DomToApi.Convert(x));
        }
        [HttpGet("{PotId:int}")]
        public Pot GetById([FromRoute] string greenhouseId, [FromRoute] int PotId)
        {
            return DomToApi.Convert(_service.Get( PotId, greenhouseId));
        }
        [HttpPost]
        public void Post([FromRoute] string greenhouseId, [FromBody] Pot value)
        {
            var convertedValue = ApiToDom.Convert(value);
            convertedValue.GreenHouseId = greenhouseId;
            _service.Add(convertedValue);
        }
        [HttpPatch("{PotId:int}")]
        public void Patch([FromRoute] string greenhouseId, [FromRoute] int PotId, [FromBody] Pot value)
        {
            var convertedValue = ApiToDom.Convert(value);
            convertedValue.GreenHouseId = greenhouseId;
            convertedValue.Id = PotId;
            _service.Update(convertedValue);
        }
    }
}
