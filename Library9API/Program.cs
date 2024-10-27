using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Library9API.Data;
using Library9API.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Library9API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Library9APIContext _context;
            RoleManager<IdentityRole> _roleManager;
            UserManager<ApplicationUser> _userManager;
            IdentityRole identityRole;
            ApplicationUser applicationUser;

            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.

            builder.Services.AddDbContext<Library9APIContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Library9APIContext")));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<Library9APIContext>().AddDefaultTokenProviders();
            

            //JWT Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }) //JWT Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
               };

            });

            builder.Services.AddAuthorization();  
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                  new OpenApiSecurityScheme
                  {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                  },
                    new string[] { }
                }
            });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            IServiceProvider serviceProvider = app.Services.CreateScope().ServiceProvider;
            _context = serviceProvider.GetRequiredService<Library9APIContext>();
            _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

          
            _context.Database.Migrate();
          
            if (_roleManager.FindByNameAsync("Admin").Result == null)
            {
                identityRole = new IdentityRole("Admin");
                _roleManager.CreateAsync(identityRole).Wait();
            }
            if (_userManager.FindByNameAsync("Admin").Result == null)
            {
                applicationUser = new ApplicationUser();
                applicationUser.UserName = "Admin";
                _userManager.CreateAsync(applicationUser, "Admin123!").Wait();
                _userManager.AddToRoleAsync(applicationUser, "Admin").Wait();
            }
          
            app.Run();
        }
    }
}

