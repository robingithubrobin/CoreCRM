using System;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace CoreCRM.IntegrationTest
{
    public class TestFixture<TStartup> : IDisposable where TStartup : class
    {
        public TestFixture()
        {
            string contentRoot;
            if (Directory.GetCurrentDirectory().EndsWith("IntegrationTest")) {
                contentRoot = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "CoreCRM"));
            } else {
                contentRoot = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "CoreCRM"));
            }

            var builder = new WebHostBuilder()
                .UseEnvironment("Development")
                .UseContentRoot(contentRoot)
                .UseStartup<TStartup>();
            Server = new TestServer(builder);

            Client = Server.CreateClient();
            Client.BaseAddress = new Uri("http://localhost:5000");
        }

        public TestServer Server { get; }

        public HttpClient Client { get; }

        public void Dispose()
        {
            Client.Dispose();
            Server.Dispose();
        }
    }
}
