using ElmBookShelf.Application.Services;
using ElmBookShelf.Domain.QueryOptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElmBookShelf.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategory(long id)
        {
            var category = await _categoryService.GetCategory(id);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories(QueryOption queryOption)
        {
            var categories = await _categoryService.GetCategories(queryOption);

            return Ok();
        }
    }
}
