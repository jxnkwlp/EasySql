using System;
using System.Collections.Generic;

namespace EasySql.Databases
{
    public interface IDatabaseDataReader : IDisposable
    {
        IEnumerable<T> GetResult<T>();
    }
}
