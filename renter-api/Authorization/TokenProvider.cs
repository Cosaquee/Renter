using Database.Interfaces;
using Microsoft.Extensions.Options;
using Models.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Authorization
{
    public class TokenProvider : ITokenProvider
    {
        private readonly TokenProviderOptions tokenProviderOptions;
        private readonly IUserRepositoryService userRepositoryService;
        private readonly IRefreshTokenRepositoryService refreshTokenRepositoryService;
        private readonly IUnitOfWork unitOfWork;

        private User user;
        private DateTime nowDateTime;
        private long nowSeconds;
        private DateTime expireDateTime;

        public TokenProvider(IOptions<TokenProviderOptions> options, IUserRepositoryService userRepositoryService, IRefreshTokenRepositoryService refreshTokenRepositoryService, IUnitOfWork unitOfWork)
        {
            this.tokenProviderOptions = options.Value;
            this.userRepositoryService = userRepositoryService;
            this.refreshTokenRepositoryService = refreshTokenRepositoryService;
            this.unitOfWork = unitOfWork;
        }

        public AuthToken GenerateToken(string userName, string password)
        {
            //get user
            user = userRepositoryService.LoginByEmail(userName, password);
            if (user == null)
            {
                return null;
            }
            return CreateToken();
        }

        public AuthToken RefreshToken(string refreshTokenId)
        {
            var refreshToken = refreshTokenRepositoryService.Get(refreshTokenId);
            if (refreshToken == null)
            {
                return null;
            }

            if (refreshToken.Expire < DateTime.UtcNow)
            {
                return null;
            }

            user = userRepositoryService.GetWithRole(refreshToken.UserId);
            if (user == null)
            {
                return null;
            }

            return CreateToken();
        }

        private AuthToken CreateToken()
        {
            SetupTime();
            //token id
            string jwtID = Guid.NewGuid().ToString() + '-' + nowSeconds;

            //Get user identity
            var claimsIdentity = GetIdentity(jwtID);

            //create token
            var encodedJwt = CreateJWTToken(claimsIdentity);
            //create refresh token
            var refreshToken = CreateRefreshToken();

            //return ready auth token
            return new AuthToken
            {
                AccessToken = encodedJwt,
                ExpiresIn = tokenProviderOptions.Expiration.TotalSeconds,
                RefreshToken = refreshToken,
                CookieName = tokenProviderOptions.CookieName
            };
        }

        private void SetupTime()
        {
            //setup datetime
            nowDateTime = DateTimeOffset.UtcNow.DateTime;
            nowSeconds = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            expireDateTime = nowDateTime.Add(tokenProviderOptions.Expiration);
        }

        private string CreateJWTToken(ClaimsIdentity claimsIdentity)
        {
            // Create the JWT and write it to a string
            var jwt = new JwtSecurityToken(
                issuer: tokenProviderOptions.Issuer,
                audience: tokenProviderOptions.Audience,
                claims: claimsIdentity.Claims,
                notBefore: nowDateTime,
                expires: expireDateTime,
                signingCredentials: tokenProviderOptions.SigningCredentials);
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private ClaimsIdentity GetIdentity(string jwtID)
        {
            //user claims need for auth setup
            return new ClaimsIdentity(new GenericIdentity(user.Email, "Token"), new Claim[]
            {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Role, user.Role.Name),
                    new Claim(JwtRegisteredClaimNames.Jti, jwtID),
                    new Claim(JwtRegisteredClaimNames.Iat, nowSeconds.ToString(), ClaimValueTypes.Integer64)
            });
        }

        private string CreateRefreshToken()
        {
            //Create unique token id
            string refrshTokenId = Guid.NewGuid().ToString() + '-' + nowSeconds;

            //Remove from db user previous refresh tokens
            refreshTokenRepositoryService.RemoveRefreshTokenForUser(user.Id);

            //Add user refresh token
            refreshTokenRepositoryService.Insert(new RefreshToken
            {
                Id = refrshTokenId,
                UserId = user.Id,
                Expire = expireDateTime
            });

            //Save changes in db
            unitOfWork.Save();

            return refrshTokenId;
        }
    }
}