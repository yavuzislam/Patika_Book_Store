using AutoMapper;
using BookStore.DbOperations;

namespace BookStore.Application.GenreOperations.Queries.GetGenres;

public class GetGenresQuery
{
    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetGenresQuery(IBookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public IEnumerable<GenreModel> Handle()
    {
        var genreList = _dbContext.Genres.Where(x => x.IsActive).OrderBy(x => x.Id).ToList();
        var result = _mapper.Map<List<GenreModel>>(genreList);
        return result;
    }
}

public class GenreModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}