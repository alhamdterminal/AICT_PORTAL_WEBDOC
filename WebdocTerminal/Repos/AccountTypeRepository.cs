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
    public class AccountTypeRepository : RepoBase<AccountType> , IAccountTypeRepository
    {
        public AccountTypeRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }


        public AccountSubCategory CheckAccount(long AccountSubCategoryId, long CompanyId, string FromAccount, string ToAccount)
        {
            var res = Db.AccountSubCategories.FromSql($" select   * from AccountSubCategory where AccountSubCategoryId={AccountSubCategoryId} and CompanyId={CompanyId} and (({FromAccount} between FromAccount and ToAccount) or ({ToAccount} between FromAccount  and ToAccount)) and IsDeleted = 0 ").FirstOrDefault();

            return res;
        }

        public AccountType CheckType(long CompanyId, string FromAccount, string ToAccount)
        {
            var res = Db.AccountTypes.FromSql($" select   * from AccountType where CompanyId={CompanyId} and (({FromAccount} between FromAccount and ToAccount) or ({ToAccount} between FromAccount  and ToAccount)) and IsDeleted = 0 ").FirstOrDefault();

            return res;
        }

        public AccountType  CheckAccountTypeAvaible(long AccountTypeId, long CompanyId, string FromAccount, string ToAccount)
        {
            var res = Db.AccountTypes.FromSql($" select  * from AccountType where AccountTypeId != {AccountTypeId} and CompanyId={CompanyId} and (({FromAccount} between FromAccount and ToAccount) or ({ToAccount} between FromAccount  and ToAccount)) and IsDeleted = 0 ").FirstOrDefault();

            return res;
        }

    }
}
