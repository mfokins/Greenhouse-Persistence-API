

namespace Core.Interfaces
{
    public interface IDataWriteRepository<T>
    {
        void Add(T entity);
        //void AddBulk(T entity);

        void Update(T entity);
        void Delete(T entity);
    }
}
