namespace PMWA.Application.Interfaces
{
    public interface IOrganizationService
    {
        Guid GetCurrentOrganizationId();
        bool IsAuthenticated();
        Guid GetAuthenticatedUserId();
    }
}
