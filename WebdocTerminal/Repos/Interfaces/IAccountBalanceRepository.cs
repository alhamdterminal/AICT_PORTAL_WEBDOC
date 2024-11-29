using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface IAccountBalanceRepository : IRepo<AccountBalance>
    {
        AccountBalance GetAccountBalanceforcustomer(long CustomerId, long companyId);

        AccountBalance CheckAccountBalance(long ChartOfAccountId, long companyId);

        AccountBalance GetAccountBalanceById(long AccountBalanceId, long companyId);

        AccountBalance CheckAccountBalanceByCustomerId(long AccountBalanceId, long CustomerId, long companyId);
        AccountBalance CheckAccountBalanceByChartOfAccountId(long AccountBalanceId, long ChartOfAccountId, long companyId);
    }
}
