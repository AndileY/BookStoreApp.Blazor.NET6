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
using static System.Reflection.Metadata.BlobBuilder;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreAppApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly BookStoreAppDboContext _context;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment webHostEnvironment;

        public BooksController(BookStoreAppDboContext context, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.mapper = mapper;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookReadOnlyDto>>> GetBooks()
        {
            //var bookDtos = await _context.Books
            //    .Include(q => q.Author)
            //    .ProjectTo<BookReadOnlyDto>(mapper.ConfigurationProvider)
            //    .ToListAsync();


            //if (_context.Books == null)
            //{
            //    return NotFound();
            //}
            if (_context.Books == null)
            {
                return NotFound();
            }

            var bookDtos = await _context.Books
                .Include(q => q.Author)
                .Select(book => new BookReadOnlyDto
                {
                    Id = book.Id,
                    Title = book.Title,
                    AuthorId = book.AuthorId ?? 0,
                    AuthorName = book.Author != null ? $"{book.Author.FirstName} {book.Author.LastName}" : "",
                    Image = book.Image,
                    Price = (double)(book.Price ?? 0)
                })
                .ToListAsync();



            return Ok(bookDtos);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailsDto>> GetBook(int id)
        {
            //var book = await _context.Books
            //   .Include(q => q.Author)
            //   .ProjectTo<BookDetailsDto>(mapper.ConfigurationProvider)
            //   .FirstOrDefaultAsync(q => q.Id == id);

            //if (_context.Books == null)
            //{
            //  return NotFound();
            //}


            //if (book == null)
            //{
            //    return NotFound();
            //}
            if (_context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(q => q.Author)
                .Where(q => q.Id == id)
                .Select(b => new BookDetailsDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Year = (int)b.Year,
                    Isbn = b.Isbn,
                    Summary = b.Summary,
                    Image = b.Image,
                    Price = (double)(b.Price ?? 0), // Explicit conversion
                    AuthorId = b.AuthorId??0
,
                    AuthorName = b.Author != null ? $"{b.Author.FirstName} {b.Author.LastName}" : ""
                })
                .FirstOrDefaultAsync();

            if (book == null)
            {
                return NotFound();
            }




            return book;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
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

            if (string.IsNullOrEmpty(bookDto.ImageData) == false)
            {
                bookDto.Image = CreateFile(bookDto.ImageData, bookDto.OriginalImageName);
                var picName = Path.GetFileName(book.Image);
                var path = $"{webHostEnvironment.WebRootPath}\\bookcoverimages\\{picName}";
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
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
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<BookCreateDto>> PostBook(BookCreateDto bookDto)
        {
            
            var book = mapper.Map<Book>(bookDto);
            book.Image = CreateFile(bookDto.ImageData, bookDto.OriginalImageName);
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
        [Authorize(Roles = "Administrator")]
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


        private string CreateFile(string imageBase64, string imageName)
        {
            var url = HttpContext.Request.Host.Value;
            var ext = Path.GetExtension(imageName);
            var fileName = $"{Guid.NewGuid()}.{ext}";

            var path = $"{webHostEnvironment.WebRootPath}\\bookcoverimages\\{fileName}";

            byte[] image = Convert.FromBase64String(imageBase64) ;
            var fileStream = System.IO.File.Create(path) ;
            fileStream.Write(image, 0, image.Length);
            fileStream.Close();

            return $"https://{url}/bookcoverimages/{fileName}";

        }

        private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
