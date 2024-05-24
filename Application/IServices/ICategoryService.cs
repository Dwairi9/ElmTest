using ElmBookShelf.Domain.QueryOptions;
using ElmBookShelf.Domain.ViewModels;

namespace ElmBookShelf.Application.IServices
{
    public interface ICategoryService
    {
        Task<CategoryViewModel> GetCategory(long id);
        Task<List<CategoryViewModel>> GetCategories(QueryOption queryOption);
    }
}
