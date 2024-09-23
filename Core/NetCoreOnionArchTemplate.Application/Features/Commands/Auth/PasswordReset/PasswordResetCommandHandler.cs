using MediatR;
using NetCoreOnionArchTemplate.Application.Abstractions.Services;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.Auth.PasswordReset
{
	public class PasswordResetCommandHandler : IRequestHandler<PasswordResetCommandRequest, PasswordResetCommandResponse>
	{
		private readonly IAuthService _authService;

		public PasswordResetCommandHandler(IAuthService authService)
		{
			_authService = authService;
		}

		public async Task<PasswordResetCommandResponse> Handle(PasswordResetCommandRequest request, CancellationToken cancellationToken)
		{
			await _authService.PasswordResetAsync(request.Email);
			return new();
		}
	}
}
