using Dapper;
using ElmBookShelf.Domain.Entities;
using ElmBookShelf.Infrastructure.Common;
using ElmBookShelf.Infrastructure.IRepositories; 
using System.Collections.Generic;
using System.Data; 
using System.Linq.Expressions; 
using System.Threading.Tasks;

namespace ElmBookShelf.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        private readonly IDbConnection _dbConnection;

        public GenericRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<T> GetByIdAsync(long id, params Expression<Func<T, object>>[] includes)
        {
            var tableName = typeof(T).Name;
            var query = GenerateSelectQueryWithJoins(tableName, includes) + " WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<T>(query, new { Id = id });
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            var tableName = typeof(T).Name;
            var query = GenerateSelectQueryWithJoins(tableName, includes);
            return await _dbConnection.QueryAsync<T>(query);
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate, int page, int pageSize, params Expression<Func<T, object>>[] includes)
        {
            var tableName = typeof(T).Name;
            var query = GenerateSelectQueryWithJoins(tableName, includes) + $" WHERE {predicate.ToSql()} OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
            return await _dbConnection.QueryAsync<T>(query, new { Offset = (page - 1) * pageSize, PageSize = pageSize });
        }

        public async Task<long> AddAsync(T entity)
        {
            var query = GenerateInsertQuery(entity);
            return await _dbConnection.ExecuteAsync(query, entity);
        }

        public async Task<long> AddAndGetIdAsync(T entity)
        {
            var tableName = typeof(T).Name;
            var query = $"{GenerateInsertQuery(entity)}; SELECT CAST(SCOPE_IDENTITY() as bigint)";
            return await _dbConnection.QuerySingleAsync<long>(query, entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            var query = GenerateInsertQuery(entities.First());
            await _dbConnection.ExecuteAsync(query, entities);
        }

        public async Task<long> UpdateAsync(T entity)
        {
            var query = GenerateUpdateQuery(entity);
            return await _dbConnection.ExecuteAsync(query, entity);
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            var query = GenerateUpdateQuery(entities.First());
            await _dbConnection.ExecuteAsync(query, entities);
        }

        public async Task<long> DeleteAsync(long id)
        {
            var tableName = typeof(T).Name;
            var query = $"DELETE FROM {tableName} WHERE Id = @Id";
            return await _dbConnection.ExecuteAsync(query, new { Id = id });
        }

        public async Task DeleteRangeAsync(IEnumerable<long> ids)
        {
            var tableName = typeof(T).Name;
            var query = $"DELETE FROM {tableName} WHERE Id IN @Ids";
            await _dbConnection.ExecuteAsync(query, new { Ids = ids });
        }

        private string GenerateSelectQueryWithJoins(string tableName, params Expression<Func<T, object>>[] includes)
        {
            var selectClause = $"SELECT * FROM {tableName}";
            var joinClauses = string.Join(" ", includes.Select(include => GenerateJoinClause(include)));
            return $"{selectClause} {joinClauses}";
        }

        private string GenerateJoinClause(Expression<Func<T, object>> include)
        {
            var body = (MemberExpression)include.Body;
            var relatedTable = body.Member.Name;
            var relatedTableForeignKey = $"{relatedTable}Id";
            return $"LEFT JOIN {relatedTable} ON {relatedTable}.{relatedTableForeignKey} = {typeof(T).Name}.{relatedTableForeignKey}";
        }

        private string GenerateInsertQuery(T entity)
        {
            var tableName = typeof(T).Name;
            var properties = typeof(T).GetProperties();
            var columns = string.Join(", ", properties.Select(p => p.Name));
            var values = string.Join(", ", properties.Select(p => $"@{p.Name}"));
            return $"INSERT INTO {tableName} ({columns}) VALUES ({values})";
        }

        private string GenerateUpdateQuery(T entity)
        {
            var tableName = typeof(T).Name;
            var properties = typeof(T).GetProperties().Where(p => p.Name != "Id");
            var setClause = string.Join(", ", properties.Select(p => $"{p.Name} = @{p.Name}"));
            return $"UPDATE {tableName} SET {setClause} WHERE Id = @Id";
        }
    }
}
