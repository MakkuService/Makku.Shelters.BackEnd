using Makku.Shelters.Application.Commands.DeleteShelter;
using Makku.Shelters.Application.Commands.UpdateShelter;
using Makku.Shelters.Application.Common.Exceptions;
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
            var handler = new UpdateShelterCommandHandler(Context);
            var newDescription = "new description";
            //Act
            await handler.Handle(
                new UpdateShelterCommand
                {
                    Id = SheltersContextFactory.ShelterIdForUpdate,
                    UserId = SheltersContextFactory.UserBId,
                    Description = newDescription
                }, CancellationToken.None);

            //Assert
            Assert.NotNull(await Context.Shelters
                .SingleOrDefaultAsync(s => s.Id == SheltersContextFactory.ShelterIdForUpdate && s.Description == newDescription));
        }

        [Fact]
        public async Task UpdateShelterCommandHandler_FailOnWrongId()
        {
            //Arrange
            var handler = new UpdateShelterCommandHandler(Context);
            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(new UpdateShelterCommand
                {
                    Id = Guid.NewGuid(),
                    UserId = SheltersContextFactory.UserAId
                }, CancellationToken.None));
        }

        [Fact]
        public async Task UpdateShelterCommandHandler_FailOnWrongUserId()
        {
            //Arrange
            var handler = new UpdateShelterCommandHandler(Context);
            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                {
                    await handler.Handle(
                        new UpdateShelterCommand
                        {
                            Id = SheltersContextFactory.ShelterIdForUpdate,
                            UserId = SheltersContextFactory.UserAId
                        },
                        CancellationToken.None);

                });
        }

    }
}
