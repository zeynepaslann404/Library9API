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
using Microsoft.IdentityModel.Tokens;
using NuGet.Packaging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Library9API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly Library9APIContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
       
        public MembersController(Library9APIContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
           
        }

        // GET: api/Members
        [HttpGet]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            var member = await _context.Members!.Include(m => m.ApplicationUser).ToListAsync();
            if (_context.Members == null)
          {
              return NotFound();
          }
            return await _context.Members.ToListAsync();
        }

        // GET: api/Members/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Employee")]
        public async Task<ActionResult<Member>> GetMember(string id)
        {
          if (_context.Members == null)
          {
              return NotFound();
          }
            var member = await _context.Members.FindAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            return member;
        }

        // PUT: api/Members/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> PutMember(string id, Member member,string? currentPassword = null)
        {
            ApplicationUser applicationUser = _userManager.FindByIdAsync(id).Result;
            if (id != member.Id)
            {
                return BadRequest();
            }
            applicationUser.IdNumber = member.ApplicationUser!.IdNumber;
            applicationUser.Name = member.ApplicationUser!.Name;
            applicationUser.MiddleName = member.ApplicationUser!.MiddleName;
            applicationUser.FamilyName = member.ApplicationUser!.FamilyName;
            applicationUser.Address = member.ApplicationUser!.Address;
            applicationUser.Gender = member.ApplicationUser!.Gender;
            applicationUser.BirthDate = member.ApplicationUser!.BirthDate;
            applicationUser.RegisterDate = member.ApplicationUser!.RegisterDate;
            applicationUser.Status = member.ApplicationUser!.Status;
            applicationUser.Email = member.ApplicationUser!.Email;

            _userManager.UpdateAsync(applicationUser).Wait();

            if (currentPassword != null)
            {
                _userManager.ChangePasswordAsync(applicationUser, currentPassword, applicationUser.Password).Wait();
            }
            member.ApplicationUser = null;
            _context.Entry(member).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
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

        // POST: api/Members
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles ="Admin,Employee")]  
        public async Task<ActionResult<Member>> PostMember(Member member)
        {
          if (_context.Members == null)
          {
              return Problem("Entity set 'Library9APIContext.Member'  is null.");
          }
            _userManager.CreateAsync(member.ApplicationUser!, member.ApplicationUser!.Password).Wait();
            _userManager.AddToRoleAsync(member.ApplicationUser, "Member").Wait();
            member.Id = member.ApplicationUser!.Id;
            member.ApplicationUser = null;
            _context.Members.Add(member);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MemberExists(member.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMember", new { id = member.Id }, member);
        }

        // DELETE: api/Members/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Member")]
        public async Task<IActionResult> DeleteMember(string id)
        {
            if (_context.Members == null)
            {
                return NotFound();
            }
            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();

            return NoContent();
        }
      

        private bool MemberExists(string id)
        {
            return (_context.Members?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
