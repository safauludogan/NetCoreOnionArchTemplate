using MediatR;
using NetCoreOnionArchTemplate.Application.Abstractions.Services;
using NetCoreOnionArchTemplate.Application.DTOs.User;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.Auth.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly IAuthService _authService;
        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            LoginUserResponse response = await _authService.LoginAsync(request.UsernameOrEmail, request.Password);
            return new LoginUserSuccessCommandResponse()
            {
                Response = response
            };
        }
    }
}
