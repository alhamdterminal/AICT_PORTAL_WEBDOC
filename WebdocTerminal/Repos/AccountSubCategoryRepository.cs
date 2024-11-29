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
    public class AccountSubCategoryRepository : RepoBase<AccountSubCategory> , IAccountSubCategoryRepository
    {
        public AccountSubCategoryRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public AccountCategory CheckAccount(long AccountCategoryId ,  long CompanyId  ,  string  FromAccount, string ToAccount)
        {
            var res = Db.AccountCategories.FromSql($"  select   * from AccountCategory where AccountCategoryId={AccountCategoryId} and CompanyId={CompanyId} and (({FromAccount} between FromAccount and ToAccount) or ({ToAccount} between FromAccount  and ToAccount)) and IsDeleted = 0 ").FirstOrDefault();

            return res;
        }

        public AccountSubCategory CheckSubCategory(long CompanyId, string FromAccount, string ToAccount)
        {
            var res = Db.AccountSubCategories.FromSql($" select  * from AccountSubCategory where CompanyId={CompanyId} and (({FromAccount} between FromAccount and ToAccount) or ({ToAccount} between FromAccount  and ToAccount)) and IsDeleted = 0 ").FirstOrDefault();

            return res;
        }

        public AccountSubCategory CheckSubCategoryAvaible(long AccountSubCategoryId, long CompanyId, string FromAccount, string ToAccount)
        {
            var res = Db.AccountSubCategories.FromSql($" select  * from AccountSubCategory where AccountSubCategoryId !={AccountSubCategoryId} and CompanyId={CompanyId} and (({FromAccount} between FromAccount and ToAccount) or ({ToAccount} between FromAccount  and ToAccount)) and IsDeleted = 0 ").FirstOrDefault();

            return res;
        }
    }
}
