using NetCoreOnionArchTemplate.Application.DTOs.User;

namespace NetCoreOnionArchTemplate.Application.Abstractions.Services.Authentication
{
    public interface IInternalAuthentication
    {
        Task<LoginUserResponse> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime, int addOnAccessTokenDate);
        Task<DTOs.Token> RefreshTokenLoginAsync(string refreshToken, int accessTokenLifeTime, int addOnAccessTokenDate);
    }
}
