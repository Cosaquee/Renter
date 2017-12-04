using Authorization;
using Renter.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Renter
{
    public partial class Startup
    {
        private void RegisterToken(IServiceCollection services)
        {

            string secretKey = Configuration["Tokens:Key"];
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var options = new TokenProviderOptions
            {
                Audience = Configuration["Tokens:Issuer"],
                Issuer = Configuration["Tokens:Issuer"],
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                Expiration = TimeSpan.FromDays(1),
                CookieName = Configuration["Tokens:CookieName"]
            };
            services.AddSingleton<IOptions<TokenProviderOptions>>(Options.Create(options));
            services.AddTransient<ITokenProvider, TokenProvider>();

            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = Configuration["Tokens:Issuer"],
                ValidateAudience = true,
                ValidAudience = Configuration["Tokens:Issuer"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            };

            services.AddAuthentication(cfg =>
                {
                    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = tokenValidationParameters;
                })
                .AddCookie(cfg =>
                {
                    cfg.Cookie.Name = Configuration["Tokens:CookieName"];
                    cfg.TicketDataFormat = new CustomJwtDataFormat(
                        SecurityAlgorithms.HmacSha256,
                        tokenValidationParameters);
                });
        }
    }
}
