﻿using MediatR;
using NetCoreOnionArchTemplate.Application.Abstractions.Services;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.Role.UpdateRole
{
	public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommandRequest, UpdateRoleCommandResponse>
	{

		private readonly IRoleService _roleService;

		public UpdateRoleCommandHandler(IRoleService roleService)
		{
			_roleService = roleService;
		}

		public async Task<UpdateRoleCommandResponse> Handle(UpdateRoleCommandRequest request, CancellationToken cancellationToken)
		{
			var result = await _roleService.UpdateRoleAsync(request.Id,request.Name);
			return new()
			{
				Succeeded = result
			};
		}
	}
}
