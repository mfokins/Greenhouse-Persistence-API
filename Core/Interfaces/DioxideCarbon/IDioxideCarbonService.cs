using Core.Interfaces;
using Core.Models;

namespace Core.Interfaces.DioxideCarbon
{

    public interface IDioxideCarbonService : IDataTemplateService<DioxideCarbonMeasurement>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="greenhouseId"></param>
        /// <returns></returns>
        DioxideCarbonMeasurement GetLatest(string greenhouseId);
    }
}