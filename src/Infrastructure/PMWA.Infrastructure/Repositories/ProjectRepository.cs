using Microsoft.EntityFrameworkCore;
using PMWA.Application.Repositories;
using PMWA.Domain.Entities;
using PMWA.Infrastructure.Contexts;

namespace PMWA.Infrastructure.Repositories
{
    public class ProjectRepository(AppDbContext context) : GenericRepository<Project>(context), IProjectRepository
    {
        private readonly AppDbContext _context = context;

        public override async Task AddAsync(Project entity)
        {
            await base.AddAsync(entity);
            var board = new Board()
            {
                Name = $"{entity.Title}'s Board",
                ProjectId = entity.Id,
                Columns =
                [
                    new Column() { Name = "To Do", Position = 1 },
                    new Column() { Name = "In Progress", Position = 2 },
                    new Column() { Name = "Done", Position = 3 }
                ]
            };
            _context.Boards.Add(board);
            await _context.SaveChangesAsync();
        }

        public override async Task<IEnumerable<Project>?> GetAllAsync()
        {
            return await _context.Projects.Include(p => p.Owner).ThenInclude(u => u.Role).ToListAsync();
        }

        public async Task ArchiveAsync(Guid id)
        {
            var project = await GetByIdAsync(id) ?? throw new Exception($"{id} not found");            
            project.IsArchived = true;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsTitleUniqueAsync(string title, Guid? projectId = null)
        {
            return projectId is null ? !await _context.Projects.AnyAsync(p => p.Title != title) :
                !await _context.Projects.AnyAsync(p => p.Title != title && p.Id != projectId.Value);
        }
    }
}
