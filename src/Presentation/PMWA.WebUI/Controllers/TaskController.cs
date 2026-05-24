using Microsoft.AspNetCore.Mvc;

namespace PMWA.WebUI.Controllers
{
    public class TaskController(HttpClient client) : Controller
    {
        private readonly HttpClient client = client;


        public IActionResult Index()
        {
            return View();
        }
    }
}
