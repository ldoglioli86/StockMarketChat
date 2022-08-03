using StockMarket.Chat.Models;
using StockMarket.Chat.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobsityChatService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatUsersController : ControllerBase
    {
        private readonly IChatUserService _service;

        public ChatUsersController(IChatUserService service)
        {
            _service = service;
        }

        [HttpGet(Name = "GetUsers")]
        [Authorize]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await _service.GetUsers();
                return Ok(users);
            }
            catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(ChatUserDto userForCreateDto)
        {
            var userFromRepo = await _service.GetUserByUserName(userForCreateDto.UserName);

            if (userFromRepo != null)
            {
                return Conflict("The user  " + userForCreateDto.UserName + " already exists.");
            }

            var userForCreate =new ChatUser {
                UserName = userForCreateDto.UserName,
                Email = userForCreateDto.Email,
                FirstName = userForCreateDto.FirstName,
                LastName = userForCreateDto.LastName                
            };

            try
            {
                await _service.CreateUser(userForCreate, userForCreateDto.Password);
                return Ok(string.Format("User {0} was created succesfully.", userForCreateDto.UserName));
            }
            catch (Exception e) {
                return BadRequest(e.Message);
            }
        }
    }
}
