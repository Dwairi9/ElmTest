using ElmBookShelf.Application.Common;
using ElmBookShelf.Application.IServices;
using ElmBookShelf.Domain.Entities;
using ElmBookShelf.Domain.QueryOptions;
using ElmBookShelf.Domain.ViewModels;
using ElmBookShelf.Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq.Expressions;
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

        public async Task<List<BookViewModel>> GetBooks(QueryOption queryOption) 
        {  
            Expression<Func<Book, bool>> predicate = null;
            string searchKey = queryOption.SearchKey;

            if (!string.IsNullOrEmpty(queryOption.SearchKey))
            {
                predicate = b => b.Title.Contains(searchKey) || b.Author.Contains(searchKey) || b.Description.Contains(searchKey) || b.PublishDate.ToString().Contains(searchKey);
            }

            var books = await _genericRepository.GetAsync(predicate, queryOption.Page, queryOption.PageSize); 
            var booksViewModel = MapperProfile.Mapper.Map<List<BookViewModel>>(books); 

            return booksViewModel;
        } 
    }
}
