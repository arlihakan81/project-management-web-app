using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMWA.Application.Dtos.Task;
using PMWA.Application.Interfaces;

namespace PMWA.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController(ITaskService taskService) : ControllerBase
    {
        private readonly ITaskService _taskService = taskService;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _taskService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id) => Ok(await _taskService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTaskDto createTaskDto)
        {
            await _taskService.CreateAsync(createTaskDto);
            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateTaskDto updateTaskDto)
        {
            await _taskService.UpdateAsync(id, updateTaskDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _taskService.DeleteAsync(id);
            return NoContent();
        }






    }
}
