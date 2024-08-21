using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR_Chat.Models;
using SignalR_Chat.Repository;

namespace SignalR_Chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            await _userRepository.AddUserAsync(user);


            return Ok();
        }

        [HttpGet("total")]
        public async Task<IActionResult> GetTotalUsers()
        {
            int totalUsers = await _userRepository.GetTotalUsersAsync();
            return Ok(totalUsers);
        }
    }
}
