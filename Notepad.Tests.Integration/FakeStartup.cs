using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Notepad.Api;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Notepad.Tests.Integration
{
    public class FakeStartup : Startup
    {
        public FakeStartup(IConfiguration configuration) : base(configuration)
        {
        }
        protected override void ConfigureAuthentication(IServiceCollection services)
        {
            //Write your implementation of fake authentication here
            services.AddAuthentication("Basic")
                .AddScheme<AuthenticationSchemeOptions, CustomAuthenticationHandler>("Basic", null);
        }
    }


    public class CustomAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public CustomAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "TEST_USER"),
                };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new System.Security.Principal.GenericPrincipal(identity, null);

            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return Task.FromResult<AuthenticateResult>(AuthenticateResult.Success(ticket));
        }
    }
}
