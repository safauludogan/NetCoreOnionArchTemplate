using MediatR;
using Microsoft.AspNetCore.Identity;
using NetCoreOnionArchTemplate.Application.Abstractions.AutoMapper;
using NetCoreOnionArchTemplate.Application.Abstractions.Services;
using NetCoreOnionArchTemplate.Application.DTOs.User;
using NetCoreOnionArchTemplate.Application.Features.Commands.Auth.Rules;
using NetCoreOnionArchTemplate.Domain.Entities.Identity;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.Auth.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly AuthRules _authRules;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserService _userService;

        public CreateUserCommandHandler(AuthRules authRules, IUserService userService, UserManager<AppUser> userManager, IMapper mapper)
        {
            _authRules = authRules;
            _userService = userService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null) user = await _userManager.FindByNameAsync(request.UserName);

            await _authRules.UserShouldNotBeExists(user);

            CreateUserDto createUser = _mapper.Map<CreateUserDto, CreateUserCommandRequest>(request);
            CreateUserResponse response = await _userService.CreateAsync(createUser);
            return new()
            {
                Message = response.Message,
                IsSuccess = response.IsSuccess,
            };
        }
    }
}
