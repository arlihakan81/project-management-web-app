using AutoMapper;
using PMWA.Application.Dtos.User;
using PMWA.Application.Interfaces;
using PMWA.Application.Repositories;

namespace PMWA.Infrastructure.Services
{
    public class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<UserDto>?> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users is null ? [] : _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto?> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user is null ? null : _mapper.Map<UserDto>(user);
        }
    }
}
