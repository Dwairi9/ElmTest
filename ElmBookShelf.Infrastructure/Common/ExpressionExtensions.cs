using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ElmBookShelf.Infrastructure.Common
{
    public static class ExpressionExtensions
    {
        public static string ToSql<T>(this Expression<Func<T, bool>> expression)
        {
            // Convert the expression tree to SQL WHERE clause string
            // This is a placeholder. In practice, you would use a library like PredicateBuilder or write a proper parser.
            return new SqlExpressionVisitor<T>().Translate(expression);
        }
    }

    public class SqlExpressionVisitor<T> : ExpressionVisitor
    {
        private string _sql;

        public string Translate(Expression<Func<T, bool>> expression)
        {
            Visit(expression);
            return _sql;
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            _sql += "(";
            Visit(node.Left);
            _sql += $" {node.NodeType} ";
            Visit(node.Right);
            _sql += ")";
            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            _sql += node.Value;
            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            _sql += node.Member.Name;
            return node;
        }
    }
}
