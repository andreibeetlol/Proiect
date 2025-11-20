using Domain.Enums;
using TaskStatusEnum = Domain.Enums.TaskStatus;

namespace Domain.Entities;

public class Task
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime? DueDate { get; set; }
    public TaskPriority Priority { get; set; }
    public TaskStatusEnum Status { get; set; }

    public int? CategoryId { get; set; }
    public Category? Category { get; set; }

    public int? TeamId { get; set; }
    public Team? Team { get; set; }

    public int? AssignedUserId { get; set; }
    public User? AssignedUser { get; set; }

    public ICollection<TaskTag> TaskTags { get; set; } = new List<TaskTag>();
}
