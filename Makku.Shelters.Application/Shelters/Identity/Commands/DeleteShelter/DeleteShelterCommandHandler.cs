using Makku.Shelters.Application.Common.Exceptions;
using Makku.Shelters.Application.Interfaces;
using Makku.Shelters.Domain;
using Makku.Shelters.Domain.ShelterProfileAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Makku.Shelters.Application.Shelters.Identity.Commands.DeleteShelter
{
    public class DeleteShelterCommandHandler : IRequestHandler<DeleteShelterCommand>
    {
        private readonly ISheltersDbContext _dbContext;

        public DeleteShelterCommandHandler(ISheltersDbContext dbContext) => _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteShelterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var identityShelter = await _dbContext.IdentityShelter(request.IdentityShelterId.ToString(), cancellationToken);

                if (identityShelter is null)
                    throw new NotFoundException($"{nameof(Shelter)}. Unable to find a shelter with the specified username.");

                var shelterProfile = await _dbContext.ShelterProfiles
                    .FirstOrDefaultAsync(up
                        => up.IdentityId == request.IdentityShelterId.ToString(), cancellationToken);

                if (shelterProfile == null)
                    throw new NotFoundException(nameof(ShelterProfile), request.IdentityShelterId.ToString());

                if (shelterProfile.IdentityId != request.RequestorGuid.ToString())
                    throw new ForbiddenException($"{nameof(Shelter)}. Cannot remove account as you are not its owner.");

                _dbContext.ShelterProfiles.Remove(shelterProfile);
                _dbContext.DeleteIdentityShelter(identityShelter);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new Exception($"{nameof(Shelter)} cannot delete.", e);
            }
        }
    }
}
