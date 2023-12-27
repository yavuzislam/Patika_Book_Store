using AutoMapper;
using BookStore.DbOperations;
using BookStore.Entities;

namespace BookStore.Application.GenreOperations.Commands.UpdateGenre;

public class UpdateGenreCommand
{
    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public UpdateGenreModel Model { get; set; }

    public int GenreId { get; set; }

    public UpdateGenreCommand(IBookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Handle()
    {
        var genre = _dbContext.Genres.SingleOrDefault(genre => genre.Id == GenreId);
        if (genre is null)
            throw new InvalidOperationException("Genre not found!");
        if (_dbContext.Genres.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId))
            throw new InvalidOperationException("Genre already exists!");

        genre.Name = Model.Name.Trim() != default ? Model.Name : genre.Name;
        genre.IsActive = Model.IsActivate;
        _dbContext.SaveChanges();
    }
}

public class UpdateGenreModel
{
    public string Name { get; set; }
    public bool IsActivate { get; set; } = true;
}