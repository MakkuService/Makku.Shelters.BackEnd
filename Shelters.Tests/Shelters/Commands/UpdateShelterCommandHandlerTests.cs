using Makku.Shelters.Application.Common.Exceptions;
using Makku.Shelters.Application.Shelters.Profile.Commands.UpdateProfile;
using Microsoft.EntityFrameworkCore;
using Shelters.Tests.Common;

namespace Shelters.Tests.Shelters.Commands
{
    public class UpdateShelterCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateShelterCommandHandler_Success()
        {
            //Arrange
            var handler = new UpdateProfileCommandHandler(Context);
            var newDescription = "new description";
            //Act
            await handler.Handle(
                new UpdateProfileCommand
                {
                    IdentityShelterId = SheltersContextFactory.ShelterDId,
                    RequestorGuid = SheltersContextFactory.ShelterDId,
                    Description = newDescription
                }, CancellationToken.None);

            //Assert
            Assert.NotNull(await Context.ShelterProfiles
                .SingleOrDefaultAsync(s => s.ShelterProfileId == SheltersContextFactory.ShelterProfileIdForUpdate && s.Description == newDescription));
        }

        [Fact]
        public async Task UpdateShelterCommandHandler_FailOnWrongId()
        {
            //Arrange
            var handler = new UpdateProfileCommandHandler(Context);
            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(new UpdateProfileCommand
                {
                    IdentityShelterId = Guid.NewGuid(),
                    RequestorGuid = SheltersContextFactory.ShelterAId
                }, CancellationToken.None));
        }

        [Fact]
        public async Task UpdateShelterCommandHandler_FailOnWrongUserId()
        {
            //Arrange
            var handler = new UpdateProfileCommandHandler(Context);
            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                {
                    await handler.Handle(
                        new UpdateProfileCommand
                        {
                            IdentityShelterId = SheltersContextFactory.ShelterProfileIdForUpdate,
                            RequestorGuid = SheltersContextFactory.ShelterAId
                        },
                        CancellationToken.None);
                });
        }

    }
}
