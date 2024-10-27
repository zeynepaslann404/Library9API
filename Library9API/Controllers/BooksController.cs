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
using System.Security.Claims;

namespace Library9API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly Library9APIContext _context;

        public BooksController(Library9APIContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            if (_context.Books == null)
            {
                return NotFound();
            }
            return await _context.Books.ToListAsync();
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
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

            return book;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

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
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            AuthorBook authorBook;
            LanguageBook languageBook;
            SubCategoryBook subCategoryBook;


            if (_context.Books == null)
            {
                return Problem("Entity set 'ApplicationContext.Books'  is null.");
            }
            book.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            if (book.AuthorIds != null)
            {
                foreach (long authorId in book.AuthorIds)
                {
                    authorBook = new AuthorBook();
                    authorBook.AuthorsId = authorId;
                    authorBook.BooksId = book.Id;
                    _context.AuthorBooks!.Add(authorBook);

                }

                _context.SaveChanges();
            }

            if (book.LanguageCodes != null)
            {
                foreach (string languageCode in book.LanguageCodes)
                {
                    languageBook = new LanguageBook();
                    languageBook.LanguagesCode = languageCode;
                    languageBook.BooksId = book.Id;
                    _context.LanguageBook!.Add(languageBook);
                }

                _context.SaveChanges();
            }

            if (book.SubCategoryIds != null)
            {
                foreach (short subCategoryId in book.SubCategoryIds)
                {
                    subCategoryBook = new SubCategoryBook();
                    subCategoryBook.SubCategoriesId = subCategoryId;
                    subCategoryBook.BooksId = book.Id;
                    _context.SubCategoryBooks!.Add(subCategoryBook);
                }

                _context.SaveChanges();

            }

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
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
