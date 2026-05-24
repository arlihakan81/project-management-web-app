using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace PMWA.WebUI.Controllers
{
    [Authorize]
    public class ProjectController(HttpClient client) : Controller
    {
        private readonly HttpClient _client = client;
        private string baseUrl = "https://localhost:7007/api/v1"; // API base URL

        private void AddToken()
        {
            var token = Request.Cookies["JWToken"];
            if (!string.IsNullOrEmpty(token))
            {
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
        }

        [HttpGet("projects")]
        public async Task<IActionResult> Index()
        {
            AddToken();
            var viewModel = new Models.Projects.ProjectListViewModel
            {
                Projects = await _client.GetFromJsonAsync<List<Application.Dtos.Project.ProjectDto>>($"{baseUrl}/projects")
            };
            return View(viewModel);
        }

        [HttpGet("projects/create")]
        public IActionResult Create()
        {
            var viewModel = new Models.Projects.ProjectCreateViewModel
            {
                CreateProjectDto = new Application.Dtos.Project.CreateProjectDto()
            };
            return View(viewModel);
        }

        [HttpPost("projects/create")]
        public async Task<IActionResult> Create(Models.Projects.ProjectCreateViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            AddToken();
            var response = await _client.PostAsJsonAsync($"{baseUrl}/projects", viewModel.CreateProjectDto);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                // Hata durumunda detaylı bilgi al
                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"{errorContent}");
                return View(viewModel);
            }            
        }

        [HttpGet("projects/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            AddToken();
            var project = await _client.GetFromJsonAsync<Application.Dtos.Project.ProjectDto>($"{baseUrl}/projects/{id}");
            if (project == null)
            {
                return NotFound();
            }
            var viewModel = new Models.Projects.ProjectDetailsViewModel
            {
                Project = project
            };
            return View(viewModel);
        }

        [HttpGet("projects/{id:guid}/edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            AddToken();
            var project = await _client.GetFromJsonAsync<Application.Dtos.Project.ProjectDto>($"{baseUrl}/projects/{id}");
            if (project == null)
            {
                return NotFound();
            }
            var viewModel = new Models.Projects.ProjectEditViewModel
            {
                UpdateProjectDto = new Application.Dtos.Project.UpdateProjectDto
                {
                    Title = project.Title,
                    Description = project.Description,
                    Deadline = project.Deadline,
                    Status = project.Status,
                    Budget = project.Budget
                }
            };
            return View(viewModel);
        }

        [HttpPost("projects/{id:guid}/edit")]
        public async Task<IActionResult> Edit(Guid id, Models.Projects.ProjectEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            AddToken();
            var response = await _client.PutAsJsonAsync($"{baseUrl}/projects/{id}", viewModel.UpdateProjectDto);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Details", new { id });
            }
            else
            {
                // Hata durumunda detaylı bilgi al
                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"{errorContent}");
                return View(viewModel);
            }
        }







    }
}
