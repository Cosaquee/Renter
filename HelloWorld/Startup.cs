using AutoMapper;
using Database;
using Database.Interfaces;
using Database.Services;
using HelloWorld.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Models.AutoMapperProfiles;
using Services.UserServices;
using Services.UserServices.Interfaces;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace HelloWorld
{
    public partial class Startup
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
                c.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:8080")
                .AllowAnyHeader()
                .AllowAnyMethod());
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
            var dbContextOptions = new DbContextOptionsBuilder<RentalContext>();
            dbContextOptions.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            services.AddDbContext<RentalContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton<DbContext>(new RentalContext(dbContextOptions.Options));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserRepositoryService, UserRepositoryService>();
            services.AddTransient<IAuthorRepositoryService, AuthorRepositoryService>();
            services.AddTransient<IBookRepositoryService, BookRepositoryService>();
            services.AddTransient<IRefreshTokenRepositoryService, RefreshTokenRepositoryService>();
            services.AddTransient<IRoleRepositoryService, RoleRepositoryService>();
            services.AddCors();
        }
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        private void RegisterUserServices(IServiceCollection services)
        {
            services.AddTransient<IUserManagementService, UserManagementService>();
        }
    }
}
