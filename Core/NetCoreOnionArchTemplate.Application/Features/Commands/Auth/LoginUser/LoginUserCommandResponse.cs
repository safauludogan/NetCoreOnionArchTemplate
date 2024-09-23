using NetCoreOnionArchTemplate.Application.DTOs;
using NetCoreOnionArchTemplate.Application.DTOs.User;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.Auth.LoginUser
{
    public class LoginUserCommandResponse
    {
    }

    public class LoginUserSuccessCommandResponse : LoginUserCommandResponse
    {
        public LoginUserResponse Response { get; set; }

    }
    public class LoginUserErrorCommandResponse : LoginUserCommandResponse
    {
        public string Message { get; set; }
    }
}
