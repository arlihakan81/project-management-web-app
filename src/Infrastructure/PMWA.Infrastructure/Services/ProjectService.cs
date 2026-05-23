using AutoMapper;
using PMWA.Application.Dtos.Project;
using PMWA.Application.Interfaces;
using PMWA.Application.Repositories;
using PMWA.Domain.Entities;

namespace PMWA.Infrastructure.Services
{
    public class ProjectService(IProjectRepository projectRepository, IMapper mapper) : IProjectService
    {
        private readonly IProjectRepository _projectRepository = projectRepository;
        private readonly IMapper _mapper = mapper;

        public async Task ArchiveAsync(Guid id)
        {
            await _projectRepository.ArchiveAsync(id);
        }

        public async Task CreateAsync(CreateProjectDto createProjectDto)
        {
            await _projectRepository.AddAsync(_mapper.Map<Project>(createProjectDto));
        }

        public async Task DeleteAsync(Guid id)
        {
            await _projectRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<ProjectDto>?> GetAllAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return projects is null ? [] : _mapper.Map<IEnumerable<ProjectDto>>(projects);
        }

        public async Task<ProjectDto?> GetByIdAsync(Guid id)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            return project is null ? null : _mapper.Map<ProjectDto>(project);
        }

        public async Task UpdateAsync(Guid id, UpdateProjectDto updateProjectDto)
        {
            var project = await _projectRepository.GetByIdAsync(id);
            await _projectRepository.UpdateAsync(_mapper.Map(updateProjectDto, project)!);
        }
    }
}
