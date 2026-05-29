namespace PMWA.Application.Dtos.Column
{
    public class CreateColumnDto
    {
        public string Name { get; set; } = string.Empty;
        public Guid BoardId { get; set; }
    }
}
