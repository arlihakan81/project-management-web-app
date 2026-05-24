using AutoMapper;
using FluentValidation;
using PMWA.Application.Dtos.Project;
using PMWA.Application.Interfaces;
using PMWA.Application.Repositories;
using PMWA.Domain.Entities;

namespace PMWA.Infrastructure.Services
{
    public class ProjectService(IProjectRepository projectRepository, IMapper mapper, IValidator<CreateProjectDto> validator) : IProjectService
    {
        private readonly IProjectRepository _projectRepository = projectRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<CreateProjectDto> _validator = validator;

        public async Task ArchiveAsync(Guid id)
        {
            await _projectRepository.ArchiveAsync(id);
        }

        public async Task CreateAsync(CreateProjectDto createProjectDto)
        {
            var validate = await _validator.ValidateAsync(createProjectDto);
            if (!validate.IsValid)
            {
                // Burada özel bir exception fırlatabilir veya hata mesajlarını dönebilirsin
                var errors = string.Join(", ", validate.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
            if(!await _projectRepository.IsTitleUniqueAsync(createProjectDto.Title))
            {
                throw new ValidationException("Project title must be unique.");
            }
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
            var project = await _projectRepository.GetByIdAsync(id) ?? throw new Exception($"{id} not found");
            var validate = await _validator.ValidateAsync(updateProjectDto);
            if (!validate.IsValid)
            {
                // Burada özel bir exception fırlatabilir veya hata mesajlarını dönebilirsin
                var errors = string.Join(", ", validate.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
            if (!await _projectRepository.IsTitleUniqueAsync(updateProjectDto.Title, id))
            {
                throw new ValidationException("Project title must be unique.");
            }
            await _projectRepository.UpdateAsync(_mapper.Map(updateProjectDto, project)!);
        }
    }
}
