using Dapper;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using tmbbApi.Services.Interfaces;

namespace tmbbApi.Services
{
    public class DataAccess : IDataAccess
    {
        public async Task<List<T>> LoadData<T, U>(string sql, U parameters, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var rows = await connection.QueryAsync<T>(sql, parameters, commandType: CommandType.StoredProcedure);

                return rows.ToList();
            }
        }

        public async Task<List<T>> LoadDataRaw<T, U>(string sql, U parameters, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var rows = await connection.QueryAsync<T>(sql, parameters, commandType: CommandType.Text);

                return rows.ToList();
            }
        }

        public Task SaveData<T>(string sql, T parameters, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
