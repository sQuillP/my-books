using my_books.Data.ViewModels;

namespace my_books.Data.Models.Services
{
    public class PublisherService
    {
        private readonly AppDbContext context;

        public PublisherService(AppDbContext context)
        {
            this.context = context;
        }

        public void AddPublisher(PublisherVM publisherVM)
        {
            var publisher = new Publisher()
            {
                Name = publisherVM.Name
            };
            context.Publishers.Add(publisher);
            context.SaveChanges();
        }


        public PublisherWithBooksAndAuthorsVM GetPublisherData(int publisherId)
        {
            var publisherData = context.Publishers.Where(n => n.Id == publisherId)
                .Select(n => new PublisherWithBooksAndAuthorsVM()
                {
                    Name = n.Name,
                    BookAuthors = n.Books.Select(n => new BookAuthorVM()
                    {
                        BookName = n.Title,
                        BookAuthors = n.Book_Authors.Select(n => n.Author.FullName).ToList()
                    }).ToList()
                }).FirstOrDefault();

            return publisherData;
        }
    }
}
