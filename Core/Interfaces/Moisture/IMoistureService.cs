using Core.Models;

namespace Core.Interfaces;

public interface IMoistureService
{
    void Add(MoistureMeasurement entity,int sensorId);
    MoistureMeasurement GetLatest(string greenhouseId,int potId);
    IEnumerable<MoistureMeasurement> GetAll(string greenhouseId,int potId, int pageNumber = 0, int pageSize = 25);
}