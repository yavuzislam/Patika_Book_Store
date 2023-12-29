using AutoMapper;
using BookStore.Application.GenreOperations.Commands.CreateGenre;
using BookStore.Application.GenreOperations.Commands.DeleteGenre;
using BookStore.Application.GenreOperations.Commands.UpdateGenre;
using BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using BookStore.Application.GenreOperations.Queries.GetGenres;
using BookStore.DbOperations;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenreController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/GenreControllers
        [HttpGet]
        public IActionResult Get()
        {
            var query = new GetGenresQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        // GET: api/GenreControllers/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var query = new GetGenreDetailQuery(_context, _mapper)
            {
                GenreId = id
            };
            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            validator.ValidateAndThrow(query);

            var result = query.Handle();
            return Ok(result);
        }

        // POST: api/GenreControllers
        [HttpPost]
        public IActionResult Post([FromBody] CreateGenreModel newGenre)
        {
            var command = new CreateGenreCommand(_context, _mapper)
            {
                Model = newGenre
            };

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        // PUT: api/GenreControllers/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateGenreModel updateGenreModel)
        {
            var command = new UpdateGenreCommand(_context, _mapper)
            {
                Model = updateGenreModel,
                GenreId = id
            };
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        // DELETE: api/GenreControllers/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var command = new DeleteGenreCommand(_context)
            {
                Id = id
            };
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}