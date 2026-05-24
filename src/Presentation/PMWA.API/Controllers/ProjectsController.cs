using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMWA.Application.Dtos.Project;
using PMWA.Application.Interfaces;

namespace PMWA.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController(IProjectService projectService, IBoardService boardService) : ControllerBase
    {
        private readonly IProjectService _projectService = projectService;
        private readonly IBoardService _boardService = boardService;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _projectService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id) => Ok(await _projectService.GetByIdAsync(id));

        [HttpGet("{id}/boards")]
        public async Task<IActionResult> GetBoards(Guid id) => Ok(await _boardService.GetByProjectIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProjectDto createProjectDto)
        {
            try
            {
                await _projectService.CreateAsync(createProjectDto);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateProjectDto updateProjectDto)
        {
            try
            {
                await _projectService.UpdateAsync(id, updateProjectDto);
                return Ok("Projeniz başarıyla güncellendi");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}/archive")]
        public async Task<IActionResult> Archive(Guid id)
        {
            try
            {
                await _projectService.ArchiveAsync(id);
                return Ok("Projeniz başarıyla arşivlendi");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _projectService.DeleteAsync(id);
                return Ok("Projeniz başarıyla silindi");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }









    }
}
