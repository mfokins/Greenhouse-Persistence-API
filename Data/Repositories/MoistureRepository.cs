using Core.Interfaces;
using Core.Models;
using Data.Mappers;
using Microsoft.EntityFrameworkCore;


namespace Data.Repositories;

public class MoistureRepository : IMoistureRepository
{
    public void Add(MoistureMeasurement entity)
    {
        using GreenHouseDbContext dbContext = new GreenHouseDbContext();

        dbContext.Greenhouses.Include(pot => pot.Pots)
            .ThenInclude(m => m.MoistureMeasurements)
            .FirstOrDefault(g => g.GreenHouseId == entity.GreenHouseId)
            .Pots.FirstOrDefault(p => p.Id == entity.PotId).MoistureMeasurements.Add(DomToDb.Convert(entity));
        dbContext.SaveChanges();
    }


    public void Update(MoistureMeasurement entity)
    {
        using GreenHouseDbContext dbContext = new GreenHouseDbContext();

        dbContext.Update(DomToDb.Convert(entity));
        dbContext.SaveChanges();
    }


    public void Delete(MoistureMeasurement entity)
    {
        using GreenHouseDbContext dbContext = new GreenHouseDbContext();

        dbContext.Greenhouses
            .Include(x => x.Pots)
            .FirstOrDefault(x => x.GreenHouseId == entity.GreenHouseId)
            .Pots.FirstOrDefault(p => p.Id == entity.PotId).MoistureMeasurements.Remove(DomToDb.Convert(entity));
        dbContext.SaveChanges();
    }

    public MoistureMeasurement GetLatest(string greenhouseId, int potId)
    {
        using GreenHouseDbContext dbContext = new GreenHouseDbContext();

        return DbToDom.Convert(dbContext.Greenhouses
            .Include(pot => pot.Pots)
            .ThenInclude(m => m.MoistureMeasurements)
            .FirstOrDefault(g => g.GreenHouseId == greenhouseId).Pots.FirstOrDefault(p => p.Id == potId)
            .MoistureMeasurements.OrderByDescending(t => t.Time).FirstOrDefault());
    }

    public MoistureMeasurement Get(int id, string greenhouseId)
    {
        // not sure we need this one though
        // dbContext.Greenhouses
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
        using GreenHouseDbContext dbContext = new GreenHouseDbContext();

        return dbContext.Greenhouses
            .Include(x => x.Pots)
            .ThenInclude(m => m.MoistureMeasurements)
            .FirstOrDefault(x => x.GreenHouseId == greenhouseId)
            .Pots.FirstOrDefault(p => p.Id == potId).MoistureMeasurements
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .Select(m => { return DbToDom.Convert(m); }
            );
    }

    public void AddBulk(IEnumerable<MoistureMeasurement> entities)
    {
        using GreenHouseDbContext dbContext = new GreenHouseDbContext();

        dbContext.Greenhouses
            .Include(g => g.Pots).ThenInclude(m=>m.MoistureMeasurements)
            .FirstOrDefault(g => g.GreenHouseId == entities.FirstOrDefault().GreenHouseId).Pots.
            FirstOrDefault(p=>p.Id==entities.FirstOrDefault().PotId).MoistureMeasurements
            .AddRange(entities.Select(entity => DomToDb.Convert(entity)));
        dbContext.SaveChanges();
    }
}