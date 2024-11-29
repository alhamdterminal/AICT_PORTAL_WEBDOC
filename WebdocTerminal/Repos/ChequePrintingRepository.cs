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
    public class ChequePrintingRepository : RepoBase<ChequePrinting> , IChequePrintingRepository
    {
        public ChequePrintingRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }


        public List<ChequePrinting> GetChequePrintingList(long? companyid , long? userid , DateTime fromdate  , DateTime todate , string search)
        {
            var resdata = new List<ChequePrinting>();


            //var res = (from voucherDetail in Db.VoucherDetails
            //           join voucher in Db.Vouchers on voucherDetail.VoucherId equals voucher.VoucherId
            //           join chartOfAccount in Db.ChartOfAccounts on voucherDetail.ChartOfAccountId equals chartOfAccount.ChartOfAccountId
            //           join VoucherServiceType in Db.VoucherServiceTypes on voucherDetail.VoucherServiceTypeId equals VoucherServiceType.VoucherServiceTypeId
            //           where
            //           voucherDetail.CompanyId == companyid
            //           && voucher.Status == "InProgress"
            //           && VoucherServiceType.Code == "B"
            //           && voucherDetail.Credit > 0
            //           && Convert.ToDateTime(voucher.VoucherDate.Date.ToString("MM/dd/yyyy")) >= Convert.ToDateTime(fromdate.Date.ToString("MM/dd/yyyy"))
            //           && Convert.ToDateTime(voucher.VoucherDate.Date.ToString("MM/dd/yyyy")) <= Convert.ToDateTime(todate.Date.ToString("MM/dd/yyyy"))
            //           && voucher.VoucherNo == search
            //           && voucherDetail.CreatedBy == userid
            //           select new ChequePrinting
            //           {
            //               VoucherId = voucher.VoucherId,
            //               VoucherDetailId = voucherDetail.VoucherDetailId,
            //               VoucherNo = voucher.VoucherNo,
            //               BankName = chartOfAccount.AccountName,
            //               VoucherDate = voucher.VoucherDate,
            //               PartyName = "",
            //               Amount = voucherDetail.Credit,
            //               AmountInWords = "",
            //               Count = Db.ChequePrintings.Where(c => c.VoucherId == voucher.VoucherId && c.VoucherDetailId == voucherDetail.VoucherDetailId).Count(),
            //               ChequeNo = voucherDetail.ChequeOrReference,

            //           }).ToList();

            //resdata.AddRange(res);

            var vouchers = Db.Vouchers.FromSql($"SELECT * From Voucher Where CompanyId = {companyid} and ( cast(VoucherDate as date) >=  cast({fromdate} as date) Or {fromdate} IS  null   or {fromdate} = '' ) and	( cast(Voucher.VoucherDate as date) <=  cast({todate}  as date) Or {todate} IS  null   or {todate} = '' )  and (VoucherNo = {search} Or {search} IS null or {search} = '')   and (CreatedBy = {userid} Or {userid} IS null or {userid} = '') and Status = 'InProgress' and IsDeleted = 0  ").ToList();


            if (vouchers.Count()> 0)
            {
 
                var res = (from voucherDetail in Db.VoucherDetails
                           join voucher in vouchers on voucherDetail.VoucherId equals voucher.VoucherId
                           join chartOfAccount in Db.ChartOfAccounts on voucherDetail.ChartOfAccountId equals chartOfAccount.ChartOfAccountId
                           join VoucherServiceType in Db.VoucherServiceTypes on voucherDetail.VoucherServiceTypeId equals VoucherServiceType.VoucherServiceTypeId
                           where
                           voucherDetail.CompanyId == companyid                           
                           && VoucherServiceType.Code == "B"
                           && voucherDetail.Credit > 0
                        
                           select new ChequePrinting
                           {
                               VoucherId = voucher.VoucherId,
                               VoucherDetailId = voucherDetail.VoucherDetailId,
                               VoucherNo = voucher.VoucherNo,
                               BankName = chartOfAccount.AccountName,
                               VoucherDate = voucher.VoucherDate,
                               PartyName =  "" ,
                               Amount = voucherDetail.Credit,
                               AmountInWords = "",
                               Count = Db.ChequePrintings.Where(c => c.VoucherId == voucher.VoucherId && c.VoucherDetailId == voucherDetail.VoucherDetailId).Count(),
                               ChequeNo = voucherDetail.ChequeOrReference,

                           }).ToList();

                resdata.AddRange(res);
            }

            foreach (var item in resdata)
            {

              var rasd =  Db.ChequePrintings.FromSql($"SELECT * From ChequePrinting Where VoucherId = {item.VoucherId} and  VoucherDetailId = {item.VoucherDetailId}  and IsDeleted = 0  ").LastOrDefault();
               
                if(rasd != null)
                {
                    item.PartyName = rasd.PartyName;
                }  
            }

            return resdata;
        }
    }
}
