using Core.Interfaces;
using Core.Models;

namespace Core.Interfaces.DioxideCarbon
{

    public interface IDioxideCarbonService : IDataTemplateService<DioxideCarbonMeasurement>
    {
        DioxideCarbonMeasurement GetLatest(string greenhouseId);
    }
}
