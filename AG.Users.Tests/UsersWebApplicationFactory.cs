using AG.Users.API;
using AG.Users.EFCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq;

namespace AG.Users.Tests
{
    public class UsersWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            return base.CreateHostBuilder()
                       .UseEnvironment("IntegrationTests");
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                // Remove the app's ApplicationDbContext registration.
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<UsersContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                // Create in memory dbContext for testing
                services.AddDbContext<UsersContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryAppDb");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var appDb = scopedServices.GetRequiredService<UsersContext>();

                    var logger = scopedServices.GetRequiredService<ILogger<UsersWebApplicationFactory<TStartup>>>();

                    appDb.Database.EnsureCreated();

                    try
                    {
                        // Seed in memory db with test data.
                        DatabaseSeeder.SeedTestData(appDb);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            "database with test messages. Error: {ex.Message}");
                    }
                }
            });
        }
    }
}
