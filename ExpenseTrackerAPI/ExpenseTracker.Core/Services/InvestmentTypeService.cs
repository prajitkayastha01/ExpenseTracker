using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseTracker.Core.Interfaces;
using ExpenseTracker.Core.Models;

namespace ExpenseTracker.Core.Services
{
    public class InvestmentTypeService: IInvestmentTypeService
    {

        private readonly IInvestmentTypeRepository _investmentTypeRepository;

        public InvestmentTypeService(IInvestmentTypeRepository investmentTypeRepostitory)
        {
            _investmentTypeRepository = investmentTypeRepostitory;
        }

        public List<InvestmentType> GetAllInvestmentTypes()
        {
            return _investmentTypeRepository.GetAllInvestmentTypes();
        }
    }
}
