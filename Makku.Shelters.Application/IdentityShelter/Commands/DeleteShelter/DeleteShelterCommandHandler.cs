using Makku.Shelters.Application.Enums;
using Makku.Shelters.Application.Identity;
using Makku.Shelters.Application.Interfaces;
using Makku.Shelters.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Makku.Shelters.Application.IdentityShelter.Commands.DeleteShelter
{
    public class DeleteShelterCommandHandler : IRequestHandler<DeleteShelterCommand, OperationResult<bool>>
    {
        private readonly ISheltersDbContext _dbContext;

        public DeleteShelterCommandHandler(ISheltersDbContext dbContext) => _dbContext = dbContext;

        public async Task<OperationResult<bool>> Handle(DeleteShelterCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<bool>();

            try
            {
                var identityShelter = await _dbContext.IdentityShelter(request.IdentityShelterId.ToString(), cancellationToken);

                if (identityShelter == null)
                {
                    result.AddError(ErrorCode.IdentityUserDoesNotExist,
                        IdentityErrorMessages.NonExistentIdentityUser);
                    return result;
                }

                var shelterProfile = await _dbContext.ShelterProfiles
                    .FirstOrDefaultAsync(up
                        => up.IdentityId == request.IdentityShelterId.ToString(), cancellationToken);

                if (shelterProfile == null)
                {
                    result.AddError(ErrorCode.NotFound, "No UserProfile found with ID {0}");
                    return result;
                }

                if (shelterProfile.IdentityId != request.RequestorGuid.ToString())
                {
                    result.AddError(ErrorCode.UnauthorizedAccountRemoval,
                        IdentityErrorMessages.UnauthorizedAccountRemoval);

                    return result;
                }

                _dbContext.ShelterProfiles.Remove(shelterProfile);
                _dbContext.DeleteIdentityShelter(identityShelter);
                await _dbContext.SaveChangesAsync(cancellationToken);

                result.Payload = true;
            }
            catch (Exception e)
            {
                result.AddUnknownError(e.Message);
            }

            return result;
        }
    }
}
