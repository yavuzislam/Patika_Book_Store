using BookStore.BookOperation.CreateBook;
using BookStore.BookOperation.DeleteBook;
using BookStore.BookOperation.GetBooks;
using BookStore.BookOperation.UpdateBook;
using BookStore.DbOperations;
using BookStore.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new(_context);
            var result = query.Handle();
            return Ok(result);
        }

        // GET: api/Book/5
        [HttpGet("{id}", Name = "GetById")]
        public IActionResult Get(int id)
        {
            GetBookByIdQuery query = new(_context);
            try
            {
                query.BookId = id;
                var result = query.Handle();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Book
        [HttpPost]
        public IActionResult Post([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        // PUT: api/Book/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateBookModel updateBook)
        {
            UpdateBookCommand command = new(_context);
            try
            {
                command.Model = updateBook;
                command.BookId = id;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }


        // DELETE: api/Book/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            DeleteBookCommand command = new(_context);
            try
            {
                command.BookId = id;
                command.Handle();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}