using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using Core.Interfaces.Temperature;
using Core.Models;
using Data.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class TemperatureRepository : ITemperatureRepository
    {
        private GreenHouseDbContext _dbContext;

        public TemperatureRepository(GreenHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(TemperatureMeasurement entity)
        {

            _dbContext.Greenhouses
                .Include(g => g.TemperatureMesurments)
                .FirstOrDefault(g => g.GreenHouseId == entity.GreenHouseId)
                .TemperatureMesurments
                    .Add(DomToDb.Convert(entity));
            _dbContext.SaveChanges();
        }

        public void Delete(TemperatureMeasurement entity)
        {
            _dbContext.Greenhouses
                .Include(g => g.TemperatureMesurments)
                .FirstOrDefault(g => g.GreenHouseId == entity.GreenHouseId)
                .TemperatureMesurments
                    .Remove(DomToDb.Convert(entity));
            //_dbContext.TemperatureMesurments.Remove(DomToDb.Convert(entity));
            _dbContext.SaveChanges();
        }

        public TemperatureMeasurement Get(int id, string greenHouseId)
        {
            throw new NotImplementedException();
            //TODO Have to change this method
        }


        public IEnumerable<TemperatureMeasurement> GetAll(string greenhouseId, int pageNumber = 0, int pageSize = 25)
        {
            return _dbContext.Greenhouses
                    .Include(g => g.TemperatureMesurments)
                    .FirstOrDefault(g => g.GreenHouseId == greenhouseId)
                    .TemperatureMesurments
                        .OrderByDescending(m => m.Time)
                        .Skip(pageNumber * pageSize)
                        .Take(pageSize)
                        .Select(t => DbToDom.Convert(t));
            // return _dbContext.TemperatureMesurments.Where(i => i.GreenHouseId == greenhouseId).Select(x => DbToDom.Convert(x));
        }



        public TemperatureMeasurement GetLatest(string greenhouseId)
        {

            return _dbContext.Greenhouses
                    .Include(g => g.TemperatureMesurments)
                    .FirstOrDefault(g => g.GreenHouseId == greenhouseId)
                    .TemperatureMesurments
                     .OrderByDescending(m => m.Time)
                     .Take(1)
                     .Select(x => DbToDom.Convert(x))
                     .FirstOrDefault();
            //return _dbContext.TemperatureMesurments

        }

        public void Update(TemperatureMeasurement entity)
        {
            throw new NotImplementedException();
        }
    }
}
