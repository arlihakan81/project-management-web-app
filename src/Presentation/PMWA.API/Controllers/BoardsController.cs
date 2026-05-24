using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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







    }
}
