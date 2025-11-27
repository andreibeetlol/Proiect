using Domain.Enums;

namespace Domain.Entities;

public class TeamMember
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public int TeamId { get; set; }
    public Team Team { get; set; } = null!;

    public TeamRole Role { get; set; } = TeamRole.Member;
}
