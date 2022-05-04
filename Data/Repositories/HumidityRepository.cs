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
            throw new System.NotImplementedException();
        }

        public IEnumerable<HumidityMeasurement> GetAll(string greenhouseId)
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
            throw new System.NotImplementedException();
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