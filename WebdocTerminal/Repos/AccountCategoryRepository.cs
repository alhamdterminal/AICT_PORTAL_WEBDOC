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
    public class AccountCategoryRepository : RepoBase<AccountCategory> , IAccountCategoryRepository
    {
        public AccountCategoryRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public bool GetAccountCategory(long companyid, string fromaccount, string toaccount)
        {
            var res = Db.AccountNatures.FromSql($"select * from AccountCategory where CompanyId= {companyid} and ( ({fromaccount} between FromAccount and ToAccount) or ({toaccount} between FromAccount  and ToAccount)) and IsDeleted = 0").FirstOrDefault();

            if (res != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        

        public AccountCategory GetAccountCategorybyId(long id , long companyid , string fromaccount , string toaccount)
        {
            var res = Db.AccountCategories.FromSql($"select   * from AccountCategory where AccountCategoryId !={id} and CompanyId={companyid} and (({fromaccount} between FromAccount and ToAccount) or ({toaccount} between FromAccount  and ToAccount)) and IsDeleted = 0 ").FirstOrDefault();

            return res;
        }

    }
}
