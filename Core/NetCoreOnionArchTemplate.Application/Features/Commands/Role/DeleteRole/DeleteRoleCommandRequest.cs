using MediatR;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.Role.DeleteRole
{
	public class DeleteRoleCommandRequest : IRequest<DeleteRoleCommandResponse>
	{
        public int Id { get; set; }
    }
}