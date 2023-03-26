using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tmbbApi.Services.Interfaces;

namespace tmbbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponderController : Controller
    {
        private readonly IResponderService _responder1;

        public ResponderController(IResponderService responder1)
        {
            _responder1 = responder1;
        }

        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetResponders()
        {
            try
            {
                var _list = await _responder1.GetResponders();
                return Ok(_list);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("{id:int}")]

        public async Task<IActionResult> GetResponderById(int id)
        {
            try
            {
                var _entity = await _responder1.GetResponderById(id);
                return Ok(_entity);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("{searchKey}")]
        public async Task<IActionResult> GetRespondersBySearch(string searchKey)
        {
            try
            {
                var _list = await _responder1.GetRespondersBySearch(searchKey);
                return Ok(_list);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
    }
}