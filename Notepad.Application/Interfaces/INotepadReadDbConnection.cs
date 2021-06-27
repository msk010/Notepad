using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Notepad.Application.Interfaces
{
    public interface INotepadReadDbConnection
    {
        Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default, string splitOn = "Id");
        Task<IReadOnlyList<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default, string splitOn = "Id");
        Task<IReadOnlyList<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default, string splitOn = "Id");

        Task<IReadOnlyList<T>> QueryAsync<T>(string sql, Type[] types, Func<object[], T> map, object param = null, IDbTransaction transaction = null, string splitOn = "Id");

        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default);
        Task<T> QuerySingleAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default);
    }
}
