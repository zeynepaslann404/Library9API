using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Library9API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Library9API.Data
{
    public class Library9APIContext : IdentityDbContext<ApplicationUser>
    {
        public Library9APIContext(DbContextOptions<Library9APIContext> options) : base(options)
        {
        }

        public DbSet<Location>? Locations { get; set; }
        public DbSet<Author>? Authors { get; set; }
        public DbSet<AuthorBook>? AuthorBooks { get; set; }
        public DbSet<Book>? Books { get; set; }
        public DbSet<LanguageBook>? LanguageBook { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Language>? Languages { get; set; }
        public DbSet<Publisher>? Publishers { get; set; }
        public DbSet<SubCategory>? SubCategories { get; set; }
        public DbSet<SubCategoryBook>? SubCategoryBooks { get; set; }
        public DbSet<Member>? Members { get; set; }
        public DbSet<Employee>? Employees { get; set; }
        public DbSet<BookCopy>? BookCopies { get; set; }
        public DbSet<Loan>? Loans { get; set; }
        public DbSet<Rating>? Ratings { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AuthorBook>().HasKey(a => new { a.AuthorsId, a.BooksId });

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LanguageBook>().HasKey(b => new { b.LanguagesCode, b.BooksId });
          
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SubCategoryBook>().HasKey(c => new { c.SubCategoriesId, c.BooksId });
        }
  

 

     

    }
}