using my_books.Data.Paging;
using my_books.Data.ViewModels;
using my_books.Exceptions;
using System.Text.RegularExpressions;

namespace my_books.Data.Models.Services
{
    public class PublisherService
    {
        private readonly AppDbContext context;

        public PublisherService(AppDbContext context)
        {
            this.context = context;
        }


        public List<Publisher> GetAllPublishers(string sortBy, string searchString, int? pageNumber)
        {
            var publishers = context.Publishers.OrderBy(x => x.Name).ToList();

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "name_desc":
                        publishers = publishers.OrderByDescending(n => n.Name).ToList();
                        break;
                    default:
                        break;
                }
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                publishers = publishers
                    .Where(n => 
                    n.Name
                    .Contains(searchString, StringComparison.CurrentCultureIgnoreCase))
                    .ToList();
            }
            //paging
            int pageSize = 5;
            publishers = PaginatedList<Publisher>
                .Create(publishers.AsQueryable(), pageNumber ?? 1, pageSize);


            return publishers;
        }




        public Publisher AddPublisher(PublisherVM publisherVM)
        {

            if (StringStartsWithNumber(publisherVM.Name))
                throw new PublisherNameException("Name starts with number", publisherVM.Name);


            var publisher = new Publisher()
            {
                Name = publisherVM.Name
            };
            context.Publishers.Add(publisher);
            context.SaveChanges();
            return publisher;
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

        public void deletePublisherById(int id)
        {
            var publisher = context.Publishers.FirstOrDefault(n => n.Id == id);
            if(publisher != null)
            {
                context.Publishers.Remove(publisher);
                context.SaveChanges();
            }
            else
            {
                throw new Exception($"The publisher with id: {id} does not exist");
            }
        }

        private bool StringStartsWithNumber(string name)
        {
            return Regex.IsMatch(name, @"^\d");
        }


        public Publisher GetPublisherById(int id) => 
            context.Publishers.FirstOrDefault(n => n.Id == id);
    }
}
