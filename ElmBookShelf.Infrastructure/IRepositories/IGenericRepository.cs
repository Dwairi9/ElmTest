using ElmBookShelf.Domain.Entities; 
using System.Collections.Generic; 
using System.Linq.Expressions; 
using System.Threading.Tasks;

namespace ElmBookShelf.Infrastructure.IRepositories
{
    public interface IGenericRepository<T> where T : Entity
    {
        Task<T> GetByIdAsync(long id, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate, int page, int pageSize, params Expression<Func<T, object>>[] includes);
        Task<long> AddAsync(T entity);
        Task<long> AddAndGetIdAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task<long> UpdateAsync(T entity);
        Task UpdateRangeAsync(IEnumerable<T> entities);
        Task<long> DeleteAsync(long id);
        Task DeleteRangeAsync(IEnumerable<long> ids);
    }
}
