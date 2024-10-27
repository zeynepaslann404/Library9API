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
    public class SubCategoryBooksController : ControllerBase
    {
        private readonly Library9APIContext _context;

        public SubCategoryBooksController(Library9APIContext context)
        {
            _context = context;
        }

        // GET: api/SubCategoryBooks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategoryBook>>> GetSubCategoryBooks()
        {
          if (_context.SubCategoryBooks == null)
          {
              return NotFound();
          }
            return await _context.SubCategoryBooks.ToListAsync();
        }

        // GET: api/SubCategoryBooks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubCategoryBook>> GetSubCategoryBook(short id)
        {
          if (_context.SubCategoryBooks == null)
          {
              return NotFound();
          }
            var subCategoryBook = await _context.SubCategoryBooks.FindAsync(id);

            if (subCategoryBook == null)
            {
                return NotFound();
            }

            return subCategoryBook;
        }

        // PUT: api/SubCategoryBooks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubCategoryBook(short id, SubCategoryBook subCategoryBook)
        {
            if (id != subCategoryBook.SubCategoriesId)
            {
                return BadRequest();
            }

            _context.Entry(subCategoryBook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubCategoryBookExists(id))
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

        // POST: api/SubCategoryBooks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubCategoryBook>> PostSubCategoryBook(SubCategoryBook subCategoryBook)
        {
          if (_context.SubCategoryBooks == null)
          {
              return Problem("Entity set 'Library9APIContext.SubCategoryBook'  is null.");
          }
            _context.SubCategoryBooks.Add(subCategoryBook);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SubCategoryBookExists(subCategoryBook.SubCategoriesId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSubCategoryBook", new { id = subCategoryBook.SubCategoriesId }, subCategoryBook);
        }

        // DELETE: api/SubCategoryBooks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategoryBook(short id)
        {
            if (_context.SubCategoryBooks== null)
            {
                return NotFound();
            }
            var subCategoryBook = await _context.SubCategoryBooks.FindAsync(id);
            if (subCategoryBook == null)
            {
                return NotFound();
            }

            _context.SubCategoryBooks.Remove(subCategoryBook);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubCategoryBookExists(short id)
        {
            return (_context.SubCategoryBooks?.Any(e => e.SubCategoriesId == id)).GetValueOrDefault();
        }
    }
}
