using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Database;
using Microsoft.EntityFrameworkCore;
using Database.Interfaces;
using Database.Services;
using Swashbuckle.AspNetCore.Swagger;
using AutoMapper;
using Models.AutoMapperProfiles;
using Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Services.UserServices;
using Services.UserServices.Interfaces;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace HelloWorld
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Mapper.Initialize(cfg =>
                cfg.AddProfiles(new[] {
                    typeof(DtosProfile)
                })
            );
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            RegisterDatebase(services);
            RegisterToken(services);
            RegisterUserServices(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Cms Rental API", Version = "v1"});
                c.DescribeAllParametersInCamelCase();
                c.DescribeStringEnumsInCamelCase();
                c.DescribeAllEnumsAsStrings();
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "API.xml");
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseAuthentication();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(@"/swagger/v1/swagger.json", "Cms Rental");
                c.ShowJsonEditor();
                c.ShowRequestHeaders();
            });

            app.UseMvc();
        }

        private void RegisterDatebase(IServiceCollection services)
        {
            services.AddDbContext<RentalContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<DbContext, RentalContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserRepositoryService, UserRepositoryService>();
            services.AddTransient<IAuthorRepositoryService, AuthorRepositoryService>();
            services.AddTransient<IBookRepositoryService, BookRepositoryService>();
        }
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        private void RegisterToken(IServiceCollection services)
        {

            string secretKey = Configuration["Tokens:Key"];
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var options = new TokenProviderOptions
            {
                Audience = "Rental",
                Issuer = "Rental",
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                Expiration = TimeSpan.FromDays(1)
            };
            services.AddSingleton<IOptions<TokenProviderOptions>>(Options.Create(options));
            services.AddTransient<ITokenProvider, TokenProvider>();

            services.AddAuthentication()
                .AddCookie(cfg => {
                    cfg.SlidingExpiration = true;
                })
                .AddJwtBearer(cfg => {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["Tokens:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = Configuration["Tokens:Issuer"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
            });

        }

        private void RegisterUserServices(IServiceCollection services)
        {
            services.AddTransient<IUserManagementService, UserManagementService>();
        }
    }
}
