using AutoMapper;
using Makku.Shelters.Application.Interfaces;
using Makku.Shelters.Application.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using System.Security.Claims;
using Makku.Shelters.Application.Common.Exceptions;
using Makku.Shelters.Domain.ShelterProfileAggregate;
using Microsoft.IdentityModel.JsonWebTokens;
using Makku.Shelters.Domain;

namespace Makku.Shelters.Application.Shelters.Identity.Commands.RegisterShelter
{
    public class RegisterShelterCommandHandler : IRequestHandler<RegisterShelterCommand, CurrentIdentityShelterVm>
    {
        private readonly ISheltersDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IdentityService _identityService;
        private readonly IMapper _mapper;

        public RegisterShelterCommandHandler(ISheltersDbContext dbContext, UserManager<IdentityUser> userManager, IdentityService identityService, IMapper mapper)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<CurrentIdentityShelterVm> Handle(RegisterShelterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await ValidateIdentityDoesNotExist(request);

                await using var transaction = await _dbContext.BeginTransactionAsync(cancellationToken);

                var identity = await CreateIdentityShelterAsync(request, transaction, cancellationToken);

                var profile = await CreateShelterProfileAsync(request, transaction, identity, cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                return new CurrentIdentityShelterVm
                {
                    UserName = request.Username,
                    ShelterName = request.ShelterName,
                    EmailAddress = request.Email,
                    Token = GetJwtString(identity, profile)
                };
            }

            catch (NotValidException ex)
            {
                throw new NotValidException($"{nameof(Shelter)} cannot register because input data is not valid. Errors: {string.Join("\n", ex.ValidationErrors)}");
            }

            catch (Exception e)
            {
                throw new Exception($"{nameof(Shelter)} cannot register. Error: {e.Message}");
            }
        }

        private async Task ValidateIdentityDoesNotExist(RegisterShelterCommand request)
        {
            var existingIdentity = await _userManager.FindByEmailAsync(request.Email);

            if (existingIdentity != null)
                throw new ConflictException("Provided email address already exists. Cannot register new user");

        }

        private async Task<IdentityUser> CreateIdentityShelterAsync(RegisterShelterCommand request,
            IDbContextTransaction transaction, CancellationToken cancellationToken)
        {
            var identity = new IdentityUser { Email = request.Email, UserName = request.Username };
            var createdIdentity = await _userManager.CreateAsync(identity, request.Password);
            if (createdIdentity.Succeeded)
                return identity;

            await transaction.RollbackAsync(cancellationToken);

            var errors = string.Empty;
            foreach (var identityError in createdIdentity.Errors)
            {
                errors += identityError.Description + "\n";
            }

            throw new NotCreatedException(nameof(Shelter), errors);
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
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new NotCreatedException(nameof(ShelterProfile), ex.Message);
            }
        }

        private string GetJwtString(IdentityUser identity, ShelterProfile profile)
        {
            var claimsIdentity = new ClaimsIdentity(new[]
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
