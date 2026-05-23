using PMWA.Domain.Enums;

namespace PMWA.Application.Dtos.Project
{
    public class CreateProjectDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal? Budget { get; set; }
        public bool IsArchived { get; set; }
        public ProjectStatus Status { get; set; }
        public Guid OwnerId { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime? Deadline { get; set; }
    }

    public class UpdateProjectDto : CreateProjectDto { }

}
