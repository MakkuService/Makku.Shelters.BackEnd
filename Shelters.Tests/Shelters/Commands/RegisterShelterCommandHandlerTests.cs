using Makku.Shelters.Application.Services;
using Makku.Shelters.Application.Shelters.Identity.Commands.RegisterShelter;
using Microsoft.EntityFrameworkCore;
using Shelters.Tests.Common;

namespace Shelters.Tests.Shelters.Commands
{
    public class RegisterShelterCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task RegisterShelterCommandHandler_Success()
        {
            //Arrange - подготовка данных для теста

            var handler = new RegisterShelterCommandHandler(
                Context, UserManager, new IdentityService(Configuration), Mapper);
            var shelterName = "Shelter name";
            var shelterEmail = "test@test.com";
            var shelterUserName = "testUser";

            //Act - выполнение логики

            await handler.Handle(
                new RegisterShelterCommand
                {
                    ShelterName = shelterName,
                    Email = shelterEmail,
                    Password = "!qaz2wsX",
                    Username = shelterUserName
                }, CancellationToken.None);

            //Assert - проверка результатов

            Assert.NotNull(
                await Context.ShelterProfiles
                    .SingleOrDefaultAsync(
                        s =>
                            s.ShelterName == shelterName && s.Email==shelterEmail));
        }


    }
}
