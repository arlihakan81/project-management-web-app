using PMWA.Domain.Entities.Commons;

namespace PMWA.Domain.Entities
{
    public class TaskAttachment : BaseEntity
    {
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;

        public Guid TaskId { get; set; }
        public Guid UserId { get; set; }


        public virtual TaskItem Task { get; set; }
        public virtual User User { get; set; }

    }
}
