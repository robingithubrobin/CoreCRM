using CoreCRM.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using CoreCRM.Models;
using Microsoft.AspNetCore.Identity;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace CoreCRM.IntegrationTest
{
    public class TestStartup : Startup
    {
        public TestStartup(IHostingEnvironment env) : base(env)
        {
            string basePath = Directory.GetCurrentDirectory();
            if (!Directory.GetCurrentDirectory().EndsWith("IntegrationTest")) {
                basePath = Path.Combine(basePath, "CoreCRM.IntegrationTest");
            }

            var builder = new ConfigurationBuilder();
            builder.SetBasePath(basePath)
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        protected override void ConfigureDbContext(IServiceCollection services)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            services
              .AddEntityFrameworkSqlite()
              .AddDbContext<ApplicationDbContext>(
                options => options.UseSqlite(connection)
              );
        }

        // method in TestStartup.cs
        protected override void EnsureDatabaseCreated(IApplicationBuilder app, ApplicationDbContext dbContext)
        {
            dbContext.Database.OpenConnection(); // see Resource #2 link why we do this

            // now run the real thing
            base.EnsureDatabaseCreated(app, dbContext);

            // Create table not in migrations
            dbContext.Database.EnsureCreated();

            DatabaseFactory(app, dbContext);
        }

        protected virtual void DatabaseFactory(IApplicationBuilder app, ApplicationDbContext dbContext)
        {
            // Create admin user.
            using (var serviceScope = app.ApplicationServices
                                         .GetRequiredService<IServiceScopeFactory>()
                                         .CreateScope()) {

                var userManager = serviceScope.ServiceProvider
                                              .GetService<UserManager<ApplicationUser>>();
                var roleManager = serviceScope.ServiceProvider
                                              .GetService<RoleManager<IdentityRole>>();

                var user = new ApplicationUser() {
                    UserName = "admin",
                    Email = "admin@example.com",
                    PhoneNumber = "18910053803",
                };

                Task.Run(async () => {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("Employee"));
                    await userManager.CreateAsync(user, "123abC_");
                    await userManager.AddToRoleAsync(user, "Admin");
                }).Wait();
            }
        }
    }
}
