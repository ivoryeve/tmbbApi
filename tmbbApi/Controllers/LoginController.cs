using Microsoft.AspNetCore.Mvc;
using tmbbApi.Services.Interfaces;
using System.Threading.Tasks;
using tmbbData;

namespace tmbbApi.Controllers
{
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginRequest request)
        {
         
            try
            {

                var user = await _loginService.LoginUserAsync(request.username, request.password);
                if (user == null)
                {
                    return Unauthorized();
                }

                var authKey = await _loginService.GenerateTokenAsync(user);
                return Ok(authKey);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }


    public class LoginRequest
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}

