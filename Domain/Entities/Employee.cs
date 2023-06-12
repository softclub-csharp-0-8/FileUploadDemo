using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Employee
{
    public int Id { get; set; }
    [Required,MaxLength(50)]
    public string FullName { get; set; }

    public int CompanyId { get; set; }
    public virtual Company Company { get; set; }
    public virtual ICollection<EmployeeSkill> EmployeeSkills { get; set; }
}



