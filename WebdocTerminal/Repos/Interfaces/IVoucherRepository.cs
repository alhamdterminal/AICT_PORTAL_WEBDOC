using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebdocTerminal.Areas.Account.Models;
using WebdocTerminal.Models;
using WebdocTerminal.Repos.Base;

namespace WebdocTerminal.Repos.Interfaces
{
    public interface IVoucherRepository : IRepo<Voucher>
    {
        string GetVoucherNo(long companyId, long vocherTypeId, string year, string month);
        List<VoucherDetail> GetVoucherDetail(long companyId, long voucherId);
        Voucher GetVoucherById(long companyId, long voucherId);

        VoucherDetail GetVoucherDetailById(long companyId, long voucherdetailid);

        List<Voucher> GetVoucherList(long userId, string fromdate, string todate, string status , string search, long companyId , long voucherTypeId);

        Voucher GetVoucherAndVoucherDetailById(long companyId, long voucherId);
        Voucher GetVoucherByfileName(string filename);

        List<TransactionViewModel> GetVoucherTransactions(string fromdate, string todate, long compnayId);
    }
}
