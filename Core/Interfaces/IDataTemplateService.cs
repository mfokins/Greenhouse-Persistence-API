using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IDataTemplateService<T>
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T Get(int id, string greenHouseId);
        
        IEnumerable<T> GetAll(string greenhouseId, int pageNumber = 0, int pageSize = 25);
        
    }
}
