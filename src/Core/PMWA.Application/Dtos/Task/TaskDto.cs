using PMWA.Application.Dtos.Column;
using PMWA.Application.Dtos.User;
using System;

namespace PMWA.Application.Dtos.Task
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string? CoverImage { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Domain.Enums.TaskType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int Position { get; set; }
        public Domain.Enums.TaskStatus Status { get; set; }     
        public Domain.Enums.Category? Category { get; set; }
        public Domain.Enums.Priority Priority { get; set; }
        public virtual UserDto? Assignee { get; set; }
        public virtual ColumnDto Column { get; set; } = new ColumnDto();
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        




    }
}
