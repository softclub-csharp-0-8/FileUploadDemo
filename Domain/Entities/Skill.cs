namespace Domain.Entities;

public class Skill
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<EmployeeSkill> EmployeeSkills { get; set; }
}