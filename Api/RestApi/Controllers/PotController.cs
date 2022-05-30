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
            List<Pot> pots = _potService.GetAll(greenhouseId, page, itemsPerPage).Select(x => DomToApi.Convert(x)).ToList();
            //Android team wanted this last minute
            pots.ForEach(x => x.LatestMoisture = _moistureService.GetLatest(greenhouseId, x.Id).Moisture);

            return pots;
        }

        [HttpGet("{PotId:int}")]
        public Pot GetById([FromRoute] string greenhouseId, [FromRoute] int PotId)
        {
            var converted = DomToApi.Convert(_potService.Get(PotId, greenhouseId));
            converted.LatestMoisture = _moistureService.GetLatest(greenhouseId, PotId).Moisture;
            return converted;
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
