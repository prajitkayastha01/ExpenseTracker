using ExpenseTracker.Core.Interfaces;
using ExpenseTracker.Core.Models;
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

        [HttpPost]

        public async Task<IActionResult> AddUserInvestment([FromBody] UserInvestment userInvestment)
        {
            var result = await _userInvestmentService.AddUserInvestment(userInvestment);

            if (result > 0)
            {
                return StatusCode(201,result);
            }
            else
            {
                return StatusCode(500, "Failed to Insert data");
            }
        }
    }
}

