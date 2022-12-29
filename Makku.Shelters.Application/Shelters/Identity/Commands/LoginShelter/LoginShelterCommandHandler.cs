using System.Security.Claims;
using System.Security.Principal;
using AutoMapper;
using Makku.Shelters.Application.Common.Exceptions;
using Makku.Shelters.Application.Interfaces;
using Makku.Shelters.Application.Services;
using Makku.Shelters.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Makku.Shelters.Application.Shelters.Identity.Commands.LoginShelter
{
    public class LoginShelterCommandHandler : IRequestHandler<LoginShelterCommand, CurrentIdentityShelterVm>
    {
        private readonly ISheltersDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IdentityService _identityService;
        private readonly IMapper _mapper;

        public LoginShelterCommandHandler(ISheltersDbContext dbContext, UserManager<IdentityUser> userManager, IdentityService identityService, IMapper mapper)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _identityService = identityService;
            _mapper = mapper;
        }
        public async Task<CurrentIdentityShelterVm> Handle(LoginShelterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var identityUser = await ValidateAndGetIdentityAsync(request);

                var userProfile = await _dbContext.ShelterProfiles
                    .FirstOrDefaultAsync(up => up.IdentityId == identityUser.Id, cancellationToken:
                        cancellationToken);

                return new CurrentIdentityShelterVm
                {
                    UserName = identityUser.UserName,
                    ShelterName = userProfile.BasicInfo.ShelterName,
                    EmailAddress = request.Email,
                    Token = GetJwtString(identityUser, userProfile)
                };
            }
            catch (Exception e)
            {
                throw new Exception($"{nameof(Shelter)} cannot login.", e);
            }
        }

        private async Task<IdentityUser> ValidateAndGetIdentityAsync(LoginShelterCommand request)
        {
            var identityUser = await _userManager.FindByEmailAsync(request.Email);

            if (identityUser is null)
                throw new NotFoundException($"{nameof(Shelter)}. Unable to find a user with the specified username.");

            var validPassword = await _userManager.CheckPasswordAsync(identityUser, request.Password);

            if (!validPassword)
                throw new ForbiddenException($"{nameof(Shelter)}. The provided password is incorrect.");

            return identityUser;
        }

        private string GetJwtString(IdentityUser identityUser, Domain.ShelterProfileAggregate.ShelterProfile userProfile)
        {
            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, identityUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, identityUser.Email),
                new Claim("IdentityId", identityUser.Id),
                new Claim("ShelterProfileId", userProfile.ShelterProfileId.ToString())
            });

            var token = _identityService.CreateSecurityToken(claimsIdentity);
            return _identityService.WriteToken(token);
        }
    }
}
