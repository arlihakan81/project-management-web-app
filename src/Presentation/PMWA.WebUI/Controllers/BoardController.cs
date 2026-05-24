using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using PMWA.Application.Dtos.Board;
using PMWA.Application.Dtos.Task;
using PMWA.Domain.Entities;
using PMWA.WebUI.Models.Boards;

namespace PMWA.WebUI.Controllers
{
    [Authorize]
    public class BoardController(HttpClient client) : Controller
    {
        private readonly HttpClient client = client;
        private readonly string apiUrl = "https://localhost:7007/api/v1";

        private void AddToken()
        {
            var token = HttpContext.Request.Cookies["JWToken"];
            if (token != null)
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

        [HttpGet("boards")]
        public async Task<IActionResult> Index()
        {
            AddToken();
            var viewModel = new BoardListViewModel()
            {
                Boards = await client.GetFromJsonAsync<List<BoardDto>>($"{apiUrl}/boards")
            };
            return View(viewModel);
        }

        [HttpGet("boards/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            AddToken();
            var board = await client.GetFromJsonAsync<BoardDto>($"{apiUrl}/boards/{id}");
            var viewModel = new BoardDetailsViewModel()
            {
                Board = board,
                Boards = await client.GetFromJsonAsync<List<BoardDto>>($"{apiUrl}/projects/{board!.Project.Id}/boards"),
                Users = []
                //await client.GetFromJsonAsync<List<PMWA.Application.Dtos.User.UserDto>>($"{apiUrl}/projects/{board.Project.Id}/users")
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(BoardDetailsViewModel viewModel)
        {
            AddToken();
            var response = await client.PostAsJsonAsync($"{apiUrl}/tasks", viewModel.CreateTaskDto);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Details", new { id = viewModel.Board!.Id });
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, error);
                return RedirectToAction("Details", new { id = viewModel.Board!.Id });
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditTask(Guid id, BoardDetailsViewModel viewModel)
        {
            AddToken();
            var response = await client.PutAsJsonAsync($"{apiUrl}/tasks/{id}", viewModel.UpdateTaskDto);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Details", new { id = viewModel.Board!.Id });
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, error);
                return RedirectToAction("Details", new { id = viewModel.Board!.Id });
            }
        }

    }
}
