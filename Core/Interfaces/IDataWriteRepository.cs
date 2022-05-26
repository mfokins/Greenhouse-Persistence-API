

namespace Core.Interfaces
{
    public interface IDataWriteRepository<T>
    {
        void Add(T entity);
        void AddBulk(IEnumerable<T> entities);

        void Update(T entity);
        void Delete(T entity);
    }
}
