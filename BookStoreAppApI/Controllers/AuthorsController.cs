using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreAppApI.Data;
using BookStoreAppApI.Models.Author;
using AutoMapper;
using System.Collections;
using BookStoreAppApI.Static;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreAppApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
    public class AuthorsController : ControllerBase
    {
        private readonly BookStoreAppDboContext _context;
        private readonly IMapper mapper;
        private readonly ILogger<AuthorsController> logger;

        public AuthorsController(BookStoreAppDboContext context, IMapper mapper, ILogger<AuthorsController>logger)
        {
            _context = context;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorReadOnyDto>>> GetAuthors()
        {
            try
            {
                var authors = await _context.Authors.ToListAsync();


                // Trim spaces from the first name, last name, and bio fields for each author
                foreach (var author in authors)
                {
                    author.FirstName = author.FirstName?.Trim();
                    author.LastName = author.LastName?.Trim();
                    author.Bio = author.Bio?.Trim();
                }
                var authorsDtos = mapper.Map<IEnumerable<AuthorReadOnyDto>>(authors);


                ////return await _context.Authors.ToListAsync();
                return Ok(authorsDtos);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error Perfoming GET in {nameof(GetAuthors)}");
                return StatusCode(500, Message.Error500Message);

            }
   

        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorReadOnyDto>> GetAuthor(int id)
        {
            try
            {
                if (_context.Authors == null)
                {
                    return NotFound();
                }
                var author = await _context.Authors.FindAsync(id);

                if (author == null)
                {
                    return NotFound();
                }

                // Trim spaces from the first name, last name, and bio fields
                author.FirstName = author.FirstName?.Trim();
                author.LastName = author.LastName?.Trim();
                author.Bio = author.Bio?.Trim();

                var authorDtos = mapper.Map<AuthorReadOnyDto>(author);


                return Ok(authorDtos);

            }
            catch(Exception ex)
            {
                logger.LogError(ex, $"Error Perfoming GET in {nameof(GetAuthors)}");
                return StatusCode(500, Message.Error500Message);

            }

          
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, AuthorUpdateDto authorDto)
        {
            if (id != authorDto.Id)
            {
                logger.LogWarning($"Update ID invalid in  {nameof(PutAuthor)} - ID: {id}");
           
                return BadRequest();
            }

            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                logger.LogWarning($"{nameof(PutAuthor)} record not found in {nameof(PutAuthor)} - ID: {id}");
                return NotFound();
            }
            mapper.Map(authorDto, author);
            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!AuthorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    logger.LogError(ex, $"Error Perfoming GET in {nameof(PutAuthor)}");
                    return StatusCode(500, Message.Error500Message);

                }
            }

            return NoContent();
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //public async Task<ActionResult<Author>> PostAuthor(Author author)
        //{
        //  if (_context.Authors == null)
        //  {
        //      return Problem("Entity set 'BookStoreAppDboContext.Authors'  is null.");
        //  }

        //    // Trim the input data before saving
        //    author.FirstName = author.FirstName?.Trim();
        //    author.LastName = author.LastName?.Trim();
        //    author.Bio = author.Bio?.Trim();

        //    _context.Authors.Add(author);
        //    await _context.SaveChangesAsync();


        //    return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
        //}
        public async Task<ActionResult<AuthorCreateDto>> PostAuthor(AuthorCreateDto authorDto)
        {
            try
            {
                var author = mapper.Map<Author>(authorDto);
                //var author = new Author
                //{
                //    Bio = authorDto.Bio,
                //    FirstName = authorDto.FirstName,
                //    LastName = authorDto.LastName
                //};
                if (_context.Authors == null)
                {
                    return Problem("Entity set 'BookStoreAppDboContext.Authors'  is null.");
                }

                // Trim the input data before saving
                author.FirstName = author.FirstName?.Trim();
                author.LastName = author.LastName?.Trim();
                author.Bio = author.Bio?.Trim();

                await _context.Authors.AddAsync(author);
                await _context.SaveChangesAsync();


                return CreatedAtAction("GetAuthor", new { id = author.Id }, author);

            }
            catch(Exception ex)
            {
                logger.LogError(ex, $"Error Perfoming POST in {nameof(PostAuthor)}",authorDto);
                return StatusCode(500, Message.Error500Message);

            }
            
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                if (_context.Authors == null)
                {
                    return NotFound();
                }
                var author = await _context.Authors.FindAsync(id);
                if (author == null)
                {
                    return NotFound();
                }

                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();

                return NoContent();

            }
            catch(Exception ex)
            {
                logger.LogError(ex, $"Error Perfoming DELETE in {nameof(PostAuthor)}");
                return StatusCode(500, Message.Error500Message);

            }
           
        }

        private bool AuthorExists(int id)
        {
            return (_context.Authors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
