using Dapper;
using ElmBookShelf.Domain.Entities;
using ElmBookShelf.Infrastructure.Common;
using ElmBookShelf.Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data; 
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace ElmBookShelf.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly IDbConnection _dbConnection;

        public GenericRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<T> GetByIdAsync(long id)
        {
            var tableName = typeof(T).Name;
            var query = $"SELECT * FROM {tableName} WHERE {tableName}Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<T>(query, new { Id = id });
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var tableName = typeof(T).Name;
            var query = $"SELECT * FROM {tableName}";
            return await _dbConnection.QueryAsync<T>(query);
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate, int page, int pageSize)
        {
            string tableName = typeof(T).Name;
            string primaryKeyName = $"{tableName}Id";
            var attribute = Attribute.GetCustomAttribute(typeof(T), typeof(TableAttribute)) as TableAttribute;

            if (attribute != null) 
            {
                tableName = attribute.Name;
            }

            var primaryKeyAttribute = typeof(T).GetProperties()
                .Select(pi => new { Property = pi, Attribute = pi.GetCustomAttributes(typeof(KeyAttribute), true).FirstOrDefault() as KeyAttribute })
                .FirstOrDefault(x => x.Attribute != null);

            if (primaryKeyAttribute != null) 
            {
                primaryKeyName = primaryKeyAttribute.Property.Name;
            }

            var queryBuilder = new StringBuilder($"SELECT * FROM {tableName}");
            var parameters = new DynamicParameters();

            if (predicate != null)
            {
                string whereClause = GenerateWhereClause(predicate, parameters);
                queryBuilder.Append($" WHERE {whereClause}");
            }

            queryBuilder.Append($" ORDER BY {primaryKeyName} OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");
            parameters.Add("Offset", (page - 1) * pageSize);
            parameters.Add("PageSize", pageSize);

            string query = queryBuilder.ToString();
            return await _dbConnection.QueryAsync<T>(query, parameters);
        }

        private string GenerateWhereClause(Expression<Func<T, bool>> predicate, DynamicParameters parameters)
        {
            var visitor = new SqlExpressionVisitor(parameters);
            return visitor.Translate(predicate.Body);
        } 
    }
}
