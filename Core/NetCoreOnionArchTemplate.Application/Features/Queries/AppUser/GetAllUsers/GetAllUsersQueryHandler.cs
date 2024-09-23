using MediatR;
using NetCoreOnionArchTemplate.Application.Abstractions.Services;
using NetCoreOnionArchTemplate.Application.Features.Queries.Auth.GetAllUsers;

namespace NetCoreOnionArchTemplate.Application.Features.Queries.AppUser.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQueryRequest, GetAllUsersQueryResponse>
    {
        private readonly IUserService _userService;

        public GetAllUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GetAllUsersQueryResponse> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
        {
            var users = await _userService.GetAllUsers(request.Page, request.Size);

            return new GetAllUsersQueryResponse()
            {
                Users = users,
                TotalUsersCount = _userService.TotalUsersCount
            };
        }
    }
}
