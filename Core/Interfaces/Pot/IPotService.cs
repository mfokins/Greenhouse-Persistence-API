

namespace Core.Interfaces.Pot
{
    public interface IPotService : IDataTemplateService<Models.Pot>
    {
        int GetPotIdBySensorId(int sensorId, string greenhouseId);
    }
}
