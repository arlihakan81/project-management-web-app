using PMWA.Application.Dtos.Task;

namespace PMWA.Application.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>?> GetAllAsync();
        Task<TaskDto?> GetByIdAsync(Guid id);
        Task CreateAsync(CreateTaskDto createTaskDto);
        Task UpdateAsync(Guid id, UpdateTaskDto updateTaskDto);
        Task DeleteAsync(Guid id);





    }
}
