using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class User : IdentityUser<int>
{
    public ICollection<Task> Tasks { get; set; } = new List<Task>();
    public ICollection<TeamMember> TeamMemberships { get; set; } = new List<TeamMember>();
}
