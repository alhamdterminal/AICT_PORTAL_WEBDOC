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
    public class VoucherRepository : RepoBase<Voucher> , IVoucherRepository
    {
        public VoucherRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public string GetVoucherNo( long companyId , long vocherTypeId , string year , string month)
        {
            var vouchercode = "";
             var vouchertype = Db.VoucherTypes.FromSql($"select * from VoucherType where VoucherTypeId={vocherTypeId}").LastOrDefault();

             var res = Db.Vouchers.FromSql($"select top 1 * from Voucher  where CompanyId = {companyId} and  [Year] = {year} and  [Month]={month} and VoucherTypeId = {vocherTypeId} order by VoucherId desc ").LastOrDefault();
             

            if (res != null)
            { 
                string path = res.VoucherNo;
                int pos = path.LastIndexOf("-") + 1;  
                vouchercode = vouchertype.Code + "-" + year + "-" + month + "-" + (Convert.ToInt64(path.Substring(pos, path.Length - pos)) + 1).ToString().PadLeft(5, '0');
            }
            else
            {
                vouchercode = vouchertype.Code + "-" + year + "-" + month + "-" + "00001";
            }
             
            return vouchercode;           
        }


        public List<VoucherDetail> GetVoucherDetail(long companyId,  long voucherId)
        {
            var res = Db.VoucherDetails.FromSql($"select * from VoucherDetail where  companyId  = {companyId} and voucherId = {voucherId} and IsDeleted = 0   ").ToList();

            return res;
        }

        public Voucher GetVoucherById(long companyId, long voucherId)
        {
            var res = Db.Vouchers.FromSql($"select * from Voucher where  companyId  = {companyId} and voucherId = {voucherId}   ").LastOrDefault();

            return res;
        }

        public VoucherDetail GetVoucherDetailById(long companyId, long voucherdetailid)
        {
            var res = Db.VoucherDetails.FromSql($"select * from VoucherDetail where  companyId  = {companyId} and VoucherDetailId = {voucherdetailid} and IsDeleted = 0  ").LastOrDefault();

            return res;
        }


        public List<Voucher> GetVoucherList(long userId, string fromdate, string todate, string status,string search, long companyId, long voucherTypeId)
        {
            var resdata = new List<Voucher>();
            if (status != null && status != "")
            {
                var vouchers = Db.Vouchers.FromSql($"SELECT * From Voucher Where CompanyId = {companyId} and (VoucherTypeId = {voucherTypeId} Or {voucherTypeId} IS null or {voucherTypeId} = '') and (CreatedBy = {userId} Or {userId} IS null or {userId} = '') and (VoucherNo = {search} Or {search} IS null or {search} = '') and (Voucher.Status = {status}  ) and	( cast(VoucherDate as date) >=  cast({fromdate} as date) Or {fromdate} IS  null   or {fromdate} = '' ) and	( cast(Voucher.VoucherDate as date) <=  cast({todate}  as date) Or {todate} IS  null   or {todate} = '' ) and IsDeleted = 0  ").Include(b => b.VoucherDetails).OrderByDescending(b => b.VoucherId).ToList();
                 
                if(vouchers.Count() > 0)
                { 
                    resdata.AddRange(vouchers);
                }
            }

            else
            {
                if (search != null && search != "")
                {
                    var vouchers = Db.Vouchers.FromSql($"SELECT * From Voucher Where  VoucherNo = {search} and IsDeleted = 0  ").Include(b => b.VoucherDetails).OrderByDescending(b => b.VoucherId).ToList();

                    resdata.AddRange(vouchers);
                }
                else
                {
                    var vouchers = Db.Vouchers.FromSql($"SELECT * From Voucher Where CompanyId = {companyId} and (VoucherTypeId = {voucherTypeId} Or {voucherTypeId} IS null or {voucherTypeId} = '')  and (CreatedBy = {userId} Or {userId} IS null or {userId} = '')   and	( cast(VoucherDate as date) >=  cast({fromdate} as date) Or {fromdate} IS  null   or {fromdate} = '' ) and	( cast(Voucher.VoucherDate as date) <=  cast({todate}  as date) Or {todate} IS  null   or {todate} = '' ) and (VoucherNo = {search} Or {search} IS null or {search} = '') and IsDeleted = 0  ").Include(b => b.VoucherDetails).OrderByDescending(b => b.VoucherId).ToList();

                    resdata.AddRange(vouchers);
                }

                 

            }

            return resdata;
        }


        public Voucher GetVoucherAndVoucherDetailById(long companyId, long voucherId)
        {
            var res = Db.Vouchers.FromSql($"select * from Voucher where  companyId  = {companyId} and voucherId = {voucherId} ").Include(b => b.VoucherDetails).LastOrDefault();

            return res;
        }


        public Voucher GetVoucherByfileName(string filename)
        {
            var res = Db.Vouchers.FromSql($"select * from Voucher where  FileName  = {filename}  ").LastOrDefault();

            return res;
        }


        public List<TransactionViewModel> GetVoucherTransactions(string fromdate , string todate , long compnayId)
        {
            var resdata = new List<TransactionViewModel>();
            var vouchers = Db.Vouchers.FromSql($"SELECT * From Voucher Where CompanyId = {compnayId} and ( cast(VoucherDate as date) >=  cast({fromdate} as date) Or {fromdate} IS  null   or {fromdate} = '' ) and	( cast(Voucher.VoucherDate as date) <=  cast({todate}  as date) Or {todate} IS  null   or {todate} = '' )   and IsDeleted = 0  ").ToList();

            if(vouchers.Count() > 0)
            {
                var res = (from vouchr in vouchers
                           join vd in Db.VoucherDetails on vouchr.VoucherId equals vd.VoucherId
                           join vt in Db.VoucherTypes on vouchr.VoucherTypeId equals vt.VoucherTypeId
                           join vst in Db.VoucherServiceTypes on vd.VoucherServiceTypeId equals vst.VoucherServiceTypeId
                           join coa in Db.ChartOfAccounts on vd.ChartOfAccountId equals coa.ChartOfAccountId
                           join department in Db.Departments on vd.DepartmentId equals department.DepartmentId
                           from customer in Db.Customers.Where(x => x.CustomerId == vd.CustomerId && x.IsActive == true).DefaultIfEmpty()
                           where
                           vt.IsActive == true
                           && vst.IsActive == true 
                           && department.IsActive == true 
                           select new TransactionViewModel
                           {
                               VoucherDetailId = vd.VoucherDetailId,
                               Account = coa.AccCode+" - "+ coa.AccountName,
                               Credit = vd.Credit,
                               Debit = vd.Debit,
                               Department = department.DepartmentCode + " - " + department.DepartmentName,
                               Narration = vd.Narration,
                               Party = customer != null ? customer.Code +" - "+ customer.Name: "",
                               Reference = vd.ChequeOrReference,
                               ServiceType = vst.Code +" - "+ vst.Name,
                               VoucherDate = vouchr.VoucherDate,
                               VoucherNo = vouchr.VoucherNo,
                               VoucherType = vt.Code +" - "+ vt.Name,
                               
                           }).ToList().OrderByDescending(x=> x.VoucherNo);

                resdata.AddRange(res);
            }


            return resdata;
        }
    }
}
