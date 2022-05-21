using Core.Interfaces;
using Core.Models;
using Data.Mappers;
using Microsoft.EntityFrameworkCore;
using Greenhouse = Data.Models.Greenhouse;

namespace Data.Repositories;

public class MoistureRepository : IMoistureRepository
{
    private GreenHouseDbContext _dbContext;

    public MoistureRepository(GreenHouseDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(MoistureMeasurement entity)
    {
        _dbContext.Greenhouses.Include(pot => pot.Pots).FirstOrDefault(g => g.GreenHouseId == entity.GreenHouseId)
            .Pots.FirstOrDefault(p => p.Id == entity.PotId).MoistureMeasurements.Add(DomToDb.Convert(entity));
        _dbContext.SaveChangesAsync();
    }

    //Not really needed
    public void Update(MoistureMeasurement entity)
    {
        _dbContext.Update(DomToDb.Convert(entity));
        _dbContext.SaveChanges();
    }

    //Not really needed
    public void Delete(MoistureMeasurement entity)
    {
        _dbContext.Greenhouses
            .Include(x => x.Pots)
            .FirstOrDefault(x => x.GreenHouseId == entity.GreenHouseId)
            .Pots.FirstOrDefault(p => p.Id == entity.PotId).MoistureMeasurements.Remove(DomToDb.Convert(entity));
        _dbContext.SaveChangesAsync();
    }

    public MoistureMeasurement GetLatest(string greenhouseId, int potId)
    {
        return DbToDom.Convert(_dbContext.Greenhouses.Include(pot => pot.Pots)
            .FirstOrDefault(g => g.GreenHouseId == greenhouseId).Pots.FirstOrDefault(p => p.Id == potId)
            .MoistureMeasurements.OrderByDescending(t => t.Time).FirstOrDefault());
    }

    public MoistureMeasurement Get(int id, string greenhouseId)
    {
        // not sure we need this one though
        // _dbContext.Greenhouses
        //     .FirstOrDefault(g => g.GreenHouseId == greenhouseId).Pots.Select(p =>
        //     {
        //         var measurement = p.MoistureMeasurements.FirstOrDefault(m => m.Id == id);
        //         return DbToDom.Convert(measurement);
        //     });
        throw new NotImplementedException();
    }

    public IEnumerable<MoistureMeasurement> GetAll(string greenhouseId, int potId, int pageNumber = 0,
        int pageSize = 25)
    {
        return _dbContext.Greenhouses
            .Include(x => x.Pots)
            .FirstOrDefault(x => x.GreenHouseId == greenhouseId)
            .Pots.FirstOrDefault(p => p.Id == potId).MoistureMeasurements
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .Select(m => { return DbToDom.Convert(m); }
            );
    }
}