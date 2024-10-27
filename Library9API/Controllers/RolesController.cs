using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Library9API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost]
        public void CreateRoles()
        {
            IdentityRole identityRole = new IdentityRole("Member"); 
            _roleManager.CreateAsync(identityRole).Wait();

            identityRole = new IdentityRole("Employee"); 
            _roleManager.CreateAsync(identityRole).Wait();
        }
    }
}

