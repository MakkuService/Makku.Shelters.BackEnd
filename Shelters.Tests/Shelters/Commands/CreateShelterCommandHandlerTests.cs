using Makku.Shelters.Application.Commands.CreateShelter;
using Microsoft.EntityFrameworkCore;
using Shelters.Tests.Common;

namespace Shelters.Tests.Shelters.Commands
{
    public class CreateShelterCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateShelterCommandHandler_Success()
        {
            //Arrange - подготовка данных для теста
            var handler = new CreateShelterCommandHandler(Context);
            var shelterName = "Shelter name";
            var shelterDescription = "Shelter description";

            //Act - выполнение логики
            var shelterId = await handler.Handle(
                new CreateShelterCommand
                {
                    Name = shelterName,
                    Description = shelterDescription,
                    UserId = SheltersContextFactory.UserAId
                }, CancellationToken.None);

            //Assert - проверка результатов
            Assert.NotNull(await Context.Shelters.SingleOrDefaultAsync(s => s.Id == shelterId && s.Name == shelterName && s.Description == shelterDescription));
        }
    }
}
