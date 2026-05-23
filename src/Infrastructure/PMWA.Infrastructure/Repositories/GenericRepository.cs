using Microsoft.EntityFrameworkCore;
using PMWA.Application.Repositories;
using PMWA.Domain.Entities.Commons;
using PMWA.Infrastructure.Contexts;
using System.Linq.Expressions;

namespace PMWA.Infrastructure.Repositories
{
    public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context = context;

        public virtual async Task AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = _context.Set<T>().Find(id) ?? throw new KeyNotFoundException("Bu öğe mevcut değil");
            entity.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<T>?> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<T?> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
