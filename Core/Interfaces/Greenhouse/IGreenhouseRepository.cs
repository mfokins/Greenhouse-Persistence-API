

namespace Core.Interfaces.Greenhouse
{
    public interface IGreenhouseRepository : IDataWriteRepository<Models.Greenhouse>
    {
        public Models.Greenhouse Get(string id);
    }
}
