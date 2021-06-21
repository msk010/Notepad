using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Notepad.Application
{
    public static class DependencyModule
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
