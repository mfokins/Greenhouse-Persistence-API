using System.Collections.Generic;
using System.Linq;
using Core.Interfaces;
using Core.Interfaces.Humidity;
using Core.Models;
using Data.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class HumidityRepository:IHumidityRepository
    {
        private GreenHouseDbContext _dbContext;

        public HumidityRepository(GreenHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public HumidityMeasurement Get(int id)
        {
            return DbToDom.Convert(
                _dbContext.HumidityMeasurements
                    .FirstOrDefault(h => h.Id == id));
        }

        IEnumerable<HumidityMeasurement> IHumidityRepository.GetAll(string greenhouseId)
        {
            return _dbContext.Greenhouses
                .Include(g => g.HumidityMeasurements)
                .FirstOrDefault(g => g.GreenHouseId == greenhouseId)
                .HumidityMeasurements
                .Select(t => DbToDom.Convert(t));
        }

        public HumidityMeasurement GetLatest(string greenhouseId)
        {
            return _dbContext.Greenhouses
                .Include(g => g.HumidityMeasurements)
                .FirstOrDefault(g => g.GreenHouseId == greenhouseId)
                .HumidityMeasurements
                .OrderByDescending(m => m.Time)
                .Take(1)
                .Select(x => DbToDom.Convert(x))
                .FirstOrDefault();
        }

        IEnumerable<HumidityMeasurement> IDataReadRepository<HumidityMeasurement>.GetAll(string greenhouseId)
        {
            throw new System.NotImplementedException();
        }

        public void Add(HumidityMeasurement entity)
        {
            _dbContext.Greenhouses.Include(g => g.HumidityMeasurements)
                .FirstOrDefault(g => g.GreenHouseId == entity.GreenHouseId)
                .HumidityMeasurements
                .Add(DomToDb.Convert(entity));
            _dbContext.SaveChanges();
        }

        public void Update(HumidityMeasurement entity)
        {
            _dbContext.HumidityMeasurements.Update(DomToDb.Convert(entity));
            _dbContext.SaveChanges();
        }

        public void Delete(HumidityMeasurement entity)
        {
            _dbContext.Greenhouses
                .Include(g => g.HumidityMeasurements)
                .FirstOrDefault(g => g.GreenHouseId == entity.GreenHouseId)
                .HumidityMeasurements
                .Remove(DomToDb.Convert(entity));
        }
    }
}