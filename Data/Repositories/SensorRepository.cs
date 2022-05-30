
using Core.Interfaces.Sensors;
using Core.Models;
using Data.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class SensorRepository : ISensorRepository
    {
        public IList<SensorStatus> GetSensorStatusesPot(string greenhouseId)
        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();
            var sensorstatuses = dbContext.Greenhouses
                .Include(g => g.Pots)
                ?.FirstOrDefault(gh => gh.GreenHouseId == greenhouseId)
                ?.Pots
                .Select(pots => DbToDom.Convert(pots.MoistureSensorStatus)).ToList();
            return sensorstatuses == null ? new List<SensorStatus>() : sensorstatuses;

        }

        public IList<SensorStatus> GetSensorStatusGreenhouse(string greenhouseId)
        {
            throw new NotImplementedException();
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();
        }

        public void SetSensorStatusGreenhouse(SensorStatus sensorStatus, string greenhouseId)
        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();
        }

        public void SetSensorStatusPot(SensorStatus sensorStatus, int sensorId, string greenhouseId)
        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();
        }
    }
}
