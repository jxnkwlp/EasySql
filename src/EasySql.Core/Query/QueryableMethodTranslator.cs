using System.Collections.Generic;
using System.Linq.Expressions;
using EasySql.Query.Translators;

namespace EasySql.Query
{
    public class QueryableMethodTranslator : IQueryableMethodTranslator
    {
        private readonly IEnumerable<IMethodCallExpressionTranslator> _translators;

        public QueryableMethodTranslator(IEnumerable<IMethodCallExpressionTranslator> translators)
        {
            _translators = translators;
        }

        public Expression Translate(ExpressionVisitor visitor, MethodCallExpression node)
        {
            foreach (var translator in _translators)
            {
                var result = translator.Translate(visitor, node);
                if (result != node)
                    return result;
            }

            return node;
        }
    }
}
