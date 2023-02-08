using Makku.Shelters.Application.Common.Exceptions;
using Makku.Shelters.Application.Interfaces;
using Makku.Shelters.Application.Shelters.Identity.Commands.LoginShelter;
using Makku.Shelters.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Makku.Shelters.Application.Shelters.Identity.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Unit>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ISheltersDbContext _dbContext;


        public ResetPasswordCommandHandler(UserManager<IdentityUser> userManager, ISheltersDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var identityUser = await ValidateAndGetIdentityAsync(request);

            var shelterProfile = await _dbContext.ShelterProfiles
                .FirstOrDefaultAsync(up => up.IdentityId == identityUser.Id, cancellationToken:
                    cancellationToken);

            if (shelterProfile == null)
                throw new NotFoundException(nameof(shelterProfile), identityUser.UserName);


            var result = await _userManager.ResetPasswordAsync(identityUser, request.Token, request.Password);
            
            if (!result.Succeeded)
                throw new InvalidOperationException("Failed to reset password.");

            return Unit.Value;
        }

        private async Task<IdentityUser> ValidateAndGetIdentityAsync(ResetPasswordCommand request)
        {
            var identityUser = await _userManager.FindByEmailAsync(request.Email);

            if (identityUser is null)
                throw new NotFoundException($"{nameof(ShelterProfile)}. Unable to find a user with the specified username.");

            var validPassword = await _userManager.CheckPasswordAsync(identityUser, request.Password);

            if (!validPassword)
                throw new ForbiddenException($"{nameof(ShelterProfile)}. The provided password is incorrect.");

            return identityUser;
        }
    }
}
