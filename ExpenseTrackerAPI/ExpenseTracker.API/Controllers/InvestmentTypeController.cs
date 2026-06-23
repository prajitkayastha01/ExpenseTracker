using ExpenseTracker.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class InvestmentTypeController: ControllerBase
    {
        private readonly IInvestmentTypeService _investmentTypeService;

        public InvestmentTypeController(IInvestmentTypeService investmentTypeService)
        {
            _investmentTypeService = investmentTypeService;
        }

        [HttpGet]

        public IActionResult GetAllInvestmentTypes()
        {
            var result = _investmentTypeService.GetAllInvestmentTypes();

            if (result == null || result.Count == 0)
            {
                return NotFound();
            }else
            {
                return Ok(result);
            }

        }
    }
}
