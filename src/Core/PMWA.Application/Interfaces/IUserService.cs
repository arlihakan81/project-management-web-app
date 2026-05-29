using PMWA.Application.Dtos.User;

namespace PMWA.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>?> GetAllAsync();
        Task<UserDto?> GetByIdAsync(Guid id);



    }
}
