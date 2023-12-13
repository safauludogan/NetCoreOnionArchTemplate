using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace NetCoreOnionArchTemplate.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public string NameSurname { get; set; }
    }
}
