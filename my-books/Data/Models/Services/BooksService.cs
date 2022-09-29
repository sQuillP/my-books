using my_books.Data.ViewModels;
using System.Threading;

namespace my_books.Data.Models.Services
{
    public class BooksService
    {
        private readonly AppDbContext appDbContext;

        public BooksService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        public void AddBookWIthAuthors(BookVM book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate.Value : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now,
                PublisherId = book.PublisherId
            };
            appDbContext.Books.Add(_book);
            appDbContext.SaveChanges();

            foreach(var id in book.AuthorIds)
            {
                var _book_author = new Book_Author()
                {
                    BookId = _book.Id,
                    AuthorId = id
                };
                appDbContext.Book_Authors.Add(_book_author);
                appDbContext.SaveChanges();
            }
        }


        public List<Book> GetAllBooks()
        {
            var allBooks = appDbContext.Books.ToList();
            return allBooks;
        }

        public BookWithAuthorsVm GetBookById(int bookId)
        {
            //Lookup queries with entityframework.sql
            var bookWithAuthors = appDbContext.Books
                .Where(n => n.Id == bookId)
                .Select(book => new BookWithAuthorsVm()
                {
                    Title = book.Title,
                    Description = book.Description,
                    IsRead = book.IsRead,
                    DateRead = book.IsRead ? book.DateRead.Value : null,
                    Rate = book.IsRead ? book.Rate.Value : null,
                    Genre = book.Genre,
                    CoverUrl = book.CoverUrl,
                    PublisherName = book.Publisher.Name,
                    AuthorNames = book.Book_Authors.Select(n => n.Author.FullName).ToList()
                }).FirstOrDefault();

            return bookWithAuthors;
        }

        public Book UpdateBookById(int bookId, BookVM book)
        {
            var _book = appDbContext.Books.FirstOrDefault(
                book => book.Id == bookId);
            if(_book != null)
            {
                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.IsRead = book.IsRead;
                _book.DateRead = book.IsRead ? book.DateRead.Value : null;
                _book.Rate = book.IsRead ? book.Rate.Value : null;
                _book.Genre = book.Genre;
                _book.CoverUrl = book.CoverUrl;
                _book.DateAdded = DateTime.Now;
                appDbContext.SaveChanges();
            }

            return _book;
        }


        public void DeleteBookById(int bookId)
        {
            var _book = appDbContext.Books.FirstOrDefault(book => book.Id == bookId);
            if(_book != null)
            {
                appDbContext.Books.Remove(_book);
                appDbContext.SaveChanges();
            }
        }
    }
}
