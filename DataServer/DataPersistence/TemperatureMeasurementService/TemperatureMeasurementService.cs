using Models;
using DataServer.Database;
using Microsoft.EntityFrameworkCore;

namespace DataServer.DataPersistence.TemperatureMeasurementService;

public class TemperatureMeasurementService : ITemperatureMeasurementService
{
    private DataDbContext _dbContext;

    public TemperatureMeasurementService(DataDbContext dbContext)
    {
        this._dbContext = dbContext;
    }
    
    public async Task<IList<TemperatureMeasurement>> GetUpdatedTemperaturesAsync(int greenhouseId, long? fromTime, long? untilTime)
    {
        throw new NotImplementedException();
    }

    public Task<TemperatureMeasurement> GetLastTemperatureAsync(int greenhouseId)
    {
        throw new NotImplementedException();
    }

    public Task<TemperatureMeasurement> AddTemperatureAsync(int greenhouseId, TemperatureMeasurement temperatureMeasurement)
    {
        throw new NotImplementedException();
    }
}