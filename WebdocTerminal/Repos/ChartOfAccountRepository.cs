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
    public class ChartOfAccountRepository : RepoBase<ChartOfAccount> , IChartOfAccountRepository
    {
        public ChartOfAccountRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public List<chartOfAccountViewModel> GetChartOfAccounts(long companyid )
        {
            var res = (
                from charofaccount in Db.ChartOfAccounts
                join accountType in  Db. AccountTypes on charofaccount.AccountTypeId equals accountType.AccountTypeId
                join accountSubCategory in  Db. AccountSubCategories on accountType.AccountSubCategoryId equals accountSubCategory.AccountSubCategoryId
                join voucherServiceTypeDetail in  Db. VoucherServiceTypeDetails on charofaccount.ChartOfAccountId equals voucherServiceTypeDetail.ChartOfAccountId

                where
                charofaccount.Status == true && charofaccount.CompanyId == companyid
                select new chartOfAccountViewModel {
                    SubCategory = accountSubCategory.Name,
                    AccountTypeId = accountType.AccountTypeId,
                    AccCode = charofaccount.AccCode,
                    AccountName = charofaccount.AccountName,
                    ChartOfAccountId = charofaccount.ChartOfAccountId,
                    AccPCode = charofaccount.AccPCode,
                    CompanyId = charofaccount.CompanyId,
                    CreatedAt = charofaccount.CreatedAt,
                    CreatedBy = charofaccount.CreatedBy,
                    DeleteDate = charofaccount.DeleteDate,
                    EditedAt = charofaccount.EditedAt,
                    EditedBy = charofaccount.EditedBy,
                    IsDeleted = charofaccount.IsDeleted,
                    Status = charofaccount.Status,
                    VoucherServiceTypeId = voucherServiceTypeDetail.VoucherServiceTypeId
                }).ToList();

            return res;
        }

        public ChartOfAccount GetChartOfAccountbyId(long id)
        {
            var res = Db.ChartOfAccounts.FromSql($"select   * from ChartOfAccount where ChartOfAccountId ={id}  and IsDeleted = 0 ").LastOrDefault();

            return res;
        }

        public ChartOfAccount chartofAccountResults(long CompanyId , string AccountName , string AccCode , long ChartOfAccountId)
        {
            var res = Db.ChartOfAccounts.FromSql($"select   * from ChartOfAccount where CompanyId = {CompanyId} and AccountName = {AccountName} and AccCode = {AccCode} and  ChartOfAccountId != {ChartOfAccountId}  and IsDeleted = 0 ").LastOrDefault();

            return res;
        }

        public AccountType CheckAccountType(long AccountTypeId, long CompanyId)
        {
            var res = Db.AccountTypes.FromSql($"select * from AccountType where CompanyId = {CompanyId}  and  AccountTypeId = {AccountTypeId} and IsActive = 1 and IsDeleted = 0 ").LastOrDefault();

            return res;
        }
    }
}
