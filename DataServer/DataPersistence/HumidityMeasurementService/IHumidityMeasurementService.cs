



using Models;

namespace DataServer.DataPersistence.HumidityMeasurementService;

public interface IHumidityMeasurementService
{
    /// <summary>
    /// Used for graphs in the application
    ///  Returns a list of humidity measurements  within a time span
    /// </summary>
    /// <returns>Returns a list of humidity measurements within a time span</returns>
    Task<IList<HumidityMeasurement>> GetUpdatedHumidityAsync(int greenhouseId,long? fromTime,long? untilTime);
    
    /// <summary>
    ///  Returns the last humidity measurement in the greenhouse 
    /// </summary>
    /// <returns>Returns the last humidity measurement in the greenhouse </returns>
    Task<HumidityMeasurement> GetLastHumidityAsync(int greenhouseId);
    
    /// <summary>
    /// Adds a new humidity measurement from the greenhouse
    /// </summary>
    Task<HumidityMeasurement> AddHumidityAsync(int greenhouseId,HumidityMeasurement humidityMeasurement);
}