using NetCoreOnionArchTemplate.Domain.Entities.Identity;

namespace NetCoreOnionArchTemplate.Application.DTOs.User
{
    public class LoginUserResponse
    {
        public Token Token { get; set; }
        public AppUser User { get; set; }
    }
}
