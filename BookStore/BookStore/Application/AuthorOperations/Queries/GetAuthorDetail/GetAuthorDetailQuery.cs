using AutoMapper;
using BookStore.DbOperations;

namespace BookStore.Application.AuthorOperations.Queries.GetAuthorDetail;

public class GetAuthorDetailQuery
{
    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public int AuthorId { get; set; }

    public GetAuthorDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public AuthorDetailModel Handle()
    {
        var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);
        if (author is null)
        {
            throw new InvalidOperationException("Author not found.");
        }

        var result = _mapper.Map<AuthorDetailModel>(author);
        return result;
    }
}

public class AuthorDetailModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string BirthDate { get; set; }
}