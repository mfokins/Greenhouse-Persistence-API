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

        public void Add(TemperatureMesurment entity)
        {
            _dbContext.TemperatureMesurments.Add(DomToDb.Convert(entity));
            _dbContext.SaveChanges();
        }

        public void Delete(TemperatureMesurment entity)
        {
            _dbContext.TemperatureMesurments.Remove(DomToDb.Convert(entity));
            _dbContext.SaveChanges();
        }

        public TemperatureMesurment Get(int id)
        {
            return DbToDom.Convert(_dbContext.TemperatureMesurments.FirstOrDefault(x => x.Id == id));
        }


        public IEnumerable<TemperatureMesurment> GetAll(string greenhouseId)
        {
            return _dbContext.TemperatureMesurments.Where(i => i.GreenHouseId == greenhouseId).Select(x => DbToDom.Convert(x));
        }



        public TemperatureMesurment GetLatest(string greenhouseId)
        {

            return _dbContext.TemperatureMesurments
                .Where(x => x.GreenHouseId == greenhouseId)
                .OrderByDescending(m => m.Time)
                .Take(1)
                .Select(x => DbToDom.Convert(x))
                .FirstOrDefault();
        }

        public void Update(TemperatureMesurment entity)
        {
            throw new NotImplementedException();
        }
    }
}
