using System.Security.Claims;
using AutoMapper;
using Makku.Shelters.Application.Enums;
using Makku.Shelters.Application.Identity.Dtos;
using Makku.Shelters.Application.Interfaces;
using Makku.Shelters.Application.Models;
using Makku.Shelters.Application.Services;
using Makku.Shelters.Domain.ShelterProfileAggregate;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Makku.Shelters.Application.Identity.Commands.LoginShelter
{
    public class LoginShelterCommandHandler : IRequestHandler<LoginShelterCommand, OperationResult<IdentityShelterProfileDto>>
    {
        private readonly ISheltersDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IdentityService _identityService;
        private OperationResult<IdentityShelterProfileDto> _result = new();
        private readonly IMapper _mapper;

        public LoginShelterCommandHandler(ISheltersDbContext dbContext, UserManager<IdentityUser> userManager, IdentityService identityService, IMapper mapper)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _identityService = identityService;
            _mapper = mapper;
        }
        public async Task<OperationResult<IdentityShelterProfileDto>> Handle(LoginShelterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var identityUser = await ValidateAndGetIdentityAsync(request);
                if (_result.IsError) return _result;

                var userProfile = await _dbContext.ShelterProfiles
                    .FirstOrDefaultAsync(up => up.IdentityId == identityUser.Id, cancellationToken:
                        cancellationToken);


                _result.Payload = _mapper.Map<IdentityShelterProfileDto>(userProfile);
                _result.Payload.UserName = identityUser.UserName;
                _result.Payload.Token = GetJwtString(identityUser, userProfile);
                return _result;

            }
            catch (Exception e)
            {
                _result.AddUnknownError(e.Message);
            }

            return _result;
        }

        private async Task<IdentityUser> ValidateAndGetIdentityAsync(LoginShelterCommand request)
        {
            var identityUser = await _userManager.FindByNameAsync(request.Username);

            if (identityUser is null)
                _result.AddError(ErrorCode.IdentityUserDoesNotExist,
                    IdentityErrorMessages.NonExistentIdentityUser);

            var validPassword = await _userManager.CheckPasswordAsync(identityUser, request.Password);

            if (!validPassword)
                _result.AddError(ErrorCode.IncorrectPassword, IdentityErrorMessages.IncorrectPassword);

            return identityUser;
        }

        private string GetJwtString(IdentityUser identityUser, ShelterProfile userProfile)
        {
            var claimsIdentity = new ClaimsIdentity(new Claim[]
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
