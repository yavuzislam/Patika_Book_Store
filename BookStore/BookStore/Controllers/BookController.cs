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
        public IEnumerable<Book> GetBooks()
        {
            var bookList = _context.Books.OrderBy(x => x.Id).ToList();
            return bookList;
        }

        // GET: api/Book/5
        [HttpGet("{id}", Name = "Get")]
        public Book Get(int id)
        {
            var book = _context.Books.Find(id);
            return book;
        }

        // POST: api/Book
        [HttpPost]
        public IActionResult Post([FromBody] Book newBook)
        {
            var newbook = _context.Books.SingleOrDefault(x => x.Title == newBook.Title);
            if (newbook is not null)
            {
                return BadRequest("Book already exists");
            }

            _context.Books.Add(newBook);
            _context.SaveChanges();
            return Ok();
        }

        // PUT: api/Book/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Book updateBook)
        {
            var book = _context.Books.Find(id);
            if (book is null)
            {
                return BadRequest("Book not found");
            }

            book.Title = updateBook.Title != default ? updateBook.Title : book.Title;
            book.GenreId = updateBook.GenreId != default ? updateBook.GenreId : book.GenreId;
            book.PageCount = updateBook.PageCount != default ? updateBook.PageCount : book.PageCount;
            book.PublishDate = updateBook.PublishDate != default ? updateBook.PublishDate : book.PublishDate;

            _context.SaveChanges();
            return Ok();
        }

        // DELETE: api/Book/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book is null)
            {
                return BadRequest("Book not found");
            }

            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
    }
}