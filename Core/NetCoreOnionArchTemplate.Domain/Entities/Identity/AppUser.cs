using Microsoft.AspNetCore.Identity;

namespace NetCoreOnionArchTemplate.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<string>
    {
        public string NameSurname { get; set; }
    }
}
