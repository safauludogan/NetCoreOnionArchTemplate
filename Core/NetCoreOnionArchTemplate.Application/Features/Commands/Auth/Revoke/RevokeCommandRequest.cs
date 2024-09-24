using MediatR;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.Auth.Revoke
{
    public class RevokeCommandRequest : IRequest<Unit>
    {
        public string Email { get; set; }
    }
}
