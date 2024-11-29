using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Account.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;
using WebdocTerminal.Repos.Interfaces;
using WebdocTerminal.Services;

namespace WebdocTerminal.Repos
{
    public class FinancialYearClosureRepository : RepoBase<FinancialYearClosure>  , IFinancialYearClosureRepository
    {
        public FinancialYearClosureRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }


        public List<FinancialYearClosureViewModel> GetClosureDetail(long CompanyId   , string year)
        {
            var finalres = new List<FinancialYearClosureViewModel>();

            var res = (from voucer in Db.Vouchers
                       from vouchartype in Db.VoucherTypes.Where(x => x.CompanyId == CompanyId && x.Code == "GV").DefaultIfEmpty()
                       from departments in Db.Departments.Where(x => x.CompanyId == CompanyId && x.DepartmentCode == "1000").DefaultIfEmpty()
                       join voucherDetail in Db.VoucherDetails on voucer.VoucherId equals voucherDetail.VoucherId
                       join chartofaccount in Db.ChartOfAccounts on voucherDetail.ChartOfAccountId equals chartofaccount.ChartOfAccountId
                       join accounttype in Db.AccountTypes on chartofaccount.AccountTypeId equals accounttype.AccountTypeId
                       join accountSubCategories in Db.AccountSubCategories on accounttype.AccountSubCategoryId equals accountSubCategories.AccountSubCategoryId
                       join accountCategories in Db.AccountCategories on accountSubCategories.AccountCategoryId equals accountCategories.AccountCategoryId
                       join accountNatures in Db.AccountNatures on accountCategories.AccountNatureId equals accountNatures.AccountNatureId
                       join financialYear in Db.FinancialYear on year equals financialYear.Year
                       where
                       voucer.Year == year
                       && voucer.Status == "Posted"
                       && (accountNatures.Code ==  "E" || accountNatures.Code == "R")

                       && voucer.CompanyId == CompanyId && voucherDetail.CompanyId == CompanyId && chartofaccount.CompanyId == CompanyId
                       && accounttype.CompanyId == CompanyId && accountSubCategories.CompanyId == CompanyId && accountCategories.CompanyId == CompanyId

                       && accountNatures.IsActive == true && accountCategories.IsActive == true && accountSubCategories.IsActive == true 
                       && accounttype.IsActive == true
                       && !Db.FinancialYearClosures.Any(s => s.FinancialYearId == financialYear.FinancialYearId && s.CompanyId == CompanyId)

                       select new FinancialYearClosureViewModel
                       {
                            VoucherType = vouchartype != null ? vouchartype.Code + "_" + vouchartype.Name : "",
                            ServiceType = accountNatures.Code ,
                            AccountName = chartofaccount.AccCode + "_" + chartofaccount.AccountName,
                            DepartName = departments != null ? departments.DepartmentCode + "_" + departments.DepartmentName : "",
                            Credit = voucherDetail.Credit,
                            Debit = voucherDetail.Debit,
                            Narration = "Closing of income statement account for the year ended " + year,

                            //DepartmentId = departments != null ? departments.DepartmentId : 0,
                            ChartOfAccountId = chartofaccount.ChartOfAccountId ,
                            //VoucherServiceTypeId = accountNatures.Code == "E" ? 
                            //                       Db.VoucherServiceTypes.Where(x=> x.Code == "E" && x.CompanyId == CompanyId).LastOrDefault().VoucherServiceTypeId :
                            //                       accountNatures.Code == "R" ?
                            //                       Db.VoucherServiceTypes.Where(x => x.Code == "I" && x.CompanyId == CompanyId).LastOrDefault().VoucherServiceTypeId
                            //                        : 0,

                       }).ToList();


            var Merged = (from resdata in res
                                   group resdata by resdata.AccountName into g
                                   select new FinancialYearClosureViewModel
                                   { 
                                       VoucherType = g.FirstOrDefault().VoucherType,
                                       ServiceType = g.FirstOrDefault().ServiceType,
                                       AccountName = g.FirstOrDefault().AccountName,
                                       DepartName = g.FirstOrDefault().DepartName,
                                       Credit = g.ToList().Sum(x=> x.Credit),
                                       Debit = g.ToList().Sum(x=> x.Debit),
                                       Narration = g.FirstOrDefault().Narration,

                                       //DepartmentId = g.FirstOrDefault().DepartmentId,
                                       ChartOfAccountId = g.FirstOrDefault().ChartOfAccountId,
                                       //VoucherServiceTypeId = g.FirstOrDefault().VoucherServiceTypeId,
                                       Balance = g.ToList().Sum(x => x.Debit) - g.ToList().Sum(x => x.Credit),

                                   }).ToList();

            finalres.AddRange(Merged);

            var resofRetained = Db.ChartOfAccounts.FromSql($"select * from ChartOfAccount where  AccCode = '2990' and IsDeleted = 0   ").LastOrDefault();
             

            if (resofRetained != null)
            {
                var revenueres = finalres.Where(x => x.ServiceType == "R").ToList();

                if (revenueres.Count() > 0)
                {
                    var resforincome = ( from vouchartype in Db.VoucherTypes
                                        from voucherServiceType in Db.VoucherServiceTypes.Where(x => x.CompanyId == CompanyId && x.Code == "G").DefaultIfEmpty()
                                        from departments in Db.Departments.Where(x => x.CompanyId == CompanyId && x.DepartmentCode == "1000").DefaultIfEmpty()
                                        where
                                          vouchartype.CompanyId == CompanyId && vouchartype.Code == "GV"
                                         select new FinancialYearClosureViewModel
                                        {
                                            VoucherType = vouchartype.Code + "_" + vouchartype.Name ,
                                            ServiceType = voucherServiceType != null ? voucherServiceType.Code : "" ,
                                            AccountName = resofRetained.AccCode + "_" + resofRetained.AccountName,
                                            DepartName = departments != null ? departments.DepartmentCode + "_" + departments.DepartmentName : "",

                                            Credit = revenueres.ToList().Sum(x => x.Credit),
                                            Debit = revenueres.ToList().Sum(x => x.Debit),
                                            Narration = "For R Closing of income statement account for the year ended " + year,
                                             
                                            ChartOfAccountId = resofRetained.ChartOfAccountId,
                                            //VoucherServiceTypeId = g.FirstOrDefault().VoucherServiceTypeId,
                                            Balance = revenueres.ToList().Sum(x => x.Debit) - revenueres.ToList().Sum(x => x.Credit),
                                        }).ToList();
                    finalres.AddRange(resforincome);
                }
                
            }




            if (resofRetained != null)
            {
                var expenseres = finalres.Where(x => x.ServiceType == "E").ToList();

                if (expenseres.Count() > 0)
                {
                    var resforincome = (from vouchartype in Db.VoucherTypes
                                        from voucherServiceType in Db.VoucherServiceTypes.Where(x => x.CompanyId == CompanyId && x.Code == "G").DefaultIfEmpty()
                                        from departments in Db.Departments.Where(x => x.CompanyId == CompanyId && x.DepartmentCode == "1000").DefaultIfEmpty()
                                        where vouchartype.CompanyId == CompanyId && vouchartype.Code == "GV"
                                        select new FinancialYearClosureViewModel
                                        {
                                            VoucherType = vouchartype.Code + "_" + vouchartype.Name ,
                                            ServiceType = voucherServiceType != null ? voucherServiceType.Code : "",
                                            AccountName = resofRetained.AccCode + "_" + resofRetained.AccountName,
                                            DepartName = departments != null ? departments.DepartmentCode + "_" + departments.DepartmentName : "",

                                            Credit = expenseres.ToList().Sum(x => x.Credit),
                                            Debit = expenseres.ToList().Sum(x => x.Debit),
                                            Narration = "For E Closing of income statement account for the year ended " + year,

                                            ChartOfAccountId = resofRetained.ChartOfAccountId,
                                            //VoucherServiceTypeId = g.FirstOrDefault().VoucherServiceTypeId,
                                            Balance = expenseres.ToList().Sum(x => x.Debit) - expenseres.ToList().Sum(x => x.Credit),
                                        }).ToList();

                    finalres.AddRange(resforincome);
                }

            }



            return finalres;
        }



    }
}
