using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Quote
{
    [Key]
    public int Id { get; set; } // serial primary key
    [Required,MaxLength(50)]
    public string Author { get; set; } // varchar(50) not null
    [Required]
    public string QuoteText { get; set; } // text not null

    public DateTime CreatedAt { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public Quote()
    {
        CreatedAt = DateTime.Now;
    }
}

