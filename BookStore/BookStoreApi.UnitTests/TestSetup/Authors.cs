using BookStore.DbOperations;
using BookStore.Entities;

namespace BookStoreApi.UnitTests.TestSetup;

public static class Authors
{
    public static void AddAuthors(this BookStoreDbContext context)
    {
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
            }
        );
    }
}