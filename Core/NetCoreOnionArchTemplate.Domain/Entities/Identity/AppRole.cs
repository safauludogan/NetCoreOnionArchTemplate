﻿using Microsoft.AspNetCore.Identity;

namespace NetCoreOnionArchTemplate.Domain.Entities.Identity
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<Endpoint> Endpoints { get; set; }
    }
}
