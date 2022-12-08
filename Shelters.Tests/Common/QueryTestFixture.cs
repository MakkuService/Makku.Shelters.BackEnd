using AutoMapper;
using Makku.Shelters.Application.Common.Mappings;
using Makku.Shelters.Application.Interfaces;
using Makku.Shelters.Persistence;

namespace Shelters.Tests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public SheltersDbContext Context;
        public IMapper Mapper;

        public QueryTestFixture()
        {
            Context = SheltersContextFactory.Create();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(
                    typeof(ISheltersDbContext).Assembly));
            });
            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            SheltersContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
