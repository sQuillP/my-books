using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data;
using my_books.Data.Models.Services;
using my_books.Data.ViewModels;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BooksService booksService;

        public BooksController(BooksService booksService)
        {
            this.booksService = booksService;
        }

        [HttpPost("add-book-with-authors")]
        public IActionResult AddBook([FromBody]BookVM book)
        {
            booksService.AddBookWIthAuthors(book);

            return Ok();
        }

        [HttpGet("get-all-books")]
        public IActionResult GetAllBooks()
        {
            var books = booksService.GetAllBooks();
            return Ok(books);
        }

        //NOTE: param id must match method parameter!!!
        [HttpGet("get-book-by-id/{bookId}")]
        public IActionResult GetBookById(int bookId)
        {
            var book = booksService.GetBookById(bookId);

            return Ok(book);
        }


        [HttpPut("update-book-by-id/{bookId}")]
        public IActionResult UpdateBookById(int bookId, [FromBody]BookVM book)
        {
            var update = booksService.UpdateBookById(bookId, book);
            return Ok(update);
        }

        [HttpDelete("delete-book-by-id/{bookId}")]
        public IActionResult DeleteBookById(int bookId)
        {
            booksService.DeleteBookById(bookId);
            return Ok();
        }
    }
}
