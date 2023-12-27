using AutoMapper;
using BookStore.Application.BookOperation.Commands.CreateBook;
using BookStore.Application.BookOperation.Commands.DeleteBook;
using BookStore.Application.BookOperation.Commands.UpdateBook;
using BookStore.Application.BookOperation.Queries.GetBooks;
using BookStore.DbOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        // GET: api/Book/5
        [HttpGet("{id}", Name = "GetById")]
        public IActionResult Get(int id)
        {
            GetBookByIdQuery query = new(_context, _mapper);
            query.BookId = id;

            GetBookByIdQueryValidator validator = new GetBookByIdQueryValidator();
            validator.ValidateAndThrow(query);
            var result = query.Handle();

            return Ok(result);


            // GetBookByIdQuery query = new(_context, _mapper);
            // try
            // {
            //     query.BookId = id;
            //     GetBookByIdQueryValidator validator = new GetBookByIdQueryValidator();
            //     validator.ValidateAndThrow(query);
            //     var result = query.Handle();
            //     return Ok(result);
            // }
            // catch (Exception ex)
            // {
            //     return BadRequest(ex.Message);
            // }
        }

        // POST: api/Book
        [HttpPost]
        public IActionResult Post([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new(_context, _mapper);
            command.Model = newBook;
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();

            // ValidationResult result = validator.Validate(command);
            // if (!result.IsValid)
            //     foreach (var item in result.Errors)
            //         Console.WriteLine("Property :" + item.PropertyName + " Error Message :" + item.ErrorMessage);
            // else
            //     command.Handle();


            // CreateBookCommand command = new(_context, _mapper);
            // try
            // {
            //     command.Model = newBook;
            //     CreateBookCommandValidator validator = new CreateBookCommandValidator();
            //     validator.ValidateAndThrow(command);
            //     command.Handle();
            //
            //     // ValidationResult result = validator.Validate(command);
            //     // if (!result.IsValid)
            //     //     foreach (var item in result.Errors)
            //     //         Console.WriteLine("Property :" + item.PropertyName + " Error Message :" + item.ErrorMessage);
            //     // else
            //     //     command.Handle();
            // }
            // catch (Exception ex)
            // {
            //     return BadRequest(ex.Message);
            // }

            // return Ok();
        }

        // PUT: api/Book/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateBookModel updateBook)
        {
            UpdateBookCommand command = new(_context, _mapper);
            command.Model = updateBook;
            command.BookId = id;

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();


            // UpdateBookCommand command = new(_context, _mapper);
            // try
            // {
            //     command.Model = updateBook;
            //     command.BookId = id;
            //     UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            //     validator.ValidateAndThrow(command);
            //     command.Handle();
            // }
            // catch (Exception ex)
            // {
            //     return BadRequest(ex.Message);
            // }
            //
            // return Ok();
        }


        // DELETE: api/Book/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DeleteBookCommand command = new(_context);
            command.BookId = id;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();

            // DeleteBookCommand command = new(_context);
            // try
            // {
            //     command.BookId = id;
            //     DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            //     validator.ValidateAndThrow(command);
            //     command.Handle();
            //     return Ok();
            // }
            // catch (Exception ex)
            // {
            //     return BadRequest(ex.Message);
            // }
        }
    }
}