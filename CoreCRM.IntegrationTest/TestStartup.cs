using CoreCRM.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoreCRM.IntegrationTest
{
    public class TestStartup : Startup
    {
        public TestStartup(IHostingEnvironment env) : base(env)
        {
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
        protected override void EnsureDatabaseCreated(ApplicationDbContext dbContext)
        {
            dbContext.Database.OpenConnection(); // see Resource #2 link why we do this
            //dbContext.Database.EnsureCreated();

            // now run the real thing
            base.EnsureDatabaseCreated(dbContext);
        }
    }
}
