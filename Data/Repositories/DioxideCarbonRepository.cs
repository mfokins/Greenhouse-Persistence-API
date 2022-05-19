using Core.Interfaces;
using Core.Interfaces.DioxideCarbon;
using Core.Models;
using Data.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class DioxideCarbonRepository : IDioxideCarbonRepository
    {
        private GreenHouseDbContext _dbContext;

        public DioxideCarbonRepository(GreenHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public DioxideCarbonMeasurement Get(int id, string greenHouseId)
        {
            throw new NotImplementedException();
        }
        
        public DioxideCarbonMeasurement GetLatest(string greenhouseId)
        {
            return _dbContext.Greenhouses
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
            return _dbContext.Greenhouses
                .Include(g => g.DioxideCarbonMeasurements)
                .FirstOrDefault(g => g.GreenHouseId == greenhouseId)
                .DioxideCarbonMeasurements
                .OrderByDescending(m => m.Time)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .Select(t => DbToDom.Convert(t));
        }

        public async void Add(DioxideCarbonMeasurement entity)
        {
            _dbContext.Greenhouses.Include(g => g.DioxideCarbonMeasurements)
                .FirstOrDefault(g => g.GreenHouseId == entity.GreenHouseId)
                .DioxideCarbonMeasurements.Add(DomToDb.Convert(entity));
            await _dbContext.SaveChangesAsync();
        }

        public async void Update(DioxideCarbonMeasurement entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(DioxideCarbonMeasurement entity)
        {
            _dbContext.Greenhouses
                .Include(g => g.DioxideCarbonMeasurements)
                .FirstOrDefault(g => g.GreenHouseId == entity.GreenHouseId)
                .DioxideCarbonMeasurements
                .Remove(DomToDb.Convert(entity));
            _dbContext.SaveChanges();
        }
    }
}