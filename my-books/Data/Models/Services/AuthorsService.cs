using my_books.Data.ViewModels;

namespace my_books.Data.Models.Services
{
    public class AuthorsService
    {
        private readonly AppDbContext context;

        public AuthorsService(AppDbContext appDbContext)
        {
            this.context = appDbContext;
        }

        public void AddAuthor(AuthorVM authorVM)
        {
            var author = new Author()
            {
                FullName = authorVM.FullName
            };

            context.Authors.Add(author);
            context.SaveChanges();
        }


        public AuthorWithBooksVM GetAuthorWithBooks(int authorId)
        {
            var author = context.Authors.Where(n => n.Id == authorId)
                .Select(n => new AuthorWithBooksVM()
                {
                    FullName = n.FullName,
                    BookTitles = n.Book_Authors.Select(n => n.Book.Title).ToList()
                }).FirstOrDefault();

            return author;
        }

    }
}
