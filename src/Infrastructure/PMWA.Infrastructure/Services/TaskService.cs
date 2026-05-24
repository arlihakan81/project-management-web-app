using AutoMapper;
using FluentValidation;
using PMWA.Application.Dtos.Task;
using PMWA.Application.Interfaces;
using PMWA.Application.Repositories;
using PMWA.Domain.Entities;

namespace PMWA.Infrastructure.Services
{
    public class TaskService(ITaskRepository taskRepository, IMapper mapper, IValidator<CreateTaskDto> validator) : ITaskService
    {
        private readonly ITaskRepository _taskRepository = taskRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<CreateTaskDto> _validator = validator;

        public async Task CreateAsync(CreateTaskDto createTaskDto)
        {
            var validate = await _validator.ValidateAsync(createTaskDto);
            if (!validate.IsValid)
            {
                // Burada özel bir exception fırlatabilir veya hata mesajlarını dönebilirsin
                var errors = string.Join(", ", validate.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
            await _taskRepository.AddAsync(_mapper.Map<TaskItem>(createTaskDto));
        }

        public async Task DeleteAsync(Guid id)
        {
            await _taskRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TaskDto>?> GetAllAsync()
        {
            var tasks = await _taskRepository.GetAllAsync();
            return tasks is null ? [] : _mapper.Map<IEnumerable<TaskDto>>(tasks);
        }

        public async Task<TaskDto?> GetByIdAsync(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            return task is null ? null : _mapper.Map<TaskDto>(task);
        }

        public async Task UpdateAsync(Guid id, UpdateTaskDto updateTaskDto)
        {
            var task = await _taskRepository.GetByIdAsync(id) ?? throw new Exception($"{id} not found");
            var validate = await _validator.ValidateAsync(updateTaskDto);
            if (!validate.IsValid)
            {
                // Burada özel bir exception fırlatabilir veya hata mesajlarını dönebilirsin
                var errors = string.Join(", ", validate.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException(errors);
            }
            await _taskRepository.UpdateAsync(_mapper.Map(updateTaskDto, task));
        }
    }
}
