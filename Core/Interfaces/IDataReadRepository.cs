﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface  IDataReadRepository<T>
    {
        public T Get(int id);
        public IEnumerable<T> GetAll(string greenhouseId);
    }
}
