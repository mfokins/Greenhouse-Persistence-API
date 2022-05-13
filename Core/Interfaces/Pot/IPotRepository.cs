using Core.Models;

namespace Core.Interfaces.Pot
{
    public interface IPotRepository : IDataReadRepository<Models.Pot>,
        IDataWriteRepository<Models.Pot>
    {
        
    }
}
