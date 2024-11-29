using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface IAccountCategoryRepository : IRepo<AccountCategory>
    {
        bool GetAccountCategory(long companyid, string fromaccount, string toaccount);

        AccountCategory GetAccountCategorybyId(long id, long companyid, string fromaccount, string toaccount);
    }
}
