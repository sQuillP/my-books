using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Models.Services;
using my_books.Data.ViewModels;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorsService authorDb;

        public AuthorsController(AuthorsService authorsService)
        {
           authorDb = authorsService;
        }

        [HttpPost("add-author")]
        public IActionResult AddAuthor([FromBody]AuthorVM authorVM)
        {
            authorDb.AddAuthor(authorVM);
            return Ok();
        }


        [HttpGet("get-author-with-books-by-id/{authorId}")]
        public IActionResult GetAuthorWithBooks(int authorId)
        {
            var response = authorDb.GetAuthorWithBooks(authorId);
            return Ok(response);
        }
    }
}
