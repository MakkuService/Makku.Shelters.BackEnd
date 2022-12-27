using AutoMapper;
using Makku.Shelters.Application.Enums;
using Makku.Shelters.Application.Interfaces;
using Makku.Shelters.Application.Models;
using Makku.Shelters.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using System.Security.Claims;
using Makku.Shelters.Application.Identity.Dtos;
using Makku.Shelters.Domain.Exceptions;
using Makku.Shelters.Domain.ShelterProfileAggregate;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Makku.Shelters.Application.Identity.Commands.RegisterShelter
{
    public class RegisterShelterCommandHandler : IRequestHandler<RegisterShelterCommand, OperationResult<IdentityShelterProfileDto>>
    {
        private readonly ISheltersDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IdentityService _identityService;
        private OperationResult<IdentityShelterProfileDto> _result = new();
        private readonly IMapper _mapper;

        public RegisterShelterCommandHandler(ISheltersDbContext dbContext, UserManager<IdentityUser> userManager, IdentityService identityService, IMapper mapper)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<OperationResult<IdentityShelterProfileDto>> Handle(RegisterShelterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await ValidateIdentityDoesNotExist(request);
                if (_result.IsError) return _result;

                await using var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);

                var identity = await CreateIdentityShelterAsync(request, transaction, cancellationToken);
                if (_result.IsError) return _result;

                var profile = await CreateShelterProfileAsync(request, transaction, identity, cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                _result.Payload = _mapper.Map<IdentityShelterProfileDto>(profile);
                _result.Payload.UserName = identity.UserName;
                _result.Payload.Token = GetJwtString(identity, profile);
                return _result;
            }

            catch (ShelterProfileNotValidException ex)
            {
                ex.ValidationErrors.ForEach(e => _result.AddError(ErrorCode.ValidationError, e));
            }

            catch (Exception e)
            {
                _result.AddUnknownError(e.Message);
            }

            return _result;
        }

        private async Task ValidateIdentityDoesNotExist(RegisterShelterCommand request)
        {
            var existingIdentity = await _userManager.FindByEmailAsync(request.Email);

            if (existingIdentity != null)
                _result.AddError(ErrorCode.IdentityUserAlreadyExists, IdentityErrorMessages.IdentityUserAlreadyExists);

        }

        private async Task<IdentityUser> CreateIdentityShelterAsync(RegisterShelterCommand request,
            IDbContextTransaction transaction, CancellationToken cancellationToken)
        {
            var identity = new IdentityUser { Email = request.Email, UserName = request.Username };
            var createdIdentity = await _userManager.CreateAsync(identity, request.Password);
            if (createdIdentity.Succeeded) 
                return identity;
            
            await transaction.RollbackAsync(cancellationToken);

            foreach (var identityError in createdIdentity.Errors)
            {
                _result.AddError(ErrorCode.IdentityCreationFailed, identityError.Description);
            }
            return identity;
        }

        private async Task<ShelterProfile> CreateShelterProfileAsync(RegisterShelterCommand request,
            IDbContextTransaction transaction, IdentityUser identity,
            CancellationToken cancellationToken)
        {
            try
            {
                var profileInfo = BasicInfo.CreateBasicInfo(request.Email, request.ShelterName);

                var profile = ShelterProfile.CreateShelterProfile(identity.Id, profileInfo);
                _dbContext.ShelterProfiles.Add(profile);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return profile;
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        private string GetJwtString(IdentityUser identity, ShelterProfile profile)
        {
            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, identity.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, identity.Email),
            new Claim("IdentityId", identity.Id),
            new Claim("ShelterProfileId", profile.ShelterProfileId.ToString())
            });

            var token = _identityService.CreateSecurityToken(claimsIdentity);
            return _identityService.WriteToken(token);
        }
    }
}
