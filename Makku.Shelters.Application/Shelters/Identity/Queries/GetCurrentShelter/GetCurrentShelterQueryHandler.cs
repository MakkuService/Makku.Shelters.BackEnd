using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MediatR;
using Makku.Shelters.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Makku.Shelters.Application.Common.Exceptions;
using Makku.Shelters.Domain;

namespace Makku.Shelters.Application.Shelters.Identity.Queries.GetCurrentShelter
{
    public class GetCurrentShelterQueryHandler
        : IRequestHandler<GetCurrentShelterQuery,CurrentIdentityShelterVm>
    {
        private readonly ISheltersDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public GetCurrentShelterQueryHandler(ISheltersDbContext dbContext, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<CurrentIdentityShelterVm> Handle(GetCurrentShelterQuery request,
            CancellationToken cancellationToken)
        {
            var identity = await _userManager.GetUserAsync(request.ClaimsPrincipal);

            var profile = await _dbContext.ShelterProfiles
                .FirstOrDefaultAsync(up => up.ShelterProfileId == request.ShelterProfileId, cancellationToken);

            if (profile == null || profile.IdentityId != profile.IdentityId)
                throw new NotFoundException(nameof(ShelterProfile), request.ShelterProfileId);

            var result = _mapper.Map<CurrentIdentityShelterVm>(profile);
            result.UserName = identity.UserName;
            return result;
        }
    }
}
