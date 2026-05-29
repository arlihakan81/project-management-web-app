using Microsoft.EntityFrameworkCore;
using PMWA.Application.Repositories;
using PMWA.Domain.Entities;
using PMWA.Infrastructure.Contexts;

namespace PMWA.Infrastructure.Repositories
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        private readonly AppDbContext _context = context;

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = _context.Users.Find(id) ?? throw new Exception($"{id} not found");
            user.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>?> GetAllAsync()
        {
            return await _context.Users.Include(u => u.Role).ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
