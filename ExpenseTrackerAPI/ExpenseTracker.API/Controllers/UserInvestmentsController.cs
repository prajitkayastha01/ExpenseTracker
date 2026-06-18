using ExpenseTracker.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]

    public class UserInvestmentsController: ControllerBase
    {
        private readonly IUserInvestmentService _userInvestmentService;

        public UserInvestmentsController(IUserInvestmentService userInvestmentService)
        {
            _userInvestmentService = userInvestmentService;
        }

        [HttpGet("{userId}")]

        public async Task<IActionResult> GetUserInvestmentsById(int userId)
        {
            var result = await _userInvestmentService.GetUserInvestmentsByUserId(userId);
            return Ok(result);
        }
    }
}

