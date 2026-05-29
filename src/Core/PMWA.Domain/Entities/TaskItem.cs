using PMWA.Domain.Entities.Commons;

namespace PMWA.Domain.Entities
{
    public class TaskItem : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? CoverImage { get; set; }
        public string? Description { get; set; }
        public Enums.TaskType Type { get; set; }
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime? DueDate { get; set; }
        public int Position { get; set; } = 0;
        public Enums.TaskStatus Status { get; set; }
        public Enums.Category? Category { get; set; }
        public Enums.Priority Priority { get; set; }        
        public Guid AssigneeId { get; set; }

        public Guid ColumnId { get; set; }
        public virtual Column Column { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<TaskAttachment>? Attachments { get; set; }

    }
}
