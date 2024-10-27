using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library9API.Data;
using Library9API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Library9API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly Library9APIContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoansController(Library9APIContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Loans
        [HttpGet]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult<IEnumerable<Loan>>> GetLoans()
        {
          if (_context.Loans == null)
          {
              return NotFound();
          }
            return await _context.Loans.ToListAsync();
        }

        // GET: api/Loans/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult<Loan>> GetLoan(int id)
        {
          if (_context.Loans == null)
          {
              return NotFound();
          }
            var loan = await _context.Loans.FindAsync(id);

            if (loan == null)
            {
                return NotFound();
            }

            return loan;
        }

        // PUT: api/Loans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> PutLoan(int id, Loan loan)
        {
            var selectedBookCopy = await _context.Loans!.FirstOrDefaultAsync(b => b.BookCopiesId == id && b.IsDelivered == false);
            if (selectedBookCopy==null)
            {
                return BadRequest();
            }
            selectedBookCopy.ReturnTime = loan.ReturnTime;

            selectedBookCopy.IsDelivered = true;

            selectedBookCopy.EmployeesId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _context.Entry(selectedBookCopy).State = EntityState.Modified;

            var bookCopy = await _context.BookCopies!.FindAsync(id);

            bookCopy!.IsAvailable = true;

            _context.BookCopies.Update(bookCopy);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoanExists(selectedBookCopy.Id))
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

        // POST: api/Loans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult<Loan>> PostLoan(Loan loan, string userName)
        {
          if (_context.Loans == null)
          {
              return Problem("Entity set 'Library9APIContext.Loan'  is null.");
          }
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return NotFound("User not found");
            }

            loan.MembersId = user.Id;

            loan.EmployeesId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var bookCopy = await _context.BookCopies!.FindAsync(loan.BookCopiesId);

            if (bookCopy == null)
            {
                return NotFound();
            }
            if (!bookCopy.IsAvailable) 
            {
                return BadRequest();
            }
            bookCopy.IsAvailable = false;
            _context.BookCopies.Update(bookCopy);
            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetLoan", new { id = loan.Id }, loan);
        }

        // DELETE: api/Loans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoan(int id)
        {
            if (_context.Loans == null)
            {
                return NotFound();
            }
            var loan = await _context.Loans.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }

            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoanExists(int id)
        {
            return (_context.Loans?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
