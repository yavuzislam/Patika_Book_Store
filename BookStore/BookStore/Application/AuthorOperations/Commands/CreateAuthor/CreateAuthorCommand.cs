using AutoMapper;
using BookStore.DbOperations;
using BookStore.Entities;

namespace BookStore.Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommand
{
    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateAuthorModel Model { get; set; }

    public CreateAuthorCommand(IBookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Handle()
    {
        var author =
            _dbContext.Authors.SingleOrDefault(x =>
                string.Equals(x.Name.ToLower(), Model.Name.ToLower(), StringComparison.Ordinal)
                && string.Equals(x.Surname.ToLower(), Model.Surname.ToLower(), StringComparison.Ordinal));
        if (author is not null)
            throw new InvalidOperationException("Author already exists!");
        var result = _mapper.Map<Author>(Model);
        _dbContext.Authors.Add(result);
        _dbContext.SaveChanges();
    }
}

public class CreateAuthorModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDate { get; set; }
}