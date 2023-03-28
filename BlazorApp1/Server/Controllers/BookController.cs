using BlazorApp1.Data;
using BlazorApp1.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Server.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {

        public BookController(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        public AppDbContext AppDbContext { get; }


        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            await AppDbContext.Books.AddAsync(book);
            await AppDbContext.SaveChangesAsync();
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var list = await AppDbContext.Books.ToArrayAsync();

            return Ok(list);
        }
    }
}
