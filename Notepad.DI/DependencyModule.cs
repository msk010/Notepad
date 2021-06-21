using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notepad.Application.Interfaces;
using Notepad.Intrastructure.EFCore.Context;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Notepad.DI
{
    public static class DependencyModule
    {
        public static void ConfigureInternalDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<NotepadDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(NotepadDbContext).Assembly.FullName)));
            services.AddScoped<INotepadDbContext>(provider => provider.GetService<NotepadDbContext>());

            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
