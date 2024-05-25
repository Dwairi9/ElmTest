using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ElmBookShelf.Infrastructure.Common
{  
    public class SqlExpressionVisitor : ExpressionVisitor
    {
        private readonly StringBuilder _sb;
        private readonly DynamicParameters _parameters;
        private int _paramCount;

        public SqlExpressionVisitor(DynamicParameters parameters)
        {
            _sb = new StringBuilder();
            _parameters = parameters;
            _paramCount = 0;
        }

        public string Translate(Expression expression)
        {
            Visit(expression);
            return _sb.ToString();
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            _sb.Append("(");
            Visit(node.Left);
            _sb.Append($" {GetSqlOperator(node.NodeType)} ");
            Visit(node.Right);
            _sb.Append(")");
            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Expression != null && node.Expression.NodeType == ExpressionType.Parameter)
            {
                _sb.Append(node.Member.Name);
            }
            else
            {
                var value = GetValue(node);
                var paramName = $"@param{_paramCount++}";
                _sb.Append(paramName);
                _parameters.Add(paramName, value);
            }
            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            var paramName = $"@param{_paramCount++}";
            _sb.Append(paramName);
            _parameters.Add(paramName, node.Value);
            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == "Contains")
            {
                Visit(node.Object);
                _sb.Append(" LIKE ");
                var value = GetValue(node.Arguments[0]);
                var paramName = $"@param{_paramCount++}";
                _sb.Append(paramName);
                _parameters.Add(paramName, $"%{value}%");
            }
            else if (node.Method.Name == "ToString")
            {
                Visit(node.Object);
                return node;
            }
            else
            {
                throw new NotSupportedException($"Method {node.Method.Name} is not supported in SQL translation.");
            }

            return node;
        }

        private object GetValue(Expression expression)
        {
            if (expression.NodeType == ExpressionType.Constant)
            {
                var constant = (ConstantExpression)expression;
                return constant.Value;
            }

            if (expression.NodeType == ExpressionType.MemberAccess)
            {
                var member = (MemberExpression)expression;
                if (member.Expression is ConstantExpression constantExpression)
                {
                    var fieldInfo = member.Member as System.Reflection.FieldInfo;
                    var propertyInfo = member.Member as System.Reflection.PropertyInfo;

                    if (fieldInfo != null)
                    {
                        return fieldInfo.GetValue(constantExpression.Value);
                    }
                    else if (propertyInfo != null)
                    {
                        return propertyInfo.GetValue(constantExpression.Value);
                    }
                }
                else
                {
                    // Handling the case where the member expression is accessing a property of an object
                    var compiledLambda = Expression.Lambda(member).Compile();
                    return compiledLambda.DynamicInvoke();
                }
            }

            throw new NotSupportedException($"Expression type {expression.NodeType} is not supported in SQL translation.");
        }

        private string GetSqlOperator(ExpressionType type)
        {
            return type switch
            {
                ExpressionType.Equal => "=",
                ExpressionType.AndAlso => "AND",
                ExpressionType.OrElse => "OR",
                _ => throw new NotSupportedException($"Expression type {type} is not supported in SQL translation."),
            };
        }
    }
}
