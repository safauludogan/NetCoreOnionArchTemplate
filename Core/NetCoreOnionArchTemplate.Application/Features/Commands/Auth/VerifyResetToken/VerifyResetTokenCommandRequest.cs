using MediatR;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.Auth.VerifyResetToken
{
	public class VerifyResetTokenCommandRequest : IRequest<VerifyResetTokenCommandResponse>
	{
        public string ResetToken { get; set; }
        public string UserId { get; set; }
    }
}