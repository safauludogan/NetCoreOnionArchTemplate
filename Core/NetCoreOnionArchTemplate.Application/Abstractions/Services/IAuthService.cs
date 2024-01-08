using NetCoreOnionArchTemplate.Application.Abstractions.Services.Authantication;
using NetCoreOnionArchTemplate.Application.Abstractions.Services.Authentication;

namespace NetCoreOnionArchTemplate.Application.Abstractions.Services
{
    public interface IAuthService : IInternalAuthentication, IExternalAuthentication
    {
        Task PasswordResetAsync(string email);
		Task<bool> VerifyResetTokenAsync(string resetToken,string userId);
    }
}
