using Application.DTOs;

namespace Application.Interfaces;

public interface ITeamService
{
    Task<IEnumerable<TeamDto>> GetAllAsync();
    Task<TeamDto> CreateAsync(CreateTeamDto request, int ownerId);
}
