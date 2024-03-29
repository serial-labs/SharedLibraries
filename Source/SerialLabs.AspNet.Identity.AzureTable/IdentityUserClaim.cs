﻿using System.Security.Claims;

namespace SerialLabs.AspNet.Identity.AzureTable
{
    /// <summary>
    /// Encapsulates a claim logic adn provide a serializable constructor
    /// </summary>
    public class IdentityUserClaim
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public IdentityUserClaim()
        { }

        public IdentityUserClaim(Claim claim)
        {
            ClaimType = claim.Type;
            ClaimValue = claim.Value;
        }

        public IdentityUserClaim(string type, string value)
        {
            ClaimType = type;
            ClaimValue = value;
        }
    }
}
