using AutoMapper;
using Database;
using Database.Interfaces;
using Database.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Models.AutoMapperProfiles;
using Renter.Models;
using Services.UserServices;
using Services.UserServices.Interfaces;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Threading.Tasks;

namespace Renter
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
            services.AddMvc()
                .AddJsonOptions(
                    options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );;
            RegisterDatebase(services);
            RegisterToken(services);
            RegisterUserServices(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Cms Rental API", Version = "v1" });
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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDbInitializer dbInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            Task.Run(dbInitializer.Seed).Wait();

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
            dbContextOptions.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            
            services.AddScoped<DbContext>((provider) =>
            {
                return new RentalContext(dbContextOptions.Options);
            });

            services.AddScoped<IAuthorRepositoryService, AuthorRepositoryService>();
            services.AddScoped<IBookRatingRepositoryService, BookRatingRepositoryService>();
            services.AddScoped<IBookRepositoryService, BookRepositoryService>();
            services.AddScoped<ICategoryRepositoryService, CategoryRepositoryService>();
            services.AddScoped<IDirectorRepositoryService, DirectorRepositoryService>();
            services.AddScoped<IMovieRatingRepositoryService, MovieRatingRepositoryService>();
            services.AddScoped<IMovieRepositoryService, MovieRepositoryService>();
            services.AddScoped<IRefreshTokenRepositoryService, RefreshTokenRepositoryService>();
            services.AddScoped<IRentBookRepositoryService, RentBookRepositoryService>();
            services.AddScoped<IRentMovieRepositoryService, RentMovieRepositoryService>();
            services.AddScoped<IRoleRepositoryService, RoleRepositoryService>();
            services.AddScoped<ISubscriptionRepositoryService, SubscriptionRepositoryService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepositoryService, UserRepositoryService>();
            services.AddScoped<IUserSubscriptionRepositoryService, UserSubscriptionRepositoryService>();
            services.AddScoped<IDbInitializer, DbInitializer>();

            services.AddCors();
        }

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        private void RegisterUserServices(IServiceCollection services)
        {
            services.AddTransient<IUserManagementService, UserManagementService>();
        }
    }
}
