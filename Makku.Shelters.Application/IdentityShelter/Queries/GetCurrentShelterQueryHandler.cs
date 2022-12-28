using AutoMapper;
using Makku.Shelters.Application.Models;
using Microsoft.AspNetCore.Identity;
using MediatR;
using Makku.Shelters.Application.IdentityShelter.Dtos;
using Makku.Shelters.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Makku.Shelters.Application.IdentityShelter.Queries
{
    public class GetCurrentShelterQueryHandler
        : IRequestHandler<GetCurrentShelterQuery, OperationResult<IdentityShelterProfileVm>>
    {
        private readonly ISheltersDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private OperationResult<IdentityShelterProfileVm> _result = new();
        private readonly IMapper _mapper;

        public GetCurrentShelterQueryHandler(ISheltersDbContext dbContext, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<OperationResult<IdentityShelterProfileVm>> Handle(GetCurrentShelterQuery request,
            CancellationToken cancellationToken)
        {
            var identity = await _userManager.GetUserAsync(request.ClaimsPrincipal);

            var profile = await _dbContext.ShelterProfiles
                .FirstOrDefaultAsync(up => up.ShelterProfileId == request.ShelterProfileId, cancellationToken);

            _result.Payload = _mapper.Map<IdentityShelterProfileVm>(profile);
            _result.Payload.UserName = identity.UserName;
            return _result;
        }
    }
}
