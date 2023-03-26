using tmbbData;

namespace tmbbApi.Services.Interfaces
{
    public interface IResponderService
    {
        Task<List<Responders>> GetResponders();
        Task<Responders> GetResponderById (int ResponderId);
        Task<List<Responders>> GetRespondersBySearch(string SearchKey);
    }
}
