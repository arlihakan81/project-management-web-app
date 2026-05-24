using PMWA.Application.Dtos.Board;
using PMWA.Application.Dtos.Task;

namespace PMWA.Application.Dtos.Column
{
    public class ColumnDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Position { get; set; }

        public virtual BoardDto Board { get; set; }
        public virtual ICollection<TaskDto>? Tasks { get; set; }
    }
}
