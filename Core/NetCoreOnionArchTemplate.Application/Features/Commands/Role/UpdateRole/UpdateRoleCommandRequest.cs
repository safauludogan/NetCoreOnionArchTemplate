﻿using MediatR;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.Role.UpdateRole
{
	public class UpdateRoleCommandRequest : IRequest<UpdateRoleCommandResponse>
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
	}
}