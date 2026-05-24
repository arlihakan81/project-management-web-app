using PMWA.Domain.Entities;

namespace PMWA.Application.Repositories
{
    public interface ITaskRepository : IGenericRepository<TaskItem>
    {
        Task<IEnumerable<TaskItem>?> GetByProjectIdAsync(Guid projectId);



    }
}
