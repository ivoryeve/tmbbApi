using System.Collections.Generic;
using System.Threading.Tasks;

namespace tmbbApi.Services.Interfaces
{
    public interface IDataAccess
    {
        Task<List<T>> LoadData<T, U>(string sql, U parameters, string connectionString);
        Task<List<T>> LoadDataRaw<T, U>(string sql, U parameters, string connectionString);
        Task SaveData<T>(string sql, T parameters, string connectionString);
    }
}
 