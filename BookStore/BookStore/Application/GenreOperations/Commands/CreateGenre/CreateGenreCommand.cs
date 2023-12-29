using AutoMapper;
using BookStore.DbOperations;
using BookStore.Entities;

namespace BookStore.Application.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommand
{
    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateGenreModel Model { get; set; }

    public CreateGenreCommand(IBookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Handle()
    {
        var genre = _dbContext.Genres.SingleOrDefault(x => x.Name == Model.Name);
        if (genre is not null)
            throw new InvalidOperationException("Genre already exists!");
        genre = _mapper.Map<Genre>(Model);
        _dbContext.Genres.Add(genre);
        _dbContext.SaveChanges();
    }
}

public class CreateGenreModel
{
    public string Name { get; set; }
}