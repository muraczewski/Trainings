using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace RestApiCoreTrainings.IntegrationTests
{
    public abstract class TestBase
    {
        protected readonly TestServer _server;
        protected readonly HttpClient _client;

        protected TestBase()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
            _client.DefaultRequestHeaders
        }
    }
}
