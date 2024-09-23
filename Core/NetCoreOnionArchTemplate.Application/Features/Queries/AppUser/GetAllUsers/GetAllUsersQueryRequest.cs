using MediatR;
using NetCoreOnionArchTemplate.Application.Features.Queries.AppUser.GetAllUsers;

namespace NetCoreOnionArchTemplate.Application.Features.Queries.Auth.GetAllUsers
{
    public class GetAllUsersQueryRequest : IRequest<GetAllUsersQueryResponse>
    {
        public int Page { get; set; }
        public int Size { get; set; } = 5;
    }
}