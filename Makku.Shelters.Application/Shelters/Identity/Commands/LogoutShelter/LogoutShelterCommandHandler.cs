using System.Security.Claims;
using Makku.Shelters.Application.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Makku.Shelters.Application.Shelters.Identity.Commands.LogoutShelter
{
    public class LogoutShelterCommandHandler : IRequestHandler<LogoutShelterCommand>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public LogoutShelterCommandHandler(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Unit> Handle(LogoutShelterCommand request, CancellationToken cancellationToken)
        {
            var identityUser = await _userManager.GetUserAsync(request.ClaimsPrincipal);

            if (identityUser == null)
                throw new NotFoundException(nameof(IdentityUser), request.ShelterProfileId.ToString());
            
            await _signInManager.SignOutAsync();

            var result = await _userManager.UpdateSecurityStampAsync(identityUser);
            if (!result.Succeeded)
                throw new InvalidOperationException("Unable to revoke access token.");

            return Unit.Value;
        }
    }
}
