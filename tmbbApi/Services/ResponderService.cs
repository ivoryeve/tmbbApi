using tmbbApi.Services.Interfaces;
using tmbbData;

namespace tmbbApi.Services
{
    public class ResponderService : IResponderService
    {
        private readonly IDataAccess _data;
        private readonly IConfiguration _conn;

        public ResponderService(IDataAccess data, IConfiguration conn)
        {
            _data = data;
            _conn = conn;
        }

        public async Task<List<Responders>> GetResponders()
        {
            var _list = await _data.LoadDataRaw<Responders, dynamic>("select * from vw_100_profiles",
                new
                {

                },
                _conn.GetConnectionString("DefaultConnection"));
            return _list;
        }
        public async Task<Responders> GetResponderById(int ResponderId)
        {
            var _entity = await _data.LoadDataRaw<Responders, dynamic>($"select * from vw_100_profiles where pro_uid = {ResponderId}",
                new
                {

                },
                _conn.GetConnectionString("DefaultConnection"));
            return _entity.FirstOrDefault();
        }

        public async Task<List<Responders>> GetRespondersBySearch(string SearchKey)
        {
            var _list = await _data.LoadDataRaw<Responders, dynamic>($"select * from vw_100_profiles where fullname like '%{SearchKey}%'",
                new
                {

                },
                _conn.GetConnectionString("DefaultConnection"));
            return _list;
        }


    }
}
