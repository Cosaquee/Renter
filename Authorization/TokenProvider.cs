using Database.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Authorization
{
    public class TokenProvider : ITokenProvider
    {
        private readonly TokenProviderOptions tokenProviderOptions;
        private readonly IUserRepositoryService userRepositoryService;
        public TokenProvider(IOptions<TokenProviderOptions> options, IUserRepositoryService userRepositoryService)
        {
            this.tokenProviderOptions = options.Value;
            this.userRepositoryService = userRepositoryService;
        }

        public async Task<AuthToken> GenerateToken(string userName, string password)
        {
            var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var identity = await GetIdentity(userName, password, now);
            if (identity == null)
            {
                return null;
            }

            // Create the JWT and write it to a string
            var nowDate = DateTimeOffset.FromUnixTimeSeconds(now).UtcDateTime;
            var jwt = new JwtSecurityToken(
                issuer: tokenProviderOptions.Issuer,
                audience: tokenProviderOptions.Audience,
                claims: identity.Claims,
                notBefore: nowDate,
                expires: nowDate.Add(tokenProviderOptions.Expiration),
                signingCredentials: tokenProviderOptions.SigningCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new AuthToken
            {
                AccessToken = encodedJwt,
                ExpiresIn = tokenProviderOptions.Expiration.TotalSeconds
            };
        }

        private async Task<ClaimsIdentity> GetIdentity(string userName, string password, long now)
        {
            var user = await userRepositoryService.FindUserAsync(userName, password);
            if(user != null)
            {
                return new ClaimsIdentity(new GenericIdentity(user.UserName, "Token"), new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Role, user.Role.Name),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, now.ToString(), ClaimValueTypes.Integer64)
                });
            }

            return null;
        }
    }

}


