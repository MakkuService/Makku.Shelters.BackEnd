using Makku.Shelters.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Makku.Shelters.Application.Interfaces
{
    public interface ISheltersDbContext
    {
        DbSet<ShelterProfile> ShelterProfiles { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);

        Task<IdentityUser?> IdentityShelter(string identityShelterId, CancellationToken cancellationToken);

        void DeleteIdentityShelter(IdentityUser identityShelter);
    }
}
