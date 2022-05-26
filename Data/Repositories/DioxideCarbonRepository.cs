using Core.Interfaces;
using Core.Interfaces.DioxideCarbon;
using Core.Models;
using Data.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class DioxideCarbonRepository : IDioxideCarbonRepository
    {
        public DioxideCarbonMeasurement Get(int id, string greenHouseId)
        {
            throw new NotImplementedException();
        }
        
        public DioxideCarbonMeasurement GetLatest(string greenhouseId)
        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();

            return dbContext.Greenhouses
                .Include(g => g.DioxideCarbonMeasurements)
                .FirstOrDefault(g => g.GreenHouseId == greenhouseId)
                .DioxideCarbonMeasurements
                .OrderByDescending(m => m.Time)
                .Take(1)
                .Select(x => DbToDom.Convert(x))
                .FirstOrDefault();
        }

        public IEnumerable<DioxideCarbonMeasurement> GetAll(string greenhouseId,
            int pageNumber = 0, int pageSize = 25)
        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();

            return dbContext.Greenhouses
                .Include(g => g.DioxideCarbonMeasurements)
                .FirstOrDefault(g => g.GreenHouseId == greenhouseId)
                .DioxideCarbonMeasurements
                .OrderByDescending(m => m.Time)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .Select(t => DbToDom.Convert(t));
        }

        public  void Add(DioxideCarbonMeasurement entity)
        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();

            dbContext.Greenhouses.Include(g => g.DioxideCarbonMeasurements)
                .FirstOrDefault(g => g.GreenHouseId == entity.GreenHouseId)
                .DioxideCarbonMeasurements.Add(DomToDb.Convert(entity));
             dbContext.SaveChanges();
        }

        public async void Update(DioxideCarbonMeasurement entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(DioxideCarbonMeasurement entity)
        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();
            
            dbContext.Greenhouses
                .Include(g => g.DioxideCarbonMeasurements)
                .FirstOrDefault(g => g.GreenHouseId == entity.GreenHouseId)
                .DioxideCarbonMeasurements
                .Remove(DomToDb.Convert(entity));
            dbContext.SaveChanges();
        }

        public void AddBulk(IEnumerable<DioxideCarbonMeasurement> entities)
        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();

            dbContext.Greenhouses
                .Include(g => g.DioxideCarbonMeasurements)
                .FirstOrDefault(g => g.GreenHouseId == entities.FirstOrDefault().GreenHouseId)
                .DioxideCarbonMeasurements.AddRange(entities.Select(entity => DomToDb.Convert(entity)));
            dbContext.SaveChanges();

        }
    }
}