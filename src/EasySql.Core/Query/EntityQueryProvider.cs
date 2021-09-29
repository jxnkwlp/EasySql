using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace EasySql.Query
{
    public class EntityQueryProvider : IQueryProvider
    {
        private readonly MethodInfo _genericCreateQueryMethod;
        private readonly MethodInfo _genericExecuteMethod;

        private readonly IQueryExecutor _queryExecutor;

        public EntityQueryProvider(IQueryExecutor queryExecutor)
        {
            _queryExecutor = queryExecutor;

            _genericCreateQueryMethod = typeof(EntityQueryProvider)
                .GetRuntimeMethods()
                .Single(m => m.Name == "CreateQuery" && m.IsGenericMethod);

            _genericExecuteMethod = queryExecutor
                .GetType()
                .GetRuntimeMethods()
                .Single(m => m.Name == "Execute" && m.IsGenericMethod);
        }


        public IQueryable CreateQuery(Expression expression)
        {
            return (IQueryable)_genericCreateQueryMethod
                   .MakeGenericMethod(expression.Type.MakeGenericType(typeof(IEnumerable<>)))
                   .Invoke(this, new object[] { expression });
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new EntityQueryable<TElement>(this, expression);
        }

        public object Execute(Expression expression)
        {
            return _genericExecuteMethod.MakeGenericMethod(expression.Type).Invoke(_queryExecutor, new[] { expression });
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _queryExecutor.Execute<TResult>(expression);
        }
    }
}
