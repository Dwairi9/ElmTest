using ElmBookShelf.Application.IServices; 
using ElmBookShelf.Domain.QueryOptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElmBookShelf.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookService bookService, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        } 

        [HttpPost("GetBooks")]
        public async Task<IActionResult> GetBooks(QueryOption queryOption) 
        {
            try
            {
                var books = await _bookService.GetBooks(queryOption);
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the book.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
