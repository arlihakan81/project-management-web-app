namespace PMWA.Application.Dtos.TaskAttachment
{
    public class TaskAttachmentDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

    }
}
