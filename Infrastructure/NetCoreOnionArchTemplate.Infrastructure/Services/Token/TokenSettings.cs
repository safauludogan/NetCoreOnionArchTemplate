
namespace NetCoreOnionArchTemplate.Infrastructure.Services.Token
{
    public class TokenSettings
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string SecurityKey { get; set; }
        public int TokenValidityInMinutes { get; set; }
        public int RefreshTokenValidityInDays { get; set; }
    }
}
