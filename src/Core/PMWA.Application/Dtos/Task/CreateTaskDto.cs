namespace PMWA.Application.Dtos.Task
{
    public class CreateTaskDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Domain.Enums.TaskType Type { get; set; }
        public DateTime? DueDate { get; set; }
        public int Position { get; set; }
        public Domain.Enums.TaskStatus Status { get; set; }
        public Guid ColumnId { get; set; }
        public Guid? AssigneeId { get; set; }
    }

    public class UpdateTaskDto : CreateTaskDto { }
}
