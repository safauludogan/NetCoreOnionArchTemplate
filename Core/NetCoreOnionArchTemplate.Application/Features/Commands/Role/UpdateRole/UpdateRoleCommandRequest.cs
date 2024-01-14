using MediatR;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.Role.UpdateRole
{
	public class UpdateRoleCommandRequest : IRequest<UpdateRoleCommandResponse>
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
}