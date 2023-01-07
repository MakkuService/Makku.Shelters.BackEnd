using Makku.Shelters.Application.Common.Exceptions;
using Makku.Shelters.Application.Interfaces;
using Makku.Shelters.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Makku.Shelters.Application.Shelters.Profile.Commands.UpdateProfile
{
    public class UpdateProfileCommandHandler: IRequestHandler<UpdateProfileCommand>
    {
        private readonly ISheltersDbContext _dbContext;
        public UpdateProfileCommandHandler(ISheltersDbContext dbContext) => _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateProfileCommand request,
            CancellationToken cancellationToken)
        {

            var identityShelter = await _dbContext.IdentityShelter(request.IdentityShelterId.ToString(), cancellationToken);

            if (identityShelter is null)
                throw new NotFoundException($"{nameof(ShelterProfile)}. Unable to find a shelter with the specified username.");

            var shelterProfile = await _dbContext.ShelterProfiles
                .FirstOrDefaultAsync(up
                    => up.IdentityId == request.IdentityShelterId.ToString(), cancellationToken);

            if (shelterProfile == null)
                throw new NotFoundException(nameof(ShelterProfile), request.IdentityShelterId.ToString());

            if (shelterProfile.IdentityId != request.RequestorGuid.ToString())
                throw new ForbiddenException($"{nameof(ShelterProfile)}. Cannot update shelterProfile as you are not it's owner.");

            shelterProfile.Description = request.Description;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
