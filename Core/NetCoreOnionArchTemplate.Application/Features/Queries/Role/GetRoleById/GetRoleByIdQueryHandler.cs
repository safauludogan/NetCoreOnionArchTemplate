using MediatR;
using NetCoreOnionArchTemplate.Application.Abstractions.Services;

namespace NetCoreOnionArchTemplate.Application.Features.Queries.Role.GetRoleById
{
	public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQueryRequest, GetRoleByIdQueryResponse>
	{

		private readonly IRoleService _roleService;

		public GetRoleByIdQueryHandler(IRoleService roleService)
		{
			_roleService = roleService;
		}

		public async Task<GetRoleByIdQueryResponse> Handle(GetRoleByIdQueryRequest request, CancellationToken cancellationToken)
		{
			var role = await _roleService.GetRoleByIdAsync(request.Id);
			return new()
			{
				Id = role.id,
				Name = role.name
			};
		}
	}
}
