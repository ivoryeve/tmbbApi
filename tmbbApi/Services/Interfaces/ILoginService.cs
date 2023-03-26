using tmbbData;

namespace tmbbApi.Services.Interfaces
{
    public interface ILoginService
    {
        Task<string> GenerateTokenAsync(Users user);
        Task<Users> LoginUserAsync (string username, string password);
        string HashPassword(string password);

        Users User1 { get;  }
    }
}
