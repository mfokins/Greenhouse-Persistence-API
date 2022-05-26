using Core.Interfaces.Humidity;
using Core.Models;
using Data.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class HumidityRepository : IHumidityRepository
    {

        public HumidityMeasurement Get(int id, string greenHouseId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<HumidityMeasurement> GetAll(string greenhouseId, int pageNumber = 0, int pageSize = 25)
        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();

            return dbContext.Greenhouses
                .Include(g => g.HumidityMeasurements)
                .FirstOrDefault(g => g.GreenHouseId == greenhouseId)
                .HumidityMeasurements
                .OrderByDescending(m => m.Time)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .Select(t => DbToDom.Convert(t));
        }

        public HumidityMeasurement GetLatest(string greenhouseId)
        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();
            
            return dbContext.Greenhouses
                .Include(g => g.HumidityMeasurements)
                .FirstOrDefault(g => g.GreenHouseId == greenhouseId)
                .HumidityMeasurements
                .OrderByDescending(m => m.Time)
                .Take(1)
                .Select(x => DbToDom.Convert(x))
                .FirstOrDefault();

        }


        public void Add(HumidityMeasurement entity)
        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();

            dbContext.Greenhouses
                .Include(g => g.HumidityMeasurements)
                .FirstOrDefault(g => g.GreenHouseId == entity.GreenHouseId)
                .HumidityMeasurements
                .Add(DomToDb.Convert(entity));
            dbContext.SaveChanges();
        }

        public void Update(HumidityMeasurement entity)
        {
            throw new System.NotImplementedException();
        }


        public void Delete(HumidityMeasurement entity)
        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();
            
            dbContext.Greenhouses
                .Include(g => g.HumidityMeasurements)
                .FirstOrDefault(g => g.GreenHouseId == entity.GreenHouseId)
                .HumidityMeasurements
                .Remove(DomToDb.Convert(entity));
        }
    }
}