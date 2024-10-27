using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library9API.Data;
using Library9API.Models;
using Microsoft.AspNetCore.Authorization;
namespace Library9API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookCopiesController : ControllerBase
    {
        private readonly Library9APIContext _context;

        public BookCopiesController(Library9APIContext context)
        {
            _context = context;
        }

        // GET: api/BookCopies
        [HttpGet]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult<IEnumerable<BookCopy>>> GetBookCopies()
        {
          if (_context.BookCopies == null)
          {
              return NotFound();
          }
            return await _context.BookCopies.ToListAsync();
        }

        // GET: api/BookCopies/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult<BookCopy>> GetBookCopy(int id)
        {
          if (_context.BookCopies == null)
          {
              return NotFound();
          }
            var bookCopy = await _context.BookCopies.FindAsync(id);

            if (bookCopy == null)
            {
                return NotFound();
            }

            return bookCopy;
        }

        // PUT: api/BookCopies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> PutBookCopy(int id, BookCopy bookCopy)
        {
            if (id != bookCopy.Id)
            {
                return BadRequest();
            }

            _context.Entry(bookCopy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookCopyExists(id))
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

        // POST: api/BookCopies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookCopy>> PostBookCopy(BookCopy bookCopy)
        {
          if (_context.BookCopies == null)
          {
              return Problem("Entity set 'Library9APIContext.BookCopy'  is null.");
          }
            _context.BookCopies.Add(bookCopy);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookCopy", new { id = bookCopy.Id }, bookCopy);
        }

        // DELETE: api/BookCopies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookCopy(int id)
        {
            if (_context.BookCopies == null)
            {
                return NotFound();
            }
            var bookCopy = await _context.BookCopies.FindAsync(id);
            if (bookCopy == null)
            {
                return NotFound();
            }

            _context.BookCopies.Remove(bookCopy);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookCopyExists(int id)
        {
            return (_context.BookCopies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
