using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Account.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface IChartOfAccountRepository : IRepo<ChartOfAccount>
    {
        List<chartOfAccountViewModel> GetChartOfAccounts(long companyid);

        ChartOfAccount GetChartOfAccountbyId(long id);

        ChartOfAccount chartofAccountResults(long CompanyId, string AccountName, string AccCode, long ChartOfAccountId);

        AccountType CheckAccountType(long AccountTypeId, long CompanyId);
    }
}
