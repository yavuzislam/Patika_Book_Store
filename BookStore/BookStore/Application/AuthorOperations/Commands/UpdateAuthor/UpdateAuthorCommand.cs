using AutoMapper;
using BookStore.DbOperations;
using BookStore.Entities;

namespace BookStore.Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommand
{
    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public UpdateAuthorModel Model { get; set; }
    public int AuthorId { get; set; }

    public UpdateAuthorCommand(IBookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Handle()
    {
        var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);
        if (author is null)
            throw new InvalidOperationException("Author not found.");

        if (_dbContext.Authors.Any(x =>
                x.Name.ToLower() == Model.Name.ToLower() && x.Surname.ToLower() == Model.Surname.ToLower() &&
                x.Id != AuthorId))
            throw new InvalidOperationException("Author already exists!");
        // if (_dbContext.Authors.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Surname.ToLower() == Model.Surname.ToLower() && x.Id != AuthorId))
        _mapper.Map(Model, author);
        _dbContext.SaveChanges();
    }
}

public class UpdateAuthorModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public DateTime BirthDate { get; set; }
}