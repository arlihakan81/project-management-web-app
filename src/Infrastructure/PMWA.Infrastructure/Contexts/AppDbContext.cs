using Microsoft.EntityFrameworkCore;
using PMWA.Application.Interfaces;
using PMWA.Domain.Entities;
using PMWA.Domain.Entities.Commons;

namespace PMWA.Infrastructure.Contexts
{
    public class AppDbContext(IOrganizationService organizationService = null!) : DbContext
    {
        private readonly IOrganizationService _organizationService = organizationService;

        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=PMWADb; Trusted_Connection=True; TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(u => u.OrganizationId == _organizationService.GetCurrentOrganizationId() && !u.IsDeleted);
            modelBuilder.Entity<Project>().HasQueryFilter(p => p.OrganizationId == _organizationService.GetCurrentOrganizationId() && !p.IsDeleted && !p.IsArchived);

            modelBuilder.Entity<Project>().HasOne(p => p.Owner).WithMany(u => u.Projects).HasForeignKey(p => p.OwnerId);
            modelBuilder.Entity<Project>().HasOne(p => p.CreatedBy).WithMany().HasForeignKey(p => p.CreatedById).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Project>().HasOne(p => p.ModifiedBy).WithMany().HasForeignKey(p => p.ModifiedById);

            modelBuilder.Entity<Role>().HasData(
                [
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Admin"
                    },
                    new()
                    {
                        Id = Guid.NewGuid(),
                        Name = "User"
                    }
                ]);
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries().Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (BaseEntity)entry.Entity;
                var now = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = now;
                    if(!_organizationService.IsAuthenticated())
                        await base.SaveChangesAsync(cancellationToken); // Save changes to generate IDs for new entities
                    entity.OrganizationId = _organizationService.GetCurrentOrganizationId();
                    entity.CreatedById = _organizationService.GetAuthenticatedUserId();
                }
                else
                {
                    entity.ModifiedAt = now;
                    if(!_organizationService.IsAuthenticated())
                        await base.SaveChangesAsync(cancellationToken); // Save changes to generate IDs for existing entities
                    entity.ModifiedById = _organizationService.GetAuthenticatedUserId();
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }


    }
}
