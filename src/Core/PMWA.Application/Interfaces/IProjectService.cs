using PMWA.Application.Dtos.Project;

namespace PMWA.Application.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>?> GetAllAsync();
        Task<ProjectDto?> GetByIdAsync(Guid id);
        Task CreateAsync(CreateProjectDto createProjectDto);
        Task UpdateAsync(Guid id, UpdateProjectDto updateProjectDto);
        Task DeleteAsync(Guid id);
        Task ArchiveAsync(Guid id);


    }
}
