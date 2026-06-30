using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Core.DTOs;
using ExpenseTracker.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IUserAccountService _userAccountService;

        public AuthController(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginData)
        {
            if (loginData == null || string.IsNullOrEmpty(loginData.Email) || string.IsNullOrEmpty(loginData.Password))
            {
                return BadRequest("Invalid Request.");                
            }
            string token = await _userAccountService.AuthenticateUserAsync(loginData);

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }


            return Ok(new { token = token, message = "login Succeessful" });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userAccountService.RegisterAsync(data);

            if (result.Success)
            {
                return Ok(new { message = result.Message });
            }

            // Return 409 Conflict if email exists, otherwise 500
            if (result.Message.Contains("already registered"))
                return Conflict(new { message = result.Message });

            return StatusCode(500, new { message = result.Message });
        }
    }
}