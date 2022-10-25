using Makku.Shelters.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Makku.Shelters.Persistence.EntityTypesConfiguration
{
    public class ShelterConfiguration : IEntityTypeConfiguration<Shelter>
    {
        public void Configure(EntityTypeBuilder<Shelter> builder)
        {
            builder.HasKey(shelter => shelter.Id);
            builder.HasIndex(shelter => shelter.Id).IsUnique();
            builder.Property(shelter => shelter.Id).HasMaxLength(250);
        }
    }
}
