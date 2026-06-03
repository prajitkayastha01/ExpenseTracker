using ExpenseTracker.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace ExpenseTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAccountController : ControllerBase
    {
        private readonly IUserAccountService _userAccountService;
        public UserAccountController(IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        [HttpGet("{userId}")]
        public IActionResult GetAccountsByUserId(int userId)
        {
            var result = _userAccountService.GetAccountsByUserId(userId);

            if (result == null || result.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
