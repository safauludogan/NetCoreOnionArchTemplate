using MediatR;

namespace NetCoreOnionArchTemplate.Application.Features.Queries.Role.GetRoles
{
	public class GetAllRolesQueryRequest : IRequest<GetAllRolesQueryResponse>
	{
		public int Page { get; set; } = 0;
		public int Size { get; set; } = 5;
    }
}