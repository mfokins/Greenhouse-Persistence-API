using System.Collections.Generic;
using Core.Interfaces;
using Core.Interfaces.Luminosity;
using Core.Models;

namespace Data.Repositories
{
    public class LuminosityRepository : ILuminosityRepository //TODO all the operations
    {
        //Decided to wait with implementation because luminosity might be omitted
        private GreenHouseDbContext _dbContext;

        public LuminosityRepository(GreenHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public LuminosityMeasurement Get(int id)
        {
            throw new System.NotImplementedException();
        }

        IEnumerable<LuminosityMeasurement> ILuminosityRepository.GetAll(string greenhouseId)
        {
            throw new System.NotImplementedException();
        }

        public LuminosityMeasurement GetLatest(string greenhouseId)
        {
            throw new System.NotImplementedException();
        }

        IEnumerable<LuminosityMeasurement> IDataReadRepository<LuminosityMeasurement>.GetAll(string greenhouseId)
        {
            throw new System.NotImplementedException();
        }

        public void Add(LuminosityMeasurement entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(LuminosityMeasurement entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(LuminosityMeasurement entity)
        {
            throw new System.NotImplementedException();
        }
    }
}