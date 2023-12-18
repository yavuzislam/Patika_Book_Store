using BookStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DbOperations;

public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context =
               new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
        {
            if (context.Books.Any())
            {
                return;
            }

            context.Books.AddRange(
                new Book()
                {
                    Title = "The Grapes of Wrath",
                    GenreId = 2,
                    PageCount = 464,
                    PublishDate = new System.DateTime(1939, 4, 14)
                },
                new Book()
                {
                    Title = "The Great Gatsby",
                    GenreId = 1,
                    PageCount = 180,
                    PublishDate = new System.DateTime(1925, 4, 10)
                },
                new Book()
                {
                    Title = "Nineteen Eighty-Four",
                    GenreId = 1,
                    PageCount = 328,
                    PublishDate = new System.DateTime(1949, 6, 8)
                }
            );
            context.SaveChanges();
        }
    }
}