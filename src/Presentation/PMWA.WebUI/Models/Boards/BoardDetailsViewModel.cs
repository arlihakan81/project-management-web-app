using PMWA.Application.Dtos.Board;
using PMWA.Application.Dtos.Column;
using PMWA.Application.Dtos.Task;
using PMWA.Application.Dtos.User;

namespace PMWA.WebUI.Models.Boards
{
    public class BoardDetailsViewModel
    {
        public BoardDto? Board { get; set; }
        public List<BoardDto>? Boards { get; set; }
        public CreateTaskDto CreateTaskDto { get; set; } = new CreateTaskDto();
        public List<UserDto>? Users { get; set; }
        public UpdateTaskDto? UpdateTaskDto { get; set; }
        public CreateColumnDto CreateColumnDto { get; set; }
    }
}
