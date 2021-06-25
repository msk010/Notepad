using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Notepad.Application.Configs;
using Notepad.Application.Features.UserFeatures.Commands;
using Notepad.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Notepad.Api.Middlewares
{
    public class UserContextMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<UserContextMiddleware> _logger;

        public UserContextMiddleware(RequestDelegate next, ILogger<UserContextMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext httpContext, IUserContext userContext, IMediator mediator)
        {            
            if (httpContext.User.Identity is ClaimsIdentity claimsIdentity && httpContext.User.Identity.IsAuthenticated)
            {
                string login = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                string email = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                string firstName = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
                string lastName = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;
                int? userId = httpContext.Session.GetInt32("UserId");

                userContext.Create(userId, login, email, firstName, lastName);

                if (!userId.HasValue)
                {
                    userId = await mediator.Send(new SaveCurrentUserCommand());
                    httpContext.Session.SetInt32("UserId", userId.Value);
                    userContext.SetUserId(userId.Value);
                }

            }

            await _next.Invoke(httpContext);
        }
    }
}
