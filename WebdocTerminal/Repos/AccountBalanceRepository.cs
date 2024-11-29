using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;

namespace WebdocTerminal.Repos
{
    public class AccountBalanceRepository : RepoBase<AccountBalance> , IAccountBalanceRepository
    {
        public AccountBalanceRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public AccountBalance GetAccountBalanceforcustomer(long CustomerId , long companyId)
        {
            var res = Db.AccountBalances.FromSql($"select * from AccountBalance where CustomerId ={CustomerId}  and CompanyId = {companyId} and  IsActive = 1 and IsDeleted = 0 ").LastOrDefault();
              
            return res;
        }


        public AccountBalance CheckAccountBalance(long ChartOfAccountId, long companyId)
        {
            var res = Db.AccountBalances.FromSql($"select * from AccountBalance where ChartOfAccountId ={ChartOfAccountId}  and CompanyId = {companyId} and  IsActive = 1 and IsDeleted = 0 ").LastOrDefault();

            return res;
        }

        public AccountBalance GetAccountBalanceById(long AccountBalanceId, long companyId)
        {
            var res = Db.AccountBalances.FromSql($"select * from AccountBalance where AccountBalanceId ={AccountBalanceId}  and CompanyId = {companyId} and  IsActive = 1 and IsDeleted = 0 ").LastOrDefault();

            return res;
        }


        public AccountBalance CheckAccountBalanceByCustomerId(long AccountBalanceId, long CustomerId, long companyId)
        {
            var res = Db.AccountBalances.FromSql($"select * from AccountBalance where AccountBalanceId != {AccountBalanceId}  and CustomerId = {CustomerId} and CompanyId = {companyId} and  IsActive = 1 and IsDeleted = 0 ").LastOrDefault();

            return res;
        }

        public AccountBalance CheckAccountBalanceByChartOfAccountId(long AccountBalanceId, long ChartOfAccountId, long companyId)
        {
            var res = Db.AccountBalances.FromSql($"select * from AccountBalance where AccountBalanceId != {AccountBalanceId}  and ChartOfAccountId = {ChartOfAccountId} and CompanyId = {companyId} and  IsActive = 1 and IsDeleted = 0 ").LastOrDefault();

            return res;
        }
    }
}
