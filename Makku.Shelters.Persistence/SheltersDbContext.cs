using Makku.Shelters.Application.Interfaces;
using Makku.Shelters.Domain;
using Makku.Shelters.Persistence.EntityTypesConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Makku.Shelters.Persistence
{
    public class SheltersDbContext : DbContext, ISheltersDbContext
    {
        public DbSet<Shelter> Shelters { get; set; }

        public SheltersDbContext(DbContextOptions<SheltersDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ShelterConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
