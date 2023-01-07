using Microsoft.EntityFrameworkCore;
using Makku.Shelters.Domain;
using Makku.Shelters.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Shelters.Tests.Common
{

    public class SheltersContextFactory
    {
        public static Guid ShelterAId = Guid.NewGuid();
        public static Guid ShelterBId = Guid.NewGuid();
        public static Guid ShelterCId = Guid.NewGuid();
        public static Guid ShelterDId = Guid.NewGuid();

        public static Guid ShelterProfileIdForDelete = Guid.NewGuid();
        public static Guid ShelterProfileIdForUpdate = Guid.NewGuid();

        public static SheltersDbContext Create()
        {
            var options = new DbContextOptionsBuilder<SheltersDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;
            var context = new SheltersDbContext(options);
            context.Database.EnsureCreated();

            var shelter1 = ShelterProfile.CreateShelterProfile(ShelterAId.ToString(), "test1@test.ru", "Shelter1");
            shelter1.ShelterProfileId = Guid.Parse("A6BB65BB-5AC2-4AFA-8A28-2616F675B825");
            shelter1.CreatedOn = DateTime.Today;
           
            var shelter2 = ShelterProfile.CreateShelterProfile(ShelterBId.ToString(), "test2@test.ru", "Shelter2");
            shelter2.ShelterProfileId = Guid.Parse("909F7C29-891B-4BE1-8504-21F84F262084");
            shelter2.CreatedOn = DateTime.Today;

            var shelter3 = ShelterProfile.CreateShelterProfile(ShelterCId.ToString(), "test3@test.ru", "Shelter3");
            shelter3.ShelterProfileId = ShelterProfileIdForDelete;
            shelter3.CreatedOn = DateTime.Today;

            var shelter4 = ShelterProfile.CreateShelterProfile(ShelterDId.ToString(), "test4@test.ru", "Shelter4");
            shelter4.ShelterProfileId = ShelterProfileIdForUpdate;
            shelter4.CreatedOn = DateTime.Today;


            context.ShelterProfiles.AddRange(
                shelter1,
                shelter2,
                shelter3,
                shelter4
            );

            var userA = new IdentityUser("UserA")
            {
                Id = ShelterAId.ToString()
            };
            var userB = new IdentityUser("UserB")
            {
                Id = ShelterBId.ToString()
            };
            var userC = new IdentityUser("UserC")
            {
                Id = ShelterCId.ToString()
            };
            var userD = new IdentityUser("UserD")
            {
                Id = ShelterDId.ToString()
            };

            context.Users.AddRange(userA, userB, userC, userD);
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


