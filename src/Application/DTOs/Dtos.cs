using Domain.Enums;
using TaskStatusEnum = Domain.Enums.TaskStatus;

namespace Application.DTOs;

public class RegisterDto
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public int UserId { get; set; }
}

public class CreateTaskDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public TaskPriority Priority { get; set; }
    public int? CategoryId { get; set; }
    public int? TeamId { get; set; }
    public int? AssignedUserId { get; set; }
    public List<int> TagIds { get; set; } = new List<int>();
}

public class UpdateTaskDto : CreateTaskDto
{
    public TaskStatusEnum Status { get; set; }
}

public class TaskDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public TaskPriority Priority { get; set; }
    public TaskStatusEnum Status { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string TeamName { get; set; } = string.Empty;
    public string AssignedUserName { get; set; } = string.Empty;
    public List<string> Tags { get; set; } = new List<string>();
}

public class CreateTeamDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class TeamDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
