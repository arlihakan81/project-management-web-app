using PMWA.Domain.Entities.Commons;

namespace PMWA.Domain.Entities
{
    public class Column : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Color { get; set; }
        public int Position { get; set; }
        public Guid BoardId { get; set; }
        public virtual Board Board { get; set; }

        public virtual ICollection<TaskItem>? Tasks { get; set; }
    }
}