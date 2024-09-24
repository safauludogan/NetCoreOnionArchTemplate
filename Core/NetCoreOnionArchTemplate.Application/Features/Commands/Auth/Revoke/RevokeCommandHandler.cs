using MediatR;
using Microsoft.AspNetCore.Identity;
using NetCoreOnionArchTemplate.Application.Abstractions.Services;
using NetCoreOnionArchTemplate.Application.Features.Commands.Auth.Rules;
using NetCoreOnionArchTemplate.Domain.Entities.Identity;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.Auth.Revoke
{
    public class RevokeCommandHandler : IRequestHandler<RevokeCommandRequest, Unit>
    {
        private readonly AuthRules _authRules;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuthService _authService;
        public RevokeCommandHandler(AuthRules authRules, UserManager<AppUser> userManager, IAuthService authService)
        {
            _authRules = authRules;
            _userManager = userManager;
            _authService = authService;
        }

        public async Task<Unit> Handle(RevokeCommandRequest request, CancellationToken cancellationToken)
        {
            /// Kullanıcının refreshToken'ını silerek logout yaptırıyoruz.
            AppUser? user = await _userManager.FindByEmailAsync(request.Email);
            
            await _authRules.EmailAddressShouldNotBeInvalid(user);
          
            await _authService.RevokeAsync(user!);
            
            return Unit.Value;
        }
    }
}
