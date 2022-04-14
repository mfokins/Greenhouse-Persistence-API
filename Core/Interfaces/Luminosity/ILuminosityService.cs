using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces.Luminosity
{
    public interface ILuminosityService : IDataTemplateService<LuminosityMeasurement>
    {
        LuminosityMeasurement GetLatest(string greenhouseId);
    }
}