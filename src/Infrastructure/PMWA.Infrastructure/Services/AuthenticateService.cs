using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PMWA.Application.Interfaces;
using PMWA.Application.Requests;
using PMWA.Domain.Entities;
using PMWA.Infrastructure.Contexts;
using System.Net.Http.Headers;

namespace PMWA.Infrastructure.Services
{
    public class AuthenticateService(AppDbContext context, ITokenService tokenService) : IAuthenticateService
    {
        private readonly AppDbContext _context = context;
        private readonly ITokenService _tokenService = tokenService;

        public async Task<string> AuthenticateAsync(LoginRequest request)
        {
            var user = await GetByEmailAsync(request.Email);
            if (user is null)
                return null!;
            if (!user.IsEmailConfirmed && user.IsDeleted)
                throw new UnauthorizedAccessException("Your email is not confirmed");
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password)
                is PasswordVerificationResult.Failed)
                throw new UnauthorizedAccessException("Invalid credentials");
            return _tokenService.GenerateToken(user);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.Include(u => u.Role).IgnoreQueryFilters().FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task RegisterAsync(RegisterRequest request)
        {
            var organization = new Organization()
            {
                Name = request.Email.Split('@')[1],
                Domain = request.Email.Split('@')[1],
                Users = [
                    new()
                    {
                        Name = request.Name,
                        Email = request.Email,
                        PasswordHash = new PasswordHasher<User>().HashPassword(null!, request.Password),
                        RoleId = _context.Roles.FirstOrDefault(r => r.Name == "Admin")!.Id                        
                    }
                ]
            };
            _context.Organizations.Add(organization);
            await _context.SaveChangesAsync();
        }
    }
}
