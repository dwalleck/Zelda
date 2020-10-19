using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Zelda.Api.IntegrationTests.Controllers
{
    public class LinksControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        public LinksControllerTests(WebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateDefaultClient();
        }

        [Fact]
        public async Task GetAllLinks_ReturnsOk()
        {
            var response = await _client.GetAsync("api/links");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAllLinks_ReturnsContentType()
        {
            var response = await _client.GetAsync("api/links");
            Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
        }

        
    }
}
