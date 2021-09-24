using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace EasySql.Query
{
    /// <summary>
    ///  Rewrite or fix expression
    /// </summary>
    public class QueryExpressionRewriteVisitor : ExpressionVisitor
    {
        public override Expression Visit(Expression node)
        {
            return base.Visit(node);
        }

        protected override Expression VisitExtension(Expression node)
        {
            return base.VisitExtension(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            var left = Visit(node.Left);
            var right = Visit(node.Right);

            return Expression.MakeBinary(node.NodeType, left, right);
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            if (node.NodeType == ExpressionType.Not)
            {
                return Expression.MakeBinary(ExpressionType.Equal, node.Operand, Expression.Constant(false));
            }

            if (node.NodeType == ExpressionType.Convert)
            {
                var a = Visit(node.Operand);
                return node.Update(a);
            }

            return base.VisitUnary(node);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            return base.VisitMethodCall(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Expression is ConstantExpression constantExpression)
            {
                return Visit(node.Expression);
            }

            return base.VisitMember(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node.Type.IsClass && node.Type.IsNested)
            {
                var fields = node.Type.GetRuntimeFields().ToArray();

                if (fields.Length != 1)
                {
                    throw new System.Exception($"Can't read the constant '{node.Type.Name}' ");
                }

                var value = fields[0].GetValue(node.Value);

                return Expression.Constant(value, fields[0].FieldType);
            }

            return base.VisitConstant(node);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return base.VisitParameter(node);
        }
    }
}
