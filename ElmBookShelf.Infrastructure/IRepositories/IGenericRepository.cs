using ElmBookShelf.Domain.Entities; 
using System.Collections.Generic; 
using System.Linq.Expressions; 
using System.Threading.Tasks;

namespace ElmBookShelf.Infrastructure.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(long id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate, int page, int pageSize);
    }
}
