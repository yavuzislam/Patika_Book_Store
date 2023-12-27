using AutoMapper;
using BookStore.DbOperations;
using BookStore.Entities;

namespace BookStore.Application.GenreOperations.Queries.GetGenreDetail;

public class GetGenreDetailQuery
{
    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public int GenreId { get; set; }

    public GetGenreDetailQuery(IBookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public GetGenreDetailModel Handle()
    {
        var genre = _dbContext.Genres.SingleOrDefault(x => x.Id == GenreId && x.IsActive);
        if (genre is null)
            throw new InvalidOperationException("Genre not found.");

        return _mapper.Map<GetGenreDetailModel>(genre);
    }
}

public class GetGenreDetailModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}