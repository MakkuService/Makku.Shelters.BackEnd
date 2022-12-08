using Makku.Shelters.Persistence;

namespace Shelters.Tests.Common
{
    public abstract class TestCommandBase
    {
        protected readonly SheltersDbContext Context;

        public TestCommandBase()
        {
            Context = SheltersContextFactory.Create();
        }

        public void Dispose()
        {
            SheltersContextFactory.Destroy(Context);
        }
    }
}
