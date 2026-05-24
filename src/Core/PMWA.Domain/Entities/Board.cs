using PMWA.Domain.Entities.Commons;

namespace PMWA.Domain.Entities
{
    public class Board : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? CoverImage { get; set; }
        public string? Color { get; set; }
        public Guid ProjectId { get; set; }
        public bool IsArchived { get; set; }
        public virtual Project Project { get; set; } = null!;
        public ICollection<Column>? Columns { get; set; }
    }
}
