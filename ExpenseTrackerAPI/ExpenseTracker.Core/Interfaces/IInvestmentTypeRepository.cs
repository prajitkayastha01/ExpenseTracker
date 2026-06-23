using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseTracker.Core.Models;

namespace ExpenseTracker.Core.Interfaces
{
    public interface IInvestmentTypeRepository
    {
        List<InvestmentType> GetAllInvestmentTypes();
    }
}
