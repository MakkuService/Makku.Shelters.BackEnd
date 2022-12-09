using Makku.Shelters.Application.Commands.CreateShelter;
using Makku.Shelters.Application.Commands.DeleteShelter;
using Makku.Shelters.Application.Common.Exceptions;
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
                    Id = SheltersContextFactory.NoteIdForDelete,
                    UserId = SheltersContextFactory.UserAId
                }, CancellationToken.None);

            //Assert
            Assert.Null(Context.Shelters.SingleOrDefaultAsync(s => s.Id == SheltersContextFactory.NoteIdForDelete));
        }

        [Fact]
        public async Task DeleteShelterCommandHandler_FailOnWrongUserId()
        {
            //Arrange
            var deleteHandler = new DeleteShelterCommandHandler(Context);
            var createHandler = new CreateShelterCommandHandler(Context);
            var shelterId = await createHandler.Handle(
                new CreateShelterCommand
                {
                    Name = "Shelter name",
                    UserId = SheltersContextFactory.UserAId
                }, CancellationToken.None);

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await deleteHandler.Handle(new DeleteShelterCommand
            {
                Id = shelterId,
                UserId = SheltersContextFactory.UserBId
            }, CancellationToken.None));

        }

    }
}
