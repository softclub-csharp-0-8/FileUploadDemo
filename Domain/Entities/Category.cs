using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Category
{

    public Category()
    {
        Quotes = new List<Quote>();
    }
    public int Id { get; set; }
    [Required,MaxLength(50)]
    public string Name { get; set; }

    public List<Quote> Quotes { get; set; }
}