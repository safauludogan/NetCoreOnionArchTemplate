﻿using MediatR;
using NetCoreOnionArchTemplate.Application.Abstractions.Services;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.AppUser.AssignRoleToUser
{
	public class AssignRoleToUserCommandHandler : IRequestHandler<AssignRoleToUserCommandRequest, AssignRoleToUserCommandResponse>
	{
		readonly IUserService _userService;

		public AssignRoleToUserCommandHandler(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<AssignRoleToUserCommandResponse> Handle(AssignRoleToUserCommandRequest request, CancellationToken cancellationToken)
		{
			await _userService.AssignRoleToUserAsync(request.UserId, request.Roles);
			return new();
		}
	}
}