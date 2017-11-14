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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Cms Rental API", Version = "v1"});
                c.DescribeAllParametersInCamelCase();
                c.DescribeStringEnumsInCamelCase();
                c.DescribeAllEnumsAsStrings();
                
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
            services.AddSingleton<DbContext, RentalContext>();
            services.AddDbContext<RentalContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserRepositoryService, UserRepositoryService>();
        }
    }
}
