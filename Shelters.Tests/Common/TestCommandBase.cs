using Makku.Shelters.Persistence;

namespace Shelters.Tests.Common
{
    public abstract class TestCommandBase : IDisposable
    {
        protected readonly SheltersDbContext Context;

        protected TestCommandBase() => Context = SheltersContextFactory.Create();

        public void Dispose() => SheltersContextFactory.Destroy(Context);
    }
}
