using IeltsApp.DataAccess.Interface;
using IeltsApp.DataAccess.Models;
using IeltsApp.DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace IeltsApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogDatabaseService _databaseService;

        public BlogController(IBlogDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet]
        public async Task<List<Blog>> Get() =>
            await _databaseService.GetAsync();

        [HttpGet("{title}")]
        public async Task<ActionResult<Blog>> Get(string title)
        {
            var blog = await _databaseService.GetByTitleAsync(title);

            if (blog is null)
            {
                return NotFound();
            }

            return blog;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Blog newBlog)
        {
            await _databaseService.CreateAsync(newBlog);

            return CreatedAtAction(nameof(Get), new { id = newBlog.Id }, newBlog);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Blog updatedBlog)
        {
            var book = await _databaseService.GetByIdAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            updatedBlog.Id = book.Id;

            await _databaseService.UpdateAsync(id, updatedBlog);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var book = await _databaseService.GetByIdAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            await _databaseService.RemoveAsync(id);

            return NoContent();
        }


    }
}
