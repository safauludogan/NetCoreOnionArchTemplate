using NetCoreOnionArchTemplate.Application.DTOs.User;

namespace NetCoreOnionArchTemplate.Application.Abstractions.Services.Authentication
{
    public interface IInternalAuthentication
    {
        Task<LoginUserResponse> LoginAsync(string usernameOrEmail, string password);
        Task<DTOs.Token> RefreshTokenLoginAsync(string refreshToken);
    }
}
