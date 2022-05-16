
using Core.Models;

namespace Core.Interfaces.DioxideCarbon
{

    public interface IDioxideCarbonRepository : IDataReadRepository<DioxideCarbonMeasurement>,
        IDataWriteRepository<DioxideCarbonMeasurement>
    {
        
        DioxideCarbonMeasurement GetLatest(string greenhouseId);
    }
}