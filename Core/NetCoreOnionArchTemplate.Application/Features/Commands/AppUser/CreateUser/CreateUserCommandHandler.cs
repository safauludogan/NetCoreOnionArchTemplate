using MediatR;
using Microsoft.AspNetCore.Identity;
using NetCoreOnionArchTemplate.Application.Exceptions;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {

            IdentityResult result = await _userManager.CreateAsync(new()
            {
                UserName = request.UserName,
                NameSurname = request.NameSurname,
                Email = request.Email,
            }, request.Password);

            CreateUserCommandResponse response = new() { IsSuccess = result.Succeeded };
            if (result.Succeeded)
                response.Message = "User Created";
            foreach (var error in result.Errors)
            {
                response.Message += $"{error.Code} - {error.Description}\n";
            }
            return response;
        }
    }
}
