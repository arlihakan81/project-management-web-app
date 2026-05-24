using Microsoft.EntityFrameworkCore;
using PMWA.Application.Repositories;
using PMWA.Domain.Entities;
using PMWA.Infrastructure.Contexts;
using System.Linq.Expressions;

namespace PMWA.Infrastructure.Repositories
{
    public class TaskRepository(AppDbContext context) : GenericRepository<TaskItem>(context), ITaskRepository
    {
        private readonly AppDbContext _context = context;

        public override async Task<IEnumerable<TaskItem>?> GetAllAsync()
        {
            return await _context.Tasks.Include(t => t.User).ThenInclude(u => u!.Role)
                .Include(t => t.Column).ThenInclude(c => c.Board).ThenInclude(b => b.Project).ToListAsync();
        }

        public override async Task<TaskItem?> GetByIdAsync(Guid id)
        {
            return await _context.Tasks.Include(t => t.User).ThenInclude(u => u!.Role)
                .Include(t => t.Column).ThenInclude(c => c.Board).ThenInclude(b => b.Project)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TaskItem>?> GetByProjectIdAsync(Guid projectId)
        {
            return await _context.Tasks.Where(t => t.Column.Board.ProjectId == projectId).ToListAsync();
        }
    }
}
