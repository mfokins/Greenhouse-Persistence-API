using Core.Models;

namespace Core.Interfaces;

public interface IMoistureRepository :
    IDataWriteRepository<MoistureMeasurement>
{
    MoistureMeasurement Get(int id, string greenHouseId);
    MoistureMeasurement GetLatest(string greenhouseId, int potId);
    public IEnumerable<MoistureMeasurement> GetAll(string greenhouseId,int potId, int pageNumber = 0, int pageSize = 25);
}