using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Team> Teams { get; }
    DbSet<TeamMember> TeamMembers { get; }
    DbSet<Category> Categories { get; }
    DbSet<Domain.Entities.Task> Tasks { get; }
    DbSet<Tag> Tags { get; }
    DbSet<TaskTag> TaskTags { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
