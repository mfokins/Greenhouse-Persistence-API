using Api.Mappers;
using Api.Models;
using Core.Interfaces;
using Core.Interfaces.Pot;
using Microsoft.AspNetCore.Mvc;

namespace Api.RestApi.Controllers
{
    [Route("[controller]/{greenhouseId}")]

    public class PotController : ControllerBase
    {
        private IPotService _potService;
        private readonly IMoistureService _moistureService;

        public PotController(IPotService potService, IMoistureService moistureService)
        {

            _potService = potService;
            _moistureService = moistureService;
        }
        [HttpGet]
        public IEnumerable<Pot> Get([FromRoute] string greenhouseId, [FromQuery] int page = 0, [FromQuery] int itemsPerPage = 25)
        {
            var pots = _potService.GetAll(greenhouseId, page, itemsPerPage).Select(x => DomToApi.Convert(x));
            foreach (var pot in pots)
            {
                pot.LatestMoisture = _moistureService.GetLatest(greenhouseId, pot.Id).Moisture;
            }
            return pots;
        }

        [HttpGet("{PotId:int}")]
        public Pot GetById([FromRoute] string greenhouseId, [FromRoute] int PotId)
        {
            return DomToApi.Convert(_potService.Get(PotId, greenhouseId));
        }

        [HttpPost]
        public void Post([FromRoute] string greenhouseId, [FromBody] Pot value)
        {
            var convertedValue = ApiToDom.Convert(value);
            convertedValue.GreenHouseId = greenhouseId;
            _potService.Add(convertedValue);
        }
        [HttpPatch("{PotId:int}")]
        public void Patch([FromRoute] string greenhouseId, [FromRoute] int PotId, [FromBody] Pot value)
        {
            var convertedValue = ApiToDom.Convert(value);
            convertedValue.GreenHouseId = greenhouseId;
            convertedValue.Id = PotId;
            _potService.Update(convertedValue);
        }
    }
}
