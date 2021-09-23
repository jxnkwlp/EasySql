using System.Linq.Expressions;

namespace EasySql.Query
{
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
            return base.VisitBinary(node);
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            if (node.NodeType == ExpressionType.Not)
            {
                return Expression.MakeBinary(ExpressionType.Equal, node.Operand, Expression.Constant(false));
            }

            return base.VisitUnary(node);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            return base.VisitMethodCall(node);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            return base.VisitMember(node);
        }
    }
}
