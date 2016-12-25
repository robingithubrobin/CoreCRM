using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace CoreCRM.IntegrationTest
{
    public abstract class AbstractIntegrationTest : IClassFixture<IClassFixture<TestStarting>>
    {
        public readonly HttpClient client;
        public readonly TestServer server;

        // here we get our testServerFixture, also see above IClassFixture.
        protected AbstractIntegrationTest(TestFixture<TestStartup> fixture)
        {
            client = fixture.Client;
            server = fixture.Server;
        }
    }
}
