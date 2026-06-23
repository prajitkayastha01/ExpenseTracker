using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Core.DTOs;
using ExpenseTracker.Data;
using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserAccountRepository _userAccountRepository;

        public AuthController(IUserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            int userId = await _userAccountRepository.ValidateUserCredentialsAsync(request);

            if (userId > 0)
            {
                // need to add logic for JWT
                // passing unencoded example token
                return Ok(new { token = $"MOCK-JWT-TOKEN-FOR-USER-{userId}", userId = userId });
            }

            return Unauthorized(new { message = "Invalid email or password" });
        }
    }
}