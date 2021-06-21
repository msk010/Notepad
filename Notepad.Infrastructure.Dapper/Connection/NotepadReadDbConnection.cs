using Dapper;
using Microsoft.Extensions.Configuration;
using Notepad.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Notepad.Infrastructure.Dapper.Connection
{
    public class NotepadReadDbConnection : INotepadReadDbConnection, IDisposable
    {
        private readonly IDbConnection _dbConnection;
        public NotepadReadDbConnection(IDbConnection dbConnection, IConfiguration configuration)
        {
            _dbConnection = dbConnection;
        }

        public void Dispose() => _dbConnection.Dispose();

        public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return (await _dbConnection.QueryAsync<T>(sql, param, transaction)).AsList();
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<T>(sql, param, transaction);
        }

        public async Task<T> QuerySingleAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await _dbConnection.QuerySingleAsync<T>(sql, param, transaction);
        }

        public async Task<IReadOnlyList<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default, string splitOn = "Id")
        {
            return (await _dbConnection.QueryAsync(sql, map, param, transaction, splitOn: splitOn)).AsList();
        }

        public async Task<IReadOnlyList<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default, string splitOn = "Id")
        {
            return (await _dbConnection.QueryAsync(sql, map, param, transaction, splitOn: splitOn)).AsList();
        }
    }
}
