﻿using Microsoft.Data.SqlClient;
using System.Data;

namespace ApiSec.Infrastructure.Persistence.Context
{
    public class MSSQLContext : IDB
    {
        private SqlConnection _conexao = null!;
        private readonly string _con;

        public MSSQLContext(IDBConfiguration config)
        {
            _con = config.ConnectionString;
        }
        public void Dispose()
        {
            if (_conexao != null)
            {
                if (_conexao.State == ConnectionState.Open)
                    _conexao.Close();
                _conexao.Dispose();
            }
        }

        public async Task<IDbConnection> GetConAsync()
        {
            return await Task.Run(() => { return _conexao = new SqlConnection(_con); });
        }
    }
}
