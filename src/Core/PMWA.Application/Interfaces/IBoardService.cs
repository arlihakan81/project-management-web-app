using PMWA.Application.Dtos.Board;

namespace PMWA.Application.Interfaces
{
    public interface IBoardService
    {
        Task<IEnumerable<BoardDto>?> GetAllAsync();
        Task<BoardDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<BoardDto>?> GetByProjectIdAsync(Guid projectId);



    }
}
