using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface IAccountTypeRepository : IRepo<AccountType> 
    {
        AccountSubCategory CheckAccount(long AccountSubCategoryId, long CompanyId, string FromAccount, string ToAccount);
        AccountType CheckType(long CompanyId, string FromAccount, string ToAccount);
        AccountType CheckAccountTypeAvaible(long AccountTypeId, long CompanyId, string FromAccount, string ToAccount);
    }
}
