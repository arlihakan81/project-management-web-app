using PMWA.Domain.Entities;
using System.Security.Claims;

namespace PMWA.Application.Interfaces
{
    public interface ITokenService
    {
        List<Claim>? GetClaims(User user);
        string GenerateToken(User user);
    }
}
