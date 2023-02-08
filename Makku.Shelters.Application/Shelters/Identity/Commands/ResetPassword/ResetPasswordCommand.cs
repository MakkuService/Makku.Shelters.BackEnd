using MediatR;

namespace Makku.Shelters.Application.Shelters.Identity.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest<Unit>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
