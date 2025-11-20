using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using TaskStatus = Domain.Enums.TaskStatus;

namespace Application.Services;

public class TaskService : ITaskService
{
    private readonly IApplicationDbContext _context;

    public TaskService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskDto>> GetAllAsync()
    {
        return await _context.Tasks
            .Include(t => t.Category)
            .Include(t => t.Team)
            .Include(t => t.AssignedUser)
            .Include(t => t.TaskTags).ThenInclude(tt => tt.Tag)
            .Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                DueDate = t.DueDate,
                Priority = t.Priority,
                Status = t.Status,
                CategoryName = t.Category != null ? t.Category.Name : string.Empty,
                TeamName = t.Team != null ? t.Team.Name : string.Empty,
                AssignedUserName = t.AssignedUser != null ? t.AssignedUser.UserName : string.Empty,
                Tags = t.TaskTags.Select(tt => tt.Tag.Name).ToList()
            })
            .ToListAsync();
    }

    public async Task<TaskDto?> GetByIdAsync(int id)
    {
        var t = await _context.Tasks
            .Include(t => t.Category)
            .Include(t => t.Team)
            .Include(t => t.AssignedUser)
            .Include(t => t.TaskTags).ThenInclude(tt => tt.Tag)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (t == null) return null;

        return new TaskDto
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            DueDate = t.DueDate,
            Priority = t.Priority,
            Status = t.Status,
            CategoryName = t.Category != null ? t.Category.Name : string.Empty,
            TeamName = t.Team != null ? t.Team.Name : string.Empty,
            AssignedUserName = t.AssignedUser != null ? t.AssignedUser.UserName : string.Empty,
            Tags = t.TaskTags.Select(tt => tt.Tag.Name).ToList()
        };
    }

    public async Task<TaskDto> CreateAsync(CreateTaskDto request, int userId)
    {
        var task = new Domain.Entities.Task
        {
            Title = request.Title,
            Description = request.Description,
            DueDate = request.DueDate,
            Priority = request.Priority,
            Status = TaskStatus.ToDo,
            CategoryId = request.CategoryId,
            TeamId = request.TeamId,
            AssignedUserId = request.AssignedUserId ?? userId
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        if (request.TagIds.Any())
        {
            foreach (var tagId in request.TagIds)
            {
                _context.TaskTags.Add(new TaskTag { TaskId = task.Id, TagId = tagId });
            }
            await _context.SaveChangesAsync();
        }

        return await GetByIdAsync(task.Id);
    }

    public async Task<bool> UpdateAsync(int id, UpdateTaskDto request)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return false;

        task.Title = request.Title;
        task.Description = request.Description;
        task.DueDate = request.DueDate;
        task.Priority = request.Priority;
        task.Status = request.Status;
        task.CategoryId = request.CategoryId;
        task.TeamId = request.TeamId;
        task.AssignedUserId = request.AssignedUserId;

        // Update tags
        var currentTags = _context.TaskTags.Where(tt => tt.TaskId == id);
        _context.TaskTags.RemoveRange(currentTags);

        if (request.TagIds.Any())
        {
             foreach (var tagId in request.TagIds)
            {
                _context.TaskTags.Add(new TaskTag { TaskId = task.Id, TagId = tagId });
            }
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return false;

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }
}
