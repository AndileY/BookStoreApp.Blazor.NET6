using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreAppApI.Data;
using AutoMapper;
using BookStoreAppApI.Models.Book;
using BookStoreAppApI.Models.Author;
using AutoMapper.QueryableExtensions;

namespace BookStoreAppApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookStoreAppDboContext _context;
        private readonly IMapper mapper;

        public BooksController(BookStoreAppDboContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookReadOnlyDto>>> GetBooks()
        {
            var bookDtos = await _context.Books
                .Include(q => q.Author)
                .ProjectTo<BookReadOnlyDto>(mapper.ConfigurationProvider)
                .ToListAsync();
            //var bookDtos = mapper.Map<IEnumerable<BookReadOnlyDto>>(book);

            if (_context.Books == null)
          {
              return NotFound();
          }
            return Ok(bookDtos);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailsDto>> GetBook(int id)
        {
            var book = await _context.Books
               .Include(q => q.Author)
               .ProjectTo<BookDetailsDto>(mapper.ConfigurationProvider)
               .FirstOrDefaultAsync(q => q.Id == id);

            if (_context.Books == null)
            {
              return NotFound();
            }
            

            if (book == null)
            {
                return NotFound();
            }
         

            return book;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookUpdateDto bookDto)
        {
            if (id != bookDto.Id)
            {
                return BadRequest();
            }
            var book = await _context.Books.FindAsync(id);
            
            if(book == null)
            {
                return NotFound();

            }

            mapper.Map(bookDto, book);
            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookCreateDto>> PostBook(BookCreateDto bookDto)
        {
            var book = mapper.Map<Book>(bookDto);
          if (_context.Books == null)
          {
              return Problem("Entity set 'BookStoreAppDboContext.Books'  is null.");
          }

            // Check if the Author exists, if not, create a new one
            if (book.AuthorId.HasValue)
            {
                var author = await _context.Authors.FindAsync(book.AuthorId.Value);
                if (author != null)
                {
                    book.Author = author;  // Associate the existing author
                }
                else
                {
                    return BadRequest("Author not found");
                }
            }
            else
            {
                // You can handle creating a new Author if needed
                return BadRequest("AuthorId must be provided.");
            }


            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
