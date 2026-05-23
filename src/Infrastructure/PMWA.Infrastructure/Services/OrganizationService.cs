using Microsoft.AspNetCore.Http;
using PMWA.Application.Interfaces;
using System.Security.Claims;

namespace PMWA.Infrastructure.Services
{
    public class OrganizationService(IHttpContextAccessor httpContextAccessor) : IOrganizationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public Guid GetAuthenticatedUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim is not null && Guid.TryParse(userIdClaim.Value, out var userId))
                return userId;
            throw new Exception("User ID claim not found or invalid");
        }

        public Guid GetCurrentOrganizationId()
        {
            var organizationIdClaim = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "OrganizationId");
            if (organizationIdClaim is not null && Guid.TryParse(organizationIdClaim.Value, out var organizationId))
                return organizationId;
            throw new Exception("Organization ID claim not found or invalid");
        }

        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext?.User.Identity!.IsAuthenticated ?? false;
        }
    }
}
