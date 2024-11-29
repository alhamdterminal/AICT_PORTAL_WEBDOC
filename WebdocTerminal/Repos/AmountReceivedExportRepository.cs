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
    public class AmountReceivedExportRepository : RepoBase<AmountReceivedExport> , IAmountReceivedExportRepository
    {
        public AmountReceivedExportRepository(IUserResolveService userResolveService) : base(userResolveService)
        {

        }

        public AmountReceivedViewmodel AmountReceivedByInvoice(long invoiceExportId)
        {
            var inv = Db.InvoiceExports.Find(invoiceExportId);

            //var inv_count = (from invoiceExport in Db.InvoiceExports
            //                 where invoiceExport.InvoiceNo == inv.InvoiceNo
            //                 select invoiceExport.InvoiceExportId).Count();

            var received = new AmountReceivedViewmodel
            {
                InvoiceExportId = inv.InvoiceExportId,
                AmountReceived = Convert.ToInt32(inv.GrandTotal),
                BillAmount = Convert.ToInt32(inv.GrandTotal),
                BalanceAmount = Convert.ToInt32(inv.AfterDiscount),
                SalesTax = Convert.ToInt32(inv.AfterSalesTax)
            };

            //var amountReceivedExport = Db.AmountReceivedExports
            //    .OrderByDescending(r => r.AmountReceivedExportId)
            //    .FirstOrDefault(r => r.InvoiceExport.InvoiceNo == inv.InvoiceNo);

            //if (amountReceivedExport != null)
            //{
            //    received.BalanceAmount = amountReceivedExport.BalanceAmount;
            //}

            return received;
        }
    }
}
