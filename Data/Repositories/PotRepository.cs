using Core.Interfaces;
using Core.Interfaces.Pot;
using Core.Models;
using Data.Mappers;
using Microsoft.EntityFrameworkCore;


namespace Data.Repositories
{
    public class PotRepository : IPotRepository
    {
        private GreenHouseDbContext _dbContext;
        private readonly IThresholdRepository _thresholdRepository;


        public PotRepository(GreenHouseDbContext dbContext, IThresholdRepository thresholdRepository)
        {
            _dbContext = dbContext;
            _thresholdRepository = thresholdRepository;
        }

        public void Add(Pot entity)

        {
           
            _dbContext.Greenhouses
                .Include(x => x.Pots)
                .Include(z => z.Thresholds)
                .FirstOrDefault(x => x.GreenHouseId == entity.GreenHouseId)
                .Pots.Add(DomToDb.Convert(entity));
            _dbContext.SaveChanges();

        }

        public void Delete(Pot entity)
        {
            _dbContext.Greenhouses
                .Include(x => x.Pots)
                .Include(z => z.Thresholds)
                .FirstOrDefault(x => x.GreenHouseId == entity.GreenHouseId)
                .Pots.Remove(DomToDb.Convert(entity));
            _dbContext.SaveChanges();

        }

        public Pot Get(int id, string greenHouseId)
        {


            var converted = DbToDom.Convert(_dbContext.Greenhouses
                .Include(x => x.Pots)
                .FirstOrDefault(x => x.GreenHouseId == greenHouseId)
                .Pots.FirstOrDefault(x => x.Id == id));
            converted.moistureThreshold = _thresholdRepository.GetMoisturehresholds(greenHouseId, id);
            return converted;
        }

        public IEnumerable<Pot> GetAll(string greenhouseId, int pageNumber = 0, int pageSize = 25)
        {


            return _dbContext.Greenhouses
                .Include(x => x.Pots)
                .FirstOrDefault(x => x.GreenHouseId == greenhouseId)
                .Pots.Skip(pageNumber * pageSize)
                .Take(pageSize)
                .Select(t =>
                {
                    var pot = DbToDom.Convert(t);
                    pot.moistureThreshold = _thresholdRepository.GetMoisturehresholds(greenhouseId, t.Id);
                    pot.GreenHouseId = greenhouseId;
                    return pot;
                }
                );

        }

        public void Update(Pot entity)
        {
            _dbContext.Update(DomToDb.Convert(entity));
            _dbContext.SaveChanges();
        }
    }
}
