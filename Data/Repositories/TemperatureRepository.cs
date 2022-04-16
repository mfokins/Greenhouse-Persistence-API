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
            throw new NotImplementedException();

            //_dbContext.TemperatureMesurments.Add(DomToDb.Convert(entity));
            //_dbContext.SaveChanges();
        }

        public void Delete(TemperatureMeasurement entity)
        {
            throw new NotImplementedException();

            //_dbContext.TemperatureMesurments.Remove(DomToDb.Convert(entity));
            //_dbContext.SaveChanges();
        }

        public TemperatureMeasurement Get(int id)
        {
            throw new NotImplementedException();

            // return DbToDom.Convert(_dbContext.TemperatureMesurments.FirstOrDefault(x => x.Id == id));
        }


        public IEnumerable<TemperatureMeasurement> GetAll(string greenhouseId)
        {
            throw new NotImplementedException();
            // return _dbContext.TemperatureMesurments.Where(i => i.GreenHouseId == greenhouseId).Select(x => DbToDom.Convert(x));
        }



        public TemperatureMeasurement GetLatest(string greenhouseId)
        {
            throw new NotImplementedException();
            //return _dbContext.TemperatureMesurments
            //    .Where(x => x.GreenHouseId == greenhouseId)
            //    .OrderByDescending(m => m.Time)
            //    .Take(1)
            //    .Select(x => DbToDom.Convert(x))
            //    .FirstOrDefault();
        }

        public void Update(TemperatureMeasurement entity)
        {
            throw new NotImplementedException();
        }
    }
}
