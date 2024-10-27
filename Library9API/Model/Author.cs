using Library9API.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
public class Author
{
    public long Id { get; set; }

    [Required]
    [StringLength(800)]
    public string FullName { get; set; } = "";
    [StringLength(1000)]
    [Column(TypeName = "nvarchar(1000)")]
    public string? Biography { get; set; }

    [Range(-4000, 2100)]
    public short BirthYear { get; set; }

    [Range(-4000, 2100)]
    public short? DeathYear { get; set; }
    
    public string? UserId;
    [JsonIgnore]
    public List<AuthorBook>? AuthorBooks{ get; set; }
}