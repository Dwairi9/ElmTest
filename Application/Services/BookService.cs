using ElmBookShelf.Application.Common;
using ElmBookShelf.Application.IServices;
using ElmBookShelf.Domain.Entities;
using ElmBookShelf.Domain.QueryOptions;
using ElmBookShelf.Domain.ViewModels;
using ElmBookShelf.Infrastructure.IRepositories;
using Newtonsoft.Json;
using System.Collections.Generic; 
using System.Threading.Tasks;

namespace ElmBookShelf.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IGenericRepository<Book> _genericRepository;

        public BookService(IGenericRepository<Book> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<BookViewModel> GetBook(long id)
        {
            var book = await _genericRepository.GetByIdAsync(id, b => b.Category);

            var bookViewModel = MapperProfile.Mapper.Map<BookViewModel>(book);

            return bookViewModel;
        }

        public async Task<List<BookViewModel>> GetBooks(BookQueryOption queryOption) 
        {
            var books = await _genericRepository.GetAsync(b =>
                (queryOption.CategoryId == null || b.CategoryId == queryOption.CategoryId) &&
                (string.IsNullOrEmpty(queryOption.SearchKey) || b.BookInfo.Contains(queryOption.SearchKey, StringComparison.OrdinalIgnoreCase)),
            queryOption.Page, queryOption.PageSize,
            b=> b.Category);

            var booksViewModel = MapperProfile.Mapper.Map<List<BookViewModel>>(books);

            return booksViewModel;
        } 
    }
}
