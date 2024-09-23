using NetCoreOnionArchTemplate.Application.Base;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.Auth.Exceptions
{
    public class EmailAndUsernameOrPasswordShouldNotBeInvalidException : BaseException
    {
        public EmailAndUsernameOrPasswordShouldNotBeInvalidException() : base("Kullanıcı adı veya şifre hatalı!")
        {

        }
    }
}
