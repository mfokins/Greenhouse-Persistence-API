

using Models;

namespace DataServer.DataPersistence.TemperatureMeasurementService;

public interface ITemperatureMeasurementService
{
    /// <summary>
    ///  Returns a list of temperature measurements within a time span
    /// </summary>
    /// <returns>Returns a list of temperature measurements within a time span</returns>
    Task<IList<TemperatureMeasurement>> GetUpdatedTemperaturesAsync(int greenhouseId,long? fromTime,long? untilTime);
    
    /// <summary>
    ///  Returns the last temperature measurement in the greenhouse 
    /// </summary>
    /// <returns>Returns the last added temperature in the greenhouse</returns>
    Task<TemperatureMeasurement> GetLastTemperatureAsync(int greenhouseId);
    
    /// <summary>
    /// Adds a new temperature measurement from the greenhouse
    /// </summary>
    Task<TemperatureMeasurement> AddTemperatureAsync(int greenhouseId,TemperatureMeasurement temperatureMeasurement);

    

}