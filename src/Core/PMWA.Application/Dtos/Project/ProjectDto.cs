using PMWA.Application.Dtos.User;
using PMWA.Domain.Enums;

namespace PMWA.Application.Dtos.Project
{
    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal? Budget { get; set; }
        public bool IsArchived { get; set; }
        public ProjectStatus Status { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }

        public virtual UserDto Owner { get; set; }

        public virtual ICollection<UserDto>? Assignees { get; set; }

    }
}
