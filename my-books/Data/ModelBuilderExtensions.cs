using Microsoft.EntityFrameworkCore;
using my_books.Data.Models;

namespace my_books.Data
{
    public static class ModelBuilderExtensions
    {

        public static void Seed(this ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Book>().HasData(
            //    new Book()
            //    {
            //        Title = "Alice in Wonderland",
            //        Description = "A very adventerous book",
            //        IsRead = true,
            //        DateRead = DateTime.Now.AddDays(-10),
            //        Rate = 4,
            //        Genre = "Fiction",
            //        Author = "William Pattison",
            //        CoverUrl = "https:...",
            //        DateAdded = DateTime.Now
            //    },
            //    new Book()
            //    {
            //        Title = "Great Gatsby",
            //        Description = "A wonderful read",
            //        IsRead = true,
            //        DateRead = DateTime.Now.AddDays(-10),
            //        Rate = 4,
            //        Genre = "Fiction",
            //        Author = "Bob Smith",
            //        CoverUrl = "https:...",
            //        DateAdded = DateTime.Now
            //    }
            //    );
        }
    }
}
