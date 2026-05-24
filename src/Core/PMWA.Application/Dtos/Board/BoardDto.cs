using PMWA.Application.Dtos.Project;

namespace PMWA.Application.Dtos.Board
{
    public class BoardDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public virtual ProjectDto Project { get; set; } = new ProjectDto();
        public virtual List<Column.ColumnDto> Columns { get; set; } = [];
    }
}
