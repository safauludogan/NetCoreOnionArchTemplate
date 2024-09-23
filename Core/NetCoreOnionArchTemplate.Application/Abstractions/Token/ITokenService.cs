using NetCoreOnionArchTemplate.Domain.Entities.Identity;

namespace NetCoreOnionArchTemplate.Application.Abstractions.Token
{
    public interface ITokenService
    {
        Task<DTOs.Token> CreateAccessToken(AppUser user);
        string CreateRefreshToken();
    }
}
