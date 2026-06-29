using System.Security.Claims;
using ExpenseTracker.Core.Interfaces;
using ExpenseTracker.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController: ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
            {
                _transactionService = transactionService;
            }

        [HttpGet("transactions")]
        public async Task<IActionResult> GetTransactions()
        {
            // 1. Extract the UserId directly from the verified Token Claims Principle
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized(new { message = "User identity claim not found in token." });
            }

            int userAccountId = int.Parse(userIdClaim.Value);

            var result = await _transactionService.GetTransactionByUserAccountId(userAccountId);

            if (result == null || result.Count == 0)
            {
                return NotFound();
            }else 
            { 
                return Ok(result);
            }
        }

        [HttpGet("balance/{userAccountId}")]
        public async Task<IActionResult> GetBalance(int userAccountId)
        { 
            var result = await _transactionService.GetBalance(userAccountId);

            return Ok(result);

        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromBody] Transaction transaction)
        {
            var result = await _transactionService.AddTransactionAsync(transaction);
            if (result > 0)
            {
                return Ok(result);
            }
            else if (result == -1)
            {
                return StatusCode(500, "Failed to Insert data");          
            }
            return BadRequest("Failed to add transaction.");
        }

        [HttpDelete ("{id}")]

        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var result = await _transactionService.DeleteTransaction(id);
            if (result > 0)
            {
                return Ok(result);
            }
            else if (result == -1)
            {
                return StatusCode(500, "Failed to delete transaction");
            }
            return NotFound();
        }
    }
}
