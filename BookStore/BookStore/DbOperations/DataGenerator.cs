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

            context.Authors.AddRange(new Author()
                {
                    Name = "John",
                    Surname = "Steinbeck",
                    BirthDate = new DateTime(1902, 2, 27)
                }, new Author()
                {
                    Name = "F. Scott",
                    Surname = "Fitzgerald",
                    BirthDate = new DateTime(1896, 9, 24)
                },
                new Author()
                {
                    Name = "George",
                    Surname = "Orwell",
                    BirthDate = new DateTime(1903, 6, 25)
                });

            context.Genres.AddRange(
                new Genre()
                {
                    Name = "Personal Growth"
                },
                new Genre()
                {
                    Name = "Science Fiction"
                },
                new Genre()
                {
                    Name = "Romance"
                }
            );

            context.Books.AddRange(
                new Book()
                {
                    Title = "The Grapes of Wrath",
                    GenreId = 2,
                    AuthorId = 1,
                    PageCount = 464,
                    PublishDate = new System.DateTime(1939, 4, 14)
                },
                new Book()
                {
                    Title = "The Great Gatsby",
                    GenreId = 1,
                    AuthorId = 2,
                    PageCount = 180,
                    PublishDate = new System.DateTime(1925, 4, 10)
                },
                new Book()
                {
                    Title = "Nineteen Eighty-Four",
                    GenreId = 1,
                    AuthorId = 3,
                    PageCount = 328,
                    PublishDate = new System.DateTime(1949, 6, 8)
                }
            );
            context.SaveChanges();
        }
    }
}