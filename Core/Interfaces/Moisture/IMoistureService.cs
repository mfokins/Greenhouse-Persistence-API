using Core.Models;

namespace Core.Interfaces;

public interface IMoistureService
{
    void Add(MoistureMeasurement entity);
    void Update(MoistureMeasurement entity);
    void Delete(MoistureMeasurement entity);
    MoistureMeasurement Get(int id, string greenHouseId);
    MoistureMeasurement GetLatest(string greenhouseId,int potId);
    IEnumerable<MoistureMeasurement> GetAll(string greenhouseId,int potId, int pageNumber = 0, int pageSize = 25);
}