using StockMarket.Chat.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StockMarket.Chat.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _service;

        public AuthenticationController(IAuthenticationService service)
        {
            _service = service;
        }

        [HttpPost("login/{username}/{password}")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var serviceResult = await _service.Login(username, password);

            if (serviceResult.Successful)
            {
                return Ok(serviceResult);
            }
            return BadRequest(serviceResult);
        }
    }
}

