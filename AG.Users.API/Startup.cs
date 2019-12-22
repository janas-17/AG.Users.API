using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AG.Users.API.HealthChecks;
using AG.Users.Data.Services;
using AG.Users.Data.Users;
using AG.Users.EFCore;
using AG.Users.EFCore.Models;
using HealthChecks.UI.Client;
using HealthChecks.UI.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace AG.Users.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<UsersContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("default")));

            services.AddScoped<OperatorValidationService>();
            services.AddScoped<UserValidationService<Operator>>();
            services.AddScoped<UserValidationService<Administrator>>();
            services.AddScoped<OperatorRepo>();
            services.AddScoped<AdministratorRepo>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() { Title = "AG Users API", Version = "v1" });
            });

            services.AddMvc();
            services
                .AddHealthChecksUI()
                .AddHealthChecks()
                .AddMemoryHealthCheck("memory")
                .AddSqlServer(Configuration.GetConnectionString("default"))
                .AddDbContextCheck<UsersContext>("DbContextHealthCheck");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AG Users API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting()
                .UseEndpoints(config =>
                {
                    config.MapHealthChecks("/healthz", new HealthCheckOptions
                    {
                        Predicate = _ => true,
                        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                    });

                    config.MapHealthChecksUI(setup =>
                    {
                        setup.UIPath = "/show-health-ui";
                        setup.ApiPath = "/health-ui-api";
                    });

                    config.MapDefaultControllerRoute();
                });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
