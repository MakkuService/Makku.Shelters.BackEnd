using Makku.Shelters.Application.Shelters.Identity.Commands.DeleteShelter;
using Microsoft.EntityFrameworkCore;
using Shelters.Tests.Common;

namespace Shelters.Tests.Shelters.Commands
{
    public class DeleteShelterCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteShelterCommandHandler_Success()
        {
            //Arrange
            var handler = new DeleteShelterCommandHandler(Context);
            //Act
            await handler.Handle(
                new DeleteShelterCommand
                {
                    IdentityShelterId = SheltersContextFactory.ShelterCId,
                    RequestorGuid = SheltersContextFactory.ShelterCId,
                }, CancellationToken.None);

            //Assert
            Assert.Null(await Context.ShelterProfiles.SingleOrDefaultAsync(s => s.ShelterProfileId == SheltersContextFactory.ShelterProfileIdForDelete));
        }
    }
}
