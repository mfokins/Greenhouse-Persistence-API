using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IDataWriteRepository<T>
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
