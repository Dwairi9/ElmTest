using ElmBookShelf.Application.Common;
using ElmBookShelf.Application.IServices;
using ElmBookShelf.Domain.Entities;
using ElmBookShelf.Domain.QueryOptions;
using ElmBookShelf.Domain.ViewModels;
using ElmBookShelf.Infrastructure.IRepositories; 
using System.Collections.Generic; 
using System.Threading.Tasks;

namespace ElmBookShelf.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _genericRepository;

        public CategoryService(IGenericRepository<Category> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<CategoryViewModel> GetCategory(long id)
        {
            var catigory = await _genericRepository.GetByIdAsync(id);

            var catigoryViewModel = MapperProfile.Mapper.Map<CategoryViewModel>(catigory);

            return catigoryViewModel;
        }

        public async Task<List<CategoryViewModel>> GetCategories(QueryOption queryOption)
        {
            var catigories = await _genericRepository.GetAllAsync(b =>
                (string.IsNullOrEmpty(queryOption.SearchKey) || b.Name.Contains(queryOption.SearchKey, StringComparison.OrdinalIgnoreCase)));

            var catigoriesViewModel = MapperProfile.Mapper.Map<List<CategoryViewModel>>(catigories);

            return catigoriesViewModel;
        }
    }
}
