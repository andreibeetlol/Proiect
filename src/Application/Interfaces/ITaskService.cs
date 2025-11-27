using Application.DTOs;

namespace Application.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskDto>> GetAllAsync();
    Task<TaskDto?> GetByIdAsync(int id);
    Task<TaskDto> CreateAsync(CreateTaskDto request, int userId);
    Task<bool> UpdateAsync(int id, UpdateTaskDto request);
    Task<bool> DeleteAsync(int id);
}
