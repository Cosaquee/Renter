using AutoMapper;
using Database;
using Database.Interfaces;
using Database.Services;
using Renter.Models;
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
                );
            RegisterDatebase(services);
            RegisterToken(services);
            RegisterUserServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDbInitializer dbInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            dbInitializer.Seed();

            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:8080")
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseAuthentication();

            app.UseMvc();
        }

        private void RegisterDatebase(IServiceCollection services)
        {
            var dbContextOptions = new DbContextOptionsBuilder<RentalContext>();
            dbContextOptions.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

            // services.AddDbContext<RentalContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);

            services.AddScoped<DbContext>((provider) =>
            {
                return new RentalContext(dbContextOptions.Options);
            }
            );


            services.AddTransient<IAuthorRepositoryService, AuthorRepositoryService>();
            services.AddTransient<IBookRatingRepositoryService, BookRatingRepositoryService>();
            services.AddTransient<IBookRepositoryService, BookRepositoryService>();
            services.AddTransient<ICategoryRepositoryService, CategoryRepositoryService>();
            services.AddTransient<IDirectorRepositoryService, DirectorRepositoryService>();
            services.AddTransient<IMovieRatingRepositoryService, MovieRatingRepositoryService>();
            services.AddTransient<IMovieRepositoryService, MovieRepositoryService>();
            services.AddTransient<IRefreshTokenRepositoryService, RefreshTokenRepositoryService>();
            services.AddTransient<IRentBookRepositoryService, RentBookRepositoryService>();
            services.AddTransient<IRentMovieRepositoryService, RentMovieRepositoryService>();
            services.AddTransient<IRoleRepositoryService, RoleRepositoryService>();
            services.AddTransient<ISubscriptionRepositoryService, SubscriptionRepositoryService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserRepositoryService, UserRepositoryService>();
            services.AddTransient<IUserSubscriptionRepositoryService, UserSubscriptionRepositoryService>();
            services.AddTransient<IDbInitializer, DbInitializer>();

            services.AddCors();
        }
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        private void RegisterUserServices(IServiceCollection services)
        {
            services.AddTransient<IUserManagementService, UserManagementService>();
        }
    }
}
