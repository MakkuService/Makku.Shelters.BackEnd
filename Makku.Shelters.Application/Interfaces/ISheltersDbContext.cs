using Makku.Shelters.Domain;
using Microsoft.EntityFrameworkCore;

namespace Makku.Shelters.Application.Interfaces
{
    public interface ISheltersDbContext
    {
        DbSet<Shelter> Shelters { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
