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


        public void Add(TemperatureMeasurement entity)
        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();

            dbContext.Greenhouses
                .Include(g => g.TemperatureMesurments)
                .FirstOrDefault(g => g.GreenHouseId == entity.GreenHouseId)
                .TemperatureMesurments
                    .Add(DomToDb.Convert(entity));
            dbContext.SaveChanges();
        }

        //public void AddBulk(TemperatureMeasurement entity)
        //{
        //    dbContext.Greenhouses
        //        .Include(g => g.TemperatureMesurments)
        //        .FirstOrDefault(g => g.GreenHouseId == entity.GreenHouseId)
        //        .TemperatureMesurments.AddRange(DomToDb.Convert(entity));
        //    ;            dbContext.Greenhouses
        //        .Include(g => g.TemperatureMesurments)
        //        .FirstOrDefault(g => g.GreenHouseId == entity.GreenHouseId)
        //        .TemperatureMesurments
        //            .Add(DomToDb.Convert(entity));
        //    dbContext.SaveChanges();
        //}

        public void Delete(TemperatureMeasurement entity)
        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();

            dbContext.Greenhouses
                .Include(g => g.TemperatureMesurments)
                .FirstOrDefault(g => g.GreenHouseId == entity.GreenHouseId)
                .TemperatureMesurments
                    .Remove(DomToDb.Convert(entity));
            dbContext.SaveChanges();
        }

        public TemperatureMeasurement Get(int id, string greenHouseId)
        {
            throw new NotImplementedException();
            //TODO Have to change this method
        }


        public IEnumerable<TemperatureMeasurement> GetAll(string greenhouseId, int pageNumber = 0, int pageSize = 25)
        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();

            return dbContext.Greenhouses
                    .Include(g => g.TemperatureMesurments)
                    .FirstOrDefault(g => g.GreenHouseId == greenhouseId)
                    .TemperatureMesurments
                        .OrderByDescending(m => m.Time)
                        .Skip(pageNumber * pageSize)
                        .Take(pageSize)
                        .Select(t => DbToDom.Convert(t));
        }



        public TemperatureMeasurement GetLatest(string greenhouseId)
        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();

            return dbContext.Greenhouses
                    .Include(g => g.TemperatureMesurments)
                    .FirstOrDefault(g => g.GreenHouseId == greenhouseId)
                    .TemperatureMesurments
                     .OrderByDescending(m => m.Time)
                     .Take(1)
                     .Select(x => DbToDom.Convert(x))
                     .FirstOrDefault();
            //return dbContext.TemperatureMesurments

        }

        public void Update(TemperatureMeasurement entity)
        {
            throw new NotImplementedException();
        }
    }
}
