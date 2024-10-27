using Library9API.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Library9API.Models
{
    public class Book
    {
        public int Id { get; set; }

        [StringLength(13, MinimumLength = 10)] //ısbn max 13 karakter olabilir ve çince falan olamaz. Bu yüzden nvarchar değil,
        [Column(TypeName = "varchar(13)")] //nvarchar ı varchar(13) yaptık.  varchar yaptık . 
        public string? ISBN { get; set; }

        [Required] 
        [StringLength(2000)]
        public string Title { get; set; } = "";

        [Range(1, short.MaxValue)]
        public short PageCount { get; set; }

        [Range(-4000, 2100)]
        public short PublishingYear { get; set; }

        [StringLength(5000)]
        public string? Description { get; set; }

        [Range(0, int.MaxValue)]
        public int PrintCount { get; set; }

        public bool Banned { get; set; }

        [JsonIgnore]  
        public float Rating { get; set; }

        public int PublisherId { get; set; }

        [NotMapped]
        public List<long>? AuthorIds { get; set; }

        [NotMapped]
        public List<string>? LanguageCodes { get; set; }

        [NotMapped]
        public List<short>? SubCategoryIds { get; set; }


        [StringLength(6, MinimumLength = 3)]
        [Column(TypeName = "varchar(6)")]
        public string LocationShelf { get; set; } = "";

        
        public List<AuthorBook>? AuthorBook { get; set; }//ara tablo 
        [ForeignKey(nameof(PublisherId))] //one to many ilişki 
        public Publisher? Publisher { get; set; }

        [JsonIgnore]
        public List<SubCategoryBook>? SubCategoryBooks { get; set; }//many to many 
      
        public List<LanguageBook>? LanguageBooks { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(LocationShelf))]
        public Location? Location { get; set; }

        public string? UserId;


    }
}

