using PMWA.Domain.Entities;

namespace PMWA.Application.Repositories
{
    public interface IBoardRepository : IGenericRepository<Board>
    {
        Task<IEnumerable<Board>?> GetByProjectIdAsync(Guid projectId);
    }
}
