using System.Collections.Generic;
using Core.Models;

namespace Core.Interfaces.Luminosity
{
    public interface ILuminosityRepository : IDataReadRepository<LuminosityMeasurement>,
        IDataWriteRepository<LuminosityMeasurement>
    {
        IEnumerable<LuminosityMeasurement> GetAll(string greenhouseId);

        LuminosityMeasurement GetLatest(string greenhouseId);
    }
}