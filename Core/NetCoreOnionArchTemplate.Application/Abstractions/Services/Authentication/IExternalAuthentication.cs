namespace NetCoreOnionArchTemplate.Application.Abstractions.Services.Authantication
{
    public interface IExternalAuthentication
    {
        Task<DTOs.Token> FacebookLoginAsync(string authToken);
        Task<DTOs.Token> GoogleLoginAsync(string idToken);
    }
}
