using AutoMapper;
using BookStore.DbOperations;

namespace BookStore.Application.AuthorOperations.Queries.GetAuthor;

public class GetAuthorsQuery
{
    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAuthorsQuery(IBookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public IEnumerable<AuthorModel> Handle()
    {
        var authors = _dbContext.Authors.OrderBy(x => x.Id).ToList();
        var result = _mapper.Map<List<AuthorModel>>(authors);
        return result;
    }
}

public class AuthorModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string BirthDate { get; set; }
}