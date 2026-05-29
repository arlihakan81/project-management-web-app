namespace PMWA.Domain.Entities
{
    public class ProjectMember
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime JoinedAt { get; set; }

        public virtual User User { get; set; }
        public virtual Project Project { get; set; }



    }
}
