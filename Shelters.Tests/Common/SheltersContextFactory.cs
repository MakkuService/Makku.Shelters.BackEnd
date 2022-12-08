using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Makku.Shelters.Domain;
using Makku.Shelters.Persistence;

namespace Shelters.Tests.Common
{

    public class SheltersContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();

        public static Guid NoteIdForDelete = Guid.NewGuid();
        public static Guid NoteIdForUpdate = Guid.NewGuid();

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
                    CreationDate = DateTime.Today,
                    Details = "Details1",
                    EditDate = null,
                    Id = Guid.Parse("A6BB65BB-5AC2-4AFA-8A28-2616F675B825"),
                    Title = "Title1",
                    UserId = UserAId
                },
                new Shelter
                {
                    CreationDate = DateTime.Today,
                    Details = "Details2",
                    EditDate = null,
                    Id = Guid.Parse("909F7C29-891B-4BE1-8504-21F84F262084"),
                    Title = "Title2",
                    UserId = UserBId
                },
                new Shelter
                {
                    CreationDate = DateTime.Today,
                    Details = "Details3",
                    EditDate = null,
                    Id = NoteIdForDelete,
                    Title = "Title3",
                    UserId = UserAId
                },
                new Shelter
                {
                    CreationDate = DateTime.Today,
                    Details = "Details4",
                    EditDate = null,
                    Id = NoteIdForUpdate,
                    Title = "Title4",
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


