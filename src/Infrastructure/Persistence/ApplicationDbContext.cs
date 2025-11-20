using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<int>, int>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamMember> TeamMembers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Domain.Entities.Task> Tasks { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<TaskTag> TaskTags { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Task Configuration
        builder.Entity<Domain.Entities.Task>()
            .HasOne(t => t.Category)
            .WithMany(c => c.Tasks)
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<Domain.Entities.Task>()
            .HasOne(t => t.Team)
            .WithMany(team => team.Tasks)
            .HasForeignKey(t => t.TeamId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Domain.Entities.Task>()
            .HasOne(t => t.AssignedUser)
            .WithMany(u => u.Tasks)
            .HasForeignKey(t => t.AssignedUserId)
            .OnDelete(DeleteBehavior.SetNull);

        // TeamMember Configuration (Many-to-Many with attributes)
        builder.Entity<TeamMember>()
            .HasKey(tm => new { tm.UserId, tm.TeamId });

        builder.Entity<TeamMember>()
            .HasOne(tm => tm.User)
            .WithMany(u => u.TeamMemberships)
            .HasForeignKey(tm => tm.UserId);

        builder.Entity<TeamMember>()
            .HasOne(tm => tm.Team)
            .WithMany(t => t.Members)
            .HasForeignKey(tm => tm.TeamId);

        // TaskTag Configuration (Many-to-Many)
        builder.Entity<TaskTag>()
            .HasKey(tt => new { tt.TaskId, tt.TagId });

        builder.Entity<TaskTag>()
            .HasOne(tt => tt.Task)
            .WithMany(t => t.TaskTags)
            .HasForeignKey(tt => tt.TaskId);

        builder.Entity<TaskTag>()
            .HasOne(tt => tt.Tag)
            .WithMany(t => t.TaskTags)
            .HasForeignKey(tt => tt.TagId);
    }
}
