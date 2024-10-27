using Library9API.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library9API.Models
{
    public class Publisher
    {
        public int Id { get; set; }

        [Required]
        [StringLength(800)]
        public string Name { get; set; } = "";

        [Phone]
        [StringLength(15, MinimumLength = 7)]
        [Column(TypeName = "varchar(15)")]
        public string? Phone { get; set; }

        [EmailAddress]
        [StringLength(320, MinimumLength = 3)]
        [Column(TypeName = "varchar(320)")]
        public string? EMail { get; set; }

        [StringLength(800)]
        public string? ContactPerson { get; set; }

        public List<Book>? Books { get; set; }
    }
}