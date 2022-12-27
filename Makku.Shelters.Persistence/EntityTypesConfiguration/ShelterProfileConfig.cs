using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Makku.Shelters.Domain.ShelterProfileAggregate;

namespace Makku.Shelters.Persistence.EntityTypesConfiguration
{
    internal class ShelterProfileConfig : IEntityTypeConfiguration<ShelterProfile>
    {
        public void Configure(EntityTypeBuilder<ShelterProfile> builder)
        {
            builder.OwnsOne(up => up.BasicInfo);
        }
    }
}
