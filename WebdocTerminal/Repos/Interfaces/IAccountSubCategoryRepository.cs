using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
   public interface IAccountSubCategoryRepository : IRepo<AccountSubCategory>
    {
        AccountCategory CheckAccount(long AccountCategoryId, long CompanyId, string FromAccount, string ToAccount);
        AccountSubCategory CheckSubCategory(long CompanyId, string FromAccount, string ToAccount);
        AccountSubCategory CheckSubCategoryAvaible(long AccountSubCategoryId, long CompanyId, string FromAccount, string ToAccount);
    }
}
