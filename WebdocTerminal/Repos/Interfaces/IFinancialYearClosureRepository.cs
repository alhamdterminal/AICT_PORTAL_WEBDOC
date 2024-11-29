using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Account.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface IFinancialYearClosureRepository : IRepo<FinancialYearClosure>
    {
        List<FinancialYearClosureViewModel> GetClosureDetail(long CompanyId, string year);
    }
}
