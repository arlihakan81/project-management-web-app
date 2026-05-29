using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMWA.Application.Interfaces;

namespace PMWA.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _userService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id) => Ok(await _userService.GetByIdAsync(id));


    }
}
