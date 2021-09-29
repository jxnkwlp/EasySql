using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using EasySql.Query.SqlExpressions;

namespace EasySql.Query.Translators
{
    public class StringMethodCallExpressionTranslator : IMethodCallExpressionTranslator
    {
        private static readonly MethodInfo _containsMethodInfo = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        private static readonly MethodInfo _containsMethodInfo2 = typeof(string).GetMethod("Contains", new[] { typeof(string), typeof(StringComparison) });

        private static readonly MethodInfo _equalsMethodInfo = typeof(string).GetMethod("Equals", new[] { typeof(string) });
        private static readonly MethodInfo _equalsMethodInfo2 = typeof(string).GetMethod("Equals", new[] { typeof(string), typeof(StringComparison) });

        private static readonly MethodInfo _startsWithMethodInfo = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
        private static readonly MethodInfo _endsWithMethodInfo = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });

        public Expression Translate(ExpressionVisitor visitor, MethodCallExpression expression)
        {
            if (expression.Method.DeclaringType != typeof(string))
                return expression;

            var args = visitor.Visit(expression.Arguments[0]) as SqlConstantExpression;
            var target = visitor.Visit(expression.Object) as SqlExpression;

            if (expression.Method == _containsMethodInfo || expression.Method == _containsMethodInfo2)
            {
                var left = new SqlFunctionExpression(null, "CHARINDEX", new List<SqlExpression> { args, target });

                return new SqlBinaryExpression(left, new SqlConstantExpression(0), ExpressionType.GreaterThan);
            }
            else if (expression.Method == _equalsMethodInfo)
            {
                return new SqlBinaryExpression(target, args, ExpressionType.Equal);
            }
            else if (expression.Method == _startsWithMethodInfo)
            {
                var value = args.Update(args.Value + "%") as SqlExpression;
                return new SqlFunctionExpression(target, null, "LIKE", new List<SqlExpression> { value });
            }
            else if (expression.Method == _endsWithMethodInfo)
            {
                var value = args.Update("%" + args.Value) as SqlExpression;
                return new SqlFunctionExpression(target, null, "LIKE", new List<SqlExpression> { value });
            }
            else
            {
                throw new NotSupportedException($"The method '{expression.Method}' not support.");
            }
        }
    }

}
