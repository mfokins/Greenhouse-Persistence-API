
using Models;

namespace DataServer.DataPersistence.DioxideCarbonMeasurementService;

public interface IDioxideCarbonMeasurementService
{
    /// <summary>
    ///  Returns a list of dioxide carbon measurements within a time span
    /// 
    /// </summary>
    /// <returns>Returns a list of dioxide carbon measurements within a time span</returns>
    Task<IList<DioxideCarbonMeasurement>> GetUpdatedDioxideCarbonAsync(int greenhouseId,long? fromTime,long? untilTime);
    
    /// <summary>
    ///  Returns the last dioxide carbon measurement in the greenhouse 
    /// </summary>
    /// <returns>Returns the last added dioxide carbon measurement in the greenhouse </returns>
    Task<DioxideCarbonMeasurement> GetLastDioxideCarbonAsync(int greenhouseId);
    
    /// <summary>
    /// Adds a new dioxide carbon measurement from the greenhouse
    /// </summary>
    Task<DioxideCarbonMeasurement> AddDioxideCarbonAsync(int greenhouseId,DioxideCarbonMeasurement dioxideCarbonMeasurement);
}