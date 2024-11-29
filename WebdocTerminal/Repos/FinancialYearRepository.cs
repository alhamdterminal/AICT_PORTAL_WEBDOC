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
    public class FinancialYearRepository : RepoBase<FinancialYear> , IFinancialYearRepository
    {
        public FinancialYearRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public List<FinancialYear> GetPenddingFinancialYears(long companyId)
        {
            var res = (from financialYear in Db.FinancialYear
                       where
                       financialYear.CompanyId == companyId
                       && !Db.FinancialYearClosures.Any(s => s.FinancialYearId == financialYear.FinancialYearId && s.CompanyId == companyId)
                       select financialYear).ToList();

            return res;
        }
    }
}
