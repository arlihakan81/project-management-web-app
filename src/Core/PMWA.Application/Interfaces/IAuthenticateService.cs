using PMWA.Application.Requests;
using PMWA.Domain.Entities;

namespace PMWA.Application.Interfaces
{
    public interface IAuthenticateService
    {
        Task<string> AuthenticateAsync(LoginRequest request);
        Task RegisterAsync(RegisterRequest request);
        Task<User?> GetByEmailAsync(string email);
            

    }
}
