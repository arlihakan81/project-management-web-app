using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMWA.Application.Dtos.Column;
using PMWA.Application.Interfaces;

namespace PMWA.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class BoardsController(IBoardService boardService) : ControllerBase
    {
        private readonly IBoardService _boardService = boardService;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _boardService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id) => Ok(await _boardService.GetByIdAsync(id));

        [HttpPost("{id}/column")]
        public async Task<IActionResult> AddColumn(Guid id, [FromBody] CreateColumnDto createColumnDto)
        {
            await _boardService.AddColumnAsync(id, createColumnDto.Name);
            return NoContent();
        }



    }
}
