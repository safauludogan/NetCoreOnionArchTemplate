using NetCoreOnionArchTemplate.Application.Base;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.Auth.Exceptions
{
    public class UserAlreadyExistsException : BaseException
    {
        public UserAlreadyExistsException() : base("Böyle bir kullanıcı zaten var!") { }
    }
}
