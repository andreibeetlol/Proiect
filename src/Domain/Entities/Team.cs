namespace Domain.Entities;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public ICollection<TeamMember> Members { get; set; } = new List<TeamMember>();
    public ICollection<Task> Tasks { get; set; } = new List<Task>();
}
