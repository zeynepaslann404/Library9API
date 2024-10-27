using Library9API.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Library9API.Models
{
    public class BookCopy
    {
        public int Id { get; set; }

        public int BooksId { get; set; }

        public bool IsAvailable { get; set; }

        [ForeignKey(nameof(BooksId))]
        public Book? Book { get; set; }






    }
}
