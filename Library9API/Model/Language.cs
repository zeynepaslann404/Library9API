using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Library9API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Library9API.Models
{
    public class Language
    {
        [Key]
        [Required]
        [StringLength(3, MinimumLength = 3)]
        [Column(TypeName = "char(3)")]
        public string Code { get; set; } = "";
        public List<LanguageBook>? LanguageBooks { get; set; }

    }
}
