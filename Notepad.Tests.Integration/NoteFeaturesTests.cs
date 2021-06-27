using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Notepad.Api;
using NUnit.Framework;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Notepad.Tests.Integration
{
    public class Tests 
    {
        private TestServer _server;
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            // Arrange
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.json");

            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");

            _server = new TestServer(new WebHostBuilder()
               .UseStartup<FakeStartup>()
               .ConfigureAppConfiguration((context, conf) =>
               {
                   conf.AddJsonFile(configPath);
               })
            );
            _client = _server.CreateClient();
        }

        [Test]
        public async Task GetList_Test()
        {
            // Act
            var response = await _client.GetAsync("api/note/list");
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}