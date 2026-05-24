using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PMWA.Application.Interfaces;
using System.Security.Claims;
using PMWA.Domain.Entities;
using PMWA.Application.Requests;

namespace PMWA.WebUI.Controllers
{
    [Authorize]
    public class AuthenticateController(HttpClient client, IAuthenticateService authService) : Controller
    {
        private readonly HttpClient _client = client;
        private readonly IAuthenticateService _authService = authService;
        private string baseUrl = "https://localhost:7007/"; // API base URL

        [HttpGet("login")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var response = await _client.PostAsJsonAsync($"{baseUrl}api/v1/auth/login", request);
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                Response.Cookies.Append("JWToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddHours(1)
                });
            }

            var user = await _authService.GetByEmailAsync(request.Email);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı bulunamadı veya parola hatalı");
                return View();
            }

            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı bulunamadı veya parola hatalı");
                return View();
            }

            var claims = new List<Claim>
            {
                new ("avatar", user.Avatar ?? string.Empty),
                new (ClaimTypes.NameIdentifier, user.Id.ToString()),
                new (ClaimTypes.Name, user.Name),
                new (ClaimTypes.Email, user.Email),
                new (ClaimTypes.Role, user.RoleId.ToString()),
                new ("OrganizationId", user.OrganizationId.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1) // Set expiration time as needed
            });

            return RedirectToAction("Index", "Home");
        }



    }
}
