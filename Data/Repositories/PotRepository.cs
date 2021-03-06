using Core.Interfaces;
using Core.Interfaces.Pot;
using Core.Models;
using Data.Mappers;
using Microsoft.EntityFrameworkCore;


namespace Data.Repositories
{
    public class PotRepository : IPotRepository
    {
        private readonly IThresholdRepository _thresholdRepository;

        public PotRepository(IThresholdRepository thresholdRepository)
        {
            _thresholdRepository = thresholdRepository;
        }

        public void Add(Pot entity)

        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();

            var converted = DomToDb.Convert(entity);
            converted.MoistureSensorStatus = new Models.SensorStatus() { Type = Models.SensorType.Moisture, IsWorking = false };
            dbContext.Greenhouses
                .Include(x => x.Pots)
                .Include(z => z.Thresholds)
                .FirstOrDefault(x => x.GreenHouseId == entity.GreenHouseId)
                .Pots.Add(converted);
            dbContext.SaveChanges();
        }

        public void AddBulk(IEnumerable<Pot> entities)
        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();
            dbContext.Greenhouses
                .Include(g => g.Pots)
                .FirstOrDefault(g => g.GreenHouseId == entities.FirstOrDefault().GreenHouseId)
                .Pots.AddRange(entities.Select(entity =>
                {
                    var converted = DomToDb.Convert(entity);
                    converted.MoistureSensorStatus = new Models.SensorStatus() { Type = Models.SensorType.Moisture, IsWorking = false };
                    return converted;
                }));
            dbContext.SaveChanges();
        }

        public void Delete(Pot entity)
        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();

            dbContext.Greenhouses
                .Include(x => x.Pots)
                .Include(z => z.Thresholds)
                .FirstOrDefault(x => x.GreenHouseId == entity.GreenHouseId)
                .Pots.Remove(DomToDb.Convert(entity));
            dbContext.SaveChanges();
        }

        public Pot Get(int id, string greenHouseId)
        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();


            var converted = DbToDom.Convert(dbContext.Greenhouses
                .Include(x => x.Pots)
                .FirstOrDefault(x => x.GreenHouseId == greenHouseId)
                .Pots.FirstOrDefault(x => x.Id == id));
            converted.moistureThreshold = _thresholdRepository.GetMoisturehresholds(greenHouseId, id);
            return converted;
        }

        public IEnumerable<Pot> GetAll(string greenhouseId, int pageNumber = 0, int pageSize = 25)
        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();

            return dbContext.Greenhouses
                .Include(x => x.Pots)
                .FirstOrDefault(x => x.GreenHouseId == greenhouseId)
                .Pots.Skip(pageNumber * pageSize)
                .Take(pageSize)
                .Select(t =>
                    {
                        var pot = DbToDom.Convert(t);
                        pot.moistureThreshold = _thresholdRepository.GetMoisturehresholds(greenhouseId, t.Id);
                        pot.GreenHouseId = greenhouseId;
                        return pot;
                    }
                );
        }

        public int GetPotIdBySensorId(int sensorId, string greenhouseId)
        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();
            return dbContext.Greenhouses
                .Include(x => x.Pots)
                .FirstOrDefault(gh => gh.GreenHouseId == greenhouseId)
                .Pots
                .FirstOrDefault(p => p.MoistureSensorId == sensorId)
                .Id;
        }

        public void Update(Pot entity)
        {
            Models.Pot converted;
            using (GreenHouseDbContext dbContext = new GreenHouseDbContext())
            {
                 converted = DomToDb.Convert(entity);
                converted.MoistureSensorStatus = dbContext.Greenhouses
                    .Include(x => x.Pots)
                    .ThenInclude(s => s.MoistureSensorStatus)
                    .FirstOrDefault(gh => gh.GreenHouseId == entity.GreenHouseId)
                    .Pots.FirstOrDefault(p => p.Id == entity.Id)
                    .MoistureSensorStatus;
            }
            using (GreenHouseDbContext dbContext = new GreenHouseDbContext())
            {
                dbContext.Update(converted);
                dbContext.SaveChanges();
            }
        }
    }
}