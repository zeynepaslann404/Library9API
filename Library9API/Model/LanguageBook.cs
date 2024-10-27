using Library9API.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Library9API.Models
{
    public class LanguageBook
    {
        public string LanguagesCode { get; set; } = "";

        public int BooksId { get; set; }

        [ForeignKey(nameof(LanguagesCode))]
        public Language? Language { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(BooksId))]
        public Book? Book { get; set; }


    }
}
