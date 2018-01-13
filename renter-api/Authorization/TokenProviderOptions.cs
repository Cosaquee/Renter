using Microsoft.IdentityModel.Tokens;
using System;

namespace Authorization
{
    public class TokenProviderOptions
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan Expiration { get; set; }

        public SigningCredentials SigningCredentials { get; set; }

        public string CookieName { get; set; }
    }
}