using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using BookStore.Application.AuthorOperations.Commands.DeleteAuthor;
using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using BookStore.Application.AuthorOperations.Queries.GetAuthor;
using BookStore.Application.AuthorOperations.Queries.GetAuthorDetail;
using BookStore.DbOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class AuthorController : ControllerBase
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public AuthorController(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAuthors()
    {
        GetAuthorsQuery query = new(_context, _mapper);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}", Name = "GetAuthorById")]
    public IActionResult Get(int id)
    {
        GetAuthorDetailQuery query = new(_context, _mapper)
        {
            AuthorId = id
        };
        GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
        validator.ValidateAndThrow(query);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateAuthorModel newAuthor)
    {
        CreateAuthorCommand command = new(_context, _mapper)
        {
            Model = newAuthor
        };
        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] UpdateAuthorModel updateAuthorModel)
    {
        var command = new UpdateAuthorCommand(_context, _mapper)
        {
            AuthorId = id,
            Model = updateAuthorModel
        };
        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var command = new DeleteAuthorCommand(_context)
        {
            AuthorId = id
        };
        DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator(_context);
        validator.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }
}