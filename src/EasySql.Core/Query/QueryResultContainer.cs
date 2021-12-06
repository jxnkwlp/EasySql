using System.Collections;
using System.Collections.Generic;

namespace EasySql.Query
{
    public class QueryResultContainer<TResult> : IEnumerable<TResult>
    {
        private readonly IEnumerable<TResult> _results;

        public QueryResultContainer(IEnumerable<TResult> results)
        {
            _results = results;
        }

        public IEnumerator<TResult> GetEnumerator()
        {
            return _results.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
