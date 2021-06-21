using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notepad.Application.Interfaces;
using Notepad.Intrastructure.EFCore.Context;

namespace Notepad.Intrastructure.EFCore
{
    public static class DependencyModule
    {
        public static void AddEFCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<NotepadDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(NotepadDbContext).Assembly.FullName)));
            services.AddScoped<INotepadDbContext>(provider => provider.GetService<NotepadDbContext>());
        }
    }
}
