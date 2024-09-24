using NetCoreOnionArchTemplate.Application.Base;

namespace NetCoreOnionArchTemplate.Application.Features.Commands.Auth.Exceptions
{
    public class EmailAddressShouldNotBeInvalidException : BaseException
    {
        public EmailAddressShouldNotBeInvalidException() : base("Kullanıcı bulunamadı!")
        {
            
        }
    }
}
