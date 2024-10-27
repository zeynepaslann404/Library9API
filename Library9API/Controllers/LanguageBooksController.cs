using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library9API.Data;
using Library9API.Models;

namespace Library9API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageBooksController : ControllerBase
    {
        private readonly Library9APIContext _context;

        public LanguageBooksController(Library9APIContext context)
        {
            _context = context;
        }

        // GET: api/LanguageBooks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LanguageBook>>> GetLanguageBooks()
        {
          if (_context.LanguageBook == null)
          {
              return NotFound();
          }
            return await _context.LanguageBook.ToListAsync();
        }

        // GET: api/LanguageBooks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LanguageBook>> GetLanguageBook(string id)
        {
          if (_context.LanguageBook == null)
          {
              return NotFound();
          }
            var languageBook = await _context.LanguageBook.FindAsync(id);

            if (languageBook == null)
            {
                return NotFound();
            }

            return languageBook;
        }

        // PUT: api/LanguageBooks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLanguageBook(string id, LanguageBook languageBook)
        {
            if (id != languageBook.LanguagesCode)
            {
                return BadRequest();
            }

            _context.Entry(languageBook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LanguageBookExists(id))
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

        // POST: api/LanguageBooks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LanguageBook>> PostLanguageBook(LanguageBook languageBook)
        {
          if (_context.LanguageBook == null)
          {
              return Problem("Entity set 'Library9APIContext.LanguageBook'  is null.");
          }
            _context.LanguageBook.Add(languageBook);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LanguageBookExists(languageBook.LanguagesCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLanguageBook", new { id = languageBook.LanguagesCode }, languageBook);
        }

        // DELETE: api/LanguageBooks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLanguageBook(string id)
        {
            if (_context.LanguageBook == null)
            {
                return NotFound();
            }
            var languageBook = await _context.LanguageBook.FindAsync(id);
            if (languageBook == null)
            {
                return NotFound();
            }

            _context.LanguageBook.Remove(languageBook);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LanguageBookExists(string id)
        {
            return (_context.LanguageBook?.Any(e => e.LanguagesCode == id)).GetValueOrDefault();
        }
    }
}
