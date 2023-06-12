using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Company
{
    public int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; }

    public virtual ICollection<Employee> Employees { get; set; }
}