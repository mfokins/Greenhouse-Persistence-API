using System.Collections.Generic;
using Core.Interfaces;
using Core.Interfaces.Humidity;
using Core.Models;

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

        IEnumerable<HumidityMeasurement> IHumidityRepository.GetAll(string greenhouseId)
        {
            throw new System.NotImplementedException();
        }

        public HumidityMeasurement GetLatest(string greenhouseId)
        {
            throw new System.NotImplementedException();
        }

        IEnumerable<HumidityMeasurement> IDataReadRepository<HumidityMeasurement>.GetAll(string greenhouseId)
        {
            throw new System.NotImplementedException();
        }

        public void Add(HumidityMeasurement entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(HumidityMeasurement entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(HumidityMeasurement entity)
        {
            throw new System.NotImplementedException();
        }
    }
}