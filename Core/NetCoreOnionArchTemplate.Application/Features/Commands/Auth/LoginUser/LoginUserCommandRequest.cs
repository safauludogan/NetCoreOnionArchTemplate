using MediatR;
using System.ComponentModel;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.Auth.LoginUser
{
    public class LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
    {
        [DefaultValue("safa")]
        public string UsernameOrEmail { get; set; }
        [DefaultValue("123123")]
        public string Password { get; set; }
    }
}
