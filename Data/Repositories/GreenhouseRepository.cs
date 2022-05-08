using Core.Interfaces.Greenhouse;
using Core.Models;
using Data.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class GreenhouseRepository : IGreenhouseRepository
    {
        private GreenHouseDbContext _dbContext;

        public GreenhouseRepository(GreenHouseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Greenhouse entity)
        {
            _dbContext.Greenhouses.Add(DomToDb.Convert(entity));
            _dbContext.SaveChanges();
            
        }

        public void Delete(Greenhouse entity)
        {
            _dbContext.Greenhouses.Remove(DomToDb.Convert(entity));
            _dbContext.SaveChanges();
            
        }

        public Greenhouse Get(string id)
        {
            var greenhouse = _dbContext.Greenhouses
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
