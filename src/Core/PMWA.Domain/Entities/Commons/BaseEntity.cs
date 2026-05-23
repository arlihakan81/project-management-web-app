namespace PMWA.Domain.Entities.Commons
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ModifiedAt { get; set; }

        public Guid CreatedById { get; set; }
        public Guid? ModifiedById { get; set; }
        public Guid OrganizationId { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual User? ModifiedBy { get; set; }
        public virtual Organization Organization { get; set; }

    }
}
