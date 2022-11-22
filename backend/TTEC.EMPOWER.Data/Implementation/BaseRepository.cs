using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTEC.EMPOWER.Data.Implementation
{
    [ExcludeFromCodeCoverage]
    public abstract class BaseRepository
    {
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;

        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        // use for buffered queries that return a type
        protected async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> getData)
        {
            try
            {
                await using var connection = new OracleConnection(_connectionString);
                await connection.OpenAsync();
                return await getData(connection);
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a Oracle timeout", GetType().FullName), ex);
            }
            catch (OracleException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a Oracle exception (not a timeout)", GetType().FullName), ex);
            }
        }

        // use for buffered queries that do not return a type
        protected async Task WithConnection(Func<IDbConnection, Task> getData)
        {
            try
            {
                await using var connection = new OracleConnection(_connectionString);
                await connection.OpenAsync();
                await getData(connection);
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a Oracle timeout", GetType().FullName), ex);
            }
            catch (OracleException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a Oracle exception (not a timeout)", GetType().FullName), ex);
            }
        }

        //use for non-buffered queries that return a type
        protected async Task<TResult> WithConnection<TRead, TResult>(Func<IDbConnection, Task<TRead>> getData, Func<TRead, Task<TResult>> process)
        {
            try
            {
                await using var connection = new OracleConnection(_connectionString);
                await connection.OpenAsync();
                var data = await getData(connection);
                return await process(data);
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a Oracle timeout", GetType().FullName), ex);
            }
            catch (OracleException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a Oracle exception (not a timeout)", GetType().FullName), ex);
            }
        }

    }
}
