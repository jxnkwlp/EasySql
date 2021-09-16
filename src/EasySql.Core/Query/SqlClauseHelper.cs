//using System.Linq.Expressions;
//using EasySql.Query.Clauses;

//namespace EasySql.Query
//{
//    public static class SqlClauseHelper
//    {
//        public static ISqlClause ParseExpression(MemberExpression node)
//        {
//            return new ColumnClause(node.Member.Name);
//        }

//        public static ISqlClause ParseExpression(ConstantExpression node)
//        {
//            return new ConstantClause(node.Value);
//        }

//        public static ISqlClause ParseBinaryExpression(BinaryExpression node)
//        {
//            return null;
//        }
//    }
//}
