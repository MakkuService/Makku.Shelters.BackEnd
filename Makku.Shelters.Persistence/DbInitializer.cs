namespace Makku.Shelters.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(SheltersDbContext context) => context.Database.EnsureCreated();
    }
}
