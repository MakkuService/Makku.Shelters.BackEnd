using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Makku.Shelters.Application.Shelters.Identity.Commands.LogoutShelter
{
    public class LogoutShelterCommandHandler : IRequestHandler<LogoutShelterCommand>
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LogoutShelterCommandHandler(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<Unit> Handle(LogoutShelterCommand request, CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync();
            return Unit.Value;
        }
    }
}
