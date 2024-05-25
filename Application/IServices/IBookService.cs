using ElmBookShelf.Domain.QueryOptions;
using ElmBookShelf.Domain.ViewModels;
using System.Threading.Tasks;

namespace ElmBookShelf.Application.IServices
{
    public interface IBookService
    { 
        Task<List<BookViewModel>> GetBooks(QueryOption queryOption);
    }
}
