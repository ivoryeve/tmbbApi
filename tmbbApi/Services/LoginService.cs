using tmbbData;
using tmbbApi.Services.Interfaces;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace tmbbApi.Services
{
    public class LoginService : ILoginService
    {
        private readonly IDataAccess _data;
        private readonly IConfiguration _conn;
        private string secretKey;

        public LoginService(IDataAccess data, IConfiguration conn)
        {
            _data = data;
            _conn = conn;
            secretKey = conn.GetValue<string>("Jwt:SecretKey");
        }

        public Users User1 { get; private set; }


        public async Task<Users> LoginUserAsync(string username, string password)
        {
            var hashedPassword = HashPassword(password);
            var _user = await _data.LoadData<Users, dynamic>("sp_user_login",
                new
                {
                    username = username,
                    password = hashedPassword
                },
            _conn.GetConnectionString("DefaultConnection"));
            return _user.FirstOrDefault();
        }

        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hashedPassword = Convert.ToBase64String(hashedBytes);
                return hashedPassword;
            }
        }



        public async Task<string> GenerateTokenAsync(Users user)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.user_uid.ToString()),
                new Claim(ClaimTypes.Name, user.username)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new( new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            User1 = user;

            return tokenHandler.WriteToken(token);

        }

        //public async Task<string> GenerateTokenAsync(Users user)
        //{
        //    var claims = new[]
        //    {
        //        new Claim(ClaimTypes.NameIdentifier, user.user_uid.ToString()),
        //        new Claim(ClaimTypes.Name, user.username)
        //    };

        //    var secretKey = new byte[16];
        //    using (var rng = RandomNumberGenerator.Create())
        //    {
        //        rng.GetBytes(secretKey);
        //    }
        //    var base64SecretKey = Convert.ToBase64String(secretKey);

        //    var key1 = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(base64SecretKey));

        //    var key = Encoding.ASCII.GetBytes(_conn["Jwt:SecretKey"]);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(claims),
        //        Expires = DateTime.UtcNow.AddHours(2),
        //        SigningCredentials = new SigningCredentials(key1, SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var token = tokenHandler.CreateToken(tokenDescriptor);

        //    Save user to User1 property
        //   User1 = user;

        //    return tokenHandler.WriteToken(token);
        //}

    }
}
