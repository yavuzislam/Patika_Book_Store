using AutoMapper;
using BookStore.DbOperations;
using BookStore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.UnitTests.TestSetup;

public class CommonTestFixture
{
    public BookStoreDbContext Context { get; set; }
    public IMapper Mapper { get; set; }

    public CommonTestFixture()
    {
        var options = new DbContextOptionsBuilder<BookStoreDbContext>()
            .UseInMemoryDatabase("TestDb").Options;

        Context = new BookStoreDbContext(options);
        Context.Database.EnsureCreated();
        Context.AddBooks();
        Context.AddAuthors();
        Context.AddGenres(); 

        Mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        }).CreateMapper();
    }
}