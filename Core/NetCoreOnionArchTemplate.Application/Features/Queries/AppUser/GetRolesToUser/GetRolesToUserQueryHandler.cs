using MediatR;
using NetCoreOnionArchTemplate.Application.Abstractions.Services;

namespace NetCoreOnionArchTemplate.Application.Features.Queries.AppUser.GetRolesToUser
{
	public class GetRolesToUserQueryHandler : IRequestHandler<GetRolesToUserQueryRequest, GetRolesToUserQueryResponse>
	{
		private readonly IUserService _userService;

		public GetRolesToUserQueryHandler(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<GetRolesToUserQueryResponse> Handle(GetRolesToUserQueryRequest request, CancellationToken cancellationToken)
		{
			var userRoles = await _userService.GetRolesToUser(request.userId);
			return new GetRolesToUserQueryResponse() { Roles = userRoles };
		}
	}
}
