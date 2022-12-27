using Makku.Shelters.Application.Interfaces;
using Makku.Shelters.Domain;
using Makku.Shelters.Domain.ShelterProfileAggregate;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Makku.Shelters.Persistence
{
    public class SheltersDbContext : IdentityDbContext, ISheltersDbContext
    {
        public DbSet<Shelter> Shelters { get; set; }
        public DbSet<ShelterProfile> ShelterProfiles { get; set; }
        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
        {
            return base.Database.BeginTransactionAsync(cancellationToken);
        }

        public SheltersDbContext(DbContextOptions<SheltersDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<BasicInfo>();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SheltersDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
