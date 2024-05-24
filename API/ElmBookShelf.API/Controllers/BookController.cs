using ElmBookShelf.Application.Services;
using ElmBookShelf.Domain.QueryOptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElmBookShelf.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBook(long id)
        {
            var book = await _bookService.GetBook(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks(BookQueryOption queryOption) 
        {
            var books = await _bookService.GetBooks(queryOption);
            return Ok();
        }
    }
}
