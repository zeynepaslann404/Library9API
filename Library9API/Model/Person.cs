using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Library9API.Models
{
    public class ApplicationUser : IdentityUser //kullanıcıların ortak özellikleri identity tar. tutulur.
    {
        public long IdNumber { get; set; } // id number için bir koşul yok default değer 0 gelir . 
        public string Name { get; set; } = "";
        public string? MiddleName { get; set; }
        public string? FamilyName { get; set; }
        public string Address { get; set; } = "";
        public bool Gender { get; set; } 
        public DateTime BirthDate { get; set; }
        public DateTime RegisterDate { get; set; }
        public byte Status { get; set; }
        [NotMapped]
        [Required]
        public string Password { get; set; } = "";
        [NotMapped]
        [Required]
        [Compare(nameof(Password))] 
        public string ConfirmPassword { get; set; } = "";
    }
    public class Member
    {
        [Key] //Primary Key 
        public string Id { get; set; } = "";

        [ForeignKey(nameof(Id))]
        public ApplicationUser? ApplicationUser { get; set; }

        public byte EducationalDegree { get; set; }

    }
    public class Employee
    {
        [Key]
        public string Id { get; set; } = "";

        [ForeignKey(nameof(Id))]
        public ApplicationUser? ApplicationUser { get; set; }

        public string Title { get; set; } = "";
        public float Salary { get; set; }
        public string Department { get; set; } = "";
        public string? Shift { get; set; }
    }




}

