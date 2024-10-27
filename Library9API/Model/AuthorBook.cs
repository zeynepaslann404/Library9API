using Library9API.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Library9API.Models
{
    public class AuthorBook
    {
        public long AuthorsId { get; set; }

        public int BooksId { get; set; }

        [ForeignKey(nameof(AuthorsId))] // foreign key (AuthorsId) Author modelindeki primary key(Id) ile eşleşir. 
        public Author? Author { get; set; } 

        [JsonIgnore]
        [ForeignKey(nameof(BooksId))] 
        public Book? Book { get; set; }//Book adında Book o-t-o.
    }
}
