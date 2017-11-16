using Database.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
            var identity = await GetIdentity(userName, password);
            if (string.IsNullOrEmpty(identity))
            {
                return null;
            }

            var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, identity),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToString(), ClaimValueTypes.Integer64)
            };

            // Create the JWT and write it to a string
            var nowDate = DateTimeOffset.FromUnixTimeSeconds(now).UtcDateTime;
            var jwt = new JwtSecurityToken(
                issuer: tokenProviderOptions.Issuer,
                audience: tokenProviderOptions.Audience,
                claims: claims,
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

        private Task<string> GetIdentity(string userName, string password)
        {
            return userRepositoryService.GetUserIdAsync(userName, password);
            //if (userName == "asd" && password == "asd")
            //{
            //    return Task.FromResult("userID");
            //}
            //else
            //{
            //    return Task.FromResult(default(string));
            //}
        }
    }

}


