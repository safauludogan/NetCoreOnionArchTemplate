using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace NetCoreOnionArchTemplate.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public string NameSurname { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
    }
}
