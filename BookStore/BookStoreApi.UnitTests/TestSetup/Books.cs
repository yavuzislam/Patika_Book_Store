using BookStore.DbOperations;
using BookStore.Entities;

namespace BookStoreApi.UnitTests.TestSetup;

public static class Books
{
    public static void AddBooks(this BookStoreDbContext context)
    {
        context.Books.AddRange(
            new Book()
            {
                Title = "The Grapes of Wrath",
                GenreId = 2,
                AuthorId = 1,
                PageCount = 464,
                PublishDate = new DateTime(1939, 4, 14)
            },
            new Book()
            {
                Title = "The Great Gatsby",
                GenreId = 1,
                AuthorId = 2,
                PageCount = 180,
                PublishDate = new DateTime(1925, 4, 10)
            },
            new Book()
            {
                Title = "Nineteen Eighty-Four",
                GenreId = 1,
                AuthorId = 3,
                PageCount = 328,
                PublishDate = new DateTime(1949, 6, 8)
            }
        );
    }
}