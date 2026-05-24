namespace PMWA.Application.Dtos.Task
{
    public class CreateTaskDto
    {
        public string Title { get; set; } = string.Empty;
        public string? CoverImage { get; set; }
        public string? Description { get; set; }
        public Domain.Enums.TaskType Type { get; set; }
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime? DueDate { get; set; }
        public int Position { get; set; }
        public Domain.Enums.TaskStatus Status { get; set; }
        public Domain.Enums.Category? Category { get; set; }
        public Domain.Enums.Priority Priority { get; set; }

        public Guid ColumnId { get; set; }
        public Guid? AssigneeId { get; set; }
    }

    public class UpdateTaskDto : CreateTaskDto { }
}
