using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notepad.Application.Interfaces;
using Notepad.Infrastructure.Dapper.Connection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Notepad.Infrastructure.Dapper
{
    public static class DependencyModule
    {
        public static void AddDapper(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDbConnection>(db => new SqlConnection(
                    configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<INotepadReadDbConnection, NotepadReadDbConnection>();
        }
    }
}
