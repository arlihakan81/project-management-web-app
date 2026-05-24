using PMWA.Domain.Entities.Commons;
using PMWA.Domain.Enums;

namespace PMWA.Domain.Entities
{
    public class Project : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal? Budget { get; set; }
        public bool IsArchived { get; set; } = false;
        public ProjectStatus Status { get; set; }
        public DateTime StartAt { get; set; } = DateTime.Now;
        public DateTime? Deadline { get; set; }
        public Guid OwnerId { get; set; }

        // Navigation properties
        public virtual User Owner { get; set; }
        public virtual ICollection<Board>? Boards { get; set; }





    }
}
