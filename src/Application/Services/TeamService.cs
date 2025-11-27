using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using TaskStatus = Domain.Enums.TaskStatus;

namespace Application.Services;

public class TeamService : ITeamService
{
    private readonly IApplicationDbContext _context;

    public TeamService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TeamDto>> GetAllAsync()
    {
        return await _context.Teams
            .Select(t => new TeamDto
            {
                Id = t.Id,
                Name = t.Name,
                Description = t.Description
            })
            .ToListAsync();
    }

    public async Task<TeamDto> CreateAsync(CreateTeamDto request, int ownerId)
    {
        var team = new Team
        {
            Name = request.Name,
            Description = request.Description
        };

        _context.Teams.Add(team);
        await _context.SaveChangesAsync();

        // Add creator as Admin
        _context.TeamMembers.Add(new TeamMember
        {
            TeamId = team.Id,
            UserId = ownerId,
            Role = TeamRole.Admin
        });
        await _context.SaveChangesAsync();

        return new TeamDto { Id = team.Id, Name = team.Name, Description = team.Description };
    }
}
