using MediatR;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.Auth.PasswordReset
{
	public class PasswordResetCommandRequest : IRequest<PasswordResetCommandResponse>
	{
        public string Email { get; set; }
    }
}