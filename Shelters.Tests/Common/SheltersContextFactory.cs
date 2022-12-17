using Microsoft.EntityFrameworkCore;
using Makku.Shelters.Domain;
using Makku.Shelters.Persistence;

namespace Shelters.Tests.Common
{

    public class SheltersContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();

        public static Guid ShelterIdForDelete = Guid.NewGuid();
        public static Guid ShelterIdForUpdate = Guid.NewGuid();

        public static SheltersDbContext Create()
        {
            var options = new DbContextOptionsBuilder<SheltersDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new SheltersDbContext(options);
            context.Database.EnsureCreated();
            context.Shelters.AddRange(
                new Shelter
                {
                    CreatedOn = DateTime.Today,
                    Description = "Description1",
                    ModifiedOn = null,
                    Id = Guid.Parse("A6BB65BB-5AC2-4AFA-8A28-2616F675B825"),
                    Name = "Shelter1",
                    UserId = UserAId
                },
                new Shelter
                {
                    CreatedOn = DateTime.Today,
                    Description = "Description2",
                    ModifiedOn = null,
                    Id = Guid.Parse("909F7C29-891B-4BE1-8504-21F84F262084"),
                    Name = "Shelter2",
                    UserId = UserBId

                },
                new Shelter
                {
                    CreatedOn = DateTime.Today,
                    Description = "Description3",
                    ModifiedOn = null,
                    Id = ShelterIdForDelete,
                    Name = "Shelter3",
                    UserId = UserAId
                },
                new Shelter
                {
                    CreatedOn = DateTime.Today,
                    Description = "Description4",
                    ModifiedOn = null,
                    Id = ShelterIdForUpdate,
                    Name = "Shelter4",
                    UserId = UserBId
                }
            );
            context.SaveChanges();
            return context;
        }

        public static void Destroy(SheltersDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}


