using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Makku.Shelters.Application.Interfaces;
using Makku.Shelters.Domain;
using Makku.Shelters.Persistence.EntityTypesConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Makku.Shelters.Persistence
{
    public class SheltersDbContext : DbContext, ISheltersDbContext
    {
        public DbSet<Shelter> Shelters { get; set; }

        public SheltersDbContext(DbContextOptions<SheltersDbContext> options)
            : base(options)
        { }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ShelterConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
