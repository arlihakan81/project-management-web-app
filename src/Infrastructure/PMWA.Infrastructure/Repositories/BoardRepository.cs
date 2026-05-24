using Microsoft.EntityFrameworkCore;
using PMWA.Application.Repositories;
using PMWA.Domain.Entities;
using PMWA.Infrastructure.Contexts;

namespace PMWA.Infrastructure.Repositories
{
    public class BoardRepository(AppDbContext context) : GenericRepository<Domain.Entities.Board>(context), IBoardRepository    
    {
        private readonly AppDbContext _context = context;

        public override async Task<IEnumerable<Board>?> GetAllAsync()
        {
            return await _context.Boards.Include(b => b.Columns)!.ThenInclude(c => c.Tasks).Include(b => b.Project).ThenInclude(p => p.Owner).ToListAsync();
        }

        public override async Task<Board?> GetByIdAsync(Guid id)
        {
            return await _context.Boards.Include(b => b.Columns)!.ThenInclude(c => c.Tasks).Include(b => b.Project).ThenInclude(p => p.Owner).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Board>?> GetByProjectIdAsync(Guid projectId)
        {
            return await _context.Boards.Where(b => b.ProjectId == projectId).Include(b => b.Project).ThenInclude(p => p.Owner).ToListAsync();
        }
    }
}
