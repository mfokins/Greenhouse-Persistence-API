using Core.Interfaces;
using Core.Interfaces.DioxideCarbon;
using Core.Models;
using Data.Mappers;

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
            throw new NotImplementedException();
        }

        IEnumerable<DioxideCarbonMeasurement> IDataReadRepository<DioxideCarbonMeasurement>.GetAll(string greenhouseId, int pageNumber = 0, int pageSize = 25)
        {
            throw new NotImplementedException();
        }

        public async void Add(DioxideCarbonMeasurement entity)
        {
            throw new NotImplementedException();

            await _dbContext.DioxideCarbonMeasurement.AddAsync(DomToDb.Convert(entity));
            await _dbContext.SaveChangesAsync();
        }

        public async void Update(DioxideCarbonMeasurement entity)
        {
            throw new NotImplementedException();
            //There is no need an update for CO2
            _dbContext.DioxideCarbonMeasurement.Update(DomToDb.Convert(entity));
            await _dbContext.SaveChangesAsync();
        }

        public async void Delete(DioxideCarbonMeasurement entity)
        {
            throw new NotImplementedException();
            _dbContext.DioxideCarbonMeasurement.Remove(DomToDb.Convert(entity));
            await _dbContext.SaveChangesAsync();
        }
    }
}