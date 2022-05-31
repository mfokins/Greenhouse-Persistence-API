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
                .ThenInclude(p => p.MoistureSensorStatus)
                ?.FirstOrDefault(gh => gh.GreenHouseId == greenhouseId)
                ?.Pots
                .Select(pot => pot.MoistureSensorStatus == null ? SensorStatus.Empty : DbToDom.Convert(pot.MoistureSensorStatus,pot.Name)).ToList();
            return sensorstatuses ?? new List<SensorStatus>();

        }

        public IList<SensorStatus> GetSensorStatusGreenhouse(string greenhouseId)
        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();
            var sensorstatuses = dbContext.Greenhouses
                .Include(g => g.SensorStatuses)
                ?.FirstOrDefault(gh => gh.GreenHouseId == greenhouseId)
                ?.SensorStatuses
                .Select(s => DbToDom.Convert(s)).ToList();
            return sensorstatuses ?? new List<SensorStatus>();
        }

        public void SetSensorStatusGreenhouse(SensorStatus sensorStatus, string greenhouseId)
        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();
            var sensorStatuses = dbContext
                .Greenhouses
                .Include(g => g.SensorStatuses)
                ?.FirstOrDefault(s => s.GreenHouseId == greenhouseId)
                ?.SensorStatuses;
            if (sensorStatuses == null)
                return;
            var sensor = sensorStatuses.FirstOrDefault(status => status.Type == (Models.SensorType)sensorStatus.Type);
            if (sensor == null)
            {
                sensorStatuses.Add(DomToDb.Convert(sensorStatus));
            }
            else
            {
                sensor.IsWorking = sensorStatus.IsWorking;
            }
            dbContext.SaveChanges();
        }

        public void SetSensorStatusPot(SensorStatus sensorStatus, int potId, string greenhouseId)
        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();
            var currentStatus = dbContext
                .Greenhouses
            .Include(g => g.Pots)
            .ThenInclude(p => p.MoistureSensorStatus)
            ?.FirstOrDefault(gh => gh.GreenHouseId == greenhouseId)
            ?.Pots
            ?.FirstOrDefault(p => p.Id == potId)
            ?.MoistureSensorStatus;
            if(currentStatus == null)
            {
                try{

                    currentStatus = DomToDb.Convert(sensorStatus);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                    //Can a lot of thing go wrong if smth isnt created before
                }
            }
            else
            {
                currentStatus.IsWorking = sensorStatus.IsWorking;
            }
            dbContext.SaveChanges();
        }
    }
}
