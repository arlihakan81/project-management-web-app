using PMWA.Domain.Entities;

namespace PMWA.Application.Repositories
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<bool> IsTitleUniqueAsync(string title, Guid? projectId = null);
        Task ArchiveAsync(Guid id);
    }
}
