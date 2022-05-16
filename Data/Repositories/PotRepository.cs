using Core.Interfaces.Pot;
using Core.Models;
using Data.Mappers;
using Microsoft.EntityFrameworkCore;


namespace Data.Repositories
{
    public class PotRepository : IPotRepository
    {
        private GreenHouseDbContext _dbContext;
#pragma warning disable CS8602

        public PotRepository(GreenHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Pot entity)
        {
            _dbContext.Greenhouses
                .Include(x => x.Pots)
                .FirstOrDefault(x => x.GreenHouseId == entity.GreenHouseId)
                .Pots.Add(DomToDb.Convert(entity));
            _dbContext.SaveChanges();

        }

        public void Delete(Pot entity)
        {
            _dbContext.Greenhouses
                .Include(x => x.Pots)
                .FirstOrDefault(x => x.GreenHouseId == entity.GreenHouseId)
                .Pots.Remove(DomToDb.Convert(entity));
            _dbContext.SaveChanges();

        }

        public Pot Get(int id, string greenHouseId)
        {
            return DbToDom.Convert(_dbContext.Greenhouses
                .Include(x => x.Pots)
                .FirstOrDefault(x => x.GreenHouseId == greenHouseId)
                .Pots.FirstOrDefault(x => x.Id == id));
        }

        public IEnumerable<Pot> GetAll(string greenhouseId, int pageNumber = 0, int pageSize = 25)
        {
            return _dbContext.Greenhouses
                .Include(x => x.Pots)
                .FirstOrDefault(x => x.GreenHouseId == greenhouseId)
                .Pots.Skip(pageNumber * pageSize)
                .Take(pageSize)
                .Select(t => DbToDom.Convert(t)).Select(pot =>
                {
                    pot.GreenHouseId = greenhouseId;
                    return pot;
                });

        }

        public void Update(Pot entity)
        {
            _dbContext.Update(DomToDb.Convert(entity)); 
            _dbContext.SaveChanges();
            
        }
    }
}
