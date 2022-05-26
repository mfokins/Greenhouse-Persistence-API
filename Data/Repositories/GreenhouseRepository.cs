using Core.Interfaces.Greenhouse;
using Core.Models;
using Data.Mappers;

namespace Data.Repositories
{
    public class GreenhouseRepository : IGreenhouseRepository
    {

        public void Add(Greenhouse entity)
        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();

            dbContext.Greenhouses.Add(DomToDb.Convert(entity));
            dbContext.SaveChanges();
            
        }

        public void AddBulk(IEnumerable<Greenhouse> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(Greenhouse entity)
        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();

            dbContext.Greenhouses.Remove(DomToDb.Convert(entity));
            dbContext.SaveChanges();
            
        }

        public Greenhouse Get(string id)
        {
            using GreenHouseDbContext dbContext = new GreenHouseDbContext();

            var greenhouse = dbContext.Greenhouses
                .Where(greenhouse => greenhouse.GreenHouseId == id)
                .FirstOrDefault();

            return greenhouse == null ? null : DbToDom.Convert(greenhouse);
        }

        public void Update(Greenhouse entity)
        {
            throw new NotImplementedException();
        }
    }
}
